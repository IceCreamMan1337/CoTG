using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_AvengerClass : OdinLayout 
{


      bool PersonalScore_Avenger(
                AttackableUnit Killer,
    AttackableUnit DeadUnit,
    float Score_Avenger
          )
      {
      return
            // Sequence name :Sequence
            (
                  TestUnitHasBuff(
                        DeadUnit, 
                        null,
                        "OdinScoreAvengerTarget", 
                        true) &&
                  IncrementPlayerScore(
                        Killer, 
                       ScoreCategory.Combat, 
                       ScoreEvent.Avenger,
                        Score_Avenger
                        ) &&
                  SpellBuffClear(
                        DeadUnit,
                        "OdinScoreAvengerTarget")

            );
      }
}

