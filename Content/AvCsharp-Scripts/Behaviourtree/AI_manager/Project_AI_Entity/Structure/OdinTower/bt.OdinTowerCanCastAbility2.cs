using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;

/*
class OdinTowerCanCastAbility2 : AI_Characters 
{
      AttackableUnit Self,

     public bool OdinTowerCanCastAbility2()
      {
      return
            // Sequence name :OdinTowerCanCastAbility2
            (
                  GetUnitSpellLevel(
                        out AbilityLevel2, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        2) &&
                  GreaterInt(
                        AbilityLevel2, 
                        0) &&
                  GetSpellSlotCooldown(
                        out Spell2Cooldown, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        2) &&
                  LessEqualFloat(
                        Spell2Cooldown, 
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
                              2) &&
                        LessEqualFloat(
                              PAR_Cost, 
                              Current_PAR)
                  ) &&
                  TestCanCastSpell(
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        2, 
                        true)

            ),
      }
}

*/