using CoTGEnumNetwork.Packets.Handlers;
using CrystalSlash.Game;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_Ping_Load_Info_106 : PacketHandlerBase<C2S_Ping_Load_Info>
    {
        public override bool HandlePacket(int userId, C2S_Ping_Load_Info req)
        {
            var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
            peerInfo.IsReady = req.ConnectionInfo.Ready;
            PingLoadInfoNotify(peerInfo, req);
            return true;
        }
    }
}
