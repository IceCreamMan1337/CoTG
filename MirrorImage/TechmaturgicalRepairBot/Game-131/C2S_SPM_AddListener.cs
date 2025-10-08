using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using TechmaturgicalRepairBot.Game.Common;
using MirrorImage;

namespace TechmaturgicalRepairBot.Game
{
    public sealed class C2S_SPM_AddListener : GamePacket, IUnusedPacket // 0x097
    {
        public override GamePacketID ID => GamePacketID.C2S_SPM_AddListener;
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