using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.PacketDefinitions;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using LENet;
using log4net;
using SiphoningStrike;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Channel = ChildrenOfTheGraveEnumNetwork.Packets.Enums.Channel;
using SiphoningStrike.Game.Events;
using SiphoningStrike.Game;
using System.Linq;
using System.Numerics;
using MirrorImage;
using BasePacket = MirrorImage.BasePacket;
using ChildrenOfTheGraveLibrary.Packets.Common;
using static PacketVersioning.PktVersioning;

namespace PacketDefinitions126
{
    /// <summary>
    /// Class containing all functions related to sending and receiving packets.
    /// TODO: refactor this class (may be able to replace it with LeaguePackets' implementation), get rid of IGame, use generic API requests+responses also for disconnect and unpause
    /// </summary>
    public class PacketHandlerManager126
    {
        // Logger
        private static ILog Logger = LoggerProvider.GetLogger();

        private delegate ICoreRequest RequestConvertor(byte[] data);
        private readonly Dictionary<Tuple<GamePacketID, Channel>, RequestConvertor> GameConvertorTable;
        private readonly Dictionary<LoadScreenPacketID, RequestConvertor> LoadScreenConvertorTable;
        private readonly List<RequestConvertor> ChatConvertorTable;
        // should be one-to-one, no two users for the same Peer
        private readonly Peer[] Peers;
        private readonly BlowFish[] Blowfishes;
        private readonly Host Server;

        public PacketHandlerManager126(BlowFish[] blowfishes, Host server)
        {
            Blowfishes = blowfishes;
            Server = server;
            Peers = new Peer[blowfishes.Length];
            GameConvertorTable = new Dictionary<Tuple<GamePacketID, Channel>, RequestConvertor>();
            LoadScreenConvertorTable = new Dictionary<LoadScreenPacketID, RequestConvertor>();
            ChatConvertorTable = new List<RequestConvertor>();
            InitializePacketConvertors();
        }

        private static void Log(string str, object packet)
        {

            if (Game.Config.EnableLogPKT)
            {
                if (packet.GetType().Name == "S2C_WaypointGroup")
                {
                    var newpacket = packet as S2C_WaypointGroup;


                    Logger.Debug(str + " " + packet.GetType().Name + " " + Newtonsoft.Json.JsonConvert.SerializeObject(packet, Newtonsoft.Json.Formatting.Indented));
                    foreach (var wp in newpacket.Movements.First().Waypoints)
                    {
                        Logger.Debug(" x = " + ((new Vector2(wp.X, wp.Y) * 2) + new Vector2(6991.434f, 7223.343f)).X + " Y :" + ((new Vector2(wp.X, wp.Y) * 2) + new Vector2(6991.434f, 7223.343f)).Y);
                    }
                }
                else if (packet.GetType().Name != "S2C_OnReplication")
                {
                    //Console.WriteLine(str + " " + packet.GetType().Name + " " + Newtonsoft.Json.JsonConvert.SerializeObject(packet, Newtonsoft.Json.Formatting.Indented));
                    Logger.Debug(str + " " + packet.GetType().Name + " " + Newtonsoft.Json.JsonConvert.SerializeObject(packet, Newtonsoft.Json.Formatting.Indented));
                }
            }
        }

        internal void InitializePacketConvertors()
        {
            foreach (var m in typeof(PacketReader126).GetMethods())
            {
                foreach (Attribute attr in m.GetCustomAttributes(true))
                {
                    if (attr is PacketType126)
                    {
                        if (((PacketType126)attr).ChannelId == Channel.CHL_LOADING_SCREEN)
                        {

                            var method = (RequestConvertor)Delegate.CreateDelegate(typeof(RequestConvertor), m);

                            LoadScreenConvertorTable.Add(((PacketType126)attr).LoadScreenPacketId, method);
                        }
                        if (((PacketType126)attr).ChannelId == Channel.CHL_COMMUNICATION)
                        {
                            var method = (RequestConvertor)Delegate.CreateDelegate(typeof(RequestConvertor), m);
                            ChatConvertorTable.Add(method);
                        }
                        else
                        {
                            var key = new Tuple<GamePacketID, Channel>(((PacketType126)attr).GamePacketId, ((PacketType126)attr).ChannelId);
                            var method = (RequestConvertor)Delegate.CreateDelegate(typeof(RequestConvertor), m);
                            GameConvertorTable.Add(key, method);
                        }
                    }
                }
            }
        }

