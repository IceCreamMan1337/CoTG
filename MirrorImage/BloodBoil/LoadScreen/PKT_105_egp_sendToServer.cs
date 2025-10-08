using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;


namespace BloodBoil.LoadScreen
{
    public class egp_sendToServer : LoadScreenPacket
    {

        public override LoadScreenPacketID ID => LoadScreenPacketID.egp_sendToServer;
        public int ClientID { get; set; }
        public short MessageId { get; set; }

        internal override void ReadBody(ByteReader reader)
        {
            //reader.ReadPad(3);

            this.ClientID = reader.ReadInt32();
            this.MessageId = reader.ReadInt16();
           // this.ExtraBytes = reader.ReadLeft();

        }

        internal override void WriteBody(ByteWriter writer)
        {
            writer.WriteInt32(this.ClientID);
            writer.WriteInt16(this.MessageId);

        }
    }
}