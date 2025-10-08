using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;


class AcquireTargetClass : AI_Characters
{


    public bool AcquireTarget(
     out bool __TargetValid,
     out AttackableUnit __Target,
     out bool __DeAggro,
     out Vector3 __TargetAcquiredPosition,
     out float __DeAggroTime,
     bool TargetValid,
     AttackableUnit Target,
     Vector3 TargetAcquiredPosition,
     float DeAggroTime,
     Vector3 SelfPosition,
     AttackableUnit Self,
     bool DeAggro,
     float DeAggroDistance,
     float DeAggroTimeThreshold
        )
    {
        bool _TargetValid = TargetValid;
        AttackableUnit _Target = Target;
        bool _DeAggro = DeAggro;
        Vector3 _TargetAcquiredPosition = TargetAcquiredPosition;
        float _DeAggroTime = DeAggroTime;


        bool result =

            // Sequence name :AcquireTarget

            DebugAction("AcquireTarget ") &&
                  GetUnitAttackRange(
                        out SelfAttackRange,
                        Self) &&
                  AddFloat(
                        out TargetAcquisitionRange,
                        SelfAttackRange,
                        800) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :TargetIsValid
                        (
                            DebugAction("TargetIsValid ") &&
                              TargetValid == true &&
                              SetVarBool(
                                    out _TargetValid,
                                    false) &&
                              TestUnitCondition(
                                    Target,
                                    true) &&
                              SetVarBool(
                                    out _TargetValid,
                                    true) &&
                              // Sequence name :DeaggroChecker
                              (
                                    // Sequence name :TargetIsChampion
                                    (
                                        DebugAction("Sequence name :Attack ") &&
                                          GetUnitType(
                                                out UnitType,
                                                Target) &&
                                          UnitType == HERO_UNIT &&
                                          SetVarBool(
                                                out TargetValid,
                                                false)
                                    ) ||
                                    // Sequence name :LostAggro
                                    (
                                          DeAggro == false &&
                                          DistanceBetweenObjectAndPoint(
                                                out Distance,
                                                Self,
                                                TargetAcquiredPosition) &&
                                          GreaterFloat(
                                                Distance,
                                                DeAggroDistance) &&
                                          SetVarBool(
                                                out _DeAggro,
                                                true) &&
                                          GetGameTime(
                                                out _DeAggroTime)
                                    ) ||
                                    // Sequence name :ForgetAboutTarget
                                    (
                                          DeAggro == true &&
                                          // Sequence name :TimeOrAttacked
                                          (
                                                // Sequence name :Time
                                                (
                                                      GetGameTime(
                                                            out CurrentTime) &&
                                                      SubtractFloat(
                                                            out TimeDiff,
                                                            CurrentTime,
                                                            DeAggroTime) &&
                                                      GreaterFloat(
                                                            TimeDiff,
                                                            DeAggroTimeThreshold) &&
                                                      SetVarBool(
                                                            out _TargetValid,
                                                            false)
                                                ) ||
                                                // Sequence name :AttackedByTarget
                                                (
                                                      GetUnitAIAttackers(
                                                            out Attackers,
                                                            Self) &&
                                                      ForEach(Attackers, Attacker =>                                                             // Sequence name :CheckForTarget

                                                                  _Target == Attacker

                                                       ) &&
                                                      SetVarBool(
                                                            out _TargetValid,
                                                            false)
                                                )
                                          )
                                    )
                              )
                        )
                        || MaskFailure()
                  ) &&
                        // Sequence name :AlwaysSeekNewTarget



