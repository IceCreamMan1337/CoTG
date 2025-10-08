using System.Collections.Generic;

using PacketDefinitions126;
using PacketDefinitions106;
using CoTGEnumNetwork.Packets.Handlers;
using MirrorImage;
using CoTG.CoTGServer;
using CoTGLibrary.Packets.PacketHandlers.SiphoningStrike;
using CoTG.CoTGServer.Packets.PacketHandlers;


using Siph = SiphoningStrike;
using Crys = CrystalSlash;
using CoTGLibrary.Packets.PacketHandlers.CrystalSlash;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using System.Numerics;
using CoTGEnumNetwork.NetInfo;
using CoTGEnumNetwork.Packets.Enums;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.Inventory;
using CoTG.CoTGServer.GameObjects.StatsNS;
using CoTG.CoTGServer.GameObjects.SpellNS.Missile;
using CoTG.CoTGServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;



//this one is for switch in function of the version of client 
namespace PacketVersioning
{
    public class PktVersioning
    {
        /// <summary>
        /// Time since the game has started. Mostly used for networking to sync up players with the server.
        /// </summary>

        // Networking Vars

        private static PacketServer126 PacketServer126 { get; set; }

        private static PacketServer106 PacketServer106 { get; set; }

        /// <summary>
        /// Handler for response packets sent by the server to game clients.
        /// </summary>
        // private static NetworkHandler<ICoreRequest> ResponseHandler { get; set; }

        /// <summary>
        /// Interface containing all function related packets (except handshake) which are sent by the server to game clients.
        /// </summary>
        internal static PacketNotifier126 PacketNotifier126 { get; private set; }

        internal static PacketNotifier106 PacketNotifier106 { get; private set; }



