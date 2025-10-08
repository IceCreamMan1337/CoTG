using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Ziggs_LevelUpClass : AI_Characters 
{
      
     public bool Ziggs_LevelUp(
         AttackableUnit Self,
      int UnitLevel
         )
      {
        return
            // Sequence name :LevelUpSpells
            (
                  GetUnitSpellLevel(
                        out AbilityLevel0,
                        Self,
                        SPELLBOOK_CHAMPION,
                        0) &&
                  GetUnitSpellLevel(
                        out AbilityLevel1,
                        Self,
                        SPELLBOOK_CHAMPION,
                        1) &&
                  GetUnitSpellLevel(
                        out AbilityLevel2,
                        Self,
                        SPELLBOOK_CHAMPION,
                        2) &&
                  // Sequence name :LevelUp
                  (
                        // Sequence name :LevelUpUltimate
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
                        // Sequence name :LevelUp0_First
                        (
                              AbilityLevel0 == 0 &&
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    0,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    0)
                        ) ||
                        // Sequence name :LevelUp1_First
                        (
                              AbilityLevel1 == 0 &&
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    1,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    1)
                        ) ||
                        // Sequence name :LevelUpAbility1
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
                        // Sequence name :LevelUpAbility2
                        (
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    2,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    2)
                        ) ||
                        // Sequence name :LevelUpAbility0
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
                  )
            );




      }
}

