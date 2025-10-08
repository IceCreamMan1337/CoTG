using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;


class TargetVisibilityUpdatesClass : AI_Characters
{


    public bool TargetVisibilityUpdates(
        out bool __TargetValid,
     out Vector3 __LastKnownPosition,
     out float __LastKnownTime,
     out float __LastKnownHealthRatio,
     bool TargetValid,
     Vector3 LastKnownPosition,
     float LastKnownTime,
     float LastKnownTimeThreshold,
     float LastKnownHealthRatio,
     AttackableUnit Target,
     AttackableUnit Self
        )
    {
        bool _TargetValid = TargetValid;
        Vector3 _LastKnownPosition = LastKnownPosition;
        float _LastKnownTime = LastKnownTime;
        float _LastKnownHealthRatio = LastKnownHealthRatio;


        bool result =
                          // Sequence name :MaskFailure

                          // Sequence name :Selector

                          // Sequence name :TargetIsVisible
                          (
                                TargetValid == true &&
                                TestUnitCondition(
                                      Target,
                                      true) &&
                                GetUnitType(
                                      out TargetUnitType,
                                      Target) &&
                                TargetUnitType == HERO_UNIT &&
                                TestUnitIsVisibleToTeam(
                                      Self,
                                      Target,
                                      true) &&
                                ClearUnitAISpellPosition() &&
                                ComputeUnitAISpellPosition(
                                      Target,
                                      Self,
                                      100,
                                      false) &&
                                GetUnitAISpellPosition(
                                      out _LastKnownPosition) &&
                                ClearUnitAISpellPosition() &&
                                GetUnitHealthRatio(
                                      out _LastKnownHealthRatio,
                                      Target) &&
                                GetGameTime(
                                      out _LastKnownTime)
                          ) ||
                          // Sequence name :TargetIsInvisible
                          (
                                TargetValid == true &&
                                TestUnitCondition(
                                      Target,
                                      true) &&
                                TestUnitIsVisibleToTeam(
                                      Self,
                                      Target,
                                      false) &&
                                // Sequence name :InvalidateTarget
                                (
                                      // Sequence name :PositionOld
                                      (
                                            GetGameTime(
                                                  out CurrentTime) &&
                                            SubtractFloat(
                                                  out TimeDiff,
                                                  CurrentTime,
                                                  LastKnownTime) &&
                                            GreaterFloat(
                                                  TimeDiff,
                                                  LastKnownTimeThreshold) &&
                                            AddString(
                                                  out ToPrint,
                                                  "TimePassed: ",
                                                  $"{TimeDiff}")
                                      ) ||
                                      // Sequence name :AtPosition
                                      (
                                            DistanceBetweenObjectAndPoint(
                                                  out Distance,
                                                  Self,
                                                  LastKnownPosition) &&
                                            LessFloat(
                                                  Distance,
                                                  10)
                                      )
                                ) &&
                                SetVarBool(
                                      out _TargetValid,
                                      false)

                          )


              || MaskFailure()
              ;

        __TargetValid = _TargetValid;
        __LastKnownPosition = _LastKnownPosition;
        __LastKnownTime = _LastKnownTime;
        __LastKnownHealthRatio = _LastKnownHealthRatio;
        return result;
    }
}

