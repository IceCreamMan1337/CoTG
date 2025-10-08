namespace BehaviourTrees;


class OdinTowerPushLaneClass : OdinLayout
{

    public bool OdinTowerPushLane()
    {
        return
                    // Sequence name :OdinTowerPushLane

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

