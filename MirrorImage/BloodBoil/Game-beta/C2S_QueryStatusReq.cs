
using MirrorImage;
namespace BloodBoil.Game
{
    public sealed class C2S_QueryStatusReq : GamePacket // 0x017
    {
        public override GamePacketID ID => GamePacketID.C2S_QueryStatusReq;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}