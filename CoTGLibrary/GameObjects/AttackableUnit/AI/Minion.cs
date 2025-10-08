using System;
using System.Collections.Generic;
using System.Numerics;
using CoTGEnumNetwork;
using CoTGEnumNetwork.Enums;
using CoTGLibrary.GameObjects;
using CoTG.CoTGServer.API;
using CoTG.CoTGServer.Content;
using CoTG.CoTGServer.Content.Navigation;
using CoTG.CoTGServer.GameObjects.StatsNS;
using CoTG.CoTGServer.Scripting.CSharp;
using static CoTG.CoTGServer.Game;

using FCS = CoTG.CoTGServer.Scripting.CSharp.Converted.Functions_CS;
using static PacketVersioning.PktVersioning;


namespace CoTG.CoTGServer.GameObjects.AttackableUnits.AI;

public class Minion : ObjAIBase
{
    /// <summary>
    /// Unit which spawned this minion.
    /// </summary>
    public ObjAIBase? Owner { get; }
    /// <summary>
    /// Whether or not this minion is considered a ward.
    /// </summary>
    public bool IsWard { get; protected set; }
    /// <summary>
    /// Whether or not this minion is a LaneMinion.
    /// </summary>
    public bool IsLaneMinion { get; protected set; }
    /// <summary>
    /// Only unit which is allowed to see this minion.
    /// </summary>
    public ObjAIBase? VisibilityOwner { get; }
    internal int DamageBonus { get; set; }
    internal int HealthBonus { get; set; }
    internal MinionFlags MinionFlags { get; set; }

    public int MinionLevel { get; protected set; }
    public MinionRoamState RoamState { get; private set; }
    public override bool HasSkins => Owner != null;

    BehaviourTree BehaviourTree = null;

    public bool UseBehaviourTree = false;

    //hack 

    public bool ispet { get; set; }





    private IEnumerable<NavigationGridCell> cells;
    private bool _wallMinion;
    public bool IsWallMinion
    {
        get => _wallMinion;
        set
        {
            if (value && !_wallMinion)
            {
                _wallMinion = value;
                cells = Game.Map.NavigationGrid.GetAllCellsInRange(Position, CollisionRadius);
            }
        }
    }




    public Minion(
        ObjAIBase? owner,
        Vector2 position,
        string model,
        string name,
        TeamId team = TeamId.TEAM_NEUTRAL,
        int skinId = 0,
        bool ignoreCollision = false,
        bool targetable = true,
        bool isWard = false,
        ObjAIBase? visibilityOwner = null,
        Stats? stats = null,
        string AIScript = "",
        int initialLevel = 1,
         int visionRadius = 1100,
        bool _useBT = false,
        bool _ispet = false
    ) : base(model, name, position, visionRadius, skinId, 0, team, stats, AIScript)
    {
        Owner = owner;

        if (owner != null)
        {
            GoldOwner = new GoldOwner(owner);
        }

        IsLaneMinion = false;
        IsWard = isWard;

        SetStatus(StatusFlags.Ghosted, ignoreCollision); //
        SetStatus(StatusFlags.Targetable, targetable);
        Stats.LevelUp(initialLevel);

        VisibilityOwner = visibilityOwner;
        MoveOrder = OrderType.Stop;

        Replication = new ReplicationMinion(this);
        UseBehaviourTree = _useBT;

        if (_useBT == true)
        {
            if (Game.Map.MapData.Id == 8 && (model.Contains("Blue_Minion") || model.Contains("Red_Minion") || model.Contains("BlueSuperminion") || model.Contains("RedSuperminion")))
            {
                hackloadspellminion();
                LoadBehaviourTreeMinionOdin();
            }
            else if (Game.Map.MapData.Id == 8 && this.capturepointid != -1)
            {
                hackloadspell();
                LoadBehaviourTreeTurretOdin();

                //hack or turret move 

                //Check
                if (CharData.IsTower)
                {
                    SetStatus(StatusFlags.CanMove, false);
                    Stats.MoveSpeed.FlatBonus = -Stats.MoveSpeed.Total;
                }
            }
        }
        // ispet = _ispet;
        //all this part is an hack for fix minion 
        if (Game.Map.MapData.Id == 8 && (model.Contains("BlueSuperminion") || model.Contains("RedSuperminion")))
        {

            this.Stats.Range.BaseValue = 350;
            this.Stats.AcquisitionRange.BaseValue = 350;
            this.Stats.PerceptionRange.BaseValue = 350;

        }
        if (Game.Map.MapData.Id == 8 && (model.Contains("Blue_Minion") || model.Contains("Red_Minion")))
        {

            this.Stats.Range.BaseValue = 650;
            this.Stats.AcquisitionRange.BaseValue = 650;
            this.Stats.PerceptionRange.BaseValue = 650;
        }
    }
    internal void hackloadspell()
    {

        //find another method 
        // this.SetSpell("OdinGuardianSpellAttackCast", 0, true);
        //  this.SetSpellToCast(this.Spells.Passive);
        //  this.Spells.cas
        // Particle p1 = new Particle("capture_point_gauge.troy", this, this.Position, this, "", this.Position, this, "", 25000, 1.0f, Vector3.Zero, "", false, FXFlags.BindDirection, this.Team);
    }

