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
    public sealed class C2S_ClientFinished : GamePacket // 0x091
    {
        public override GamePacketID ID => GamePacketID.C2S_ClientFinished;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}