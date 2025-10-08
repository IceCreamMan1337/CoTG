using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using ChildrenOfTheGraveLibrary.Packets.Common;
using CrystalSlash.LoadScreen;
using static PacketVersioning.PktVersioning;




namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_RequestJoinTeam_106 : PacketHandlerBase<RequestJoinTeam>
    {
        private readonly BlowFish[] Blowfishes;
        public override bool HandlePacket(int userId, RequestJoinTeam req)
        {
            var team = Game.PlayerManager.GetPeerInfo(userId).Team;
            var humanPlayers = Game.PlayerManager.GetPlayers(false);
            // var ALLPlayers = Game.PlayerManager.GetPlayers(true);
            LoadScreenInfoNotify(userId, team, [.. humanPlayers]);

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
            WorldSendGameNumberNotify();
            return true;
        }
    }
}