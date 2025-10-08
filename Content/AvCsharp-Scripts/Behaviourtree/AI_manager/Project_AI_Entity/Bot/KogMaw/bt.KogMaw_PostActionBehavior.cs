using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class KogMaw_PostActionBehaviorClass : AI_Characters 
{
     

     public bool KogMaw_PostActionBehavior(
          AttackableUnit Self,
      string ActionPerformed
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

