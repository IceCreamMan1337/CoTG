using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;


namespace CrystalSlash.LoadScreen
{
    public class Chat : LoadScreenPacket
    {//tips ? unused in replay 
        public override LoadScreenPacketID ID => LoadScreenPacketID.Chat ;

        public uint ClientID { get; set; }
        public uint NetID { get; set; }
        public int  Size { get; set; } 
        public uint ChatType { get; set; }

        public string Message { get; set; } = "";

        internal override void ReadBody(ByteReader reader)
        {
            reader.ReadPad(1);
            this.ClientID = reader.ReadUInt16();
          //  NetID = reader.ReadUInt32();
          //  Localized = reader.ReadBool();
            this.ChatType = reader.ReadUInt32();
          //  var paramsSize = reader.ReadInt32();
            var messageSize = reader.ReadInt32();
            this.Size = messageSize;
         //   if (messageSize > 1024)
         //       throw new IOException("Message size too big!");
            
            var msg = reader.ReadBytes(messageSize);
          
               
                Message = Encoding.UTF8.GetString(msg);
                reader.ReadPad(1);
            
            
        }
        internal override void WriteBody(ByteWriter writer)
        {
            writer.WritePad(1);
            writer.WriteUInt16((ushort)this.ClientID);
            writer.WriteUInt32(this.ChatType);

            byte[] message;

            message = Encoding.UTF8.GetBytes(Message);
       
           
            var messageSize = message.Length;
          //  if (messageSize > 1024)
          //      throw new IOException("Message size too big!");

            writer.WriteInt32(messageSize);

 
            writer.WriteBytes(message);
            writer.WritePad(1);
        }
    }
}
