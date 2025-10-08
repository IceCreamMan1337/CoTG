using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class PostActionBehaviorClass : AI_Characters 
{
      

     public bool PostActionBehavior(
         AttackableUnit Self,
      string ActionPerformed
         )
      {
        return
              // Sequence name :ReturnFailure
              (
                    SetVarBool(
                          out Run,
                          false) &&
                    Run == true

              );
      }
}

