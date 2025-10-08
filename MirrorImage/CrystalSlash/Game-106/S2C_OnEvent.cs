using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using CrystalSlash.Game.Common;
using MirrorImage;
using CrystalSlash.Game.Events;

namespace CrystalSlash.Game
{
    public sealed class S2C_OnEvent : GamePacket // 0x0AB
    {
        public override GamePacketID ID => GamePacketID.S2C_OnEvent;
        public IEvent Event { get; set; }
        internal override void ReadBody(ByteReader reader)
        {
            this.Event = reader.ReadEvent();

        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteEvent(Event);

        }
    }
}