namespace BehaviourTrees;


class InitializationcustomgameClass : BehaviourTree
{


    public bool Initializationcustomgame(
               out float StartGameTime,
     out float LaneUpdateTime,
     out float EnemyStrengthTop,
     out float EnemyStrengthMid,
     out float EnemyStrengthBot,
     out float FriendlyStrengthTop,
     out float FriendlyStrengthBot,
     out float FriendlyStrengthMid,
     out AISquadClass Squad_PushBot,
     out AISquadClass Squad_PushMid,
     out AISquadClass Squad_PushTop,
     out AISquadClass Squad_WaitAtBase,
     out AIMissionClass Mission_PushBot,
     out AIMissionClass Mission_PushMid,
     out AIMissionClass Mission_PushTop,
     out AIMissionClass Mission_WaitAtBase,
     out float PrevLaneDistributionTime,
     out float PointValue_Champion,
     out float PointValue_Minion,
     out int DistributionCount,
     out float DynamicDistributionStartTime,
     out float DynamicDistributionUpdateTime,
     out float UpdateGoldXP,
     out bool DisconnectAdjustmentEnabled,
     out int DisconnectAdjustmentEntityID,
     out float TotalDeadTurrets,
     out bool DifficultyScaling_IsWinState,
     out bool IsDifficultySet,
     out bool OverrideDifficulty)

