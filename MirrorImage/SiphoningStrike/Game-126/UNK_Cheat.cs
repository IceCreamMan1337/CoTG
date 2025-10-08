using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using SiphoningStrike.Game.Common;
using MirrorImage;
using MirrorImage;
using static Dropbox.Api.TeamLog.SharedLinkAccessLevel;
using SiphoningStrike.Game.Cheat;
namespace SiphoningStrike.Game
{
#if DEBUG_AB || RELEASE_AB
    public sealed class UNK_Cheat : GamePacket // Ab_pkt
    {

        public override GamePacketID ID => GamePacketID.UNK_Cheat;


         public CheatID IDCheat { get; set; }

        internal override void ReadBody(ByteReader reader)
        {
            this.IDCheat = (CheatID)reader.ReadByte();
        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteByte((byte)this.IDCheat);
        }
    }
#endif
}



