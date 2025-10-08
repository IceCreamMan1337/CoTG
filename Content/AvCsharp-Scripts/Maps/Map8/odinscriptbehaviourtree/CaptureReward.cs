using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class CaptureRewardClass : OdinLayout 
{


     public bool CaptureReward(
                float CaptureRadius,
    TeamId CapturingTeam,
    Vector3 CaptureCenter,
      AttackableUnit CapturingTeamTurret
          )
      {
      return
            // Sequence name :Sequence
            (
                  GetChampionCollection(
                        out ChampionCollection) &&
                  GetUnitsInTargetArea(
                        out LocalChampionCollection, 
                        CapturingTeamTurret, 
                        CaptureCenter, 
                        CaptureRadius, 
                        AffectFriends | AffectHeroes) &&
                  GetCollectionCount(
                        out intneverused, 
                        LocalChampionCollection)

            );
      }
}

