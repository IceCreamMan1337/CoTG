using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class ReturnToBaseClass : AI_Characters 
{
     

     public bool ReturnToBase(
          out bool __TeleportHome,
      out string _ActionPerformed,
      AttackableUnit Self,
      bool TeleportHome,
      Vector3 SelfPosition
         )
      {

        bool _TeleportHome = TeleportHome;
        string ActionPerformed = default;
        bool result =
              // Sequence name :ReturnToBase
              (
                    GetUnitAIBasePosition(
                          out BaseLocation,
                          Self) &&
                    DistanceBetweenObjectAndPoint(
                          out Distance,
                          Self,
                          BaseLocation) &&
                    GreaterFloat(
                          Distance,
                          300) &&
                    // Sequence name :LowHealth
                    (
                          GetUnitMaxHealth(
                                out MaxHealth,
                                Self) &&
                          GetUnitCurrentHealth(
                                out Health,
                                Self) &&
                          DivideFloat(
                                out Health_Ratio,
                                Health,
                                MaxHealth) &&
                          // Sequence name :EvaluateTeleportHome
                          (
                                // Sequence name :PreviouslyTriggered
                                (
                                      TeleportHome == true &&
                                      LessEqualFloat(
                                            Health_Ratio,
                                            0.35f)
                                ) ||
                                // Sequence name :NewScenario
                                (
                                      TeleportHome == false &&
                                      LessEqualFloat(
                                            Health_Ratio,
                                            0.25f) &&
                                      SetVarBool(
                                            out TeleportHome,
                                            true)
                                )
                          )
                    ) &&
                    // Sequence name :RunHome_MicroRetreat_Teleport
                    (
                          // Sequence name :TeleportingAndEnemiesNotNear
                          (
                                TestUnitIsChanneling(
                                      Self,
                                      true) &&
                                CountUnitsInTargetArea(
                                      out EnemyChampionCount,
                                      Self,
                                      SelfPosition,
                                      750,
                                      AffectEnemies | AffectHeroes,
                                      "") &&
                                LessEqualInt(
                                      EnemyChampionCount,
                                      0)
                          ) ||
                          // Sequence name :RunHome
                          (
                                DistanceBetweenObjectAndPoint(
                                      out BaseDistance,
                                      Self,
                                      BaseLocation) &&
                                LessEqualFloat(
                                      BaseDistance,
                                      4000) &&
                                IssueMoveToPositionOrder(
                                      BaseLocation)
                          ) ||
                          // Sequence name :EnemiesNearBy_MicroRetreat
                          (
                                // Sequence name :Selector
                                (
                                      // Sequence name :Beginner
                                      (
                                            TestEntityDifficultyLevel(
                                                  true,
                                                EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                            SetVarFloat(
                                                  out EnemyCheckRange,
                                                  1000)
                                      ) ||
                                      // Sequence name :IntermediateAdvanced
                                      (
                                            // Sequence name :Difficulty
                                            (
                                                  TestEntityDifficultyLevel(
                                                        true,
                                                      EntityDiffcultyType.DIFFICULTY_INTERMEDIATE) ||
                                                        TestEntityDifficultyLevel(
                                                        true,
                                                     EntityDiffcultyType.DIFFICULTY_ADVANCED)
                                            ) &&
                                            SetVarFloat(
                                                  out EnemyCheckRange,
                                                  1600)
                                      )
                                ) &&
                                GetUnitAIClosestTargetInArea(
                                      out ClosestEnemy,
                                      Self,
                                     default,
                                      true,
                                      SelfPosition,
                                      EnemyCheckRange,
                                      AffectEnemies | AffectHeroes) &&
                                ComputeUnitAISafePosition(
                                      800,
                                      false,
                                      false) &&
                                GetUnitAISafePosition(
                                      out SafePosition) &&
                                IssueMoveToPositionOrder(
                                      SafePosition)
                          ) ||
                          IssueTeleportToBaseOrder()
                    ) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :Sequence
                          (
                                SetVarBool(
                                      out Run,
                                      false) &&
                                Run == true &&
                                // Sequence name :RunOrTeleport
                                (
                                      // Sequence name :TeleportHome
                                      (
                                            GetUnitAIClosestTargetInArea(
                                                  out ClosestTurret,
                                                  Self,
                                                  default,
                                                  false,
                                                  SelfPosition,
                                                  30000,
                                                  AffectFriends | AffectTurrets) &&
                                            // Sequence name :ContinueIF
                                            (
                                                  // Sequence name :I am far away from base
                                                  (
                                                        DistanceBetweenObjectAndPoint(
                                                              out BaseDistance,
                                                              Self,
                                                              BaseLocation) &&
                                                        GreaterFloat(
                                                              BaseDistance,
                                                              3500)
                                                  ) &&
                                                  // Sequence name :An enemy is not near that turret
                                                  (
                                                        GetUnitPosition(
                                                              out TurretPosition,
                                                              ClosestTurret) &&
                                                        CountUnitsInTargetArea(
                                                              out DangerousEnemyCount,
                                                              Self,
                                                              TurretPosition,
                                                              1300,
                                                              AffectEnemies | AffectHeroes,
                                                              "") &&
                                                        LessInt(
                                                              DangerousEnemyCount,
                                                              1)
                                                  ) &&
                                                  // Sequence name :There is not an enemy champion near me


                                 
                                                  (
                                                        CountUnitsInTargetArea(
                                                              out DangerousEnemyNearbCount,
                                                              Self,
                                                              SelfPosition,
                                                              1300,
                                                              AffectEnemies | AffectHeroes,
                                                              "") &&
                                                        LessInt(
                                                              DangerousEnemyNearbCount,
                                                              1)
                                                  )
                                            ) &&
                                            // Sequence name :TeleportOrGotoTurret
                                            (
                                                  // Sequence name :Teleport
                                                  (
                                                        GetDistanceBetweenUnits(
                                                              out Distance,
                                                              ClosestTurret,
                                                              Self) &&
                                                        LessFloat(
                                                              Distance,
                                                              125) &&
                                                        // Sequence name :CheckIfSpellPositionIsValid
                                                        (
                                                              // Sequence name :CloseToSpellPosition
                                                              (
                                                                    TestUnitAISpellPositionValid(
                                                                          true) &&
                                                                    GetUnitAISpellPosition(

                                                                          out TeleportPosition) &&
                                                                    DistanceBetweenObjectAndPoint(
                                                                          out DistanceToTeleportPosition,
                                                                          Self,
                                                                          TeleportPosition) &&
                                                                    LessFloat(
                                                                          DistanceToTeleportPosition,
                                                                          50)
                                                              ) ||
                                                              TestUnitAISpellPositionValid(
                                                                    false)
                                                        ) &&
                                                        // Sequence name :ChannelingOrTeleport
                                                        (
                                                              TestUnitIsChanneling(
                                                                    Self,
                                                                    true)
                                                              ||
                                                              IssueTeleportToBaseOrder()
                                                        )
                                                  &&
                                                        ClearUnitAISpellPosition()
                                                  ) ||
                                                  // Sequence name :GotoTurret
                                                  (
                                                        // Sequence name :ComputeOrUsePreviousSpellPosition
                                                        (
                                                              // Sequence name :ComputeNewSpellPosition
                                                              (
                                                                    // Sequence name :InvalidPosition
                                                                    (
                                                                          TestUnitAISpellPositionValid(
                                                                                false) ||
                                                                          // Sequence name :DetectDeadTurret
                                                                          (
                                                                                GetUnitAISpellPosition(
                                                                                      out TeleportPosition) &&
                                                                                DistanceBetweenObjectAndPoint(
                                                                                      out Distance,
                                                                                      ClosestTurret,
                                                                                      TeleportPosition) &&
                                                                                GreaterFloat(
                                                                                      Distance,
                                                                                      500)
                                                                          )
                                                                    ) &&
                                                                    ComputeUnitAISpellPosition(
                                                                          ClosestTurret,
                                                                          Self,
                                                                          150,
                                                                          false)
                                                              ) ||
                                                              TestUnitAISpellPositionValid(
                                                                    true)
                                                        ) &&
                                                        GetUnitAISpellPosition(
                                                              out TeleportPosition) &&
                                                        IssueMoveToPositionOrder(
                                                              TeleportPosition)
                                                  )
                                            )
                                      ) ||
                                      // Sequence name :RunHome
                                      (
                                            GetUnitAIBasePosition(
                                                  out BaseLocation,
                                                  Self) &&
                                            IssueMoveToPositionOrder(
                                                  BaseLocation)
                                      )
                                )
                          )
                          || MaskFailure()
                    ) &&
                    SetVarString(
                          out ActionPerformed,
                          "ReturnToBase")

              );


         __TeleportHome = _TeleportHome;
         _ActionPerformed = ActionPerformed;


        return result;
      }
}

