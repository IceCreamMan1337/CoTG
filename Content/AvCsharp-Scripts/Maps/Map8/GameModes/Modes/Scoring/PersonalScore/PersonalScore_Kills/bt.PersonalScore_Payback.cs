using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_PaybackClass : OdinLayout 
{


     public bool PersonalScore_Payback(
                AttackableUnit DeadUnit,
    AttackableUnit Killer,
    float Score_Payback
          )
      {
      return
            // Sequence name :Sequence
            (
                  TestUnitHasBuff(
                        Killer, 
                        null,
                        "OdinScoreKiller", 
                        true) &&
                  GetUnitBuffCaster(
                        out Killers_killer, 
                        Killer,
                        "OdinScoreKiller") &&
                  Killers_killer == DeadUnit &&
                  IncrementPlayerScore(
                        Killer, 
                       ScoreCategory.Combat, 
                       ScoreEvent.Payback,
                       Score_Payback
                        ) &&
                  SpellBuffClear(
                        DeadUnit,
                        "OdinScoreKiller")

            );
      }
}