    {

        var disconnectAdjustmentInitialization = new DisconnectAdjustmentInitializationClass();

        float _StartGameTime = default;
        float _LaneUpdateTime = default;
        float _EnemyStrengthTop = default;
        float _EnemyStrengthMid = default;
        float _EnemyStrengthBot = default;
        float _FriendlyStrengthTop = default;
        float _FriendlyStrengthBot = default;
        float _FriendlyStrengthMid = default;
        AISquadClass _Squad_PushBot = default;
        AISquadClass _Squad_PushMid = default;
        AISquadClass _Squad_PushTop = default;
        AISquadClass _Squad_WaitAtBase = default;
        AIMissionClass _Mission_PushBot = default;
        AIMissionClass _Mission_PushMid = default;
        AIMissionClass _Mission_PushTop = default;
        AIMissionClass _Mission_WaitAtBase = default;
        float _PrevLaneDistributionTime = default;
        float _PointValue_Champion = default;
        float _PointValue_Minion = default;
        int _DistributionCount = default;
        float _DynamicDistributionStartTime = default;
        float _DynamicDistributionUpdateTime = default;
        float _UpdateGoldXP = default;
        bool _DisconnectAdjustmentEnabled = default;
        int _DisconnectAdjustmentEntityID = default;
        float _TotalDeadTurrets = default;
        bool _DifficultyScaling_IsWinState = default;
        bool _IsDifficultySet = default;
        bool _OverrideDifficulty = default;


        bool result =
                  // Sequence name :MaskFailure

                  // Sequence name :RunFirstTimeOnly
                  (
                        GetGameTime(
                              out _StartGameTime) &&
                              // Sequence name :Strength

                              SetVarFloat(
                                    out LaneUpdateTime,
                                    0) &&
                              SetVarFloat(
                                    out EnemyStrengthTop,
                                    0) &&
                              SetVarFloat(
                                    out EnemyStrengthMid,
                                    0) &&
                              SetVarFloat(
                                    out EnemyStrengthBot,
                                    0) &&
                              SetVarFloat(
                                    out FriendlyStrengthTop,
                                    0) &&
                              SetVarFloat(
                                    out FriendlyStrengthBot,
                                    0) &&
                              SetVarFloat(
                                    out FriendlyStrengthMid,
                                    0)
                         &&
                              // Sequence name :AI_Squads

                              CreateAISquad(
                                    out Squad_PushBot,
                                     "",
                                    5) &&
                              CreateAISquad(
                                    out Squad_PushMid,
                                     "",
                                    5) &&
                              CreateAISquad(
                                    out Squad_PushTop,
                                     "",
                                    5) &&
                              CreateAISquad(
                                    out Squad_WaitAtBase,
                                     "",
                                    5)
                         &&
                              // Sequence name :Missions

                              CreateAIMission(
                                    out _Mission_PushBot,
                                    AIMissionTopicType.PUSH,
                                    null,
                                    default,
                                    0) &&
                              CreateAIMission(
                                    out _Mission_PushMid,
                                    AIMissionTopicType.PUSH,
                                    null,
                                    default,
                                    1) &&
                              CreateAIMission(
                                    out _Mission_PushTop,
                                    AIMissionTopicType.PUSH,
                                    null,
                                    default,
                                    2) &&
                              CreateAIMission(
                                    out _Mission_WaitAtBase,
                                    AIMissionTopicType.SUPPORT,
                                    null,
                                    default
                                    )
                         &&
                        // Sequence name :AssignSquadsToMissions
                        AssignAIMission(
                                    _Squad_PushBot,
                                    _Mission_PushBot) &&
                              AssignAIMission(
                                    _Squad_PushMid,
                                    _Mission_PushMid) &&
                              AssignAIMission(
                                    _Squad_PushTop,
                                    _Mission_PushTop) &&
                              AssignAIMission(
                                    _Squad_WaitAtBase,
                                    _Mission_WaitAtBase)
                         &&
                              // Sequence name :DistributionValues

                              SetVarFloat(
                                    out PrevLaneDistributionTime,
                                    0) &&
                              SetVarFloat(
                                    out PointValue_Champion,
                                    100) &&
                              SetVarFloat(
                                    out PointValue_Minion,
                                    20) &&
                              SetVarInt(
                                    out DistributionCount,
                                    0) &&
                              SetVarFloat(
                                    out DynamicDistributionStartTime,
                                    540) &&
                              SetVarFloat(
                                    out DynamicDistributionUpdateTime,
                                    15)
                         &&
                              // Sequence name :LevelNormalizer

                              SetVarFloat(
                                    out UpdateGoldXP,
                                    0)
                         &&
                        SetVarFloat(
                              out TotalDeadTurrets,
                              0) &&
                       disconnectAdjustmentInitialization.DisconnectAdjustmentInitialization(
                              out _DisconnectAdjustmentEntityID,
                              out _DisconnectAdjustmentEnabled) &&
                               // Sequence name :DifficultyScaling

                               SetVarBool(
                                    out _DifficultyScaling_IsWinState,
                                    false) &&
                              SetVarBool(
                                    out _IsDifficultySet,
                                    false) &&
                              SetVarBool(
                                    out _OverrideDifficulty,
                                    false)


                  )
                   ||
                               DebugAction("MaskFailure")
            ;


        StartGameTime = _StartGameTime;
        LaneUpdateTime = _LaneUpdateTime;
        PrevLaneDistributionTime = _PrevLaneDistributionTime;
        EnemyStrengthTop = _EnemyStrengthTop;
        EnemyStrengthMid = _EnemyStrengthMid;
        EnemyStrengthBot = _EnemyStrengthBot;
        FriendlyStrengthTop = _FriendlyStrengthTop;
        FriendlyStrengthBot = _FriendlyStrengthBot;
        FriendlyStrengthMid = _FriendlyStrengthMid;
        Squad_PushBot = _Squad_PushBot;
        Squad_PushMid = _Squad_PushMid;
        Squad_PushTop = _Squad_PushTop;
        Squad_WaitAtBase = _Squad_WaitAtBase;
        Mission_PushBot = _Mission_PushBot;
        Mission_PushMid = _Mission_PushMid;
        Mission_PushTop = _Mission_PushTop;
        Mission_WaitAtBase = _Mission_WaitAtBase;
        PointValue_Champion = _PointValue_Champion;
        PointValue_Minion = _PointValue_Minion;
        DistributionCount = _DistributionCount;
        DynamicDistributionStartTime = _DynamicDistributionStartTime;
        DynamicDistributionUpdateTime = _DynamicDistributionUpdateTime;
        UpdateGoldXP = _UpdateGoldXP;
        DisconnectAdjustmentEnabled = _DisconnectAdjustmentEnabled;
        DisconnectAdjustmentEntityID = _DisconnectAdjustmentEntityID;
        TotalDeadTurrets = _TotalDeadTurrets;
        DifficultyScaling_IsWinState = _DifficultyScaling_IsWinState;
        IsDifficultySet = _IsDifficultySet;
        OverrideDifficulty = _OverrideDifficulty;

        return result;
    }
}

