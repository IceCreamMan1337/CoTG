using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Shyvana_LevelUpClass : AI_Characters
{

    public bool Shyvana_LevelUp(
        AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                        // Sequence name :Sequence

                        // Sequence name :ShyvanaLevelUp

                        // Sequence name :Dragons Descent 1
                        (
                              GetUnitSpellLevel(
                                    out AbilityLevel,
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    3) &&
                              LessEqualInt(
                                    AbilityLevel,
                                    0) &&
                              TestUnitCanLevelUpSpell(
                                    Self,
                                    3,
                                    true) &&
                              LevelUpUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    3) &&
                              IncPARbt(
                                    Self,
                                    100,
                                    PrimaryAbilityResourceType.Other)
                        ) ||
                        // Sequence name :ShyvanaLevelUpUltimate
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
                        // Sequence name :ShyvanaLevelUpAbility1
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
                        // Sequence name :Twin Fang 1
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
                        // Sequence name :ShyvanaLevelUpAbility2
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
                        // Sequence name :ShyvanaLevelUpAbility0
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

