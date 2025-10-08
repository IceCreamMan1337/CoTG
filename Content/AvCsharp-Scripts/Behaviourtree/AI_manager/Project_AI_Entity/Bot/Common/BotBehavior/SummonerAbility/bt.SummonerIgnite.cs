using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SummonerIgniteClass : AI_Characters 
{
     

     public bool SummonerIgnite(
          AttackableUnit Self,
      AttackableUnit Target,
      int IgniteSlot
         )
      {
        return
              // Sequence name :IgniteCheck
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
                          IgniteSlot,
                          -1) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          IgniteSlot,
                          true) &&
                    TestUnitHasBuff(
                          Target,
                          default,
                          "SummonerDot",
                          false) &&
                    GetUnitHealthRatio(
                          out TargetHealthRatio,
                          Target) &&
                    LessFloat(
                          TargetHealthRatio,
                          0.3f) &&
                    SetAIUnitSpellTarget(
                          Target,
                          SPELLBOOK_SUMMONER,
                          IgniteSlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          IgniteSlot,
                          default, default
                          )

              );
      }
}

