using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class OdinTowerCanCastAbility3Class : OdinLayout 
{


   public bool OdinTowerCanCastAbility3(
              AttackableUnit Self
          )
      {
      return
            // Sequence name :OdinTowerCanCastAbility3
            (
                  GetUnitSpellLevel(
                        out AbilityLevel3, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        3) &&
                  GreaterInt(
                        AbilityLevel3, 
                        0) &&
                  GetSpellSlotCooldown(
                        out Spell3Cooldown, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        3) &&
                  LessEqualFloat(
                        Spell3Cooldown, 
                        0) &&
                  // Sequence name :PAR_Cost_Check
                  (
                        GetUnitPARType(
                              out PAR_Type, 
                              Self) &&
                        GetUnitCurrentPAR(
                              out Current_PAR, 
                              Self, 
                              PAR_Type) &&
                        GetUnitSpellCost(
                              out PAR_Cost, 
                              Self, 
                              SPELLBOOK_CHAMPION, 
                              3) &&
                        LessEqualFloat(
                              PAR_Cost, 
                              Current_PAR)
                  ) &&
                  TestCanCastSpell(
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        3, 
                        true)

            );
      }
}

