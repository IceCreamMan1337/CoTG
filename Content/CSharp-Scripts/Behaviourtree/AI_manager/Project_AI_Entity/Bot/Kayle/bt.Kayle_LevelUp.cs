using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Kayle_LevelUpClass : AI_Characters
{

    public bool Kayle_LevelUp(
        AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                  // Sequence name :KayleLevelUp

                  // Sequence name :Intervention 3
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
                  // Sequence name :Judgement 5
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
                  // Sequence name :Righteous Fury 1
                  (
                        GetUnitSpellLevel(
                              out AbilityLevel2,
                              Self,
                              SPELLBOOK_CHAMPION,
                              2) &&
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
                  // Sequence name :Divine Blessing 1
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
                  // Sequence name :Righteous Fury 5
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
                  // Sequence name :Divine Blessing 5
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

