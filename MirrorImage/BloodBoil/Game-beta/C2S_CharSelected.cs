using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using BloodBoil.Game.Common;
using MirrorImage;

namespace BloodBoil.Game
{
    public sealed class C2S_CharSelected : GamePacket // 0x0C6
    {
        public override GamePacketID ID => GamePacketID.C2S_CharSelected;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}