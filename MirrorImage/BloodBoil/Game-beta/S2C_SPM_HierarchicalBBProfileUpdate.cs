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
    public sealed class S2C_SPM_HierarchicalBBProfileUpdate : GamePacket, IUnusedPacket // 0x0BE
    {
        public override GamePacketID ID => GamePacketID.S2C_SPM_HierarchicalBBProfileUpdate;
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