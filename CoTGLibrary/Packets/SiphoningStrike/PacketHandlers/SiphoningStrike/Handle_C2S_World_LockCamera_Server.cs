using CoTGEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_World_LockCamera_Server : PacketHandlerBase<C2S_World_LockCamera_Server>
    {
        public override bool HandlePacket(int userId, C2S_World_LockCamera_Server req)
        {
            return true;
        }
    }
}
