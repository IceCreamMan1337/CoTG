using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Udyr_LevelUpClass : AI_Characters 
{
      
     public bool Udyr_LevelUp(
         AttackableUnit Self,
      int UnitLevel
         )
      {
        return
            // Sequence name :UdyrLevelUp
            (
                  // Sequence name :Phoenix 1
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              3) &&
                        LessEqualInt(
                              AbilityLevel,
                              0) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              3,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              3)
                  ) ||
                  // Sequence name :Turtle 1
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              1) &&
                        LessEqualInt(
                              AbilityLevel,
                              0) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              1,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              1)
                  ) ||
                  // Sequence name :Tiger 1
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              0) &&
                        LessEqualInt(
                              AbilityLevel,
                              0) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              0,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              0)
                  ) ||
                  // Sequence name :Bear 1
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              2) &&
                        LessEqualInt(
                              AbilityLevel,
                              0) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              2,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              2)
                  ) ||
                  // Sequence name :Tiger 2
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              0) &&
                        LessEqualInt(
                              AbilityLevel,
                              1) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              0,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              0)
                  ) ||
                  // Sequence name :Turtle 2
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              1) &&
                        LessEqualInt(
                              AbilityLevel,
                              1) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              1,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              1)
                  ) ||
                  // Sequence name :Tiger 3
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              0) &&
                        LessEqualInt(
                              AbilityLevel,
                              2) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              0,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              0)
                  ) ||
                  // Sequence name :Phoenix 2
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              3) &&
                        LessEqualInt(
                              AbilityLevel,
                              1) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              3,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              3)
                  ) ||
                  // Sequence name :Tiger 4
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              0) &&
                        LessEqualInt(
                              AbilityLevel,
                              3) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              0,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              0)
                  ) ||
                  // Sequence name :Turtle 3
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              1) &&
                        LessEqualInt(
                              AbilityLevel,
                              2) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              1,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              1)
                  ) ||
                  // Sequence name :Tiger 5
                  (
                        TestUnitCanLevelUpSpell(
                              Self,
                              0,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              1)
                  ) ||
                  // Sequence name :Turtle 4
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              1) &&
                        LessEqualInt(
                              AbilityLevel,
                              3) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              1,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              1)
                  ) ||
                  // Sequence name :Phoenix 3
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              3) &&
                        LessEqualInt(
                              AbilityLevel,
                              2) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              3,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              3)
                  ) ||
                  // Sequence name :Turtle5
                  (
                        TestUnitCanLevelUpSpell(
                              Self,
                              1,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              1)
                  ) ||
                  // Sequence name :Bear 4
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel,
                              Self,
                              SPELLBOOK_CHAMPION,
                              2) &&
                        LessEqualInt(
                              AbilityLevel,
                              3) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              2,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              2)
                  ) ||
                  // Sequence name :Phoenix 4
                  (
                        TestUnitCanLevelUpSpell(
                              Self,
                              3,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              3)

                  )
            );
      }
}

