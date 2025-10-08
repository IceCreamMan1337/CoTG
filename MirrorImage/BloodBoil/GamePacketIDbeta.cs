using System;


namespace BloodBoil
{

    /*
    * Game packet IDs
    * BID = bi-directional, C2S = client to server, S2C = server to cliet, UNK = unknown yet
    */
    public enum GamePacketID //beta
    {
        BID_Dummy = 0x0,
        UNK_ClientConnect_NamedPipe = 0x1,
        UNK_CHAT = 0x2,
        C2S_QueryStatusReq = 0x3,
        S2C_QueryStatusAns = 0x4,
        S2C_StartSpawn = 0x5,
        S2C_CreateHero = 0x6,
        S2C_CreateNeutral = 0x7,
        S2C_CreateTurret = 0x8,
        S2C_PlayAnimation = 0x9,
        C2S_PlayEmote = 0xA,
        S2C_PlayEmote = 0xB,
        S2C_EndSpawn = 0xC,
        S2C_StartGame = 0xD,
        S2C_EndGame = 0xE,
        C2S_CharSelected = 0xF,
        C2S_ClientReady = 0x10,
        C2S_ClientFinished = 0x11,
        C2S_NPC_UpgradeSpellReq = 0x12,
        S2C_NPC_UpgradeSpellAns = 0x13,
        C2S_NPC_IssueOrderReq = 0x14,
        S2C_FX_Create_Group = 0x15,
        S2C_FX_Kill = 0x16,
        S2C_UnitApplyDamage = 0x17,
        S2C_Pause = 0x18,
        C2S_MapPing = 0x19,
        S2C_MapPing = 0x1A,
        S2C_UnitAddGold = 0x1B,
        S2C_UnitAddEXP = 0x1C,
        UNK_UserMessagesStart = 0x1D,
        S2C_NPC_MessageToClient = 0x1E,
        C2S_AI_Command = 0x1F,
        S2C_CHAR_SpawnPet = 0x20,
        S2C_CHAR_SetCooldown = 0x21,
        S2C_NPC_Die = 0x22,
        
        C2S_NPC_CastSpellReq = 0x23,
        S2C_NPC_CastSpellAns = 0x24,
        S2C_NPC_BuffAdd2 = 0x25,
        S2C_NPC_BuffRenew = 0x26,
        S2C_NPC_BuffRemove2 = 0x27,

        S2C_NPC_SetAutocast = 0x28,
 
        C2S_BuyItemReq = 0x29,
        S2C_BuyItemAns = 0x2A,
        C2S_RemoveItemReq = 0x2B,
        S2C_RemoveItemAns = 0x2C,
        C2S_SwapItemReq = 0x2D,
        S2C_SwapItemAns = 0x2E,

        S2C_NPC_LevelUp = 0x2F,


        S2C_NPC_InstantStop_Attack = 0x30,
        S2C_Barrack_SpawnUnit = 0x31,
        UNK_Turret_Fire = 0x32,
        UNK_Turret_CreateTurret = 0x33,
        pkt32 = 0x34,
        S2C_Basic_Attack = 0x35,
        S2C_Basic_Attack_Pos = 0x36,
        S2C_OnEnterVisiblityClient = 0x37,
        S2C_OnLeaveVisiblityClient = 0x38,
        S2C_OnEnterLocalVisiblityClient = 0x39,
        S2C_OnLeaveLocalVisiblityClient = 0x3A,


        C2S_World_SendCamera_Server = 0x3B,
        C2S_World_LockCamera_Server = 0x3C,


        C2S_SendSelectedObjID = 0x3D,
        S2C_UnitApplyHeal = 0x3E,
        S2C_MissileReplication = 0x3F,
        S2C_ServerTick = 0x40,
        S2C_DampenerSwitch = 0x41,
        S2C_GlobalCombatMessage = 0x42,
        C2S_SynchVersion = 0x43,
        S2C_SynchVersion = 0x44,
        S2C_AI_Target = 0x45,
        S2C_AI_TargetHero = 0x46,

        S2C_HeroReincarnateAlive = 0x47,
        S2C_HeroReincarnate = 0x48,
        S2C_Building_Die = 0x49,
        S2C_SynchSimTime = 0x4A,
        C2S_SynchSimTime = 0x4B,
        S2C_SyncSimTimeFinal = 0x4C,
        S2C_WaypointList = 0x4D,
        S2C_WaypointListHeroWithSpeed = 0x4E,
        S2C_ServerGameSettings = 0x4F,

        S2C_NPC_BuffUpdateCount = 0x50,

        C2S_PlayEmoticon = 0x51,
        S2C_PlayEmoticon = 0x52,
        S2C_AvatarInfo_Server = 0x53,
        S2C_RemovePerceptionBubble = 0x54,
        S2C_AddUnitPerceptionBubble = 0x55,
        S2C_AddPosPerceptionBubble = 0x56,
        S2C_SpawnMinion = 0x57,
        S2C_StopAnimation = 0x58,
        
