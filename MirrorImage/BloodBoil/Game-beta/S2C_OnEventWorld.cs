using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using BloodBoil.Game.Common;
using MirrorImage;

using BloodBoil.Game.Events;
namespace BloodBoil.Game
{
    public sealed class S2C_OnEventWorld : GamePacket // 0x04A
    {
        public override GamePacketID ID => GamePacketID.S2C_OnEventWorld;
        public EventWorld EventWorld = new EventWorld();
        internal override void ReadBody(ByteReader reader)
        {
            this.EventWorld = reader.ReadEventWorld();
        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteEventWorld(EventWorld);
        }
    }
}