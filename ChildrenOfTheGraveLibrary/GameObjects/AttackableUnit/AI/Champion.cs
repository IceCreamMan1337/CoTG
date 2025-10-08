using System;
using System.Collections.Generic;
using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.NetInfo;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using SiphoningStrike.Game.Events;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.API;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Inventory;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;
using ChildrenOfTheGraveLibrary.GameObjects;
using MoonSharp.Interpreter;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua;
using ChildrenOfTheGraveLibrary.Managers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings;
using static ChildrenOfTheGraveEnumNetwork.Content.HashFunctions;
using System.Linq;
using FLS = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua.Functions;
using static PacketVersioning.PktVersioning;
using ChildrenOfTheGraveEnumNetwork;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

public class Champion : ObjAIBase, IExperienceOwner
{
    private static ILog _logger = LoggerProvider.GetLogger();
    /// <summary>
    /// Player number ordered by the config file.
    /// </summary>
    public int ClientId { get; private set; }
    public RuneInventory RuneList { get; }
    public TalentInventory TalentInventory { get; set; }
    public ChampionStats ChampionStats { get; protected set; }
    public ChampionStatistics ChampionStatistics { get; private set; }
    public Experience Experience { get; init; }
    public override bool SpawnShouldBeHidden => false;
    public override bool HasSkins => true;

    // 126Fix
    public List<EventHistoryEntry> EventHistory { get; } = new();

    public Table BBAvatarVars = LuaScriptEngine.NewTable();

    public BehaviourTree BehaviourTree = null;

    /// <summary>
    /// Indicates if the behaviour tree has been initialized to avoid duplicates with AIManager
    /// </summary>
    public bool HasBehaviourTreeInitialized { get; private set; } = false;

    // Gestion des AITask directement dans Champion
    public List<AITask> AssignedTasks { get; private set; } = new List<AITask>();
    public AITask CurrentTask { get; private set; } = null;
    private Dictionary<AITask, Dictionary<string, object>> _taskBehaviorTrees = new();

    private DeathData _lastDeathData;

    public long player_id;

    public bool InPool { get; internal set; } // Controlled by a SpawnPoint.
    public bool ShopForceEnabled;
    public bool ShopEnabled => InPool || ShopForceEnabled;

    public EntityDiffcultyType AIDifficulty { get; set; }

