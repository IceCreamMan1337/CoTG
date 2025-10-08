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
    public class ArgsKillingSpree : ArgsBase
    {
        public int Amount { get; set; }
        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            this.Amount = reader.ReadInt32();
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            writer.WriteInt32(this.Amount);
        }
    }
}
