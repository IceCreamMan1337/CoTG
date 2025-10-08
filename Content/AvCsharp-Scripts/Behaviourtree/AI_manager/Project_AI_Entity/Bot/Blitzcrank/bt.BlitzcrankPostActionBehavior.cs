using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class BlitzcrankPostActionBehaviorClass : AI_Characters 
{
     

     public bool BlitzcrankPostActionBehavior(
          string ActionPerformed,
      AttackableUnit Self
         )
      {
        return
              // Sequence name :Sequence
              (
                    NotEqualString(
                          ActionPerformed,
                          "KillChampion")

              );
      }
}

