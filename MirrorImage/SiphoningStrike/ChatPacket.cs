using System;
using MirrorImage;

namespace SiphoningStrike
{
    public sealed class ChatPacket : BasePacket
    {
        public uint ClientID { get; set; }
        public uint ChatType { get; set; }
        public string Message { get; set; }

        public ChatPacket() { }
        public ChatPacket(byte[] data) { Read(data); }

        internal override void ReadPacket(ByteReader reader)
        {
            ClientID = reader.ReadUInt32();
            ChatType = reader.ReadUInt32();
            Message = reader.ReadSizedStringWithZero();
        }

        internal override void WritePacket(ByteWriter writer)
        {
            writer.WriteUInt32(ClientID);
            writer.WriteUInt32(ChatType);
            writer.WriteSizedStringWithZero(Message);
        }
    }
}
