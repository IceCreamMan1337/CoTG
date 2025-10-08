using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Soraka_LevelUpClass : AI_Characters
{

    public bool Soraka_LevelUp(
        AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                  // Sequence name :SorakaLevelUp

                  // Sequence name :SorakaLevelUpUltimate
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
                  // Sequence name :SorakaLevelUpAbility0
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
                  // Sequence name :SorakaLevelUpAbility1
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
                  // Sequence name :SorakaLevelUpAbility2
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
                  // Sequence name :SorakaLevelUpAbility0
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
            ;




    }
}

