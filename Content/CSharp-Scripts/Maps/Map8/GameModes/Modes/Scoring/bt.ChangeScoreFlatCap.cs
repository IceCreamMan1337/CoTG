namespace BehaviourTrees;


class ChangeScoreFlatCapClass : OdinLayout
{


    public bool ChangeScoreFlatCap(
               out float __OrderScore,
     out float __ChaosScore,
     out bool ScoreChanged,
   float OrderScore,
   float ChaosScore,
   bool CanWin,
   float FloorValue,
   float ScoreValue,
   TeamId ScoringTeam

         )
    {

        bool _ScoreChanged = default;
        float _ChaosScore = ChaosScore;
        float _OrderScore = OrderScore;
        bool result =
                  // Sequence name :OrderOrChaos

                  // Sequence name :ChangeScoreForChaos
                  (
                        SetVarBool(
                              out _ScoreChanged,
                              false) &&
                        ScoringTeam == TeamId.TEAM_ORDER &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    CanWin == false &&
                                    GreaterFloat(
                                          _ChaosScore,
                                          FloorValue) &&
                                    SubtractFloat(
                                          out _ChaosScore,
                                          ChaosScore,
                                          ScoreValue) &&
                                    SetGameScore(
                                          TeamId.TEAM_CHAOS,
                                          _ChaosScore) &&
                                    SetVarBool(
                                          out _ScoreChanged,
                                          true)
                              )
                              ||
                               DebugAction("MaskFailure")
                        )
                  ) ||
                  // Sequence name :ChangeScoreForOrder
                  (
                        SetVarBool(
                              out _ScoreChanged,
                              false) &&
                        ScoringTeam == TeamId.TEAM_CHAOS &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    CanWin == false &&
                                    GreaterFloat(
                                          _OrderScore,
                                          FloorValue) &&
                                    SubtractFloat(
                                          out _OrderScore,
                                          _OrderScore,
                                          ScoreValue) &&
                                    SetGameScore(
                                          TeamId.TEAM_ORDER,
                                          _OrderScore) &&
                                    SetVarBool(
                                          out _ScoreChanged,
                                          true)

                              )
                              ||
                               DebugAction("MaskFailure")
                        )
                  )
            ;
        ScoreChanged = _ScoreChanged;
        __ChaosScore = _ChaosScore;
        __OrderScore = _OrderScore;
        return result;
    }
}

