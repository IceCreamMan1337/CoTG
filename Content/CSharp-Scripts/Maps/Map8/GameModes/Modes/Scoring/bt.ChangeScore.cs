namespace BehaviourTrees;


class ChangeScoreClass : OdinLayout
{


    public bool ChangeScore(
              out float __ChaosScore,
    out float __OrderScore,
  float ScoreValue,
  float ChaosScore,
  float OrderScore,
  TeamId ScoringTeam,
  bool CanWin


        )


    {
        float _ChaosScore = ChaosScore;
        float _OrderScore = OrderScore;
        bool result =
                          // Sequence name :OrderOrChaos

                          // Sequence name :ChangeScoreForChaos
                          (
                                ScoringTeam == TeamId.TEAM_ORDER &&
                                SubtractFloat(
                                      out _ChaosScore,
                                      ChaosScore,
                                      ScoreValue) &&
                                // Sequence name :MaskFailure
                                (
                                            // Sequence name :Selector

                                            // Sequence name :Sequence
                                            (
                                                  CanWin == false &&
                                                  MaxFloat(
                                                        out _ChaosScore,
                                                        _ChaosScore,
                                                        1)
                                            ) ||
                                            // Sequence name :DrumBeatCheck
                                            (
                                                  MultiplyFloat(
                                                        out LaterScoreValue,
                                                        ScoreValue,
                                                        5) &&
                                                  SubtractFloat(
                                                        out LaterScoreValue,
                                                        ChaosScore,
                                                        LaterScoreValue) &&
                                                  LessEqualFloat(
                                                        LaterScoreValue,
                                                        0) &&
                                                  PlayVOAudioEvent(
                                                        "Odin.SFX.scoretick",
                                                        "Announcer/Odin",
                                                        true)
                                            )

                                      ||
                                       DebugAction("MaskFailure - CALCUL POINT + DrumBeatCheck ")
                                ) &&
                                SetGameScore(
                                      TeamId.TEAM_CHAOS,
                                      ChaosScore)
                          ) ||
                          // Sequence name :ChangeScoreForOrder
                          (
                                ScoringTeam == TeamId.TEAM_CHAOS &&
                                SubtractFloat(
                                      out _OrderScore,
                                      OrderScore,
                                      ScoreValue) &&
                                // Sequence name :MaskFailure
                                (
                                            // Sequence name :Selector

                                            // Sequence name :Sequence
                                            (
                                                  CanWin == false &&
                                                  MaxFloat(
                                                        out _OrderScore,
                                                        _OrderScore,
                                                        1)
                                            ) ||
                                            // Sequence name :DrumBeatCheck
                                            (
                                                  MultiplyFloat(
                                                        out LaterScoreValue,
                                                        ScoreValue,
                                                        5) &&
                                                  SubtractFloat(
                                                        out LaterScoreValue,
                                                        OrderScore,
                                                        LaterScoreValue) &&
                                                  LessEqualFloat(
                                                        LaterScoreValue,
                                                        0) &&
                                                  PlayVOAudioEvent(
                                                        "Odin.SFX.scoretick",
                                                        "Announcer/Odin",
                                                        true)
                                            )

                                      ||
                                       DebugAction("MaskFailure - CALCUL POINT + DrumBeatCheck ")
                                ) &&
                                SetGameScore(
                                      TeamId.TEAM_ORDER,
                                      OrderScore)

                          )
                    ;
        __ChaosScore = _ChaosScore;
        __OrderScore = _OrderScore;
        return result;


    }
}

