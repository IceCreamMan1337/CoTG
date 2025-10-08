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
    public sealed class UNK_OnDisconnected : GamePacket // 0x0B1
    {
        public override GamePacketID ID => GamePacketID.UNK_OnDisconnected;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}