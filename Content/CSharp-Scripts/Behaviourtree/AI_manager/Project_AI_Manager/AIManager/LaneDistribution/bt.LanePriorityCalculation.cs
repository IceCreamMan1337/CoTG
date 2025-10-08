namespace BehaviourTrees;


class LanePriorityCalculationClass : AI_LaneDistribution
{


    public bool LanePriorityCalculation(
    out float LanePriority,
    float LanePresenceIndicator
        )
    {

        float _LanePriority = default;

        bool result =

                      // Sequence name :Calculate_Lane_Priority

                      // Sequence name :LanePresencePositive
                      (
                            GreaterEqualFloat(
                                  LanePresenceIndicator,
                                  0) &&
                            InterpolateLine(
                                  out _LanePriority,
                                  0,
                                  1,
                                  0,
                                  0.05f,
                                  0,
                                  1,
                                  LanePresenceIndicator) &&
                            MinFloat(
                                  out _LanePriority,
                                  0.05f,
                                  _LanePriority)
                      ) ||
                      // Sequence name :LanePresenceNegative
                      (
                            MultiplyFloat(
                                  out temp,
                                  LanePresenceIndicator,
                                  -1) &&
                            InterpolateLine(
                                  out _LanePriority,
                                  0,
                                  1,
                                  0,
                                  1,
                                  0,
                                  1,
                                  temp)

                      )
                ;
        LanePriority = _LanePriority;
        return result;
    }
}

