using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Fiddlesticks_LevelUpClass : AI_Characters 
{
      
     public bool Fiddlesticks_LevelUp(
         AttackableUnit Self,
      int UnitLevel
         )
      {
        return
            // Sequence name :FiddlesticksLevelUp
            (
                  // Sequence name :FiddlesticksLevelUpUltimate
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
                  // Sequence name :FiddlesticksLevelUpAbility1
                  (
                        // Sequence name :LevelCheck
                        (
                              UnitLevel == 1
                              ||
                              UnitLevel == 4
                              ||
                              UnitLevel == 9
                              ||
                              UnitLevel == 13
                              ||
                              UnitLevel == 18
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
                  // Sequence name :FiddlesticksLevelUpAbility2
                  (
                        // Sequence name :LevelCheck
                        (
                              UnitLevel == 2 ||
                              UnitLevel == 7 ||
                              UnitLevel == 10 ||
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
                  // Sequence name :FiddlesticksLevelUpAbility0
                  (
                        // Sequence name :LevelCheck
                        (
                              UnitLevel == 3 ||
                              UnitLevel == 5 ||
                              UnitLevel == 8 ||
                              UnitLevel == 14 ||
                              UnitLevel == 17
                        ) &&
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

