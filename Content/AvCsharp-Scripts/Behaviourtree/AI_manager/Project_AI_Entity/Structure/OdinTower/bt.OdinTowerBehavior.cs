using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;

/*
class OdinTowerBehaviorClass : AI_Characters 
{
    private InitializationClass initialization = new InitializationClass();
    private ReferenceUpdateClass referenceUpdate = new ReferenceUpdateClass();
    private PassiveActionsClass passiveActions = new PassiveActionsClass();
    private TargetVisibilityUpdatesClass targetVisibilityUpdates = new TargetVisibilityUpdatesClass();
    private ActionsClass actions = new ActionsClass();
// Sequence name :Structure.OdinTower.OdinTowerBehavior
      (
            // Sequence name :OdinTowerBehavior
            (
                  // Sequence name :Run Once
                  (
                        TestUnitAIFirstTime(
                              true) &&
                        GetUnitAISelf(
                              out Self) &&
                        GetUnitPosition(
                              out AssistPosition, 
                              Self) &&
                        SetVarFloat(
                              out AcquisitionRange, 
                              600) &&
                        SetVarFloat(
                              out AttackRadius, 
                              750) &&
                        SetVarFloat(
                              out DefenseRadius, 
                              30000) &&
                        GetUnitPosition(
                              out SelfPosition, 
                              Self) &&
                        SetUnitInvulnerable(
                              Self, 
                              true) &&
                        SetUnitTargetableState(
                              Self, 
                              false)
                  ) ||
                  // Sequence name :Behavior Update
                  (
                        GetUnitPosition(
                              out SelfPosition, 
                              Self) &&
                        GetUnitTeam(
                              out SelfTeam, 
                              Self) &&
                        // Sequence name :Select Behavior
                        (
                              // Sequence name :Attack Behavior
                              (
                                    // Sequence name :Selector
                                    (
                                          // Sequence name :AcquireTargetChampion
                                          (
                                                // Sequence name :Use Previous Target
                                                (
                                                      SetVarBool(
                                                            out LostAggro, 
                                                            false) &&
                                                      TestUnitAIAttackTargetValid(
                                                            true) &&
                                                      GetUnitAIAttackTarget(
                                                            out AggroTarget, 
                                                            out AggroTarget) &&
                                                      GetUnitType(
                                                            out AggroTargetType, 
                                                            AggroTarget) &&
                                                      AggroTargetType == HERO_UNIT &&
                                                      SetVarVector(
                                                            out AggroPosition, 
                                                            AssistPosition) &&
                                                      TestUnitIsVisible(
                                                            Self, 
                                                            AggroTarget, 
                                                            true) &&
                                                      // Sequence name :Check For Deaggro
                                                      (
                                                            DistanceBetweenObjectAndPoint(
                                                                  out Distance, 
                                                                  AggroTarget, 
                                                                  AggroPosition) &&
                                                            // Sequence name :Test For Aggro Loss
                                                            (
                                                                  // Sequence name :Test Target Distance
                                                                  (
                                                                        GreaterFloat(
                                                                              Distance, 
                                                                              AcquisitionRange) &&
                                                                        SetVarBool(
                                                                              out LostAggro, 
                                                                              true)
                                                                  ) ||
                                                                  // Sequence name :Sequence
                                                                  (
                                                                        GetUnitTeam(
                                                                              out TargetTeam, 
                                                                              AggroTarget) &&
                                                                        SelfTeam == TargetTeam &&
                                                                        SetVarBool(
                                                                              out LostAggro, 
                                                                              true)
                                                                  ) ||
                                                                  SetVarBool(
                                                                        out LostAggro, 
                                                                        false)
                                                            )
                                                      ) &&
                                                      LostAggro == false
                                                ) ||
                                                // Sequence name :AcquireNewTarget
                                                (
                                                      GetUnitsInTargetArea(
                                                            out EnemyUnitsNearSelf, 
                                                            Self, 
                                                            SelfPosition, 
                                                            AcquisitionRange, 
                                                            AffectEnemies | AffectHeroes, 
                                                            "") &&
                                                      SetVarFloat(
                                                            out CurrentClosestDistance, 
                                                            AcquisitionRange) &&
                                                      // Sequence name :Find Closest Target
                                                      (
                                                            GetCollectionCount(
                                                                  out EnemyCount, 
                                                                  EnemyUnitsNearSelf) &&
                                                            GreaterInt(
                                                                  EnemyCount, 
                                                                  0) &&
                                                            SetVarBool(
                                                                  out TargetFound, 
                                                                  false) &&
                                                            EnemyUnitsNearSelf.ForEach( EnemyUnit => (
                                                                  // Sequence name :Find Closest Target
                                                                  (
                                                                        DistanceBetweenObjectAndPoint(
                                                                              out Distance, 
                                                                              EnemyUnit, 
                                                                              SelfPosition) &&
                                                                        LessFloat(
                                                                              Distance, 
                                                                              CurrentClosestDistance) &&
                                                                        TestUnitIsVisible(
                                                                              Self, 
                                                                              EnemyUnit, 
                                                                              true) &&
                                                                        // Sequence name :Check Aggro
                                                                        (
                                                                              // Sequence name :HasOldTarget
                                                                              (
                                                                                    LostAggro == true &&
                                                                                    GetUnitAIAttackTarget(
                                                                                          out AggroTarget, 
                                                                                          out AggroTarget) &&
                                                                                    NotEqualUnit(
                                                                                          AggroTarget, 
                                                                                          EnemyUnit)
                                                                              ) ||
                                                                              LostAggro == false
                                                                        ) &&
                                                                        SetVarFloat(
                                                                              out CurrentClosestDistance, 
                                                                              Distance) &&
                                                                        SetVarAttackableUnit(
                                                                              out CurrentClosestTarget, 
                                                                              EnemyUnit) &&
                                                                        SetVarBool(
                                                                              out TargetFound, 
                                                                              true)
                                                                  )
                                                            )
                                                      ) &&
                                                      TargetFound == true &&
                                                      SetUnitAIAssistTarget(
                                                            Self) &&
                                                      SetUnitAIAttackTarget(
                                                            CurrentClosestTarget) &&
                                                      SetVarVector(
                                                            out AssistPosition, 
                                                            SelfPosition)
                                                )
                                          ) ||
                                          // Sequence name :AcquireTarget
                                          (
                                                // Sequence name :Use Previous Target
                                                (
                                                      SetVarBool(
                                                            out LostAggro, 
                                                            false) &&
                                                      TestUnitAIAttackTargetValid(
                                                            true) &&
                                                      GetUnitAIAttackTarget(
                                                            out AggroTarget, 
                                                            out AggroTarget) &&
                                                      SetVarVector(
                                                            out AggroPosition, 
                                                            AssistPosition) &&
                                                      TestUnitIsVisible(
                                                            Self, 
                                                            AggroTarget, 
                                                            true) &&
                                                      // Sequence name :Check For Deaggro
                                                      (
                                                            DistanceBetweenObjectAndPoint(
                                                                  out Distance, 
                                                                  AggroTarget, 
                                                                  AggroPosition) &&
                                                            // Sequence name :Test For Aggro Loss
                                                            (
                                                                  // Sequence name :Test Target Distance
                                                                  (
                                                                        GreaterFloat(
                                                                              Distance, 
                                                                              AcquisitionRange) &&
                                                                        SetVarBool(
                                                                              out LostAggro, 
                                                                              true)
                                                                  ) ||
                                                                  SetVarBool(
                                                                        out LostAggro, 
                                                                        false)
                                                            )
                                                      ) &&
                                                      LostAggro == false
                                                ) ||
                                                // Sequence name :AcquireNewTarget
                                                (
                                                      GetUnitsInTargetArea(
                                                            out EnemyUnitsNearSelf, 
                                                            Self, 
                                                            SelfPosition, 
                                                            AcquisitionRange, 
                                                            AffectBuildings,AffectEnemies | AffectHeroes | AffectMinions,AffectTurrets, 
                                                            "") &&
                                                      SetVarFloat(
                                                            out CurrentClosestDistance, 
                                                            AcquisitionRange) &&
                                                      // Sequence name :Find Closest Target
                                                      (
                                                            GetCollectionCount(
                                                                  out EnemyCount, 
                                                                  EnemyUnitsNearSelf) &&
                                                            GreaterInt(
                                                                  EnemyCount, 
                                                                  0) &&
                                                            SetVarBool(
                                                                  out TargetFound, 
                                                                  false) &&
                                                            EnemyUnitsNearSelf.ForEach( EnemyUnit => (
                                                                  // Sequence name :Find Closest Target
                                                                  (
                                                                        DistanceBetweenObjectAndPoint(
                                                                              out Distance, 
                                                                              EnemyUnit, 
                                                                              SelfPosition) &&
                                                                        LessFloat(
                                                                              Distance, 
                                                                              CurrentClosestDistance) &&
                                                                        TestUnitIsVisible(
                                                                              Self, 
                                                                              EnemyUnit, 
                                                                              true) &&
                                                                        // Sequence name :Check Aggro
                                                                        (
                                                                              // Sequence name :HasOldTarget
                                                                              (
                                                                                    LostAggro == true &&
                                                                                    GetUnitAIAttackTarget(
                                                                                          out AggroTarget, 
                                                                                          out AggroTarget) &&
                                                                                    NotEqualUnit(
                                                                                          AggroTarget, 
                                                                                          EnemyUnit)
                                                                              ) ||
                                                                              LostAggro == false
                                                                        ) &&
                                                                        SetVarFloat(
                                                                              out CurrentClosestDistance, 
                                                                              Distance) &&
                                                                        SetVarAttackableUnit(
                                                                              out CurrentClosestTarget, 
                                                                              EnemyUnit) &&
                                                                        SetVarBool(
                                                                              out TargetFound, 
                                                                              true)
                                                                  )
                                                            )
                                                      ) &&
                                                      TargetFound == true &&
                                                      SetUnitAIAssistTarget(
                                                            Self) &&
                                                      SetUnitAIAttackTarget(
                                                            CurrentClosestTarget) &&
                                                      SetVarVector(
                                                            out AssistPosition, 
                                                            SelfPosition)
                                                )
                                          )
                                    ) &&
                                    // Sequence name :Attack Target
                                    (
                                          GetUnitAIAttackTarget(
                                                out Target, 
                                                out Target) &&
                                          GetUnitTeam(
                                                out TargetTeam, 
                                                Target) &&
                                          NotEqualUnitTeam(
                                                TargetTeam, 
                                                SelfTeam) &&
                                          IssueAttackOrder()

                                    )
                              )
                        )
                  )
            ),
      }
}

*/