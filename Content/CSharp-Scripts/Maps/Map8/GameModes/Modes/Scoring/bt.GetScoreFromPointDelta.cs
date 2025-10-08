namespace BehaviourTrees;


class GetScoreFromPointDeltaClass : OdinLayout
{


    public bool GetScoreFromPointDelta(
               out float ScoreChange,
   int PointDelta

         )
    {
        float _ScoreChange = default;
        bool result =
                          // Sequence name :Sequence

                          AbsInt(
                                out PointDelta,
                                PointDelta) &&
                          SetVarFloat(
                                out _ScoreChange,
                                0) &&
                          // Sequence name :Selector
                          (
                                // Sequence name :1_Point_Ahead
                                (
                                      PointDelta == 1 &&
                                      SetVarFloat(
                                            out _ScoreChange,
                                            1) &&
                                      DebugAction("team has 1 capture point ")
                                ) ||
                                // Sequence name :2_Points_Ahead
                                (
                                      PointDelta == 2 &&
                                      SetVarFloat(
                                            out _ScoreChange,
                                            1.5f)
                                ) ||
                                // Sequence name :3_Points_Ahead
                                (
                                      PointDelta == 3 &&
                                      SetVarFloat(
                                            out _ScoreChange,
                                            2f)
                                ) ||
                                // Sequence name :4_Points_Ahead
                                (
                                      PointDelta == 4 &&
                                      SetVarFloat(
                                            out _ScoreChange,
                                            2.5f)
                                ) ||
                                // Sequence name :5_Capped
                                (
                                      PointDelta == 5 &&
                                      SetVarFloat(
                                            out _ScoreChange,
                                            5)

                                )
                          )
                    ;
        ScoreChange = _ScoreChange;
        return result;
    }
}

