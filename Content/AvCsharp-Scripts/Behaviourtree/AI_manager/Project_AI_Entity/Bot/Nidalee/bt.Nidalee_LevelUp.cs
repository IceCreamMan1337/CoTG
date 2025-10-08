using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Nidalee_LevelUpClass : AI_Characters 
{
      
     public bool Nidalee_LevelUp(
         AttackableUnit Self,
      int UnitLevel
         )
      {
        return
            // Sequence name :NidaleeLevelUp
            (
                  // Sequence name :AspectOfTheCougar3
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
                  // Sequence name :JavelinToss1
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel0,
                              Self,
                              SPELLBOOK_CHAMPION,
                              0) &&
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
                  // Sequence name :Bushwhack1
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel1,
                              Self,
                              SPELLBOOK_CHAMPION,
                              1) &&
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
                  // Sequence name :PrimalSurge5
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
                  // Sequence name :JavelinToss5
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
                  // Sequence name :Bushwhack5
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
            );
      }
}

