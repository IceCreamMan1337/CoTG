using System.Linq;
using System.Numerics;
using CoTGEnumNetwork;
using CoTGEnumNetwork.Enums;
using CoTGLibrary.GameObjects;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.GameObjects.StatsNS;
using CoTG.CoTGServer.Scripting.CSharp;
using CoTGLibrary.Vision;
using static PacketVersioning.PktVersioning;
using MoonSharp.Interpreter;

namespace CoTG.CoTGServer.GameObjects;

public class NeutralMinion : Minion
{
    public NeutralMinionCamp Camp { get; private set; }
    public string SpawnAnimation { get; private set; }
    public override bool IsAffectedByFoW => true;
    public override bool SpawnShouldBeHidden => true;

    // Leashing properties
    private Vector3 _originalPosition;
    private Vector3 _lastKnownTargetPosition;
    private const float LEASH_RADIUS = 850f;
    private const float LEASH_PROTECTION_RADIUS = 750f;
    private const float SEARCH_DURATION = 3f; // Time in seconds to search for target after losing it
    private const float SEARCH_RADIUS = 300f; // Radius to search for new targets at last known position
    private float _searchTimer = 0f;
    private bool _isSearching = false;
    private bool _isReturning = false; // Track if minion is returning to camp

    private bool IsCampLeader
    {
        get
        {
            // Always pick the first alive minion as leader
            return Camp.Minions.FirstOrDefault(m => m is NeutralMinion nm && !nm.Stats.IsDead) == this;
        }
    }

    private NeutralMinion GetCampLeader()
    {
        return Camp.Minions.FirstOrDefault(m => m is NeutralMinion nm && !nm.Stats.IsDead) as NeutralMinion;
    }

    public NeutralMinion
    (
        string name,
        string model,
        Vector3 position,
        Vector3 faceDirection,
        NeutralMinionCamp monsterCamp,
        TeamId team = TeamId.TEAM_NEUTRAL,
        string spawnAnimation = "",
        bool isTargetable = true,
        bool ignoresCollision = false,
        Stats? stats = null,
        string aiScript = ""
    ) : base
    (
        null,
        position.ToVector2(),
        model,
        name,
        team,
        0,
        ignoresCollision,
        isTargetable,
        false,
        null,
        stats,
        aiScript
    )
    {
        Direction = faceDirection;
        Camp = monsterCamp;
        Team = team;
        SpawnAnimation = spawnAnimation;
        FaceDirection(faceDirection);
        _originalPosition = position;

        if (aiScript == "")
        {
            aiScript = "Leashed.lua";
        }
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        this.Stats.Range.IncFlatBonusPerm(150.0f);
        NPC_LevelUpNotify(this);
        CreateNeutralNotify(this, Game.Time.GameTime, userId, team, doVision);
    }

    internal override void OnAdded()
    {
        base.OnAdded();
    }

    public override void Die(DeathData data)
    {
        base.Die(data);
        NeutralCampManager.KillMinion((data.Killer as ObjAIBase)!, this);
    }

    public override void TakeDamage(DamageData damageData, IEventSource sourceScript = null)
    {
        if (damageData.Attacker is AttackableUnit attacker)
        {
            AggroWholeCamp(attacker);
        }
        base.TakeDamage(damageData, sourceScript);
    }

    public void CallForHelp(AttackableUnit attacker)
    {
        AggroWholeCamp(attacker);
    }

    private void AggroWholeCamp(AttackableUnit attacker)
    {
        foreach (var campMinion in Camp.Minions)
        {
            if (campMinion is NeutralMinion nm && !nm.Stats.IsDead)
            {
                nm._lastKnownTargetPosition = attacker.Position3D;
                nm._isSearching = false;
                nm._searchTimer = 0f;
                nm._isReturning = false;
                nm.TargetUnit = attacker;
                nm.AIScript.OnOrder(OrderType.AttackTo, attacker, attacker.Position);
            }
        }
    }

    private void RetreatWholeCamp()
    {
        foreach (var campMinion in Camp.Minions)
        {
            if (campMinion is NeutralMinion nm && !nm.Stats.IsDead)
            {
                nm._isSearching = false;
                nm._searchTimer = 0f;
                nm._isReturning = true;
                nm.TargetUnit = null;
                nm._lastKnownTargetPosition = nm._originalPosition;
                nm.SetAIState(AIState.AI_RETREAT, true);
                nm.IssueOrDelayOrder(OrderType.MoveTo, null, nm._originalPosition.ToVector2(), fromAiScript: true);
            }
        }
    }

    public override bool IsValidTarget(AttackableUnit target)
    {
        return target switch
        {
            BaseTurret => false,
            Minion minion => minion.Owner != null && base.IsValidTarget(target),
            _ => base.IsValidTarget(target)
        };
    }

