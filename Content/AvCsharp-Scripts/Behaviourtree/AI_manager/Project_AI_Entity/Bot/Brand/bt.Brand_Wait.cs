using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Brand_WaitClass : AI_Characters 
{

     bool Brand_Wait() { 
     
        return
        (
            // Sequence name :DoesNothing
            (
                  GetUnitAISelf(
                        out Self)

            )
        );
      }
}

