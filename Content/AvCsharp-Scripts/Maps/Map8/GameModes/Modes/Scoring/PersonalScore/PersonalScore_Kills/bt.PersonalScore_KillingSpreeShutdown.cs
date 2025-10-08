using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_KillingSpreeShutdownClass : OdinLayout 
{


     public bool PersonalScore_KillingSpreeShutdown(
                int KillingSpreeBeforeDeath,
    AttackableUnit DeadUnit,
    AttackableUnit Killer,
    float Score_SpreeShutdown2,
    float Score_SpreeShutdown1,
    float Score_SpreeShutdown0
          )
      {
      return
            // Sequence name :MaskFailure
            (
                  // Sequence name :Selector
                  (
                        // Sequence name :Sequence
                        (
                              GreaterEqualInt(
                                    KillingSpreeBeforeDeath, 
                                    8) &&
                              IncrementPlayerScore(
                                    Killer, 
                                   ScoreCategory.Combat, 
                                   ScoreEvent.BountyHunter2,
                                    Score_SpreeShutdown2
                                    )
                        ) ||
                        // Sequence name :Sequence
                        (
                              GreaterEqualInt(
                                    KillingSpreeBeforeDeath, 
                                    5) &&
                              IncrementPlayerScore(
                                    Killer, 
                                    ScoreCategory.Combat, 
                                   ScoreEvent.BountyHunter2,
                                    Score_SpreeShutdown1
                                    )
                        ) ||
                        // Sequence name :Sequence
                        (
                              GreaterEqualInt(
                                    KillingSpreeBeforeDeath, 
                                    3) &&
                              IncrementPlayerScore(
                                    Killer,
                                    ScoreCategory.Combat,
                                   ScoreEvent.BountyHunter2,
                                    Score_SpreeShutdown0
                                    )

                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            );
      }
}