        private RequestConvertor GetConvertor(LoadScreenPacketID packetId)
        {
            var packetsHandledWhilePaused = new List<LoadScreenPacketID>
            {
                LoadScreenPacketID.RequestJoinTeam,
                //LoadScreenPacketID.Chat CHECK
            };

            if (Game.StateHandler.State == GameState.PAUSE && !packetsHandledWhilePaused.Contains(packetId))
            {
                return null;
            }

            if (LoadScreenConvertorTable.ContainsKey(packetId))
            {
                return LoadScreenConvertorTable[packetId];
            }

            return null;
        }

        private RequestConvertor GetConvertor(GamePacketID packetId, Channel channelId)
        {
            var packetsHandledWhilePaused = new List<GamePacketID>
            {
                GamePacketID.BID_Dummy,
                GamePacketID.C2S_SynchSimTime,
                GamePacketID.BID_ResumePacket,
                GamePacketID.C2S_QueryStatusReq,
                GamePacketID.C2S_ClientReady,
                GamePacketID.C2S_Exit,
                GamePacketID.S2C_World_SendGameNumber,
                GamePacketID.C2S_SendSelectedObjID,
                GamePacketID.C2S_CharSelected,
                // The next two are required to reconnect 
                GamePacketID.C2S_SynchVersion,
                GamePacketID.C2S_Ping_Load_Info,
                // The next 5 are not really needed when reconnecting,
                // but they don't do much harm either
                //GamePacketID.C2S_UpdateGameOptions,
                GamePacketID.C2S_OnReplication_Acc,
                GamePacketID.C2S_StatsUpdateReq,
                GamePacketID.C2S_World_SendCamera_Server,
                GamePacketID.C2S_OnTipEvent,
#if DEBUG_AB || RELEASE_AB
                GamePacketID.UNK_Cheat
#endif
            };
            if (Game.StateHandler.State is GameState.PAUSE && !packetsHandledWhilePaused.Contains(packetId))
            {
                return null;
            }
            var key = new Tuple<GamePacketID, Channel>(packetId, channelId);
            if (GameConvertorTable.ContainsKey(key))
            {
                return GameConvertorTable[key];
            }

            return null;
        }

