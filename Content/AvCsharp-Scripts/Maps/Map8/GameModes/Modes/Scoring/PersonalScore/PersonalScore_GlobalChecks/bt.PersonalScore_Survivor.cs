using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_SurvivorClass : OdinLayout 
{


     public bool PersonalScore_Survivor(
                float Score_Survivor)
      {
      return
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        GetChampionCollection(
                              out AllChamps) &&
                        ForEach(AllChamps,Champ => (
                              // Sequence name :Sequence
                              (
                                    TestUnitHasBuff(
                                          Champ, 
                                          null,
                                          "OdinScoreSurvivor", 
                                          true) &&
                                    IncrementPlayerScore(
                                          Champ, 
                                         ScoreCategory.Combat, 
                                         ScoreEvent.Survivor,
                                          Score_Survivor
                                          ) &&
                                    SpellBuffClear(
                                          Champ,
                                          "OdinScoreSurvivor")

                              ))
                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            );
      }
}

