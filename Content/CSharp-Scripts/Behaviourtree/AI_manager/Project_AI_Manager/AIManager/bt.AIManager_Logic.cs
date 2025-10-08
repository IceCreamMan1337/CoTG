using AIScripts;
using BehaviourTrees.Map8;

namespace BehaviourTrees;


class AIManager_LogicClass : CommonAI
{
    float lastTimeExecuted_EP_winLossState;
    float lastTimeExecuted_EP_disconnectAdjustmentManager;

    private InitializationClass _initialization;
    private WinLossStateClass _winLossState;
    private BeginnerOverrideDifficulty_TurretDestructionClass _beginnerOverrideDifficulty_TurretDestruction;
    private DifficultySettingClass _difficultySetting;
    private ReferenceUpdateGlobalClass _referenceUpdateGlobal;
    private LevelNormalizerClass _levelNormalizer;
    private DisconnectAdjustmentManagerClass _disconnectAdjustmentManager;
    private Lane_OptimalDistributionClass _lane_OptimalDistribution;
    private StaticLaneDistributionClass _staticLaneDistribution;

    private List<Func<bool>> _EP_winLossState;
    private List<Func<bool>> _EP_disconnectAdjustmentManager;

    public bool AIManager_Logic()
    {
        _initialization ??= new InitializationClass();
        _winLossState ??= new WinLossStateClass();
        _beginnerOverrideDifficulty_TurretDestruction ??= new BeginnerOverrideDifficulty_TurretDestructionClass();
        _difficultySetting ??= new DifficultySettingClass();
        _referenceUpdateGlobal ??= new ReferenceUpdateGlobalClass();
        _levelNormalizer ??= new LevelNormalizerClass();
        _disconnectAdjustmentManager ??= new DisconnectAdjustmentManagerClass();
        _lane_OptimalDistribution ??= new Lane_OptimalDistributionClass();
        _staticLaneDistribution ??= new StaticLaneDistributionClass();

        var initialization = _initialization;
        var winLossState = _winLossState;
        var beginnerOverrideDifficulty_TurretDestruction = _beginnerOverrideDifficulty_TurretDestruction;
        var difficultySetting = _difficultySetting;
        var referenceUpdateGlobal = _referenceUpdateGlobal;
        var levelNormalizer = _levelNormalizer;
        var disconnectAdjustmentManager = _disconnectAdjustmentManager;
        var lane_OptimalDistribution = _lane_OptimalDistribution;
        var staticLaneDistribution = _staticLaneDistribution;

        if (_EP_winLossState == null)
        {
            _EP_winLossState = new List<Func<bool>>
            { () =>
                {
                    return winLossState.WinLossState(out DifficultyScaling_IsWinState,
                                                      out IsDifficultySet,
                                                      ReferenceUnitTeam,
                                                      DifficultyScaling_IsWinState,
                                                      IsDifficultySet);
                }
            };
        }

        if (_EP_disconnectAdjustmentManager == null)
        {
            _EP_disconnectAdjustmentManager = new List<Func<bool>>
            { () =>
                {
                    return disconnectAdjustmentManager.DisconnectAdjustmentManager(
                                            out DisconnectAdjustmentEntityID,
                                            ReferenceUnit,
                                            DisconnectAdjustmentEnabled,
                                            DisconnectAdjustmentEntityID,
                                            Squad_WaitAtBase,
                                            AllEntities);
                }
            };
        }


        return
        // Sequence name :AIManager_Logic
        (
            // Sequence name :MaskFailure
            (
                // Sequence name :Init
                (
                    TestAIFirstTime(true) &&
                    initialization.Initialization(
                        out StartGameTime,
                        out LaneUpdateTime,
                        out EnemyStrengthTop,
                        out EnemyStrengthMid,
                        out EnemyStrengthBot,
                        out FriendlyStrengthTop,
                        out FriendlyStrengthBot,
                        out FriendlyStrengthMid,
                        out Squad_PushBot,
                        out Squad_PushMid,
                        out Squad_PushTop,
                        out Squad_WaitAtBase,
                        out Mission_PushBot,
                        out Mission_PushMid,
                        out Mission_PushTop,
                        out Mission_WaitAtBase,
                        out PrevLaneDistributionTime,
                        out PointValue_Champion,  //Champion ??? 
                        out PointValue_Minion,  // Minion ???
                        out DistributionCount,
                        out DynamicDistributionStartTime,
                        out DynamicDistributionUpdateTime,
                        out UpdateGoldXP,
                        out DisconnectAdjustmentEnabled,
                        out DisconnectAdjustmentEntityID,
                        out TotalDeadTurrets,
                        out DifficultyScaling_IsWinState,
                        out IsDifficultySet,
                        out OverrideDifficulty
                        ) &&
                        CreateAISquad(out KillSquad,"",5)
                    ) ||
                    DebugAction("MaskFailure")
                ) &&
                // Sequence name :HasEntities
                (
                    GetAIManagerEntities(out AllEntities) &&
                    ForEach(AllEntities, Entity =>
                        // Sequence name :Sequence
                        SetVarAttackableUnit(out ReferenceUnit, Entity) &&
                        GetUnitTeam(out ReferenceUnitTeam, ReferenceUnit)
                    )
                ) &&
                // Sequence name :SetDiffcultyIndex
                (
                    // Sequence name :Beginner
                    (
                            TestAITeamDifficulty(GameDifficultyType.GAME_DIFFICULTY_NEWBIE, true) &&
                            SetVarInt(out DifficultyIndex, 0)
                    ) ||
                    // Sequence name :Intermediate
                    (
                            TestAITeamDifficulty(GameDifficultyType.GAME_DIFFICULTY_INTERMEDIATE, true) &&
                            SetVarInt(out DifficultyIndex, 1)
                    ) ||
                    // Sequence name :Advanced
                    (
                            TestAITeamDifficulty(GameDifficultyType.GAME_DIFFICULTY_ADVANCED, true) &&
                            SetVarInt(out DifficultyIndex, 2)
                    )
                ) &&
                // Sequence name :MaskFailure
                (
                    // Sequence name :DifficultySetting
                    (
                        DifficultyIndex == 0 && ExecutePeriodically(ref lastTimeExecuted_EP_winLossState, _EP_winLossState, true, 10)
                    ) ||
                    DebugAction("MaskFailure")
                ) &&
                beginnerOverrideDifficulty_TurretDestruction.BeginnerOverrideDifficulty_TurretDestruction(
                    out OverrideDifficulty,
                    out IsDifficultySet,
                    OverrideDifficulty,
                    ReferenceUnitTeam,
                    DynamicDistributionStartTime,
                    IsDifficultySet) &&
                difficultySetting.DifficultySetting(
                    out DynamicDistributionStartTime,
                    out DynamicDistributionUpdateTime,
                    out IsDifficultySet,
                    DifficultyIndex,
                    DifficultyScaling_IsWinState,
                    DynamicDistributionStartTime,
                    IsDifficultySet,
                    OverrideDifficulty) &&
                referenceUpdateGlobal.ReferenceUpdateGlobal(
                    out LaneUpdateTime,
                    out EnemyStrengthTop,
                    out EnemyStrengthBot,
                    out EnemyStrengthMid,
                    out FriendlyStrengthTop,
                    out FriendlyStrengthMid,
                    out FriendlyStrengthBot,
                    LaneUpdateTime,
                    ReferenceUnit,
                    EnemyStrengthTop,
                    EnemyStrengthMid,
                    EnemyStrengthBot,
                    FriendlyStrengthTop,
                    FriendlyStrengthMid,
                    FriendlyStrengthBot,
                    PointValue_Champion,
                    PointValue_Minion) &&
                levelNormalizer.LevelNormalizer(out UpdateGoldXP, UpdateGoldXP, ReferenceUnit, DifficultyIndex) &&
                DebugAction("EP_disconnectAdjustmentManager IMPORTANT") &&
                ExecutePeriodically(ref lastTimeExecuted_EP_disconnectAdjustmentManager, _EP_disconnectAdjustmentManager, true, 10) &&
                // Sequence name :Sequence
                // Sequence name :MaskFailure
                (
                    // Sequence name :Sequence
                    (
                        PeekTeamMessage(out SourceUnit, out TargetUnit, out AITaskTopic, out EventString, true) &&
                        // Sequence name :MessageType
                        (
                            // Sequence name :HelpEvent
                            (
                                EventString == "Help" &&
                                GetUnitPosition(out SourceUnitPosition, SourceUnit) &&
                                // Sequence name :AreUnitsAlive?
                                (
                                    // Sequence name :Alive
                                    (
                                        TestUnitCondition(SourceUnit) &&
                                        TestUnitCondition(TargetUnit) &&
                                        GetDistanceBetweenUnits(out DistanceBetweenEventUnits, SourceUnit, TargetUnit) &&
                                        LessFloat(DistanceBetweenEventUnits, 1500) &&
                                        // Sequence name :SendHelpIfAvailable
                                        (
                                            GetUnitsInTargetArea(
                                                out FriendlyChampsNearby,
                                                SourceUnit,
                                                SourceUnitPosition,
                                                1500,
                                                SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf) &&
                                            GetCollectionCount(out NumFriendlyChampsNearby, FriendlyChampsNearby) &&
                                            GreaterInt(NumFriendlyChampsNearby, 0) &&
                                            CreateAIMission(out KillMission, AIMissionTopicType.KILL, TargetUnit, default) &&
                                            AssignAIMission(KillSquad, KillMission) &&
                                            ForEach(FriendlyChampsNearby, FriendlyChamp =>
                                                // Sequence name :Sequence
                                                AddAIEntityToSquad(FriendlyChamp, KillSquad)
                                            )
                                        )
                                    ) ||
                                    // Sequence name :NotAlive
                                    (
                                        GetUnitsInTargetArea(
                                            out FriendlyChampsNearby,
                                            SourceUnit,
                                            SourceUnitPosition,
                                            1500,
                                            SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes) &&
                                        // Sequence name :Sequence
                                        ForEach(FriendlyChampsNearby, FriendlyChamp =>
                                            TestAIEntityHasTask(FriendlyChamp, AITaskTopicType.ASSIST, null, default) &&
                                            GetUnitAIClosestLaneID(out LaneID, FriendlyChamp) &&
                                            // Sequence name :AssignToNearestLane
                                            (
                                                // Sequence name :Bot
                                                (LaneID == 0 && AddAIEntityToSquad(FriendlyChamp, Squad_PushBot)) ||
                                                // Sequence name :Mid
                                                (LaneID == 1 && AddAIEntityToSquad(FriendlyChamp, Squad_PushMid)) ||
                                                // Sequence name :Top
                                                (LaneID == 2 && AddAIEntityToSquad(FriendlyChamp, Squad_PushTop))
                                            )
                                        )
                                    )
                                )
                            ) ||
                            // Sequence name :DoneEvent
                            (
                                EventString == "Done" &&
                                GetUnitPosition(out SourceUnitPosition, SourceUnit) &&
                                GetUnitsInTargetArea(
                                    out FriendlyChampsNearby,
                                    SourceUnit,
                                    SourceUnitPosition,
                                    2000,
                                    SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes
                                ) &&
                                // Sequence name :Sequence
                                ForEach(FriendlyChampsNearby, FriendlyChamp =>
                                    TestAIEntityHasTask(FriendlyChamp, AITaskTopicType.ASSIST, null, default) &&
                                    GetUnitAIClosestLaneID(out LaneID, FriendlyChamp) &&
                                    // Sequence name :AssignToNearestLane
                                    (
                                        // Sequence name :Bot
                                        (LaneID == 0 && AddAIEntityToSquad(FriendlyChamp, Squad_PushBot)) ||
                                        // Sequence name :Mid
                                        (LaneID == 1 && AddAIEntityToSquad(FriendlyChamp, Squad_PushMid)) ||
                                        // Sequence name :Top
                                        (LaneID == 2 && AddAIEntityToSquad(FriendlyChamp, Squad_PushTop))
                                    )
                                )
                            ) ||
                            // Sequence name :PushEvent
                            (
                                EventString == "Push" &&
                                GetUnitAIClosestLaneID(out LaneID, SourceUnit) &&
                                // Sequence name :AssignToNearestLane
                                (
                                    // Sequence name :Bot
                                    (LaneID == 0 && AddAIEntityToSquad(SourceUnit, Squad_PushBot)) ||
                                    // Sequence name :Mid
                                    (LaneID == 1 && AddAIEntityToSquad(SourceUnit, Squad_PushMid)) ||
                                    // Sequence name :Top
                                    (LaneID == 2 && AddAIEntityToSquad(SourceUnit, Squad_PushTop))
                                )
                            )
                        )
                    ) ||
                    DebugAction("MaskFailure")
                ) &&
                GetGameTime(out CurrentGameTime) &&
                SubtractFloat(out TimeDiff, CurrentGameTime, StartGameTime) &&
                // Sequence name :LaneDistribution
                (
                    // Sequence name :EarlyGameLaneDistribution
                    (
                        LessEqualFloat(TimeDiff, DynamicDistributionStartTime) &&
                        staticLaneDistribution.StaticLaneDistribution(
                            out Squad_PushBot,
                            out Squad_PushMid,
                            out Squad_PushTop,
                            AllEntities,
                            DisconnectAdjustmentEntityID,
                            Squad_PushBot,
                            Squad_PushMid,
                            Squad_PushTop
                        )
                    ) ||
                    // Sequence name :MidGame
                    (
                        SubtractFloat(out LaneUpdateTimeDiff, CurrentGameTime, PrevLaneDistributionTime) &&
                        GreaterFloat(LaneUpdateTimeDiff, DynamicDistributionUpdateTime) &&
                        lane_OptimalDistribution.Lane_OptimalDistribution(
                            EnemyStrengthTop,
                            EnemyStrengthMid,
                            EnemyStrengthBot,
                            FriendlyStrengthTop,
                            FriendlyStrengthMid,
                            FriendlyStrengthBot,
                            AllEntities,
                            Squad_PushTop,
                            Squad_PushMid,
                            Squad_PushBot,
                            100,
                            DisconnectAdjustmentEntityID
                        ) &&
                        SetVarFloat(out PrevLaneDistributionTime, CurrentGameTime) &&
                        SetVarBool(out IsDifficultySet, false)
                    )
                )
        //todo: check parenthesis 
            );





    }
}