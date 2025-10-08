using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SummonerExhaustClass : AI_Characters 
{
      

     public bool SummonerExhaust(
         AttackableUnit Self,
      AttackableUnit Target,
      int ExhaustSlot
         )
      {
        return
              // Sequence name :ExhaustCheck
              (
                    // Sequence name :Difficulty
                    (
                          TestEntityDifficultyLevel(
                                true,
                             EntityDiffcultyType.DIFFICULTY_INTERMEDIATE)
                                 ||
            TestEntityDifficultyLevel(
                                true,
                               EntityDiffcultyType.DIFFICULTY_ADVANCED)
                    ) &&
                    NotEqualInt(
                          ExhaustSlot,
                          -1) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          ExhaustSlot,
                          true) &&
                    TestUnitHasBuff(
                          Target,
                         default,
                          "SummonerExhaust",
                          false) &&
                    SetAIUnitSpellTarget(
                          Target,
                          SPELLBOOK_SUMMONER,
                          ExhaustSlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          ExhaustSlot,
                          default, default
                          )

              );
      }
}