        private void PrintPacket(byte[] buffer, string str)
        {
            // FIXME: currently lock disabled, not needed?
            if (Game.Config.EnableLogPKT)
            {
                Console.Write(str);
                string emptystring = string.Empty;
                foreach (var b in buffer)
                {
                    emptystring = emptystring + b.ToString("X2") + " ";
                }
                Logger.Debug(emptystring);
                Console.WriteLine("");
                Console.WriteLine("--------");
            }
        }
        public static bool alreadysended = false;
        public bool SendPacket(int userId, BasePacket packet, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            if (Game.Config.EnableLogPKT)
            {
                Log("SEND", packet);
                string emptystring = "SEND ";
                foreach (byte b in packet.GetBytes())
                {
                    //  //  Console.Write($"{b:X2} "); // Affiche chaque byte en hexadécimal avec deux chiffres.
                    emptystring = emptystring + $"{b:X2}" + "";
                }
                Logger.Debug(emptystring);
            }

            return SendPacket(userId, packet.GetBytes(), channelNo, flag, packet);




        }
        //hack 
        public static List<uint> alreadywrited = new();
        public bool SendPacket(int userId, byte[] source, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE, BasePacket packetid = default, bool iscreatehero = false)
        {

            if (Game.Config.EnableReplay)
            {
                List<byte> packet = new();


                int timestamp = 0;

                if (channelNo == Channel.CHL_LOADING_SCREEN && userId == 1)
                {

                    byte[] timestampBytes = BitConverter.GetBytes(timestamp);
                    packet.AddRange(timestampBytes);
                    packet.Add((byte)channelNo);
                    int sizeofpacket = source.Count(); //int?
                    byte[] sizebitted = BitConverter.GetBytes(sizeofpacket);
                    packet.AddRange(sizebitted);
                    packet.AddRange(source);

                    WritePacketToBinary(packet.ToArray(), Game.nameofreplay);
                }
                else
                {
                    if (channelNo == Channel.CHL_HANDSHAKE && userId == 1)
                    {
                        byte[] timestampBytes = BitConverter.GetBytes(timestamp);
                        packet.AddRange(timestampBytes);
                        packet.Add((byte)channelNo);
                        //hack for write the actionid 
                        packet.Add(0x18);
                        packet.Add(0x00);
                        packet.Add(0x00);
                        packet.Add(0x00);
                        packet.AddRange(source);
                        WritePacketToBinary(packet.ToArray(), Game.nameofreplay);
                    }
                    else
                    {

                        float timestamp2 = Game.Time.GameTime / 1000.0f;
                        byte[] timestampBytes = BitConverter.GetBytes(timestamp2);
                        packet.AddRange(timestampBytes);
                        packet.Add((byte)channelNo);
                        uint sizeofpacket = (uint)source.Count(); //uint ? 
                        byte[] sizebitted = BitConverter.GetBytes(sizeofpacket);
                        packet.AddRange(sizebitted);
                        packet.AddRange(source);


                        if (packetid is not S2C_CreateHero)
                        {
                            WritePacketToBinary(packet.ToArray(), Game.nameofreplay);

                        }

                        else
                        {
                            if (!alreadywrited.Contains((packetid as S2C_CreateHero).ClientID))
                            {
                                alreadywrited.Add((packetid as S2C_CreateHero).ClientID);
                                WritePacketToBinary(packet.ToArray(), Game.nameofreplay);
                            }

                        }



                    }

                }
            }




            // Sometimes we try to send packets to a user that doesn't exist (like in broadcast when not all players are connected).
            if (userId >= 0 && userId < Peers.Length && Peers[userId] != null)
            {
                byte[] temp;
                if (source.Length >= 8)
                {
                    // _peers.Length == _blowfishes.Length
                    temp = Blowfishes[userId].Encrypt(source);
                }
                else
                {
                    temp = source;
                }

                return Peers[userId].Send((byte)channelNo, new Packet(temp, flag)) == 0;
            }
            return false;
        }
        static void WritePacketToBinary(byte[] packetData, string fileName)
        {
            if (!(Game.StateHandler.State == GameState.EXIT || Game.StateHandler.State == GameState.PRE_EXIT || Game.StateHandler.State == GameState.ENDGAME))
            {
                using (FileStream fileStream = new(fileName, FileMode.Append, FileAccess.Write))
                {
                    fileStream.Seek(0, SeekOrigin.End); // Se déplace à la fin du fichier
                    fileStream.Write(packetData, 0, packetData.Length);
                }
            }
            // Écriture des données du paquet à la fin du fichier binaire

        }
        public bool BroadcastPacket(BasePacket packet, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            if (Game.Config.EnableLogPKT)
            {
                Log("BROADCAST", packet);
                //string emptystring = "BROADCAST ";
                //foreach (byte b in packet.GetBytes())
                //{
                // //   Console.Write($"{b:X2} "); // Affiche chaque byte en hexadécimal avec deux chiffres.
                //    emptystring = (emptystring + $"{b:X2} " + " ");

                //}
                //Logger.Debug(emptystring);
            }
            return BroadcastPacket(packet.GetBytes(), channelNo, flag);
        }

