using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Sion_PostActionBehaviorClass : AI_Characters
{


    public bool Sion_PostActionBehavior(
         AttackableUnit Self,
     string ActionPerformed
        )
    {
        return
                    // Sequence name :Sequence

                    NotEqualString(
                          ActionPerformed,
                          "KillChampion") &&
                    NotEqualString(
                          ActionPerformed,
                          "Attack") &&
                          // Sequence name :ToggleSpell

                          TestUnitHasBuff(
                                Self, default
                                ,
                                "Enrage",
                                true) &&
                          TestCanCastSpell(
                                Self,
                                SPELLBOOK_CHAMPION,
                                2,
                                true) &&
                          CastUnitSpell(
                                Self,
                                SPELLBOOK_CHAMPION,
                                2, default
                                , default
                                )


              ;
    }
}

