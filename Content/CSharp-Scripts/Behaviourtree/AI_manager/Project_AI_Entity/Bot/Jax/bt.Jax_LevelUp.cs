using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Jax_LevelUpClass : AI_Characters
{

    public bool Jax_LevelUp(
        AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                  // Sequence name :Sequence

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
                  // Sequence name :JaxLevelUp
                  (
                        // Sequence name :JaxLevelUpUltimate
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
                        // Sequence name :JaxLevelUpAbility0
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
                        // Sequence name :JaxLevelUpAbility2
                        (
                              LessEqualInt(
                                    Spell2Level,
                                    Spell1Level) &&
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    2,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    2)
                        ) ||
                        // Sequence name :JaxLevelUpAbility1
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
            ;
    }
}

