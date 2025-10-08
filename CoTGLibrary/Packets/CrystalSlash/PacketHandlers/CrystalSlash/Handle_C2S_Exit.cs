using CoTGEnumNetwork.Packets.Handlers;
using PacketDefinitions106;
using CrystalSlash.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_Exit_106 : PacketHandlerBase<C2S_Exit>
    {
        private readonly PacketHandlerManager106 _packetHandlerManager;

        public Handle_C2S_Exit_106(PacketHandlerManager106 packetHandlerManager)
        {
            _packetHandlerManager = packetHandlerManager;
        }

        public override bool HandlePacket(int userId, C2S_Exit req)
        {
            return _packetHandlerManager.HandleDisconnect(userId);
        }
    }
}