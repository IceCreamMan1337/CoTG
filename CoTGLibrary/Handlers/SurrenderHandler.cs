using System;
using System.Collections.Generic;
using System.Linq;
using CoTGEnumNetwork.Domain;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using CoTG.CoTGServer.Logging;
using log4net;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.Handlers
{
    // TODO: Make the surrender UI button become clickable upon hitting SurrenderMinimumTime
    public class SurrenderHandler : IUpdate
    {
        private Dictionary<Champion, bool> _votes = new();
        private static ILog _logger = LoggerProvider.GetLogger();
        private bool toEnd = false;
        private float toEndTimer = 3000.0f;

        public float SurrenderMinimumTime { get; set; }
        public float SurrenderRestTime { get; set; }
        public float SurrenderLength { get; set; }
        public float LastSurrenderTime { get; set; }
        public bool IsSurrenderActive { get; set; }
        public TeamId Team { get; set; }

        // TODO: The first two parameters are in milliseconds, the third is seconds. QoL fix this?
        public SurrenderHandler(TeamId team, float minTime, float restTime, float length)
        {
            Team = team;
            SurrenderMinimumTime = minTime;
            SurrenderRestTime = restTime;
            SurrenderLength = length;
        }

        public Tuple<int, int> GetVoteCounts()
        {
            int yes = _votes.Count(kv => kv.Value == true);
            int no = _votes.Count - yes;
            return new Tuple<int, int>(yes, no);
        }

        public void HandleSurrender(int userId, Champion who, bool vote)
        {
            if (Game.Time.GameTime < SurrenderMinimumTime)
            {
                TeamSurrenderStatusNotify(userId, who.Team, who.Team, SurrenderReason.NotAllowedYet, 0, 0);
                return;
            }

            bool open = !IsSurrenderActive;
            if (!IsSurrenderActive && Game.Time.GameTime < LastSurrenderTime + SurrenderRestTime)
            {
                TeamSurrenderStatusNotify(userId, who.Team, who.Team, SurrenderReason.DontSpamSurrender, 0, 0);
                return;
            }
            IsSurrenderActive = true;
            LastSurrenderTime = Game.Time.GameTime;
            _votes.Clear();

            if (_votes.ContainsKey(who))
            {
                TeamSurrenderStatusNotify(userId, who.Team, who.Team, SurrenderReason.AlreadyVoted, 0, 0);
                return;
            }
            _votes[who] = vote;
            Tuple<int, int> voteCounts = GetVoteCounts();
            var players = Game.PlayerManager.GetPlayers(false).ToArray();
            int total = players.Length;

            _logger.Info($"Champion {who.Model} voted {vote}. Currently {voteCounts.Item1} yes votes, {voteCounts.Item2} no votes, with {total} total players");

            TeamSurrenderVoteNotify(who, open, vote, (byte)voteCounts.Item1, (byte)voteCounts.Item2, (byte)total, SurrenderLength);

            if (voteCounts.Item1 >= total - 1)
            {
                IsSurrenderActive = false;
                foreach (var p in players)
                {
                    TeamSurrenderStatusNotify(p.ClientId, p.Team, Team, SurrenderReason.SurrenderAgreed, (byte)voteCounts.Item1, (byte)voteCounts.Item2);
                }

                toEnd = true;
            }
        }

        public void Update()
        {
            if (IsSurrenderActive && Game.Time.GameTime >= LastSurrenderTime + (SurrenderLength * 1000.0f))
            {
                IsSurrenderActive = false;
                Tuple<int, int> count = GetVoteCounts();
                foreach (var p in Game.PlayerManager.GetPlayers(false))
                {
                    if (p.Team == Team)
                    {
                        TeamSurrenderStatusNotify(p.ClientId, p.Team, Team, SurrenderReason.VoteWasNoSurrender, (byte)count.Item1, (byte)count.Item2);
                    }
                }
            }

            if (toEnd)
            {
                toEndTimer -= Game.Time.DeltaTime;
                if (toEndTimer <= 0)
                {
                    //This will have to be changed in the future in order to properly support Map8 surrender.
                    Nexus ourNexus = (Nexus)Game.ObjectManager.GetObjects().First(o => o.Value is Nexus && o.Value.Team == Team).Value;
                    if (ourNexus == null)
                    {
                        _logger.Error("Unable to surrender correctly, couldn't find the nexus!");
                        return;
                    }
                    ourNexus.Die(null);
                }
            }
        }
    }
}
