using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;

/*
class OdinTowerCanCastAbility1 : AI_Characters 
{
      AttackableUnit Self,

     public bool OdinTowerCanCastAbility1()
      {
      return
            // Sequence name :OdinTowerCanCastAbility1
            (
                  GetUnitSpellLevel(
                        out Ability1Level, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        1) &&
                  GreaterInt(
                        Ability1Level, 
                        0) &&
                  GetSpellSlotCooldown(
                        out Spell1Cooldown, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        1) &&
                  LessEqualFloat(
                        Spell1Cooldown, 
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
                              1) &&
                        LessEqualFloat(
                              PAR_Cost, 
                              Current_PAR)
                  ) &&
                  TestCanCastSpell(
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        1, 
                        true)

            ),
      }
}

*/