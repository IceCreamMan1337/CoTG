using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class PersonalScore_BuffClass : OdinLayout
{


    bool PersonalScore_Buff(
              float Score,
  TeamId Team,
  Vector3 Position,
    float Radius,
  ScoreEvent ScoringEvent,
  String BuffToTest,
  bool RemoveBuff
        )
    {

        return
                   // Sequence name :Sequence

                   // Sequence name :Selector
                   (
                         // Sequence name :TeamIsOrder
                         (
                               Team == TeamId.TEAM_ORDER &&
                               GetTurret(
                                     out ReferenceUnit,
                                     TeamId.TEAM_ORDER,
                                     0,
                                     1)
                         ) ||
                         // Sequence name :TeamIsChaos
                         (
                               Team == TeamId.TEAM_CHAOS &&
                               GetTurret(
                                     out ReferenceUnit,
                                     TeamId.TEAM_CHAOS,
                                     0,
                                     1)
                         )
                   ) &&
                   GetUnitsInTargetArea(
                         out ToGivePoints,
                         ReferenceUnit,
                         Position,
                         Radius,
                         AffectFriends | AffectHeroes) &&
                   ForEach(ToGivePoints, IndividualPointUnit =>
                               // Sequence name :Sequence

                               TestUnitHasBuff(
                                     IndividualPointUnit,
                                     null,
                                     "BuffToTest",
                                     true) &&
                               IncrementPlayerScore(
                                     IndividualPointUnit,
                                   ScoreCategory.Objective,//  CATEGORY_OBJECTIVE, 
                                     ScoringEvent,
                                     Score) &&
                               RemoveBuff == true &&
                               SpellBuffClear(
                                     IndividualPointUnit,
                                     "BuffToTest")


                   )
             ;


    }
}

