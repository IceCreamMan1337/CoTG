namespace BehaviourTrees;


class CalculatePointValues_HelperClass : OdinLayout
{

    public bool CalculatePointValues_Helper(
               out float CalculatedPointValue,
   float BasePointValue,
   TeamId PointOwner,
   TeamId ReferenceTeam
)
    {


        float _CalculatedPointValue = default;
        bool result =
                          // Sequence name :Selector

                          // Sequence name :Neutral
                          (
                                PointOwner == TeamId.TEAM_NEUTRAL &&
                                AddFloat(
                                      out _CalculatedPointValue,
                                      BasePointValue,
                                      0)
                          ) ||
                          // Sequence name :PointOwner!=RefOwner
                          (
                                NotEqualUnitTeam(
                                      PointOwner,
                                      ReferenceTeam) &&
                                AddFloat(
                                      out _CalculatedPointValue,
                                      BasePointValue,
                                      1)
                          ) ||
                          // Sequence name :PointOwner==RefOwner
                          (
                                PointOwner == ReferenceTeam &&
                                AddFloat(
                                      out _CalculatedPointValue,
                                      BasePointValue,
                                      -1)

                          )
                    ;

        CalculatedPointValue = _CalculatedPointValue;
        return result;
    }
}