                        GetUnitAIClosestTargetInArea(
                              out NearestEnemy,
                              Self,
                              default,
                              true,
                              SelfPosition,
                              TargetAcquisitionRange,
                              AffectBuildings | AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&
                              GetUnitAILowestHPTargetInArea(
                              out _Target,
                              Self,
                              default,
                              true,
                              SelfPosition,
                              TargetAcquisitionRange,
                              default,
                              false,
                              false,
                               AffectBuildings | AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&
                               GetUnitsInTargetArea(
                              out EnemyStructures,
                              Self,
                              SelfPosition,
                              1000,
                              AffectBuildings | AffectEnemies | AffectTurrets) &&
                                 DebugAction("GetCollectionCount ") &&
                        GetCollectionCount(
                              out NumEnemyStructures,
                              EnemyStructures) &&
                        // Sequence name :MaskFailure
                        (
                                    // Sequence name :ChampionOrTowerOrTooFarPastFirstMinion

                                    // Sequence name :TargetAtPlatform?
                                    (
                                      DebugAction("TargetAtPlatform ") &&
                                          GetUnitTeam(
                                                out SelfTeam,
                                                Self) &&
                                          // Sequence name :GetEnemyBase
                                          (
                                                // Sequence name :ChaosTeam
                                                (
                                                      SelfTeam == TeamId.TEAM_CHAOS &&
                                                      MakeVector(
                                                            out EnemyBase,
                                                            27,
                                                            175,
                                                            284)
                                                ) ||
                                                // Sequence name :OrderTeam
                                                (
                                                      SelfTeam == TeamId.TEAM_ORDER &&
                                                      MakeVector(
                                                            out EnemyBase,
                                                            13955,
                                                            176,
                                                            14215)
                                                )
                                          ) &&

                                          DistanceBetweenObjectAndPoint(
                                                out TargetDistanceToBase,
                                                _Target,
                                                EnemyBase) &&
                                          LessFloat(
                                                TargetDistanceToBase,
                                                1000) &&
                                          GetUnitAIClosestTargetInArea(
                                                out _Target,
                                                Self,
                                                default,
                                                true,
                                                SelfPosition,
                                                4000,
                                                AffectBuildings | AffectEnemies | AffectMinions | AffectTurrets)
                                    ) ||
                                    // Sequence name :BuildingNearby?
                                    (
                                    DebugAction("BuildingNearby ") &&
                                          GreaterInt(
                                                NumEnemyStructures,
                                                0) &&
                                          SetVarAttackableUnit(
                                                out _Target,
                                                NearestEnemy)
                                    ) ||
                                    // Sequence name :IsNearestEnemyAChampion?
                                    (
                                      DebugAction("IsNearestEnemyAChampion ") &&
                                          GetUnitType(
                                                out UnitType,
                                                NearestEnemy) &&
                                          UnitType == HERO_UNIT &&
                                          NumEnemyStructures == 0 &&
                                          SetVarAttackableUnit(
                                                out _Target,
                                                NearestEnemy)
                                    ) ||
                                    // Sequence name :TooFarPastFirstMinion
                                    (
                                    DebugAction("TooFarPastFirstMinion ") &&
                                          FindFirstAllyMinionNearby(
                                                out FirstAlliedMinion,
                                                Self,
                                                1000) &&
                                          GetUnitAIBasePosition(
                                                out BasePosition,
                                                Self) &&
                                          DistanceBetweenObjectAndPoint(
                                                out DistanceFirstMinionToBase,
                                                FirstAlliedMinion,
                                                BasePosition) &&
                                          DistanceBetweenObjectAndPoint(
                                                out DistanceTargetToBase,
                                                _Target,
                                                BasePosition) &&
                                          AddFloat(
                                                out DistanceFirstMinionToBase,
                                                DistanceFirstMinionToBase,
                                                75) &&
                                          GreaterFloat(
                                                DistanceTargetToBase,
                                                DistanceFirstMinionToBase) &&
                                          GetDistanceBetweenUnits(
                                                out Distance,
                                                FirstAlliedMinion,
                                                _Target) &&
                                          AddFloat(
                                                out SelfAttackRange,
                                                SelfAttackRange,
                                                75) &&
                                          GreaterFloat(
                                                Distance,
                                                SelfAttackRange) &&
                                          SetVarAttackableUnit(
                                                out _Target,
                                                NearestEnemy)
                                    )
                               || MaskFailure()
                        ) &&
                        SetVarBool(
                              out _TargetValid,
                              true) &&
                        SetVarBool(
                              out _DeAggro,
                              false) &&
                        SetVarVector(
                              out _TargetAcquiredPosition,
                              SelfPosition)


            ;

        __TargetValid = _TargetValid;
        __Target = _Target;
        __DeAggro = _DeAggro;
        __TargetAcquiredPosition = _TargetAcquiredPosition;
        __DeAggroTime = _DeAggroTime;

        return result;
    }
}

