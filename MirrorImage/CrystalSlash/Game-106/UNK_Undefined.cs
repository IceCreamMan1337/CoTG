using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using CrystalSlash.Game.Common;
using MirrorImage;

namespace CrystalSlash.Game
{
    public sealed class UNK_Undefined : GamePacket, IUnusedPacket // 0x0CF
    {
        public override GamePacketID ID => GamePacketID.UNK_Undefined;
        internal override void ReadBody(ByteReader reader)
        {
            //Unused
        }
        internal override void WriteBody(ByteWriter writer)
        {
            //Unused
        }
    }
}