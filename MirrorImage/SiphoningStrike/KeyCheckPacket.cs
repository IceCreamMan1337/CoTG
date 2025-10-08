using System;
using MirrorImage;

namespace SiphoningStrike
{
    public sealed class KeyCheckPacket : BasePacket
    {
        public byte Action { get; set; }
        public uint ClientID { get; set; }
        public ulong PlayerID { get; set; }
        public ulong EncryptedPlayerID { get; set; }

        public KeyCheckPacket() { }
        public KeyCheckPacket(byte[] data) { Read(data); }

        internal override void ReadPacket(ByteReader reader)
        {
            Action = reader.ReadByte();
            reader.ReadPad(3);
            ClientID = reader.ReadUInt32();
            PlayerID = reader.ReadUInt64();
            EncryptedPlayerID = reader.ReadUInt64();
        }

        internal override void WritePacket(ByteWriter writer)
        {
            writer.WriteByte(Action);
            writer.WritePad(3);
            writer.WriteUInt32(ClientID);
            writer.WriteUInt64(PlayerID);
            writer.WriteUInt64(EncryptedPlayerID);
        }
    }
}