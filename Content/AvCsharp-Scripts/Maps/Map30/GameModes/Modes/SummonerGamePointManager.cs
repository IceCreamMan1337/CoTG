using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using BehaviourTrees.Map8;

namespace BehaviourTrees;


 class SummonerGamePointManagerClass : OdinLayout
{

  public bool SummonerGamePointManager()
    {
        return DebugAction("initialization SetGameScore") &&
               SetGameScore(
                   TeamId.TEAM_ORDER,
                   10) &&
               SetGameScore(
                   TeamId.TEAM_CHAOS,
                   10) &&
               DebugAction("finish SetGameScore");
            }
}
