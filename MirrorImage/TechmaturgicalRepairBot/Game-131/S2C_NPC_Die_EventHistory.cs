using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using TechmaturgicalRepairBot.Game.Common;
using MirrorImage;
using TechmaturgicalRepairBot.Game.Events;
using System.Threading.Tasks;

namespace TechmaturgicalRepairBot.Game
{
    public sealed class S2C_NPC_Die_EventHistory : GamePacket // 0x024
    {
        public class EventData
        {
            public float TimeStamp { get; set; }
            public ushort Count { get; set; }
            public uint SourceNetID { get; set; }

            public List<EventData> Event = new List<EventData>();
        }

        public override GamePacketID ID => GamePacketID.S2C_NPC_Die_EventHistory;


        public uint KillerNetID { get; set; }
        public float DurationOpenedWindows { get; set; }


        public byte EventSourceType { get; set; }


       // public float TimeStamp { get; set; }


        public uint Othernetid { get; set; }
 
        
        public List<EventHistoryEntry> Entries { get; set; } = new List<EventHistoryEntry>();


        //the rest after doesn't exist just an hack for get historie work 

        public int Entriescounthack { get; set; }

        public byte eventid { get; set; }

        public float PhysicalDamage { get; set; }

        

        public float TrueDamage { get; set; }

        

        public uint ParentCasterNetID { get; set; }








        //anothertest 


        public float timetogetdied { get; set; }

        public float Damage { get; set; }

        public uint ParentScriptNameHash { get; set; }

        public byte typeofdamage { get; set; } // it seem exist 03 / 04 / 00 for me 04 = auto attack / 00 attackdamage ? 01 magic damage ? 



        //
        public float TimeStamp { get; set; }
        public ushort Count { get; set; }
        public uint SourceNetID { get; set; }

        // public string hexString = "24110000400f00004040441f4100000000b0090000380000008a48404401000b0f00004011000040c55d6b4200000000000000000400000000000000ffffffff0f00004004700000a410404401000b7931004011000040dec95c4100000000000000000400000000000000ffffffff79310040047100006cfe3f4401000b0f000040110000407e82704200000000000000000400000000000000ffffffff0f00004004700000bff33f4401002611000040110000400200000004afdf060000000003000000be0c7900110000400100000030dd3f4402000b5a31004011000040dec9dc4100000000000000000400000000000000ffffffff5a3100400471000058ca3f4401000b0f00004011000040057177420000000000000000000000007d690e027d690e020f000040003000001fc23f4402000b173100401100004034eba34100000000000000000400000000000000ffffffff17310040047100004ea93f4401000b0f00004011000040057177420000000000000000000000007d690e027d690e020f00004000300000558c3f4401000b17310040110000401519d64000000000000000000400000000000000ffffffff173100400471000030883f4401000b0f00004011000040057177420000000000000000000000007d690e027d690e020f0000400030000086753f4401000b0e0000401100004000000000d3e93843000000000000000077e8b00677e8b0060e0000400010000086753f44010026110000401100004002000000d229dc0a000000000000000077e8b0060e000040002b11054a6f3f4402000b5a31004011000040dec9dc4100000000000000000400000000000000ffffffff5a3100400471000019673f44010026110000401100004002000000c9d6dd090000000001000000886403001100004001000000364c3f440100260e0000401100004002000000beab07000000000000000000d2dea6051100004001000000364c3f4401000b0e0000401100004000000000feae1a430000000000000000d2dea605d2dea6051100004010700000364c3f44010026110000401100004002000000d229dc0a0000000000000000d2dea605110000400162c70501463f4401000b0f00004011000040057177420000000000000000000000007d690e027d690e020f00004000300000ee413f440100260e0000401100004002000000d2dea6050000000000000000557a05060e00004000ef1200ee413f4401000b0e00004011000040000000009b06be420000000000000000557a0506557a05060e00004000700000ee413f44010026110000401100004002000000d229dc0a0000000000000000557a05060e000040003951059b393f4401000b7931004011000040dec95c4100000000000000000400000000000000ffffffff7931004004710000ee243f4401000b0f00004011000040057177420000000000000000000000007d690e027d690e020f00004000300000b61e3f4401000b0f000040110000407e82704200000000000000000400000000000000ffffffff0f00004004700000350e3f440100260f0000401100004002000000e5c4020a000000000000000073010f060f00004000000000350e3f4401000b0f00004011000040b4d5a94200000000000000000000000072010f0673010f060f00004000100000350e3f44010026110000401100004002000000d229dc0a000000000000000073010f060f00004000cb6905e1ec3e440100261100004011000040020000004585060d00000000000000004585060d1100004001f2d005e1ec3e4401002611000040110000400200000099d0bf0e00000000000000004585060d110000400100000063dc3e4402002611000040110000400200000004afdf060000000003000000be0c79001100004001000000aac33e44010026110000401100004002000000c29841050000000003000000be0c790011000040016b120033bb3e440100261100004011000040020000004bd1eb0300000000000000001228f50411000040010000005aa03e4401002611000040110000400200000044565d0c00000000000000001228f5041100004001d312005aa03e440100261100004011000040020000004ec4620d00000000000000001228f5041100004001d312005aa03e4401002611000040110000400200000005d3ca0400000000000000001228f5041100004001d312005aa03e44010026110000401100004002000000d5f06e0000000000000000001228f5041100004001d312005aa03e44010026110000401100004002000000d5e46e0f00000000000000001228f5041100004001d312005aa03e4401002611000040110000400200000004afdf060000000003000000be0c7900110000400100000010923e44010026110000401100004002000000441c280500000000000000001228f5041100004001661200948b3e440100261100004011000040020000004bd1eb030000000000000000d5e46e0f1100004001000000337f3e44010026110000401100004002000000ee49980a0000000001000000846403001100004001f0120001793e4401002611000040110000400200000044565d0c0000000000000000d5e46e0f110000400166120001793e440100261100004011000040020000004ec4620d0000000000000000d5e46e0f110000400166120001793e4401002611000040110000400200000005d3ca040000000000000000d5e46e0f110000400166120001793e44010026110000401100004002000000c281520f0000000000000000d5e46e0f110000400166120001793e44010026110000401100004002000000df6f60080000000000000000d5e46e0f110000400166120001793e44010026110000401100004002000000d5e46e0f0000000000000000d5e46e0f110000400166120001793e4401002611000040110000400200000004afdf060000000003000000be0c79001100004001000000a26c3e4401002611000040110000400200000094304b0c000000000000000094304b0c1100004001661200a26c3e4401002611000040110000400200000004afdf060000000003000000be0c7900110000400100000000e03d440100261100004011000040020000004bd1eb030000000000000000d5e46e0f110000400100000079cb3d4401002611000040110000400200000044565d0c0000000000000000d5e46e0f110000400166120079cb3d440100261100004011000040020000004ec4620d0000000000000000d5e46e0f110000400166120079cb3d4401002611000040110000400200000005d3ca040000000000000000d5e46e0f110000400166120079cb3d440100261100004011000040020000001228f5040000000000000000d5e46e0f110000400166120079cb3d44010026110000401100004002000000458260080000000000000000d5e46e0f1100004001661200";




