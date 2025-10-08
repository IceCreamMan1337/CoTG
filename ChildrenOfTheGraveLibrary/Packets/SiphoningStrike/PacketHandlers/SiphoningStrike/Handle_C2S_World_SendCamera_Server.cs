using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;

namespace ChildrenOfTheGraveLibrary.Packets.PacketHandlers.SiphoningStrike
{
    public class Handle_C2S_World_SendCamera_Server : PacketHandlerBase<C2S_World_SendCamera_Server>
    {
        public override bool HandlePacket(int userId, C2S_World_SendCamera_Server req)
        {
            return true;
        }
    }
}
