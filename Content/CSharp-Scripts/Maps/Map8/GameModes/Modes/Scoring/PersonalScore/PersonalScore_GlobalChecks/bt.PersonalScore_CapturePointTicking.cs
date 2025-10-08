namespace BehaviourTrees;


class PersonalScore_CapturePointTickingClass : OdinLayout
{

    public bool For_EP_PersonalScore_CapturePointTicking(
               TeamId CapturePointOwnerA,
   TeamId CapturePointOwnerB,
   TeamId CapturePointOwnerC,
   TeamId CapturePointOwnerD,
   TeamId CapturePointOwnerE
         )
    {

        var getCapturePointDelta = new GetCapturePointDeltaClass();
        return
                                // Sequence name :Sequence


                                // Sequence name :GivePersonalRewardsForHeldPoints

                                getCapturePointDelta.GetCapturePointDelta(
                                      out CCDelta,
                                      CapturePointOwnerA,
                                      CapturePointOwnerB,
                                      CapturePointOwnerC,
                                      CapturePointOwnerD,
                                      CapturePointOwnerE) &&
                                SetVarFloat(
                                      out ChaosValue,
                                      0) &&
                                SetVarFloat(
                                      out OrderValue,
                                      0) &&
                                // Sequence name :Selector
                                (
                                      // Sequence name :CCDelta==5
                                      (
                                            CCDelta == 5 &&
                                            SetVarFloat(
                                                  out OrderValue,
                                                  10)
                                      ) ||
                                      // Sequence name :CCDelta==-5
                                      (
                                            CCDelta == -5 &&
                                            SetVarFloat(
                                                  out ChaosValue,
                                                  10)
                                      ) ||
                                      // Sequence name :CCDelta&gt;0
                                      (
                                            GreaterInt(
                                                  CCDelta,
                                                  0) &&
                                            AddFloat(
                                                  out OrderValue,
                                                  1,
                                                  CCDelta)
                                      ) ||
                                      // Sequence name :Sequence
                                      (
                                            LessInt(
                                                  CCDelta,
                                                  0) &&
                                            SubtractFloat(
                                                  out ChaosValue,
                                                  1,
                                                  CCDelta)
                                      ) ||
                                      CCDelta == 0
                                ) &&
                                GetChampionCollection(
                                      out Champions_) &&
                                ForEach(Champions_, Champ =>
                                            // Sequence name :Sequence

                                            GetUnitTeam(
                                                  out ChampTeam,
                                                  Champ) &&
                                            // Sequence name :MaskFailure
                                            (
                                                        // Sequence name :Selector

                                                        // Sequence name :Order
                                                        (
                                                              ChampTeam == TeamId.TEAM_ORDER &&
                                                              GreaterInt(
                                                                    CCDelta,
                                                                    0) &&
                                                              IncrementPlayerScore(
                                                                    Champ,
                                                                    ScoreCategory.Objective,
                                                                   ScoreEvent.Sentinel,
                                                                    OrderValue,
                                                                    false)
                                                        ) ||
                                                        // Sequence name :Chaos
                                                        (
                                                              ChampTeam == TeamId.TEAM_CHAOS &&
                                                              LessInt(
                                                                    CCDelta,
                                                                    0) &&
                                                              IncrementPlayerScore(
                                                                    Champ,
                                                                    ScoreCategory.Objective,
                                                                   ScoreEvent.Sentinel,
                                                                    ChaosValue,
                                                                    false)

                                                        )

                                                  ||
                                 DebugAction("MaskFailure")
                                            )


                          )

              ;
    }
}

