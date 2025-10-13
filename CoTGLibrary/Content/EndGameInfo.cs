using System.Collections.Generic;
using System.Linq;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.GameObjects.StatsNS;
using Newtonsoft.Json;

namespace CoTG.CoTGServer.Content
{

    public class EndGameInfo
    {
        [JsonProperty]
        public long GameId;
        [JsonProperty]
        public float Time;
        [JsonProperty]
        public int WinningTeam;
        [JsonProperty]
        public int MapId;
        [JsonProperty]
        public Dictionary<TeamId, EndGameTeamStats> EndGameTeams = new()
        {
            {TeamId.TEAM_ORDER, new EndGameTeamStats()},
            {TeamId.TEAM_CHAOS, new EndGameTeamStats()}
        };

        internal EndGameInfo(int winningTeam)
        {
            GameId = Game.Config.GameId;
            Time = Game.Time.GameTime;
            WinningTeam = winningTeam;
            MapId = Game.Map.Id;
            foreach (var player in Game.PlayerManager.GetPlayers())
            {
                EndGameTeams[player.Team].Players.Add(new EndGameChampionStats(player.Champion));
            }
        }
    }
}

public class EndGameTeamStats
{
    [JsonProperty]
    public int TeamKillScore => Players.Sum(x => x.ChampionStatistics.Kills);
    [JsonProperty]
    public float TeamPointScore => Players.Sum(x => x.Score);
    [JsonProperty]
    public List<EndGameChampionStats> Players = [];
}

public class EndGameChampionStats
{
    [JsonProperty]
    public string Name;
    [JsonProperty]
    public string Champion;
    [JsonProperty]
    public int SkinId;
    [JsonProperty]
    public int Level;
    [JsonProperty]
    public float Score;
    [JsonProperty]
    public float Gold;
    [JsonProperty]
    public ChampionStatistics ChampionStatistics;
    [JsonProperty]
    public List<int> Items = [];

    public EndGameChampionStats(Champion ch)
    {
        Name = ch.Name;
        Champion = ch.Model;
        SkinId = ch.SkinID;
        Level = ch.Experience.Level;
        Score = ch.ChampionStats.Score;
        ChampionStatistics = ch.ChampionStatistics;
        Gold = ch.GoldOwner.TotalGoldEarned;

        foreach (var item in ch.ItemInventory.GetItems())
        {
            Items.Add(item.ItemData.Id);
        }
    }
}