    public Champion(string model,
                    ClientInfo clientInfo,
                    uint netId = 0,
                    TeamId team = TeamId.TEAM_ORDER,
                    Stats? stats = null,
                    ChampionStats? championStats = null,
                    EntityDiffcultyType difficultyai = EntityDiffcultyType.NONE,
                    bool useDoomSpells = false
        )
        : base(model, clientInfo.Name, new Vector2(), 1200, clientInfo.SkinNo, netId, team, stats)
    {
        //TODO: Champion.ClientInfo?


        player_id = clientInfo.PlayerId;
        ClientId = clientInfo.ClientId;

        RuneList = clientInfo.Runes;
        TalentInventory = clientInfo.Talents;
        ChampionStats = championStats is not null ? championStats : new();

        ChampionStatistics = new()
        {
            TeamId = (int)Team
        };

        Experience = new(this);
        float amount = GlobalData.ChampionVariables.AmbientGoldAmount;
        float interval = GlobalData.ChampionVariables.AmbientGoldInterval;
        Stats.GoldPerSecond.BaseValue = amount / interval;
        //TODO: automaticaly rise spell levels with CharData.SpellLevelsUp

        Spells[(int)SpellSlotType.SummonerSpellSlots] = new Spell(this, clientInfo.SummonerSkills[0], (int)SpellSlotType.SummonerSpellSlots);
        Spells[(int)SpellSlotType.SummonerSpellSlots].LevelUp();
        Spells[(int)SpellSlotType.SummonerSpellSlots + 1] = new Spell(this, clientInfo.SummonerSkills[1], (int)SpellSlotType.SummonerSpellSlots + 1);
        Spells[(int)SpellSlotType.SummonerSpellSlots + 1].LevelUp();

        Replication = new ReplicationHero(this);

        if (clientInfo.PlayerId <= -1)
        {
            IsBot = true;
            AIDifficulty = difficultyai;
            if (useDoomSpells)
            {
                SetDoomBotSpells();
            }
            if (Game.Map.Id != 1)
            {
                LoadOLDBehaviourTree();
            }
            else
            {
                LoadBehaviourTree();
            }
        }


    }
    internal void LoadOLDBehaviourTree()
    {
        BehaviourTree = Game.ScriptEngine.CreateObject<BehaviourTree>($"BehaviourTrees.Map{Game.Map.Id}", Model) ?? new BehaviourTree(this);
        BehaviourTree.Owner = this;
    }
    internal void LoadBehaviourTree()
    {
        if (Game.Config.EnableLogBehaviourTree)
            _logger.Debug($"LoadBehaviourTree: Chargement du behaviour tree pour le champion '{Model}' (Team: {Team})");

        // New system: load advanced behaviour tree from Behaviourtree/Bot
        string championName = Model;
        string advancedTreeNamespace = "BehaviourTrees.all";
        string advancedTreeClass = $"{championName}BehaviorClass";
        if (Game.Config.EnableLogBehaviourTree)
            _logger.Debug($"LoadBehaviourTree: Tentative de chargement du behaviour tree avancé: {advancedTreeNamespace}.{advancedTreeClass}");

        // Try to load the champion-specific advanced behaviour tree
        BehaviourTree = Game.ScriptEngine.CreateObject<BehaviourTree>(advancedTreeNamespace, advancedTreeClass);

        if (BehaviourTree != null)
        {
            if (Game.Config.EnableLogBehaviourTree)
                _logger.Debug($"LoadBehaviourTree: Behaviour tree avancé chargé avec succès pour '{championName}'");
            // Set the owner immediately after creation and propagate it to child nodes
            BehaviourTree.SetOwner(this);
        }
        else
        {
            if (Game.Config.EnableLogBehaviourTree)
                _logger.Debug($"LoadBehaviourTree: Échec du chargement du behaviour tree avancé, fallback sur l'ancien système");

            // If the champion doesn't have an advanced behaviour tree, fallback to the old system
            string fallbackNamespace = $"BehaviourTrees.Map{Game.Map.Id}";
            if (Game.Config.EnableLogBehaviourTree)
                _logger.Debug($"LoadBehaviourTree: Tentative de chargement du fallback: {fallbackNamespace}.{Model}");

            BehaviourTree = Game.ScriptEngine.CreateObject<BehaviourTree>(fallbackNamespace, Model);

            if (BehaviourTree != null)
            {
                if (Game.Config.EnableLogBehaviourTree)
                    _logger.Debug($"LoadBehaviourTree: Behaviour tree fallback chargé avec succès");
                // Set the owner immediately after creation and propagate it to child nodes
                BehaviourTree.SetOwner(this);
            }
            else
            {
                if (Game.Config.EnableLogBehaviourTree)
                    _logger.Debug($"LoadBehaviourTree: Échec du chargement du fallback, création d'un BehaviourTree par défaut");
                // Create a default BehaviourTree with the correct owner
                BehaviourTree = new BehaviourTree(this);
            }
        }
        if (Game.Config.EnableLogBehaviourTree)
            _logger.Debug($"LoadBehaviourTree: Behaviour tree final: {BehaviourTree.GetType().Name} pour le champion '{Model}' (Owner: {BehaviourTree.Owner?.Name ?? "null"})");

        SetAIState(AIState.AI_SOFTATTACK);
        // Mark that the behaviour tree has been initialized to avoid duplicates with AIManager
        HasBehaviourTreeInitialized = true;
    }
    private static ILog Logger = LoggerProvider.GetLogger();
    internal override void OnAdded()
    {
        Game.ObjectManager.AddChampion(this);
        base.OnAdded();
        TalentInventory.Activate(this);

        ItemData? bluePill = ContentManager.GetItemData(2001); //Game.Map.GameMode.MapScriptMetadata.RecallSpellItemId
        if (bluePill is not null)
        {
            if (!string.IsNullOrEmpty(bluePill.SpellName))
            {
                Spells[10] = new Spell(this, bluePill.SpellName, 10);
                Stats.SetSpellEnabled((byte)SpellSlotType.BluePillSlot, true);
                ItemInventory.SetItemToSlot(bluePill, 6);
            }
            ItemInventory.SetExtraItem(10, bluePill);
        }

        // Runes
        byte runeItemSlot = 8;
        foreach (var rune in RuneList.Runes)
        {
            var runeItem = ContentManager.GetItemData(rune.Value);
            var newRune = ItemInventory.SetExtraItem(runeItemSlot, runeItem);
            AddStatModifier(runeItem);
            runeItemSlot++;
        }
        Stats.SetSummonerSpellEnabled(0, true);
        Stats.SetSummonerSpellEnabled(1, true);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        var peerInfo = Game.PlayerManager.GetClientInfoByChampion(this);
        SwapItemsNotify(peerInfo, userId, team, doVision);
        AvatarInfoNotify(peerInfo, userId);

        bool ownChamp = peerInfo.ClientId == userId;

        if (ownChamp)
        {
            int relativeBluePillSlot = (int)SpellSlotType.BluePillSlot;
            var itemInstance = ItemInventory.GetItem((byte)relativeBluePillSlot);
            BuyItemNotify(this, itemInstance);
            foreach (var spell in Spells)
            {
                if (spell == null)
                {
                    continue;
                }

                if (spell.Level > 0)
                {
                    // NotifyNPC_UpgradeSpellAns has no effect here
                    SetSpellLevelNotifier(userId, NetId, spell.Slot, spell.Level);

                    float currentCD = spell.CurrentCooldown;
                    float totalCD = spell.Cooldown;
                    if (currentCD > 0)
                    {
                        // 126Fix
                        CHAR_SetCooldownNotify(this, spell.Slot, currentCD, totalCD, userId);
                    }
                }
            }
        }
    }
    internal void setupSquadPushLane(Lane laneid, bool reversepath)
    {
        this.SetupLane(laneid);
        if (reversepath)
        {
            var lane = new List<Vector2>();
            var waypoints = new List<Vector2>();
            lane.Add(this.Position);
            waypoints.AddRange(Game.Map.NavigationPoints[laneid].Select(x => x.Position));
            List<Vector2> orderedWaypoints = new();
            Vector2 currentPoint = this.Position;
            orderedWaypoints.Add(currentPoint);
            while (waypoints.Count > 0)
            {
                Vector2 closestPoint = waypoints
                    .OrderBy(point => Vector2.Distance(currentPoint, point))
                    .First();
                orderedWaypoints.Add(closestPoint);
                waypoints.Remove(closestPoint);
                currentPoint = closestPoint;
            }
            FLS.TeleportToPosition(this, orderedWaypoints[0].ToVector3(100));
            this.Setupwaypointlane(orderedWaypoints);
            this.lanetofollow.Reverse();
        }
        else
        {
            var lane = new List<Vector2>();
            var waypoints = new List<Vector2>();
            lane.Add(this.Position);
            waypoints.AddRange(Game.Map.NavigationPoints[laneid].Select(x => x.Position));
            List<Vector2> orderedWaypoints = new();
            Vector2 currentPoint = this.Position;
            orderedWaypoints.Add(currentPoint);
            while (waypoints.Count > 0)
            {
                Vector2 closestPoint = waypoints
                    .OrderBy(point => Vector2.Distance(currentPoint, point))
                    .First();
                orderedWaypoints.Add(closestPoint);
                waypoints.Remove(closestPoint);
                currentPoint = closestPoint;
            }
            FLS.TeleportToPosition(this, orderedWaypoints[0].ToVector3(100));
            this.Setupwaypointlane(orderedWaypoints);
        }

    }
    internal override void OnRemoved()
    {
        base.OnRemoved();
        Game.ObjectManager.RemoveChampion(this);
    }

