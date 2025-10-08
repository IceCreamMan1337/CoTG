using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using SiphoningStrike.Game.Common;
using MirrorImage;
using MirrorImage;

namespace SiphoningStrike.Game
{
    public sealed class S2C_StartSpawn : GamePacket // 0x065
    {
        public override GamePacketID ID => GamePacketID.S2C_StartSpawn;

        public ushort BotCountOrder { get; set; }
        public ushort BotCountChaos { get; set; }

        internal override void ReadBody(ByteReader reader)
        {
            this.BotCountOrder = reader.ReadUInt16();
            this.BotCountChaos = reader.ReadUInt16();
        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteUInt16(BotCountOrder);
            writer.WriteUInt16(BotCountChaos);
        }
    }
}