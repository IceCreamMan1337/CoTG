using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;
namespace

 HeavenlyWave.Game.Events
{
    public class ArgsSurrenderVotes : ArgsBase
    {
        public int ForVote { get; set; }
        public int AgainstVote { get; set; }
        public uint TeamID { get; set; }

        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            this.ForVote = reader.ReadInt32();
            this.AgainstVote = reader.ReadInt32();
            this.TeamID = reader.ReadUInt32();
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            writer.WriteInt32(this.ForVote);
            writer.WriteInt32(this.AgainstVote);
            writer.WriteUInt32(this.TeamID);
        }
    }
}
