using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;


class KillChampionAcquisitionClass : AI_Characters
{

    private FindTargetByDistanceHealthScoreClass findTargetByDistanceHealthScore = new();
    private DistanceHealthScoreClass distanceHealthScore = new();
    public bool KillChampionAcquisition(
         out AttackableUnit _KillChampionTarget,
     AttackableUnit Self,
     Vector3 SelfPosition,
     bool TargetValid,
     AttackableUnit PreviousTarget,
     Vector3 LastKnownPosition,
     float TargetAcquisitionRange
        )
    {

        AttackableUnit KillChampionTarget = default;
        bool result =
                    // Sequence name :AcquireLowHealthChampion

                    SetVarFloat(
                          out BestScore,
                          -1) &&
                    // Sequence name :MaskFailure
                    (
                             (DebugAction("FindTargetByDistanceHealthScore") &&
                          findTargetByDistanceHealthScore.FindTargetByDistanceHealthScore(
                                out KillChampionTarget,
                                out BestScore,
                                Self,
                                TargetAcquisitionRange))
                          || MaskFailure()
                    ) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :UseMemoryOfPreviousTarget
                          (
                                TargetValid == true &&
                                TestUnitCondition(
                                      PreviousTarget,
                                      true) &&
                                TestUnitIsVisibleToTeam(
                                      Self,
                                      PreviousTarget,
                                      false) &&
                                GetUnitType(
                                      out UnitType,
                                      PreviousTarget) &&
                                UnitType == HERO_UNIT &&
                                DistanceBetweenObjectAndPoint(
                                      out Distance,
                                      Self,
                                      LastKnownPosition) &&
                                LessFloat(
                                      Distance,
                                      TargetAcquisitionRange) &&
                                GetUnitCurrentHealth(
                                      out PreviousTargetHealth,
                                      PreviousTarget) &&
                                distanceHealthScore.DistanceHealthScore(
                                      out PreviousTargetScore,
                                      PreviousTargetHealth,
                                      Distance) &&
                                GreaterFloat(
                                      PreviousTargetScore,
                                      BestScore) &&
                                SetVarAttackableUnit(
                                      out KillChampionTarget,
                                      PreviousTarget) &&
                                SetVarFloat(
                                      out BestScore,
                                      PreviousTargetScore)
                          )
                           || MaskFailure()
                    ) &&
                    GreaterEqualFloat(
                          BestScore,
                          0)

              ;
        _KillChampionTarget = KillChampionTarget;
        return result;
    }

}

