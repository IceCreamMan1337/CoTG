using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class MinionAcquireTargetClass : AI_Characters
{
    private MinionDeaggroCheckerClass minionDeaggroChecker = new();

    public bool MinionAcquireTarget(
         AttackableUnit Self,
     Vector3 AssistPosition,
     Vector3 SelfPosition)
    {
        return
                    // Sequence name :MinionAcquireTarget

                    // Sequence name :Respond To Call For Help
                    (
                          TestHaveCallForHelpTarget(
                                true) &&
                          GetUnitCallForHelpTargetUnit(
                                out CFHTarget) &&
                          GetUnitCallForHelpCallerUnit(
                                out CFHCaller) &&
                          SetUnitAIAttackTarget(
                                CFHTarget) &&
                          SetUnitAIAssistTarget(
                                CFHCaller)
                    ) ||
                    // Sequence name :Use Previous Target
                    (
                          SetVarBool(
                                out LostAggro,
                                false) &&
                          TestUnitAIAttackTargetValid(
                                true) &&
                          GetUnitAIAttackTarget(
                                out AggroTarget) &&
                          SetVarVector(
                                out AggroPosition,
                                AssistPosition) &&
                          TestUnitIsVisible(
                                Self,
                                AggroTarget,
                                true) &&
                         minionDeaggroChecker.MinionDeaggroChecker(
                                out LostAggro,
                                AggroPosition,
                                AggroTarget) &&
                          LostAggro == false
                    ) ||
                    // Sequence name :AcquireNewTarget
                    (
                          GetUnitTargetAcquistionRange(
                                out CurrentClosestDistance) &&
                          GetUnitsInTargetArea(
                                out EnemyUnitsNearSelf,
                                Self,
                                SelfPosition,
                                CurrentClosestDistance,
                                AffectBuildings | AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets
                               ) &&
                                // Sequence name :Find Closest Target

                                GetCollectionCount(
                                      out EnemyCount,
                                      EnemyUnitsNearSelf) &&
                                GreaterInt(
                                      EnemyCount,
                                      0) &&
                                SetVarBool(
                                      out TargetFound,
                                      false) &&
                               ForEach(EnemyUnitsNearSelf, EnemyUnit =>
                                            // Sequence name :Find Closest Target

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
                           &&
                          TargetFound == true &&
                          SetUnitAIAssistTarget(
                                Self) &&
                          SetUnitAIAttackTarget(
                                CurrentClosestTarget) &&
                          SetVarVector(
                                out AssistPosition,
                                SelfPosition)

                    )
              ;
    }
}

