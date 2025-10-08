using System;
using System.IO;
using Newtonsoft.Json.Linq;
using SiphoningStrike;
using System.Collections.Generic;


namespace MirrorImage
{

    public interface IBasePacket
    {
        
    }
    public abstract class BasePacket : IBasePacket
    {
        public byte[] BytesLeft { get; set; } = new byte[0];

        internal BasePacket() { }

        internal abstract void ReadPacket(ByteReader reader);

        public void Read(byte[] data)
        {
            var reader = new ByteReader(data);
            this.ReadPacket(reader);
            this.BytesLeft = reader.ReadBytesLeft();
        }

       public static string GetVersioning()
        {
             string filePath = @".\Settings\GameInfo.json";
            if (!File.Exists(filePath))
            {
                string ClientVersion = "1.0.0.126";
                Console.WriteLine("filenotfound default version = 1.0.0.126");
                return ClientVersion;
            }
            else{
                string jsonContent = File.ReadAllText(filePath);

                JObject jsonObject = JObject.Parse(jsonContent);


                string ClientVersion = jsonObject["gameInfo"]["CLIENT_VERSION"].Value<string>();


                return ClientVersion;
            }
            
        }

        private static BasePacket Construct(byte[] data, ChannelID channel)
        {
            if (data.Length == 0)
            {
                throw new IOException("Empty packet can not be packet!");
            }
            switch (channel)
            {
                case ChannelID.Default:
                    return new KeyCheckPacket();

                case ChannelID.ClientToServer:
                case ChannelID.SynchClock:
                case ChannelID.Broadcast:
                case ChannelID.BroadcastUnreliable:
                    var SiphoningStrikeGameID = (GamePacketID)data[0];
                    if (SiphoningStrikeGameID == GamePacketID.Batch)
                    {
                        return new BatchGamePacket();
                    }
                    if (SiphoningStrikeGameID == GamePacketID.ExtendedPacket)
                    {
                        if (data.Length < 7)
                        {
                            throw new IOException("Packet too small to be extended packet!");
                        }
                        SiphoningStrikeGameID = (GamePacketID)((ushort)(data[5]) | (ushort)(data[6] << 8));
                    }
                    if (GamePacket.Lookup.ContainsKey(SiphoningStrikeGameID))
                    {
                        return GamePacket.Lookup[SiphoningStrikeGameID]();
                    }
                    return new UnknownPacket();
              
                case ChannelID.Chat:
                    return new ChatPacket();

                case ChannelID.LoadingScreen:
                    var SiphoningStrikeLoadScreenID = (LoadScreenPacketID)data[0];
                    if (LoadScreenPacket.Lookup.ContainsKey(SiphoningStrikeLoadScreenID))
                    {
                        return LoadScreenPacket.Lookup[SiphoningStrikeLoadScreenID]();
                    }
                    return new UnknownPacket();
                default:
                    return new UnknownPacket();
            }
        }

        public static BasePacket Create(byte[] data, ChannelID channel)
        {
            var result = Construct(data, channel);
            result.Read(data);
            return result;
        }

        internal abstract void WritePacket(ByteWriter writer);

        public byte[] GetBytes()
        {
            var writer = new ByteWriter();
            this.WritePacket(writer);
            writer.WriteBytes(this.BytesLeft);
            return writer.GetBytes();
        }
    }
}
