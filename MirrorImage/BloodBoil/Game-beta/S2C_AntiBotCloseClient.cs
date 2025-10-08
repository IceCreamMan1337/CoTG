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
    public sealed class S2C_AntiBotCloseClient : GamePacket, IUnusedPacket // 0x0EB
    {
        public override GamePacketID ID => GamePacketID.S2C_AntiBotCloseClient;
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