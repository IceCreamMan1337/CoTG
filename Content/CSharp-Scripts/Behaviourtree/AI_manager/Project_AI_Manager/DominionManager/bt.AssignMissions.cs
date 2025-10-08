namespace BehaviourTrees;


class AssignMissionsClass : bt_OdinManager
{


    public bool AssignMissions(
          out int __PreviousOwnedCPCount,
          out int __PreviousNeutralCPCount,
          out int __PreviousEnemyCPCount,
          out float __NextUpdateTime,
          out bool __ManuallyForceUpdate,
          int OwnedCPCount,
          int NeutralCPCount,
          int EnemyCPCount,
          AttackableUnit CapturePointA,
          AttackableUnit CapturePointB,
          AttackableUnit CapturePointC,
          AttackableUnit CapturePointD,
          AttackableUnit CapturePointE,
          TeamId ReferenceTeam,
          float ChampionPointValue,
          AISquadClass CapturePointASquad,
          AISquadClass CapturePointBSquad,
          AISquadClass CapturePointCSquad,
          AISquadClass CapturePointDSquad,
          AISquadClass CapturePointESquad,
          Vector3 CapturePointAPosition,
          Vector3 CapturePointBPosition,
          Vector3 CapturePointCPosition,
          Vector3 CapturePointDPosition,
          Vector3 CapturePointEPosition,
          float StrengthEvaluatorRadius,
          float MinionPointValue,
          float CPPointValue,
          int UpdateLimit,
          int DifficultyIndex,
          AISquadClass WaitInBaseSquad,
          bool DisconnectAdjustmentEnabled)
    {

        var strengthEvaluation = new ODINStrengthEvaluationClass();
        var getPriorityScore = new GetPriorityScoreClass();
        int _PreviousOwnedCPCount = default;
        int _PreviousNeutralCPCount = default;
        int _PreviousEnemyCPCount = default;
        float _NextUpdateTime = default;
        bool _ManuallyForceUpdate = default;

        bool result =

                  // Sequence name :Sequence

                  GetAIManagerEntities(
                        out AIEntities) &&
                  SetVarInt(
                        out NeedOffsetA,
                        0) &&
                  SetVarInt(
                        out NeedOffsetB,
                        0) &&
                  SetVarInt(
                        out NeedOffsetC,
                        0) &&
                  SetVarInt(
                        out NeedOffsetD,
                        0) &&
                  SetVarInt(
                        out NeedOffsetE,
                        0) &&
                  SetVarFloat(
                        out StrengthOffsetA,
                        0) &&
                  SetVarFloat(
                        out StrengthOffsetB,
                        0) &&
                  SetVarFloat(
                        out StrengthOffsetC,
                        0) &&
                  SetVarFloat(
                        out StrengthOffsetD,
                        0) &&
                  SetVarFloat(
                        out StrengthOffsetE,
                        0) &&
                  SetVarFloat(
                        out TopPointScoreModifier,
                        1.2f) &&
                  SetVarFloat(
                        out MidPointScoreModifier,
                        1.1f) &&
                  SetVarFloat(
                        out BasePointScoreModifier,
                        1.1f) &&
                  SetVarFloat(
                        out MaxDistance,
                        14000) &&
                  SetVarFloat(
                        out DistanceNormalizer,
                        21000) &&
                  SetVarFloat(
                        out StrengthOffset,
                        ChampionPointValue) &&
                  SetVarInt(
                        out CurrentEntityID,
                        0) &&
                  SetVarInt(
                        out DisconnectAdjustmentEntityID,
                        3) &&
                  SetVarBool(
                        out DisconnectAdjustmentNeeded,
                        false) &&
                  // Sequence name :StrengthOffsetModifier
                  (
                        // Sequence name :Beginner
                        (
                              DifficultyIndex == 0 &&
                              MultiplyFloat(
                                    out StrengthOffset,
                                    StrengthOffset,
                                    1)
                        ) ||
                        // Sequence name :IntermediateAdvanced
                        (
                              GreaterInt(
                                    DifficultyIndex,
                                    0) &&
                              MultiplyFloat(
                                    out StrengthOffset,
                                    StrengthOffset,
                                    2)
                        )
                  ) &&
                        // Sequence name :DynamicAssignments

                        // Sequence name :MaskFailure
                        (
                              ForEach(AIEntities, Entity =>
                                          // Sequence name :Sequence

                                          // Sequence name :MaskFailure
                                          (
                                                // Sequence name :DisconnectAdjustment
                                                (
                                                      DisconnectAdjustmentEnabled == true &&
                                                      CurrentEntityID == DisconnectAdjustmentEntityID &&
                                                      // Sequence name :GetEnemyTeam
                                                      (
                                                            // Sequence name :IsOrder
                                                            (
                                                                  ReferenceTeam == TeamId.TEAM_ORDER &&
                                                                  SetVarUnitTeam(
                                                                        out EnemyTeam,
                                                                        TeamId.TEAM_CHAOS)
                                                            ) ||
                                                            // Sequence name :IsChaos
                                                            (
                                                                  ReferenceTeam == TeamId.TEAM_CHAOS &&
                                                                  SetVarUnitTeam(
                                                                        out EnemyTeam,
                                                                        TeamId.TEAM_ORDER)
                                                            )
                                                      ) &&
                                                      GetNumberOfConnectedPlayers(
                                                            out ConnectedPlayersOnEnemyTeam,
                                                            EnemyTeam) &&
                                                      LessInt(
                                                            ConnectedPlayersOnEnemyTeam,
                                                            5) &&
                                                      AddAIEntityToSquad(
                                                            Entity,
                                                            WaitInBaseSquad) &&
                                                      SetVarBool(
                                                            out DisconnectAdjustmentNeeded,
                                                            true)
                                                )
                                                ||
                               DebugAction("MaskFailure")
                                          ) &&
                                          AddInt(
                                                out CurrentEntityID,
                                                CurrentEntityID,
                                                1)

                              )
                              ||
                               DebugAction("MaskFailure")
                        ) &&
                        SetVarInt(
                              out CurrentEntityID,
                              0) &&
                        // Sequence name :UpdateConditions
                        (
                              // Sequence name :ManuallyForceUpdate
                              (
                                    _ManuallyForceUpdate == true &&
                                    SetVarBool(
                                          out _ManuallyForceUpdate,
                                          false)
                              ) ||
                              // Sequence name :TimeToUpdate
                              (
                                    GetGameTime(
                                          out CurrentGameTime) &&
                                    GreaterFloat(
                                          CurrentGameTime,
                                          _NextUpdateTime)
                              ) ||
                              NotEqualInt(
                                    EnemyCPCount,
                                    PreviousEnemyCPCount) ||
                              NotEqualInt(
                                    NeutralCPCount,
                                    PreviousNeutralCPCount) ||
                                NotEqualInt(
                                    OwnedCPCount,
                                    PreviousOwnedCPCount)
                        ) &&
                        ForEach(AIEntities, Entity =>                               // Sequence name :Sequence

                                    // Sequence name :ParseEntities
                                    (
                                          TestUnitHasBuff(
                                                Entity,
                                                null,
                                                "OdinGuardianStatsByLevel")
                                          ||
                                          TestUnitCondition(
                                                Entity, false)
                                          ||
                                          TestUnitIsChanneling(
                                                Entity)
                                          ||
                                          // Sequence name :DoNothingIfAssistingAlly
                                          (
                                                TestAIEntityHasTask(
                                                      Entity,
                                                     AITaskTopicType.ASSIST,
                                                      null,
                                                      default,
                                                      0,
                                                      true) &&
                                                GetAITaskFromEntity(
                                                      out KillTask,
                                                      Entity) &&
                                                GetAITaskTarget(
                                                      out KillTaskTarget,
                                                      KillTask) &&
                                                TestUnitCondition(
                                                      KillTaskTarget) &&
                                                GetDistanceBetweenUnits(
                                                      out DistanceToKillTaskTarget,
                                                      Entity,
                                                      KillTaskTarget) &&
                                                LessFloat(
                                                      DistanceToKillTaskTarget,
                                                      2000) &&
                                                GetUnitAIBasePosition(
                                                      out BasePosition,
                                                      Entity) &&
                                                DistanceBetweenObjectAndPoint(
                                                      out DistanceToBase,
                                                      Entity,
                                                      BasePosition) &&
                                                GreaterFloat(
                                                      DistanceToBase,
                                                      800)
                                          ) ||
                                          // Sequence name :DoNothingIfDisconnectAdjustment
                                          (
                                                DisconnectAdjustmentEnabled == true &&
                                                CurrentEntityID == DisconnectAdjustmentEntityID &&
                                                DisconnectAdjustmentNeeded == true
                                          ) ||
                                          // Sequence name :DoNothingIfReturningToBase
                                          (
                                                TestAIEntityHasTask(
                                                      Entity,
                                                      AITaskTopicType.DOMINION_RETURN_TO_BASE,
                                                      null,
                                                      default,
                                                      0,
                                                      true) &&
                                                GetUnitAIBasePosition(
                                                      out BasePosition,
                                                      Entity) &&
                                                DistanceBetweenObjectAndPoint(
                                                      out DistanceToBase,
                                                      Entity,
                                                      BasePosition) &&
                                                GetUnitCurrentHealth(
                                                      out EntityCurrentHealth,
                                                      Entity) &&
                                                GetUnitMaxHealth(
                                                      out EntityMaxHealth,
                                                      Entity) &&
                                                DivideFloat(
                                                      out EntityHealthRatio,
                                                      EntityCurrentHealth,
                                                      EntityMaxHealth) &&
                                                // Sequence name :SkipConditions
                                                (
                                                      GreaterFloat(
                                                            DistanceToBase,
                                                            800)
                                                      ||
                                                      LessFloat(
                                                            EntityHealthRatio,
                                                            0.95f)
                                                )
                                          ) ||
                                          // Sequence name :AssignToPoint
                                          (
                                                      // Sequence name :GetStrengthEval

                                                      strengthEvaluation.StrengthEvaluation(
                                                            out EnemyStrengthPointA,
                                                            out FriendlyStrengthPointA,
                                                            CapturePointAPosition,
                                                            Entity,
                                                            StrengthEvaluatorRadius,
                                                            MinionPointValue,
                                                            ChampionPointValue,
                                                            CPPointValue) &&
                                                      strengthEvaluation.StrengthEvaluation(
                                                            out EnemyStrengthPointB,
                                                            out FriendlyStrengthPointB,
                                                            CapturePointBPosition,
                                                            Entity,
                                                            StrengthEvaluatorRadius,
                                                            MinionPointValue,
                                                            ChampionPointValue,
                                                            CPPointValue) &&
                                                      strengthEvaluation.StrengthEvaluation(
                                                            out EnemyStrengthPointC,
                                                            out FriendlyStrengthPointC,
                                                            CapturePointCPosition,
                                                            Entity,
                                                            StrengthEvaluatorRadius,
                                                             MinionPointValue,
                                                            ChampionPointValue,
                                                            CPPointValue) &&
                                                      strengthEvaluation.StrengthEvaluation(
                                                            out EnemyStrengthPointD,
                                                            out FriendlyStrengthPointD,
                                                            CapturePointDPosition,
                                                            Entity,
                                                            StrengthEvaluatorRadius,
                                                             MinionPointValue,
                                                            ChampionPointValue,
                                                            CPPointValue) &&
                                                      strengthEvaluation.StrengthEvaluation(
                                                            out EnemyStrengthPointE,
                                                            out FriendlyStrengthPointE,
                                                            CapturePointEPosition,
                                                            Entity,
                                                            StrengthEvaluatorRadius,
                                                             MinionPointValue,
                                                            ChampionPointValue,
                                                            CPPointValue) &&
                                                      AddFloat(
                                                            out FriendlyStrengthPointA,
                                                            FriendlyStrengthPointA,
                                                            StrengthOffsetA) &&
                                                      AddFloat(
                                                            out FriendlyStrengthPointB,
                                                            FriendlyStrengthPointB,
                                                            StrengthOffsetB) &&
                                                      AddFloat(
                                                            out FriendlyStrengthPointC,
                                                            FriendlyStrengthPointC,
                                                            StrengthOffsetC) &&
                                                      AddFloat(
                                                            out FriendlyStrengthPointD,
                                                            FriendlyStrengthPointD,
                                                            StrengthOffsetD) &&
                                                      AddFloat(
                                                            out FriendlyStrengthPointE,
                                                            FriendlyStrengthPointE,
                                                            StrengthOffsetE)
                                                 &&
                                                      // Sequence name :GetPriority

                                                      getPriorityScore.GetPriorityScore(
                                                            out PriorityA,
                                                            FriendlyStrengthPointA,
                                                            EnemyStrengthPointA,
                                                            ChampionPointValue,
                                                            CapturePointA,
                                                            ReferenceTeam,
                                                            DifficultyIndex) &&
                                                      getPriorityScore.GetPriorityScore(
                                                            out PriorityB,
                                                            FriendlyStrengthPointB,
                                                            EnemyStrengthPointB,
                                                            ChampionPointValue,
                                                            CapturePointB,
                                                            ReferenceTeam,
                                                            DifficultyIndex) &&
                                                      getPriorityScore.GetPriorityScore(
                                                            out PriorityC,
                                                            FriendlyStrengthPointC,
                                                            EnemyStrengthPointC,
                                                            ChampionPointValue,
                                                            CapturePointC,
                                                            ReferenceTeam,
                                                            DifficultyIndex) &&
                                                      getPriorityScore.GetPriorityScore(
                                                            out PriorityD,
                                                            FriendlyStrengthPointD,
                                                            EnemyStrengthPointD,
                                                            ChampionPointValue,
                                                            CapturePointD,
                                                            ReferenceTeam,
                                                            DifficultyIndex) &&
                                                      getPriorityScore.GetPriorityScore(
                                                            out PriorityE,
                                                            FriendlyStrengthPointE,
                                                            EnemyStrengthPointE,
                                                            ChampionPointValue,
                                                            CapturePointE,
                                                            ReferenceTeam,
                                                            DifficultyIndex)
                                                 &&
                                                      // Sequence name :GetDistanceNeedPairScores

                                                      GetDistanceBetweenUnits(
                                                            out DistanceA,
                                                            Entity,
                                                            CapturePointA) &&
                                                      GetDistanceBetweenUnits(
                                                            out DistanceB,
                                                            Entity,
                                                            CapturePointB) &&
                                                      GetDistanceBetweenUnits(
                                                            out DistanceC,
                                                            Entity,
                                                            CapturePointC) &&
                                                      GetDistanceBetweenUnits(
                                                            out DistanceD,
                                                            Entity,
                                                            CapturePointD) &&
                                                      GetDistanceBetweenUnits(
                                                            out DistanceE,
                                                            Entity,
                                                            CapturePointE) &&
                                                      SubtractFloat(
                                                            out DistanceA,
                                                            MaxDistance,
                                                            DistanceA) &&
                                                      SubtractFloat(
                                                            out DistanceB,
                                                            MaxDistance,
                                                            DistanceB) &&
                                                      SubtractFloat(
                                                            out DistanceC,
                                                            MaxDistance,
                                                            DistanceC) &&
                                                      SubtractFloat(
                                                            out DistanceD,
                                                            MaxDistance,
                                                            DistanceD) &&
                                                      SubtractFloat(
                                                            out DistanceE,
                                                            MaxDistance,
                                                            DistanceE) &&
                                                      DivideFloat(
                                                            out DistanceA,
                                                            DistanceA,
                                                            DistanceNormalizer) &&
                                                      DivideFloat(
                                                            out DistanceB,
                                                            DistanceB,
                                                            DistanceNormalizer) &&
                                                      DivideFloat(
                                                            out DistanceC,
                                                            DistanceC,
                                                            DistanceNormalizer) &&
                                                      DivideFloat(
                                                            out DistanceD,
                                                            DistanceD,
                                                            DistanceNormalizer) &&
                                                      DivideFloat(
                                                            out DistanceE,
                                                            DistanceE,
                                                            DistanceNormalizer) &&
                                                      MultiplyFloat(
                                                            out PairScoreA,
                                                            DistanceA,
                                                            PriorityA) &&
                                                      MultiplyFloat(
                                                            out PairScoreB,
                                                            DistanceB,
                                                            PriorityB) &&
                                                      MultiplyFloat(
                                                            out PairScoreC,
                                                            DistanceC,
                                                            PriorityC) &&
                                                      MultiplyFloat(
                                                            out PairScoreD,
                                                            DistanceD,
                                                            PriorityD) &&
                                                      MultiplyFloat(
                                                            out PairScoreE,
                                                            DistanceE,
                                                            PriorityE)
                                                 &&
                                                      // Sequence name :InflateScoresForPointsBySide

                                                      MultiplyFloat(
                                                            out PairScoreC,
                                                            PairScoreC,
                                                            TopPointScoreModifier) &&
                                                      GetUnitTeam(
                                                            out EntityTeam,
                                                            Entity) &&
                                                      // Sequence name :WhichSide?
                                                      (
                                                            // Sequence name :Chaos
                                                            (
                                                                  EntityTeam == TeamId.TEAM_CHAOS &&
                                                                  MultiplyFloat(
                                                                        out PairScoreD,
                                                                        PairScoreD,
                                                                        MidPointScoreModifier) &&
                                                                  MultiplyFloat(
                                                                        out PairScoreE,
                                                                        PairScoreE,
                                                                        BasePointScoreModifier)
                                                            ) ||
                                                            // Sequence name :Order
                                                            (
                                                                  EntityTeam == TeamId.TEAM_ORDER &&
                                                                  MultiplyFloat(
                                                                        out PairScoreB,
                                                                        PairScoreB,
                                                                        MidPointScoreModifier) &&
                                                                  MultiplyFloat(
                                                                        out PairScoreA,
                                                                        PairScoreA,
                                                                        BasePointScoreModifier)
                                                            )
                                                      )
                                                 &&
                                                      // Sequence name :GetBestScore

                                                      SetVarFloat(
                                                            out BestScore,
                                                            -1) &&
                                                      SetVarInt(
                                                            out PointID,
                                                            -1) &&
                                                      // Sequence name :MaskFailure
                                                      (
                                                            // Sequence name :Sequence
                                                            (
                                                                  GreaterFloat(
                                                                        PairScoreA,
                                                                        BestScore) &&
                                                                  SetVarFloat(
                                                                        out BestScore,
                                                                        PairScoreA) &&
                                                                  SetVarInt(
                                                                        out PointID,
                                                                        0)
                                                            )
                                                            ||
                               DebugAction("MaskFailure")
                                                      ) &&
                                                      // Sequence name :MaskFailure
                                                      (
                                                            // Sequence name :Sequence
                                                            (
                                                                  GreaterFloat(
                                                                        PairScoreB,
                                                                        BestScore) &&
                                                                  SetVarFloat(
                                                                        out BestScore,
                                                                        PairScoreB) &&
                                                                  SetVarInt(
                                                                        out PointID,
                                                                        1)
                                                            )
                                                            ||
                               DebugAction("MaskFailure")
                                                      ) &&
                                                      // Sequence name :MaskFailure
                                                      (
                                                            // Sequence name :Sequence
                                                            (
                                                                  GreaterFloat(
                                                                        PairScoreC,
                                                                        BestScore) &&
                                                                  SetVarFloat(
                                                                        out BestScore,
                                                                        PairScoreC) &&
                                                                  SetVarInt(
                                                                        out PointID,
                                                                        2)
                                                            )
                                                            ||
                               DebugAction("MaskFailure")
                                                      ) &&
                                                      // Sequence name :MaskFailure
                                                      (
                                                            // Sequence name :Sequence
                                                            (
                                                                  GreaterFloat(
                                                                        PairScoreD,
                                                                        BestScore) &&
                                                                  SetVarFloat(
                                                                        out BestScore,
                                                                        PairScoreD) &&
                                                                  SetVarInt(
                                                                        out PointID,
                                                                        3)
                                                            )
                                                            ||
                               DebugAction("MaskFailure")
                                                      ) &&
                                                      // Sequence name :MaskFailure
                                                      (
                                                            // Sequence name :Sequence
                                                            (
                                                                  GreaterFloat(
                                                                        PairScoreE,
                                                                        BestScore) &&
                                                                  SetVarFloat(
                                                                        out BestScore,
                                                                        PairScoreE) &&
                                                                  SetVarInt(
                                                                        out PointID,
                                                                        4)
                                                            )
                                                            ||
                               DebugAction("MaskFailure")
                                                      )
                                                 &&
                                                // Sequence name :AssignToSquad
                                                (
                                                      // Sequence name :PointA
                                                      (
                                                            PointID == 0 &&
                                                            AddAIEntityToSquad(
                                                                  Entity,
                                                                  CapturePointASquad) &&
                                                            // Sequence name :MaskFailure
                                                            (
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        GetDistanceBetweenUnits(
                                                                              out DistanceToPoint,
                                                                              Entity,
                                                                              CapturePointA) &&
                                                                        GreaterFloat(
                                                                              DistanceToPoint,
                                                                              StrengthEvaluatorRadius) &&
                                                                        AddFloat(
                                                                              out StrengthOffsetA,
                                                                              StrengthOffsetA,
                                                                              StrengthOffset)
                                                                  )
                                                                  ||
                               DebugAction("MaskFailure")
                                                            )

                                                      ) ||
                                                      // Sequence name :PointB
                                                      (
                                                            PointID == 1 &&
                                                            AddAIEntityToSquad(
                                                                  Entity,
                                                                  CapturePointBSquad) &&
                                                            // Sequence name :MaskFailure
                                                            (
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        GetDistanceBetweenUnits(
                                                                              out DistanceToPoint,
                                                                              Entity,
                                                                              CapturePointB) &&
                                                                        GreaterFloat(
                                                                              DistanceToPoint,
                                                                              StrengthEvaluatorRadius) &&
                                                                        AddFloat(
                                                                              out StrengthOffsetB,
                                                                              StrengthOffsetB,
                                                                              StrengthOffset)
                                                                  )
                                                                  ||
                               DebugAction("MaskFailure")
                                                            )

                                                      ) ||
                                                      // Sequence name :PointC
                                                      (
                                                            PointID == 2 &&
                                                            AddAIEntityToSquad(
                                                                  Entity,
                                                                  CapturePointCSquad) &&
                                                            // Sequence name :MaskFailure
                                                            (
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        GetDistanceBetweenUnits(
                                                                              out DistanceToPoint,
                                                                              Entity,
                                                                              CapturePointC) &&
                                                                        GreaterFloat(
                                                                              DistanceToPoint,
                                                                              StrengthEvaluatorRadius) &&
                                                                        AddFloat(
                                                                              out StrengthOffsetC,
                                                                              StrengthOffsetC,
                                                                              StrengthOffset)
                                                                  )
                                                                  ||
                               DebugAction("MaskFailure")
                                                            )

                                                      ) ||
                                                      // Sequence name :PointD
                                                      (
                                                            PointID == 3 &&
                                                            AddAIEntityToSquad(
                                                                  Entity,
                                                                  CapturePointDSquad) &&
                                                            // Sequence name :MaskFailure
                                                            (
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        GetDistanceBetweenUnits(
                                                                              out DistanceToPoint,
                                                                              Entity,
                                                                              CapturePointD) &&
                                                                        GreaterFloat(
                                                                              DistanceToPoint,
                                                                              StrengthEvaluatorRadius) &&
                                                                        AddFloat(
                                                                              out StrengthOffsetD,
                                                                              StrengthOffsetD,
                                                                              StrengthOffset)
                                                                  )
                                                                  ||
                               DebugAction("MaskFailure")
                                                            )
                                                      ) ||
                                                      // Sequence name :PointE
                                                      (
                                                            PointID == 4 &&
                                                            AddAIEntityToSquad(
                                                                  Entity,
                                                                  CapturePointESquad) &&
                                                            // Sequence name :MaskFailure
                                                            (
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        GetDistanceBetweenUnits(
                                                                              out DistanceToPoint,
                                                                              Entity,
                                                                              CapturePointE) &&
                                                                        GreaterFloat(
                                                                              DistanceToPoint,
                                                                              StrengthEvaluatorRadius) &&
                                                                        AddFloat(
                                                                              out StrengthOffsetE,
                                                                              StrengthOffsetE,
                                                                              StrengthOffset)
                                                                  )
                                                                  ||
                               DebugAction("MaskFailure")
                                                            )
                                                      )
                                                )
                                          )
                                    ) &&
                                    AddInt(
                                          out CurrentEntityID,
                                          CurrentEntityID,
                                          1)

                        ) &&
                              // Sequence name :UpdateParameters

                              SetVarInt(
                                    out _PreviousEnemyCPCount,
                                    EnemyCPCount) &&
                              SetVarInt(
                                    out _PreviousOwnedCPCount,
                                    OwnedCPCount) &&
                              SetVarInt(
                                    out _PreviousNeutralCPCount,
                                    NeutralCPCount) &&
                              GetGameTime(
                                    out CurrentGameTime) &&
                              // Sequence name :NextUpdateTimeBasedOnDifficulty
                              (
                                    // Sequence name :Beginner
                                    (
                                          DifficultyIndex == 0 &&
                                          GetGameScore(
                                                out OrderScore,
                                                TeamId.TEAM_ORDER) &&
                                          GetGameScore(
                                                out ChaosScore,
                                                TeamId.TEAM_CHAOS) &&
                                          SubtractFloat(
                                                out ScoreDiff,
                                                ChaosScore,
                                                OrderScore) &&
                                          // Sequence name :MaskFailure
                                          (
                                                // Sequence name :Sequence
                                                (
                                                      ReferenceTeam == TeamId.TEAM_ORDER &&
                                                      MultiplyFloat(
                                                            out ScoreDiff,
                                                            ScoreDiff,
                                                            -1)
                                                )
                                                ||
                               DebugAction("MaskFailure")
                                          ) &&
                                          DivideFloat(
                                                out UpdateModifier,
                                                ScoreDiff,
                                                30) &&
                                          MinFloat(
                                                out UpdateModifier,
                                                UpdateModifier,
                                                5) &&
                                          MaxFloat(
                                                out UpdateModifier,
                                                UpdateModifier,
                                                -5) &&
                                          AddFloat(
                                                out MinUpdateDelay,
                                                10,
                                                UpdateModifier) &&
                                          AddFloat(
                                                out MaxUpdateDelay,
                                                MinUpdateDelay,
                                                5) &&
                                          GenerateRandomFloat(
                                                out temp,
                                                MinUpdateDelay,
                                                MaxUpdateDelay) &&
                                          AddFloat(
                                                out _NextUpdateTime,
                                                CurrentGameTime,
                                                temp)
                                    ) ||
                                    // Sequence name :Intermediate
                                    (
                                          DifficultyIndex == 1 &&
                                          GenerateRandomFloat(
                                                out temp,
                                                MinUpdateDelay,
                                                MaxUpdateDelay) &&
                                          AddFloat(
                                                out _NextUpdateTime,
                                                CurrentGameTime,
                                                temp)
                                    ) ||
                                    // Sequence name :Advanced
                                    (
                                          DifficultyIndex == 2 &&
                                          AddFloat(
                                                out _NextUpdateTime,
                                                CurrentGameTime,
                                                1)

                                    )
                              )


            ;
        __PreviousOwnedCPCount = _PreviousOwnedCPCount;
        __PreviousNeutralCPCount = _PreviousNeutralCPCount;
        __PreviousEnemyCPCount = _PreviousEnemyCPCount;
        __NextUpdateTime = _NextUpdateTime;
        __ManuallyForceUpdate = _ManuallyForceUpdate;

        return result;

    }
}

