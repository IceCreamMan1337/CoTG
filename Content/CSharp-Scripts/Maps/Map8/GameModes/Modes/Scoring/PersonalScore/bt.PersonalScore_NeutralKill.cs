using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class PersonalScore_NeutralKillClass : OdinLayout
{


    public bool PersonalScore_NeutralKill(
               AttackableUnit Killer,
   AttackableUnit DeadUnit
         )
    {
        return
                    // Sequence name :Sequence

                    GetUnitPosition(
                          out KillerPosition,
                          Killer) &&
                    GetUnitsInTargetArea(
                          out ChampsToGivePoints,
                          Killer,
                          KillerPosition,
                          1000,
                          AffectFriends | AffectHeroes) &&
                    ForEach(ChampsToGivePoints, IndividualChamp =>
                                // Sequence name :Sequence

                                IncrementPlayerScore(
                                      IndividualChamp,
                                     ScoreCategory.Total,
                                     ScoreEvent.TeamObjective,
                                      15
                                      ) &&
                                IncrementPlayerStat(
                                      IndividualChamp,
                                      StatEvent.TeamObjective)


                    )
              ;
    }
}

