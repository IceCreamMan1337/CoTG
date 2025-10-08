using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Yorick_WaitClass : AI_Characters 
{

      bool Yorick_Wait() { 
      return (
            // Sequence name :DoesNothing
            (
                  GetUnitAISelf(
                        out Self)

            )
);
      }
}

