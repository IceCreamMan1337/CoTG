namespace BehaviourTrees.Map8;


class OverallScoreMathClass : OdinLayout
{


    public bool OverallScoreMath(
               out float __WinningTeamScore,
   int CapturePointDelta
        ,
   float WinningTeamScore,
   TeamId WinningTeam
         )
    {

        float _WinningTeamScore = WinningTeamScore;
        bool result =
                  // Sequence name :Sequence

                  // Sequence name :Selector
                  (
                        // Sequence name :1 ahead
                        (
                              CapturePointDelta == 1 &&
                              SubtractFloat(
                                    out _WinningTeamScore,
                                    _WinningTeamScore,
                                    1)
                        ) ||
                        // Sequence name :2 ahead
                        (
                              CapturePointDelta == 2 &&
                              SubtractFloat(
                                    out _WinningTeamScore,
                                    _WinningTeamScore,
                                    1.5f)
                        ) ||
                        // Sequence name :3 ahead
                        (
                              CapturePointDelta == 3 &&
                              SubtractFloat(
                                    out _WinningTeamScore,
                                    _WinningTeamScore,
                                    2)
                        ) ||
                        // Sequence name :4 ahead
                        (
                              CapturePointDelta == 4 &&
                              SubtractFloat(
                                    out _WinningTeamScore,
                                    _WinningTeamScore,
                                    4)
                        ) ||
                              // Sequence name :Rofl stomp

                              SubtractFloat(
                                    out _WinningTeamScore,
                                    _WinningTeamScore,
                                    8)

                  ) &&
                  SetGameScore(
                        WinningTeam,
                        _WinningTeamScore)

            ;
        __WinningTeamScore = _WinningTeamScore;
        return result;
    }
}