    internal void hackloadspellminion()
    {

        //find another method 
        this.SetSpell("OdinMinionSpellAttack", 0, true);

        //  this.Buffs.Add("OdinGolemBombBuff", 25000, 1, null, this, this);

        this.Stats.AttackDamage.BaseValue = 85.0f;

        //ExpirationTimer hack because minion can be stuck in dominion 
        this.Buffs.Add("ExpirationTimer2", 60.0f, 1, null, this, this);

    }


    internal void LoadBehaviourTreeMinionOdin()
    {
        BehaviourTree = Game.ScriptEngine.CreateObject<BehaviourTree>($"BehaviourTrees.Map{Game.Map.Id}.AI_manager", "MinionAIBTODIN") ?? new BehaviourTree(this);
        BehaviourTree.Owner = this;
    }

    internal void LoadBehaviourTreeTurretOdin()
    {

        BehaviourTree = Game.ScriptEngine.CreateObject<BehaviourTree>($"BehaviourTrees.Map{Game.Map.Id}.AI_manager", "TurretAIBTODIN") ?? new BehaviourTree(this);

        BehaviourTree.Owner = this;

        // Initialiser l'owner si c'est un TurretAIBTODIN
        if (BehaviourTree.GetType().Name == "TurretAIBTODIN")
        {
            // Use reflection to call InitializeOwner
            var method = BehaviourTree.GetType().GetMethod("InitializeOwner");
            method?.Invoke(BehaviourTree, [this]);
        }

        // this.Buffs.Add("OdinParticlePHBuff", 25000, 1, null, this, this);

        // this.LoadCharScript(GetSpell("CharScriptOdinNeutralGuardian"));
        // this.Spells[(short)SpellSlotType.PassiveSpellSlot].Activate();

    }

    private float lastExecutionTime = 0;
    private float interval = 1000;

    private float tickupdatemanager = 0.1f;
    private float _lasttickupdatemanager;

    private float tickupdatemovement = 0.5f;

    private float _lastAvoidanceUpdateTime;
    private const float AVOIDANCE_UPDATE_INTERVAL = 0.25f;
    internal override void Update()
    {

        base.Update();
        BehaviourTree?.Update();

        if ((Time.ScaledDeltaTime / 1000f) - _lastAvoidanceUpdateTime >= (tickupdatemovement + AVOIDANCE_UPDATE_INTERVAL))
        {
            if (Waypoints != null && Waypoints.Count > 1)
            {
                Vector2 targetPos = Waypoints[1];
                var nearbyEntities = FCS.GetUnitsInArea(this, this.Position3D, 500.0f, SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectAllSides);
                Vector2 avoidDir = CalcAvoidance(Position, targetPos, nearbyEntities);
                Vector2 newTargetPos = targetPos + (avoidDir * 50.0f);
                var newWaypoints = Game.Map.NavigationGrid.GetPath(Position, newTargetPos, this);

                SetWaypoints(newWaypoints);
            }
            _lastAvoidanceUpdateTime = Time.ScaledDeltaTime / 1000f;
        }

    }
    private Vector2 CalcAvoidance(Vector2 position, Vector2 targetPos, IEnumerable<AttackableUnit> nearbyEntities)
    {
        Vector2 avoidDir = Vector2.Zero;
        float avoidWeight = 1.0f;
        float avoidRadius = 50.0f; // entity avoidance radius

        foreach (AttackableUnit entity in nearbyEntities)
        {
            if (entity == this || (entity.Status & StatusFlags.Ghosted) != 0)
                continue;

            Vector2 entityPos = entity.Position;
            float entityRadius = entity.CollisionRadius;
            if (Vector2.Distance(position, entityPos) <= avoidRadius + entityRadius)
            {
                Vector2 dirToEntity = (entityPos - position).Normalized();
                Vector2 avoidDirEntity = new Vector2(-dirToEntity.Y, dirToEntity.X).Normalized() * avoidRadius;
                avoidDir += avoidDirEntity * avoidWeight;
            }
        }

        return avoidDir;
    }


    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        MinionSpawnedNotify(this, userId, team, doVision);
    }

    internal override void OnRemoved()
    {
        base.OnRemoved();
    }
    public override bool CanMove()
    {
        if (capturepointid != -1)
        {
            return false;
        }


        return !IsWallMinion && base.CanMove();
    }
    public void LevelUp(int ammount = 1, bool notify = true)
    {
        int toUpgrade = Math.Clamp(MinionLevel + ammount, 0, 18);
        Stats.LevelUp(ammount);
        MinionLevel = toUpgrade;
        if (notify)
        {
            NPC_LevelUpNotify(this);
            OnReplicationUnitNotify(this, partial: false);
        }
        ApiEventManager.OnLevelUp.Publish(this);
    }

    public void SetRoamState(MinionRoamState roamState)
    {
        this.RoamState = roamState;
    }

    internal override float GetAttackRatioWhenAttackingTurret()
    {
        return GlobalData.DamageRatios.UnitToBuilding;
    }
    internal override float GetAttackRatioWhenAttackingMinion()
    {
        return GlobalData.DamageRatios.UnitToUnit;
    }
    internal override float GetAttackRatioWhenAttackingChampion()
    {
        return GlobalData.DamageRatios.UnitToHero;
    }
    internal override float GetAttackRatioWhenAttackingBuilding()
    {
        return GlobalData.DamageRatios.UnitToBuilding;
    }
    internal override float GetAttackRatio(AttackableUnit attackingUnit)
    {
        return attackingUnit.GetAttackRatioWhenAttackingMinion();
    }
}