using ChildrenOfTheGraveEnumNetwork.Enums;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

public class DisconnectAdjustments : AI_DifficultyScaling
{

    public TeamId ReferenceTeam;

    public int BotCount;

    public TeamId EnemyTeam;

    public int ConnectedPlayersOnEnemyTeam;

}

