using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.Barracks;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

namespace MapScripts;

public interface ILevelScript
{
    float InitialSpawnTime { get; }
    void OnPostLevelLoad();
    void OnLevelInit();
    void OnLevelInitServer();
    void BarrackReactiveEvent(TeamId team, Lane lane);
    void HandleDestroyedObject(AttackableUnit destroyed);
    void DisableSuperMinions(TeamId team, Lane lane);
    InitMinionSpawnInfo GetInitSpawnInfo(Lane lane, TeamId team);
    MinionSpawnInfo GetMinionSpawnInfo(Lane lane, int waveCount, TeamId teamID);
    void NeutralMinionDeath(string minionName, ObjAIBase killer, Vector3 position);
}