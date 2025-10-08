namespace BehaviourTrees;


class PersonalScore_AceClass : OdinLayout
{


    public bool PersonalScore_Ace(
               AttackableUnit Killer,
   float Score_Ace,
   AttackableUnit DeadUnit

         )
    {
        return
                    // Sequence name :MaskFailure

                    // Sequence name :Sequence
                    (
                          GetUnitTeam(
                                out KillerTeam,
                                Killer) &&
                          SetVarInt(
                                out EnemyTeamCount,
                                0) &&
                          GetChampionCollection(
                                out AllChampions) &&
                          // Sequence name :MaskFailure
                          (
                               ForEach(AllChampions, IndividualUnit =>
                                            // Sequence name :Sequence

                                            GetUnitTeam(
                                                  out UnitTeam,
                                                  IndividualUnit) &&
                                            NotEqualUnitTeam(
                                                  KillerTeam,
                                                  UnitTeam) &&
                                            TestUnitCondition(
                                                  IndividualUnit,
                                                  true) &&
                                            NotEqualUnit(
                                                  DeadUnit,
                                                  IndividualUnit) &&
                                            AddInt(
                                                  out EnemyTeamCount,
                                                  EnemyTeamCount,
                                                  1)

                                )
                                ||
                                 DebugAction("MaskFailure")
                          ) &&
                          LessEqualInt(
                                EnemyTeamCount,
                                0) &&
                          IncrementPlayerScore(
                                Killer,
                               ScoreCategory.Combat,
                               ScoreEvent.Ace,
                                Score_Ace
                                )

                    )
                    ||
                                 DebugAction("MaskFailure")
              ;
    }
}

