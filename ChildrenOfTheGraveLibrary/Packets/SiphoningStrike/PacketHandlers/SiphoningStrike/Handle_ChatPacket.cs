using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Chatbox;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;
using SiphoningStrike;
using System;
using System.Numerics;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    //TODO: Add additional team support
    public class Handle_ChatPacket : PacketHandlerBase<ChatPacket>
    {
        private readonly ILog _logger = LoggerProvider.GetLogger();

        private const string _teamChatColor = "<font color=\"#00FF00\">";
        private const string _enemyChatColor = "<font color=\"#FF0000\">";
        private const string _privateChatColor = "<font color=\"" + HTMLFHelperUtils.Colors.PRIVATE + "\">";

        private bool DebugModeMsg(TeamId allyTeam, TeamId enemyTeam, string message)
        {
            var aMsg = _teamChatColor + "[DBG] " + message;
            var eMsg = _enemyChatColor + "[DBG] " + message;

            ChatManager.System(allyTeam, aMsg);
            ChatManager.System(enemyTeam, eMsg);
            return true;
        }

        public override bool HandlePacket(int userId, ChatPacket req)
        {

            var client = Game.PlayerManager.GetPeerInfo(userId);
            var chatType = req.ChatType;

            //TODO: Figure out what this is for.
            var split = req.Message.Split(' ');
            if (split.Length > 1)
            {
                if (int.TryParse(split[0], out var x))
                {
                    if (int.TryParse(split[1], out var y))
                    {
                        client = Game.PlayerManager.GetPeerInfo(userId);
                        OnMapPingNotify(new Vector2(x, y), Pings.PING_DEFAULT, client: client);
                    }
                }
            }

            // Execute commands
            var commandStarterCharacter = Game.ChatCommandManager.CommandStarterCharacter;
            if (req.Message.StartsWith(commandStarterCharacter))
            {
                var msg = req.Message.Remove(0, 1);
                split = msg.ToLower().Split(' ');

                var command = Game.ChatCommandManager.GetCommand(split[0]);
                if (command != null)
                {
                    try
                    {
                        command.Execute(userId, true, msg);
                    }
                    catch (Exception e)
                    {
                        _logger.Warn($"{command} sent an exception:\n{e}");
                        ChatManager.System(userId, "Something went wrong... Did you write the command correctly?");
                    }
                    return true;
                }


                ChatManager.Error(
                    HTMLFHelperUtils.Font(
                        HTMLFHelperUtils.Colors.RED,
                        HTMLFHelperUtils.Bold(Game.ChatCommandManager.CommandStarterCharacter + split[0])) +
                    HTMLFHelperUtils.Font(
                        HTMLFHelperUtils.Colors.YELLOW,
                        " is not a valid command."));

                ChatManager.Info(
                    HTMLFHelperUtils.Font(
                        HTMLFHelperUtils.Colors.PINK,
                        HTMLFHelperUtils.Bold(
                            Game.ChatCommandManager.CommandStarterCharacter + "help")) +
                    HTMLFHelperUtils.Font(
                        HTMLFHelperUtils.Colors.PINK, " for a list of available commands."));

                return true;
            }

            var message = req.Message;
            var allyTeam = Game.PlayerManager.GetPeerInfo(userId).Team;
            var enemyTeam = CustomConvert.GetEnemyTeam(allyTeam);

            ////Send messages to all players if cheats are enabled.
            //if (Game.Config.ChatCheatsEnabled)
            //{
            //     return DebugModeMsg(allyTeam, enemyTeam, message);
            //}
            if (Enum.IsDefined(typeof(ChatType), (ChatType)chatType))
            {
                ChatManager.Normal(userId, message, (ChatType)chatType);
                return true;
            }
            else
            {
                _logger.Error("Unknown ChatMessageType:" + req.ChatType);
                ChatManager.System(client.ClientId, "Unable to send message.");
                return false;
            }

        }
    }
}
