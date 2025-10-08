using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_QueryStatusReq : PacketHandlerBase<C2S_QueryStatusReq>
    {
        public override bool HandlePacket(int userId, C2S_QueryStatusReq req)
        {
            S2C_QueryStatusAnsNotify(userId);
            return true;
        }
    }
}
