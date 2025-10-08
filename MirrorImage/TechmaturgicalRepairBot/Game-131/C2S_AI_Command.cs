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
    public sealed class C2S_AI_Command : GamePacket // 0x07E
    {
        public override GamePacketID ID => GamePacketID.C2S_AI_Command;

        public string Command { get; set; }

        internal override void ReadBody(ByteReader reader)
        {
            this.Command = reader.ReadZeroTerminatedString();
        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteZeroTerminatedString(this.Command);
        }
    }
}