using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Sona_LevelUpClass : AI_Characters 
{
      
     public bool Sona_LevelUp(
         AttackableUnit Self,
      int UnitLevel
         )
      {
        return
            // Sequence name :SonaLevelUp
            (
                  // Sequence name :SonaLevelUpUltimate
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
                  // Sequence name :SonaLevelUpAbility0
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
                  // Sequence name :SonaLevelUpAbility1
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
                  // Sequence name :SonaLevelUpAbility2
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
                  // Sequence name :SonaLevelUpAbility0
                  (
                        TestUnitCanLevelUpSpell(
                              Self,
                              0,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              0)
                  ) ||
                  // Sequence name :SonaLevelUpAbility2
                  (
                        TestUnitCanLevelUpSpell(
                              Self,
                              2,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              2)

                  )
            );




      }
}

