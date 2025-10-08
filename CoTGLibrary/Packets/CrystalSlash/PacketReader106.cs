using System;
using CoTGEnumNetwork;
using CoTGEnumNetwork.Enums;
using CoTGEnumNetwork.Packets.Enums;
using CoTGEnumNetwork.Packets.PacketDefinitions.Requests;
using CoTG.CoTGServer;
using CrystalSlash;
using CrystalSlash.Game;
using CrystalSlash.LoadScreen;
using static PacketDefinitions106.PacketExtensions106;

namespace PacketDefinitions106
{
    /// <summary>
    /// Class which contains all functions which are called when handling packets sent from clients to the server (C2S).
    /// </summary>
    /// TODO: Remove all CoTG based PacketCmd usage and replace with LeaguePackets' GamePacketID enum.
    public class PacketReader106
    {
        // this one only used in packet definitions, not exposed to the API currently, so no packet cmd assigned
        public static CrystalSlash.KeyCheckPacket ReadKeyCheckRequest(byte[] data)
        {
            var rq = new CrystalSlash.KeyCheckPacket();
            rq.Read(data);
            return rq;
        }

        [PacketType106(GamePacketID.C2S_SynchSimTime, Channel.CHL_GAMEPLAY)]
        public static SyncSimTimeRequest ReadSyncSimTimeRequest(byte[] data)
        {
            var rq = new C2S_SynchSimTime();
            rq.Read(data);
            return new SyncSimTimeRequest(rq.TimeLastServer, rq.TimeLastClient);
        }

        [PacketType106(GamePacketID.C2S_RemoveItemReq)]
        public static SellItemRequest ReadSellItemRequest(byte[] data)
        {
            var rq = new C2S_RemoveItemReq();
            rq.Read(data);
            return new SellItemRequest(rq.Slot, rq.Sell);
        }

        //UNKNOWN
        /*[PacketType(GamePacketID.BID_ResumePacket)]
        public static UnpauseRequest ReadUnpauseRequest(byte[] data)
        {
            var rq = new ResumePacket();
            rq.Read(data);
            return new UnpauseRequest(rq.ClientID, rq.Delayed);
        }*/

        [PacketType106(GamePacketID.C2S_QueryStatusReq)]
        public static QueryStatusRequest ReadQueryStatusRequest(byte[] data)
        {
            var rq = new C2S_QueryStatusReq();
            rq.Read(data);
            return new QueryStatusRequest();
        }

        [PacketType106(GamePacketID.C2S_Ping_Load_Info)]
        public static PingLoadInfoRequest ReadPingLoadInfoRequest(byte[] data)
        {
            var rq = new C2S_Ping_Load_Info();
            rq.Read(data);
            return new PingLoadInfoRequest((int)rq.ConnectionInfo.ClientID, (int)rq.ConnectionInfo.PlayerID, rq.ConnectionInfo.Percentage, rq.ConnectionInfo.ETA, (ushort)rq.ConnectionInfo.Count, rq.ConnectionInfo.Ping, rq.ConnectionInfo.Ready);
        }

        [PacketType106(GamePacketID.C2S_SwapItemReq)]
        public static SwapItemsRequest ReadSwapItemsRequest(byte[] data)
        {
            var rq = new C2S_SwapItemReq();
            rq.Read(data);
            return new SwapItemsRequest(rq.Source, rq.Destination);
        }

        [PacketType106(GamePacketID.C2S_World_SendCamera_Server)]
        public static ViewRequest ReadViewRequest(byte[] data)
        {
            var rq = new C2S_World_SendCamera_Server();
            rq.Read(data);
            return new ViewRequest(rq.CameraPosition, rq.CameraDirection, (int)rq.ClientID, (byte)rq.SyncID);
        }

        [PacketType106(GamePacketID.C2S_NPC_UpgradeSpellReq)]
        public static UpgradeSpellReq ReadUpgradeSpellReq(byte[] data)
        {
            var rq = new C2S_NPC_UpgradeSpellReq();
            rq.Read(data);
            return new UpgradeSpellReq(rq.Slot, true);
        }

