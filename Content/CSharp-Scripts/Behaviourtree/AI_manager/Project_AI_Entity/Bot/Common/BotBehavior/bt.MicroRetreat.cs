namespace BehaviourTrees.all;


class MicroRetreatClass : AI_Characters
{


    public bool MicroRetreat(AttackableUnit Self)
    {
        return
                    // Sequence name :Sequence

                    GetUnitAIBasePosition(
                          out BasePosition,
                          Self) &&
                    DistanceBetweenObjectAndPoint(
                          out DistanceToBase,
                          Self,
                          BasePosition) &&
                    // Sequence name :CalculateSafePosition
                    (
                          // Sequence name :NotInBase
                          (
                                GreaterFloat(
                                      DistanceToBase,
                                      3000) &&
                                ComputeUnitAISafePosition(
                                      600,
                                      false,
                                      false) &&
                                GetUnitAISafePosition(
                                      out SafePosition)
                          ) ||
                                // Sequence name :InBase

                                SetVarVector(
                                      out SafePosition,
                                      BasePosition)

                    ) &&
                    IssueMoveToPositionOrder(
                          SafePosition)

              ;
    }
}