        public bool BroadcastPacket(byte[] data, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            if (Game.Config.EnableReplay)
            {
                List<byte> packetlist = new();
                int timestamp = 0;
                if (channelNo == Channel.CHL_LOADING_SCREEN)
                {
                    byte[] timestampBytes = BitConverter.GetBytes(timestamp);
                    packetlist.AddRange(timestampBytes);
                    packetlist.Add((byte)channelNo);
                    int sizeofpacket = data.Count(); //int?
                    byte[] sizebitted = BitConverter.GetBytes(sizeofpacket);
                    packetlist.AddRange(sizebitted);
                    packetlist.AddRange(data);
                    WritePacketToBinary(packetlist.ToArray(), Game.nameofreplay);
                }
                else
                {
                    if (channelNo == Channel.CHL_HANDSHAKE)
                    {
                        byte[] timestampBytes = BitConverter.GetBytes(timestamp);
                        packetlist.AddRange(timestampBytes);
                        packetlist.Add((byte)channelNo);
                        //hack for write the actionid 
                        packetlist.Add(0x18);
                        packetlist.Add(0x00);
                        packetlist.Add(0x00);
                        packetlist.Add(0x00);
                        packetlist.AddRange(data);
                        WritePacketToBinary(packetlist.ToArray(), Game.nameofreplay);
                    }
                    else
                    {

                        float timestamp2 = Game.Time.GameTime / 1000.0f;
                        byte[] timestampBytes = BitConverter.GetBytes(timestamp2);
                        packetlist.AddRange(timestampBytes);
                        packetlist.Add((byte)channelNo);
                        uint sizeofpacket = (uint)data.Count(); //uint ? 
                        byte[] sizebitted = BitConverter.GetBytes(sizeofpacket);
                        packetlist.AddRange(sizebitted);
                        packetlist.AddRange(data);
                        WritePacketToBinary(packetlist.ToArray(), Game.nameofreplay);
                    }
                }
            }
            if (data.Length >= 8)
            {
                // send packet to all peers and save failed ones
                int failedPeers = 0;
                for (int i = 0; i < Peers.Length; i++)
                {
                    if (Peers[i] != null && Peers[i].Send((byte)channelNo, new Packet(Blowfishes[i].Encrypt(data), flag)) < 0)
                    {
                        failedPeers++;
                    }
                }

                if (failedPeers > 0)
                {
                    Debug.WriteLine($"Broadcasting packet failed for {failedPeers} peers.");
                    return false;
                }
                return true;
            }
            else
            {
                var packet = new Packet(data, flag);
                Server.Broadcast((byte)channelNo, packet);
                //this is an test to force send all broadcast before do something

                return true;
            }
        }

        public bool BroadcastPacketTeam(TeamId team, BasePacket packet, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            Log("BROADCAST TEAM", packet);
            return BroadcastPacketTeam(team, packet.GetBytes(), channelNo, flag);
        }

        // TODO: find a way with no need of player manager
        public bool BroadcastPacketTeam(TeamId team, byte[] data, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            foreach (var ci in Game.PlayerManager.GetPlayers(false))
            {
                if (ci.Team == team)
                {
                    SendPacket(ci.ClientId, data, channelNo, flag);
                }
            }

            return true;
        }

        public bool BroadcastPacketVision(GameObject o, MirrorImage.BasePacket packet, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            if (Game.Config.EnableLogPKT)
            {
                Log("BROADCAST VISION", packet);
                //foreach (byte b in packet.GetBytes())
                //{
                //    Console.Write($"{b:X2} "); // Affiche chaque byte en hexadécimal avec deux chiffres.
                //}

            }
            return BroadcastPacketVision(o, packet.GetBytes(), channelNo, flag);
        }
        public bool BroadcastPacketVision(GameObject o, byte[] data, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            foreach (int pid in o.VisibleForPlayers)
            {

                SendPacket(pid, data, channelNo, flag);
            }
            return true;
        }

        public bool HandlePacket(Peer peer, byte[] data, Channel channelId)
        {




            try
            {
                var packet = BasePacket.Create(data, (ChannelID)channelId, Game.Config.VersionOfClient);
                int clientId = (int)peer.UserData;
                Game.RequestHandler.OnMessage(clientId, packet);
                return true;
            }
            catch (NotImplementedException e)
            {
                Logger.Error("Handle Packet Error", e);
                return false;
            }
            catch (IOException e)
            {
                Logger.Error("Handle Packet Error", e);
                return false;
            }
        }

