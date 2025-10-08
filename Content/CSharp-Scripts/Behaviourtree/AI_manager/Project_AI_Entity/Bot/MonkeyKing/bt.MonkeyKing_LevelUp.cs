using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class MonkeyKing_LevelUpClass : AI_Characters
{

    public bool MonkeyKing_LevelUp(
        AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                        // Sequence name :Sequence

                        // Sequence name :MonkeyKingLevelUp

                        // Sequence name :MonkeyKingLevelUpUltimate
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
                        // Sequence name :MonkeyKingLevelUpAbility2
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
                        // Sequence name :Decoy 1
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
                        // Sequence name :MonkeyKingLevelUpAbility0
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
                        // Sequence name :MonkeyKingLevelUpAbility1
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