        /* [PacketType106(GamePacketID.C2S_UseObject)]
         public static UseObjectRequest ReadUseObjectRequest(byte[] data)
         {
             var rq = new C2S_UseObject();
             rq.Read(data);
             return new UseObjectRequest(rq.TargetNetID);
         }
        */
        //UNKNOWN
        /*[PacketType(GamePacketID.C2S_UpdateGameOptions)]
        public static AutoAttackOptionRequest ReadAutoAttackOptionRequest(byte[] data)
        {
            var rq = new C2S_UpdateGameOptions();
            rq.Read(data);
            return new AutoAttackOptionRequest(rq.AutoAttackEnabled);
        }*/

        [PacketType106(GamePacketID.C2S_PlayEmote)]
        public static EmotionPacketRequest ReadEmotionPacketRequest(byte[] data)
        {
            var rq = new C2S_PlayEmote();
            rq.Read(data);
            return new EmotionPacketRequest((Emotions)rq.EmoteID);
        }

        [PacketType106(GamePacketID.C2S_ClientReady)]
        public static StartGameRequest ReadStartGameRequest(byte[] data)
        {
            var rq = new C2S_ClientReady();
            rq.Read(data);
            //CHECK
            return new StartGameRequest(0, 0, 0, 0);
        }

        /*  [PacketType106(GamePacketID.C2S_StatsUpdateReq)]
          public static ScoreboardRequest ReadScoreboardRequest(byte[] data)
          {
              var rq = new C2S_StatsUpdateReq();
              rq.Read(data);
              return new ScoreboardRequest();
          }
        */
        [PacketType106(GamePacketID.C2S_MapPing)]
        public static AttentionPingRequest ReadAttentionPingRequest(byte[] data)
        {
            var rq = new C2S_MapPing();
            rq.Read(data);
            return new AttentionPingRequest(rq.Position.ToVector2(), rq.TargetNetID, (Pings)rq.PingCategory);
        }

        [PacketType106(LoadScreenPacketID.RequestJoinTeam)]
        public static JoinTeamRequest ReadClientJoinTeamRequest(byte[] data)
        {
            var rq = new RequestJoinTeam();
            rq.Read(data);
            return new JoinTeamRequest((int)rq.ClientID, rq.TeamID);
        }


        [PacketType106(Channel.CHL_COMMUNICATION)]
        public static ChatMessageRequest ReadChatMessageRequest(byte[] data)
        {
            var rq = new ChatPacket();
            rq.Read(data);
            if (Game.Config.EnableLogPKT)
            {
                Console.WriteLine("test if packet is correctly readed ");
            }
            return new ChatMessageRequest(rq.Message, (ChatType)rq.ChatType, (int)rq.ClientID);
        }

        /*  [PacketType106(GamePacketID.C2S_OnTipEvent)]
          public static BlueTipClickedRequest ReadBlueTipRequest(byte[] data)
          {
              var rq = new C2S_OnTipEvent();
              rq.Read(data);
              return new BlueTipClickedRequest((TipCommand)rq.TipCommand, rq.TipID);
          }
        */
        [PacketType106(GamePacketID.C2S_NPC_IssueOrderReq)]
        public static MovementRequest ReadMovementRequest(byte[] data)
        {
            var rq = new C2S_NPC_IssueOrderReq();
            rq.Read(data);
            if (rq.MovementData.Waypoints == null)
            {
                return null;
            }
            return new MovementRequest((OrderType)rq.OrderType, rq.Position.ToVector2(), rq.TargetNetID, rq.MovementData.TeleportNetID, rq.MovementData.HasTeleportID, rq.MovementData.Waypoints.ConvertAll(WaypointToVector2));
        }

        [PacketType106(GamePacketID.C2S_Waypoint_Acc, Channel.CHL_S2C)]
        public static MoveConfirmRequest ReadMoveConfirmRequest(byte[] data)
        {
            var rq = new C2S_Waypoint_Acc();
            rq.Read(data);
            return new MoveConfirmRequest(rq.SyncID, rq.TeleportCount);
        }

        [PacketType106(GamePacketID.C2S_World_LockCamera_Server)]
        public static LockCameraRequest ReadCameraLockRequest(byte[] data)
        {
            var rq = new C2S_World_LockCamera_Server();
            rq.Read(data);
            return new LockCameraRequest(rq.Locked, (int)rq.ClientID);
        }

