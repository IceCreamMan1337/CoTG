using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;

namespace SiphoningStrike.Game.Events
{
    public class ArgsItemConsumedPurchased : ArgsBase
    {
        public uint ItemID { get; set; }

        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            this.ItemID = reader.ReadUInt32();
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            writer.WriteUInt32(this.ItemID);
        }
    }
}
