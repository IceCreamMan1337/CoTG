using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class CapturePointQuestClass : OdinLayout 
{


     public bool CapturePointQuest(
                TeamId DefensiveTeam,
    TeamId OffensiveTeam,
    float OffensiveVP,
    float DefensiveVP)
      {
        return true;

      }
}

