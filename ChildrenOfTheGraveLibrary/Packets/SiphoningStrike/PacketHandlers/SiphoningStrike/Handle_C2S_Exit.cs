using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using PacketDefinitions126;
using SiphoningStrike.Game;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_Exit : PacketHandlerBase<C2S_Exit>
    {
        private readonly PacketHandlerManager126 _packetHandlerManager;

        public Handle_C2S_Exit(PacketHandlerManager126 packetHandlerManager)
        {
            _packetHandlerManager = packetHandlerManager;
        }

        public override bool HandlePacket(int userId, C2S_Exit req)
        {
            return _packetHandlerManager.HandleDisconnect(userId);
        }
    }
}