        [PacketType106(GamePacketID.C2S_BuyItemReq)]
        public static BuyItemRequest ReadBuyItemRequest(byte[] data)
        {
            var rq = new C2S_BuyItemReq();
            rq.Read(data);
            return new BuyItemRequest(rq.ItemID);
        }

        //Not present in .131
        /*[PacketType(GamePacketID.C2S_UndoItemReq)]
        public static UndoItemRequest ReadUndoItemRequest(byte[] data)
        {
            var rq = new C2S_UndoItemReq();
            rq.Read(data);
            return new UndoItemRequest();
        }*/

        [PacketType106(GamePacketID.C2S_Exit)]
        public static ExitRequest ReadExitRequest(byte[] data)
        {
            var rq = new C2S_Exit();
            rq.Read(data);
            return new ExitRequest();
        }

        [PacketType106(GamePacketID.C2S_NPC_CastSpellReq)]
        public static CastSpellRequest ReadCastSpellRequest(byte[] data)
        {
            var rq = new C2S_NPC_CastSpellReq();
            rq.Read(data);
            return new CastSpellRequest(rq.Slot, rq.IsSummonerSpellSlot, false, rq.Position.ToVector2(), rq.EndPosition.ToVector2(), rq.TargetNetID);
        }

        [PacketType106(GamePacketID.C2S_Reconnect)]
        public static SoftReconnectRequest ReadSoftReconnectRequest(byte[] data)
        {
            var rq = new C2S_Reconnect();
            rq.Read(data);
            return new SoftReconnectRequest();
        }

        [PacketType106(GamePacketID.BID_PausePacket)]
        public static PauseRequest ReadPauseGameRequest(byte[] data)
        {
            var rq = new BID_PausePacket();
            rq.Read(data);
            return new PauseRequest();
        }
        [PacketType106(GamePacketID.C2S_TeamSurrenderVote)]
        public static SurrenderRequest ReadSurrenderRequest(byte[] data)
        {
            var rq = new C2S_TeamSurrenderVote();
            rq.Read(data);
            return new SurrenderRequest(rq.VotedYes);
        }

        [PacketType106(GamePacketID.C2S_OnReplication_Acc)]
        public static ReplicationConfirmRequest ReadStatsConfirmRequest(byte[] data)
        {
            var rq = new C2S_OnReplication_Acc();
            rq.Read(data);
            return new ReplicationConfirmRequest((uint)Environment.TickCount);
        }

        [PacketType106(GamePacketID.C2S_SendSelectedObjID)]
        public static ClickRequest ReadClickRequest(byte[] data)
        {
            var rq = new C2S_SendSelectedObjID();
            rq.Read(data);
            return new ClickRequest(rq.SelectedNetID, (int)rq.ClientID);
        }

        [PacketType106(GamePacketID.C2S_SynchVersion)]
        public static SynchVersionRequest ReadSynchVersionRequest(byte[] data)
        {
            var rq = new C2S_SynchVersion();
            rq.Read(data);
            return new SynchVersionRequest((int)rq.ClientID, rq.VersionString);
        }

        [PacketType106(GamePacketID.C2S_CharSelected)]
        public static SpawnRequest ReadSpawn(byte[] data)
        {
            var rq = new C2S_CharSelected();
            rq.Read(data);
            return new SpawnRequest();
        }

        /* [PacketType106(GamePacketID.C2S_OnQuestEvent)]
         public static QuestClickedRequest ReadQuestClickedRequest(byte[] data)
         {
             var rq = new C2S_OnQuestEvent();
             rq.Read(data);
             return new QuestClickedRequest(rq.QuestID, (QuestEvent)rq.QuestEvent);
         }
        */
        //UNKNOWN
        /*[PacketType(GamePacketID.C2S_SpellChargeUpdateReq)]
        public static SpellChargeUpdateReq ReadSpellChargeUpdateReq(byte[] data)
        {
            var rq = new C2S_SpellChargeUpdateReq();
            rq.Read(data);
            return new SpellChargeUpdateReq(rq.Slot, rq.IsSummonerSpellBook, rq.Position, rq.ForceStop);
        }*/
    }
}
