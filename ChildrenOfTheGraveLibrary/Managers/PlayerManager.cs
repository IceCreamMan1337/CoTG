using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.NetInfo;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer
{
    public class PlayerManager
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        private List<ClientInfo> _players = new();
        public Dictionary<TeamId, int> PlayerTeamCount { get; set; } = new();
        public int ClientIdIndex { get; set; } = 0;

        public void AddPlayers(List<PlayerConfig> players)
        {
            foreach (var p in players)
            {
                _logger.Info($"Player: {p.Name} added as {p.Champion}");
                AddPlayer(p);
            }
        }
        // Method to check if all players are ready
        public bool AreAllPlayersReady()
        {
            // By default, assume all players are not ready


            bool allPlayersReady = true;
            foreach (var player in _players)
            {
                if (player.PlayerId <= -1)
                {
                    player.IsReady = true;
                }

            }

            // Iterate through all players
            foreach (var player in _players)
            {
                // If a player is not ready, set allPlayersReady to false and exit the loop
                if (!player.IsReady)
                {
                    allPlayersReady = false;
                    break;
                }
            }
            // Return true if all players are ready, otherwise return false
            return allPlayersReady;
        }
        public void AddPlayer(PlayerConfig config)
        {
            if (!PlayerTeamCount.TryAdd(config.Team, 1))
            {
                PlayerTeamCount[config.Team]++;
            }

            var summonerSkills = new[]
            {
                config.Summoner1,
                config.Summoner2
            };
            var teamId = config.Team;
            var info = new ClientInfo(
                config.Rank,
                teamId,
                config.Ribbon,
                config.Icon,
                config.Skin,
                config.Name,
                summonerSkills,
                config.PlayerID,
                config.Runes,
                config.Talents
            );

            info.ClientId = _players.Count;

            var c = new Champion(
                config.Champion,
                info,
                0,
                teamId,
                difficultyai: config.AIDifficulty,
                useDoomSpells: config.UseDoomSpells
            );

            info.Champion = c;
            var pos = GetHeroInitialSpawnPosition(info);
            c.SetPosition(pos, false);
            c.StopMovement();
            c.UpdateMoveOrder(OrderType.Stop);
            _players.Add(info);

            Game.ObjectManager.AddObject(c);
        }

        public void AddPlayer(ClientInfo info)
        {
            info.ClientId = _players.Count;//- 1 ;
            _players.Add(info);
        }

        // GetPlayerFromPeer
        public ClientInfo GetPeerInfo(int clientId)
        {
            if (0 <= clientId && clientId < _players.Count)
            {
                return _players[clientId];
            }
            return null;
        }

        public ClientInfo? GetClientInfoByPlayerId(long playerId)
        {
            return _players.Find(c => c.PlayerId == playerId);
        }

        public ClientInfo? GetClientInfoByChampion(Champion champ)
        {
            return _players.Find(c => c.Champion == champ);
        }

        public IEnumerable<ClientInfo> GetPlayers(bool includeBots = true)
        {
            foreach (var p in _players)
            {
                if (p.Champion.IsBot && !includeBots)
                {
                    continue; // Skip bots if includeBots is false
                }
                yield return p;
            }
        }

        public IEnumerable<ClientInfo> GetBots()
        {
            foreach (var p in _players)
            {
                if (p.Champion.IsBot)
                {
                    yield return p;
                }
            }
        }

        public IEnumerable<AttackableUnit> GetBotsFromTeam(TeamId team)
        {
            foreach (ClientInfo c in _players)
            {
                if (c.Champion.IsBot && c.Champion.Team == team)
                {
                    yield return c.Champion;
                }
            }
        }

        private Vector2 GetHeroInitialSpawnPosition(ClientInfo player)
        {
            int playerCount = PlayerTeamCount[player.Team];
            int playerIndex = player.InitialSpawnIndex;

            Vector2 pos = Game.Map.NavigationGrid.MiddleOfMap;
            if (Game.Map.SpawnPoints.TryGetValue(player.Team, out var spawn))
            {
                pos = spawn.Position;
            }

            if (!ContentManager.HeroSpawnOffset.TryGetValue(player.Team, out List<List<SpawnOffsetInfo>>? teamList))
            {
                return pos;
            }

            if (playerIndex < teamList.Count)
            {
                if (playerCount < teamList.Count() && playerIndex < teamList[playerCount].Count() && teamList[playerCount][playerIndex] != null)
                {
                    return pos + new Vector2(teamList[playerCount][playerIndex].PositionOffset.X, teamList[playerCount][playerIndex].PositionOffset.Z);
                }
                else
                {
                    return pos;
                }
            }

            return pos;
        }

        public int GetTeamSize(TeamId team)
        {
            return PlayerTeamCount[team];
        }

        public bool CheckIfAllPlayersLeft()
        {
            var players = GetPlayers(false).ToArray(); //Store player count somewhere so we don't have to call it multiple times
            // The number of those who are disconnected and not even loads.
            var count = players.Count(p => !p.IsStartedClient && p.IsDisconnected);
            _logger.Info($"The number of disconnected players {count}/{players.Length}");
            if (count == players.Length)  //-1 test only 
            {
                _logger.Info("All players have left the server. It's lonely here :(");

                // Check if server should keep alive when empty
                if (!Game.Config.KeepAliveWhenEmpty)
                {
                    _logger.Info("Server configured to shutdown when empty. Shutting down...");
                    Game.isUploadingfinished = true;
                    Game.StateHandler.SetGameState(GameState.EXIT);
                    return true;
                }
                else
                {
                    _logger.Info("Server configured to stay alive when empty. Continuing...");
                    return false;
                }
            }
            return false;
        }
    }
}

