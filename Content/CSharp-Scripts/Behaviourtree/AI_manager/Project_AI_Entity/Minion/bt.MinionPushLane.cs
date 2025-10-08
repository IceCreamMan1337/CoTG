namespace BehaviourTrees.all;


class MinionPushLaneClass : AI_Characters
{

    bool MinionPushLane()
    {

        return

                    // Sequence name :MinionPushLane

                    ClearUnitAIAttackTarget() &&
                    // Sequence name :Go To Task Position
                    (
                          // Sequence name :Sequence
                          (
                                TestUnitAIHasTask(
                                      true) &&
                                GetUnitAITaskPosition(
                                      out TaskPosition) &&
                                IssueMoveToPositionOrder(
                                      TaskPosition)
                          ) ||
                          IssueAIEnableTaskOrder()

                    )
              ;
    }
}

