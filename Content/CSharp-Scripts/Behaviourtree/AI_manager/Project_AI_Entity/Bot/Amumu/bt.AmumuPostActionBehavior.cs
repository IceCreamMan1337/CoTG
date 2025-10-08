using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class AmumuPostActionBehaviorClass : AI_Characters
{


    public bool AmumuPostActionBehavior(
            string ActionPerformed,
     AttackableUnit Self
        )
    {
        return
                    // Sequence name :Sequence

                    NotEqualString(
                          ActionPerformed,
                          "KillChampion") &&
                          // Sequence name :ToggleSpell

                          TestUnitHasBuff(
                                Self,
                                default
                                ,
                                "AuraofDespair",
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
                                ,
                                 default
                                )


              ;
    }
}