    public Vector2 GetSpawnPosition()
    {
        return Game.Map.SpawnPoints.TryGetValue(Team, out SpawnPoint? spawnPoint) ? spawnPoint.Position : Game.Map.NavigationGrid.MiddleOfMap;
    }

    internal override bool LevelUpSpell(byte slot, bool spendtrainingpoint = true)
    {
        if (Experience.SpellTrainingPoints.TrainingPoints is 0 && spendtrainingpoint)
        {
            return false;
        }

        if (!base.LevelUpSpell(slot))
        {
            return false;
        }

        if (spendtrainingpoint)
        {
            Experience.SpellTrainingPoints.SpendTrainingPoint();
        }

        if (Spells[slot].Level is 1)
        {
            Spells[slot].Enabled = true;
        }

        NPC_UpgradeSpellAnsNotify(ClientId, NetId, slot, Spells[slot].Level, Experience.SpellTrainingPoints.TrainingPoints);

        return true;
    }

    public void SetDoomBotSpells()
    {
        for (short i = 0; i < CharData.SpellNames.Length; i++)
        {
            if (!string.IsNullOrEmpty(CharData.SpellNames[i]))
            {
                string nameofspell = CharData.SpellNames[i];
                Spells[i] = new Spell(this, $"DoomBot_{nameofspell}", (byte)i);
            }
        }
    }

