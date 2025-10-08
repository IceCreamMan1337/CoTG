using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_NeutralizeClass : OdinLayout 
{


     public bool PersonalScore_Neutralize(
                float Radius,
    float AssistRadius,
    AttackableUnit CapturedGuardian,
    float PersonalScore_NeutralizeAssist,
    float PersonalScore_Neutralize,
    float PersonalScore_Opportunist,
    bool EnableSecondaryCallouts
          )
      {
      return
            // Sequence name :MaskFailure
            (
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
                              AffectEnemies | AffectHeroes) &&
                        ForEach(UnitsToGivePoints,ReferenceUnit => (
                              // Sequence name :Sequence
                              (
                                    GetDistanceBetweenUnits(
                                          out Distance, 
                                          CapturedGuardian, 
                                          ReferenceUnit) &&
                                    // Sequence name :Selector
                                    (
                                          // Sequence name :Neutralize
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
                                                      out CurrentTime
                                                      ) &&
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
                                                                  PersonalScore_Neutralize, 
                                                                  PersonalScore_Opportunist) &&
                                                            IncrementPlayerScore(
                                                                  ReferenceUnit, 
                                                                  ScoreCategory.Objective, 
                                                                  ScoreEvent.Opportunist,
                                                                  TotalPersonalScore 
                                                                  )
                                                      ) ||
                                                      IncrementPlayerScore(
                                                            ReferenceUnit,
                                                            ScoreCategory.Objective,
                                                           ScoreEvent.NodeNeutralize,
                                                            PersonalScore_Neutralize 
                                                            )
                                                ) &&
                                                IncrementPlayerStat(
                                                      ReferenceUnit, 
                                                      StatEvent.NodeNeutralize)
                                          ) ||
                                          // Sequence name :Assist
                                          (
                                                IncrementPlayerScore(
                                                      ReferenceUnit,
                                                      ScoreCategory.Objective,
                                                    ScoreEvent.NodeNeutralizeAssist,
                                                      PersonalScore_NeutralizeAssist
                                                      ) &&
                                                IncrementPlayerStat(
                                                      ReferenceUnit,
                                                      StatEvent.NodeNeutralizeAssist)

                                          )
                                    )
                              ))
                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            );
      }
}

