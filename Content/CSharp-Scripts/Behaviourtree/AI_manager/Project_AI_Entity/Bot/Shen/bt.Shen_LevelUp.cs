using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Shen_LevelUpClass : AI_Characters
{

    public bool Shen_LevelUp(
        AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                  // Sequence name :LevelUpSpells

                  GetUnitSkillPoints(
                        out SkillPoints,
                        Self) &&
                  GreaterInt(
                        SkillPoints,
                        0) &&
                  GetUnitSpellLevel(
                        out Ability0Level,
                        Self,
                        SPELLBOOK_CHAMPION,
                        0) &&
                  GetUnitSpellLevel(
                        out Ability1Level,
                        Self,
                        SPELLBOOK_CHAMPION,
                        1) &&
                  GetUnitSpellLevel(
                        out Ability2Level,
                        Self,
                        SPELLBOOK_CHAMPION,
                        2) &&
                  // Sequence name :ShenLevelUp
                  (
                        // Sequence name :ShenLevelUpUltimate
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
                        // Sequence name :ShenLevelUpAbility2
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
                        // Sequence name :ShenLevelUpAbility1
                        (
                              Ability1Level == 0 &&
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    1,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    1)
                        ) ||
                        // Sequence name :ShenLevelUpAbility0
                        (
                              LessEqualInt(
                                    Ability0Level,
                                    Ability1Level) &&
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    0,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    0)
                        ) ||
                        // Sequence name :ShenLevelUpAbility1
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

