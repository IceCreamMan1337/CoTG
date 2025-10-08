using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.NetInfo;
using ChildrenOfTheGraveLibrary.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using ChildrenOfTheGraveLibrary.Vision;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using log4net;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer
{
    // TODO: refactor this class

    /// <summary>
    /// Class that manages addition, removal, and updating of all GameObjects, their visibility, and buffs.
    /// </summary>
    public class ObjectManager
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        private readonly List<GameObject> _objectsToAdd = new();
        private readonly List<GameObject> _objectsToRemove = new();
        private readonly SortedDictionary<uint, GameObject> _objects = new();
        private readonly SortedDictionary<uint, Champion> _champions = new();
        private readonly SortedDictionary<uint, BaseTurret> _turrets = new();
        private readonly SortedDictionary<uint, Inhibitor> _inhibitors = new();
        private readonly object _objectsLock = new();

        private bool _currentlyInUpdate = false;

        // AIManager to handle AI tasks
        public Dictionary<TeamId, BehaviourTree> _aiManagers = new();
        private Dictionary<TeamId, bool> _aiManagersInitialized = new();

        // Specialized manager based on game mode
        private BehaviourTree _specializedManager;
        private bool _specializedManagerInitialized = false;

        // Throttling system for AIManager
        private float _lastAIManagerUpdateTime = 0f;
        private float _aiManagerUpdateInterval = 0.5f; // 0.5 second by default (2 FPS) - optimized for performance

        /// <summary>
        /// List of all possible teams in League of Legends. Normally there are only three.
        /// </summary>
        public List<TeamId> Teams { get; private set; }

        /// <summary>
        /// Instantiates all GameObject Dictionaries in ObjectManager.
        /// </summary>
        /// <param name="game">Game instance.</param>
        private static readonly TeamId[] AllTeams = { TeamId.TEAM_ORDER, TeamId.TEAM_CHAOS, TeamId.TEAM_NEUTRAL };

        public ObjectManager()
        {
            Teams = new List<TeamId>(AllTeams);
        }

        private void UpdateStats()
        {
            foreach (var obj in _objects.Values)
            {
                obj.UpdateStats();
            }
        }

        private void UpdateActions()
        {
            foreach (var obj in _objects.Values)
            {
                try
                {
                    obj.Update();
                }
                catch (Exception e)
                {
                    _logger.Error(null, e);
                }
            }
        }

        /// <summary>
        /// Updates the AIManager to handle AI tasks
        /// </summary>
        private void UpdateAIManager()
        {


            float currentTime = Game.Time.GameTime;

            if (currentTime - _lastAIManagerUpdateTime < _aiManagerUpdateInterval)
            {
                return; // Not time to update yet
            }

            _lastAIManagerUpdateTime = currentTime;

            if (_aiManagersInitialized.Count == 0 || !_aiManagersInitialized.All(kv => kv.Value))
            {

                InitializeAIManager();


                return;
            }

            // 1. Update the base AIManager (always)
            foreach (var kv in _aiManagers)
            {
                if (kv.Value != null)
                {

                    // Call AIManager_Logic() directly instead of Update()
                    var method = kv.Value.GetType().GetMethod("AIManager_Logic");
                    if (method != null)
                    {
                        var result = method.Invoke(kv.Value, null);
                    }




                }

            }

            // 2. Update the specialized manager (if initialized)
            if (_specializedManagerInitialized && _specializedManager != null)
            {

                // Call the appropriate method based on the manager type
                var method = _specializedManager.GetType().GetMethod("Update");
                if (method != null)
                {
                    var result = method.Invoke(_specializedManager, null);
                }
            }

            UpdateSquadMission();
        }

        private void UpdateSquadMission()
        {
            foreach (var squad in Game.Map.MapData.aisquadlistmanager.GetAllSquads())
            {
                if (squad._aisquad != null)
                {
                    // ✅ Utiliser le nouveau système de timing des squads
                    squad._aisquad.ExecuteSquadBehaviourTrees();
                }
            }
        }


        /// <summary>
        /// Configures the AIManager update interval
        /// </summary>
        /// <param name="interval">Interval in seconds (minimum 0.1s)</param>
        public void SetAIManagerUpdateInterval(float interval)
        {
            _aiManagerUpdateInterval = Math.Max(0.1f, interval);
        }

        /// <summary>
        /// Initializes the AIManager to handle AI tasks
        /// </summary>
        public void InitializeAIManager()
        {
            try
            {
                var bots = Game.PlayerManager.GetBots();
                string gameMode = GetCurrentGameMode();
                int mapId = Game.Map.MapData.Id;
                if (gameMode == "CLASSIC" && mapId == 1)
                {


                    //gametype not implemented correctly 
                    string gameType = "MATCHED_GAME";


                    int botCount = bots.Count();
                    if (botCount > 0)
                    {

                        // gameType = "CUSTOM_GAME";
                        /*  else
                              {
                        todo : tutorial 

                              }
                          */
                    }

                    foreach (var team in Teams)
                    {
                        string teamStr = team.ToString();
                        string managerKey = $"{gameMode}_{gameType}_{teamStr}";

                        if (ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.ContentManager.AiManagerData.TryGetValue(managerKey, out string managerTag))
                        {
                            _logger.Info($"[AI_SETUP] Initialisation du manager {managerTag} pour {managerKey}");
                            switch (managerTag)
                            {
                                case "Manager":
                                    _aiManagers[team] = Game.ScriptEngine.CreateObject<BehaviourTree>("BehaviourTrees", "AIManager_LogicClass");
                                    _aiManagersInitialized[team] = true;
                                    _aiManagers[team].aimanagerTeam = team;
                                    break;
                                case "CustomGameManager":
                                    _aiManagers[team] = Game.ScriptEngine.CreateObject<BehaviourTree>("BehaviourTrees", "CustomGameManager_LogicClass");
                                    _aiManagersInitialized[team] = true;
                                    _aiManagers[team].aimanagerTeam = team;
                                    break;
                                case "TutorialManager":
                                    _aiManagers[team] = Game.ScriptEngine.CreateObject<BehaviourTree>("BehaviourTrees", "TutorialManager_LogicClass");
                                    _aiManagersInitialized[team] = true;
                                    _aiManagers[team].aimanagerTeam = team;
                                    break;
                                default:
                                    _logger.Warn($"[AI_SETUP] Tag de manager inconnu: {managerTag}");
                                    break;
                            }
                        }
                        else
                        {
                            // Special handling for TEAM_NEUTRAL (always present)
                            if (team == TeamId.TEAM_NEUTRAL)
                            {
                                _logger.Info($"[AI_SETUP] TEAM_NEUTRAL - Utilisation du manager par défaut");
                                _aiManagers[team] = Game.ScriptEngine.CreateObject<BehaviourTree>("BehaviourTrees", "AIManager_LogicClass");
                                _aiManagersInitialized[team] = true;
                                _aiManagers[team].aimanagerTeam = team;
                            }
                            else
                            {
                                _logger.Warn($"[AI_SETUP] Aucun manager trouvé pour la clé: {managerKey}");
                            }
                        }

                        if (_aiManagers.ContainsKey(team) && _aiManagers[team] != null)
                        {
                            // Associate all bots from the team to the AIManager
                            var teamBots = Game.PlayerManager.GetBotsFromTeam(team).ToList();
                            _aiManagers[team].AIEntitiesAssociated = teamBots;
                        }
                        _logger.Info($"AIManager initialisé dynamiquement via .dat - Map: {mapId}, Mode: {gameMode}, Type: {gameType}, Bots: {botCount}");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Erreur lors de l'initialisation de l'AIManager: {e.Message}", e);
            }
        }

        /// <summary>
        /// Gets the current game mode
        /// </summary>
        private string GetCurrentGameMode()
        {
            return Game.Config.GameConfig.GameMode;
        }


        /// <summary>
        /// Function called every tick of the game.
        /// </summary>
        /// <param name="diff">Number of milliseconds since this tick occurred.</param>
        internal void Update()
        {

            _currentlyInUpdate = true;

            UpdateStats();
            UpdateActions();


            // It is now safe to call RemoveObject at any time,
            // but compatibility with the older remove method remains.
            foreach (var obj in _objects.Values)
            {
                if (obj.IsToRemove())
                {
                    obj.RemoveAllVisibilityLinkForEntity();


                    RemoveObject(obj);
                }
            }

            foreach (var obj in _objectsToRemove)
            {
                _objects.Remove(obj.NetId);
            }
            _objectsToRemove.Clear();

            int oldObjectsCount = _objects.Count;

            foreach (var obj in _objectsToAdd)
            {
                _objects.Add(obj.NetId, obj);
            }
            _objectsToAdd.Clear();

            var players = Game.PlayerManager.GetPlayers(includeBots: false);

            int i = 0;
            foreach (GameObject obj in _objects.Values)
            {
                VisionManager.Instance.UpdateTeamsVision(obj);
                if (i++ < oldObjectsCount)
                {
                    // Don't call on newly created objects
                    // because Update has not yet been called on them
                    obj.LateUpdate();
                }

                foreach (var kv in players)
                {
                    VisionManager.Instance.UpdateVisionSpawnAndSync(obj, kv);
                }

                obj.OnAfterSync();
            }

            OnReplicationNotify();

            WaypointGroupNotify();

            if (Game.Map.Id == 1)
            {
                UpdateAIManager();
            }



            _currentlyInUpdate = false;
        }

        /// <summary>
        /// Normally, objects will spawn at the end of the frame, but calling this function will force the teams' and players' vision of that object to update and send out a spawn notification.
        /// </summary>
        /// <param name="obj">Object to spawn.</param>
        public void SpawnObject(GameObject obj)
        {
            VisionManager.Instance.UpdateTeamsVision(obj);

            var players = Game.PlayerManager.GetPlayers(includeBots: false);
            foreach (var kv in players)
            {
                VisionManager.Instance.UpdateVisionSpawnAndSync(obj, kv, forceSpawn: true);
            }

            obj.OnAfterSync();
        }

        // Called in response to a SpawnRequest
        public void OnReconnect(int userId, TeamId team, bool hard)
        {
            foreach (GameObject obj in _objects.Values)
            {
                obj.OnReconnect(userId, team, hard);
            }
        }

        public void SpawnObjects(ClientInfo clientInfo)
        {
            foreach (GameObject obj in _objects.Values)
            {
                VisionManager.Instance.UpdateVisionSpawnAndSync(obj, clientInfo, forceSpawn: true);
            }
        }

        /// <summary>
        /// Adds a GameObject to the dictionary of GameObjects in ObjectManager.
        /// </summary>
        /// <param name="o">GameObject to add.</param>
        public void AddObject(GameObject o)
        {
            if (o != null)
            {
                _objectsToRemove.Remove(o);

                if (_currentlyInUpdate)
                {
                    _objectsToAdd.Add(o);
                }
                else
                {
                    if (!_objects.TryAdd(o.NetId, o))
                    {
                        _logger.Error($"Can't add object \"{o.Name}\"(ID: {o.NetId}) to ObjectManager!");
                    }
                }
                o.OnAdded();
            }
        }

        /// <summary>
        /// Removes a GameObject from the dictionary of GameObjects in ObjectManager.
        /// </summary>
        /// <param name="o">GameObject to remove.</param>
        public void RemoveObject(GameObject o)
        {
            if (o != null)
            {
                _objectsToAdd.Remove(o);

                if (_currentlyInUpdate)
                {
                    _objectsToRemove.Add(o);
                }
                else
                {
                    _objects.Remove(o.NetId);
                }
                o.OnRemoved();
            }
        }

        /// <summary>
        /// Gets a new Dictionary of all NetID,GameObject pairs present in the dictionary of objects in ObjectManager.
        /// </summary>
        /// <returns>Dictionary of NetIDs and the GameObjects that they refer to.</returns>
        public Dictionary<uint, GameObject> GetObjects()
        {
            var ret = new Dictionary<uint, GameObject>();
            // Create a safe copy to avoid concurrent modification during iteration
            foreach (var obj in _objects)
            {
                ret.Add(obj.Key, obj.Value);
            }

            return ret;
        }

        /// <summary>
        /// Gets a GameObject from the list of objects in ObjectManager that is identified by the specified NetID.
        /// </summary>
        /// <param name="id">NetID to check.</param>
        /// <returns>GameObject instance that has the specified NetID. Null otherwise.</returns>
        public GameObject? GetObjectById(uint id)
        {
            return _objects.GetValueOrDefault(id, null) ?? _objectsToAdd.Find(o => o.NetId == id);
        }

        /// <summary>
        /// Gets a new list of all AttackableUnit who have aisquad who are used in aimanagerdominion in ObjectManager.
        /// </summary>
        /// <param name="checkPos">Vector2 position to check.</param>
        /// <param name="range">Distance to check.</param>
        /// <param name="onlyAlive">Whether dead units should be excluded or not.</param>
        /// <returns>List of all AttackableUnits within the specified range and of the specified alive status.</returns>
        public List<AttackableUnit> GetUnitsInRange(Vector2 checkPos, float range, bool onlyAlive = false)
        {
            var units = new List<AttackableUnit>();
            foreach (var kv in _objects)
            {
                if (kv.Value is AttackableUnit u && Vector2.DistanceSquared(checkPos, u.Position) <= range * range && ((onlyAlive && !u.Stats.IsDead) || !onlyAlive))
                {
                    units.Add(u);
                }
            }

            return units;
        }

        /// <summary>
        /// Counts the number of units attacking a specified GameObject of type AttackableUnit.
        /// </summary>
        /// <param name="target">AttackableUnit potentially being attacked.</param>
        /// <returns>Number of units attacking target.</returns>
        public int CountUnitsAttackingUnit(AttackableUnit target)
        {
            int count = 0;
            foreach (var kvp in _objects)
            {
                if (kvp.Value is ObjAIBase aiBase &&
                    aiBase.Team == target.Team.GetEnemyTeam() &&
                    !aiBase.Stats.IsDead &&
                    aiBase.TargetUnit != null &&
                    aiBase.TargetUnit == target)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Forces all GameObjects of type ObjAIBase to stop targeting the specified AttackableUnit.
        /// </summary>
        /// <param name="target">AttackableUnit that should be untargeted.</param>
        public void StopTargeting(AttackableUnit target)
        {
            foreach (var kv in _objects)
            {
                if (kv.Value is ObjAIBase ai)
                {
                    ai.Untarget(target);
                }
            }
        }

        /// <summary>
        /// Adds a GameObject of type Champion to the list of Champions in ObjectManager.
        /// </summary>
        /// <param name="champion">Champion to add.</param>
        public void AddChampion(Champion champion)
        {
            _champions.Add(champion.NetId, champion);
        }

        /// <summary>
        /// Removes a GameObject of type Champion from the list of Champions in ObjectManager.
        /// </summary>
        /// <param name="champion">Champion to remove.</param>
        public void RemoveChampion(Champion champion)
        {
            _champions.Remove(champion.NetId);
        }

        /// <summary>
        /// Gets a new list of all Champions found in the list of Champions in ObjectManager.
        /// </summary>
        /// <returns>List of all valid Champions.</returns>
        public List<Champion> GetAllChampions()
        {
            var champs = new List<Champion>();
            foreach (var kv in _champions)
            {
                var c = kv.Value;
                if (c != null)
                {
                    champs.Add(c);
                }
            }

            return champs;
        }

        /// <summary>
        /// Gets a new list of all AttackableUnit who have aisquad who are used in aimanagerdominion in ObjectManager.
        /// </summary>
        /// <returns>List of all valid Champions.</returns>
        public List<AttackableUnit> GetAllCapturePoint()
        {
            var AImanagerentities = new List<AttackableUnit>();
            foreach (var kv in _objects)
            {
                var c = kv.Value;
                if (c != null)
                {
                    if (c is AttackableUnit)
                    {
                        var cp = c as AttackableUnit;
                        if (cp.capturepointid != -1) //for me need check if
                        {
                            AImanagerentities.Add(cp);
                        }
                    }

                }
            }

            return AImanagerentities;
        }

        /// <summary>
        /// Gets a new list of all Champions of the specified team found in the list of Champios in ObjectManager.
        /// </summary>
        /// <param name="team">TeamId.BLUE/PURPLE/NEUTRAL</param>
        /// <returns>List of valid Champions of the specified team.</returns>
        public List<Champion> GetAllChampionsFromTeam(TeamId team)
        {
            if (team is TeamId.TEAM_UNKNOWN)
            {
                var allChamps = new List<Champion>(_champions.Count);
                foreach (var kvp in _champions)
                {
                    allChamps.Add(kvp.Value);
                }
                return allChamps;
            }

            var champs = new List<Champion>();
            foreach (var kv in _champions)
            {
                var c = kv.Value;
                if (c.Team == team)
                {
                    champs.Add(c);
                }
            }

            return champs;
        }

        /// <summary>
        /// Gets a new list of turret found in the list of Champions in ObjectManager.
        /// </summary>
        /// <returns>List of all valid Champions.</returns>
        public List<BaseTurret> GetAllTurrets()
        {
            var turrets = new List<BaseTurret>();
            foreach (var kv in _turrets)
            {
                var c = kv.Value;
                if (c != null)
                {
                    turrets.Add(c);
                }
            }

            return turrets;
        }

        /// <summary>
        /// Gets a new list of turret found in the list of Champions in ObjectManager.
        /// </summary>
        /// <returns>List of all valid Champions.</returns>
        public List<BaseTurret> GetAllTeamTurrets(TeamId team)
        {
            if (team is TeamId.TEAM_UNKNOWN)
            {
                var allTurrets = new List<BaseTurret>(_turrets.Count);
                foreach (var kvp in _turrets)
                {
                    allTurrets.Add(kvp.Value);
                }
                return allTurrets;
            }

            var turrets = new List<BaseTurret>();
            foreach (var kv in _turrets)
            {
                var c = kv.Value;
                if (c.Team == team)
                {
                    turrets.Add(c);
                }
            }

            return turrets;
        }

        /// <summary>
        /// Gets a list of all GameObjects of type Champion that are within a certain distance from a specified position.
        /// </summary>
        /// <param name="checkPos">Vector2 position to check.</param>
        /// <param name="range">Distance to check.</param>
        /// <param name="onlyAlive">Whether dead Champions should be excluded or not.</param>
        /// <returns>List of all Champions within the specified range of the position and of the specified alive status.</returns>
        public List<Champion> GetChampionsInRange(Vector2 checkPos, float range, bool onlyAlive = false)
        {
            var champs = new List<Champion>();
            foreach (var kv in _champions)
            {
                var c = kv.Value;
                if (Vector2.DistanceSquared(checkPos, c.Position) <= range * range)
                {
                    if ((onlyAlive && !c.Stats.IsDead) || !onlyAlive)
                    {
                        champs.Add(c);
                    }
                }
            }

            return champs;
        }

        /// <summary>
        /// Gets a list of all GameObjects of type Champion that are within a certain distance from a specified position.
        /// </summary>
        /// <param name="checkPos">Vector2 position to check.</param>
        /// <param name="range">Distance to check.</param>
        /// <param name="onlyAlive">Whether dead Champions should be excluded or not.</param>
        /// <returns>List of all Champions within the specified range of the position and of the specified alive status.</returns>
        public List<Champion> GetChampionsInRangeFromTeam(Vector2 checkPos, float range, TeamId team, bool onlyAlive = false)
        {
            var champs = new List<Champion>();
            foreach (var kv in _champions)
            {
                var c = kv.Value;
                if (Vector2.DistanceSquared(checkPos, c.Position) <= range * range)
                {
                    if (c.Team == team && ((onlyAlive && !c.Stats.IsDead) || !onlyAlive))
                    {
                        champs.Add(c);
                    }
                }
            }

            return champs;
        }

        public List<IExperienceOwner> GetExperienceOwnersInRangeFromTeam(Vector2 checkPos, float range, TeamId team, bool onlyAlive = false)
        {
            List<IExperienceOwner> experienceOwners = new();

            var champions = GetChampionsInRangeFromTeam(checkPos, range, team, onlyAlive);
            foreach (var champion in champions)
            {
                experienceOwners.Add(champion);
            }

            foreach (var kv in _objects)
            {
                if (kv.Value is IExperienceOwner expOwner && Vector2.DistanceSquared(checkPos, expOwner.Experience.Owner.Position) <= range * range)
                {
                    if (expOwner.Experience.Owner.Team == team && ((onlyAlive && !expOwner.Experience.Owner.Stats.IsDead) || !onlyAlive))
                    {
                        experienceOwners.Add(expOwner);
                    }
                }
            }

            return experienceOwners;
        }

        internal void LoadScripts()
        {
            _currentlyInUpdate = true;
            foreach (var unit in _objects.Values)
            {
                if (unit is ObjAIBase obj)
                {
                    obj.LoadCharScript(obj.Spells.Passive);
                    obj.Buffs.ReloadScripts();
                    obj.ReloadSpellsScripts();
                }
            }
            _currentlyInUpdate = false;
        }
    }
}

