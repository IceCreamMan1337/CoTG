namespace BehaviourTrees;


class ScoreManagerClass : OdinLayout
{

    private DebugSayAllClass debugSayAll = new();
    private CountPointsClass countPoints = new();
    private ChangeScoreClass changeScore = new();
    private GetScoreFromPointDeltaClass getScoreFromPointDelta = new();
    private NexusVerticalBeamManagerClass nexusVerticalBeamManager = new();
    public bool ScoreManager(
                out float __PreviousTickTime,
      out float __OrderScore,
      out float __ChaosScore,
      out int __CCDelta,
      out bool __OverTimeLimitWarning,
      out bool __NexusVerticalBeamEnabledOrder,
      out bool __NexusVerticalBeamEnabledChaos,
      out uint __NexusVerticalBeamOrder_1,
      out uint __NexusVerticalBeamOrder_2,
      out uint __NexusVerticalBeamChaos_1,
      out uint __NexusVerticalBeamChaos_2,
    TeamId CapturePointOwnerA,
    TeamId CapturePointOwnerB,
    TeamId CapturePointOwnerC,
    TeamId CapturePointOwnerD,
    TeamId CapturePointOwnerE,
    float ChaosScore,
    float OrderScore,
    bool OverTimeLimitWarning,
    bool NexusVerticalBeamEnabledOrder,
    bool NexusVerticalBeamEnabledChaos,
    uint NexusVerticalBeamOrder_1,
    uint NexusVerticalBeamOrder_2,
    uint NexusVerticalBeamChaos_1,
    uint NexusVerticalBeamChaos_2,
    GameObject OrderLevelProp,
    GameObject ChaosLevelProp)
    {
        float _PreviousTickTime = default;
        int _CCDelta = default;
        float _ChaosScore = ChaosScore;
        float _OrderScore = OrderScore;
        bool _OverTimeLimitWarning = OverTimeLimitWarning;
        bool _NexusVerticalBeamEnabledOrder = NexusVerticalBeamEnabledOrder;
        bool _NexusVerticalBeamEnabledChaos = NexusVerticalBeamEnabledChaos;
        uint _NexusVerticalBeamOrder_1 = NexusVerticalBeamOrder_1;
        uint _NexusVerticalBeamOrder_2 = NexusVerticalBeamOrder_2;
        uint _NexusVerticalBeamChaos_1 = NexusVerticalBeamChaos_1;
        uint _NexusVerticalBeamChaos_2 = NexusVerticalBeamChaos_2;


        bool result =
                  // Sequence name :MaskFailure

                  // Sequence name :Update_Global_Score
                  (
                        countPoints.CountPoints(
                              out TotalOrderPoints,
                              CapturePointOwnerA,
                              CapturePointOwnerB,
                              CapturePointOwnerC,
                              CapturePointOwnerD,
                              CapturePointOwnerE,
                              TeamId.TEAM_ORDER) &&
                        countPoints.CountPoints(
                              out TotalChaosPoints,
                              CapturePointOwnerA,
                              CapturePointOwnerB,
                              CapturePointOwnerC,
                              CapturePointOwnerD,
                              CapturePointOwnerE,
                              TeamId.TEAM_CHAOS) &&

                        SubtractInt(
                              out _CCDelta,
                              TotalOrderPoints,
                              TotalChaosPoints) &&
                        // Sequence name :MaskFailure
                        (
                                    // Sequence name :Selector

                                    // Sequence name :Warning
                                    (
                                          OverTimeLimitWarning == false &&
                                          GetGameTime(
                                                out CurrentTime) &&
                                          GreaterEqualFloat(
                                                CurrentTime,
                                                5400) &&
                                          debugSayAll.DebugSayAll(
                                                "Sudden Death") &&
                                          SetVarBool(
                                                out _OverTimeLimitWarning,
                                                true)
                                    ) ||
                                    // Sequence name :PointTickDown
                                    (
                                          OverTimeLimitWarning == true &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore,
                                                out _OrderScore,
                                                1,
                                                _ChaosScore,
                                                _OrderScore,
                                                TeamId.TEAM_ORDER,
                                                true) &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore,
                                                out _OrderScore,
                                                1,
                                                _ChaosScore,
                                                _OrderScore,
                                                TeamId.TEAM_CHAOS,
                                                true)
                                    )


                               ||
                                    DebugAction("MaskFailure - no sudden death ")
                        ) &&
                        // Sequence name :MaskFailure
                        (
                                    // Sequence name :WhoIsWinning?

                                    // Sequence name :Order_Winning
                                    (
                                          GreaterInt(
                                                _CCDelta,
                                                0) &&

                                          getScoreFromPointDelta.GetScoreFromPointDelta(
                                                out ScoreChange,
                                                _CCDelta) &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore,
                                                out _OrderScore,
                                                ScoreChange,
                                                _ChaosScore,
                                                _OrderScore,
                                                TeamId.TEAM_ORDER,
                                                true)
                                    ) ||
                                    // Sequence name :Chaos_Winning
                                    (
                                          LessInt(
                                                _CCDelta,
                                                0) &&
                                          getScoreFromPointDelta.GetScoreFromPointDelta(
                                                out ScoreChange,
                                                _CCDelta) &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore,
                                                out _OrderScore,
                                                ScoreChange,
                                                _ChaosScore,
                                                _OrderScore,
                                                TeamId.TEAM_CHAOS,
                                                true)
                                    )

                              ||
                               DebugAction("MaskFailure - who is winning failed  ")
                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    SetVarBool(
                                          out GameOver,
                                          false) &&
                                    // Sequence name :Selector
                                    (
                                          LessFloat(
                                                ChaosScore,
                                                1) ||
                                          LessFloat(
                                                OrderScore,
                                                1)
                                    ) &&
                                    SetVarBool(
                                          out GameOver,
                                          true)
                              )
                              ||
                               DebugAction("MaskFailure - GAMEOVER")
                        ) &&
                        nexusVerticalBeamManager.NexusVerticalBeamManager(
                              out _NexusVerticalBeamEnabledOrder,
                              out _NexusVerticalBeamEnabledChaos,
                              out _NexusVerticalBeamOrder_1,
                              out _NexusVerticalBeamOrder_2,
                              out _NexusVerticalBeamChaos_1,
                              out _NexusVerticalBeamChaos_2,
                              NexusVerticalBeamEnabledOrder,
                              NexusVerticalBeamEnabledChaos,
                              NexusVerticalBeamOrder_1,
                              NexusVerticalBeamOrder_2,
                              NexusVerticalBeamChaos_1,
                              NexusVerticalBeamChaos_2,
                              TotalOrderPoints,
                              TotalChaosPoints,
                              OrderLevelProp,
                              ChaosLevelProp,
                              GameOver)

                  )
                  ||
                               DebugAction("MaskFailure")
            ;
        __PreviousTickTime = _PreviousTickTime;
        __CCDelta = _CCDelta;
        __ChaosScore = _ChaosScore;
        __OrderScore = _OrderScore;
        __OverTimeLimitWarning = _OverTimeLimitWarning;
        __NexusVerticalBeamEnabledOrder = _NexusVerticalBeamEnabledOrder;
        __NexusVerticalBeamEnabledChaos = _NexusVerticalBeamEnabledChaos;
        __NexusVerticalBeamOrder_1 = _NexusVerticalBeamOrder_1;
        __NexusVerticalBeamOrder_2 = _NexusVerticalBeamOrder_2;
        __NexusVerticalBeamChaos_1 = _NexusVerticalBeamChaos_1;
        __NexusVerticalBeamChaos_2 = _NexusVerticalBeamChaos_2;
        return result;
    }
}

