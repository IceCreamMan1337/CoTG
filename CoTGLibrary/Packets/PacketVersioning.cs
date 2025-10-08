using CoTG.CoTGServer;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using CoTG.CoTGServer.GameObjects.SpellNS.Missile;
using CoTG.CoTGServer.GameObjects.StatsNS;
using CoTG.CoTGServer.Inventory;
using CoTG.CoTGServer.Packets.PacketHandlers;
using CoTGEnumNetwork.Enums;
using CoTGEnumNetwork.NetInfo;
using CoTGEnumNetwork.Packets.Enums;
using CoTGEnumNetwork.Packets.Handlers;
using CoTGLibrary.Packets.PacketHandlers.SiphoningStrike;
using MirrorImage;
using PacketDefinitions126;
using SiphoningStrike.Game.Common;
using System.Collections.Generic;
using System.Numerics;
using DeathData = CoTG.CoTGServer.GameObjects.AttackableUnits.DeathData;
using Siph = SiphoningStrike;



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

        /// <summary>
        /// Handler for response packets sent by the server to game clients.
        /// </summary>
        // private static NetworkHandler<ICoreRequest> ResponseHandler { get; set; }

        /// <summary>
        /// Interface containing all function related packets (except handshake) which are sent by the server to game clients.
        /// </summary>
        internal static PacketNotifier126 PacketNotifier126 { get; private set; }

        public void NetLoop(uint timeout = 0)
        {
            PacketServer126.NetLoop(timeout);
        }

        public static void SetupVersioningServer(ushort port, string[] blowfishKeys)
        {
            PacketServer126 = new PacketServer126(port, blowfishKeys);
        }

        public static void InitializePacketHandlers(NetworkHandler<MirrorImage.BasePacket> RequestHandler)
        {
            InitializePacketHandlers126(RequestHandler);
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

            PacketNotifier126 = new PacketNotifier126(PacketServer126.PacketHandlerManager, Game.Map.NavigationGrid);
        }

        //find better way to do the rest

        public static void CreateAnimationdata(LevelProp prop, string animation, AnimationFlags animationFlag, float duration, bool destroyPropAfterAnimation)
        {
            UpdateLevelPropDataPlayAnimation animationData2 = new()
            {
                AnimationName = animation,
                AnimationFlags = (uint)animationFlag,
                Duration = duration,
                DestroyPropAfterAnimation = destroyPropAfterAnimation,
                StartMissionTime = Game.Time.GameTime,
                NetID = prop.NetId
            };

            PacketNotifier126.NotifyUpdateLevelPropS2C(animationData2);
        }

        public static void InstantStopAttackNotifier(AttackableUnit attacker, bool isSummonerSpell,
            bool keepAnimating = false,
            bool destroyMissile = true,
            bool overrideVisibility = true,
            bool forceClient = false,
            uint missileNetID = 0)
        {
            PacketNotifier126.NotifyNPC_InstantStop_Attack(attacker, isSummonerSpell, keepAnimating, destroyMissile, overrideVisibility, forceClient, missileNetID);
        }

        public static void ChangeSlotSpellDataNotifier(int userId, ObjAIBase owner, int slot, ChangeSlotSpellDataType changeType, bool isSummonerSpell = false, TargetingType targetingType = TargetingType.Invalid, string newName = "", float newRange = 0, float newMaxCastRange = 0, float newDisplayRange = 0, int newIconIndex = 0x0, List<uint> offsetTargets = null)
        {
            PacketNotifier126.NotifyChangeSlotSpellData(userId, owner, slot, changeType, slot is 4 or 5, targetingType, newName, newRange, newMaxCastRange, newDisplayRange, newIconIndex, offsetTargets);
        }

        public static void SetSpellDataNotifier(int userId, uint netId, string spellName, int slot)
        {
            PacketNotifier126.NotifyS2C_SetSpellData(userId, netId, spellName, slot);
        }

        public static void SetSpellLevelNotifier(int userId, uint netId, int slot, int level)
        {
            PacketNotifier126.NotifyS2C_SetSpellLevel(userId, netId, slot, level);
        }

        public static void TargetHeroS2CNotifier(ObjAIBase attacker, AttackableUnit? target)
        {
            PacketNotifier126.NotifyAI_TargetHeroS2C(attacker, target);
        }

        public static void TargetS2CNotifier(ObjAIBase attacker, AttackableUnit? target)
        {
            PacketNotifier126.NotifyAI_TargetS2C(attacker, target);
        }

        public static void UnitSetLookAtNotifier(AttackableUnit attacker, LookAtType lookAtType, AttackableUnit? target, Vector3 targetPosition = default)
        {
            PacketNotifier126.NotifyS2C_UnitSetLookAt(attacker, lookAtType, target, targetPosition);
        }

        public static void S2C_AIStateNotify(ObjAIBase owner, AIState state)
        {
            PacketNotifier126.NotifyS2C_AIState(owner, state);
        }

        public static void ChangeCharacterDataNotifier(ObjAIBase obj, string skinName, bool modelOnly = true, bool overrideSpells = false, bool replaceCharacterPackage = false, int countchangemodel = 0)
        {
            PacketNotifier126.NotifyS2C_ChangeCharacterData(obj, skinName, modelOnly, overrideSpells, replaceCharacterPackage, countchangemodel);
        }

        public static void PopCharacterDataNotifier(ObjAIBase obj, int countchangemodel = 0)
        {
            PacketNotifier126.NotifyS2C_PopCharacterData(obj, countchangemodel);
        }

        public static void GameStartNotify(int userId)
        {
            PacketNotifier126.NotifyGameStart(userId);
        }

        public static void StartSpawnNotify(int userId, TeamId team, List<ClientInfo> players)
        {
            PacketNotifier126.NotifyS2C_StartSpawn(userId, team, players);
        }

        public static void SpawnEndNotify(int userId)
        {
            PacketNotifier126.NotifySpawnEnd(userId);
        }

        public static void OnEventWorldNotify(object mapEvent, AttackableUnit? source = null)
        {
            PacketNotifier126.NotifyS2C_OnEventWorld(mapEvent as Siph.Game.Events.IEvent, source);
        }

        public static void SynchSimTimeNotify(float GameTime)
        {
            PacketNotifier126.NotifySynchSimTimeS2C(GameTime);
        }

        public static void SynchSimTimeNotify2(int userId, float GameTime)
        {
            PacketNotifier126.NotifySynchSimTimeS2C(userId, GameTime);
        }

        public static void OnEventNotify(object mapEvent, AttackableUnit? source = null)
        {
            PacketNotifier126.NotifyOnEvent(mapEvent as Siph.Game.Events.IEvent, source);
        }

        public static void OnMapPingNotify(Vector2 pos, Pings type, uint targetNetId = 0, ClientInfo client = null)
        {
            PacketNotifier126.NotifyS2C_MapPing(pos, type, targetNetId, client);
        }

        public static void GameScoreNotify(TeamId team, int score)
        {
            PacketNotifier126.NotifyS2C_HandleGameScore(team, score);
        }

        public static void HandleCapturePointUpdateNotify(int capturePointIndex, uint otherNetId, int PARType, int attackTeam, CapturePointUpdateCommand capturePointUpdateCommand)
        {
            PacketNotifier126.NotifyS2C_HandleCapturePointUpdate(capturePointIndex, otherNetId, PARType, attackTeam, capturePointUpdateCommand);
        }

        public static void CameraBehaviorNotify(Champion target, Vector3 position)
        {
            PacketNotifier126.NotifyS2C_CameraBehavior(target, position);
        }

        public static void OnReplicationNotify()
        {
            PacketNotifier126.NotifyOnReplication();
        }

        public static void CreateUnitHighlightNotify(int userId, GameObject unit)
        {
            PacketNotifier126.NotifyCreateUnitHighlight(userId, unit);
        }

        public static void RemoveUnitHighlightNotify(int userId, GameObject unit)
        {
            PacketNotifier126.NotifyRemoveUnitHighlight(userId, unit);
        }

        public static void EndGameNotify(TeamId winningTeam)
        {
            PacketNotifier126.NotifyS2C_EndGame(winningTeam);
        }

        public static void SetGreyscaleEnabledWhenDeadNotify(ClientInfo client, bool enabled)
        {
            PacketNotifier126.NotifyS2C_SetGreyscaleEnabledWhenDead(client, enabled);
        }

        public static void MoveCameraToPointNotify(ClientInfo player, Vector3 startPosition, Vector3 endPosition, float travelTime = 0, bool startFromCurretPosition = true, bool unlockCamera = false)
        {
            PacketNotifier126.NotifyS2C_MoveCameraToPoint(player, startPosition, endPosition, travelTime, startFromCurretPosition, unlockCamera);
        }

        public static void DisableHUDForEndOfGameNotify()
        {
            PacketNotifier126.NotifyS2C_DisableHUDForEndOfGame();
        }

        public static void TintNotify(TeamId team, bool enable, float speed, float maxWeight, CoTGEnumNetwork.Content.Color color)
        {
            PacketNotifier126.NotifyTint(team, enable, speed, maxWeight, color);
        }

        public static void ShowHealthBarNotify(AttackableUnit unit, bool show, TeamId observerTeamId = TeamId.TEAM_UNKNOWN, bool changeHealthBarType = false, HealthBarType healthBarType = HealthBarType.Invalid)
        {
            PacketNotifier126.NotifyS2C_ShowHealthBar(unit, show, observerTeamId, changeHealthBarType, healthBarType);
        }

        public static void ModifyDebugCircleRadiusNotify(int userId, uint sender, int objID, float newRadius)
        {
            PacketNotifier126.NotifyModifyDebugCircleRadius(userId, sender, objID, newRadius);
        }

        public static void SetCircularMovementRestrictionNotify(ObjAIBase unit, Vector3 center, float radius, bool restrictcam = false)
        {
            PacketNotifier126.NotifySetCircularMovementRestriction(unit, center, radius, restrictcam);
        }

        public static void DisplayFloatingTextNotify(FloatingTextData floatTextData, TeamId team = 0, int userId = -1)
        {
            PacketNotifier126.NotifyDisplayFloatingText(floatTextData, team, userId);
        }


        public static void AttachCapturePointToIdleParticlesNotify(ObjAIBase unit, byte _ParticleFlexID, byte _CpIndex, uint _ParticleAttachType)
        {
            PacketNotifier126.NotifyAttachFlexParticle(unit, _ParticleFlexID, _CpIndex, _ParticleAttachType);
        }

        public static void InteractiveMusicCommandNotify(ObjAIBase unit, byte _MusicCommand, uint _MusicEventAudioEventID, uint _MusicParamAudioEventID)
        {
            PacketNotifier126.S2C_InteractiveMusicCommand(unit, _MusicCommand, _MusicEventAudioEventID, _MusicParamAudioEventID);
        }


        public static void LoadScreenInfoNotify(int userId, TeamId team, List<ClientInfo> players)
        {
            PacketNotifier126.NotifyLoadScreenInfo(userId, team, players);
        }

        public static void RequestRenameNotify(int userId, ClientInfo player)
        {
            PacketNotifier126.NotifyRequestRename(userId, player);
        }

        public static void RequestReskinNotify(int userId, ClientInfo player)
        {
            PacketNotifier126.NotifyRequestReskin(userId, player);
        }

        public static void KeyCheckNotify(int clientID, long playerId, long _EncryptedPlayerID, bool broadcast = false)
        {
            PacketNotifier126.NotifyKeyCheck(clientID, playerId, _EncryptedPlayerID, broadcast);
        }

        public static void WorldSendGameNumberNotify()
        {
            PacketNotifier126.NotifyWorldSendGameNumber();
        }
        public static void SynchVersionNotify(int userId, TeamId team, List<ClientInfo> players, string version, string gameMode, GameFeatures gameFeatures, int mapId, string[] mutators)
        {
            PacketNotifier126.NotifySynchVersion(userId, team, players, version, gameMode, gameFeatures, mapId, mutators);

        }
        public static void SwapItemsNotify(Champion c, int sourceSlot, int destinationSlot)
        {
            PacketNotifier126.NotifySwapItemAns(c, sourceSlot, destinationSlot);
        }

        public static void SwapItemsNotify(ClientInfo clientInfo, int userId, TeamId team, bool doVision)
        {
            PacketNotifier126.NotifyS2C_CreateHero(clientInfo, userId, team, doVision);
        }

        public static void AvatarInfoNotify(ClientInfo client, int userId = -1)
        {
            PacketNotifier126.NotifyAvatarInfo(client, userId);
        }

        public static void BuyItemNotify(ObjAIBase gameObject, Item itemInstance)
        {
            PacketNotifier126.NotifyBuyItem(gameObject, itemInstance);
        }

        public static void UseItemAnsNotify(ObjAIBase unit, byte slot, int itemsInSlot, int spellCharges = 0)
        {
            PacketNotifier126.NotifyUseItemAns(unit, slot, itemsInSlot, spellCharges);
        }

        public static void RemoveItemNotify(ObjAIBase ai, int slot, int remaining)
        {
            PacketNotifier126.NotifyRemoveItem(ai, slot, remaining);
        }

        public static void CHAR_SetCooldownNotify(ObjAIBase u, int slotId, float currentCd, float totalCd, int userId = -1, bool issummonerspell = false)
        {
            PacketNotifier126.NotifyCHAR_SetCooldown(u, slotId, currentCd, totalCd, userId, issummonerspell);
        }

        public static void NPC_UpgradeSpellAnsNotify(int userId, uint netId, int slot, int level, int points)
        {
            PacketNotifier126.NotifyNPC_UpgradeSpellAns(userId, netId, slot, level, points);
        }

        public static void ToolTipVarsNotify(List<ToolTipData> data)
        {
            PacketNotifier126.NotifyS2C_ToolTipVars(data);
        }

        public static void HeroReincarnateAliveNotify(Champion c, float parToRestore)
        {
            PacketNotifier126.NotifyHeroReincarnateAlive(c, parToRestore);
        }

        public static void NPC_ForceDeadNotify(CoTG.CoTGServer.GameObjects.AttackableUnits.DeathData lastDeathData)
        {
            PacketNotifier126.NotifyNPC_ForceDead(lastDeathData);
        }

        public static void NPC_Hero_DieNotify(CoTG.CoTGServer.GameObjects.AttackableUnits.DeathData deathData)
        {
            PacketNotifier126.NotifyNPC_Hero_Die(deathData);
        }

        public static void TintPlayerNotify(Champion champ, bool enable, float speed, CoTGEnumNetwork.Content.Color color)
        {
            PacketNotifier126.NotifyTintPlayer(champ, enable, speed, color);
        }


        public static void S2C_IncrementPlayerScoreNotify(ScoreData scoreData)
        {
            PacketNotifier126.NotifyS2C_IncrementPlayerScore(scoreData);
        }

        public static void MinionSpawnedNotify(Minion minion, int userId, TeamId team, bool doVision)
        {
            PacketNotifier126.NotifyMinionSpawned(minion, userId, team, doVision);
        }

        public static void NPC_LevelUpNotify(ObjAIBase obj)
        {
            PacketNotifier126.NotifyNPC_LevelUp(obj);
        }

        public static void OnReplicationUnitNotify(AttackableUnit u, int userId = -1, bool partial = true)
        {
            PacketNotifier126.NotifyOnReplication(u, userId, partial);
        }





        public static void LaneMinionSpawnedNotify(LaneMinion m, int userId, bool doVision)
        {
            PacketNotifier126.NotifyLaneMinionSpawned(m, userId, doVision);
        }

        public static void CreateNeutralNotify(NeutralMinion monster, float time, int userId, TeamId team, bool doVision)
        {
            PacketNotifier126.NotifyCreateNeutral(monster, time, userId, team, doVision);
        }

        public static void ModifyShieldNotify(AttackableUnit unit, bool physical, bool magical, float amount, bool stopShieldFade = false)
        {
            PacketNotifier126.NotifyModifyShield(unit, physical, magical, amount);
        }

        public static void WriteNavFlagsNotify(Vector2 position, float radius, NavigationGridCellFlags flags)
        {
            PacketNotifier126.Notify_WriteNavFlags(position, radius, flags);
        }

        public static void EnterTeamVisionNotify(AttackableUnit u, int userId = -1, BasePacket? spawnPacket = null)
        {
            PacketNotifier126.NotifyEnterTeamVision(u, userId, spawnPacket);
        }

        public static void EnterVisibilityClientNotify(GameObject o, int userId = -1, bool ignoreVision = false, List<BasePacket> packets = null)
        {
            PacketNotifier126.NotifyEnterVisibilityClient(o, userId, ignoreVision, packets);
        }

        public static void OnEnterTeamVisibilityNotify(GameObject o, TeamId team, int userId)
        {
            PacketNotifier126.NotifyOnEnterTeamVisibility(o, team, userId);
        }

        public static void OnLeaveTeamVisibilityNotify(GameObject o, TeamId team, int userId = -1)
        {
            PacketNotifier126.NotifyOnLeaveTeamVisibility(o, team, userId);
        }

        public static void LeaveVisibilityClientNotify(GameObject o, TeamId team, int userId = -1)
        {
            PacketNotifier126.NotifyLeaveVisibilityClient(o, team, userId);
        }

        public static void HoldReplicationDataUntilOnReplicationNotify(AttackableUnit u, int userId, bool partial = true)
        {
            PacketNotifier126.HoldReplicationDataUntilOnReplicationNotification(u, userId, partial);
        }
        public static void HoldMovementDataUntilWaypointGroupNotify(AttackableUnit u, int userId, bool partial = true)
        {
            PacketNotifier126.HoldMovementDataUntilWaypointGroupNotification(u, userId, partial);
        }

        public static void UnitApplyDamageNotify(DamageData damageData, bool isGlobal = true)
        {
            PacketNotifier126.NotifyUnitApplyDamage(damageData, isGlobal);
        }

        public static void S2C_NPC_Die_MapViewNotify(DeathData data)
        {
            PacketNotifier126.NotifyS2C_NPC_Die_MapView(data);
        }

        public static void DeathNotify(DeathData data)
        {
            PacketNotifier126.NotifyDeath(data);
        }

        public static void WaypointGroupWithSpeedNotify(AttackableUnit u)
        {
            PacketNotifier126.NotifyWaypointGroupWithSpeed(u);
        }

        public static void WaypointGroupNotify()
        {
            PacketNotifier126.NotifyWaypointGroup();
        }

        public static void SetAnimStatesNotify(AttackableUnit u, Dictionary<string, string> animationPairs)
        {
            PacketNotifier126.NotifyS2C_SetAnimStates(u, animationPairs);
        }

        public static void PingLoadInfoNotify(ClientInfo client, object packet)
        {
            PacketNotifier126.NotifyPingLoadInfo(client, packet as Siph.Game.C2S_Ping_Load_Info);
        }

        public static void S2C_QueryStatusAnsNotify(int userId)
        {
            PacketNotifier126.NotifyS2C_QueryStatusAns(userId);
        }

        public static void DebugPacketNotify(int userId, byte[] data)
        {
            PacketNotifier126.NotifyDebugPacket(userId, data);
        }
        public static void SystemMessageNotify(ClientInfo sender, ChatType chatType, string message)
        {
            PacketNotifier126.NotifySystemMessage(sender, chatType, message);
        }

        public static void ChatPacketNotify(ClientInfo sender, ChatType chatType, string message)
        {
            PacketNotifier126.NotifyChatPacket(sender, chatType, message);
        }

        public static void Basic_AttackNotify(CoTG.CoTGServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            PacketNotifier126.NotifyBasic_Attack(castInfo);
        }

        public static void Basic_Attack_PosNotify(CoTG.CoTGServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            PacketNotifier126.NotifyBasic_Attack_Pos(castInfo);
        }

        public static void NPC_CastSpellAnsNotify(CoTG.CoTGServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            PacketNotifier126.NotifyNPC_CastSpellAns(castInfo);
        }

        public static void PausePacketNotify(ClientInfo player, int seconds, bool isTournament)
        {
            PacketNotifier126.NotifyPausePacket(player, seconds, isTournament);
        }

        public static void ResumePacketNotify(Champion unpauser, ClientInfo player, bool isDelayed)
        {
            PacketNotifier126.NotifyResumePacket(unpauser, player, isDelayed);
        }

        public static void SyncMissionStartTimeS2CNotify(int userId, float time)
        {
            PacketNotifier126.NotifySyncMissionStartTimeS2C(userId, time);
        }

        public static void SpawnPetNotify(Pet pet, int userId, TeamId team, bool doVision)
        {
            PacketNotifier126.NotifySpawnPet(pet, userId, team, doVision);
        }

        public static void TeamSurrenderStatusNotify(int userId, TeamId userTeam, TeamId surrendererTeam, SurrenderReason reason, int yesVotes, int noVotes)
        {
            PacketNotifier126.NotifyTeamSurrenderStatus(userId, userTeam, surrendererTeam, reason, yesVotes, noVotes);
        }

        public static void TeamSurrenderVoteNotify(Champion starter, bool open, bool votedYes, int yesVotes, int noVotes, int maxVotes, float timeOut)
        {
            PacketNotifier126.NotifyTeamSurrenderVote(starter, open, votedYes, yesVotes, noVotes, maxVotes, timeOut);
        }

        public static void HandleQuestUpdateNotify(Quest _quest, byte _Command)
        {
            PacketNotifier126.NotifyS2C_HandleQuestUpdate(_quest, _Command);
        }

        public static void FXCreateGroupNotify(Particle particle, string particleName, int userId = -1)
        {
            PacketNotifier126.NotifyFXCreateGroup(particle, particleName, userId);
        }

        public static void FXEnterTeamVisibilityNotify(Particle particle, TeamId team, int userId)
        {
            PacketNotifier126.NotifyFXEnterTeamVisibility(particle, team, userId);
        }

        public static void FXLeaveTeamVisibilityNotify(Particle particle, TeamId team, int userId)
        {
            PacketNotifier126.NotifyFXLeaveTeamVisibility(particle, team, userId);
        }

        public static void FXKillNotify(Particle particle)
        {
            PacketNotifier126.NotifyFXKill(particle);
        }

        public static void NPC_BuffAdd2Notify(Buff b, int stacks)
        {
            PacketNotifier126.NotifyNPC_BuffAdd2(b, stacks);
        }

        public static void NPC_BuffUpdateCountNotify(Buff b, int stacks)
        {
            PacketNotifier126.NotifyNPC_BuffUpdateCount(b, stacks);
        }

        public void NPC_BuffRemove2Notify2(Buff b)
        {
            NPC_BuffRemove2Notify(b.TargetUnit, b.Slot, b.Name);
        }

        public static void NPC_BuffRemove2Notify(AttackableUnit owner, int slot, string name)
        {
            PacketNotifier126.NotifyNPC_BuffRemove2(owner, slot, name);
        }

        public static void S2C_SetFadeOut_PushNotify(GameObject o, float value, float time, int userId)
        {
            PacketNotifier126.NotifyS2C_SetFadeOut_Push(o, value, time, userId);
        }

        public static void SetTeamNotify(AttackableUnit unit)
        {
            PacketNotifier126.NotifySetTeam(unit);
        }

        public static void S2C_PlayAnimationNotify(GameObject obj, string animation, AnimationFlags flags = 0, float timeScale = 1.0f, float startTime = 0.0f, float speedScale = 1.0f)
        {
            PacketNotifier126.NotifyS2C_PlayAnimation(obj, animation, flags, timeScale, startTime, speedScale);
        }

        public static void S2C_UnlockAnimationNotify(GameObject obj, string name)
        {
            PacketNotifier126.NotifyS2C_UnlockAnimation(obj, name);
        }

        public static void S2C_PauseAnimationNotify(GameObject obj, bool pause)
        {
            PacketNotifier126.NotifyS2C_PauseAnimation(obj, pause);
        }

        public static void S2C_StopAnimationNotify(GameObject obj, string animation, bool stopAll = false, bool fade = false, bool ignoreLock = true)
        {
            PacketNotifier126.NotifyS2C_StopAnimation(obj, animation, stopAll, fade, ignoreLock);
        }

        public static void FaceDirectionNotify(GameObject obj, Vector3 direction, bool isInstant = true, float turnTime = 0.0833f)
        {
            PacketNotifier126.NotifyFaceDirection(obj, direction, isInstant, turnTime);
        }

        public static void SetFrequencyNotify(float number = 0.0833f)
        {
            PacketNotifier126.NotifySetFrequencyS2C(number);
        }

        public static void MissileReplicationNotify(SpellMissile m, int userId = -1)
        {
            PacketNotifier126.NotifyMissileReplication(m, userId);
        }

        public static void DestroyClientMissileNotify(SpellMissile m)
        {
            PacketNotifier126.NotifyDestroyClientMissile(m);
        }

        public static void Neutral_Camp_EmptyNotify(NeutralMinionCamp neutralCamp, ObjAIBase? killer = null, int userId = -1)
        {
            PacketNotifier126.NotifyS2C_Neutral_Camp_Empty(neutralCamp, killer, userId);
        }

        public static void ActivateMinionCampNotify(NeutralMinionCamp monsterCamp, int userId = -1)
        {
            PacketNotifier126.NotifyS2C_ActivateMinionCamp(monsterCamp, userId);
        }

        public static void CreateMinionCampNotify(NeutralMinionCamp monsterCamp, int userId, TeamId team)
        {
            PacketNotifier126.NotifyS2C_CreateMinionCamp(monsterCamp, userId, team);
        }

        public static void ChainMissileSyncNotify(SpellChainMissile p)
        {
            PacketNotifier126.NotifyS2C_ChainMissileSync(p);
        }

        public static void UnitAddEXPNotify(Champion champion, float experience)
        {
            PacketNotifier126.NotifyUnitAddEXP(champion, experience);
        }

        public static void S2C_PlayEmoteNotify(Emotions type, uint netId)
        {
            PacketNotifier126.NotifyS2C_PlayEmote(type, netId);
        }

        public static void InhibitorStateNotify(Inhibitor inhibitor, DeathData? deathData = null)
        {
            PacketNotifier126.NotifyInhibitorState(inhibitor, deathData);
        }

        public static void LineMissileHitListNotify(SpellLineMissile p, IEnumerable<AttackableUnit> units)
        {
            PacketNotifier126.NotifyS2C_LineMissileHitList(p, units);
        }

        public static void UnitAddGoldNotify(Champion target, GameObject source, float gold)
        {
            PacketNotifier126.NotifyUnitAddGold(target, source, gold);
        }

        public static void AddRegionNotify(Region region, int userId, TeamId team)
        {
            PacketNotifier126.NotifyAddRegion(region, userId, team);
        }

        public static void RemoveRegionNotify(Region region)
        {
            PacketNotifier126.NotifyRemoveRegion(region);
        }

        public static void HandleTipUpdateNotify(int userId, string title, string text, string imagePath, int tipCommand, uint playerNetId, uint tipID)
        {
            PacketNotifier126.NotifyS2C_HandleTipUpdate(userId, title, text, imagePath, tipCommand, playerNetId, tipID);
        }

        public static void UnitSetMinimapIconNotify(int userId, AttackableUnit unit, bool changeIcon, bool changeBorder)
        {
            PacketNotifier126.NotifyS2C_UnitSetMinimapIcon(userId, unit, changeIcon, changeBorder);
        }

        public static void CreateTurretNotify(LaneTurret turret, int userId, bool doVision)
        {
            PacketNotifier126.NotifyCreateTurret(turret, userId, doVision);
        }

        public static void SpawnLevelPropNotify(LevelProp levelProp, int userId, TeamId team)
        {
            PacketNotifier126.NotifySpawnLevelProp(levelProp, userId, team);
        }
        public static void S2C_ChangePARColorOverrideNotifier(Champion champ, byte r, byte g, byte b, byte a, byte fr, byte fg, byte fb, byte fa, int objID = 0)
        {
            PacketNotifier126.NotifyS2C_ChangePARColorOverride(champ, r, g, b, a, fr, fg, fb, fa, objID);
        }
    }
}
