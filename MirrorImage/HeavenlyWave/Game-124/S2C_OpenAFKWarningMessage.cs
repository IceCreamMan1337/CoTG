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
    public sealed class S2C_OpenAFKWarningMessage : GamePacket // 0x0C0
    {
        public override GamePacketID ID => GamePacketID.S2C_OpenAFKWarningMessage;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}