using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Alistar_WaitClass : AI_Characters 
{

    bool Alistar_Wait()
    {

        return
        // Sequence name :Bot.Alistar.Alistar_Wait
        (
              // Sequence name :DoesNothing
              (
                    GetUnitAISelf(
                          out Self)

              )
         );
      }
}