    internal override bool CanChangeWaypoints()
    {
        if (Stats.IsDead != null && AutoAttackSpell != null)
        {
            return !Stats.IsDead && MovementParameters == null
               && AutoAttackSpell.State is SpellState.READY or SpellState.COOLDOWN;
        }
        else
        {
            return false;
        }
    }

    internal void SetLevel(int level, bool setFullHealth = true)
    {
        MinionLevel = level;

        DynValue minionTableDyn = Game.ContentManager.DefaultNeutralMinionValues.Get(Model);

        if (minionTableDyn.Type == DataType.Table)
        {
            Table minionTable = minionTableDyn.Table;
            DynValue expGivenDyn = minionTable.Get("ExpGiven");

            if (expGivenDyn.Type == DataType.Number)
            {
                Stats.ExpGivenOnDeath.BaseValue = (float)expGivenDyn.Number;
            }
        }
    }

    internal override void Update()
    {
        base.Update();

        if (Stats.IsDead)
            return;

        // If any minion in the camp is aggroed, all should be aggroed
        bool campAggroed = Camp.Minions.Any(m => m is NeutralMinion nm && nm.TargetUnit != null && !nm.Stats.IsDead);

        if (TargetUnit != null)
        {
            bool targetVisible = VisionManager.UnitHasVisionOn(this, TargetUnit);
            float distanceFromCamp = Vector3.Distance(_originalPosition, TargetUnit.Position3D);

            if (!targetVisible)
            {
                if (!_isSearching)
                {
                    _isSearching = true;
                    _lastKnownTargetPosition = TargetUnit.Position3D;
                    _searchTimer = 0f;
                }
                else
                {
                    _searchTimer += Game.Time.ScaledDeltaTime / 1000f;
                    if (_searchTimer >= SEARCH_DURATION)
                    {
                        RetreatWholeCamp();
                    }
                    else
                    {
                        SearchForTarget();
                    }
                }
            }
            else
            {
                _lastKnownTargetPosition = TargetUnit.Position3D;
                _isSearching = false;
                _searchTimer = 0f;
                _isReturning = false;

                if (distanceFromCamp > LEASH_RADIUS)
                {
                    RetreatWholeCamp();
                }
            }
        }
        else if (_isSearching)
        {
            _searchTimer += Game.Time.ScaledDeltaTime / 1000f;
            if (_searchTimer >= SEARCH_DURATION)
            {
                RetreatWholeCamp();
            }
            else
            {
                SearchForTarget();
            }
        }
        else if (_isReturning)
        {
            // If reached camp, reset state and forget target
            if (Vector3.Distance(Position3D, _originalPosition) < 50f)
            {
                _isReturning = false;
                _isSearching = false;
                _searchTimer = 0f;
                TargetUnit = null;
                _lastKnownTargetPosition = _originalPosition;
            }
        }
    }

    private void SearchForTarget()
    {
        // Move towards the last known position of the target to search for it
        if (Vector3.Distance(Position3D, _lastKnownTargetPosition) > 50f)
        {
            IssueOrDelayOrder(OrderType.MoveTo, null, _lastKnownTargetPosition.ToVector2(), fromAiScript: true);
        }
        else
        {
            // Try to find nearby targets at last known position
            var nearbyTargets = Game.ObjectManager.GetUnitsInRange(_lastKnownTargetPosition.ToVector2(), SEARCH_RADIUS, true);
            foreach (var unit in nearbyTargets)
            {
                if (IsValidTarget(unit) && unit.Team != this.Team)
                {
                    AggroWholeCamp(unit);
                    return;
                }
            }
            // If no targets, wait at this position until search timer expires
        }
    }

    private void ReturnToCamp()
    {
        _isSearching = false;
        _searchTimer = 0f;
        _isReturning = true;
        TargetUnit = null;
        this.SetAIState(AIState.AI_RETREAT, true);
        IssueOrDelayOrder(OrderType.MoveTo, null, _originalPosition.ToVector2(), fromAiScript: true);
    }

    private AttackableUnit FindValidTargetNearPosition(Vector3 position, float radius)
    {
        var units = Game.ObjectManager.GetUnitsInRange(position.ToVector2(), radius, true);
        foreach (var unit in units)
        {
            if (IsValidTarget(unit) && unit.Team != this.Team)
                return unit;
        }
        return null;
    }

    public float GetDistanceToOriginalPosition()
    {
        return Vector3.Distance(Position3D, _originalPosition);
    }

    public Vector3 GetOriginalPosition()
    {
        return _originalPosition;
    }

    public void OnTargetEnterBush(AttackableUnit target)
    {
        if (target == TargetUnit)
        {
            _isSearching = true;
            _lastKnownTargetPosition = target.Position3D;
            _searchTimer = 0f;
        }
    }
}
