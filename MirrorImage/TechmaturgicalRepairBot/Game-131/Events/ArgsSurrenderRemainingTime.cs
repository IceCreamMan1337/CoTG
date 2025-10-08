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
    public class ArgsSurrenderRemainingTime : ArgsBase
    {
        public float RemainingTime { get; set; }

        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            this.RemainingTime = reader.ReadFloat();
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            writer.WriteFloat(this.RemainingTime);
        }
    }
}
