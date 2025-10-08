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
    public sealed class UNK_Turret_CreateTurret : GamePacket // 0x040
    {
        public override GamePacketID ID => GamePacketID.UNK_Turret_CreateTurret;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}