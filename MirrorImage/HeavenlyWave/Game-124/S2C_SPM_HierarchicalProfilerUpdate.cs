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
    public sealed class S2C_SPM_HierarchicalProfilerUpdate : GamePacket, IUnusedPacket // 0x001
    {
        public override GamePacketID ID => GamePacketID.S2C_SPM_HierarchicalProfilerUpdate;
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