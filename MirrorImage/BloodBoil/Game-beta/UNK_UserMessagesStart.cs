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
    public sealed class UNK_UserMessagesStart : GamePacket // 0x063
    {
        public override GamePacketID ID => GamePacketID.UNK_UserMessagesStart;
        internal override void ReadBody(ByteReader reader)
        {
        }
        internal override void WriteBody(ByteWriter writer)
        {
        }
    }
}