namespace BehaviourTrees.all;

/*
class OdinTowerLevelUp : AI_Characters 
{
      AttackableUnit Self,

     public bool OdinTowerLevelUp()
      {
      return
            // Sequence name :SwainLevelUpSpells
            (
                  GetUnitSkillPoints(
                        out Skillpoints, 
                        Self) &&
                  GreaterInt(
                        Skillpoints, 
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
                  GetUnitSpellLevel(
                        out Ability3Level, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        3) &&
                  // Sequence name :SwainSelectSpellToLevel
                  (
                        // Sequence name :LevelUpUltimate
                        (
                              LessInt(
                                    Ability3Level, 
                                    3) &&
                              TestUnitCanLevelUpSpell(
                                    Self, 
                                    3, 
                                    true) &&
                              LevelUpUnitSpell(
                                    Self, 
                                    SPELLBOOK_CHAMPION, 
                                    3)
                        ) ||
                        // Sequence name :LevelUpQ
                        (
                              LessInt(
                                    Ability0Level, 
                                    5) &&
                              TestUnitCanLevelUpSpell(
                                    Self, 
                                    0, 
                                    true) &&
                              LevelUpUnitSpell(
                                    Self, 
                                    SPELLBOOK_CHAMPION, 
                                    0)
                        ) ||
                        // Sequence name :LevelUpE
                        (
                              LessInt(
                                    Ability2Level, 
                                    5) &&
                              TestUnitCanLevelUpSpell(
                                    Self, 
                                    2, 
                                    true) &&
                              LevelUpUnitSpell(
                                    Self, 
                                    SPELLBOOK_CHAMPION, 
                                    2)
                        ) ||
                        // Sequence name :LevelUpW
                        (
                              LessInt(
                                    Ability1Level, 
                                    5) &&
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
            ),
      }
}

*/