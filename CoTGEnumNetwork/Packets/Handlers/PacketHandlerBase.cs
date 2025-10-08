using MirrorImage;

namespace CoTGEnumNetwork.Packets.Handlers
{
    public abstract class PacketHandlerBase<T> where T : BasePacket
    {
        public abstract bool HandlePacket(int userId, T req);
    }
}
