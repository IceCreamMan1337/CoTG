namespace BehaviourTrees;


class PersonalScore_RelicPickupClass : OdinLayout
{


    public bool PersonalScore_RelicPickup(
         float Score_SmallRelic,
   float Score_BigRelic
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

                                      // Sequence name :SmallRelic
                                      (
                                            TestUnitHasBuff(
                                                  Champ,
                                                  null,
                                                  "OdinScoreSmallRelic",
                                                  true) &&
                                            IncrementPlayerScore(
                                                  Champ,
                                                 ScoreCategory.Objective,
                                                 ScoreEvent.ScavengerHunt,
                                                  Score_SmallRelic
                                                  ) &&
                                            SpellBuffClear(
                                                  Champ,
                                                  "OdinScoreSmallRelic")
                                      ) ||
                                      // Sequence name :BigRelic
                                      (
                                            TestUnitHasBuff(
                                                  Champ,
                                                  null,
                                                  "OdinScoreBigRelic",
                                                  true) &&
                                            IncrementPlayerScore(
                                                  Champ,
                                                  ScoreCategory.Objective,
                                                ScoreEvent.MajorRelicPickup,
                                                  Score_BigRelic
                                                  ) &&
                                            SpellBuffClear(
                                                  Champ,
                                                  "OdinScoreBigRelic")

                                      )

                          )
                    )
                    ||
                                 DebugAction("MaskFailure")
              ;
    }
}

