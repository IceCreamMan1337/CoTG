using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Ashe_LevelUpClass : AI_Characters
{

    public bool Ashe_LevelUp(
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
                  // Sequence name :AsheLevelUp
                  (
                        // Sequence name :AsheLevelUpUltimate
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
                        // Sequence name :AsheLevelUpAbility1
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
                        // Sequence name :AsheLevelUpAbility0
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
                        // Sequence name :AsheLevelUpAbility2
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
            ;
    }
}