        S2C_UpdateGoldRedirectTarget = 0x59,
        S2C_ChangeCharacterData = 0x5A,
        S2C_PopCharacterData = 0x5B,
        S2C_PopAllCharacterData = 0x5C,
        S2C_FaceDirection = 0x5D,
        S2C_CameraBehavior = 0x5E,
        C2S_SPM_AddListener = 0x5F,
        C2S_SPM_RemoveListener = 0x60,

        S2C_SPM_HierarchicalProfilerUpdate = 0x61,
        S2C_SPM_SamplingProfilerUpdate = 0x62,
        S2C_SPM_HierarchicalMemoryUpdate = 0x63,
        C2S_SPM_AddMemoryListener = 0x64,
        C2S_SPM_RemoveMemoryListener = 0x65,

       

        

        
        
        S2C_DestroyClientMissile = 0x66,
        S2C_ChainMissileSync = 0x67,
        S2C_MissileReplication_ChainMissile = 0x68,
        S2C_BotAI = 0x69,
        S2C_AI_TargetSelection = 0x6A,


        S2C_AI_State = 0x6B,
        S2C_OnEvent = 0x6C,
        UNK_OnDisconnected = 0x6D,
        S2C_World_SendCamera_Server_Ack = 0x6E,
        S2C_World_SendGameNumber = 0x6F,

        S2C_NPC_Die_EventHistory = 0x70,
        C2S_Ping_Load_Info = 0x71,
        S2C_Ping_Load_Info = 0x72,
        C2S_Exit = 0x73,
        S2C_Exit = 0x74,
        C2S_Reconnect = 0x75,
        S2C_Reconnect = 0x76,
        S2C_Reconnect_Done = 0x77,
        C2S_Waypoint_Acc = 0x78,
        S2C_WaypointGroup = 0x79,
        S2C_WaypointGroupWithSpeed = 0x7A,
        S2C_Connected = 0x7B,


        C2S_ToggleInputLockingFlag = 0x7C,


        S2C_ToggleInputLockingFlag = 0x7D,
        S2C_ToggleFoW = 0x7E,

        S2C_SetCircularCameraRestriction = 0x7F,


        S2C_LockCamera = 0x80,

        S2C_OnReplication = 0x81,
        C2S_OnReplication_Acc = 0x82,
        S2C_MoveCameraToPoint = 0x83,
        S2C_PlayTutorialAudioEvent = 0x84,
        S2C_ChangeSlotSpellType = 0x85,
        BID_PausePacket = 0x86,
        BID_ResumePacket = 0x87,
        S2C_SetFrequency = 0x88,
        S2C_SetFadeOut_Push = 0x89,
        S2C_SetFadeOut_Pop = 0x8A,

        C2S_SPM_AddBBProfileListener = 0x8B,
        C2S_SPM_RemoveBBProfileListener = 0x8C,
        S2C_SPM_HierarchicalBBProfileUpdate = 0x8D,


        S2C_CreateUnitHighlight = 0x8E,
        S2C_RemoveUnitHighlight = 0x8F,
        S2C_OpenTutorialPopup = 0x90,
        C2S_OnTutorialPopupClosed = 0x91,
        S2C_OpenAFKWarningMessage = 0x92,
        S2C_CloseShop = 0x93,





      
        UpperLimitForPacketIDs = 0x100,







        //doesnt exit : 

        ExtendedPacket = 0x0FE, //


        C2S_AntiBot = 0x10000,
        C2S_AntiBotDP = 0x10001,
        C2S_OnQuestEvent = 0x10002,
        C2S_OnRespawnPointEvent = 0x10003,
        C2S_OnScoreBoardOpened = 0x10004,
        C2S_OnTipEvent = 0x10005,
        C2S_StatsUpdateReq = 0x10006,
        C2S_UseObject = 0x10007,
        C2S_WriteNavFlags_Acc = 0x10008,
        S2C_WriteNavFlags = 0x10009,
        S2C_AntiBot = 0x1000A,
        S2C_AntiBotWriteLog = 0x1000B,
        S2C_AntiBotKickOut = 0x1000C,
        S2C_AntiBotBanPlayer = 0x1000D,
        S2C_AntiBotTrojan = 0x1000E,
        S2C_AntiBotCloseClient = 0x1000F,

        S2C_AttachFlexParticle = 0x20000,
        S2C_ChangeSlotSpellIcon = 0x20001,              
        S2C_ChangeSlotSpellOffsetTarget = 0x20002,
        S2C_ChangeCharacterVoice = 0x20003,
        S2C_ChangePARColorOverride = 0x20004,
        S2C_CHAR_CancelTargetingReticle = 0x20005,
        UNK_Unused_0C = 0x20006,
        S2C_UpdateLevelProp = 0x20007,
        S2C_UnitChangeTeam = 0x20008,                   
        S2C_UnitSetMinimapIcon = 0x20009 ,
        S2C_SyncMissionStartTime = 0x2000A ,
        S2C_ColorRemapFX = 0x2000B,
        S2C_DisplayFloatingText = 0x2000C,
        S2C_EndOfGameEvent = 0x2000D,
        S2C_FX_OnEnterTeamVisiblity = 0x2000E,
        S2C_FX_OnLeaveTeamVisiblity = 0x2000F   ,

