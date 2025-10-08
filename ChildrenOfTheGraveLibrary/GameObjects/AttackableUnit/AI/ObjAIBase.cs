using System;
using System.Activities.Presentation.View;
using System.Collections.Generic;
using System.Numerics;
using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.Content;
using ChildrenOfTheGraveLibrary.GameObjects.Stats;
using ChildrenOfTheGraveLibrary.Scripting.Lua.Scripts;
using ChildrenOfTheGraveLibrary.Utilities;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.API;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Inventory;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua;
using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua.Functions;
using log4net;
using static PacketVersioning.PktVersioning;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

/// <summary>
/// Base class for all moving, attackable, and attacking units.
/// ObjAIBases normally follow these guidelines of functionality: Self movement, Inventory, Targeting, Attacking, and Spells.
/// </summary>
public class ObjAIBase : AttackableUnit
{
    // Crucial Vars
    private float _autoAttackCurrentCooldown;
    private bool _skipNextAutoAttack;
    private Spell _castingSpell;
    private Spell _lastAutoAttack;
    private Random _random = new();
    protected AIState _aiState = AIState.AI_IDLE;
    protected bool _aiPaused;
    protected Pet _lastPetSpawned;
    private static ILog _logger = LoggerProvider.GetLogger();

    /// <summary>
    /// Variable storing all the data related to this AI's current auto attack. *NOTE*: Will be deprecated as the spells system gets finished.
    /// </summary>
    public Spell AutoAttackSpell { get; protected set; }
    /// <summary>
    /// Spell this AI is currently channeling.
    /// </summary>
    public Spell ChannelSpell { get; protected set; }
    /// <summary>
    /// The ID of the skin this unit should use for its model.
    /// </summary>
    public int SkinID { get; set; }
    public bool HasAutoAttacked { get; set; }
    /// <summary>
    /// If the unit can auto attack, controlled by the AI script
    /// </summary>
    public bool AutoAttackOn { get; set; } = true;
    /// <summary>
    /// Whether or not this AI has made their first auto attack against their current target. Refreshes after untargeting or targeting another unit.
    /// </summary>
    public bool HasMadeInitialAttack { get; set; }
    /// <summary>
    /// Variable housing all variables and functions related to this AI's Inventory, ex: Items.
    /// </summary>
    /// TODO: Verify if we want to move this to AttackableUnit since items are related to stats.
    public ItemInventory ItemInventory { get; protected set; }
    /// <summary>
    /// Whether or not this AI is currently auto attacking.
    /// </summary>
    public bool IsAttacking { get; private set; }
    /// <summary>
    /// Spell this unit will cast when in range of its target.
    /// Overrides auto attack spell casting.
    /// </summary>
    public Spell? SpellToCast { get; protected set; }
    /// <summary>
    /// Arguments for the queued spell cast
    /// </summary>
    public Spell.CastArguments? SpellToCastArguments { get; protected set; }
    /// <summary>
    /// Whether or not this AI's auto attacks apply damage to their target immediately after their cast time ends.
    /// </summary>
    public bool IsMelee { get; set; }
    /// <summary>
    /// Whether or not this AI's next auto attacks will be critical.
    /// </summary>
    public bool IsNextAutoCrit { get; set; }
    /// <summary>
    /// Whether or not this AI's can have different skins (Champions, Pets and Champions's Minions).
    /// </summary>
    public virtual bool HasSkins => false;

    /// <summary>
    /// Current order this AI is performing.
    /// </summary>
    /// TODO: Rework AI so this enum can be finished.
    public OrderType MoveOrder { get; set; }

    /// <summary>
    /// The last issued move order from the AI (either from scripts or client), it can be different than the real MoveOrder, used purely for scripts
    /// </summary>
    public OrderType LastIssueMoveOrder { get; protected set; }

    /// <summary>
    /// If this unit has the setting (auto-attack) enabled
    /// </summary>
    public bool AutoAttackAutoAcquireTarget { get; set; }

    internal List<AssistMarker> AlliedAssistMarkers = new();
    internal List<AssistMarker> EnemyAssistMarkers = new();

    private bool _hasDelayedCastOrder = false;
    private bool _hasDelayedMovementOrder = false;

    private (OrderType OrderType, AttackableUnit TargetUnit, Vector2 TargetPosition, List<Vector2> Waypoints) _delayedMovementOrder;
    private (OrderType OrderType, AttackableUnit TargetUnit, Vector2 TargetPosition, byte SpellSlot) _delayedCastOrder;

    public EvolutionState EvolutionState { get; set; }
    public SpellInventory Spells { get; }

    private record CharacterData(int id, string skin);
    //TODO: _lastSeenCharData and Sync
    private List<CharacterData> _characterData = new();

    private bool _previousCanAttack = true;

    internal Dictionary<AttackableUnit, Wrapper<HitResult>> TargetHitResults = new();

    /// <summary>
    /// Unit this AI will auto attack or use a spell on when in range.
    /// </summary>
    public AttackableUnit? TargetUnit { get; set; }

    public Vector3? SpellPosition { get; set; }

    public Vector2? LastIssueOrderPosition { get; private set; }
    public ICharScript CharScript => ScriptInternal;
    internal ICharScriptInternal ScriptInternal { get; private set; }
    public bool IsBot { get; set; }
    public string AIScriptName { get; }
    public IAIScriptInternal AIScript { get; protected set; }

    public int CountChangeModel { get; set; }
    //hack
    public string oldmodelname { get; set; }

    public float lasttimegetupdated { get; set; } = 0;

    // PRD (Pseudo-Random Distribution) for critical hits
    // This system replaces traditional flat crit percentage with a more fair distribution
    private int _prdCritCounter = 0;

    /// <summary>
    /// Complete PRD lookup table for 1-100% crit chance.
    /// 
    /// WHY PRD IS FAIRER THAN FLAT CRIT PERCENTAGE:
    /// 
    /// === PROBLEM WITH FLAT CRIT % ===
    /// Traditional RNG uses fixed probability (e.g., 25% every attack):
    /// - Can have 20+ attacks without crit (extremely frustrating)
    /// - Can have 4-5 crits in a row (randomly wins lanes)
    /// - Pure variance can determine fight outcomes regardless of skill
    /// - Creates inconsistent power spikes that feel unfair
    /// 
    /// === PRD SOLUTION: BAD LUCK PROTECTION ===
    /// PRD starts with LOW probability but escalates with each miss:
    /// - Attack 1: ~8.47% (for 25% base crit)
    /// - Attack 2: ~16.95% (if first missed)
    /// - Attack 3: ~25.42% (if both missed)
    /// - Attack 12: 100% GUARANTEED (maximum 11 misses possible)
    /// 
    /// RESULT: Eliminates frustrating dry spells while maintaining 25% average
    /// 
    /// === PRD SOLUTION: GOOD LUCK DAMPENING ===
    /// Each crit resets counter to 0, making consecutive crits much harder:
    /// - 2 crits in a row: 8.7x LESS likely than flat RNG
    /// - 3 crits in a row: 25.7x LESS likely than flat RNG
    /// - 4 crits in a row: 75x LESS likely than flat RNG
    /// 
    /// RESULT: Prevents random power spikes that unfairly swing lane outcomes
    /// 
    /// === GAMEPLAY BENEFITS ===
    /// 1. CONSISTENT EXPERIENCE: Players can predict crit frequency more reliably
    /// 2. SKILL-BASED COMBAT: Reduces RNG impact on critical fights
    /// 3. BALANCED LANES: Prevents random crit streaks from deciding laning phase
    /// 4. FAIR POWER SPIKES: Maintains intended damage output without variance extremes
    /// 5. PSYCHOLOGICAL SATISFACTION: Reduces frustration from unlucky streaks
    /// 
    /// This is why Riot Games implemented PRD - it preserves statistical balance
    /// while creating a more fair and predictable player experience.
    /// </summary>
    private static readonly float[] PRD_Q_TABLE = {
        0.000156f, 0.00062f, 0.001386f, 0.002449f, 0.003802f, 0.00544f, 0.007359f, 0.009552f, 0.012016f, 0.014746f,
        0.017736f, 0.020983f, 0.024482f, 0.02823f, 0.032221f, 0.036452f, 0.04092f, 0.04562f, 0.050549f, 0.055704f,
        0.061081f, 0.066676f, 0.072488f, 0.078511f, 0.084744f, 0.091183f, 0.097826f, 0.10467f, 0.111712f, 0.118949f,
        0.126379f, 0.134001f, 0.141805f, 0.14981f, 0.157983f, 0.166329f, 0.174909f, 0.183625f, 0.192486f, 0.201547f,
        0.21092f, 0.220365f, 0.229899f, 0.23954f, 0.249307f, 0.259872f, 0.270453f, 0.281008f, 0.291552f, 0.302103f,
        0.312677f, 0.323291f, 0.33412f, 0.34737f, 0.360398f, 0.373217f, 0.38584f, 0.398278f, 0.410545f, 0.42265f,
        0.434604f, 0.446419f, 0.458104f, 0.46967f, 0.481125f, 0.492481f, 0.507463f, 0.529412f, 0.550725f, 0.571429f,
        0.591549f, 0.611111f, 0.630137f, 0.648649f, 0.666667f, 0.684211f, 0.701299f, 0.717949f, 0.734177f, 0.75f,
        0.765432f, 0.780488f, 0.795181f, 0.809524f, 0.823529f, 0.837209f, 0.850575f, 0.863636f, 0.876404f, 0.888889f,
        0.901099f, 0.913043f, 0.924731f, 0.93617f, 0.947368f, 0.958333f, 0.969072f, 0.979592f, 0.989899f, 1.0f
    };

