using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class PersonalScore_IncrementClass : OdinLayout
{


    public bool PersonalScore_Increment(
               ScoreCategory ScoreCategory,
   ScoreEvent ScoreEvent,
   float ScoreValue,
   StatEvent StatEvent,
   AttackableUnit RefEnemyTarget,
   float Radius
         )
    {
        return
                    // Sequence name :Sequence

                    GetUnitPosition(
                          out TargetLocation,
                          RefEnemyTarget) &&
                    GetUnitsInTargetArea(
                          out ToGivePoints,
                          RefEnemyTarget,
                          TargetLocation,
                          Radius,
                          AffectEnemies | AffectHeroes) &&
                    ForEach(ToGivePoints, IndividualPointUnit =>
                                // Sequence name :Sequence

                                IncrementPlayerScore(
                                      IndividualPointUnit,
                                      ScoreCategory,
                                      ScoreEvent,
                                      ScoreValue
                                      ) &&
                                IncrementPlayerStat(
                                      IndividualPointUnit,
                                      StatEvent)


                    )
              ;
    }
}

