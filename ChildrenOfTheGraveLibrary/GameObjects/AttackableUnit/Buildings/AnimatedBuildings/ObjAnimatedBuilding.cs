using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;

public class ObjAnimatedBuilding : ObjBuilding
{
    public ObjAnimatedBuilding(string name, int collisionRadius = 40,
        Vector2 position = new(), int visionRadius = 0, uint netId = 0, TeamId team = TeamId.TEAM_ORDER, Stats stats = null) :
        base(name, "", collisionRadius, position, visionRadius, netId, team, stats)
    {
        //SetStatus(StatusFlags.Targetable, false);
        // SetStatus(StatusFlags.Invulnerable, true);
        Replication = new ReplicationAnimatedBuilding(this);
        CollisionType = 0;
    }
}
