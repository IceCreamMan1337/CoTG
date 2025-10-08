using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class DominionAIManager_LogicClass : bt_OdinManager
{

    public bool DominionAIManager_Logic()
    {


        var updateDistributionTime_Initialization = new UpdateDistributionTime_InitializationClass();
        var countCapturePointsOwned = new CountCapturePointsOwnedClass();
        var goldScaling = new GoldScalingClass();
        var updateDistributionTime_Helper = new UpdateDistributionTime_HelperClass();
        var assignMissions = new AssignMissionsClass();

        return
                  // Sequence name :Sequence

                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Initialization
                        (
                              TestAIFirstTime(
                                    true) &&
                              GetWorldLocationByName(
                                    out CapturePointAPosition,
                                    "CapturePointA") &&
                              GetWorldLocationByName(
                                    out CapturePointBPosition,
                                    "CapturePointB") &&
                              GetWorldLocationByName(
                                    out CapturePointCPosition,
                                    "CapturePointC") &&
                              GetWorldLocationByName(
                                    out CapturePointDPosition,
                                    "CapturePointD") &&
                              GetWorldLocationByName(
                                    out CapturePointEPosition,
                                    "CapturePointE") &&
                              CreateAISquad(
                                    out CapturePointASquad,
                                    "CapturePointASquad",
                                    5) &&
                              CreateAISquad(
                                    out CapturePointBSquad,
                                    "CapturePointBSquad",
                                    5) &&
                              CreateAISquad(
                                    out CapturePointCSquad,
                                    "CapturePointCSquad",
                                    5) &&
                              CreateAISquad(
                                    out CapturePointDSquad,
                                    "CapturePointDSquad",
                                    5) &&
                              CreateAISquad(
                                    out CapturePointESquad,
                                    "CapturePointESquad", 5) &&
                              CreateAISquad(
                                    out WaitInBaseSquad,
                                    "WaitInBaseSquad", 5) &&
                              CreateAISquad(
                                    out KillSquad,
                                    "KillSquad", 5) &&
                              CreateAIMission(
                                    out CapturePointA_mission,
                                    AIMissionTopicType.SUPPORT,
                                    null,
                                    CapturePointAPosition
                                    ) &&
                              CreateAIMission(
                                    out CapturePointB_mission,
                                    AIMissionTopicType.SUPPORT,
                                    null,
                                    CapturePointBPosition
                                    ) &&
                              CreateAIMission(
                                    out CapturePointC_mission,
                                    AIMissionTopicType.SUPPORT,
                                    null,
                                    CapturePointCPosition
                                    ) &&
                              CreateAIMission(
                                    out CapturePointD_mission,
                                    AIMissionTopicType.SUPPORT,
                                    null,
                                    CapturePointDPosition
                                    ) &&
                              CreateAIMission(
                                    out CapturePointE_mission,
                                    AIMissionTopicType.SUPPORT,
                                    null,
                                    CapturePointEPosition
                                    ) &&
                              CreateAIMission(
                                    out WaitInBaseMission,
                                    AIMissionTopicType.DEFEND,
                                    null,
                                    default
                                    ) &&
                              AssignAIMission(
                                    CapturePointASquad,
                                    CapturePointA_mission) &&
                              AssignAIMission(
                                    CapturePointBSquad,
                                    CapturePointB_mission) &&
                              AssignAIMission(
                                    CapturePointCSquad,
                                    CapturePointC_mission) &&
                              AssignAIMission(
                                    CapturePointDSquad,
                                    CapturePointD_mission) &&
                              AssignAIMission(
                                    CapturePointESquad,
                                    CapturePointE_mission) &&
                              AssignAIMission(
                                    WaitInBaseSquad,
                                    WaitInBaseMission) &&
                              SetVarFloat(
                                    out MinionPointValue,
                                    20) &&
                              SetVarFloat(
                                    out ChampionPointValue,
                                    100) &&
                              SetVarFloat(
                                    out CPPointValue,
                                    0) &&
                              SetVarBool(
                                    out CapturePointsFound,
                                    false) &&
                              SetVarFloat(
                                    out StrengthEvaluatorRadius,
                                    1600) &&
                              SetVarBool(
                                    out Opened,
                                    false) &&
                              SetVarInt(
                                    out UpdateLimit,
                                    1) &&
                              SetVarInt(
                                    out PreviousEnemyCPCount,
                                    0) &&
                              SetVarInt(
                                    out PreviousNeutralCPCount,
                                    0) &&
                              SetVarInt(
                                    out PreviousOwnedCPCount,
                                    0) &&
                              SetVarBool(
                                    out DisconnectAdjustmentEnabled,
                                    true) &&
                              updateDistributionTime_Initialization.UpdateDistributionTime_Initialization(
                                    out IsBeingCapturedA,
                                    out IsBeingCapturedB,
                                    out IsBeingCapturedC,
                                    out IsBeingCapturedD,
                                    out IsBeingCapturedE) &&
                              SetVarFloat(
                                    out NextUpdateTime,
                                    0) &&
                              SetVarFloat(
                                    out LastGoldScalingUpdateTime,
                                    0) &&
                              // Sequence name :SetDifficultyIndex
                              (
                                    // Sequence name :Beginner
                                    (
                                          TestAITeamDifficulty(
                                                GameDifficultyType.GAME_DIFFICULTY_NEWBIE,
                                                true) &&
                                          SetVarInt(
                                                out DifficultyIndex,
                                                0)
                                    ) ||
                                    // Sequence name :Intermediate
                                    (
                                          TestAITeamDifficulty(
                                                GameDifficultyType.GAME_DIFFICULTY_INTERMEDIATE,
                                                true) &&
                                          SetVarInt(
                                                out DifficultyIndex,
                                                1)
                                    ) ||
                                    // Sequence name :Advanced
                                    (
                                          TestAITeamDifficulty(
                                                GameDifficultyType.GAME_DIFFICULTY_ADVANCED,
                                                true) &&
                                          SetVarInt(
                                                out DifficultyIndex,
                                                2)
                                    )
                              ) &&
                              SetVarBool(
                                    out ManuallyForceUpdate,
                                    false)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                        // Sequence name :HasEntities

                        GetAIManagerEntities(
                              out AllEntities) &&
                        ForEach(AllEntities, Entity =>

                                SetVarAttackableUnit(
                                    out ReferenceUnit,
                                    Entity)
                        ) &&
                        TestUnitHasBuff(
                              ReferenceUnit,
                              null,
                              "OdinGuardianStatsByLevel", false) &&
                        GetUnitTeam(
                              out ReferenceTeam,
                              ReferenceUnit)
                   &&
                  // Sequence name :Init_GetCapturePoints
                  (
                        CapturePointsFound == true ||
                        // Sequence name :GetCapturePoints
                        (
                              GetUnitsInTargetArea(
                                    out CapPointAColl,
                                    ReferenceUnit,
                                    CapturePointAPosition,
                                    500,
                                    AffectEnemies | AffectFriends | AffectMinions | AffectNeutral | AffectUseable
                                    ) &&
                              GetUnitsInTargetArea(
                                    out CapPointBColl,
                                    ReferenceUnit,
                                    CapturePointBPosition,
                                    500,
                                    AffectEnemies | AffectFriends | AffectMinions | AffectNeutral | AffectUseable) &&
                              GetUnitsInTargetArea(
                                    out CapPointCColl,
                                    ReferenceUnit,
                                    CapturePointCPosition,
                                    500,
                                    AffectEnemies | AffectFriends | AffectMinions | AffectNeutral | AffectUseable) &&
                              GetUnitsInTargetArea(
                                    out CapPointDColl,
                                    ReferenceUnit,
                                    CapturePointDPosition,
                                    500,
                                    AffectEnemies | AffectFriends | AffectMinions | AffectNeutral | AffectUseable) &&
                              GetUnitsInTargetArea(
                                    out CapPointEColl,
                                    ReferenceUnit,
                                    CapturePointEPosition,
                                    500,
                                    AffectEnemies | AffectFriends | AffectMinions | AffectNeutral | AffectUseable) &&
                              ForEach(CapPointAColl, CapturePoint =>
                              SetVarAttackableUnit(
                                          out CapturePointA,
                                          CapturePoint)
                              ) &&

                              ForEach(CapPointBColl, CapturePoint =>
                              SetVarAttackableUnit(
                                          out CapturePointB,
                                          CapturePoint)
                              ) &&
                              ForEach(CapPointCColl, CapturePoint =>
                              SetVarAttackableUnit(
                                          out CapturePointC,
                                          CapturePoint)
                              ) &&
                              ForEach(CapPointDColl, CapturePoint =>
                              SetVarAttackableUnit(
                                          out CapturePointD,
                                          CapturePoint)
                              ) &&
                              ForEach(CapPointEColl, CapturePoint =>
                              SetVarAttackableUnit(
                                          out CapturePointE,
                                          CapturePoint)
                              ) &&
                              SetVarBool(
                                    out CapturePointsFound,
                                    true)
                        )
                  ) &&
                  countCapturePointsOwned.CountCapturePointsOwned(
                        out EnemyCPCount,
                        out OwnedCPCount,
                        out NeutralCPCount,
                        CapturePointA,
                        CapturePointB,
                        CapturePointC,
                        CapturePointD,
                        CapturePointE,
                        ReferenceTeam) &&
                   goldScaling.GoldScaling(
                         out LastGoldScalingUpdateTime,
                         DifficultyIndex,
                         LastGoldScalingUpdateTime,
                         ReferenceUnit) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              PeekTeamMessage(
                                    out SourceUnit,
                                    out TargetUnit,
                                    out AITaskTopic,
                                    out EventString,
                                    true) &&
                              // Sequence name :MessageType
                              (
                                    // Sequence name :HelpEvent
                                    (
                                          EventString == "Help" &&
                                          GetUnitPosition(
                                                out SourceUnitPosition,
                                                SourceUnit) &&
                                          // Sequence name :AreUnitsAlive?
                                          (
                                                // Sequence name :Alive
                                                (
                                                      TestUnitCondition(
                                                            SourceUnit) &&
                                                      TestUnitCondition(
                                                            TargetUnit) &&
                                                      GetDistanceBetweenUnits(
                                                            out DistanceBetweenEventUnits,
                                                            SourceUnit,
                                                            TargetUnit) &&
                                                      LessFloat(
                                                            DistanceBetweenEventUnits,
                                                            1500) &&
                                                            // Sequence name :SendHelpIfAvailable

                                                            GetUnitsInTargetArea(
                                                                  out FriendlyChampsNearby,
                                                                  SourceUnit,
                                                                  SourceUnitPosition,
                                                                  1500,
                                                                  AffectFriends | AffectHeroes | NotAffectSelf
                                                                  ) &&
                                                            GetCollectionCount(
                                                                  out NumFriendlyChampsNearby,
                                                                  FriendlyChampsNearby) &&
                                                            GreaterInt(
                                                                  NumFriendlyChampsNearby,
                                                                  0) &&
                                                            CreateAIMission(
                                                                  out KillMission,
                                                                 AIMissionTopicType.KILL,
                                                                  TargetUnit,
                                                                  default
                                                                  ) &&
                                                            AssignAIMission(
                                                                  KillSquad,
                                                                  KillMission) &&
                                                            ForEach(FriendlyChampsNearby, FriendlyChamp =>
                                                                        // Sequence name :Sequence

                                                                        AddAIEntityToSquad(
                                                                              FriendlyChamp,
                                                                              KillSquad)

                                                            )

                                                ) ||
                                                // Sequence name :NotAlive
                                                (
                                                      GetUnitsInTargetArea(
                                                            out FriendlyChampsNearby,
                                                            SourceUnit,
                                                            SourceUnitPosition,
                                                            1500,
                                                            AffectFriends | AffectHeroes) &&
                                                      ForEach(FriendlyChampsNearby, FriendlyChamp =>
                                                                  // Sequence name :Sequence

                                                                  TestAIEntityHasTask(
                                                                        FriendlyChamp,
                                                                       AITaskTopicType.ASSIST,
                                                                        null,
                                                                        default,
                                                                        0,
                                                                        true) &&
                                                                  AddAIEntityToSquad(
                                                                        FriendlyChamp,
                                                                        CapturePointCSquad) &&
                                                                  SetVarBool(
                                                                        out ManuallyForceUpdate,
                                                                        true)

                                                      )
                                                )
                                          )
                                    ) ||
                                    // Sequence name :DoneEvent
                                    (
                                          EventString == "Done" &&
                                          GetUnitPosition(
                                                out SourceUnitPosition,
                                                SourceUnit) &&
                                          GetUnitsInTargetArea(
                                                out FriendlyChampsNearby,
                                                SourceUnit,
                                                SourceUnitPosition,
                                                2000,
                                                AffectFriends | AffectHeroes) &&
                                          ForEach(FriendlyChampsNearby, FriendlyChamp =>
                                                      // Sequence name :Sequence

                                                      TestAIEntityHasTask(
                                                            FriendlyChamp,
                                                                       AITaskTopicType.ASSIST,
                                                                        null,
                                                                        default,
                                                                        0,
                                                                        true) &&
                                                      AddAIEntityToSquad(
                                                            FriendlyChamp,
                                                            CapturePointCSquad) &&
                                                      SetVarBool(
                                                            out ManuallyForceUpdate,
                                                            true)

                                          )
                                    )
                              )
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :ForceGameToOpen
                        (
                              NotEqualBool(
                                    Opened,
                                    true) &&
                              GetGameTime(
                                    out GameTime) &&
                              // Sequence name :Conditions
                              (
                                    GreaterEqualFloat(
                                          GameTime,
                                          110)
                                    ||
                                    GreaterEqualInt(
                                          OwnedCPCount,
                                          2)
                              ) &&
                              SetVarBool(
                                    out Opened,
                                    true)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :GameLogic
                  (
                        // Sequence name :FixedOpeningStrategy
                        (
                              NotEqualBool(
                                    Opened,
                                    true) &&
                              SetVarInt(
                                    out BotIndex,
                                    0) &&
                              ForEach(AllEntities, Entity =>
                                          // Sequence name :Selector

                                          TestUnitHasBuff(
                                                Entity,
                                                null,
                                                "OdinGuardianStatsByLevel")
                                          ||
                                          TestUnitCondition(
                                                Entity, false)
                                          ||
                                          // Sequence name :Sequence
                                          (
                                                // Sequence name :Selector
                                                (
                                                      // Sequence name :Bot0ToBottom
                                                      (
                                                            BotIndex == 0 &&
                                                            // Sequence name :Selector
                                                            (
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        ReferenceTeam == TeamId.TEAM_ORDER &&
                                                                        AddAIEntityToSquad(
                                                                              Entity,
                                                                              CapturePointASquad)
                                                                  ) ||
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        ReferenceTeam == TeamId.TEAM_CHAOS &&
                                                                        AddAIEntityToSquad(
                                                                              Entity,
                                                                              CapturePointESquad)
                                                                  )
                                                            )
                                                      ) ||
                                                      // Sequence name :Bot1ToMid
                                                      (
                                                            BotIndex == 1 &&
                                                            // Sequence name :Selector
                                                            (
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        ReferenceTeam == TeamId.TEAM_ORDER &&
                                                                        AddAIEntityToSquad(
                                                                              Entity,
                                                                              CapturePointBSquad)
                                                                  ) ||
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        ReferenceTeam == TeamId.TEAM_CHAOS &&
                                                                        AddAIEntityToSquad(
                                                                              Entity,
                                                                              CapturePointDSquad)
                                                                  )
                                                            )
                                                      ) ||
                                                            // Sequence name :AllOtherBotsToTop

                                                            AddAIEntityToSquad(
                                                                  Entity,
                                                                  CapturePointCSquad)

                                                ) &&
                                                AddInt(
                                                      out BotIndex,
                                                      BotIndex,
                                                      1)
                                          )

                              )
                        ) ||
                        // Sequence name :DynamicDistribution
                        (
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :ForceUpdateIfAllyPointIsBeingCaptured
                                    (
                                          NotEqualInt(
                                                DifficultyIndex,
                                                0) &&
                                          updateDistributionTime_Helper.UpdateDistributionTime_Helper(
                                                out IsBeingCapturedA,
                                                out NextUpdateTime,
                                                ReferenceTeam,
                                                CapturePointA,
                                                IsBeingCapturedA,
                                                NextUpdateTime) &&
                                          updateDistributionTime_Helper.UpdateDistributionTime_Helper(
                                                out IsBeingCapturedB,
                                                out NextUpdateTime,
                                                ReferenceTeam,
                                                CapturePointB,
                                                IsBeingCapturedB,
                                                NextUpdateTime) &&
                                          updateDistributionTime_Helper.UpdateDistributionTime_Helper(
                                                out IsBeingCapturedC,
                                                out NextUpdateTime,
                                                ReferenceTeam,
                                                CapturePointC,
                                                IsBeingCapturedC,
                                                NextUpdateTime) &&
                                          updateDistributionTime_Helper.UpdateDistributionTime_Helper(
                                                out IsBeingCapturedD,
                                                out NextUpdateTime,
                                                ReferenceTeam,
                                                CapturePointD,
                                                IsBeingCapturedD,
                                                NextUpdateTime) &&
                                          updateDistributionTime_Helper.UpdateDistributionTime_Helper(
                                                out IsBeingCapturedE,
                                                out NextUpdateTime,
                                                ReferenceTeam,
                                                CapturePointE,
                                                IsBeingCapturedE,
                                                NextUpdateTime)
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              assignMissions.AssignMissions(
                                    out PreviousOwnedCPCount,
                                    out PreviousNeutralCPCount,
                                    out PreviousEnemyCPCount,
                                    out NextUpdateTime,
                                    out ManuallyForceUpdate,
                                    OwnedCPCount,
                                    NeutralCPCount,
                                    EnemyCPCount,
                                    CapturePointA,
                                    CapturePointB,
                                    CapturePointC,
                                    CapturePointD,
                                    CapturePointE,
                                    ReferenceTeam,
                                    ChampionPointValue,
                                    CapturePointASquad,
                                    CapturePointBSquad,
                                    CapturePointCSquad,
                                    CapturePointDSquad,
                                    CapturePointESquad,
                                    CapturePointAPosition,
                                    CapturePointBPosition,
                                    CapturePointCPosition,
                                    CapturePointDPosition,
                                    CapturePointEPosition,
                                    StrengthEvaluatorRadius,
                                    MinionPointValue,
                                    CPPointValue,
                                    UpdateLimit,

                                    //  PreviousOwnedCPCount, 
                                    //   PreviousNeutralCPCount, 
                                    //   PreviousEnemyCPCount, 
                                    //   NextUpdateTime, 

                                    DifficultyIndex,
                                    WaitInBaseSquad,
                                    DisconnectAdjustmentEnabled

                                    //   ManuallyForceUpdate

                                    )

                        )
                  )
            ;
    }
}

