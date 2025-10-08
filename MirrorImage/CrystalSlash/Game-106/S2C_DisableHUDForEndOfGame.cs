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
    public sealed class S2C_DisableHUDForEndOfGame : GamePacket // 0x0C4
    {
        public override GamePacketID ID => GamePacketID.S2C_DisableHUDForEndOfGame;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}