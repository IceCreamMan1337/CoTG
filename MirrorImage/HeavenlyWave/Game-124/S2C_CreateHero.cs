using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using HeavenlyWave.Game.Common;
using MirrorImage;
using System.Runtime.Versioning;

namespace HeavenlyWave.Game
{
    public sealed class S2C_CreateHero : GamePacket // 0x04F
    {
        public override GamePacketID ID => GamePacketID.S2C_CreateHero;

        public uint UnitNetID { get; set; }
        public uint ClientID { get; set; }
        public byte NetNodeID { get; set; }
        public byte SkillLevel { get; set; }
        public bool TeamIsOrder { get; set; }
        public bool IsBot { get; set; }
        public byte BotRank { get; set; }
        public byte SpawnPositionIndex { get; set; }
        public uint SkinID { get; set; }
        public string Name { get; set; } = "";
        public string Skin { get; set; } = "";

        internal override void ReadBody(ByteReader reader)
        {
            this.UnitNetID = reader.ReadUInt32();
            this.ClientID = reader.ReadUInt32();
            this.NetNodeID = reader.ReadByte();
            this.SkillLevel = reader.ReadByte();
            this.TeamIsOrder = reader.ReadBool();
            this.IsBot = reader.ReadBool();
            this.BotRank = reader.ReadByte();
            this.SpawnPositionIndex = reader.ReadByte();
            this.SkinID = reader.ReadUInt32();
            this.Name = reader.ReadFixedString(40); //if version 126 = 40 //if version 131 = 128 
            this.Skin = reader.ReadFixedString(40);
        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteUInt32(this.UnitNetID);
            writer.WriteUInt32(this.ClientID);
            writer.WriteByte(this.NetNodeID);
            writer.WriteByte(this.SkillLevel);
            writer.WriteBool(this.TeamIsOrder);
            writer.WriteBool(this.IsBot);
            writer.WriteByte(this.BotRank);
            writer.WriteByte(this.SpawnPositionIndex);
            writer.WriteUInt32(this.SkinID);
            writer.WriteFixedString(this.Name, 40); //if version 131 = 128 
            writer.WriteFixedString(this.Skin, 40);
        }
    }
}