        S2C_IncrementPlayerScore     = 0x30001,
        S2C_IncrementPlayerStat = 0x30002,
        S2C_LevelUpSpell = 0x30003,
        S2C_ModifyShield = 0x30004,
        S2C_MusicCueCommand = 0x30005,
        S2C_OnEnterTeamVisiblity = 0x30006,
        S2C_OnLeaveTeamVisiblity = 0x30007,

        S2C_PauseAnimation = 0x30008,
        S2C_ReplayOnly_GoldEarned = 0x30009, 


        S2C_HandleCapturePointUpdate = 0x3000A            /* = 0x0DC */, // xxxx unused in replay      yep dominion
        S2C_HandleGameScore = 0x3000B                       /* = 0x0DD */, // xxxx unused in replay      yep dominion
        S2C_HandleRespawnPointUpdate = 0x3000C ,
        S2C_HandleQuestUpdate = 0x3000D,
        S2C_HandleTipUpdate = 0x3000E ,
        S2C_HandleUIHighlight = 0x3000F,

        S2C_SetCircularMovementRestriction = 0x40001,
        S2C_SetFoWStatus = 0x40002,
        S2C_SetSpellData = 0x40003,
        S2C_SpawnBot = 0x40004,
        S2C_SpawnLevelProp = 0x40005,
            S2C_NPC_Hero_Die = 0x40006,
        S2C_PlayVOAudioEvent = 0x40007,


        S2C_NPC_ForceDead = 0x40008,
        S2C_NPC_BuffAddGroup = 0x40009,
        S2C_NPC_BuffReplace = 0x4000A,
        S2C_NPC_BuffReplaceGroup = 0x4000B,
        S2C_NPC_BuffRemoveGroup = 0x4000C,
        S2C_SetItem = 0x4000D,
        S2C_UseItemAns = 0x4000E,

       
       
        S2C_NPC_BuffUpdateCountGroup = 0x500006,
        S2C_ShowHealthBar = 0x500007,

        
        S2C_ToggleFoWOn = 0x500008,




        S2C_CreateHero_131 = 140008,
        S2C_CreateTurret_131 = 140009,
        S2C_SynchVersion_131 = 140010,



        S2C_SetInputLockingFlag = 0x9C,
        C2S_OnShopOpened = 0x9D,
        S2C_ShowObjectiveText = 0x9E,
        S2C_HideObjectiveText = 0x9F,
        S2C_RefreshObjectiveText = 0xA0,
        S2C_ShowAuxiliaryText = 0xA1,
        S2C_HideAuxiliaryText = 0xA2,
        S2C_RefreshAuxiliaryText = 0xA3,
        S2C_HighlightHUDElement = 0xA4,
        S2C_HighlightShopElement = 0xA5,
        C2S_TeamSurrenderVote = 0xA6,
        S2C_TeamSurrenderVote = 0xA7,
        S2C_TeamSurrenderCountDown = 0xA8,
        S2C_TeamSurrenderStatus = 0xA9,
        S2C_LineMissileHitList = 0xAA,
        C2S_TutorialAudioEventFinished = 0xAB,
        S2C_HighlightTitanBarElement = 0xAC,
        S2C_ToggleUIHighlight = 0xAD,
        S2C_DisplayLocalizedTutorialChatText = 0xAE,
        S2C_ToolTipVars = 0xAF,
        S2C_MuteVolumeCategory = 0xB0,
        S2C_OnEventWorld = 0xB1,
        S2C_AnimatedBuildingSetCurrentSkin = 0xB2,
        S2C_SetGreyscaleEnabledWhenDead = 0xB3,
        S2C_DisableHUDForEndOfGame = 0xB4,
        S2C_ChangeSlotSpellName = 0xB5,
        S2C_SwitchNexusesToOnIdleParticles = 0xB6,
        S2C_FadeMinions = 0xB7,
        S2C_FadeOutMainSFX = 0xB8,
        S2C_HeroStats = 0xB9,
        S2C_SetAnimStates = 0xBA,
        C2S_ClientCheatDetectionSignal = 0xBB,
        S2C_AddDebugCircle = 0xBC,
        S2C_RemoveDebugCircle = 0xBD,
        S2C_ModifyDebugCircleRadius = 0xBE,
        S2C_ModifyDebugCircleColor = 0xBF,
        UNK_Undefined = 0xC0,
        S2C_Neutral_Camp_Empty = 0xC1,
        S2C_ResetForSlowLoader = 0xC2,
        Batch = 0xFF,





    }

}
