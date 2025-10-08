namespace BehaviourTrees;


class PersonalScore_AngelClass : OdinLayout
{


    public bool PersonalScore_Angel(
              float Score_Angel,
   float Score_ArchAngel
         )
    {
        return
                    // Sequence name :MaskFailure

                    // Sequence name :Sequence
                    (
                          GetChampionCollection(
                                out AllChamps) &&
                          ForEach(AllChamps, Champ =>
                                      // Sequence name :Selector

                                      // Sequence name :Angel
                                      (
                                            TestUnitHasBuff(
                                                  Champ,
                                                  null,
                                                  "OdinScoreAngel",
                                                  true) &&
                                            GreaterFloat(
                                                  Score_Angel,
                                                  0) &&
                                            IncrementPlayerScore(
                                                  Champ,
                                                  ScoreCategory.Combat,
                                                ScoreEvent.Angel,
                                                  Score_Angel
                                                  ) &&
                                            SpellBuffClear(
                                                  Champ,
                                                  "OdinScoreAngel")
                                      ) ||
                                      // Sequence name :ArchAngel
                                      (
                                            TestUnitHasBuff(
                                                  Champ,
                                                  null,
                                                  "OdinScoreArchAngel",
                                                  true) &&
                                            IncrementPlayerScore(
                                                  Champ,
                                                  ScoreCategory.Combat,
                                                ScoreEvent.ArchAngel,
                                                  Score_ArchAngel
                                                  ) &&
                                            SpellBuffClear(
                                                  Champ,
                                                  "OdinScoreArchAngel")

                                      )

                          )
                    )
                    ||
                                 DebugAction("MaskFailure")
              ;
    }
}

