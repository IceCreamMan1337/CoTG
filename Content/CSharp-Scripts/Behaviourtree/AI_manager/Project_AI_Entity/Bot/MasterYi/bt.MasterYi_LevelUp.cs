using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class MasterYi_LevelUpClass : AI_Characters
{

    public bool MasterYi_LevelUp(
        AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                  // Sequence name :MasterYiLevelUp

                  // Sequence name :MasterYiLevelUpUltimate
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
                  // Sequence name :MasterYiLevelUpAbility0
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
                  // Sequence name :MasterYiLevelUpAbility2
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
                  // Sequence name :MasterYiLevelUpAbility1
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
                  // Sequence name :MasterYiLevelUpAbility2
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
            ;
    }
}

