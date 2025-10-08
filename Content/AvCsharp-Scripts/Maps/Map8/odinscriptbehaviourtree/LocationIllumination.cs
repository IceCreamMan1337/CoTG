using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class LocationIlluminationClass : OdinLayout 
{


     public bool LocationIllumination(
               Vector3 Location
         )
      {
      return
            // Sequence name :Sequence
            (
                  AddPositionPerceptionBubble(
                        out BubbleID, 
                        Location, 
                        600, 
                        2000, 
                        TeamId.TEAM_ORDER, 
                        false, 
                        null, 
                        null) &&
                  AddPositionPerceptionBubble(
                        out BubbleID, 
                        Location, 
                        600, 
                        2000, 
                        TeamId.TEAM_CHAOS, 
                        false,
                        null,
                        null)

            );
      }
}

