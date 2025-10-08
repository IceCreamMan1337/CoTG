using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class DominionReturnToBaseClass : AI_Characters
{


    public bool DominionReturnToBase(
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
                  // Sequence name :Sequence

                  TestUnitHasBuff(
                        Self,
                        default,
                        "OdinCaptureChannel",
                        false) &&
                  TestAIEntityHasTask(
                        Self,
                      AITaskTopicType.DOMINION_RETURN_TO_BASE,
                        default,
                       default,
                        default,
                        true) &&
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
                              GetUnitAIBasePosition(
                                    out BaseLocation,
                                    Self) &&
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
                              SetVarFloat(
                                    out EnemyCheckRange,
                                    1400) &&
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
                        // Sequence name :TeleportHome
                        (
                              IssueTeleportToBaseOrder() &&
                              SetVarBool(
                                    out _TeleportHome,
                                    true)
                        )
                  ) &&
                  SetVarString(
                        out ActionPerformed,
                        "ReturnToBase")

            ;

        __TeleportHome = _TeleportHome;
        _ActionPerformed = ActionPerformed;
        return result;
    }
}

