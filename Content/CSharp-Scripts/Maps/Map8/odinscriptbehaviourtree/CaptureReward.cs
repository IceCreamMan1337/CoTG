using static CoTGEnumNetwork.Enums.SpellDataFlags;

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

              ;
    }
}

