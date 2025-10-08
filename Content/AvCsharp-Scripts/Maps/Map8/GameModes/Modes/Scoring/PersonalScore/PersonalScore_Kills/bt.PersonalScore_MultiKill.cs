using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_MultiKillClass : OdinLayout 
{


      bool PersonalScore_MultiKill(
                AttackableUnit DeadUnit,
    AttackableUnit Killer,
    int NumberOfKills
          )
      {
      return
            // Sequence name :Sequence
            (
                  GetUnitType(
                        out KillerType, 
                        Killer) &&
                  KillerType == HERO_UNIT &&
                  AddString(
                        out ToPrint,
                        "Number of Kills: ", 
                        $"{NumberOfKills}") &&
                  // Sequence name :x2_x3_x4_x5
                  (
                        // Sequence name :x5
                        (
                              GreaterEqualInt(
                                    NumberOfKills, 
                                    5) &&
                              IncrementPlayerScore(
                                    Killer,
                                     ScoreCategory.Combat,
                                   ScoreEvent.PentaKill,
                                    50
                                    )
                        ) ||
                        // Sequence name :x4
                        (
                              GreaterEqualInt(
                                    NumberOfKills, 
                                    4) &&
                              IncrementPlayerScore(
                                    Killer,
                                    ScoreCategory.Combat,
                                   ScoreEvent.QuadraKill, 
                                   35 
                                    )
                        ) ||
                        // Sequence name :x3
                        (
                              GreaterEqualInt(
                                    NumberOfKills, 
                                    3) &&
                              IncrementPlayerScore(
                                    Killer,
                                    ScoreCategory.Combat,
                                   ScoreEvent.TripleKill,
                                    25
                                    )
                        ) ||
                        // Sequence name :x2
                        (
                              GreaterEqualInt(
                                    NumberOfKills, 
                                    2) &&
                              IncrementPlayerScore(
                                    Killer,
                                    ScoreCategory.Combat,
                                   ScoreEvent.DoubleKill,
                                    15
                                    )

                        )
                  )
            );
      }
}

