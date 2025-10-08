using System.Numerics;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.GameObjects.AttackableUnits.Buildings.Barracks;
using CoTG.CoTGServer.GameObjects.AttackableUnits;

namespace MapScripts;

public class DefaultLevelScript : ILevelScript
{
    public float InitialSpawnTime { get; private set; } = 0.0f;
    public virtual void OnPostLevelLoad()
    {
    }
    public virtual void OnLevelInit()
    {
    }
    public virtual void OnLevelInitServer()
    {
    }
    public virtual void BarrackReactiveEvent(TeamId team, Lane lane)
    {
    }
    public virtual void HandleDestroyedObject(AttackableUnit destroyed)
    {
    }
    public virtual void DisableSuperMinions(TeamId team, Lane lane)
    {
    }
    public virtual InitMinionSpawnInfo GetInitSpawnInfo(Lane lane, TeamId team)
    {
        return new();
    }
    public virtual MinionSpawnInfo GetMinionSpawnInfo(Lane lane, int waveCount, TeamId teamID)
    {
        return new();
    }
    public virtual void NeutralMinionDeath(string minionName, ObjAIBase killer, Vector3 position)
    {
    }
}