        public bool HandleDisconnect(Peer peer)
        {
            if (peer == null || peer.UserData == null)
            {
                // Didn't receive an ID by initiating a handshake.
                return true;
            }
            int clientId = (int)peer.UserData;
            return HandleDisconnect(clientId);
        }

        public bool HandleDisconnect(int clientId)
        {
            var peerInfo = Game.PlayerManager.GetPeerInfo(clientId);
            if (peerInfo.IsDisconnected)
            {
                Debug.WriteLine($"Prevented double disconnect of {peerInfo.PlayerId}");
                return true;
            }

            Debug.WriteLine($"Player {peerInfo.PlayerId} disconnected!");


            var annoucement = new OnLeave { OtherNetID = peerInfo.Champion.NetId };
            OnEventWorldNotify(annoucement, peerInfo.Champion);
            peerInfo.IsDisconnected = true;
            peerInfo.IsStartedClient = false;
            Peers[clientId] = null;

            return Game.PlayerManager.CheckIfAllPlayersLeft() || peerInfo.Champion.OnDisconnect();
        }

        public bool HandlePacket(Peer peer, Packet packet, Channel channelId)
        {
            var data = packet.Data;

            // if channel id is HANDSHAKE we should initialize blowfish key and return
            if (channelId == Channel.CHL_HANDSHAKE)
            {
                return HandleHandshake(peer, data);
            }

            // every packet that is not blowfish go here
            if (data.Length >= 8)
            {
                //TODO: An unhandled exception of type 'System.NullReferenceException' occurred
                if (peer.UserData == null)
                {
#if DEBUG
                    if (Blowfishes.Length == 1)
                    {
                        HandleReconnect(peer, 0);
                    }
                    else
#endif
                        return false; // Unauthorized peer sends garbage
                }
                int clientId = (int)peer.UserData;
                data = Blowfishes[clientId].Decrypt(data);
            }

            return HandlePacket(peer, data, channelId);
        }

#if DEBUG
        private bool HandleReconnect(Peer peer, int userId)
        {
            var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
            peerInfo.IsStartedClient = true;
            peerInfo.IsDisconnected = false;
            peer.UserData = peerInfo.ClientId;
            Peers[peerInfo.ClientId] = peer;
            return true;
        }
#endif

        private bool HandleHandshake(Peer peer, byte[] data)
        {
            var request = new KeyCheckPacket(data);
            Log("HANDSHAKE", request);

            var peerInfo = Game.PlayerManager.GetClientInfoByPlayerId((long)request.PlayerID);
            if (peerInfo == null)
            {
                Debug.WriteLine($"Player ID {request.PlayerID} is invalid.");
                return false;
            }

            if (Blowfishes[peerInfo.ClientId].Encrypt(request.PlayerID) != request.EncryptedPlayerID) // in 106 blowfish is different ? 
            {
                Debug.WriteLine($"Blowfish key is wrong!");


                // return false;
            }
            //  peerInfo.ClientId = Game.PlayerManager.ClientIdIndex++;
            if (Peers[peerInfo.ClientId] != null && !peerInfo.IsDisconnected)
            {
                //Debug.WriteLine($"Player {request.PlayerID} is already connected. Request from {peer.Address.IPEndPoint.Address.ToString()}.");
                Debug.WriteLine($"Player {request.PlayerID} is reconnecting.");
                Peers[peerInfo.ClientId].DisconnectNow(0); //TODO: Verify
                HandleDisconnect(peerInfo.ClientId);
                //return false;
            }

            peerInfo.IsStartedClient = true;

            Debug.WriteLine("Connected client No " + peerInfo.ClientId);


            peer.UserData = peerInfo.ClientId;
            Peers[peerInfo.ClientId] = peer;


            // on re-insigne les bon clientid au bon playerid  
            foreach (var player in Game.PlayerManager.GetPlayers(false))
            {

                if (player.PlayerId == peerInfo.PlayerId)
                {
                    player.ClientId = peerInfo.ClientId;
                }
            }

            bool result = true;
            // inform players about their player numbers
            foreach (var player in Game.PlayerManager.GetPlayers(false))
            {




                var response = new KeyCheckPacket
                {
                    ClientID = (uint)player.ClientId,
                    PlayerID = (ulong)player.PlayerId,
                    EncryptedPlayerID = Blowfishes[peerInfo.ClientId].Encrypt(request.PlayerID)
                };
                result = result && SendPacket(peerInfo.ClientId, response, Channel.CHL_HANDSHAKE);
            }

            // only if all packets were sent successfully return true
            return result;
        }


