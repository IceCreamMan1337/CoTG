using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Cassiopeia_LevelUpClass : AI_Characters 
{
      
     public bool Cassiopeia_LevelUp(
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
                        // Sequence name :Petrifying Gaze 3
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
                        // Sequence name :Noxious Blast 1
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
                        // Sequence name :Twin Fang 1
                        (
                              AbilityLevel2 == 0 &&
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    2,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    2)
                        ) ||
                        // Sequence name :Miasma 1
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
                        // Sequence name :Twin Fang or Noxious Blast
                        (
                              // Sequence name :Twin Fang 5
                              (
                                    AbilityLevel2 == AbilityLevel0 &&
                                    TestUnitCanLevelUpSpell(
                                          Self,
                                          2,
                                          true) &&
                                    LevelUpUnitSpell(
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          2)
                              ) ||
                              // Sequence name :Noxious Blast 5
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
                        ) ||
                        // Sequence name :Miasma 5
                        (
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    1,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    1)

                        )
                  )
            );
      }
}

