using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;

namespace SiphoningStrike.Game.Events
{
    public class ArgsBase
    {
        public uint OtherNetID { get; set; }
        public virtual void ReadArgs(ByteReader reader)
        {
            this.OtherNetID = reader.ReadUInt32();
        }
        public virtual void WriteArgs(ByteWriter writer)
        {
            writer.WriteUInt32(this.OtherNetID);
        }
    }
}
