using System.Linq;
using CoTGEnumNetwork;
using CoTGEnumNetwork.Packets.Enums;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.Chatbox.Commands
{
    public class MobsCommand : ChatCommandBase
    {
        public override string Command => "mobs";
        public override string Syntax => $"{Command} teamNumber";

        public MobsCommand(ChatCommandManager chatCommandManager) : base(chatCommandManager)
        {
        }

        public override void Execute(int userId, bool hasReceivedArguments, string arguments = "")
        {
            var split = arguments.ToLower().Split(' ');
            if (split.Length < 2)
            {
                ChatManager.SyntaxError();
                ShowSyntax();
                return;
            }

            if (!int.TryParse(split[1], out var team))
            {
                return;
            }

            var units = Game.ObjectManager.GetObjects()
                .Where(xx => xx.Value.Team == team.ToTeamId())
                .Where(xx => xx.Value is Minion || xx.Value is NeutralMinion);

            var client = Game.PlayerManager.GetPeerInfo(userId);
            foreach (var unit in units)
            {
                OnMapPingNotify(unit.Value.Position, Pings.PING_DANGER, client: client);
            }
        }
    }
}