        public static ulong Encrypt126(State state, ulong block)
        {
            uint lo = (uint)(block & 0xFFFFFFFF);
            uint hi = (uint)(block >> 32);

            lo ^= state.pikey[0];
            hi ^= Feistel(state, lo) ^ state.pikey[1];
            lo ^= Feistel(state, hi) ^ state.pikey[2];
            hi ^= Feistel(state, lo) ^ state.pikey[3];
            lo ^= Feistel(state, hi) ^ state.pikey[4];
            hi ^= Feistel(state, lo) ^ state.pikey[5];
            lo ^= Feistel(state, hi) ^ state.pikey[6];
            hi ^= Feistel(state, lo) ^ state.pikey[7];
            lo ^= Feistel(state, hi) ^ state.pikey[8];
            hi ^= Feistel(state, lo) ^ state.pikey[9];
            lo ^= Feistel(state, hi) ^ state.pikey[10];
            hi ^= Feistel(state, lo) ^ state.pikey[11];
            lo ^= Feistel(state, hi) ^ state.pikey[12];
            hi ^= Feistel(state, lo) ^ state.pikey[13];
            lo ^= Feistel(state, hi) ^ state.pikey[14];
            hi ^= Feistel(state, lo) ^ state.pikey[15];
            lo ^= Feistel(state, hi) ^ state.pikey[16];
            hi ^= state.pikey[17];

            return ((ulong)hi << 32) | lo;
        }
        public static ulong Decrypt126(State state, ulong block)
        {
            uint lo = (uint)(block & 0xFFFFFFFF);       // Lower 32 bits
            uint hi = (uint)(block >> 32);              // Upper 32 bits

            lo ^= state.pikey[17];
            hi ^= Feistel(state, lo) ^ state.pikey[16];
            lo ^= Feistel(state, hi) ^ state.pikey[15];
            hi ^= Feistel(state, lo) ^ state.pikey[14];
            lo ^= Feistel(state, hi) ^ state.pikey[13];
            hi ^= Feistel(state, lo) ^ state.pikey[12];
            lo ^= Feistel(state, hi) ^ state.pikey[11];
            hi ^= Feistel(state, lo) ^ state.pikey[10];
            lo ^= Feistel(state, hi) ^ state.pikey[9];
            hi ^= Feistel(state, lo) ^ state.pikey[8];
            lo ^= Feistel(state, hi) ^ state.pikey[7];
            hi ^= Feistel(state, lo) ^ state.pikey[6];
            lo ^= Feistel(state, hi) ^ state.pikey[5];
            hi ^= Feistel(state, lo) ^ state.pikey[4];
            lo ^= Feistel(state, hi) ^ state.pikey[3];
            hi ^= Feistel(state, lo) ^ state.pikey[2];
            lo ^= Feistel(state, hi) ^ state.pikey[1];
            hi ^= state.pikey[0];

            return ((ulong)hi << 32) | lo;
        }
        public static uint Feistel(State state, uint value)
        {
            uint byte3 = (value >> 24) & 0xFF;  // Get the most significant byte (byte 3)
            uint byte2 = (value >> 16) & 0xFF;  // Get the second most significant byte (byte 2)
            uint byte1 = (value >> 8) & 0xFF;   // Get the second least significant byte (byte 1)
            uint byte0 = value & 0xFF;          // Get the least significant byte (byte 0)

            uint h = state.sboxes[0][byte3] + state.sboxes[1][byte2];
            uint result = (h ^ state.sboxes[2][byte1]) + state.sboxes[3][byte0];

            return result;
        }

        public class State
        {
            public uint[] pikey; // Assuming pikey is an array of uints
            public uint[][] sboxes;  // Assuming sboxes is a 2D array of uints
        }
    }

}
