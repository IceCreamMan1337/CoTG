using CoTGEnumNetwork.Packets.Handlers;
using CoTGLibrary.Packets.Common;
using SiphoningStrike.LoadScreen;
using System.Linq;
using static PacketVersioning.PktVersioning;



namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_RequestJoinTeam : PacketHandlerBase<RequestJoinTeam>
    {
        private readonly BlowFish[] Blowfishes;
        public override bool HandlePacket(int userId, RequestJoinTeam req)
        {
            var team = Game.PlayerManager.GetPeerInfo(userId).Team;
            var humanPlayers = Game.PlayerManager.GetPlayers(false).ToList();

            WorldSendGameNumberNotify();

            LoadScreenInfoNotify(userId, team, humanPlayers);

            foreach (var player in humanPlayers)
            {
                // Load everyone's player name.
                RequestRenameNotify(userId, player);

                // Load everyone's champion.
                RequestReskinNotify(userId, player);

                if (player.ClientId == userId)
                {
                    KeyCheckNotify(player.ClientId, player.PlayerId, 0, broadcast: true);

                }
            }

            foreach (var bot in Game.PlayerManager.GetBots())
            {
                // Load everyone's player name.
                RequestRenameNotify(userId, bot);

                // Load everyone's champion.
                RequestReskinNotify(userId, bot);
            }

            return true;
        }
    }
}