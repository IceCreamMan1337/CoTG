using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Chogath_LevelUpClass : AI_Characters 
{
      
     public bool Chogath_LevelUp(
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
                  // Sequence name :ChogathLevelUp
                  (
                        // Sequence name :ChogathLevelUpUltimate
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
                        // Sequence name :ChogathLevelUpAbility0
                        (
                              GreaterEqualInt(
                                    AbilityLevel1,
                                    AbilityLevel0) &&
                              GreaterEqualInt(
                                    AbilityLevel2,
                                    AbilityLevel0) &&
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    0,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    0)
                        ) ||
                        // Sequence name :ChogathLevelUpAbility1
                        (
                              GreaterEqualInt(
                                    AbilityLevel2,
                                    AbilityLevel1) &&
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    1,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    1)
                        ) ||
                        // Sequence name :ChogathLevelUpAbility2
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

