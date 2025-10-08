using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class SummonerSurgeClass : AI_Characters
{


    public bool SummonerSurge(
         AttackableUnit Self,
     int SurgeSlot
        )
    {
        return
                    // Sequence name :SurgeCheck

                    // Sequence name :Difficulty
                    (
                          TestEntityDifficultyLevel(
                                true,
                             EntityDiffcultyType.DIFFICULTY_INTERMEDIATE)
                          ||
                          TestEntityDifficultyLevel(
                                true,
                             EntityDiffcultyType.DIFFICULTY_ADVANCED)
                    ) &&
                    NotEqualInt(
                          SurgeSlot,
                          -1) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          SurgeSlot,
                          true) &&
                    SetAIUnitSpellTarget(
                          Self,
                          SPELLBOOK_SUMMONER,
                          SurgeSlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          SurgeSlot,
                          default,
                         default)

              ;
    }
}

