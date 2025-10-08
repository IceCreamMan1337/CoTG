using CoTGEnumNetwork;
using CoTGEnumNetwork.Packets.Enums;
using CoTGEnumNetwork.Packets.Handlers;
using CrystalSlash.Game;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_MapPing_106 : PacketHandlerBase<C2S_MapPing>
    {
        public override bool HandlePacket(int userId, C2S_MapPing req)
        {
            var client = Game.PlayerManager.GetPeerInfo(userId);
            OnMapPingNotify(req.Position.ToVector2(), (Pings)req.PingCategory, req.TargetNetID, client);
            return true;
        }
    }
}
