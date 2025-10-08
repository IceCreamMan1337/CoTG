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
    public sealed class C2S_OnShopOpened : GamePacket // 0x060
    {
        public override GamePacketID ID => GamePacketID.C2S_OnShopOpened;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}