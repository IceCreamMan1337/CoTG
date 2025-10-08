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
    public sealed class C2S_ClientConnect_NamedPipe : GamePacket // 0x09A
    {
        public override GamePacketID ID => GamePacketID.UNK_ClientConnect_NamedPipe;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}