        internal override void ReadBody(ByteReader reader)
        {
            //  this.EventSourceType = reader.ReadByte();
            this.KillerNetID = reader.ReadUInt32();

            this.Othernetid = reader.ReadUInt32();
            this.DurationOpenedWindows = reader.ReadFloat();
            this.SenderNetID = reader.ReadUInt32();

            this.BytesLeft = reader.ReadBytes(4); // find what is this 
            int events = reader.ReadInt32();  //surely an bitshift 
            int count = events - 32;
            for (int i = 0; i < events; i++)
            {
                Entries.Add(reader.ReadEventHistoryEntry());
            }




          //  int killerEventSourceType = (int)((valeurEnti�re >> positionDuChamp) & mask);
        }
        internal override void WriteBody(ByteWriter writer)
        {

            Othernetid = SenderNetID;

            //   writer.WriteByte(EventSourceType);
            writer.WriteUInt32(KillerNetID);
           // writer.WriteUInt32(Othernetid);
            writer.WriteFloat(timetogetdied);

            writer.WritePad(4); //bitfield here ? 

            writer.WritePad(4); // buffersize ? 

            //  for (int i = Entries.Count - 1; i >= 0; i--)
            //  {
            //      var ev = Entries[i];
            //      writer.WriteEventHistoryEntry(ev);


            //normally here is the event 
             writer.WriteFloat(TimeStamp);
            writer.WriteUInt16(1); // normally is count here 
            
            writer.WriteByte(11); //0b is damagegiven event we try to emulate 
            writer.WriteUInt32(KillerNetID); // normally is source here 



            //  if (ev.Event.ID == EventID.OnDamageGiven)
            //  {
            writer.WriteUInt32(Othernetid);
            writer.WriteFloat(Damage);
                    writer.WriteUInt32(ParentScriptNameHash);
                    writer.WritePad(4); //??? 
                    writer.WriteByte(0); //typeofdamage
            writer.WriteUInt32(ParentScriptNameHash);
                    writer.WriteUInt32(ParentScriptNameHash);
                    writer.WriteUInt32(KillerNetID);
                    writer.WriteByte(0); // typeofdamageagain ? 
            if (typeofdamage == 0)
                    {
                        writer.WriteByte(48);
                        writer.WritePad(2);
                    }
                    if(typeofdamage == 4)
                     {
                         writer.WriteByte(113);
                        writer.WritePad(2);
                      }

            // }



            //}





            // �crire dans le champ de bits killerEventSourceType
            // �crire la valeur de killerEventSourceType en utilisant uniquement les 4 premiers bits de EventSourceType
            // writer.WriteBytes(byteArray);



            //writer.WriteUInt32(SenderNetID);
            //    writer.WritePad(8);
            //    writer.WritePad(1);
            //writer.WriteInt32(Entries.Count + 50);
            //   writer.WriteUInt32((uint)Entriescounthack);
            // writer.WritePad(3);
            //normally we check all entries , but eventhistory is not implemented , so we will bypass that 
            //foreach (var ev in Entries)
            /* for (int i = Entries.Count - 1; i >= 0; i--)
             {
                 var ev = Entries[i];
                 writer.WriteEventHistoryEntry(ev);
             } */


            //startevent
            //   writer.WritePad(4); //timestamp
            //   writer.WriteByte(1);
            //   writer.WriteByte((byte)eventid);
            //   writer.WritePad(3);
            //   writer.WriteUInt32(Othernetid);
            //   writer.WriteUInt32(KillerNetID);
            //   writer.WritePad(4);


            //   writer.WriteFloat(PhysicalDamage);
            //   writer.WriteFloat(MagicalDamage);
            //  writer.WriteFloat(TrueDamage);

            //  writer.WriteUInt32(ParentScriptNameHash);
            //  writer.WriteUInt32(ParentScriptNameHash);
            //  writer.WriteUInt32(ParentCasterNetID);

            //  writer.WritePad(4);

        }
    }
}