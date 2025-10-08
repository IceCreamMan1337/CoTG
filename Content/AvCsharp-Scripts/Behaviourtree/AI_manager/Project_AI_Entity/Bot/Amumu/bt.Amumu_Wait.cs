using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Amumu_WaitClass : AI_Characters 
{
    bool Amumu_Wait()
    {

        return
      // Sequence name :Bot.Amumu.Amumu_Wait
      (
            // Sequence name :DoesNothing
            (
                  GetUnitAISelf(
                        out Self)

            )
     );
      }
}

