using System;
using System.IO;
using Newtonsoft.Json.Linq;
using SiphoningStrike;
using BloodBoil;
using CrystalSlash;
using HeavenlyWave;
using TechmaturgicalRepairBot;
using System.Collections.Generic;


namespace MirrorImage
{

    public interface IBasePacket
    {
        
    }
    public abstract class BasePacket : IBasePacket
    {
        /// <summary>
        /// here is an string for choose version of packet 
        /// </summary>




        /// 
        ///
        /// 

       

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
          //  string filePath = @"..\ChildrenOfTheGraveServerConsole\bin\Debug\net9.0\Settings\GameInfo.json"; // Remplacez par le chemin de votre fichier JSON
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

        private static BasePacket Construct(byte[] data, ChannelID channel, string version)
        {
            if (version == null)
            {
                version = GetVersioning();
            }
            //Console.WriteLine(version);
            var CheckChannelAndVersion = (channel, version);

            if (data.Length == 0)
            {
                throw new IOException("Empty packet can not be packet!");
            }
            switch (CheckChannelAndVersion)
            {
                case (ChannelID.Default, "0.9.22.14"):
       
                    return new BloodBoil.KeyCheckPacket();

                case (ChannelID.Default, "1.0.0.106"):
                    return new CrystalSlash.KeyCheckPacket();

                case (ChannelID.Default, "1.0.0.124"):
                    return new HeavenlyWave.KeyCheckPacket();

                case (ChannelID.Default, "1.0.0.126"):
                    return new SiphoningStrike.KeyCheckPacket();

                case (ChannelID.Default, "1.0.0.131"):
                    return new SiphoningStrike.KeyCheckPacket();

                case (ChannelID.Default, "1.0.0.132"):
                    return new TechmaturgicalRepairBot.KeyCheckPacket();

                case (ChannelID.ClientToServer, "0.9.22.14"):
                case (ChannelID.SynchClock, "0.9.22.14"):
                case (ChannelID.Broadcast, "0.9.22.14"):
                case (ChannelID.BroadcastUnreliable, "0.9.22.14"):
                 
                    var BloodBoilGameID = (BloodBoil.GamePacketID)data[0];
                    if (BloodBoilGameID == BloodBoil.GamePacketID.Batch)
                    {
                        return new BloodBoil.BatchGamePacket();
                    }
                    if (BloodBoilGameID == BloodBoil.GamePacketID.ExtendedPacket)
                    {
                        if (data.Length < 7)
                        {
                            throw new IOException("Packet too small to be extended packet!");
                        }
                        BloodBoilGameID = (BloodBoil.GamePacketID)((ushort)(data[5]) | (ushort)(data[6] << 8));
                    }
                    if (BloodBoil.GamePacket.Lookup.ContainsKey(BloodBoilGameID))
                    {
                        return BloodBoil.GamePacket.Lookup[BloodBoilGameID]();
                    }
                    return new UnknownPacket();

                case (ChannelID.ClientToServer, "1.0.0.106"):
                case (ChannelID.SynchClock, "1.0.0.106"):
                case (ChannelID.Broadcast, "1.0.0.106"):
                case (ChannelID.BroadcastUnreliable, "1.0.0.106"):
                    var CrystalSlashGameID = (CrystalSlash.GamePacketID)data[0];
                    if (CrystalSlashGameID == CrystalSlash.GamePacketID.Batch)
                    {
                        return new CrystalSlash.BatchGamePacket();
                    }
                    if (CrystalSlashGameID == CrystalSlash.GamePacketID.ExtendedPacket)
                    {
                        if (data.Length < 7)
                        {
                            throw new IOException("Packet too small to be extended packet!");
                        }
                        CrystalSlashGameID = (CrystalSlash.GamePacketID)((ushort)(data[5]) | (ushort)(data[6] << 8));
                    }
                    if (CrystalSlash.GamePacket.Lookup.ContainsKey(CrystalSlashGameID))
                    {
                        return CrystalSlash.GamePacket.Lookup[CrystalSlashGameID]();
                    }
                    return new UnknownPacket();

                case (ChannelID.ClientToServer, "1.0.0.124"):
                case (ChannelID.SynchClock, "1.0.0.124"):
                case (ChannelID.Broadcast, "1.0.0.124"):
                case (ChannelID.BroadcastUnreliable, "1.0.0.124"):
                    var HeavenlyWaveGameID = (HeavenlyWave.GamePacketID)data[0];
                    if (HeavenlyWaveGameID == HeavenlyWave.GamePacketID.Batch)
                    {
                        return new HeavenlyWave.BatchGamePacket();
                    }
                    if (HeavenlyWaveGameID == HeavenlyWave.GamePacketID.ExtendedPacket)
                    {
                        if (data.Length < 7)
                        {
                            throw new IOException("Packet too small to be extended packet!");
                        }
                        HeavenlyWaveGameID = (HeavenlyWave.GamePacketID)((ushort)(data[5]) | (ushort)(data[6] << 8));
                    }
                    if (HeavenlyWave.GamePacket.Lookup.ContainsKey(HeavenlyWaveGameID))
                    {
                        return HeavenlyWave.GamePacket.Lookup[HeavenlyWaveGameID]();
                    }
                    return new UnknownPacket();

                case (ChannelID.ClientToServer, "1.0.0.126"):
                case (ChannelID.SynchClock, "1.0.0.126"):
                case (ChannelID.Broadcast, "1.0.0.126"):
                case (ChannelID.BroadcastUnreliable, "1.0.0.126"):
                case (ChannelID.ClientToServer, "1.0.0.131"):
                case (ChannelID.SynchClock, "1.0.0.131"):
                case (ChannelID.Broadcast, "1.0.0.131"):
                case (ChannelID.BroadcastUnreliable, "1.0.0.131"):
                    var SiphoningStrikeGameID = (SiphoningStrike.GamePacketID)data[0];
                    if (SiphoningStrikeGameID == SiphoningStrike.GamePacketID.Batch)
                    {
                        return new SiphoningStrike.BatchGamePacket();
                    }
                    if (SiphoningStrikeGameID == SiphoningStrike.GamePacketID.ExtendedPacket)
                    {
                        if (data.Length < 7)
                        {
                            throw new IOException("Packet too small to be extended packet!");
                        }
                        SiphoningStrikeGameID = (SiphoningStrike.GamePacketID)((ushort)(data[5]) | (ushort)(data[6] << 8));
                    }
                    if (SiphoningStrike.GamePacket.Lookup.ContainsKey(SiphoningStrikeGameID))
                    {
                        return SiphoningStrike.GamePacket.Lookup[SiphoningStrikeGameID]();
                    }
                    return new UnknownPacket();

                case (ChannelID.ClientToServer, "1.0.0.132"):
                case (ChannelID.SynchClock, "1.0.0.132"):
                case (ChannelID.Broadcast, "1.0.0.132"):
                case (ChannelID.BroadcastUnreliable, "1.0.0.132"):
                    var TechmaturgicalRepairBotGameID = (TechmaturgicalRepairBot.GamePacketID)data[0];
                    if (TechmaturgicalRepairBotGameID == TechmaturgicalRepairBot.GamePacketID.Batch)
                    {
                        return new TechmaturgicalRepairBot.BatchGamePacket();
                    }
                    if (TechmaturgicalRepairBotGameID == TechmaturgicalRepairBot.GamePacketID.ExtendedPacket)
                    {
                        if (data.Length < 7)
                        {
                            throw new IOException("Packet too small to be extended packet!");
                        }
                        TechmaturgicalRepairBotGameID = (TechmaturgicalRepairBot.GamePacketID)((ushort)(data[5]) | (ushort)(data[6] << 8));
                    }
                    if (TechmaturgicalRepairBot.GamePacket.Lookup.ContainsKey(TechmaturgicalRepairBotGameID))
                    {
                        return TechmaturgicalRepairBot.GamePacket.Lookup[TechmaturgicalRepairBotGameID]();
                    }
                    return new UnknownPacket();


                case (ChannelID.Chat, "0.9.22.14"):
                    return new BloodBoil.ChatPacket();
                case (ChannelID.Chat, "1.0.0.106"):
                    return new CrystalSlash.ChatPacket();
                case (ChannelID.Chat, "1.0.0.124"):
                    return new HeavenlyWave.ChatPacket();
                case (ChannelID.Chat, "1.0.0.126"):
                case (ChannelID.Chat, "1.0.0.131"):
                    return new SiphoningStrike.ChatPacket();
                case (ChannelID.Chat, "1.0.0.132"):
                    return new TechmaturgicalRepairBot.ChatPacket();


                case (ChannelID.LoadingScreen, "0.9.22.14"):
                 
                    var BloodBoilLoadScreenID = (BloodBoil.LoadScreenPacketID)data[0];
                    if (BloodBoil.LoadScreenPacket.Lookup.ContainsKey(BloodBoilLoadScreenID))
                    {
                        return BloodBoil.LoadScreenPacket.Lookup[BloodBoilLoadScreenID]();
                    }
                    return new UnknownPacket();

                case (ChannelID.LoadingScreen, "1.0.0.106"):
                    var CrystalSlashLoadScreenID = (CrystalSlash.LoadScreenPacketID)data[0];
                    if (CrystalSlash.LoadScreenPacket.Lookup.ContainsKey(CrystalSlashLoadScreenID))
                    {
                        return CrystalSlash.LoadScreenPacket.Lookup[CrystalSlashLoadScreenID]();
                    }
                    return new UnknownPacket();

                case (ChannelID.LoadingScreen, "1.0.0.124"):
                    var HeavenlyWaveLoadScreenID = (HeavenlyWave.LoadScreenPacketID)data[0];
                    if (HeavenlyWave.LoadScreenPacket.Lookup.ContainsKey(HeavenlyWaveLoadScreenID))
                    {
                        return HeavenlyWave.LoadScreenPacket.Lookup[HeavenlyWaveLoadScreenID]();
                    }
                    return new UnknownPacket();

                case (ChannelID.LoadingScreen, "1.0.0.126"):
                case (ChannelID.LoadingScreen, "1.0.0.131"):
                    var SiphoningStrikeLoadScreenID = (SiphoningStrike.LoadScreenPacketID)data[0];
                    if (SiphoningStrike.LoadScreenPacket.Lookup.ContainsKey(SiphoningStrikeLoadScreenID))
                    {
                        return SiphoningStrike.LoadScreenPacket.Lookup[SiphoningStrikeLoadScreenID]();
                    }
                    return new UnknownPacket();

                case (ChannelID.LoadingScreen, "1.0.0.132"):
                    var TechmaturgicalRepairBotLoadScreenID = (TechmaturgicalRepairBot.LoadScreenPacketID)data[0];
                    if (TechmaturgicalRepairBot.LoadScreenPacket.Lookup.ContainsKey(TechmaturgicalRepairBotLoadScreenID))
                    {
                        return TechmaturgicalRepairBot.LoadScreenPacket.Lookup[TechmaturgicalRepairBotLoadScreenID]();
                    }
                    return new UnknownPacket();
                default:
                    return new UnknownPacket();
            }
        }

        public static BasePacket Create(byte[] data, ChannelID channel, string version = "1.0.0.126")
        {
            var result = Construct(data, channel, version);
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
