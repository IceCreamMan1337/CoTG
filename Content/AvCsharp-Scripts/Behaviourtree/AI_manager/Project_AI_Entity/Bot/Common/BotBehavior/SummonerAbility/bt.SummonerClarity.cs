using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SummonerClarityClass : AI_Characters 
{
     

     public bool SummonerClarity(
          out string _ActionPerformed,
      AttackableUnit Self,
      int ClaritySlot
         )
      {

        string ActionPerformed = "";
        bool result =
              // Sequence name :ClarityCheck
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
                          ClaritySlot,
                          -1) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          ClaritySlot,
                          true) &&
                    GetUnitPARRatio(
                          out PAR_Ratio,
                          Self,
                         PrimaryAbilityResourceType.MANA) &&
                    LessEqualFloat(
                          PAR_Ratio,
                          0.4f) &&
                    SetAIUnitSpellTarget(
                          Self,
                          SPELLBOOK_SUMMONER,
                          ClaritySlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          ClaritySlot,
                          default, default
                          ) &&
                    SetVarString(
                          out ActionPerformed,
                          "SummonerClarity")

              );

        _ActionPerformed = ActionPerformed;
        return result;
      }
}

