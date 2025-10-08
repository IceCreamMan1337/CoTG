using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;
namespace

 TechmaturgicalRepairBot.Game.Events
{
    public class ArgsCapturePoint : ArgsBase
    {
        public uint CapturePoint { get; set; }
        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            this.CapturePoint = reader.ReadUInt32();
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            writer.WriteUInt32(this.CapturePoint);
        }
    }
}
