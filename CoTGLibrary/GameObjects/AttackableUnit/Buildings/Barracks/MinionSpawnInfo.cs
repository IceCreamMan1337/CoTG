using System.Collections.Generic;

namespace CoTG.CoTGServer.GameObjects.AttackableUnits.Buildings.Barracks;

public class MinionSpawnInfo
{
    public bool IsDestroyed { get; set; }
    public int GoldRadius { get; set; }
    public int ExperienceRadius { get; set; }
    public Dictionary<string, MinionData> MinionData { get; set; } = new();
    public List<string> MinionSpawnOrder { get; set; } = new();
}