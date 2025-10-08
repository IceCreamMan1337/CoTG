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
    public sealed class S2C_OnLeaveTeamVisiblity : GamePacket // 0x0EF
    {
        public override GamePacketID ID => GamePacketID.S2C_OnLeaveTeamVisiblity;

        public byte VisiblityTeam { get; set; }   // same than "enter" , only 0 or 1 surely an bool 

        internal override void ReadBody(ByteReader reader)
        {
            this.VisiblityTeam = reader.ReadByte();
        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteByte(this.VisiblityTeam);
        }
    }
}