    /// <summary>
    /// Uses Pseudo-Random Distribution to determine critical hit using the exact lookup table.
    /// 
    /// HOW IT WORKS:
    /// 1. Get Q constant from lookup table based on crit percentage
    /// 2. Calculate current chance = Q × (failure_count + 1)
    /// 3. Roll against this escalating probability
    /// 4. Reset counter on crit, increment on miss
    /// 
    /// FAIRNESS EXAMPLES (25% crit chance):
    /// 
    /// BAD LUCK PROTECTION:
    /// - Pure RNG: Could miss 30+ times in a row (0.75^30 = 0.0178% chance)
    /// - PRD: Maximum 11 misses, then guaranteed crit (eliminates extreme frustration)
    /// 
    /// GOOD LUCK DAMPENING:
    /// - Pure RNG: 3 crits in a row = 1.56% chance (1 in 64)
    /// - PRD: 3 crits in a row = 0.061% chance (1 in 1,645)
    /// - Reduces unfair lane-winning RNG by 25x
    /// 
    /// This creates consistent, skill-based gameplay where RNG enhances but doesn't dominate fights.
    /// </summary>
    public bool RollForCritPRD(float critChance)
    {
        if (critChance <= 0) return false;
        if (critChance >= 1.0f) return true;

        // Get PRD constant from lookup table (pre-calculated for optimal performance)
        int pctChance = Math.Clamp((int)(critChance * 100), 1, 100);
        float prdConstant = PRD_Q_TABLE[pctChance - 1];

        // Calculate escalating probability: starts low, increases with each failure
        // This is the core of PRD - probability grows linearly with miss count
        float currentChance = prdConstant * (_prdCritCounter + 1);
        bool isCrit = _random.NextSingle() < currentChance;

        if (isCrit)
        {
            _prdCritCounter = 0; // Reset on crit - prevents consecutive lucky streaks
        }
        else
        {
            _prdCritCounter++; // Increment failure count - builds toward guaranteed crit
        }

        return isCrit;
    }

    public ObjAIBase(
            string model,
            string name = "",
            Vector2 position = new(),
            int visionRadius = 0,
            int skinId = 0,
            uint netId = 0,
            TeamId team = TeamId.TEAM_NEUTRAL,
            Stats? stats = null,
            string aiScript = ""
        ) : base(name, model, 0, position, visionRadius, netId, team, stats)
    {
        SkinID = skinId;
        ItemInventory = new ItemInventory(this);
        CountChangeModel = 0;
        // TODO: Centralize this instead of letting it lay in the initialization.
        CollisionRadius = CharData.GameplayCollisionRadius;

        if (CharData.PathfindingCollisionRadius > 0)
        {
            PathfindingRadius = CharData.PathfindingCollisionRadius;
        }
        else
        {
            PathfindingRadius = 40;
        }

        // TODO: Centralize this instead of letting it lay in the initialization.
        if (visionRadius > 0)
        {
            Stats.PerceptionRange.BaseValue = visionRadius;
        }
        else if (CharData.PerceptionBubbleRadius > 0)
        {
            Stats.PerceptionRange.BaseValue = CharData.PerceptionBubbleRadius;
        }
        else
        {
            Stats.PerceptionRange.BaseValue = 1100;
        }

        Stats.CurrentMana = Stats.ManaPoints.Total;
        Stats.CurrentHealth = Stats.HealthPoints.Total;

        EvolutionState = new EvolutionState();

        Spells = new SpellInventory(this);
        SpellToCast = null;

        if (!string.IsNullOrEmpty(model))
        {
            IsMelee = CharData.IsMelee;

            // SpellSlots
            // 0 - 3
            for (short i = 0; i < CharData.SpellNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(CharData.SpellNames[i]))
                {

                    Spells[i] = new Spell(this, CharData.SpellNames[i], (byte)i);
                }
            }


            // InventorySlots
            // 4 - 9 
            for (byte i = (int)SpellSlotType.InventorySlots; i < (int)SpellSlotType.BluePillSlot; i++)
            {
                Spells[i] = new Spell(this, "BaseSpell", i);
            }

            // bluepill
            // 10 - 11
            //in packet bluepill is on slot 10 , but no way to get it work omfg 
            // Spells[(int)SpellSlotType.BluePillSlot] = new Spell(this, "Recall", (int)SpellSlotType.BluePillSlot);
            Spells[(int)SpellSlotType.BluePillSlot] = new Spell(this, "BaseSpell", (int)SpellSlotType.BluePillSlot);
            Spells[(int)SpellSlotType.TempItemSlot] = new Spell(this, "BaseSpell", (int)SpellSlotType.TempItemSlot);




            // Summoner Spells //pas d'incidence de l'emplacement a premiere vu 
            // 4 - 5
            Spells[(int)SpellSlotType.SummonerSpellSlots] = new Spell(this, "BaseSpell", (int)SpellSlotType.SummonerSpellSlots);
            Spells[(int)SpellSlotType.SummonerSpellSlots + 1] = new Spell(this, "BaseSpell", (int)SpellSlotType.SummonerSpellSlots + 1);



            // Fix126
            //// RuneSlots
            //// 15 - 44
            //for (short i = (int)SpellSlotType.RuneSlots; i < (int)SpellSlotType.ExtraSlots; i++)
            //{
            //    Spells[(byte)i] = new Spell(this, "BaseSpell", (byte)i);
            //}

            // ExtraSpells
            // 45 - 60
            for (short i = 0; i < CharData.ExtraSpells.Length; i++)
            {
                var extraSpellName = "BaseSpell";
                if (!string.IsNullOrEmpty(CharData.ExtraSpells[i]))
                {
                    extraSpellName = CharData.ExtraSpells[i];
                }

                var slot = i + (int)SpellSlotType.ExtraSlots;
                Spells[(byte)slot] = new Spell(this, extraSpellName, (byte)slot);
                Spells[(byte)slot].LevelUp();
            }

            // Special Spell Slots
            // 61 - 62
            Spells[(int)SpellSlotType.RespawnSpellSlot] = new Spell(this, "BaseSpell", (int)SpellSlotType.RespawnSpellSlot);
            Spells[(int)SpellSlotType.UseSpellSlot] = new Spell(this, "BaseSpell", (int)SpellSlotType.UseSpellSlot);

            // Fix126
            // Passive
            // 63
            // Spells[(int)SpellSlotType.PassiveSpellSlot] = new Spell(this, CharData.PassiveData.PassiveLuaName, (int)SpellSlotType.PassiveSpellSlot);

            // BasicAttackNormalSlots & BasicAttackCriticalSlots
            // 64 - 72 & 73 - 81
            for (short i = 0; i < CharData.BasicAttacks.Length; i++)
            {
                var aaName = CharData.BasicAttacks[i].Name;
                if (model.Contains("OdinNeutralGuardian"))
                {
                    aaName = "OdinGuardianSpellAttackCast";
                    //  DebugSayCrash(this, "");
                }



                //if (!string.IsNullOrEmpty(aaName))
                // If you ask the client to launch an attack for which it does not have an ini,
                // then even if the attack is in the champion ini, the client will pause the animation.
                if (ContentManager.GetSpellData(aaName) != null)
                {
                    // Fix126
                    int slot = i + (int)SpellSlotType.BasicAttackSlots;
                    Spells[(byte)slot] = new Spell(this, aaName, (byte)slot);
                }
            }

            AutoAttackSpell = GetNewAutoAttack();
        }
        else
        {
            IsMelee = true;
        }

        AIScriptName = aiScript;

        LoadCharScript(Spells.Passive);
        LoadAIScript();

        SetAIState(AIState.AI_IDLE);

