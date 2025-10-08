using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Irelia_LevelUpClass : AI_Characters 
{
      
     public bool Irelia_LevelUp(
         AttackableUnit Self,
      int UnitLevel
         )
      {
        return
            // Sequence name :Sequence
            (
                  GetUnitSpellLevel(
                        out Spell0Level,
                        Self,
                        SPELLBOOK_CHAMPION,
                        0) &&
                  GetUnitSpellLevel(
                        out Spell1Level,
                        Self,
                        SPELLBOOK_CHAMPION,
                        1) &&
                  GetUnitSpellLevel(
                        out Spell2Level,
                        Self,
                        SPELLBOOK_CHAMPION,
                        2) &&
                  // Sequence name :IreliaLevelUp
                  (
                        // Sequence name :IreliaLevelUpUltimate
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
                        // Sequence name :Bladesurge 1
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
                        // Sequence name :EquilibriumStrike 1
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
                        // Sequence name :IreliaLevelUpAbility1
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
                        // Sequence name :IreliaLevelUpAbility0
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
                        // Sequence name :IreliaLevelUpAbility2
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
                  )
            );


      }
}

