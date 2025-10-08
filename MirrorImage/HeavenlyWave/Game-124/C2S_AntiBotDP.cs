using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using HeavenlyWave.Game.Common;
using MirrorImage;

namespace HeavenlyWave.Game
{
    public sealed class C2S_AntiBotDP : GamePacket, IUnusedPacket // 0x0EC
    {
        public override GamePacketID ID => GamePacketID.C2S_AntiBotDP;
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