        // Add listeners for undo prevention
        ApiEventManager.OnDealDamage.AddListener(this, this,
            _ => ItemInventory.ClearUndoHistory()
        );


    }




    internal override void OnAdded()
    {
        base.OnAdded();

        if (!(Game.StateHandler.State == GameState.GAMELOOP))
        {

            Game.pendingActions.Add(ActivateAIScript);
            Game.pendingActions.Add(ActivateScriptInternal);

            return;
        }

        try
        {
            AIScript.Activate();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }

        try
        {
            ScriptInternal.OnActivate();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }
    }

    private void ActivateAIScript()
    {
        try
        {
            AIScript.Activate();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }
    }

    private void ActivateScriptInternal()
    {
        try
        {
            ScriptInternal.OnActivate();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }
    }

    /// <summary>
    /// Loads the Passive Script
    /// </summary>
    internal void LoadCharScript(Spell spell)
    {
        bool isReload = CharScript != null;
        if (isReload)
        {
            try
            {
                ScriptInternal.OnDeactivate();
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
            ApiEventManager.RemoveAllListenersForOwner(CharScript);
        }

        var scriptNamespace = "CharScripts";
        var scriptName = $"CharScript{Model}";
        var script = Game.ScriptEngine.CreateObject<ICharScriptInternal>(scriptNamespace, scriptName, Game.Config.SupressScriptNotFound);
        if (script == null)
        {
            if (LuaScriptEngine.HasBBScript(scriptName))
            {
                script = new BBCharScript
                (
                    new BBScriptCtrReqArgs
                    (
                        scriptName,
                        this,
                        (this as Minion)?.Owner as Champion
                    )
                );
            }
            else
            {
                script = new CharScriptEmpty();
            }
        }
        ScriptInternal = script;
        ScriptInternal.Init(this, spell);

        if (isReload)
        {
            try
            {
                ScriptInternal.OnActivate();
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
        }
    }

    /// <summary>
    /// Loads the AI Script
    /// </summary>
    internal void LoadAIScript()
    {
        var scriptName = AIScriptName;

        if (this is Champion)
        {
            scriptName = "Hero.lua";
        }

        AIScript = Game.ScriptEngine.CreateObject<IAIScriptInternal>("AIScripts", scriptName.Replace(".lua", "AI"), Game.Config.SupressScriptNotFound);

        if (AIScript == null)
        {
            if (LuaScriptEngine.HasLuaScript(scriptName))
            {
                AIScript = new LuaAIScript(scriptName);
            }
            else
            {
                AIScript = new EmptyAIScript();
            }
        }

        AIScript.Init(this);
    }

    /// <summary>
    /// Reload the spells scripts. Debug only
    /// </summary>
    internal void ReloadSpellsScripts()
    {
        foreach (var spell in Spells)
        {
            spell?.LoadScript();
        }
    }
    public int _currentWaypointIndex = 0;
    public Vector2 Closestwaypointofthelist()
    {

        bool WaypointReached()
        {
            Vector2 currentWaypoint = this.lanetofollow[_currentWaypointIndex];

            //HACK: I don't have a better way of setting waypoint as completed at the moment (ACC)
            //This is just a circle radius and when minion enters it, it is marked as completed
            //Value 600 works for summoner's rift good, not so for Twisted Treeline (reworked)
            //this hack is an real problem for dominion , i test 200 
            float valuetest = 600.0f;
            switch (Game.Map.Id)
            {
                case 8:
                    valuetest = 50.0f;
                    break;
                case 4:
                    valuetest = 400.0f;
                    break;
                default:
                    valuetest = 600.0f;
                    break;
            }

            if (Vector2.Distance(this.Position, currentWaypoint) < valuetest)

            {
                return true;
            }

            return false;
        }

        if (WaypointReached())
        {
            _currentWaypointIndex++;
        }

        return this.lanetofollow[_currentWaypointIndex];
    }

    public Vector2 Closestwaypointofthelist2()
    {
        // Trouver le waypoint le plus proche de la position actuelle
        Vector2 currentPosition = this.Position;
        int closestIndex = 0;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < lanetofollow.Count; i++)
        {
            float distance = Vector2.Distance(currentPosition, lanetofollow[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        _currentWaypointIndex = closestIndex;
        return this.lanetofollow[_currentWaypointIndex + 1];
    }

    public override bool CanMove()
    {
        return CanChangeWaypoints()
               && (Status & StatusFlags.CanMove) != 0 && (Status & StatusFlags.CanMoveEver) != 0
               && !(
                   (Status & StatusFlags.Netted) != 0
                   || (Status & StatusFlags.Rooted) != 0
                   || (Status & StatusFlags.Sleep) != 0
                   || (Status & StatusFlags.Stunned) != 0
                   || (Status & StatusFlags.Suppressed) != 0
               );
    }

    internal override bool CanChangeWaypoints()
    {
        return !Stats.IsDead && MovementParameters == null
                             && (_castingSpell == null || !_castingSpell.SpellData.CantCancelWhileWindingUp)
                             && (ChannelSpell == null || !ChannelSpell.SpellData.CantCancelWhileChanneling || ChannelSpell.SpellData.CanMoveWhileChanneling);
    }

    /// <summary>
    /// Whether or not this AI is able to auto attack.
    /// </summary>
    /// <returns></returns>
    internal bool CanAttack()
    {
        // TODO: Verify if all cases are accounted for.
        return (Status & StatusFlags.CanAttack) != 0
            && (Status & StatusFlags.Charmed) == 0
            && (Status & StatusFlags.Disarmed) == 0
            && (Status & StatusFlags.Feared) == 0
            && (Status & StatusFlags.Pacified) == 0
            && (Status & StatusFlags.Sleep) == 0
            && (Status & StatusFlags.Stunned) == 0
            && (Status & StatusFlags.Suppressed) == 0
            && (_castingSpell == null || !_castingSpell.SpellData.CantCancelWhileWindingUp)
            && (ChannelSpell == null || !ChannelSpell.SpellData.CantCancelWhileChanneling);
    }
    /// <summary>
    /// Whether or not this AI is able to cast spells. Stats
    /// </summary>
    /// <returns></returns>
    internal bool CanCast()
    {
        return (Status & StatusFlags.CanCast) != 0
               && (_castingSpell == null || !_castingSpell.SpellData.CantCancelWhileWindingUp)
               && (ChannelSpell == null || !ChannelSpell.SpellData.CantCancelWhileChanneling);
    }

    public static bool CanLevelUpSpell(ObjAIBase obj, int slot)
    {
        int level;
        switch (obj)
        {
            case Champion c:
                level = c.Experience.Level;
                break;
            case Minion m:
                level = m.MinionLevel;
                break;
            default:
                return false;
        }
        ;
        return obj.CharData.SpellsUpLevels[slot][obj.Spells[slot].Level] <= level;
    }


    internal override bool Move(float diff)
    {
        // If we have waypoints, but our move order is one of these, we shouldn't move.
        if (MoveOrder == OrderType.CastSpell
            || MoveOrder == OrderType.OrderNone
            || MoveOrder == OrderType.Stop
            || MoveOrder == OrderType.Taunt)
        {
            return false;
        }

        return base.Move(diff);
    }

    /// <summary>
    /// Cancels any auto attacks this AI is performing and resets the time between the next auto attack if specified.
    /// </summary>
    /// <param name="reset">Whether or not to reset the delay between the next auto attack.</param>
    /// <param name="fullCancel">Remove the attacking state too</param>
    public void CancelAutoAttack(bool reset, bool fullCancel = false)
    {
        if (AutoAttackSpell != null)
        {
            AutoAttackSpell.SetSpellState(SpellState.READY);
            if (reset)
            {
                _autoAttackCurrentCooldown = 0;
                AutoAttackSpell.ResetSpellCast();
            }
            if (fullCancel)
            {
                IsAttacking = false;
                HasMadeInitialAttack = false;
            }
            InstantStopAttackNotifier(this, false);
        }
    }

    /// <summary>
    /// Function which refreshes this AI's waypoints if they have a target.
    /// </summary>
    protected virtual void RefreshWaypoints(float idealRange)
    {
        if (MovementParameters != null)
        {
            return;
        }

        if (TargetUnit != null && _castingSpell == null && ChannelSpell == null && MoveOrder != OrderType.AttackTo)
        {
            UpdateMoveOrder(OrderType.AttackTo, true);
        }

        if (SpellToCast != null)
        {
            //TODO: .99 Multiplication is a BandAid fix for cases where, due to float imprecision wizardry, idealRange > SpellToCast.CastRange
            //Further Research required
            idealRange = SpellToCast.CastRange;

        }

        Vector2 targetPos = Vector2.Zero;

        if (MoveOrder == OrderType.AttackTo
            && TargetUnit != null
            && !TargetUnit.Stats.IsDead)
        {
            targetPos = TargetUnit.Position;
        }

        if (MoveOrder == OrderType.AttackMove
            || MoveOrder == OrderType.AttackTerrainOnce
            || (MoveOrder == OrderType.AttackTerrainSustained
            && !IsPathEnded()))
        {
            targetPos = Waypoints.Count > 0 ? Waypoints[Waypoints.Count - 1] : Vector2.Zero;

            if (targetPos == Vector2.Zero)
            {
                return;
            }
        }
        // If the target is already in range, stay where we are.
        if (MoveOrder == OrderType.AttackMove
            && targetPos != Vector2.Zero
            && MovementParameters == null
            && Vector2.DistanceSquared(Position, targetPos) <= idealRange * idealRange
            && _autoAttackCurrentCooldown <= 0)
        {
            UpdateMoveOrder(OrderType.Stop, true);
        }
        // No TargetUnit
        else if (targetPos == Vector2.Zero)
        {
            return;
        }

        if (MoveOrder == OrderType.AttackTo && targetPos != Vector2.Zero)
        {
            var dist = Vector2.DistanceSquared(Position, targetPos);
            if (dist <= idealRange * idealRange)
            {
                //TODO: In the current implementation, OrderType is the state of the unit...
                //TODO: ...and I'm not sure it's right to change it if the attack continues.
                UpdateMoveOrder(OrderType.Hold, true);
            }
            else
            {
                if (!Game.Map.PathingHandler.IsWalkable(targetPos, PathfindingRadius))
                {
                    targetPos = Game.Map.NavigationGrid.GetClosestTerrainExit(targetPos, PathfindingRadius);
                }
                var newWaypoints = Game.Map.PathingHandler.GetPath(Position, targetPos, this);
                if (newWaypoints != null)//&& newWaypoints.Count > 1)
                {
                    SetWaypoints(newWaypoints);
                }
                if (TargetUnit != null)
                {
                    InstantStopAttackNotifier(this, false);
                }

            }
        }
    }

    /// <summary>
    /// Gets a random auto attack spell from the list of auto attacks available for this AI.
    /// Will only select crit auto attacks if the next auto attack is going to be a crit, otherwise normal auto attacks will be selected.
    /// </summary>
    /// <returns>Random auto attack spell.</returns>
    private Spell GetNewAutoAttack()
    {
        if (IsNextAutoCrit)
        {
            return Spells[(int)BasicAttackTypes.SPELLS_BASICATTACK_CRITICAL_SLOT];
        }

        float chancePointer = 0;
        float totalChance = 0;
        float randomNum = _random.NextSingle(); //0.7

        Stack<KeyValuePair<BasicAttackInfo, int>> attacks = new(CharData.BasicAttacks.Length - 1);

        for (int i = (int)BasicAttackTypes.SPELLS_BASICATTACKSLOT1; i <= (int)BasicAttackTypes.SPELLS_BASICATTACKSLOT4; i++)
        {
            BasicAttackInfo inf = CharData.BasicAttacks[i - 0x40];
            if (Spells[i] == _lastAutoAttack || inf.Probability <= 0)
            {
                continue;
            }
            totalChance += inf.Probability;
            attacks.Push(new KeyValuePair<BasicAttackInfo, int>(inf, i));
        }
        float mult = 1 / totalChance;
        while (attacks.TryPop(out KeyValuePair<BasicAttackInfo, int> inf))
        {
            if (randomNum >= chancePointer && randomNum <= chancePointer + inf.Key.Probability * mult)
            {
                return Spells[inf.Value];
            }
        }
        return Spells[(int)BasicAttackTypes.SPELLS_BASICATTACKSLOT1];
    }

    /// <summary>
    /// Search a Spell with specified name.
    /// </summary>
    /// <param name="name">Spell Name to search.</param>
    /// <returns>First Spell found, or null.</returns>
    public Spell? GetSpell(string name)
    {
        foreach (var spell in Spells)
        {
            if (spell?.SpellName == name)
            {
                return spell;
            }
        }
        return null;
    }

    internal virtual bool LevelUpSpell(byte slot, bool spendtrainingpoint = true)
    {
        Spell? s = Spells[slot];
        if (s is null || !CanLevelUpSpell(this, slot))
        {
            return false;
        }
        s.LevelUp();
        ApiEventManager.OnLevelUpSpell.Publish(s);
        ApiEventManager.OnUnitLevelUpSpell.Publish(this, s);
        return true;
    }

    public void EvolveSpell(byte slot, string toCast = null)
    {
        uint evolutionPoints = EvolutionState.EvolvePoints;
        if (evolutionPoints != 0)
        {
            EvolutionState.EvolveFlags = EvolutionState.EvolveFlags | (uint)(1 << slot);
            EvolutionState.DecrementEvolvePoints(1);
            if (toCast != null)
            {
                Spell c = GetSpell(toCast);
                c.TryCast(null, Position3D, Position3D);
            }
        }
    }

    /// <summary>
    /// Sets this AI's current auto attack to their base auto attack.
    /// </summary>
    public void ResetAutoAttackSpell(bool isReset = false)
    {
        if (isReset)
        {
            CancelAutoAttack(true); //TODO: Check
        }
        AutoAttackSpell.IsAutoAttackOverride = false;
        AutoAttackSpell = GetNewAutoAttack();
    }

    /// <summary>
    /// Sets this unit's auto attack spell that they will use when in range of their target (unless they are going to cast a spell first).
    /// </summary>
    /// <param name="name">Internal name of the spell to set.</param>
    /// <param name="isReset">Whether or not setting this spell causes auto attacks to be reset (cooldown).</param>
    public void SetAutoAttackSpell(string name, bool isReset)
    {
        int slot = -1;
        for (int i = 0; i < Spells.Count; i++)
        {
            if (Spells[i]?.Name == name)
            {
                slot = i;
                break;
            }
        }
        if (slot == -1)
        {
            throw new ArgumentException($"Error: Spell '{name}' not found");
            //slot = (int)SpellSlotType.TempItemSlot;
            //SetSpell(name, slot, true);
        }
        SetAutoAttackSpell(slot, isReset);
    }

    public void SetAutoAttackSpell(int slot, bool isReset, int level = 1)
    {
        AutoAttackSpell.IsAutoAttackOverride = false;
        AutoAttackSpell = Spells[slot];
        AutoAttackSpell.IsAutoAttackOverride = true;
        AutoAttackSpell.SetLevel(level);
        if (isReset)
        {
            CancelAutoAttack(true); //TODO: Check
        }
    }

    /// <summary>
    /// Forces this AI to skip its next auto attack. Usually used when spells intend to override the next auto attack with another spell.
    /// </summary>
    public void SkipNextAutoAttack()
    {
        //TODO: This is sent in packets after skipping next auto-attack, verify if it's really needed.
        InstantStopAttackNotifier(this, false);
        _skipNextAutoAttack = true;
    }

    /// <summary>
    /// Sets the spell for the given slot to a new spell of the given name.
    /// </summary>
    /// <param name="name">Internal name of the spell to set.</param>
    /// <param name="slot">Slot of the spell to replace.</param>
    /// <param name="enabled">Whether or not the new spell should be enabled.</param>
    /// <param name="networkOld">Whether or not to notify clients of this change using an older packet method.</param>
    /// <returns>Newly created spell set.</returns>
    public Spell SetSpell(string name, int slot, bool enabled, bool networkOld = false, bool isitemsell = false, bool force = false, bool isitemuse = false)
    {
        if (Spells[slot] != null && Spells[slot].IsAutoAttack)
        {
            return null;
        }

        if (Spells[slot] == null || name != Spells[slot].SpellName)
        {

            Spell toReturn = GetSpell(name) ?? new Spell(this, name, slot);

            if (Spells[slot] != null)
            {
                Spells[slot].Deactivate();
                toReturn.SetLevel(Spells[slot].Level);
            }

            Spells[slot] = toReturn;
            Spells[slot].Sealed = !enabled;



        }

        if (this is Champion champion)
        {
            if (!isitemsell)
            {
                int userId = Game.PlayerManager.GetClientInfoByChampion(champion).ClientId;
                if (!isitemuse)
                {
                    ChangeSlotSpellDataNotifier(userId, champion, slot, ChangeSlotSpellDataType.SpellName, slot is 4 or 5, newName: name);
                }


                if (champion.Model.Contains("Leblanc") || champion.Model.Contains("Xerath"))
                {

                    Spell toReturn = GetSpell(name) ?? new Spell(this, name, slot);


                    ChangeSlotSpellDataNotifier(userId, champion, slot, ChangeSlotSpellDataType.TargetingType, slot is 4 or 5, toReturn.TargetingType, name, toReturn.CastRange, toReturn.CastRange, toReturn.CastRange, toReturn.IconIndex);
                    ChangeSlotSpellDataNotifier(userId, champion, slot, ChangeSlotSpellDataType.OffsetTarget, slot is 4 or 5, toReturn.TargetingType, name, toReturn.CastRange, toReturn.CastRange, toReturn.CastRange, toReturn.IconIndex);
                    ChangeSlotSpellDataNotifier(userId, champion, slot, ChangeSlotSpellDataType.IconIndex, slot is 4 or 5, toReturn.TargetingType, name, toReturn.CastRange, toReturn.CastRange, toReturn.CastRange, toReturn.IconIndex);

                }
                if (networkOld)
                {
                    SetSpellDataNotifier(userId, NetId, name, slot);
                }

                SetSpellLevelNotifier(userId, champion.NetId, slot, Spells[slot].Level);
            }

        }

        return Spells[slot];
    }

    /// <summary>
    /// Sets the spell that this unit will cast when it gets in range of the spell's target.
    /// Overrides auto attack spell casting.
    /// </summary>
    /// <param name="spell">Spell that will be cast.</param>
    /// <param name="args">Spell to cast arguments</param>
    internal void SetSpellToCast(Spell? spell, Spell.CastArguments? args = null)
    {
        SpellToCast = spell;
        SpellToCastArguments = args;
        if (spell == null)
        {
            return;
        }
        if (args != null)
        {
            if (args.Pos.HasValue && args.Pos != Vector3.Zero)
            {
                //TODO: .99 Multiplication is a BandAid fix for cases where, due to float imprecision wizardry, idealRange > SpellToCast.CastRange
                //Further Research required
                var closestCircleEdgePoint = Extensions.GetClosestCircleEdgePoint(Position, args.Pos.Value.ToVector2(), spell.CastRange * 0.99f);
                var exit = Game.Map.NavigationGrid.GetClosestTerrainExit(closestCircleEdgePoint, PathfindingRadius);
                var path = Game.Map.PathingHandler.GetPath(Position, exit, this);
                if (path != null)
                {
                    SetWaypoints(path);
                }
                else
                {
                    SetWaypoints(new List<Vector2> { Position, exit });
                }

                UpdateMoveOrder(OrderType.MoveTo, true);
            }
            if (args.Target != null)
            {
                // Unit targeted.
                SetTargetUnit(args.Target, true);
                UpdateMoveOrder(OrderType.AttackTo, true);
            }
            else
            {
                SetTargetUnit(null, true);
            }
        }
    }

    /// <summary>
    /// Sets the spell that this unit is currently casting.
    /// </summary>
    /// <param name="s">Spell that is being cast.</param>
    internal void SetCastSpell(Spell s)
    {
        _castingSpell = s;
    }

    /// <summary>
    /// Gets the spell this unit is currently casting.
    /// </summary>
    /// <returns>Spell that is being cast.</returns>
    public Spell CastSpell => _castingSpell;


    /// <summary>
    /// Forces this unit to stop targeting the given unit.
    /// Applies to attacks, spell casts, spell channels, and any queued spell casts.
    /// </summary>
    /// <param name="target"></param>
    public void Untarget(AttackableUnit target)
    {
        if (TargetUnit == target)
        {
            SetTargetUnit(null, true);
        }

        if (_castingSpell != null)
        {
            _castingSpell.RemoveTarget(target);
        }
        if (ChannelSpell != null)
        {
            ChannelSpell.RemoveTarget(target);
        }
        if (SpellToCast != null)
        {
            SpellToCast.RemoveTarget(target);
        }
    }
    /// <summary>
    /// Sets this AI's current target unit. This relates to both auto attacks as well as general spell targeting.
    /// </summary>
    /// <param name="target">Unit to target.</param>
    /// 
    public void SetTargetUnit(AttackableUnit? target, bool networked = true, LostTargetEvent lostTargetEvent = LostTargetEvent.DEFAULT)
    {
        if (target == TargetUnit)
        {
            return;
        }

        var oldTarget = TargetUnit;
        TargetUnit = target;
        if (target == null && oldTarget != null)
        {
            ApiEventManager.OnTargetLost.Publish(this, oldTarget);
            AIScript.TargetLost(lostTargetEvent, oldTarget);

            //seem necessary ? 
            TargetS2CNotifier(this, null);
        }
        if (oldTarget is Champion)
        {
            TargetHeroS2CNotifier(this, null);
        }
        if (networked)
        {
            if (this is BaseTurret)
                TargetS2CNotifier(this, TargetUnit);

            if (TargetUnit != null && this is not BaseTurret)
                UnitSetLookAtNotifier(this, LookAtType.Unit, TargetUnit);

            if (TargetUnit is Champion)
            {
                TargetHeroS2CNotifier(this, TargetUnit);
            }
        }
    }


    public void SetSpellPos(Vector3 Pos)
    {
        if (SpellPosition == Pos)
        {
            return;
        }

        SpellPosition = Pos;
        if (Pos != null)
        {
            UnitSetLookAtNotifier(this, LookAtType.Location, null, Pos);
        }


    }

    /// <summary>
    /// Swaps the spells in the given slots
    /// </summary>
    /// <param name="sourceSlot">Slot the spell was previously in.</param>
    /// <param name="destinationSlot">Slot the spell was swapped to.</param>
    public void SwapSpells(byte sourceSlot, byte destinationSlot)
    {
        if (Spells[sourceSlot].IsAutoAttack || Spells[destinationSlot].IsAutoAttack)
        {
            return;
        }

        var srcSlotName = Spells[sourceSlot].SpellName;
        var destSlotName = Spells[destinationSlot].SpellName;

        var enabledBuffer = Stats.GetSpellEnabled(sourceSlot);
        var buffer = Spells[sourceSlot];
        Spells[sourceSlot] = Spells[destinationSlot];
        Spells[sourceSlot].Slot = sourceSlot;
        Spells[destinationSlot] = buffer;
        Spells[destinationSlot].Slot = destinationSlot;


        if (this is Champion champion)
        {
            int clientId = Game.PlayerManager.GetClientInfoByChampion(champion).ClientId;
            SetSpellDataNotifier(clientId, NetId, destSlotName, sourceSlot);
            SetSpellDataNotifier(clientId, NetId, srcSlotName, destinationSlot);
        }
    }

    /// <summary>
    /// Sets the spell that will be channeled by this unit. Used by Spell for manual stopping and networking.
    /// </summary>
    /// <param name="spell">Spell that is being channeled.</param>
    /// <param name="network">Whether or not to send the channeling of this spell to clients.</param>
    internal void SetChannelSpell(Spell spell, bool network = true)
    {
        ChannelSpell = spell;
    }

    /// <summary>
    /// Forces this AI to stop channeling based on the given condition with the given reason.
    /// </summary>
    /// <param name="condition">Canceled or successful?</param>
    /// <param name="reason">How it should be treated.</param>
    internal void StopChanneling(ChannelingStopCondition condition, ChannelingStopSource reason)
    {
        if (ChannelSpell != null)
        {
            ChannelSpell.StopChanneling(condition, reason);
            ChannelSpell = null;
        }
    }

    /// <summary>
    /// Gets the most recently spawned Pet unit which is owned by this unit.
    /// </summary>
    public Pet GetPet()
    {
        return _lastPetSpawned;
    }

    /// <summary>
    /// Sets the most recently spawned Pet unit which is owned by this unit.
    /// </summary>
    public void SetPet(Pet pet)
    {
        _lastPetSpawned = pet;
    }

    protected override void OnUpdateStats()
    {
        base.OnUpdateStats();

        try
        {
            ScriptInternal.OnUpdateStats();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }

        foreach (var spell in Spells)
        {
            spell?.UpdateStats();
        }

        ItemInventory.UpdateStats();
    }
    internal override void Update()
    {
        base.Update();

        try
        {
            ScriptInternal.OnUpdate();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }

        if (!_aiPaused)
        {
            try
            {
                AIScript.Update();
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
        }

        foreach (var spell in Spells)
        {
            spell?.Update();
        }

        ItemInventory.OnUpdate();

        UpdateAssistMarkers();
        UpdateTarget();

        if (_autoAttackCurrentCooldown > 0)
        {
            _autoAttackCurrentCooldown -= Game.Time.DeltaTimeSeconds;
        }

        if (_lastPetSpawned != null && _lastPetSpawned.Stats.IsDead)
        {
            SetPet(null);
        }

        /*if( this.MoveOrder == OrderType.Hold ||
            this.MoveOrder == OrderType.OrderNone ||
            this.MoveOrder == OrderType.Taunt ||
            this.MoveOrder == OrderType.Stop ||
           // this.MoveOrder == OrderType.AttackTo ||
            this.MoveOrder == OrderType.PetHardAttack ||
            this.MoveOrder == OrderType.PetHardStop

            )
        {*/
        //we update busyflags only when unit doesn't move 
        //   UpdateBusyFlag();
        // }
        //  ClearEmptyCells(UpdateCell());

    }




    private Vector2 lastUpdatedPosition;



    internal override void LateUpdate()
    {
        base.LateUpdate();

        // Stop targeting an untargetable unit.
        if (TargetUnit != null && (TargetUnit.Status & StatusFlags.Targetable) == 0)
        {
            if (TargetUnit.CharData.IsUseable)
            {
                return;
            }
            Untarget(TargetUnit);
        }
    }

    public override void TakeDamage(DamageData damageData, IEventSource sourceScript = null)
    {
        base.TakeDamage(damageData, sourceScript);

        if (damageData.Damage <= 0)
        {
            return;
        }

        if (damageData.Attacker is ObjAIBase obj)
        {
            AddAssistMarker(obj, 10.0f);
        }

        var objects = Game.ObjectManager.GetObjects();
        foreach (var it in objects)
        {
            if (it.Value is ObjAIBase u)
            {
                float acquisitionRange = u.Stats.AcquisitionRange.Total;
                float acquisitionRangeSquared = acquisitionRange * acquisitionRange;
                if (
                    u != this
                    && !u.Stats.IsDead
                    && u.Team == Team
                    && u.AIScript.AIScriptMetaData.HandlesCallsForHelp
                    && Vector2.DistanceSquared(u.Position, Position) <= acquisitionRangeSquared
                    && Vector2.DistanceSquared(u.Position, damageData.Attacker.Position) <= acquisitionRangeSquared
                )
                {
                    try
                    {
                        u.CallForHelp((ObjAIBase)damageData.Attacker, this);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(null, e);
                    }
                }
            }
        }
    }

    public virtual void CallForHelp(ObjAIBase attacker, ObjAIBase target)
    {
        AIScript.OnCallForHelp(attacker, this);
    }

    /// <summary>
    /// Updates this AI's current target and attack actions depending on conditions such as crowd control, death state, vision, distance to target, etc.
    /// Used for both auto and spell attacks.
    /// </summary>
    /// 
    public static bool hashackrange = false;

    private const int rangeIncrease = 200;
    private void UpdateTarget()
    {
        if (Stats.IsDead)
        {
            if (TargetUnit is not null)
            {
                CancelAutoAttack(true, true);
                SetTargetUnit(null);
            }
            return;
        }

        if (this is not LaneTurret && this is not BaseTurret)
        {
            //stun
            if ((this.Status & StatusFlags.Stunned) != 0)
            {
                if (MovementParameters == null)
                {
                    StopMovement();

                    var list = new List<Vector2>();
                    list.Add(this.Position);
                    SetWaypoints(list);
                }
                return;
            }

            //cage
            if ((this.Status & StatusFlags.CanMove) == 0)
            {
                if (this is not Minion)
                {
                    if (MovementParameters == null)
                    {
                        StopMovement();

                        var list = new List<Vector2>();
                        list.Add(this.Position);
                        SetWaypoints(list);
                    }
                    return;
                }


            }
        }


        var canAttack = CanAttack();

        if (canAttack && !_previousCanAttack)
        {
            AIScript.OnCanAttack();
        }

        _previousCanAttack = canAttack;






        ///HACK UGLY ASSFUCK 
        ///actually melee champ can't attack nexus/inhib due these building are in an wall ( not walkable cell ) 
        ///
        //if (TargetUnit != null && this.IsMelee)
        //{
        //    // 
        //    if ((TargetUnit is Inhibitor || TargetUnit is Nexus || TargetUnit is LaneTurret || TargetUnit is BaseTurret) && !hashackrange)
        //    {

        //        hashackrange = true;
        //        if (this.Stats.Range.FlatBonus < 190)  // 
        //        {
        //            this.Stats.Range.IncFlatBonus(rangeIncrease);
        //        }
        //    }
        //    else if (!(TargetUnit is Inhibitor || TargetUnit is Nexus || TargetUnit is LaneTurret || TargetUnit is BaseTurret) && hashackrange)
        //    {

        //        hashackrange = false;

        //        this.Stats.Range.IncFlatBonus(-rangeIncrease);

        //    }
        //}
        //else
        //{

        //    if (hashackrange)
        //    {
        //        hashackrange = false;

        //        this.Stats.Range.IncFlatBonus(-rangeIncrease);
        //    }
        //}

        //snapshot for avoid TargetUnit == null if at 1 tickrate the entity disapear or another 
        var target = TargetUnit;

        if (target == null)
        {
            if ((IsAttacking && !AutoAttackSpell.SpellData.CantCancelWhileWindingUp) || HasMadeInitialAttack)
            {
                CancelAutoAttack(!HasAutoAttacked, true);
            }
        }
        else if (target.Stats.IsDead ||
                 (!target.Status.HasFlag(StatusFlags.Targetable) && target.CharData.IsUseable) ||
                 !target.IsVisibleByTeam(Team))
        {
            if (IsAttacking)
            {
                CancelAutoAttack(!HasAutoAttacked, true);
            }

            if (!target.IsVisibleByTeam(Team))
            {
                SetTargetUnit(null, true, LostTargetEvent.LOST_VISIBILITY);
            }
            else
            {
                SetTargetUnit(null);
            }

            return;
        }

        else if (IsAttacking)
        {
            if (Vector2.Distance(TargetUnit.Position, Position) > GetTotalCancelAttackRange()
                && AutoAttackSpell.State == SpellState.CASTING && !AutoAttackSpell.SpellData.CantCancelWhileWindingUp)
            {
                CancelAutoAttack(!HasAutoAttacked, true);
            }

            if (AutoAttackSpell.State == SpellState.READY)
            {
                IsAttacking = false;
            }

            return;
        }


        if (SpellToCast is not null && TargetUnit is null && SpellToCastArguments?.Pos != null)
        {
            float idealRange = SpellToCast.CastRange;
            float distance = Vector2.DistanceSquared(SpellToCastArguments.Pos.Value.ToVector2(), Position);
            if (MoveOrder == OrderType.MoveTo && distance <= idealRange * idealRange)
            {
                StopMovement();
                SpellToCast.TryCast(SpellToCastArguments);
            }
            else
            {
                RefreshWaypoints(idealRange);
            }
        }
        else if (SpellToCast != null && TargetUnit != null && !IsAttacking)
        {

            float idealRange = SpellToCast.CastRange;
            // Spell casts usually do not take into account collision radius, thus range is center -> center VS edge -> edge for attacks.
            /*   if (SpellToCast.Caster.IsMelee && SpellToCast.CastRange < SpellToCast.Caster.GetTotalAttackRange())
               {
                   idealRange = SpellToCast.Caster.GetTotalAttackRange();
               }
             */

            var targetendpos = TargetUnit.Position;

            //test : sometimes an entity can be in an wall , for autoattack and another is complicated 
            /*  if (!Game.Map.PathingHandler.IsWalkable(targetendpos))
              {
                  targetendpos = Game.Map.NavigationGrid.GetClosestWalkableToInFunctionOfFrom(Position, targetendpos, TargetUnit.CollisionRadius);

              }
           */

            if ((MoveOrder is OrderType.AttackTo or OrderType.MoveTo) && Extensions.CheckCircleCollision(SpellToCast.Caster.Position, SpellToCast.CastRange, TargetUnit.Position, TargetUnit.CollisionRadius))

            {
                if (!SpellToCast.IsValidTarget(this, TargetUnit, SpellToCast.Data.Flags))
                {
                    StopMovement();
                }
                else
                {
                    if (SpellToCastArguments != null)
                    {
                        SpellToCast.TryCast(SpellToCastArguments);
                        StopMovement();
                        SetTargetUnit(null);
                    }
                }
            }
            else
            {
                RefreshWaypoints(idealRange);
            }




        }
        else if (TargetUnit != null && TargetUnit.Team != Team && MoveOrder != OrderType.CastSpell) // TODO: Verify if there are any other cases we want to avoid.
        {
            var idealRange = GetTotalAttackRange();

            var targetendpos = TargetUnit.Position;

            //test : sometimes an entity can be in an wall , for autoattack and another is complicated 
            /* if (!Game.Map.PathingHandler.IsWalkable(targetendpos))
             {
                 targetendpos = Game.Map.NavigationGrid.GetClosestWalkableToInFunctionOfFrom(Position, targetendpos, TargetUnit.CollisionRadius);
             }
             */

            if (TargetUnit != null)
            {
                //Console.WriteLine("TRY ATTACK ");
            }



            if (Vector2.DistanceSquared(Position, Extensions.GetClosestCircleEdgePoint(Position, targetendpos, TargetUnit.CollisionRadius)) <= idealRange * idealRange &&
                MovementParameters == null)
            {

                if (AutoAttackSpell != null)
                {
                    if (AutoAttackSpell.State == SpellState.READY)
                    {
                        // Stops us from continuing to move towards the target.
                        RefreshWaypoints(idealRange);

                        if (canAttack && AutoAttackOn)
                        {
                            // NOTE: Crit determination moved to Spell.Cast() to avoid multiple rolls per attack
                            // IsNextAutoCrit will be set properly when the attack actually executes
                            if (_autoAttackCurrentCooldown <= 0)
                            {
                                HasAutoAttacked = false;
                                //TODO: AutoAttackSpell.ResetSpellCast();
                                IsAttacking = true;

                                // Fix126
                                if (AutoAttackSpell.Slot >= (int)SpellSlotType.BasicAttackSlots)
                                {
                                    AutoAttackSpell = GetNewAutoAttack();
                                }
                                //TODO: Check where exactly it should be published.
                                // This callback is often used to override and skip auto attacks.
                                ApiEventManager.OnPreAttack.Publish(this, (AutoAttackSpell, TargetUnit));

                                if (!_skipNextAutoAttack)
                                {
                                    var aas = AutoAttackSpell;
                                    //TODO: Check where exactly it should be published.


                                    ApiEventManager.OnLaunchAttack.Publish(this, (AutoAttackSpell, TargetUnit));
                                    aas.TryCast(
                                        TargetUnit, TargetUnit.Position3D, TargetUnit.Position3D,
                                        overrideCoolDownCheck: true,
                                        useAutoAttackSpell: true,
                                        updateAutoAttackTimer: true
                                    );
                                    _autoAttackCurrentCooldown = 1.0f / Stats.GetTotalAttackSpeed();

                                    if (TargetUnit != null)
                                    {
                                        // Console.WriteLine("ACCOMPLISH  ");
                                    }

                                }
                                else
                                {
                                    _skipNextAutoAttack = false;
                                    _autoAttackCurrentCooldown = 1.0f / Stats.GetTotalAttackSpeed();
                                }
                            }
                        }
                    }
                }

            }

            else
            {
                if (this is LaneMinion && (TargetUnit is Champion))
                {
                    var boostedRange = idealRange + GlobalData.AIAttackTargetSelectionVariables.MinionTargetingHeroBoost;
                    if (Vector2.DistanceSquared(Position, TargetUnit.Position) > boostedRange * boostedRange)
                    {
                        SetTargetUnit(null, true, LostTargetEvent.OUT_OF_RANGE);
                        return;
                    }
                }



                if (Status.HasFlag(StatusFlags.CanMoveEver))
                {
                    RefreshWaypoints(idealRange);
                }
                else
                {
                    SetTargetUnit(null, true, LostTargetEvent.OUT_OF_RANGE);
                }
            }
        }
        else
        {
            if (AutoAttackSpell is { State: SpellState.READY } && IsAttacking)
            {
                IsAttacking = false;
                HasMadeInitialAttack = false;
            }
        }
    }

    public float GetTotalAttackRange()
    {
        return Stats.Range.Total + CollisionRadius + (TargetUnit is LaneTurret or Nexus or Inhibitor ? TargetUnit?.CollisionRadius ?? 0 : 0);
    }

    public float GetTotalCancelAttackRange()
    {
        return GetTotalAttackRange() + GlobalData.AttackRangeVariables.ClosingAttackRangeModifier;
    }

    public (OrderType OrderType, AttackableUnit TargetUnit, Vector2 TargetPosition, object OrderData) GetDelayedOrder()
    {
        if (_hasDelayedCastOrder)
        {
            return _delayedCastOrder;
        }

        if (_hasDelayedMovementOrder)
        {
            return _delayedMovementOrder;
        }

        return (OrderType.OrderNone, null, default, null);

    }

    public void IssueOrDelayOrder(OrderType orderType, AttackableUnit targetUnit, Vector2 targetPosition, dynamic data = null, bool fromAiScript = false)
    {
        // Console.WriteLine(this.Status);
        if (this is not LaneTurret && this is not BaseTurret)
        {
            if (Status.HasFlag(StatusFlags.Stunned))
            {
                StopMovement();
                var list = new List<Vector2>();
                list.Add(this.Position);
                SetWaypoints(list);
                return;
            }

            if (!Status.HasFlag(StatusFlags.CanMove))
            {
                if (this is not Minion)
                {
                    StopMovement();
                    var list = new List<Vector2>();
                    list.Add(this.Position);
                    SetWaypoints(list);
                    return;
                }
            }
        }

        if (IgnoreUserIssueOrder() && !fromAiScript)
            return;

        LastIssueOrderPosition = targetPosition;

        bool handledByAiScript = false;
        if (!fromAiScript)
            handledByAiScript = AIScript.OnOrder(orderType, targetUnit, targetPosition);

        if (handledByAiScript)
            return;

        if (orderType == OrderType.CastSpell)
        {
            _hasDelayedCastOrder = true;
            _delayedCastOrder = (orderType, targetUnit, targetPosition, data);
            return;
        }

        //TODO: Should it be here?
        if (SpellToCast != null)
        {
            SetSpellToCast(null);
        }

        if (CanChangeWaypoints())
        {
            IssueMovementOrder(orderType, targetUnit, targetPosition, data);
        }
        else
        {
            _hasDelayedMovementOrder = true;
            _delayedMovementOrder = (orderType, targetUnit, targetPosition, data);
        }
    }

    internal void IssueDelayedOrder()
    {
        //TODO: Is there a need for a corner case check?
        //if (!CanChangeWaypoints()) return;

        if (_hasDelayedCastOrder)
        {
            IssueCastOrder(_delayedCastOrder.OrderType, _delayedCastOrder.TargetUnit, _delayedCastOrder.TargetPosition, _delayedCastOrder.SpellSlot);
            return;
        }
        else if (_hasDelayedMovementOrder)
        {
            IssueMovementOrder(_delayedMovementOrder.OrderType, _delayedMovementOrder.TargetUnit, _delayedMovementOrder.TargetPosition, _delayedMovementOrder.Waypoints);
        }

        /*
        //TODO: Here we need to return to the previous state?
        // The fact that a unit has an target does not mean that it will move towards it (turrets)
        else if (TargetUnit != null)
        {
            IssueOrder(OrderType.AttackTo, TargetUnit);
        }
        else if (PathHasTrueEnd) //TODO: Continue movement after Dashes
        // (PathHasTrueEnd is reset with a call to SetWaypoints from DashToTarget)
        // (But it doesn't reset when the PathTrueEnd is reached)
        {
            IssueOrder(OrderType.MoveTo, null, PathTrueEnd);
        }
        */
        else
        {
            UpdateMoveOrder(OrderType.Hold, true);
        }
    }

    private void IssueCastOrder(OrderType orderType, AttackableUnit targetUnit = null, Vector2 targetPosition = default, byte spellSlot = default)
    {
        _hasDelayedCastOrder = false;
        _hasDelayedMovementOrder = false;

        if (Spells[spellSlot] != null)
        {
            Spells[spellSlot].TryCast(targetUnit, targetPosition.ToVector3(0), targetPosition.ToVector3(0));
        }
    }


    private bool IgnoreUserIssueOrder()
    {
        //we ignore user issue order if we are taunted, charmed, feared
        return (Status & StatusFlags.Taunted) != 0 || (Status & StatusFlags.Feared) != 0 ||
               (Status & StatusFlags.Charmed) != 0;
    }

    public void IssueMovementOrder(OrderType orderType, AttackableUnit? targetUnit = null, Vector2 targetPosition = default, List<Vector2>? waypoints = null)
    {
        if (Status != StatusFlags.CanMove || Status == StatusFlags.Stunned)
        {
            StopMovement();
            SetWaypoints([]);
        }

        _hasDelayedCastOrder = false;
        _hasDelayedMovementOrder = false;

        UpdateMoveOrder(orderType, true);

        if (orderType == OrderType.Stop)
        {
            SetAIState(AIState.AI_STOP);
            SetTargetUnit(null);
            return;
        }

        if (targetUnit == null)
        {
            PathTrueEnd = targetPosition;
            PathHasTrueEnd = true;
        }
        else
        {
            targetPosition = targetUnit.Position;
        }

        AutoAttackOn = true;

        //TODO: Perhaps there are cases where we don't want to recalculate the path.
        // if (waypoints != Waypoints) // If we are not on the right track.

        if (IsDestinationRestricted(targetPosition))
        {
            targetPosition = RestrictedAreaPos(targetPosition.ToVector3(targetPosition.Y), this).ToVector2();
        }

        List<Vector2> waypointsasync = default;

        if (waypoints == null || waypoints[0] != Position)
        {
            if (Waypoints.Count > 0 && Waypoints[^1] != targetPosition)
            {
                if (targetUnit == null)
                {
                    waypointsasync = Game.Map.NavigationGrid.GetPath(Position, targetPosition, this);

                }
                else
                {
                    waypointsasync = Game.Map.NavigationGrid.GetPath(Position, targetPosition, this);
                    // waypoints = Game.Map.NavigationGrid.GetPathToUnit(Position, targetUnit, PathfindingRadius, this);
                }
            }
        }

        if (orderType == OrderType.AttackMove)
        {
            if (!IsAttacking || AutoAttackSpell.State == SpellState.READY)
            {
                //SetWaypoints(waypoints);
                SetWaypoints(waypointsasync);
                SetTargetUnit(null, true);
            }
        }
        else
        {
            // SetWaypoints(waypoints);
            SetWaypoints(waypointsasync);
            SetTargetUnit(targetUnit, true);
        }

        if (orderType == OrderType.AttackTo && targetUnit != null &&
            Vector2.DistanceSquared(Position, targetUnit.Position) < GetTotalAttackRange() * GetTotalAttackRange())
        {
            RefreshWaypoints(GetTotalAttackRange());
        }
    }

    protected override void OnDashEnd()
    {
        IssueDelayedOrder();
    }
    protected override void OnReachedDestination()
    {
        AIScript.OnReachedDestinationForGoingToLastLocation();
        AIScript.OnStoppedMoving();
    }

    /// <summary>
    /// Sets this unit's move order to the given order.
    /// </summary>
    /// <param name="order">MoveOrder to set.</param>
    public void UpdateMoveOrder(OrderType order, bool publish = true)
    {
        if ((this.Status & StatusFlags.CanMove) == 0 || (this.Status & StatusFlags.Stunned) != 0)
        {

            StopMovement();
            SetWaypoints(new List<Vector2>());
        }

        if (publish)
        {
            // Return if scripts do not allow this order.
            if (!ApiEventManager.OnUnitUpdateMoveOrder.Publish(this, order))
            {
                return;
            }
        }

        MoveOrder = order;

        if (MoveOrder is OrderType.OrderNone or OrderType.Stop or OrderType.PetHardStop
            && !IsPathEnded())
        {
            StopMovement();
            SetTargetUnit(null, true);
        }

        if (MoveOrder is OrderType.Hold or OrderType.Taunt)
        {
            StopMovement();
        }
    }

    public AttackableUnit TargetAcquisition(Vector2 position, float range)
    {
        var distanceSqrToTarget = -1.0f;
        AttackableUnit nextTarget = null;

        foreach (var o in Game.Map.CollisionHandler.EnumerateNearestObjects(new Circle(position, range)))
        {
            if (o.Team == Team || o is not AttackableUnit u || (u.Status & StatusFlags.Targetable) == 0)
            {
                continue;
            }

            var dist2 = Vector2.DistanceSquared(position, u.Position);
            if (distanceSqrToTarget < 0 || dist2 <= distanceSqrToTarget)
            {
                distanceSqrToTarget = dist2;
                nextTarget = u;
            }
        }

        if (nextTarget != null)
        {
            return nextTarget;
        }

        return null;
    }

    public override void OnCollision(GameObject collider, CollisionTypeOurs collisionType)
    {
        base.OnCollision(collider, collisionType);
        if (collider is AttackableUnit unit)
        {
            if ((unit.Status & StatusFlags.Ghosted) == 0 && (this.Status & StatusFlags.Ghosted) == 0)
                AIScript.OnCollision(unit);
        }
        else
            AIScript.OnCollisionTerrain();
    }

    public virtual bool IsValidTarget(AttackableUnit target)
    {
        return target.Team != this.Team && (target.Status & StatusFlags.Targetable) != 0 &&
               target.IsVisibleByTeam(this.Team);
    }


    protected override void OnCanMove()
    {
        AIScript.OnCanMove();
    }

    /// <summary>
    /// Gets the state of this unit's AI.
    /// </summary>
    public AIState GetAIState()
    {
        return _aiState;
    }

    /// <summary>
    /// Sets the state of this unit's AI.
    /// </summary>
    /// <param name="newState">State to set.</param>
    public void SetAIState(AIState newState, bool publish = false)
    {
        _aiState = newState;
        if (publish)
            S2C_AIStateNotify(this, newState);
    }

    /// <summary>
    /// Whether or not this unit's AI is innactive.
    /// </summary>
    public bool IsAIPaused()
    {
        return _aiPaused;
    }

    /// <summary>
    /// Forces this unit's AI to pause/unpause.
    /// </summary>
    /// <param name="isPaused">Whether or not to pause.</param>
    public void PauseAI(bool isPaused)
    {
        if (isPaused)
            AIScript.HaltAI();
        else
            AIScript.Init(this); //HACK: there is no resume AI in Riot's scripts (except river crab), soo yeah we do a second init
        _aiPaused = isPaused;
    }

    /// <summary>
    /// Gets the HashString for this unit's model. Used for packets so clients know what data to load.
    /// </summary>
    /// <returns>Hashed string of this unit's model.</returns>
    internal override uint GetObjHash()
    {
        var gobj = "[Character]" + Model;
        if (HasSkins)
        {
            var szSkin = SkinID < 10 ? "0" + SkinID : SkinID.ToString();
            gobj += szSkin;
        }
        return HashFunctions.HashStringNorm(gobj);
    }

    internal void AddAssistMarker(ObjAIBase sourceUnit, float duration, DamageData damageData = null)
    {
        if (sourceUnit is Champion)
        {
            if (sourceUnit.Team == Team)
            {
                AuxAddAssistMarker(AlliedAssistMarkers, sourceUnit, duration, damageData);
            }
            else
            {
                AuxAddAssistMarker(EnemyAssistMarkers, sourceUnit, duration, damageData);
            }
        }
    }

    static void AuxAddAssistMarker(List<AssistMarker> assistList, ObjAIBase sourceUnit, float duration, DamageData damageData = null)
    {
        AssistMarker? assistMarker = assistList.Find(x => x.Source == sourceUnit);
        if (assistMarker is not null)
        {
            float desiredDuration = Game.Time.GameTime + duration * 1000;
            assistMarker.StartTime = Game.Time.GameTime;
            assistMarker.EndTime = assistMarker.EndTime < desiredDuration ? desiredDuration : assistMarker.EndTime;
        }
        else
        {
            assistMarker = new()
            {
                Source = sourceUnit,
                StartTime = Game.Time.GameTime,
                EndTime = Game.Time.GameTime + duration * 1000
            };

            assistList.Add(assistMarker);
        }

        if (damageData is not null)
        {
            switch (damageData.DamageType)
            {
                case DamageType.DAMAGE_TYPE_PHYSICAL:
                    assistMarker.PhysicalDamage += damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_MAGICAL:
                    assistMarker.MagicalDamage += damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_TRUE:
                    assistMarker.TrueDamage += damageData.Damage;
                    break;
            }
        }

        // Sort in-place by descending StartTime using insertion sort (efficient for small lists)
        for (int i = 1; i < assistList.Count; i++)
        {
            var key = assistList[i];
            int j = i - 1;
            while (j >= 0 && assistList[j].StartTime < key.StartTime)
            {
                assistList[j + 1] = assistList[j];
                j--;
            }
            assistList[j + 1] = key;
        }
    }

    private void UpdateAssistMarkers()
    {
        //Maybe optimize this later since it's a sorted list?
        RemoveExpiredMarkers(AlliedAssistMarkers);
        RemoveExpiredMarkers(EnemyAssistMarkers);
    }

    private static void RemoveExpiredMarkers(List<AssistMarker> markers)
    {
        for (int i = markers.Count - 1; i >= 0; i--)
        {
            if (markers[i].EndTime < Game.Time.GameTime)
            {
                markers.RemoveAt(i);
            }
        }
    }

    public int PushCharacterData(string skinName, bool overrideSpells)
    {
        // var baserange = this.CharData.AttackRange;
        int id = _characterData.Count;
        _characterData.Add(new CharacterData(id, skinName));

        //this is only for heimer and never used on other champ at first view, i think normally we need change entire 
        //set of spell of the entity , for moment we will change only the basicattackslot 
        if (overrideSpells)
        {

            for (short i = 0; i < CharData.BasicAttacks.Length; i++)
            {
                this.CharData.BasicAttacks[i] = new BasicAttackInfo(
                this.CharData.AttackDelayOffsetPercent,
                this.CharData.AttackDelayCastOffsetPercent,
                this.CharData.AttackDelayCastOffsetPercentAttackSpeedRatio,
                name: skinName + "BasicAttack",
                attackCastTime: this.CharData.AttackCastTime,
                attackTotalTime: this.CharData.AttackTotalTime,
                probability: this.CharData.BaseAttackProbability
                    );



                var aaName = CharData.BasicAttacks[i].Name;
                //if (!string.IsNullOrEmpty(aaName))
                // If you ask the client to launch an attack for which it does not have an ini,
                // then even if the attack is in the champion ini, the client will pause the animation.
                if (ContentManager.GetSpellData(aaName) != null)
                {
                    // Fix126
                    int slot = i + (int)SpellSlotType.BasicAttackSlots;
                    Spells[(byte)slot] = new Spell(this, aaName, (byte)slot);
                }
            }


        }
        ChangeCharacterDataNotifier(this, skinName, countchangemodel: this.CountChangeModel);
        this.CountChangeModel++;
        return id;
    }

    public int PushCharacterData2(string skinName, bool overrideSpells)
    {
        // var baserange = this.CharData.AttackRange;
        int id = _characterData.Count;
        var chardata = new CharacterData(id, this.Model);
        oldmodelname = this.Model;
        _characterData.Add(chardata);
        this.CountChangeModel++;

        this.ChangeModel(skinName, overrideSpells);


        var backup = CharData.AttackRange;
        var CharData2 = ContentManager.GetCharData(skinName) ?? CharData;

        if (backup != CharData2.AttackRange)
        {
            this.Stats.Range.IncBaseValuePerm(CharData2.AttackRange - backup);
        }

        ChangeCharacterDataNotifier(this, skinName, overrideSpells, true, countchangemodel: this.CountChangeModel);
        if (overrideSpells)
        {
            //this seem necessary for the server 
            ApplyChangeSpell(true, skinName);
        }
        return id;
    }


    public int PushCharacterData3(string skinName, bool overrideSpells)
    {
        // var baserange = this.CharData.AttackRange;
        int id = _characterData.Count;
        var chardata = new CharacterData(id, this.Model);
        oldmodelname = this.Model;
        _characterData.Add(chardata);
        this.CountChangeModel++;

        this.ChangeModel(skinName, overrideSpells);


        var backup = CharData.AttackRange;
        var CharData2 = ContentManager.GetCharData(skinName) ?? CharData;

        if (backup != CharData2.AttackRange)
        {
            this.Stats.Range.IncBaseValuePerm(CharData2.AttackRange - backup);
        }

        ChangeCharacterDataNotifier(this, skinName, overrideSpells, true, countchangemodel: this.CountChangeModel);
        if (overrideSpells)
        {
            //this seem necessary for the server 
            ApplyChangeSpell3(true, skinName);
        }
        return id;
    }


    public void ApplyChangeSpell(bool forceuh = false, string modelname = "")
    {

        var CharData2 = ContentManager.GetCharData(modelname) ?? CharData;
        //Console.WriteLine(CharData2.SpellNames[0]);
        // Console.WriteLine(this.Spells[0].Name);

        SetSpell(CharData2.SpellNames[0], 0, true, force: forceuh);
        // Console.WriteLine(this.Spells[0].Name);
        SetSpell(CharData2.SpellNames[1], 1, true, force: forceuh);
        SetSpell(CharData2.SpellNames[2], 2, true, force: forceuh);
        //  SetSpell(this.CharData.SpellNames[3], 3, true);
        if (CharData2.ExtraSpells.Length > 0)
        {
            int i = 0;
            foreach (string extspell in CharData2.ExtraSpells)
            {
                SetSpell(extspell, i + (int)SpellSlotType.ExtraSlots, true, force: forceuh);
                i++;
            }
        }


    }

    public void ApplyChangeSpell3(bool forceuh = false, string modelname = "")
    {

        var CharData2 = ContentManager.GetCharData(modelname) ?? CharData;
        //Console.WriteLine(CharData2.SpellNames[0]);
        // Console.WriteLine(this.Spells[0].Name);

        SetSpell(CharData2.SpellNames[0], 0, true, force: forceuh);
        // Console.WriteLine(this.Spells[0].Name);
        SetSpell(CharData2.SpellNames[1], 1, true, force: forceuh);
        SetSpell(CharData2.SpellNames[2], 2, true, force: forceuh);
        SetSpell(CharData2.SpellNames[3], 3, true, force: forceuh);
        //  SetSpell(this.CharData.SpellNames[3], 3, true);
        if (CharData2.ExtraSpells.Length > 0)
        {
            int i = 0;
            foreach (string extspell in CharData2.ExtraSpells)
            {
                SetSpell(extspell, i + (int)SpellSlotType.ExtraSlots, true, force: forceuh);
                i++;
            }
        }


    }

    public void PopAllCharacterData()
    {
        _characterData.Clear();
        ChangeCharacterDataNotifier(
            this,
            skinName: Model,
            modelOnly: true, //TODO: Verify
            overrideSpells: true
        );

    }

    public void PopCharacterData(int id)
    {
        int i = _characterData.FindIndex(cd => cd.id == id);
        if (i == -1)
        {
            return;
        }
        var cd = _characterData[i];
        if (i == _characterData.Count - 1)
        {
            var prevCD = (i > 0) ? _characterData[i - 1] : new CharacterData(0, Model);

            /*Game.PacketNotifier.NotifyS2C_ChangeCharacterData(
                this,
                skinName: prevCD.skin,
                modelOnly: true, //TODO: Verify
                overrideSpells: false //TODO:
            );*/
            PopCharacterDataNotifier(
                this,
             this.CountChangeModel
            );
        }
        _characterData.RemoveAt(i);

    }


    public void PopCharacterData2(int id, bool overidespell = false)
    {
        int i = _characterData.FindIndex(cd => cd.id == id);
        if (i == -1)
        {
            return;
        }
        var cd = _characterData[i];
        if (i == _characterData.Count - 1)
        {
            var prevCD = (i > 0) ? _characterData[i - 1] : new CharacterData(0, Model);

            /*Game.PacketNotifier.NotifyS2C_ChangeCharacterData(
                this,
                skinName: prevCD.skin,
                modelOnly: true, //TODO: Verify
                overrideSpells: false //TODO:
            );*/
            PopCharacterDataNotifier(
                this,
             this.CountChangeModel
            );
            if (overidespell)
            {
                this.ChangeModel(this.oldmodelname, overidespell);
                ApplyChangeSpell(true, this.oldmodelname);
                // DebugSay(this, "");
            }
        }


        var backup = this.Stats.Range.Total;
        var CharData2 = ContentManager.GetCharData(this.oldmodelname) ?? CharData;

        if (backup != CharData2.AttackRange)
        {
            this.Stats.Range.IncBaseValuePerm(CharData2.AttackRange - backup);
        }

        _characterData.RemoveAt(i);

    }

    //Deprecate?
    /// <summary>
    /// Sets this unit's current model to the specified internally named model. *NOTE*: If the model is not present in the client files, all connected players will crash.
    /// </summary>
    /// <param name="model">Internally named model to set.</param>
    /// <returns></returns>
    /// TODO: Implement model verification (perhaps by making a list of all models in Content) so that clients don't crash if a model which doesn't exist in client files is given.
    public bool ChangeModel(string model, bool overridespell = false)
    {
        if (Model == model)
        {
            return false;
        }

        Model = model;
        if (overridespell)
        {

        }
        //   Game.PacketNotifier.NotifyS2C_ChangeCharacterData(this, Model);
        return true;
    }

    internal ObjAIBase? GetGoldRedirectTarget()
    {
        ObjAIBase? target = null;
        GoldRedirectTarget?.TryGetTarget(out target);
        return target;
    }

    internal void ChangeGoldRedirectTarget(ObjAIBase? target)
    {
        if (target is null)
        {
            GoldRedirectTarget = null;
            return;
        }

        GoldRedirectTarget = new(target);
    }
}