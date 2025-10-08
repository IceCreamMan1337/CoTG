using System.Linq;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Chatbox;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer;

public static class ChatManager
{
    private static string TEXT_COLOR = HTMLFHelperUtils.Colors.YELLOW;

    /// <summary>
    /// Sends a formatted system message with a red [Error] tag.
    /// </summary>
    /// <param name="message">System message to send.</param>
    /// <param name="fontSize">System message font size.</param>
    public static void Error(string message, int fontSize = 16)
    {
        var color = HTMLFHelperUtils.Colors.RED;
        var tag = HTMLFHelperUtils.Bold("[Error]");

        System(HTMLFHelperUtils.Font(fontSize, color, tag) + HTMLFHelperUtils.Font(TEXT_COLOR, ": " + message));
    }

    /// <summary>
    /// Sends a formatted system message with a blue [ChildrenOfTheGrave info] tag.
    /// </summary>
    /// <param name="message">System message to send.</param>
    /// <param name="fontSize">System message font size.</param>
    public static void Info(string message, int fontSize = 16)
    {
        var color = HTMLFHelperUtils.Colors.GREEN;
        var tag = HTMLFHelperUtils.Bold("[ChildrenOfTheGrave info]");

        System(HTMLFHelperUtils.Font(fontSize, color, tag) + HTMLFHelperUtils.Font(TEXT_COLOR, ": " + message));
    }

    /// <summary>
    /// Sends a formatted system message with a blue [Syntax] tag.
    /// </summary>
    /// <param name="message">System message to send.</param>
    /// <param name="fontSize">System message font size.</param>
    public static void Syntax(string message, int fontSize = 16)
    {
        var color = HTMLFHelperUtils.Colors.BLUE;
        var tag = HTMLFHelperUtils.Bold("[Syntax]");

        System(HTMLFHelperUtils.Font(fontSize, color, tag) + HTMLFHelperUtils.Font(TEXT_COLOR, ": " + message));
    }

    /// <summary>
    /// Sends a formatted syntax error system message with a red [Error] tag.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="fontSize"></param>
    public static void SyntaxError(string message = ": Incorrect command syntax", int fontSize = 16)
    {
        var color = HTMLFHelperUtils.Colors.RED;
        var tag = HTMLFHelperUtils.Bold("[Error]");

        System(HTMLFHelperUtils.Font(fontSize, color, tag) + HTMLFHelperUtils.Font(TEXT_COLOR, ": " + message));
    }

    /// <summary>
    /// Sends an unformatted chat message.
    /// </summary>
    /// <param name="userid">ID of the user who is sending the message.</param>
    /// <param name="message">Chat message to send.</param>
    /// <param name="type">Chat message scope type.</param>
    public static void Normal(int userid, string message, ChatType type)
    {
        var sender = Game.PlayerManager.GetPeerInfo(userid);
        ChatPacketNotify(sender, type, message);
    }

    /// <summary>
    /// Sends a system message to the specified user.
    /// </summary>
    /// <param name="userid">ID of the user to send the message to.</param>
    /// <param name="message">System message to send.</param>
    public static void System(int userid, string message)
    {
        var sender = Game.PlayerManager.GetPeerInfo(userid);
        SystemMessageNotify(sender, ChatType.Private, message);
    }

    /// <summary>
    /// Sends a system message to the specified team.
    /// </summary>
    /// <param name="team">TeamId to send the message to.</param>
    /// <param name="message">System message to send.</param>
    public static void System(TeamId team, string message)
    {
        var randomTeamSender = Game.PlayerManager.GetPlayers().FirstOrDefault(player => player.Team == team);
        if (randomTeamSender != null)
            SystemMessageNotify(randomTeamSender, ChatType.Team, message);
    }

    /// <summary>
    /// Sends a system message to all players.
    /// </summary>
    /// <param name="message">System message to send.</param>
    public static void System(string message)
    {
        var randomSender = Game.PlayerManager.GetPlayers().First();
        SystemMessageNotify(randomSender, ChatType.All, message);
    }
}