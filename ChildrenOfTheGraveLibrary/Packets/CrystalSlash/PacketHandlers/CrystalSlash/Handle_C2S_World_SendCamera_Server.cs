using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using CrystalSlash.Game;

namespace ChildrenOfTheGraveLibrary.Packets.PacketHandlers.CrystalSlash
{
    public class Handle_C2S_World_SendCamera_Server_106 : PacketHandlerBase<C2S_World_SendCamera_Server>
    {
        public override bool HandlePacket(int userId, C2S_World_SendCamera_Server req)
        {
            return true;
        }
    }
}
