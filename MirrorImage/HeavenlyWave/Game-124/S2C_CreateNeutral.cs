using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using HeavenlyWave.Game.Common;
using MirrorImage;

namespace HeavenlyWave.Game
{
    public sealed class S2C_CreateNeutral : GamePacket // 0x066
    {
        public override GamePacketID ID => GamePacketID.S2C_CreateNeutral;

        public uint UnitNetID { get; set; }
        public byte UnitNetNodeID { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 GroupPosition { get; set; }
        public Vector3 FaceDirectionPosition { get; set; }
        public string Name { get; set; } = "";
        public string SkinName { get; set; } = "";
        public string UniqueName { get; set; } = "";
        public string MinimapIcon { get; set; } = "";
        public uint TeamID { get; set; }
        public int DamageBonus { get; set; }
        public int HealthBonus { get; set; }
        public int RoamState { get; set; }
        public int GroupNumber { get; set; }
        public bool BehaviorTree { get; set; }

        internal override void ReadBody(ByteReader reader)
        {
            UnitNetID = reader.ReadUInt32();
            UnitNetNodeID = reader.ReadByte();
            Position = reader.ReadVector3();
            GroupPosition = reader.ReadVector3();
            FaceDirectionPosition = reader.ReadVector3();
            Name = reader.ReadFixedString(64);
            SkinName = reader.ReadFixedString(64);
            UniqueName = reader.ReadFixedString(64);
            MinimapIcon = reader.ReadFixedString(64);
            TeamID = reader.ReadUInt32();
            DamageBonus = reader.ReadInt32();
            HealthBonus = reader.ReadInt32();
            RoamState = reader.ReadInt32();
            GroupNumber = reader.ReadInt32();

            byte bitfield = reader.ReadByte();
            BehaviorTree = (bitfield & 0x01) != 0;
        }

        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteUInt32(UnitNetID);
            writer.WriteByte(UnitNetNodeID);
            writer.WriteVector3(Position);
            writer.WriteVector3(GroupPosition);
            writer.WriteVector3(FaceDirectionPosition);
            writer.WriteFixedString(Name, 64);
            writer.WriteFixedString(SkinName, 64);
            writer.WriteFixedString(UniqueName, 64);
            writer.WriteFixedString(MinimapIcon, 64);
            writer.WriteUInt32(TeamID);
            writer.WriteInt32(DamageBonus);
            writer.WriteInt32(HealthBonus);
            writer.WriteInt32(RoamState);
            writer.WriteInt32(GroupNumber);

            byte bitfield = 0;
            if (BehaviorTree)
                bitfield |= 0x01;
            writer.WriteByte(bitfield);
        }
    }
}