using CoTGEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_Waypoint_Acc : PacketHandlerBase<C2S_Waypoint_Acc>
    {
        public override bool HandlePacket(int userId, C2S_Waypoint_Acc req)
        {
            // TODO: check movement cheat

            return true;
        }
    }
}
