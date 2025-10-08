using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;

namespace SiphoningStrike.Game.Events
{
    public class ArgsAlert : ArgsBase
    {
        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            reader.ReadPad(8);
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            writer.WritePad(8);
        }
    }
}
