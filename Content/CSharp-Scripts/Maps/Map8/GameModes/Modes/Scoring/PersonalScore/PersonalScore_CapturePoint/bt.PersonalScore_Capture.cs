using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class PersonalScore_CaptureClass : OdinLayout
{


    public bool PersonalScore_Capture(
                float Radius,
    float AssistRadius,
    AttackableUnit CapturedGuardian,
    float PersonalScore_CaptureAssist,
    float PersonalScore_Capture,
    float PersonalScore_Strategist,
    bool EnableSecondaryCallouts
          )
    {
        return
                    // Sequence name :MaskFailure

                    // Sequence name :Sequence
                    (
                          GetUnitPosition(
                                out ReferencePosition,
                                CapturedGuardian) &&
                          GetUnitsInTargetArea(
                                out UnitsToGivePoints,
                                CapturedGuardian,
                                ReferencePosition,
                                AssistRadius,
                                AffectFriends | AffectHeroes) &&
                          ForEach(UnitsToGivePoints, ReferenceUnit =>
                                      // Sequence name :Sequence

                                      GetDistanceBetweenUnits(
                                            out Distance,
                                            CapturedGuardian,
                                            ReferenceUnit) &&
                                      // Sequence name :Selector
                                      (
                                            // Sequence name :Capture
                                            (
                                                  LessEqualFloat(
                                                        Distance,
                                                        Radius) &&
                                                  TestUnitHasBuff(
                                                        ReferenceUnit,
                                                        null,
                                                        "OdinCaptureChannel",
                                                        true) &&
                                                  GetGameTime(
                                                        out CurrentTime) &&
                                                  // Sequence name :NinjaOrTideTurner_Augmentation
                                                  (
                                                        // Sequence name :Ninja
                                                        (
                                                              EnableSecondaryCallouts == true &&
                                                              TestUnitHasBuff(
                                                                    ReferenceUnit,
                                                                    null,
                                                                    "OdinScoreNinja",
                                                                    true) &&
                                                              AddFloat(
                                                                    out TotalPersonalScore,
                                                                    PersonalScore_Capture,
                                                                    PersonalScore_Strategist) &&
                                                              IncrementPlayerScore(
                                                                    ReferenceUnit,
                                                                    ScoreCategory.Objective,
                                                                   ScoreEvent.Strategist,
                                                                    TotalPersonalScore
                                                                    )
                                                        ) ||
                                                        IncrementPlayerScore(
                                                              ReferenceUnit,
                                                              ScoreCategory.Objective,
                                                             ScoreEvent.NodeCapture,
                                                              PersonalScore_Capture
                                                              )
                                                  ) &&
                                                  IncrementPlayerStat(
                                                        ReferenceUnit,
                                                          StatEvent.NodeCapture)
                                            ) ||
                                            // Sequence name :Assist
                                            (
                                                  IncrementPlayerScore(
                                                        ReferenceUnit,
                                                        ScoreCategory.Objective,
                                                      ScoreEvent.NodeCaptureAssist,
                                                        PersonalScore_CaptureAssist
                                                        ) &&
                                                  IncrementPlayerStat(
                                                        ReferenceUnit,
                                                      StatEvent.NodeCaptureAssist)

                                            )
                                      )

                          )
                    )
                    ||
                                 DebugAction("MaskFailure")
              ;
    }
}

