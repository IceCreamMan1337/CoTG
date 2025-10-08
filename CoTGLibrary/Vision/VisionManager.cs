using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CoTG.CoTGServer;
using CoTGEnumNetwork.Enums;
using CoTGEnumNetwork.NetInfo;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.Logging;
using log4net;

namespace CoTGLibrary.Vision
{
    public class VisionManager
    {
        public static VisionManager Instance { get; private set; }
        private static readonly ILog _logger = LoggerProvider.GetLogger();
        private readonly Dictionary<TeamId, HashSet<GameObject>> _visionProviders = new();
        private static readonly TeamId[] AllTeams = { TeamId.TEAM_ORDER, TeamId.TEAM_CHAOS, TeamId.TEAM_NEUTRAL };
        public List<TeamId> Teams { get; }
        private readonly object _visionLock = new();
        public bool DebugVision { get; set; } = false;

        public VisionManager()
        {
            Instance = this;
            Teams = AllTeams.ToList();
            foreach (var team in Teams)
            {
                _visionProviders.Add(team, new HashSet<GameObject>());
            }
        }

        /// <summary>
        /// Adds a vision provider for a team. Only unique providers are stored.
        /// </summary>
        public void AddVisionProvider(GameObject obj, TeamId team)
        {
            // Skip TEAM_UNKNOWN as it shouldn't have vision providers
            if (team == TeamId.TEAM_UNKNOWN)
            {
                return;
            }

            if (_visionProviders.TryGetValue(team, out var set))
            {
                lock (_visionLock)
                {
                    set.Add(obj);
                }
            }
            else
            {
                _logger.Warn($"Attempted to add vision provider for unknown team: {team}");
            }
        }

        /// <summary>
        /// Removes a vision provider for a team.
        /// </summary>
        public void RemoveVisionProvider(GameObject obj, TeamId team)
        {
            // Skip TEAM_UNKNOWN as it shouldn't have vision providers
            if (team == TeamId.TEAM_UNKNOWN)
            {
                return;
            }

            if (_visionProviders.TryGetValue(team, out var set))
            {
                lock (_visionLock)
                {
                    set.Remove(obj);
                }
            }
            else
            {
                _logger.Warn($"Attempted to remove vision provider for unknown team: {team}");
            }
        }

        /// <summary>
        /// Updates vision for all teams on a GameObject.
        /// </summary>
        public void UpdateTeamsVision(GameObject obj)
        {
            foreach (var team in Teams)
            {
                obj.SetVisibleByTeam(team, !obj.IsAffectedByFoW || TeamHasVisionOn(team, obj));
            }
        }

        /// <summary>
        /// Updates vision for a player (used for spawn/sync).
        /// </summary>
        public void UpdateVisionSpawnAndSync(GameObject obj, ClientInfo clientInfo, bool forceSpawn = false)
        {
            int cid = clientInfo.ClientId;
            TeamId team = clientInfo.Team;
            Champion champion = clientInfo.Champion;
            bool nearSighted = (champion.Status & StatusFlags.NearSighted) != 0;
            bool shouldBeVisibleForPlayer;
            if (obj is Particle p && p.VisibilityOwner != null)
            {
                shouldBeVisibleForPlayer = p.VisibilityOwner == champion;
            }
            else
            {
                shouldBeVisibleForPlayer = !obj.IsAffectedByFoW || (
                    nearSighted ? UnitHasVisionOn(champion, obj, nearSighted) : obj.IsVisibleByTeam(champion.Team)
                );
            }
            obj.Sync(cid, team, shouldBeVisibleForPlayer, forceSpawn);
        }

        /// <summary>
        /// Checks if a team has vision on a GameObject, with all special rules.
        /// </summary>
        public bool TeamHasVisionOn(TeamId team, GameObject o)
        {
            if (o is null)
                return false;
            if (!o.IsAffectedByFoW)
                return true;
            var set = _visionProviders[team];
            lock (_visionLock)
            {
                foreach (var p in set)
                {
                    // Prevent enemy turrets from providing vision for your team
                    if ((p is BaseTurret || p is Region) && p.Team != team)
                        continue;
                    // Prevent lane minions from providing vision for your team if not on your team
                    if (p is Minion laneMinion && laneMinion.Team != team && laneMinion.Team != TeamId.TEAM_NEUTRAL)
                        continue;
                    if (UnitHasVisionOn(p, o))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if an observer has vision on a tested object, with all special rules.
        /// </summary>
        public static bool UnitHasVisionOn(GameObject observer, GameObject tested, bool nearSighted = false)
        {


            if (!tested.IsAffectedByFoW)
                return true;

            if (tested is AttackableUnit tunit2
            && (tunit2.Status & StatusFlags.RevealSpecificUnit) != 0)
                return true;



            if (tested is AttackableUnit tunit
                && (tunit.Status & StatusFlags.Stealthed) != 0
                && (tunit.Status & StatusFlags.RevealSpecificUnit) == 0
                && tunit.Team != observer.Team)
            {
                return false;
            }
            if (observer is Region region)
            {
                if (region.VisionTarget != null && region.VisionTarget != tested)
                    return false;
            }
            else if (tested.Team == observer.Team && !nearSighted)
            {
                return true;
            }
            if (
                !(observer is AttackableUnit u && u.Stats.IsDead)
                && Vector2.DistanceSquared(observer.Position, tested.Position) < observer.VisionRadius * observer.VisionRadius
                && !Game.Map.NavigationGrid.IsAnythingBetween(observer, tested, true)
            )
            {
                return true;
            }
            return false;
        }

        // for future extensibility
        public void OnObjectMoved(GameObject obj)
        {
            // In a real system, update only affected vision here
            if (DebugVision) _logger.Debug($"[Vision] Object moved: {obj.Name}");
            UpdateTeamsVision(obj);
        }
        public void OnObjectSpawned(GameObject obj)
        {
            if (DebugVision) _logger.Debug($"[Vision] Object spawned: {obj.Name}");
            UpdateTeamsVision(obj);
        }
        public void OnObjectDied(GameObject obj)
        {
            if (DebugVision) _logger.Debug($"[Vision] Object died: {obj.Name}");
            UpdateTeamsVision(obj);
        }
    }
}
