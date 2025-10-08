namespace BehaviourTrees.all;


class WanderClass : AI_Characters
{


    public bool Wander(
        out float __WanderUntilTime,
     out string _ActionPerformed,
     AttackableUnit Self,
     float WanderUntilTime
        )
    {
        string ActionPerformed = default;
        float _WanderUntilTime = WanderUntilTime;
        bool result =
                  // Sequence name :Sequence

                  // Sequence name :NotAlreadyCapturingPoint
                  (
                        TestUnitHasBuff(
                              Self,
                              default,
                              "OdinCaptureChannel",
                              false)
                        ||
                        TestUnitIsChanneling(
                              Self,
                              false)
                  ) &&
                  GetGameTime(
                        out CurrentTime) &&
                  SubtractFloat(
                        out TimeDiff,
                        WanderUntilTime,
                        CurrentTime) &&
                  // Sequence name :Selector
                  (
                              // Sequence name :StillWandering

                              GreaterFloat(
                                    TimeDiff,
                                    0)
                         ||
                        // Sequence name :CloseEnoughContinueIdling
                        (
                              GetUnitAITaskPosition(
                                    out TaskPosition) &&
                              DistanceBetweenObjectAndPoint(
                                    out Distance,
                                    Self,
                                    TaskPosition) &&
                              LessFloat(
                                    Distance,
                                    200) &&
                              GenerateRandomFloat(
                                    out TimeToWander,
                                    2.5f,
                                    1f) &&
                              AddFloat(
                                    out _WanderUntilTime,
                                    TimeToWander,
                                    CurrentTime)
                        )
                  ) &&
                  IssueWanderOrder(

                        ) &&
                  SetVarString(
                        out ActionPerformed,
                        "Wander")

            ;
        _ActionPerformed = ActionPerformed;
        __WanderUntilTime = _WanderUntilTime;
        return result;
    }
}

