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
    public sealed class S2C_UpdateLevelProp : GamePacket // 0x0DA
    {
        public override GamePacketID ID => GamePacketID.S2C_UpdateLevelProp;

        public UpdateLevelPropData UpdateLevelPropData { get; set; }

        internal override void ReadBody(ByteReader reader)
        {
            this.UpdateLevelPropData = reader.ReadUpdateLevelPropData();
        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteUpdateLevelPropData(this.UpdateLevelPropData);
        }
    }
}