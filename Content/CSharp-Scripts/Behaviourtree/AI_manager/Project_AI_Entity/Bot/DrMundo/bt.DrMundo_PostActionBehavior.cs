using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class DrMundo_PostActionBehaviorClass : AI_Characters
{


    public bool DrMundo_PostActionBehavior(
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
                                Self,
                                default
                                ,
                                "BurningAgony",
                                true) &&
                          TestCanCastSpell(
                                Self,
                                SPELLBOOK_CHAMPION,
                                1,
                                true) &&
                          CastUnitSpell(
                                Self,
                                SPELLBOOK_CHAMPION,
                                1,
                                default
                                , default

                                )


              ;
    }
}

