using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class DrMundo_LevelUpClass : AI_Characters
{

    public bool DrMundo_LevelUp(
        AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                  // Sequence name :DrMundoLevelUp

                  // Sequence name :DrMundoLevelUpUltimate
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
                  // Sequence name :DrMundoLevelUpAbility0
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
                  // Sequence name :DrMundoLevelUpAbility1
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
                  // Sequence name :DrMundoLevelUpAbility2
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
                  // Sequence name :DrMundoLevelUpAbility1
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
            ;

    }
}