    private float _EXPTimer;
    private float _goldTimer;

    // Nouveau système de mise à jour des behaviour trees
    private float _behaviourTreeUpdateInterval = 0.5f; // 0.5 secondes entre chaque mise à jour
    private float _lastBehaviourTreeUpdate = 0f;
    private float _botUpdateOffset = 0f; // Décalage unique par bot pour éviter les blocages simultanés
    private float _accumulatedTime = 0f; // Temps accumulé avec DeltaTime

    private float tickupdatemanager = 0.1f;
    private float _lasttickupdatemanager;

    private float tickupdatemovement = 0.5f;
    internal override void Update()
    {
        if (Experience.Level < Experience.MAX_LEVEL && Experience.Exp >= Experience.ExpToNextLevel())
        {
            Experience.LevelUp();
            Stats.LevelUp();
        }

        base.Update();

        // Nouveau système de mise à jour des behaviour trees avec DeltaTime
        if (BehaviourTree != null)
        {
            // Accumuler le temps avec ScaledDeltaTime (temps accéléré pour la simulation)
            _accumulatedTime += Game.Time.ScaledDeltaTime / 1000f; // Convertir en secondes
            // Initialiser le décalage unique pour ce bot (basé sur son NetId)
            if (_botUpdateOffset == 0f)
            {
                // Utiliser une meilleure méthode pour les NetId élevés
                // Prendre les 2 derniers chiffres du NetId pour avoir une distribution plus équilibrée
                int lastTwoDigits = (int)(NetId % 100); // 0-99
                _botUpdateOffset = lastTwoDigits / 10.0f * 0.1f; // 0.0, 0.1, 0.2, ..., 0.9

                // Alternative : utiliser le hash du NetId pour une distribution plus aléatoire
                // _botUpdateOffset = (NetId.GetHashCode() % 10) * 0.1f;
            }

            // Vérifier si c'est le moment de mettre à jour ce bot spécifique
            float timeSinceLastUpdate = _accumulatedTime - _lastBehaviourTreeUpdate;
            if (timeSinceLastUpdate >= _behaviourTreeUpdateInterval)
            {
                // Appliquer le décalage pour ce bot
                float adjustedTime = _accumulatedTime + _botUpdateOffset;
                if ((adjustedTime % _behaviourTreeUpdateInterval) < 0.1f) // Fenêtre de 0.1s pour la mise à jour
                {
                    BehaviourTree.Update();
                    _lastBehaviourTreeUpdate = _accumulatedTime;
                }
            }
        }
        if (ChampionStats.IsGeneratingGold)
        {
            _goldTimer -= Game.Time.ScaledDeltaTime;
            if (_goldTimer <= 0)
            {
                float interval = GlobalData.ChampionVariables.AmbientGoldInterval;
                GoldOwner.AddGold(Stats.GoldPerSecond.Total * interval, false);
                _goldTimer = interval;
            }
        }
        else if (Game.Time.GameTime >= GlobalData.ObjAIBaseVariables.AmbientGoldDelay)
        {
            ChampionStats.IsGeneratingGold = true;
        }
        if (GlobalData.ChampionVariables.AmbientXPAmount > 0)
        {
            _EXPTimer -= Game.Time.ScaledDeltaTime;
            if (_EXPTimer <= 0)
            {
                Experience.AddEXP(GlobalData.ChampionVariables.AmbientXPAmount, false);
                _EXPTimer = GlobalData.ChampionVariables.AmbientXPInterval;
            }
        }
        if (Stats.IsDead || Stats.IsZombie)
        {
            Stats.RespawnTimer -= Game.Time.ScaledDeltaTime;
            if (Stats.RespawnTimer <= 0 && !Stats.IsZombie)
            {
                Respawn();
            }
        }

        // TODO: Find out the best way to bulk send these for all champions (tool tip handler?).
        // League sends a single packet detailing every champion's tool tip changes.
        //TODO: What about buffs on non-champions that can also have tooltips?
        List<ToolTipData> _tipsChanged = new();
        foreach (var spell in Spells)
        {
            if (spell?.ToolTipData.Changed == true)
            {
                _tipsChanged.Add(spell.ToolTipData);
                spell.ToolTipData.MarkAsUnchanged();
            }
        }
        //TODO: Either spawn scripts instead of buffs on stack, or have ToolTipData in the slot
        foreach (var buff in Buffs.All())
        {
            if (buff.ToolTipData.Changed)
            {
                _tipsChanged.Add(buff.ToolTipData);
                buff.ToolTipData.MarkAsUnchanged();
            }
        }
        if (_tipsChanged.Count > 0)
        {
            ToolTipVarsNotify(_tipsChanged);
        }


        //  _lasttickupdatemanager = time;
        //}

    }

