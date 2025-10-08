using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;
namespace

 BloodBoil.LoadScreen
{
    //supposition 

    /* in 2008/2009 an exploit has founded who permit play 12 v 12 
    pretty sure league can "accept" until 24 player by team 
    so this packet per default have 24 player id */ 
    public class TeamRosterUpdate : LoadScreenPacket
    {
        private ulong[] _orderPlayerIDs = new ulong[24];
        private ulong[] _chaosPlayerIDs = new ulong[24];

        public override LoadScreenPacketID ID => LoadScreenPacketID.TeamRosterUpdate;

        public byte[] buffer = new byte[3];
        public uint TeamSizeOrder { get; set; }
        public uint TeamSizeChaos { get; set; }

        public byte[] buffer2 = new byte[4];
        public ulong[] OrderPlayerIDs => _orderPlayerIDs;
        public ulong[] ChaosPlayerIDs => _chaosPlayerIDs;
        public uint CurrentTeamSizeOrder { get; set; }
        public uint CurrentTeamSizeChaos { get; set; }

        internal override void ReadBody(ByteReader reader)
        {
            this.buffer = reader.ReadBytes(3);

            this.TeamSizeOrder = reader.ReadUInt32();
            this.TeamSizeChaos = reader.ReadUInt32();
            this.buffer2 = reader.ReadBytes(4);
            for (var i = 0; i < OrderPlayerIDs.Length; i++)
            {
                this.OrderPlayerIDs[i] = reader.ReadUInt64();
            }
            for (var i = 0; i < ChaosPlayerIDs.Length; i++)
            {
                this.ChaosPlayerIDs[i] = reader.ReadUInt64();
            }
            this.CurrentTeamSizeOrder = reader.ReadUInt32();
            this.CurrentTeamSizeChaos = reader.ReadUInt32();
        }

        internal override void WriteBody(ByteWriter writer)
        {
            writer.WritePad(3);

            writer.WriteUInt32(this.TeamSizeOrder);
            writer.WriteUInt32(this.TeamSizeChaos);
            writer.WritePad(4);
            for (var i = 0; i < OrderPlayerIDs.Length; i++)
            {
                writer.WriteUInt64(this.OrderPlayerIDs[i]);
            }
            for (var i = 0; i < ChaosPlayerIDs.Length; i++)
            {
                writer.WriteUInt64(this.ChaosPlayerIDs[i]);
            }
            writer.WriteUInt32(this.CurrentTeamSizeOrder);
            writer.WriteUInt32(this.CurrentTeamSizeChaos);
        }
    }
}