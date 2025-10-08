using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Rammus_LevelUpClass : AI_Characters 
{
      
     public bool Rammus_LevelUp(
         AttackableUnit Self,
      int UnitLevel
         )
      {
        return
            // Sequence name :RammusLevelUp
            (
                  // Sequence name :RammusLevelUpUltimate
                  (
                        TestUnitCanLevelUpSpell(
                              Self,
                              3,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              3)
                  ) ||
                  // Sequence name :RammusLevelUpAbility1
                  (
                        // Sequence name :LevelCheck
                        (
                              UnitLevel == 1 ||
                              UnitLevel == 5 ||
                              UnitLevel == 8 ||
                              UnitLevel == 13 ||
                              UnitLevel == 17
                        ) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              1,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              1)
                  ) ||
                  // Sequence name :RammusLevelUpAbility2
                  (
                        // Sequence name :LevelCheck
                        (
                              UnitLevel == 2 ||
                              UnitLevel == 4 ||
                              UnitLevel == 7 ||
                              UnitLevel == 12 ||
                              UnitLevel == 15
                        ) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              2,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              2)
                  ) ||
                  // Sequence name :RammusLevelUpAbility0
                  (
                        TestUnitCanLevelUpSpell(
                              Self,
                              0,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              0)

                  )
            );
      }
}