    public void Respawn()
    {
        var spawnPos = GetSpawnPosition();
        SetPosition(spawnPos);

        float parToRestore = 0;
        // TODO: Find a better way to do this, perhaps through scripts. Otherwise, make sure all types are accounted for.
        if (Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.MANA || Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.Energy || Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.Wind)
        {
            parToRestore = Stats.ManaPoints.Total;
        }
        Stats.CurrentMana = parToRestore;
        Stats.CurrentHealth = Stats.HealthPoints.Total;
        Stats.IsDead = false;
        Stats.IsZombie = false;
        ShopForceEnabled = false;
        PopAllCharacterData();
        SetStatus(
            StatusFlags.CanAttack | StatusFlags.CanCast |
            StatusFlags.CanMove | StatusFlags.CanMoveEver |
            StatusFlags.Targetable, true
        );
        Stats.RespawnTimer = -1;

        SetDashingState(false, MoveStopReason.HeroReincarnate);
        ResetWaypoints();

        HeroReincarnateAliveNotify(this, parToRestore);
        ApiEventManager.OnResurrect.Publish(this);
    }

    internal bool OnDisconnect()
    {
        ItemInventory.ClearUndoHistory();
        ApiEventManager.OnDisconnect.Publish(this);
        StopMovement();
        SetWaypoints(Game.Map.PathingHandler.GetPath(Position, GetSpawnPosition(), this));
        UpdateMoveOrder(OrderType.MoveTo, true);
        return true;
    }

    internal void OnKill(DeathData deathData)
    {
        ApiEventManager.OnKill.Publish(deathData.Killer, deathData);

        if (deathData.Unit is Minion)
        {
            ChampionStatistics.MinionsKilled++;
            if (deathData.Unit.Team == TeamId.TEAM_NEUTRAL)
            {
                ChampionStatistics.NeutralMinionsKilled++;
            }

            var gold = deathData.Unit.Stats.GoldGivenOnDeath.Total;
            if (gold <= 0)
            {
                return;
            }

            //GoldOwner.AddGold(gold, true, deathData.Unit);

            if (ChampionStats.DeathSpree > 0)
            {
                ChampionStats.GoldFromMinions += gold;
            }

            if (ChampionStats.GoldFromMinions >= 1000)
            {
                ChampionStats.GoldFromMinions -= 1000;
                ChampionStats.DeathSpree--;
            }
        }
    }

    internal float GetRespawnTime()
    {
        return Game.Map.MapData.DeathTimes.ElementAtOrDefault(Experience.Level - 1);
    }

    public void ForceDead()
    {
        Stats.IsZombie = false;
        _lastDeathData.BecomeZombie = false;
        DieInternal();
        NPC_ForceDeadNotify(_lastDeathData);
    }

    public override void Die(DeathData data)
    {
        //hack 
        SetStatus(StatusFlags.Ghosted, true);
        SetStatus(StatusFlags.GhostProof, true);

        if (Stats.IsZombie)
        {
            ForceDead();
            return;
        }

        ApiEventManager.OnDeath.Publish(data.Unit, data);
        Stats.RespawnTimer = (data.DeathDuration = GetRespawnTime()) * 1000.0f;
        ChampionStatistics.Deaths++;
        Stats.IsZombie = data.BecomeZombie;
        ShopForceEnabled = true;

        if (data.BecomeZombie)
        {
            ApiEventManager.OnZombie.Publish(this, data);
        }

        //TODO: Check this
        if (data.Killer is not Champion)
        {
            Champion? cKiller = EnemyAssistMarkers.Find(x => x.Source is Champion)?.Source as Champion;
            if (cKiller is not null)
            {
                data.Killer = cKiller;
            }
        }

        if (data.Killer is Champion c)
        {
            ChampionDeathManager.ProcessKill(data);
            c.OnKill(data);
            //Publish OnChampionKill (?)
        }
        NPC_Hero_DieNotify(data);
        DieInternal();
        _lastDeathData = data;
    }

