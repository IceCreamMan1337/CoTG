using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Annie_WaitClass : AI_Characters 
{
  bool Annie_Wait() {
       return  // Sequence name :Bot.Annie.Annie_Wait
        (
              // Sequence name :DoesNothing
              (
                    GetUnitAISelf(
                          out Self)

              )
              );
      }
}

