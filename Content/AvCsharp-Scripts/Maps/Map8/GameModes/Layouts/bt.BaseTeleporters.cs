using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class BaseTeleportersClass : OdinLayout
{

     public bool BaseTeleporters() { 
      return
            // Sequence name :Selector
            (
                  // Sequence name :Initialization
                  (
                        __IsFirstRun == true 
                  ) 
                  // Sequence name :Update
                 /* (
                        ()

                  )*/
            );
      }
}

