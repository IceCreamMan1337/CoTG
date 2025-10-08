using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using BehaviourTrees.Map8;

namespace BehaviourTrees;


class CustomGameManager_LogicClass : CommonAI
{
    private InitializationcustomgameClass initializationcustomgame = new InitializationcustomgameClass();
    private WinLossStateClass winLossState = new WinLossStateClass();
    private BeginnerOverrideDifficulty_TurretDestructionClass beginnerOverrideDifficulty_TurretDestruction = new BeginnerOverrideDifficulty_TurretDestructionClass();
    private DifficultySettingClass difficultySetting = new DifficultySettingClass();
    private ReferenceUpdateGlobalClass referenceUpdateGlobal = new ReferenceUpdateGlobalClass();
    private LevelNormalizerClass levelNormalizer = new LevelNormalizerClass();
    private DisconnectAdjustmentManagerClass disconnectAdjustmentManager = new DisconnectAdjustmentManagerClass();
    private Lane_OptimalDistributionClass lane_OptimalDistribution = new Lane_OptimalDistributionClass();
    private StaticLaneDistributionClass  staticLaneDistribution = new StaticLaneDistributionClass();
    private AssignToLaneClass assignToLane = new AssignToLaneClass();
    private ConformLanesToPlayersClass conformLanesToPlayers = new ConformLanesToPlayersClass();
    private CustomGameStaticLaneDistributionClass customGameStaticLaneDistribution = new CustomGameStaticLaneDistributionClass();    public bool CustomGameManager_Logic() { 
      
        
        return (
            // Sequence name :CustomGameManager_Logic
            (
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Init
                        (
                              TestAIFirstTime(
                                    true) &&
                             initializationcustomgame.Initializationcustomgame(
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
                              SetVarBool(
                                    out PlayersOnTeam, 
                                    false) &&
                              SetVarInt(
                                    out Bot1Lane, 
                                    0) &&
                              SetVarInt(
                                    out Bot2Lane, 
                                    2) &&
                              SetVarInt(
                                    out Bot3Lane, 
                                    1) &&
                              SetVarInt(
                                    out Bot4Lane, 
                                    0) &&
                              SetVarBool(
                                    out TeamInit, 
                                    false)
                        )
                  ) &&
                  // Sequence name :HasEntities
                  (
                        GetAIManagerEntities(
                              out AllEntities) &&
                        ForEach(AllEntities, Entity => (
                              // Sequence name :Sequence
                              (
                                    SetVarAttackableUnit(
                                          out ReferenceUnit, 
                                          Entity) &&
                                    GetUnitTeam(
                                          out ReferenceUnitTeam, 
                                          ReferenceUnit)
                              ))
                        )
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :PlayingWithPlayers?
                        (
                              TeamInit == false &&
                              GetChampionCollection(
                                    out ChampCollection) &&
                              SetVarInt(
                                    out ChampCount, 
                                    0) &&
                              SetVarInt(
                                    out BotCount, 
                                    0) &&
                              ForEach(ChampCollection, Champ => (                                    // Sequence name :Sequence
                                    (
                                          GetUnitTeam(
                                                out ChampTeam, 
                                                Champ) &&
                                          ChampTeam == ReferenceUnitTeam &&
                                          AddInt(
                                                out ChampCount, 
                                                ChampCount, 
                                                1)
                                    ))
                              ) &&
                              ForEach(AllEntities,Entity => (                                    // Sequence name :Sequence
                                    (
                                          GetUnitTeam(
                                                out EntityTeam, 
                                                Entity) &&
                                          EntityTeam == ReferenceUnitTeam &&
                                          AddInt(
                                                out BotCount, 
                                                BotCount, 
                                                1)
                                    ))
                              ) &&
                              SetVarBool(
                                    out TeamInit, 
                                    true) &&
                              NotEqualInt(
                                    ChampCount, 
                                    BotCount) &&
                              GreaterInt(
                                    BotCount, 
                                    0) &&
                              SetVarBool(
                                    out PlayersOnTeam, 
                                    true)
                        )
                  ) &&
                  SetVarInt(
                        out DifficultyIndex, 
                        0) &&
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
             levelNormalizer.LevelNormalizer(
                        out UpdateGoldXP, 
                        UpdateGoldXP, 
                        ReferenceUnit, 
                        DifficultyIndex) &&
                  GetGameTime(
                        out CurrentGameTime) &&
                  SubtractFloat(
                        out TimeDiff, 
                        CurrentGameTime, 
                        StartGameTime) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :ConformLaningToPlayerPositioning
                        (
                              LessFloat(
                                    CurrentGameTime, 
                                    90) &&
                              PlayersOnTeam == true &&
                          conformLanesToPlayers.ConformLanesToPlayers(
                                    out Bot1Lane, 
                                    out Bot2Lane, 
                                    out Bot3Lane, 
                                    out Bot4Lane, 
                                    AllEntities, 
                                    ReferenceUnitTeam)
                        )
                  ) &&
                  // Sequence name :LaneDistribution
                  (
                        // Sequence name :EarlyGameLaneDistribution
                        (
                              LessEqualFloat(
                                    TimeDiff, 
                                    DynamicDistributionStartTime) &&
                          customGameStaticLaneDistribution. CustomGameStaticLaneDistribution(
                                    out Squad_PushBot, 
                                    out Squad_PushMid, 
                                    out Squad_PushTop, 
                                    out Bot1Lane, 
                                    out Bot2Lane, 
                                    out Bot3Lane, 
                                    out Bot4Lane, 
                                    AllEntities, 
                                    Squad_PushBot, 
                                    Squad_PushMid, 
                                    Squad_PushTop, 
                                    Bot1Lane, 
                                    Bot2Lane, 
                                    Bot3Lane, 
                                    Bot4Lane)
                        ) ||
                        // Sequence name :MidGame
                        (
                              SubtractFloat(
                                    out LaneUpdateTimeDiff, 
                                    CurrentGameTime, 
                                    PrevLaneDistributionTime) &&
                              GreaterFloat(
                                    LaneUpdateTimeDiff, 
                                    DynamicDistributionUpdateTime) &&
                             lane_OptimalDistribution. Lane_OptimalDistribution(
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
                                DisconnectAdjustmentEntityID) &&
                              SetVarFloat(
                                    out PrevLaneDistributionTime, 
                                    CurrentGameTime) &&
                              SetVarBool(
                                    out IsDifficultySet,
                                    false)

                        )
                  ))
            );
      }
}

