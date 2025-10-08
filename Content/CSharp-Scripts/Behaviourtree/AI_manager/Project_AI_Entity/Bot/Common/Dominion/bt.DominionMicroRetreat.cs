namespace BehaviourTrees.all;


class DominionMicroRetreatClass : AI_Characters
{
    public bool DominionMicroRetreat()
    {


        // Sequence name :Bot.Common.Dominion.DominionMicroRetreat
        return
                    // Sequence name :Sequence

                    ComputeUnitAISafePosition(
                          600,
                          true,
                          true) &&
                    GetUnitAISafePosition(
                          out SafePosition) &&
                    IssueMoveToPositionOrder(
                          SafePosition)

              ;
    }
}

