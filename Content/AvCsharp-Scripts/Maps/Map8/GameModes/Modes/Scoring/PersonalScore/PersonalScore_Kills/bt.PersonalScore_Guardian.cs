using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_GuardianClass : OdinLayout 
{
      

     public bool PersonalScore_Guardian(
          AttackableUnit Killer,
    AttackableUnit DeadUnit,
    float PersonalScore_Guardian
          )
      {
      return
            // Sequence name :Sequence
            (
                  TestUnitHasBuff(
                        DeadUnit, 
                        null,
                        "OdinScoreLowHPAttacker", 
                        true) &&
                  IncrementPlayerScore(
                        Killer, 
                       ScoreCategory.Combat, 
                       ScoreEvent.Guardian,
                        PersonalScore_Guardian
                        ) &&
                  SpellBuffClear(
                        DeadUnit,
                        "OdinScoreLowHPAttacker")

            );
      }
}

