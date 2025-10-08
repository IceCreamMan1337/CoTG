using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Packets.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_MapPing : PacketHandlerBase<C2S_MapPing>
    {
        public override bool HandlePacket(int userId, C2S_MapPing req)
        {
            var client = Game.PlayerManager.GetPeerInfo(userId);
            OnMapPingNotify(req.Position.ToVector2(), (Pings)req.PingCategory, req.TargetNetID, client);
            return true;
        }
    }
}
