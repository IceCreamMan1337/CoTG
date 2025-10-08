using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SummonerGhostClass : AI_Characters 
{
      

     public bool SummonerGhost(
         AttackableUnit Self,
      int GhostSlot
         )
      {
        return
              // Sequence name :GhostCheck
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
                          GhostSlot,
                          -1) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          GhostSlot,
                          true) &&
                    SetAIUnitSpellTarget(
                          Self,
                          SPELLBOOK_SUMMONER,
                          GhostSlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          GhostSlot,
                         default,
                          default)

              );
      }
}