        public void NetLoop(uint timeout = 0)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketServer106.NetLoop(timeout); break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketServer126.NetLoop(timeout); break;
                default:
                    PacketServer126.NetLoop(timeout); break;
            }

        }

        public static void SetupVersioningServer(ushort port, string[] blowfishKeys)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketServer106 = new PacketServer106(port, blowfishKeys);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketServer126 = new PacketServer126(port, blowfishKeys);
                    break;
                default:
                    PacketServer126 = new PacketServer126(port, blowfishKeys);
                    break;
            }
        }

        public static void InitializePacketHandlers(NetworkHandler<MirrorImage.BasePacket> RequestHandler)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    InitializePacketHandlers106(RequestHandler);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    InitializePacketHandlers126(RequestHandler);
                    break;
                default:
                    InitializePacketHandlers126(RequestHandler);
                    break;
            }
        }


        public static void InitializePacketHandlers106(NetworkHandler<MirrorImage.BasePacket> RequestHandler)
        {
            // maybe use reflection, the problem is that Register is generic and so it needs to know its type at
            // compile time, maybe just use interface and in runetime figure out the type - and again there is
            // a problem with passing generic delegate to non-generic function, if we try to only constraint the
            // argument to interface ICoreRequest we will get an error cause our generic handlers use generic type
            // even with where statement that doesn't work

            RequestHandler.Register<Crys.Game.C2S_BuyItemReq>(new Handle_C2S_BuyItemReq_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_CharSelected>(new Handle_C2S_CharSelected_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_ClientReady>(new Handle_C2S_ClientReady_106().HandlePacket);

            RequestHandler.Register<Crys.Game.C2S_Exit>(new Handle_C2S_Exit_106(PacketServer106.PacketHandlerManager).HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_MapPing>(new Handle_C2S_MapPing_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_NPC_CastSpellReq>(new Handle_C2S_NPC_CastSpellReq_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_NPC_IssueOrderReq>(new Handle_C2S_NPC_IssueOrderReq_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_NPC_UpgradeSpellReq>(new Handle_C2S_NPC_UpgradeSpellReq_106().HandlePacket);
            //  RequestHandler.Register<Crys.Game.C2S_OnQuestEvent>(new Handle_C2S_OnQuestEvent().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_OnReplication_Acc>(new Handle_C2S_OnReplication_Acc_106().HandlePacket);
            // RequestHandler.Register<Crys.Game.C2S_OnScoreBoardOpened>(new Handle_C2S_OnScoreBoardOpened().HandlePacket);
            //  RequestHandler.Register<Crys.Game.C2S_OnTipEvent>(new Handle_C2S_OnTipEvent().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_Ping_Load_Info>(new Handle_C2S_Ping_Load_Info_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_PlayEmote>(new Handle_C2S_PlayEmote_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_QueryStatusReq>(new Handle_C2S_QueryStatusReq_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_Reconnect>(new Handle_C2S_Reconnect_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_RemoveItemReq>(new Handle_C2S_RemoveItemReq_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_SwapItemReq>(new Handle_C2S_SwapItemReq_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_SynchSimTime>(new Handle_C2S_SynchSimTime_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_SynchVersion>(new Handle_C2S_SynchVersion_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_TeamSurrenderVote>(new Handle_C2S_TeamSurrenderVote_106().HandlePacket);
            // RequestHandler.Register<Crys.Game.C2S_UseObject>(new Handle_C2S_UseObject().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_Waypoint_Acc>(new Handle_C2S_Waypoint_Acc_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_World_LockCamera_Server>(new Handle_C2S_World_LockCamera_Server_106().HandlePacket);
            RequestHandler.Register<Crys.Game.C2S_World_SendCamera_Server>(new Handle_C2S_World_SendCamera_Server_106().HandlePacket);
            RequestHandler.Register<Crys.ChatPacket>(new Handle_ChatPacket_106().HandlePacket);
            RequestHandler.Register<Crys.LoadScreen.RequestJoinTeam>(new Handle_RequestJoinTeam_106().HandlePacket);
        }

        public static void InitializePacketHandlers126(NetworkHandler<MirrorImage.BasePacket> RequestHandler)
        {
            // maybe use reflection, the problem is that Register is generic and so it needs to know its type at
            // compile time, maybe just use interface and in runetime figure out the type - and again there is
            // a problem with passing generic delegate to non-generic function, if we try to only constraint the
            // argument to interface ICoreRequest we will get an error cause our generic handlers use generic type
            // even with where statement that doesn't work

            RequestHandler.Register<Siph.Game.C2S_BuyItemReq>(new Handle_C2S_BuyItemReq().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_CharSelected>(new Handle_C2S_CharSelected().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_ClientReady>(new Handle_C2S_ClientReady().HandlePacket);

            RequestHandler.Register<Siph.Game.C2S_Exit>(new Handle_C2S_Exit(PacketServer126.PacketHandlerManager).HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_MapPing>(new Handle_C2S_MapPing().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_NPC_CastSpellReq>(new Handle_C2S_NPC_CastSpellReq().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_NPC_IssueOrderReq>(new Handle_C2S_NPC_IssueOrderReq().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_NPC_UpgradeSpellReq>(new Handle_C2S_NPC_UpgradeSpellReq().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_OnQuestEvent>(new Handle_C2S_OnQuestEvent().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_OnReplication_Acc>(new Handle_C2S_OnReplication_Acc().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_OnScoreBoardOpened>(new Handle_C2S_OnScoreBoardOpened().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_OnTipEvent>(new Handle_C2S_OnTipEvent().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_Ping_Load_Info>(new Handle_C2S_Ping_Load_Info().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_PlayEmote>(new Handle_C2S_PlayEmote().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_QueryStatusReq>(new Handle_C2S_QueryStatusReq().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_Reconnect>(new Handle_C2S_Reconnect().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_RemoveItemReq>(new Handle_C2S_RemoveItemReq().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_SwapItemReq>(new Handle_C2S_SwapItemReq().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_SynchSimTime>(new Handle_C2S_SynchSimTime().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_SynchVersion>(new Handle_C2S_SynchVersion().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_TeamSurrenderVote>(new Handle_C2S_TeamSurrenderVote().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_UseObject>(new Handle_C2S_UseObject().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_Waypoint_Acc>(new Handle_C2S_Waypoint_Acc().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_World_LockCamera_Server>(new Handle_C2S_World_LockCamera_Server().HandlePacket);
            RequestHandler.Register<Siph.Game.C2S_World_SendCamera_Server>(new Handle_C2S_World_SendCamera_Server().HandlePacket);

#if DEBUG_AB || RELEASE_AB
                
            RequestHandler.Register<Siph.Game.UNK_Cheat>(new Handle_UNK_Cheat().HandlePacket);
#endif

            RequestHandler.Register<Siph.ChatPacket>(new Handle_ChatPacket().HandlePacket);
            RequestHandler.Register<Siph.LoadScreen.RequestJoinTeam>(new Handle_RequestJoinTeam().HandlePacket);
        }

        public static void InitializePacketNotifier()
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106 = new PacketNotifier106(PacketServer106.PacketHandlerManager, Game.Map.NavigationGrid);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126 = new PacketNotifier126(PacketServer126.PacketHandlerManager, Game.Map.NavigationGrid);
                    break;
                default:
                    PacketNotifier126 = new PacketNotifier126(PacketServer126.PacketHandlerManager, Game.Map.NavigationGrid);
                    break;
            }
        }










        //find better way to do the rest

        public static void CreateAnimationdata(LevelProp prop, string animation, AnimationFlags animationFlag, float duration, bool destroyPropAfterAnimation)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    var animationData = new Crys.Game.Common.UpdateLevelPropDataPlayAnimation
                    {
                        AnimationName = animation,
                        AnimationFlags = (uint)animationFlag,
                        Duration = duration,
                        DestroyPropAfterAnimation = destroyPropAfterAnimation,
                        StartMissionTime = Game.Time.GameTime,
                        NetID = prop.NetId
                    };





                    PacketNotifier106.NotifyUpdateLevelPropS2C(animationData);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    var animationData2 = new Siph.Game.Common.UpdateLevelPropDataPlayAnimation
                    {
                        AnimationName = animation,
                        AnimationFlags = (uint)animationFlag,
                        Duration = duration,
                        DestroyPropAfterAnimation = destroyPropAfterAnimation,
                        StartMissionTime = Game.Time.GameTime,
                        NetID = prop.NetId
                    };



                    PacketNotifier126.NotifyUpdateLevelPropS2C(animationData2);
                    break;
                default:
                    var animationData3 = new Siph.Game.Common.UpdateLevelPropDataPlayAnimation
                    {
                        AnimationName = animation,
                        AnimationFlags = (uint)animationFlag,
                        Duration = duration,
                        DestroyPropAfterAnimation = destroyPropAfterAnimation,
                        StartMissionTime = Game.Time.GameTime,
                        NetID = prop.NetId
                    };



                    PacketNotifier126.NotifyUpdateLevelPropS2C(animationData3);
                    break;
            }
        }


        public static void InstantStopAttackNotifier(AttackableUnit attacker, bool isSummonerSpell,
            bool keepAnimating = false,
            bool destroyMissile = true,
            bool overrideVisibility = true,
            bool forceClient = false,
            uint missileNetID = 0)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyNPC_InstantStop_Attack(attacker, isSummonerSpell, keepAnimating, destroyMissile, overrideVisibility, forceClient, missileNetID);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyNPC_InstantStop_Attack(attacker, isSummonerSpell, keepAnimating, destroyMissile, overrideVisibility, forceClient, missileNetID);
                    break;
                default:
                    PacketNotifier126.NotifyNPC_InstantStop_Attack(attacker, isSummonerSpell, keepAnimating, destroyMissile, overrideVisibility, forceClient, missileNetID);
                    break;
            }
        }


        public static void ChangeSlotSpellDataNotifier(int userId, ObjAIBase owner, int slot, ChangeSlotSpellDataType changeType, bool isSummonerSpell = false, TargetingType targetingType = TargetingType.Invalid, string newName = "", float newRange = 0, float newMaxCastRange = 0, float newDisplayRange = 0, int newIconIndex = 0x0, List<uint> offsetTargets = null)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyChangeSlotSpellData(userId, owner, slot, changeType, slot is 4 or 5, targetingType, newName, newRange, newMaxCastRange, newDisplayRange, newIconIndex, offsetTargets);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyChangeSlotSpellData(userId, owner, slot, changeType, slot is 4 or 5, targetingType, newName, newRange, newMaxCastRange, newDisplayRange, newIconIndex, offsetTargets);
                    break;
                default:
                    PacketNotifier126.NotifyChangeSlotSpellData(userId, owner, slot, changeType, slot is 4 or 5, targetingType, newName, newRange, newMaxCastRange, newDisplayRange, newIconIndex, offsetTargets);
                    break;
            }
        }

        public static void SetSpellDataNotifier(int userId, uint netId, string spellName, int slot)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_SetSpellData(userId, netId, spellName, slot);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_SetSpellData(userId, netId, spellName, slot);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_SetSpellData(userId, netId, spellName, slot);
                    break;
            }
        }

        public static void SetSpellLevelNotifier(int userId, uint netId, int slot, int level)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_SetSpellLevel(userId, netId, slot, level);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_SetSpellLevel(userId, netId, slot, level);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_SetSpellLevel(userId, netId, slot, level);
                    break;
            }
        }

        public static void TargetHeroS2CNotifier(ObjAIBase attacker, AttackableUnit? target)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyAI_TargetHeroS2C(attacker, target);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyAI_TargetHeroS2C(attacker, target);
                    break;
                default:
                    PacketNotifier126.NotifyAI_TargetHeroS2C(attacker, target);
                    break;
            }
        }

        public static void TargetS2CNotifier(ObjAIBase attacker, AttackableUnit? target)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyAI_TargetS2C(attacker, target);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyAI_TargetS2C(attacker, target);
                    break;
                default:
                    PacketNotifier126.NotifyAI_TargetS2C(attacker, target);
                    break;
            }
        }

        public static void UnitSetLookAtNotifier(AttackableUnit attacker, LookAtType lookAtType, AttackableUnit? target, Vector3 targetPosition = default)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_UnitSetLookAt(attacker, lookAtType, target, targetPosition);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_UnitSetLookAt(attacker, lookAtType, target, targetPosition);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_UnitSetLookAt(attacker, lookAtType, target, targetPosition);
                    break;
            }
        }

        public static void S2C_AIStateNotify(ObjAIBase owner, AIState state)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_AIState(owner, state);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_AIState(owner, state);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_AIState(owner, state);
                    break;
            }
        }

        public static void ChangeCharacterDataNotifier(ObjAIBase obj, string skinName, bool modelOnly = true, bool overrideSpells = false, bool replaceCharacterPackage = false, int countchangemodel = 0)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_ChangeCharacterData(obj, skinName, modelOnly, overrideSpells, replaceCharacterPackage, countchangemodel);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_ChangeCharacterData(obj, skinName, modelOnly, overrideSpells, replaceCharacterPackage, countchangemodel);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_ChangeCharacterData(obj, skinName, modelOnly, overrideSpells, replaceCharacterPackage, countchangemodel);
                    break;
            }
        }
        public static void PopCharacterDataNotifier(ObjAIBase obj, int countchangemodel = 0)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_PopCharacterData(obj, countchangemodel);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_PopCharacterData(obj, countchangemodel);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_PopCharacterData(obj, countchangemodel);
                    break;
            }
        }


        public static void GameStartNotify(int userId)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyGameStart(userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyGameStart(userId);
                    break;
                default:
                    PacketNotifier126.NotifyGameStart(userId);
                    break;
            }
        }

        public static void StartSpawnNotify(int userId, TeamId team, List<ClientInfo> players)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_StartSpawn(userId, team, players);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_StartSpawn(userId, team, players);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_StartSpawn(userId, team, players);
                    break;
            }
        }

        public static void SpawnEndNotify(int userId)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySpawnEnd(userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySpawnEnd(userId);
                    break;
                default:
                    PacketNotifier126.NotifySpawnEnd(userId);
                    break;
            }
        }

        public static void OnEventWorldNotify(object mapEvent, AttackableUnit? source = null)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_OnEventWorld(mapEvent as Crys.Game.Events.IEvent, source);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_OnEventWorld(mapEvent as Siph.Game.Events.IEvent, source);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_OnEventWorld(mapEvent as Siph.Game.Events.IEvent, source);
                    break;
            }
        }



        public static void SynchSimTimeNotify(float GameTime)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySynchSimTimeS2C(GameTime);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySynchSimTimeS2C(GameTime);
                    break;
                default:
                    PacketNotifier126.NotifySynchSimTimeS2C(GameTime);
                    break;
            }
        }

        public static void SynchSimTimeNotify2(int userId, float GameTime)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySynchSimTimeS2C(userId, GameTime);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySynchSimTimeS2C(userId, GameTime);
                    break;
                default:
                    PacketNotifier126.NotifySynchSimTimeS2C(userId, GameTime);
                    break;
            }
        }

        public static void OnEventNotify(object mapEvent, AttackableUnit? source = null)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    // PacketNotifier106.NotifyOnEvent((mapEvent as Crys.Game.Events.IEvent), source);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyOnEvent(mapEvent as Siph.Game.Events.IEvent, source);
                    break;
                default:
                    PacketNotifier126.NotifyOnEvent(mapEvent as Siph.Game.Events.IEvent, source);
                    break;
            }
        }

        public static void OnMapPingNotify(Vector2 pos, Pings type, uint targetNetId = 0, ClientInfo client = null)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_MapPing(pos, type, targetNetId, client);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_MapPing(pos, type, targetNetId, client);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_MapPing(pos, type, targetNetId, client);
                    break;
            }
        }

        public static void GameScoreNotify(TeamId team, int score)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_HandleGameScore(team, score);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_HandleGameScore(team, score);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_HandleGameScore(team, score);
                    break;
            }
        }

        public static void HandleCapturePointUpdateNotify(int capturePointIndex, uint otherNetId, int PARType, int attackTeam, CapturePointUpdateCommand capturePointUpdateCommand)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_HandleCapturePointUpdate(capturePointIndex, otherNetId, PARType, attackTeam, capturePointUpdateCommand);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_HandleCapturePointUpdate(capturePointIndex, otherNetId, PARType, attackTeam, capturePointUpdateCommand);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_HandleCapturePointUpdate(capturePointIndex, otherNetId, PARType, attackTeam, capturePointUpdateCommand);
                    break;
            }
        }

        public static void CameraBehaviorNotify(Champion target, Vector3 position)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_CameraBehavior(target, position);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_CameraBehavior(target, position);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_CameraBehavior(target, position);
                    break;
            }
        }

        public static void OnReplicationNotify()
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyOnReplication();
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyOnReplication();
                    break;
                default:
                    PacketNotifier126.NotifyOnReplication();
                    break;
            }
        }

        public static void CreateUnitHighlightNotify(int userId, GameObject unit)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyCreateUnitHighlight(userId, unit);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyCreateUnitHighlight(userId, unit);
                    break;
                default:
                    PacketNotifier126.NotifyCreateUnitHighlight(userId, unit);
                    break;
            }
        }

        public static void RemoveUnitHighlightNotify(int userId, GameObject unit)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyRemoveUnitHighlight(userId, unit);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyRemoveUnitHighlight(userId, unit);
                    break;
                default:
                    PacketNotifier126.NotifyRemoveUnitHighlight(userId, unit);
                    break;
            }
        }

        public static void EndGameNotify(TeamId winningTeam)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_EndGame(winningTeam);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_EndGame(winningTeam);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_EndGame(winningTeam);
                    break;
            }
        }

        public static void SetGreyscaleEnabledWhenDeadNotify(ClientInfo client, bool enabled)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_SetGreyscaleEnabledWhenDead(client, enabled);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_SetGreyscaleEnabledWhenDead(client, enabled);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_SetGreyscaleEnabledWhenDead(client, enabled);
                    break;
            }
        }

        public static void MoveCameraToPointNotify(ClientInfo player, Vector3 startPosition, Vector3 endPosition, float travelTime = 0, bool startFromCurretPosition = true, bool unlockCamera = false)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_MoveCameraToPoint(player, startPosition, endPosition, travelTime, startFromCurretPosition, unlockCamera);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_MoveCameraToPoint(player, startPosition, endPosition, travelTime, startFromCurretPosition, unlockCamera);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_MoveCameraToPoint(player, startPosition, endPosition, travelTime, startFromCurretPosition, unlockCamera);
                    break;
            }
        }

        public static void DisableHUDForEndOfGameNotify()
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_DisableHUDForEndOfGame();
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_DisableHUDForEndOfGame();
                    break;
                default:
                    PacketNotifier126.NotifyS2C_DisableHUDForEndOfGame();
                    break;
            }
        }

        public static void TintNotify(TeamId team, bool enable, float speed, float maxWeight, CoTGEnumNetwork.Content.Color color)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyTint(team, enable, speed, maxWeight, color);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyTint(team, enable, speed, maxWeight, color);
                    break;
                default:
                    PacketNotifier126.NotifyTint(team, enable, speed, maxWeight, color);
                    break;
            }
        }

        public static void ShowHealthBarNotify(AttackableUnit unit, bool show, TeamId observerTeamId = TeamId.TEAM_UNKNOWN, bool changeHealthBarType = false, HealthBarType healthBarType = HealthBarType.Invalid)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_ShowHealthBar(unit, show, observerTeamId, changeHealthBarType, healthBarType);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_ShowHealthBar(unit, show, observerTeamId, changeHealthBarType, healthBarType);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_ShowHealthBar(unit, show, observerTeamId, changeHealthBarType, healthBarType);
                    break;
            }
        }

        public static void ModifyDebugCircleRadiusNotify(int userId, uint sender, int objID, float newRadius)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyModifyDebugCircleRadius(userId, sender, objID, newRadius);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyModifyDebugCircleRadius(userId, sender, objID, newRadius);
                    break;
                default:
                    PacketNotifier126.NotifyModifyDebugCircleRadius(userId, sender, objID, newRadius);
                    break;
            }
        }

        public static void SetCircularMovementRestrictionNotify(ObjAIBase unit, Vector3 center, float radius, bool restrictcam = false)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySetCircularMovementRestriction(unit, center, radius, restrictcam);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySetCircularMovementRestriction(unit, center, radius, restrictcam);
                    break;
                default:
                    PacketNotifier126.NotifySetCircularMovementRestriction(unit, center, radius, restrictcam);
                    break;
            }
        }

        public static void DisplayFloatingTextNotify(FloatingTextData floatTextData, TeamId team = 0, int userId = -1)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyDisplayFloatingText(floatTextData, team, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyDisplayFloatingText(floatTextData, team, userId);
                    break;
                default:
                    PacketNotifier126.NotifyDisplayFloatingText(floatTextData, team, userId);
                    break;
            }
        }


        public static void AttachCapturePointToIdleParticlesNotify(ObjAIBase unit, byte _ParticleFlexID, byte _CpIndex, uint _ParticleAttachType)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyAttachFlexParticle(unit, _ParticleFlexID, _CpIndex, _ParticleAttachType);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyAttachFlexParticle(unit, _ParticleFlexID, _CpIndex, _ParticleAttachType);
                    break;
                default:
                    PacketNotifier126.NotifyAttachFlexParticle(unit, _ParticleFlexID, _CpIndex, _ParticleAttachType);
                    break;
            }
        }

        public static void InteractiveMusicCommandNotify(ObjAIBase unit, byte _MusicCommand, uint _MusicEventAudioEventID, uint _MusicParamAudioEventID)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.S2C_InteractiveMusicCommand(unit, _MusicCommand, _MusicEventAudioEventID, _MusicParamAudioEventID);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.S2C_InteractiveMusicCommand(unit, _MusicCommand, _MusicEventAudioEventID, _MusicParamAudioEventID);
                    break;
                default:
                    PacketNotifier126.S2C_InteractiveMusicCommand(unit, _MusicCommand, _MusicEventAudioEventID, _MusicParamAudioEventID);
                    break;
            }
        }


        public static void LoadScreenInfoNotify(int userId, TeamId team, List<ClientInfo> players)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyLoadScreenInfo(userId, team, players);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyLoadScreenInfo(userId, team, players);
                    break;
                default:
                    PacketNotifier126.NotifyLoadScreenInfo(userId, team, players);
                    break;
            }
        }

        public static void RequestRenameNotify(int userId, ClientInfo player)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyRequestRename(userId, player);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyRequestRename(userId, player);
                    break;
                default:
                    PacketNotifier126.NotifyRequestRename(userId, player);
                    break;
            }
        }

        public static void RequestReskinNotify(int userId, ClientInfo player)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyRequestReskin(userId, player);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyRequestReskin(userId, player);
                    break;
                default:
                    PacketNotifier126.NotifyRequestReskin(userId, player);
                    break;
            }
        }

        public static void KeyCheckNotify(int clientID, long playerId, long _EncryptedPlayerID, bool broadcast = false)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyKeyCheck(clientID, playerId, _EncryptedPlayerID, broadcast);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyKeyCheck(clientID, playerId, _EncryptedPlayerID, broadcast);
                    break;
                default:
                    PacketNotifier126.NotifyKeyCheck(clientID, playerId, _EncryptedPlayerID, broadcast);
                    break;
            }
        }

        public static void WorldSendGameNumberNotify()
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyWorldSendGameNumber();
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyWorldSendGameNumber();
                    break;
                default:
                    PacketNotifier126.NotifyWorldSendGameNumber();
                    break;
            }
        }
        public static void SynchVersionNotify(int userId, TeamId team, List<ClientInfo> players, string version, string gameMode, GameFeatures gameFeatures, int mapId, string[] mutators)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySynchVersion(userId, team, players, version, gameMode, gameFeatures, mapId, mutators);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySynchVersion(userId, team, players, version, gameMode, gameFeatures, mapId, mutators);
                    break;
                default:
                    PacketNotifier126.NotifySynchVersion(userId, team, players, version, gameMode, gameFeatures, mapId, mutators);
                    break;
            }
        }
        public static void SwapItemsNotify(Champion c, int sourceSlot, int destinationSlot)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySwapItemAns(c, sourceSlot, destinationSlot);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySwapItemAns(c, sourceSlot, destinationSlot);
                    break;
                default:
                    PacketNotifier126.NotifySwapItemAns(c, sourceSlot, destinationSlot);
                    break;
            }
        }

        public static void SwapItemsNotify(ClientInfo clientInfo, int userId, TeamId team, bool doVision)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_CreateHero(clientInfo, userId, team, doVision);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_CreateHero(clientInfo, userId, team, doVision);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_CreateHero(clientInfo, userId, team, doVision);
                    break;
            }
        }

        public static void AvatarInfoNotify(ClientInfo client, int userId = -1)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyAvatarInfo(client, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyAvatarInfo(client, userId);
                    break;
                default:
                    PacketNotifier126.NotifyAvatarInfo(client, userId);
                    break;
            }
        }

        public static void BuyItemNotify(ObjAIBase gameObject, Item itemInstance)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyBuyItem(gameObject, itemInstance);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyBuyItem(gameObject, itemInstance);
                    break;
                default:
                    PacketNotifier126.NotifyBuyItem(gameObject, itemInstance);
                    break;
            }
        }

        public static void UseItemAnsNotify(ObjAIBase unit, byte slot, int itemsInSlot, int spellCharges = 0)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyUseItemAns(unit, slot, itemsInSlot, spellCharges);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyUseItemAns(unit, slot, itemsInSlot, spellCharges);
                    break;
                default:
                    PacketNotifier126.NotifyUseItemAns(unit, slot, itemsInSlot, spellCharges);
                    break;
            }
        }

        public static void RemoveItemNotify(ObjAIBase ai, int slot, int remaining)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyRemoveItem(ai, slot, remaining);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyRemoveItem(ai, slot, remaining);
                    break;
                default:
                    PacketNotifier126.NotifyRemoveItem(ai, slot, remaining);
                    break;
            }
        }

        public static void CHAR_SetCooldownNotify(ObjAIBase u, int slotId, float currentCd, float totalCd, int userId = -1, bool issummonerspell = false)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyCHAR_SetCooldown(u, slotId, currentCd, totalCd, userId, issummonerspell);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyCHAR_SetCooldown(u, slotId, currentCd, totalCd, userId, issummonerspell);
                    break;
                default:
                    PacketNotifier126.NotifyCHAR_SetCooldown(u, slotId, currentCd, totalCd, userId, issummonerspell);
                    break;
            }
        }

        public static void NPC_UpgradeSpellAnsNotify(int userId, uint netId, int slot, int level, int points)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyNPC_UpgradeSpellAns(userId, netId, slot, level, points);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyNPC_UpgradeSpellAns(userId, netId, slot, level, points);
                    break;
                default:
                    PacketNotifier126.NotifyNPC_UpgradeSpellAns(userId, netId, slot, level, points);
                    break;
            }
        }

        public static void ToolTipVarsNotify(List<ToolTipData> data)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_ToolTipVars(data);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_ToolTipVars(data);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_ToolTipVars(data);
                    break;
            }
        }

        public static void HeroReincarnateAliveNotify(Champion c, float parToRestore)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyHeroReincarnateAlive(c, parToRestore);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyHeroReincarnateAlive(c, parToRestore);
                    break;
                default:
                    PacketNotifier126.NotifyHeroReincarnateAlive(c, parToRestore);
                    break;
            }
        }

        public static void NPC_ForceDeadNotify(DeathData lastDeathData)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyNPC_ForceDead(lastDeathData);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyNPC_ForceDead(lastDeathData);
                    break;
                default:
                    PacketNotifier126.NotifyNPC_ForceDead(lastDeathData);
                    break;
            }
        }

        public static void NPC_Hero_DieNotify(DeathData deathData)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyNPC_Hero_Die(deathData);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyNPC_Hero_Die(deathData);
                    break;
                default:
                    PacketNotifier126.NotifyNPC_Hero_Die(deathData);
                    break;
            }
        }

        public static void TintPlayerNotify(Champion champ, bool enable, float speed, CoTGEnumNetwork.Content.Color color)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyTintPlayer(champ, enable, speed, color);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyTintPlayer(champ, enable, speed, color);
                    break;
                default:
                    PacketNotifier126.NotifyTintPlayer(champ, enable, speed, color);
                    break;
            }
        }


        public static void S2C_IncrementPlayerScoreNotify(ScoreData scoreData)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_IncrementPlayerScore(scoreData);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_IncrementPlayerScore(scoreData);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_IncrementPlayerScore(scoreData);
                    break;
            }
        }

        public static void MinionSpawnedNotify(Minion minion, int userId, TeamId team, bool doVision)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyMinionSpawned(minion, userId, team, doVision);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyMinionSpawned(minion, userId, team, doVision);
                    break;
                default:
                    PacketNotifier126.NotifyMinionSpawned(minion, userId, team, doVision);
                    break;
            }
        }

        public static void NPC_LevelUpNotify(ObjAIBase obj)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyNPC_LevelUp(obj);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyNPC_LevelUp(obj);
                    break;
                default:
                    PacketNotifier126.NotifyNPC_LevelUp(obj);
                    break;
            }
        }

        public static void OnReplicationUnitNotify(AttackableUnit u, int userId = -1, bool partial = true)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyOnReplication(u, userId, partial);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyOnReplication(u, userId, partial);
                    break;
                default:
                    PacketNotifier126.NotifyOnReplication(u, userId, partial);
                    break;
            }
        }





        public static void LaneMinionSpawnedNotify(LaneMinion m, int userId, bool doVision)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyLaneMinionSpawned(m, userId, doVision);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyLaneMinionSpawned(m, userId, doVision);
                    break;
                default:
                    PacketNotifier126.NotifyLaneMinionSpawned(m, userId, doVision);
                    break;
            }
        }

        public static void CreateNeutralNotify(NeutralMinion monster, float time, int userId, TeamId team, bool doVision)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyCreateNeutral(monster, time, userId, team, doVision);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyCreateNeutral(monster, time, userId, team, doVision);
                    break;
                default:
                    PacketNotifier126.NotifyCreateNeutral(monster, time, userId, team, doVision);
                    break;
            }
        }

        public static void ModifyShieldNotify(AttackableUnit unit, bool physical, bool magical, float amount, bool stopShieldFade = false)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyModifyShield(unit, physical, magical, amount);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyModifyShield(unit, physical, magical, amount);
                    break;
                default:
                    PacketNotifier126.NotifyModifyShield(unit, physical, magical, amount);
                    break;
            }
        }

        public static void WriteNavFlagsNotify(Vector2 position, float radius, NavigationGridCellFlags flags)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.Notify_WriteNavFlags(position, radius, flags);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.Notify_WriteNavFlags(position, radius, flags);
                    break;
                default:
                    PacketNotifier126.Notify_WriteNavFlags(position, radius, flags);
                    break;
            }
        }

        public static void EnterTeamVisionNotify(AttackableUnit u, int userId = -1, BasePacket? spawnPacket = null)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyEnterTeamVision(u, userId, spawnPacket);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyEnterTeamVision(u, userId, spawnPacket);
                    break;
                default:
                    PacketNotifier126.NotifyEnterTeamVision(u, userId, spawnPacket);
                    break;
            }
        }

        public static void EnterVisibilityClientNotify(GameObject o, int userId = -1, bool ignoreVision = false, List<BasePacket> packets = null)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyEnterVisibilityClient(o, userId, ignoreVision, packets);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyEnterVisibilityClient(o, userId, ignoreVision, packets);
                    break;
                default:
                    PacketNotifier126.NotifyEnterVisibilityClient(o, userId, ignoreVision, packets);
                    break;
            }
        }

        public static void OnEnterTeamVisibilityNotify(GameObject o, TeamId team, int userId)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyOnEnterTeamVisibility(o, team, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyOnEnterTeamVisibility(o, team, userId);
                    break;
                default:
                    PacketNotifier126.NotifyOnEnterTeamVisibility(o, team, userId);
                    break;
            }
        }

        public static void OnLeaveTeamVisibilityNotify(GameObject o, TeamId team, int userId = -1)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyOnLeaveTeamVisibility(o, team, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyOnLeaveTeamVisibility(o, team, userId);
                    break;
                default:
                    PacketNotifier126.NotifyOnLeaveTeamVisibility(o, team, userId);
                    break;
            }
        }

        public static void LeaveVisibilityClientNotify(GameObject o, TeamId team, int userId = -1)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyLeaveVisibilityClient(o, team, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyLeaveVisibilityClient(o, team, userId);
                    break;
                default:
                    PacketNotifier126.NotifyLeaveVisibilityClient(o, team, userId);
                    break;
            }
        }

        public static void HoldReplicationDataUntilOnReplicationNotify(AttackableUnit u, int userId, bool partial = true)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.HoldReplicationDataUntilOnReplicationNotification(u, userId, partial);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.HoldReplicationDataUntilOnReplicationNotification(u, userId, partial);
                    break;
                default:
                    PacketNotifier126.HoldReplicationDataUntilOnReplicationNotification(u, userId, partial);
                    break;
            }
        }
        public static void HoldMovementDataUntilWaypointGroupNotify(AttackableUnit u, int userId, bool partial = true)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.HoldMovementDataUntilWaypointGroupNotification(u, userId, partial);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.HoldMovementDataUntilWaypointGroupNotification(u, userId, partial);
                    break;
                default:
                    PacketNotifier126.HoldMovementDataUntilWaypointGroupNotification(u, userId, partial);
                    break;
            }
        }

        public static void UnitApplyDamageNotify(DamageData damageData, bool isGlobal = true)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyUnitApplyDamage(damageData, isGlobal);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyUnitApplyDamage(damageData, isGlobal);
                    break;
                default:
                    PacketNotifier126.NotifyUnitApplyDamage(damageData, isGlobal);
                    break;
            }
        }

        public static void S2C_NPC_Die_MapViewNotify(DeathData data)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_NPC_Die_MapView(data);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_NPC_Die_MapView(data);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_NPC_Die_MapView(data);
                    break;
            }
        }

        public static void DeathNotify(DeathData data)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyDeath(data);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyDeath(data);
                    break;
                default:
                    PacketNotifier126.NotifyDeath(data);
                    break;
            }
        }

        public static void WaypointGroupWithSpeedNotify(AttackableUnit u)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyWaypointGroupWithSpeed(u);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyWaypointGroupWithSpeed(u);
                    break;
                default:
                    PacketNotifier126.NotifyWaypointGroupWithSpeed(u);
                    break;
            }
        }

        public static void WaypointGroupNotify()
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyWaypointGroup();
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyWaypointGroup();
                    break;
                default:
                    PacketNotifier126.NotifyWaypointGroup();
                    break;
            }
        }

        public static void SetAnimStatesNotify(AttackableUnit u, Dictionary<string, string> animationPairs)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_SetAnimStates(u, animationPairs);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_SetAnimStates(u, animationPairs);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_SetAnimStates(u, animationPairs);
                    break;
            }
        }

        public static void PingLoadInfoNotify(ClientInfo client, object packet)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyPingLoadInfo(client, packet as Crys.Game.C2S_Ping_Load_Info);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyPingLoadInfo(client, packet as Siph.Game.C2S_Ping_Load_Info);
                    break;
                default:
                    PacketNotifier126.NotifyPingLoadInfo(client, packet as Siph.Game.C2S_Ping_Load_Info);
                    break;
            }
        }

        public static void S2C_QueryStatusAnsNotify(int userId)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_QueryStatusAns(userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_QueryStatusAns(userId);
                    break;
                default:
                    PacketNotifier126.NotifyS2C_QueryStatusAns(userId);
                    break;
            }
        }

        public static void DebugPacketNotify(int userId, byte[] data)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyDebugPacket(userId, data);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyDebugPacket(userId, data);
                    break;
                default:
                    PacketNotifier126.NotifyDebugPacket(userId, data);
                    break;
            }
        }
        public static void SystemMessageNotify(ClientInfo sender, ChatType chatType, string message)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySystemMessage(sender, chatType, message);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySystemMessage(sender, chatType, message);

                    break;
                default:
                    PacketNotifier126.NotifySystemMessage(sender, chatType, message);
                    break;
            }
        }

        public static void ChatPacketNotify(ClientInfo sender, ChatType chatType, string message)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyChatPacket(sender, chatType, message);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyChatPacket(sender, chatType, message);

                    break;
                default:
                    PacketNotifier126.NotifyChatPacket(sender, chatType, message);
                    break;
            }
        }

        public static void Basic_AttackNotify(CoTG.CoTGServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyBasic_Attack(castInfo);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyBasic_Attack(castInfo);

                    break;
                default:
                    PacketNotifier126.NotifyBasic_Attack(castInfo);
                    break;
            }
        }

        public static void Basic_Attack_PosNotify(CoTG.CoTGServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyBasic_Attack_Pos(castInfo);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyBasic_Attack_Pos(castInfo);

                    break;
                default:
                    PacketNotifier126.NotifyBasic_Attack_Pos(castInfo);
                    break;
            }
        }

        public static void NPC_CastSpellAnsNotify(CoTG.CoTGServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyNPC_CastSpellAns(castInfo);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyNPC_CastSpellAns(castInfo);

                    break;
                default:
                    PacketNotifier126.NotifyNPC_CastSpellAns(castInfo);
                    break;
            }
        }

        public static void PausePacketNotify(ClientInfo player, int seconds, bool isTournament)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyPausePacket(player, seconds, isTournament);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyPausePacket(player, seconds, isTournament);

                    break;
                default:
                    PacketNotifier126.NotifyPausePacket(player, seconds, isTournament);
                    break;
            }
        }

        public static void ResumePacketNotify(Champion unpauser, ClientInfo player, bool isDelayed)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyResumePacket(unpauser, player, isDelayed);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyResumePacket(unpauser, player, isDelayed);

                    break;
                default:
                    PacketNotifier126.NotifyResumePacket(unpauser, player, isDelayed);
                    break;
            }
        }

        public static void SyncMissionStartTimeS2CNotify(int userId, float time)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySyncMissionStartTimeS2C(userId, time);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySyncMissionStartTimeS2C(userId, time);

                    break;
                default:
                    PacketNotifier126.NotifySyncMissionStartTimeS2C(userId, time);
                    break;
            }
        }

        public static void SpawnPetNotify(Pet pet, int userId, TeamId team, bool doVision)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySpawnPet(pet, userId, team, doVision);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySpawnPet(pet, userId, team, doVision);

                    break;
                default:
                    PacketNotifier126.NotifySpawnPet(pet, userId, team, doVision);
                    break;
            }
        }

        public static void TeamSurrenderStatusNotify(int userId, TeamId userTeam, TeamId surrendererTeam, SurrenderReason reason, int yesVotes, int noVotes)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyTeamSurrenderStatus(userId, userTeam, surrendererTeam, reason, yesVotes, noVotes);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyTeamSurrenderStatus(userId, userTeam, surrendererTeam, reason, yesVotes, noVotes);

                    break;
                default:
                    PacketNotifier126.NotifyTeamSurrenderStatus(userId, userTeam, surrendererTeam, reason, yesVotes, noVotes);
                    break;
            }
        }

        public static void TeamSurrenderVoteNotify(Champion starter, bool open, bool votedYes, int yesVotes, int noVotes, int maxVotes, float timeOut)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyTeamSurrenderVote(starter, open, votedYes, yesVotes, noVotes, maxVotes, timeOut);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyTeamSurrenderVote(starter, open, votedYes, yesVotes, noVotes, maxVotes, timeOut);

                    break;
                default:
                    PacketNotifier126.NotifyTeamSurrenderVote(starter, open, votedYes, yesVotes, noVotes, maxVotes, timeOut);
                    break;
            }
        }

        public static void HandleQuestUpdateNotify(Quest _quest, byte _Command)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_HandleQuestUpdate(_quest, _Command);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_HandleQuestUpdate(_quest, _Command);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_HandleQuestUpdate(_quest, _Command);
                    break;
            }
        }

        public static void FXCreateGroupNotify(Particle particle, string particleName, int userId = -1)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyFXCreateGroup(particle, particleName, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyFXCreateGroup(particle, particleName, userId);

                    break;
                default:
                    PacketNotifier126.NotifyFXCreateGroup(particle, particleName, userId);
                    break;
            }
        }

        public static void FXEnterTeamVisibilityNotify(Particle particle, TeamId team, int userId)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyFXEnterTeamVisibility(particle, team, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyFXEnterTeamVisibility(particle, team, userId);

                    break;
                default:
                    PacketNotifier126.NotifyFXEnterTeamVisibility(particle, team, userId);
                    break;
            }
        }

        public static void FXLeaveTeamVisibilityNotify(Particle particle, TeamId team, int userId)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyFXLeaveTeamVisibility(particle, team, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyFXLeaveTeamVisibility(particle, team, userId);

                    break;
                default:
                    PacketNotifier126.NotifyFXLeaveTeamVisibility(particle, team, userId);
                    break;
            }
        }

        public static void FXKillNotify(Particle particle)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyFXKill(particle);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyFXKill(particle);

                    break;
                default:
                    PacketNotifier126.NotifyFXKill(particle);
                    break;
            }
        }

        public static void NPC_BuffAdd2Notify(Buff b, int stacks)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyNPC_BuffAdd2(b, stacks);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyNPC_BuffAdd2(b, stacks);

                    break;
                default:
                    PacketNotifier126.NotifyNPC_BuffAdd2(b, stacks);
                    break;
            }
        }

        public static void NPC_BuffUpdateCountNotify(Buff b, int stacks)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyNPC_BuffUpdateCount(b, stacks);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyNPC_BuffUpdateCount(b, stacks);

                    break;
                default:
                    PacketNotifier126.NotifyNPC_BuffUpdateCount(b, stacks);
                    break;
            }
        }

        public void NPC_BuffRemove2Notify2(Buff b)
        {
            NPC_BuffRemove2Notify(b.TargetUnit, b.Slot, b.Name);
        }


        public static void NPC_BuffRemove2Notify(AttackableUnit owner, int slot, string name)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyNPC_BuffRemove2(owner, slot, name);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyNPC_BuffRemove2(owner, slot, name);

                    break;
                default:
                    PacketNotifier126.NotifyNPC_BuffRemove2(owner, slot, name);
                    break;
            }
        }

        public static void S2C_SetFadeOut_PushNotify(GameObject o, float value, float time, int userId)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_SetFadeOut_Push(o, value, time, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_SetFadeOut_Push(o, value, time, userId);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_SetFadeOut_Push(o, value, time, userId);
                    break;
            }
        }

        public static void SetTeamNotify(AttackableUnit unit)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySetTeam(unit);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySetTeam(unit);

                    break;
                default:
                    PacketNotifier126.NotifySetTeam(unit);
                    break;
            }
        }

        public static void S2C_PlayAnimationNotify(GameObject obj, string animation, AnimationFlags flags = 0, float timeScale = 1.0f, float startTime = 0.0f, float speedScale = 1.0f)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_PlayAnimation(obj, animation, flags, timeScale, startTime, speedScale);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_PlayAnimation(obj, animation, flags, timeScale, startTime, speedScale);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_PlayAnimation(obj, animation, flags, timeScale, startTime, speedScale);
                    break;
            }
        }

        public static void S2C_UnlockAnimationNotify(GameObject obj, string name)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_UnlockAnimation(obj, name);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_UnlockAnimation(obj, name);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_UnlockAnimation(obj, name);
                    break;
            }
        }

        public static void S2C_PauseAnimationNotify(GameObject obj, bool pause)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_PauseAnimation(obj, pause);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_PauseAnimation(obj, pause);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_PauseAnimation(obj, pause);
                    break;
            }
        }

        public static void S2C_StopAnimationNotify(GameObject obj, string animation, bool stopAll = false, bool fade = false, bool ignoreLock = true)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_StopAnimation(obj, animation, stopAll, fade, ignoreLock);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_StopAnimation(obj, animation, stopAll, fade, ignoreLock);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_StopAnimation(obj, animation, stopAll, fade, ignoreLock);
                    break;
            }
        }

        public static void FaceDirectionNotify(GameObject obj, Vector3 direction, bool isInstant = true, float turnTime = 0.0833f)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyFaceDirection(obj, direction, isInstant, turnTime);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyFaceDirection(obj, direction, isInstant, turnTime);

                    break;
                default:
                    PacketNotifier126.NotifyFaceDirection(obj, direction, isInstant, turnTime);
                    break;
            }
        }

        public static void SetFrequencyNotify(float number = 0.0833f)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    // PacketNotifier106.NotifySetFrequencyS2C(obj, direction, isInstant, turnTime);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySetFrequencyS2C(number);

                    break;
                default:
                    PacketNotifier126.NotifySetFrequencyS2C(number);
                    break;
            }
        }

        public static void MissileReplicationNotify(SpellMissile m, int userId = -1)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyMissileReplication(m, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyMissileReplication(m, userId);

                    break;
                default:
                    PacketNotifier126.NotifyMissileReplication(m, userId);
                    break;
            }
        }

        public static void DestroyClientMissileNotify(SpellMissile m)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyDestroyClientMissile(m);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyDestroyClientMissile(m);

                    break;
                default:
                    PacketNotifier126.NotifyDestroyClientMissile(m);
                    break;
            }
        }

        public static void Neutral_Camp_EmptyNotify(NeutralMinionCamp neutralCamp, ObjAIBase? killer = null, int userId = -1)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_Neutral_Camp_Empty(neutralCamp, killer, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_Neutral_Camp_Empty(neutralCamp, killer, userId);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_Neutral_Camp_Empty(neutralCamp, killer, userId);
                    break;
            }
        }

        public static void ActivateMinionCampNotify(NeutralMinionCamp monsterCamp, int userId = -1)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_ActivateMinionCamp(monsterCamp, userId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_ActivateMinionCamp(monsterCamp, userId);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_ActivateMinionCamp(monsterCamp, userId);
                    break;
            }
        }

        public static void CreateMinionCampNotify(NeutralMinionCamp monsterCamp, int userId, TeamId team)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_CreateMinionCamp(monsterCamp, userId, team);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_CreateMinionCamp(monsterCamp, userId, team);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_CreateMinionCamp(monsterCamp, userId, team);
                    break;
            }


        }

        public static void ChainMissileSyncNotify(SpellChainMissile p)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_ChainMissileSync(p);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_ChainMissileSync(p);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_ChainMissileSync(p);
                    break;
            }
        }

        public static void UnitAddEXPNotify(Champion champion, float experience)
        {
            //  Console.ReadLine();

            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyUnitAddEXP(champion, experience);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyUnitAddEXP(champion, experience);

                    break;
                default:
                    PacketNotifier126.NotifyUnitAddEXP(champion, experience);
                    break;
            }
        }

        public static void S2C_PlayEmoteNotify(Emotions type, uint netId)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_PlayEmote(type, netId);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_PlayEmote(type, netId);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_PlayEmote(type, netId);
                    break;
            }
        }

        public static void InhibitorStateNotify(Inhibitor inhibitor, DeathData? deathData = null)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyInhibitorState(inhibitor, deathData);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyInhibitorState(inhibitor, deathData);

                    break;
                default:
                    PacketNotifier126.NotifyInhibitorState(inhibitor, deathData);
                    break;
            }
        }

        public static void LineMissileHitListNotify(SpellLineMissile p, IEnumerable<AttackableUnit> units)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_LineMissileHitList(p, units);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_LineMissileHitList(p, units);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_LineMissileHitList(p, units);
                    break;
            }
        }

        public static void UnitAddGoldNotify(Champion target, GameObject source, float gold)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyUnitAddGold(target, source, gold);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyUnitAddGold(target, source, gold);

                    break;
                default:
                    PacketNotifier126.NotifyUnitAddGold(target, source, gold);
                    break;
            }
        }

        public static void AddRegionNotify(Region region, int userId, TeamId team)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyAddRegion(region, userId, team);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyAddRegion(region, userId, team);

                    break;
                default:
                    PacketNotifier126.NotifyAddRegion(region, userId, team);
                    break;
            }
        }

        public static void RemoveRegionNotify(Region region)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyRemoveRegion(region);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyRemoveRegion(region);

                    break;
                default:
                    PacketNotifier126.NotifyRemoveRegion(region);
                    break;
            }
        }

        public static void HandleTipUpdateNotify(int userId, string title, string text, string imagePath, int tipCommand, uint playerNetId, uint tipID)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_HandleTipUpdate(userId, title, text, imagePath, tipCommand, playerNetId, tipID);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_HandleTipUpdate(userId, title, text, imagePath, tipCommand, playerNetId, tipID);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_HandleTipUpdate(userId, title, text, imagePath, tipCommand, playerNetId, tipID);
                    break;
            }
        }

        public static void UnitSetMinimapIconNotify(int userId, AttackableUnit unit, bool changeIcon, bool changeBorder)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyS2C_UnitSetMinimapIcon(userId, unit, changeIcon, changeBorder);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_UnitSetMinimapIcon(userId, unit, changeIcon, changeBorder);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_UnitSetMinimapIcon(userId, unit, changeIcon, changeBorder);
                    break;
            }
        }

        public static void CreateTurretNotify(LaneTurret turret, int userId, bool doVision)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifyCreateTurret(turret, userId, doVision);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyCreateTurret(turret, userId, doVision);

                    break;
                default:
                    PacketNotifier126.NotifyCreateTurret(turret, userId, doVision);
                    break;
            }
        }

        public static void SpawnLevelPropNotify(LevelProp levelProp, int userId, TeamId team)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    PacketNotifier106.NotifySpawnLevelProp(levelProp, userId, team);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifySpawnLevelProp(levelProp, userId, team);

                    break;
                default:
                    PacketNotifier126.NotifySpawnLevelProp(levelProp, userId, team);
                    break;
            }
        }
        public static void S2C_ChangePARColorOverrideNotifier(Champion champ, byte r, byte g, byte b, byte a, byte fr, byte fg, byte fb, byte fa, int objID = 0)
        {
            switch (Game.Config.VersionOfClient)
            {
                case "1.0.0.106":
                case "0.9.22.14":
                    //  PacketNotifier106.NotifyS2C_ChangePARColorOverride(champ, r, g, b, a, fr, fg, fb, fa, objID);
                    break;
                case "1.0.0.126":
                case "1.0.0.131":
                case "1.0.0.124":
                    PacketNotifier126.NotifyS2C_ChangePARColorOverride(champ, r, g, b, a, fr, fg, fb, fa, objID);

                    break;
                default:
                    PacketNotifier126.NotifyS2C_ChangePARColorOverride(champ, r, g, b, a, fr, fg, fb, fa, objID);
                    break;
            }
        }
    }


}
