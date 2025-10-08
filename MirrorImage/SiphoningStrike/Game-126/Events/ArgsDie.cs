using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;

namespace SiphoningStrike.Game.Events
{
    public class ArgsDie : ArgsBase
    {
        private uint[] _assists = new uint[12];
        public float GoldGiven { get; set; }
        public int AssistCount { get; set; }
        public uint[] Assists => _assists;

        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            this.GoldGiven = reader.ReadFloat();
            this.AssistCount = reader.ReadInt32();
            for (var i = 0; i < this.Assists.Length; i++)
            {
                this.Assists[i] = reader.ReadUInt32();
            }
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            writer.WriteFloat(this.GoldGiven);
            writer.WriteInt32(this.AssistCount);
            for (var i = 0; i < this.Assists.Length; i++)
            {
                writer.WriteUInt32(this.Assists[i]);
            }
        }
    }
}
