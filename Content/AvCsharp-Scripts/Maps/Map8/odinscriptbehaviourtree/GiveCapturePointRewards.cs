using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GiveCapturePointRewardsClass : OdinLayout 
{


     public bool GiveCapturePointRewards(
                TeamId RewardedTeam,
    Vector3 CapturePoint,
      float GoldReward,
    float CaptureRadius
          )
      {
      return
            // Sequence name :Sequence
            (
                  GetTurret(
                        out ReferenceUnit, 
                        TeamId.TEAM_ORDER, 
                        0, 
                        1) &&
                  AddFloat(
                        out RewardRadius, 
                        CaptureRadius, 
                        200) &&
                  GetUnitsInTargetArea(
                        out LocalChampionCollection, 
                        ReferenceUnit, 
                        CapturePoint, 
                        RewardRadius, 
                        AffectEnemies |AffectFriends | AffectHeroes | AlwaysSelf) &&
                  ForEach(LocalChampionCollection, LocalChampion => (
                        // Sequence name :Sequence
                        (
                              GetUnitTeam(
                                    out LocalChampionTeam, 
                                    LocalChampion) &&
                              RewardedTeam == LocalChampionTeam &&
                              GiveChampionGold(
                                    LocalChampion, 
                                    GoldReward)

                        ))
                  )
            );
      }
}