    private void DieInternal()
    {
        Stats.IsDead = true;
        Game.ObjectManager.StopTargeting(this);
        SetDashingState(false, MoveStopReason.Death);

        Buffs.RemoveNotLastingThroughDeath();
        EventHistory.Clear();
    }

    public T CreateEventForHistory<T>(AttackableUnit source, IEventSource sourceScript) where T : ArgsForClient, new()
    {

        if (source is null || sourceScript is null)
        {

            return null;
        }

        T e = new()
        {
            ParentCasterNetID = source.NetId,
            OtherNetID = NetId,
            ScriptNameHash = (sourceScript as Spell).IsAutoAttack ? 0 : sourceScript.ScriptNameHash,
            ParentScriptNameHash = (sourceScript as Spell).IsAutoAttack ? 0xFFFFFFFF : sourceScript.ScriptNameHash,
            EventSource = (uint)((sourceScript as Spell).IsAutoAttack ? EventSource.BASICATTACK : EventSource.SPELL), // ?
            ParentTeam = (uint)source.Team,
            SourceSpellLevel = 0,
            ParentSourceType = 0,
        };

        if (sourceScript.ParentScript != null)
        {
            e.ScriptNameHash = sourceScript.ScriptNameHash;
            e.ParentScriptNameHash = sourceScript.ParentScript.ScriptNameHash;
        }
        else if (sourceScript is Buff b && b.OriginSpell != null)
        {
            e.ScriptNameHash = sourceScript.ScriptNameHash;
            e.ParentScriptNameHash = HashString(b.OriginSpell.Name);
        }

        EventHistoryEntry entry = new()
        {
            Timestamp = Game.Time.GameTime / 1000f, // ?
            Count = 1, //TODO: stack?
            Source = source.NetId,
            Event = (IEvent)e
        };

        EventHistory.Add(entry);
        return e;

        //  return null;
    }



    public void TintScreen(bool enable, float speed, ChildrenOfTheGraveEnumNetwork.Content.Color color)
    {
        TintPlayerNotify(this, enable, speed, color);
    }

    internal override void TakeHeal(HealData healData)
    {
        base.TakeHeal(healData);

        var e = CreateEventForHistory<OnCastHeal>(healData.SourceUnit, healData.SourceScript);
        if (e != null)
        {
            e.Amount = healData.HealAmount;
        }

        if (healData.SourceUnit is Champion ch)
        {
            ch.ChampionStatistics.TotalHeal += (int)healData.HealAmount;
        }
    }

    public override void TakeDamage(DamageData damageData, IEventSource? sourceScript = null)
    {
        base.TakeDamage(damageData, sourceScript);


        if (damageData.Damage <= 0)
        {
            return;
        }

        Champion cAttacker = damageData.Attacker as Champion;
        var e = CreateEventForHistory<OnDamageGiven>(damageData.Attacker, sourceScript);

        switch (damageData.DamageType)
        {
            case DamageType.DAMAGE_TYPE_PHYSICAL:
                ChampionStatistics.PhysicalDamageTaken += damageData.Damage;
                break;
            case DamageType.DAMAGE_TYPE_MAGICAL:
                ChampionStatistics.MagicDamageTaken += damageData.Damage;
                break;
            case DamageType.DAMAGE_TYPE_TRUE:
                ChampionStatistics.TrueDamageTaken += damageData.Damage;
                break;
                //TODO: handle mixed damage?
        }

        if (cAttacker is not null)
        {
            switch (damageData.DamageType)
            {
                case DamageType.DAMAGE_TYPE_PHYSICAL:
                    ChampionStatistics.PhysicalDamageTaken += damageData.Damage;
                    cAttacker.ChampionStatistics.PhysicalDamageDealtToChampions += damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_MAGICAL:
                    ChampionStatistics.MagicDamageTaken += damageData.Damage;
                    cAttacker.ChampionStatistics.MagicDamageDealtToChampions += damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_TRUE:
                    ChampionStatistics.TrueDamageTaken += damageData.Damage;
                    cAttacker.ChampionStatistics.TrueDamageDealtToChampions += damageData.Damage;
                    break;
                    //TODO: handle mixed damage?
            }

            cAttacker.ChampionStatistics.TotalDamageDealtToChampions += damageData.Damage;
        }


        if (e is not null)
        {
            switch (damageData.DamageType)
            {
                case DamageType.DAMAGE_TYPE_PHYSICAL:
                    e.PhysicalDamage = damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_MAGICAL:
                    e.MagicalDamage = damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_TRUE:
                    e.TrueDamage = damageData.Damage;
                    break;
                    //TODO: handle mixed damage?
            }
        }

    }

