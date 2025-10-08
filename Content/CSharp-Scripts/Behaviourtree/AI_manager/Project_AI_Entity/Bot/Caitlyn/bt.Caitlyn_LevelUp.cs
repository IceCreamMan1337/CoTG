using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Caitlyn_LevelUpClass : AI_Characters
{

    public bool Caitlyn_LevelUp(
        AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                  // Sequence name :CaitlynLevelUp

                  // Sequence name :CaitlynLevelUpUltimate
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
                  // Sequence name :CaitlynLevelUpAbility2
                  (
                        // Sequence name :LevelCheck
                        (
                              UnitLevel == 2
                              ||
                              UnitLevel == 7
                              ||
                              UnitLevel == 12
                              ||
                              UnitLevel == 13
                              ||
                              UnitLevel == 14
                        ) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              2,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              2)
                  ) ||
                  // Sequence name :CaitlynLevelUpAbility1
                  (
                        // Sequence name :LevelCheck
                        (
                              UnitLevel == 4 ||
                              UnitLevel == 9
                              ||
                              UnitLevel == 15
                              ||
                              UnitLevel == 17 || UnitLevel == 18
                        ) &&
                        TestUnitCanLevelUpSpell(
                              Self,
                              1,
                              true) &&
                        LevelUpUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              1)
                  ) ||
                  // Sequence name :CaitlynLevelUpAbility0
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

