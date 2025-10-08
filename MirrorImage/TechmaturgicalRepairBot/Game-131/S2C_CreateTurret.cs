using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using TechmaturgicalRepairBot.Game.Common;
using MirrorImage;

namespace TechmaturgicalRepairBot.Game
{
    public sealed class S2C_CreateTurret : GamePacket // 0x0A5
    {
        public override GamePacketID ID => GamePacketID.S2C_CreateTurret;

        public uint UniteNetID { get; set; }
        public byte UnitNetNodeID { get; set; }
        public string Name { get; set; }

        public bool IsTargetable { get; set; }

        public uint IsTargetableToTeamSpellFlags { get; set; }


        internal override void ReadBody(ByteReader reader)
        {
            this.UniteNetID = reader.ReadUInt32();
            this.UnitNetNodeID = reader.ReadByte();
            this.Name = reader.ReadFixedString(64);

            //hack
            byte bitfield = reader.ReadByte();
            this.IsTargetable = (bitfield & 1) != 0;

            this.IsTargetableToTeamSpellFlags = reader.ReadUInt32(); 


        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteUInt32(this.UniteNetID);
            writer.WriteByte(this.UnitNetNodeID);
            writer.WriteFixedString(this.Name, 64);

            //hack
            byte bitfield = 0;
            if (IsTargetable)
                bitfield |= 1;
            writer.WriteByte(bitfield);

            writer.WriteUInt32(IsTargetableToTeamSpellFlags);

        }
    }
}