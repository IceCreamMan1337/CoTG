namespace BehaviourTrees;


class PersonalScore_QuestCompleteClass : OdinLayout
{


    public bool PersonalScore_QuestComplete(

        TeamId TeamCompleted,
   float PointsToAward
         )
    {
        return
                    // Sequence name :Sequence

                    GetChampionCollection(
                          out Champions_) &&
                    ForEach(Champions_, Champ =>
                                // Sequence name :Sequence

                                GetUnitTeam(
                                      out UnitTeam,
                                      Champ) &&
                                UnitTeam == TeamCompleted &&
                                IncrementPlayerScore(
                                      Champ,
                                     ScoreCategory.Objective,
                                     ScoreEvent.QuestComplete,
                                      PointsToAward
                                      ) &&
                                IncrementPlayerStat(
                                      Champ,
                                     StatEvent.TeamObjective)


                    )
              ;
    }
}

