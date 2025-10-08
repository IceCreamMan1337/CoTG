using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using SiphoningStrike.Game.Common;
using MirrorImage;
using SiphoningStrike.Game.Events;
using System.Threading.Tasks;
using MirrorImage;

namespace SiphoningStrike.Game
{
    public sealed class S2C_NPC_Die_EventHistory : GamePacket // 0x024
    {
        /*  public class EventData
          {
              public float TimeStamp { get; set; }
              public ushort Count { get; set; }
              public uint SourceNetID { get; set; }

              public List<EventHistoryEntry> Event = new List<EventHistoryEntry>();
          }
        */
        public override GamePacketID ID => GamePacketID.S2C_NPC_Die_EventHistory;

        public uint KillerNetID { get; set; }  //
        public float TimeWindow { get; set; }   //timedeath? 

        public List<EventHistoryEntry> Events = new List<EventHistoryEntry>();

        internal override void ReadBody(ByteReader reader)
        {



            this.SenderNetID = reader.ReadUInt32();
            this.KillerNetID = reader.ReadUInt32();




            //  this.EventSourceType = reader.ReadByte();


            //  this.Othernetid = reader.ReadUInt32();
            //  this.DurationOpenedWindows = reader.ReadFloat();


            this.BytesLeft = reader.ReadBytes(4); // find what is this 
            int events = reader.ReadInt32();  //surely an bitshift 
            int count = events - 32;
            for (int i = 0; i < events; i++)
            {
                //Entries.Add(reader.ReadEventHistoryEntry());
            }
        }
        internal override void WriteBody(ByteWriter writer)
        {
          //  writer.WriteUInt32(this.SenderNetID);
            writer.WriteUInt32(this.KillerNetID);
            writer.WriteFloat(this.TimeWindow);

            writer.WriteByte(0x0);
            writer.WriteByte(0x0);
            writer.WriteByte(0x0);
            writer.WriteByte(0x0);

     //       writer.WriteByte(0xB0);
      //      writer.WriteByte(0x09);
      //      writer.WriteByte(0x0);
       //     writer.WriteByte(0x0);

            //uint bitfield = 0;
            // bitfield |= (this.KillerEventSourceType & 0x0F);
           


            var buffer = new ByteWriter();
            foreach (var edata in this.Events)
            {
                //buffer.WriteEventHistoryEntry(edata);
               /* if (edata == null)
                {
                    edata = new EventHistoryEntry();
                }*/ 
                buffer.WriteFloat(edata.Timestamp);
                buffer.WriteUInt16(edata.Count);
                buffer.WriteByte((byte)edata.Event.ID);
                buffer.WriteUInt32(edata.Source);
                //   writer.WritePad(4);
                if (edata.Event is IEventEmptyHistory)
                {
                    buffer.WriteUInt32(edata.Event.OtherNetID);
                }
                else
                {
                    edata.Event.WriteArgs(buffer);
                }

                
            }
            writer.WriteInt32(buffer.GetBytes().Count());
            int countofevent = this.Events.Count();
            writer.WriteUInt32((uint)countofevent);
            writer.WriteBytes(buffer.GetBytes());
            
           
        }
    }

}