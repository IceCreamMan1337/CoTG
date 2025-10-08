using CoTGEnumNetwork.Enums;

namespace CoTG.CoTGServer.Scripting.CSharp;

public class DisconnectAdjustments : AI_DifficultyScaling
{

    public TeamId ReferenceTeam;

    public int BotCount;

    public TeamId EnemyTeam;

    public int ConnectedPlayersOnEnemyTeam;

}

