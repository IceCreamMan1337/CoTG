using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

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
              (
                    NotEqualString(
                          ActionPerformed,
                          "KillChampion") &&
                    // Sequence name :ToggleSpell
                    (
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

                    )
              );
      }
}

