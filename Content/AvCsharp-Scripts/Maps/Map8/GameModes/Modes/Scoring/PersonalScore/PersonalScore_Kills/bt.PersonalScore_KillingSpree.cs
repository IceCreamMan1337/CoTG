using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_KillingSpreeClass : OdinLayout 
{


     public bool PersonalScore_KillingSpree(
                AttackableUnit Killer,
    float KillingSpree_x3_Value,
    float KillingSpree_x4_Value,
    float KillingSpree_x5_Value
          )
      {


      return
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        GetKillingSpreeNumber(
                              out KillingSpreeNumber, 
                              Killer) &&
                        // Sequence name :Selector
                        (
                              // Sequence name :KillingSpreeNumber=3
                              (
                                    KillingSpreeNumber == 3 &&
                                    IncrementPlayerScore(
                                          Killer, 
                                         ScoreCategory.Combat, 
                                         ScoreEvent.KillingSpree,
                                          KillingSpree_x3_Value
                                          )
                              ) ||
                              // Sequence name :KillingSpreeNumber=4
                              (
                                    KillingSpreeNumber == 4 &&
                                    IncrementPlayerScore(
                                          Killer,
                                          ScoreCategory.Combat,
                                         ScoreEvent.Rampage,
                                         KillingSpree_x3_Value
                                          )
                              ) ||
                              // Sequence name :KillingSpreeNumber=5
                              (
                                    KillingSpreeNumber == 5 &&
                                    IncrementPlayerScore(
                                          Killer,
                                         ScoreCategory.Combat,
                                         ScoreEvent.Unstoppable,
                                       KillingSpree_x4_Value
                                          )
                              ) ||
                              // Sequence name :KillingSpreeNumber=6
                              (
                                    KillingSpreeNumber == 6 &&
                                    IncrementPlayerScore(
                                          Killer,
                                         ScoreCategory.Combat,
                                         ScoreEvent.Dominating,
                                         KillingSpree_x4_Value
                                          )
                              ) ||
                              // Sequence name :KillingSpreeNumber=7
                              (
                                    KillingSpreeNumber == 7 &&
                                    IncrementPlayerScore(
                                          Killer,
                                         ScoreCategory.Combat,
                                         ScoreEvent.GodLike,
                                         KillingSpree_x4_Value
                                          )
                              ) ||
                              // Sequence name :KillingSpreeNumber&gt;=8
                              (
                                    GreaterEqualInt(
                                          KillingSpreeNumber, 
                                          8) &&
                                    IncrementPlayerScore(
                                          Killer,
                                          ScoreCategory.Combat,
                                         ScoreEvent.Legendary,
                                          KillingSpree_x4_Value
                                          )

                              )
                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            );
      }
}

