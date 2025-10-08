using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;
namespace

 CrystalSlash.Game.Events
{
    public class ArgsGoldEarned : ArgsBase
    {
        public float Amount { get; set; }
        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            this.Amount = reader.ReadFloat();
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            writer.WriteFloat(this.Amount);
        }
    }
}
