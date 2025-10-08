using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using CrystalSlash.Game;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_Waypoint_Acc_106 : PacketHandlerBase<C2S_Waypoint_Acc>
    {
        public override bool HandlePacket(int userId, C2S_Waypoint_Acc req)
        {
            // TODO: check movement cheat
            return true;
        }
    }
}
