using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SummonerHealClass : AI_Characters 
{
      

     public bool SummonerHeal(
         AttackableUnit Self,
      int HealSlot
         )
      {
        return
              // Sequence name :HealCheck
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
                          HealSlot,
                          -1) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          HealSlot,
                          true) &&
                    GetUnitHealthRatio(
                          out HealthRatio,
                          Self) &&
                    LessFloat(
                          HealthRatio,
                          0.3f) &&
                    SetAIUnitSpellTarget(
                          Self,
                          SPELLBOOK_SUMMONER,
                          HealSlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          HealSlot,
                          default,
                          default
                          )

              );
      }
}