    public void UpdateSkin(int skinNo)
    {
        SkinID = skinNo;
    }

    public void IncrementScore(float points, ScoreCategory scoreCategory, ScoreEvent scoreEvent, bool doCallOut, bool notifyText = true)
    {
        ChampionStats.Score += points;
        var scoreData = new ScoreData(this, points, scoreCategory, scoreEvent, doCallOut);
        S2C_IncrementPlayerScoreNotify(scoreData);

        if (notifyText)
        {
            //TODO: params is the number of point lost ? 
            DisplayFloatingTextNotify(new FloatingTextData(this, $"+{(int)points} Points", FloatTextType.Score, 1073741833), Team);
        }

        ApiEventManager.OnIncrementChampionScore.Publish(scoreData.Owner, scoreData);
    }

    internal override float GetAttackRatioWhenAttackingTurret()
    {
        return GlobalData.DamageRatios.HeroToBuilding;
    }
    internal override float GetAttackRatioWhenAttackingMinion()
    {
        return GlobalData.DamageRatios.HeroToUnit;
    }
    internal override float GetAttackRatioWhenAttackingChampion()
    {
        return GlobalData.DamageRatios.HeroToHero;
    }
    internal override float GetAttackRatioWhenAttackingBuilding()
    {
        return GlobalData.DamageRatios.HeroToBuilding;
    }
    internal override float GetAttackRatio(AttackableUnit attackingUnit)
    {
        return attackingUnit.GetAttackRatioWhenAttackingChampion();
    }

    public static int GetAverageChampionLevel()
    {
        float average = 0;
        var players = Game.PlayerManager.GetPlayers(true).ToArray()
            ;
        foreach (var player in players)
        {
            average += player.Champion.Experience.Level / players.Length;
        }
        return (int)Math.Floor(average);
    }

    protected override void OnUpdateStats()
    {
        base.OnUpdateStats();
        foreach (var talent in TalentInventory.Talents)
        {
            talent.Value.ScriptInternal.OnUpdateStats();
        }
    }

    #region AITask Management

    /// <summary>
    /// Assigne une tâche à ce champion
    /// </summary>
    public void AssignTask(AITask task)
    {
        if (task == null) return;

        AssignedTasks.Add(task);

        // Si c'est la première tâche, la définir comme tâche courante
        if (CurrentTask == null)
        {
            CurrentTask = task;
        }
    }

    /// <summary>
    /// Désassigne une tâche spécifique
    /// </summary>
    public void UnassignTask(AITask task)
    {
        if (task == null) return;

        AssignedTasks.Remove(task);

        // Si c'était la tâche courante, passer à la suivante
        if (CurrentTask == task)
        {
            CurrentTask = AssignedTasks.Count > 0 ? AssignedTasks[0] : null;
        }
    }

    /// <summary>
    /// Désassigne toutes les tâches
    /// </summary>
    public void UnassignAllTasks()
    {
        AssignedTasks.Clear();
        CurrentTask = null;
    }

    /// <summary>
    /// Vérifie si le champion a une tâche spécifique
    /// </summary>
    public bool HasTask(AITaskTopicType topic, AttackableUnit targetUnit = null, Vector3 targetLocation = default)
    {
        return AssignedTasks.Any(task =>
            task.Topic == topic &&
            (targetUnit == null || task.TargetUnit == targetUnit) &&
            (targetLocation == default || task.TargetLocation == targetLocation));
    }

    /// <summary>
    /// Vérifie si le champion a des tâches assignées
    /// </summary>
    public bool HasAnyTask()
    {
        return AssignedTasks.Count > 0;
    }

    /// <summary>
    /// Récupère la première tâche correspondant aux critères
    /// </summary>
    public AITask GetTask(AITaskTopicType topic, AttackableUnit targetUnit = null, Vector3 targetLocation = default)
    {
        return AssignedTasks.FirstOrDefault(task =>
            task.Topic == topic &&
            (targetUnit == null || task.TargetUnit == targetUnit) &&
            (targetLocation == default || task.TargetLocation == targetLocation));
    }


    #endregion

}