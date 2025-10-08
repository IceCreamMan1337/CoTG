using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class ObeliskCapturePointGoldRewardsClass : OdinLayout 
{


    public bool ObeliskCapturePointGoldRewards(TeamId TeamToGiveGold, Vector3 ReferencePosition)

    {
      return
            // Sequence name :MaskFailure
            (
                  // Sequence name :GiveGoldRewards
                  (
                        // Sequence name :Order_Chaos_Turret
                        (
                              // Sequence name :Order_Turret
                              (
                                    TeamToGiveGold == TeamId.TEAM_ORDER &&
                                    GetTurret(
                                          out ShrineTurret, 
                                          TeamId.TEAM_ORDER, 
                                          0, 
                                          1)
                              ) ||
                              GetTurret(
                                    out ShrineTurret, 
                                    TeamId.TEAM_CHAOS, 
                                    0, 
                                    1)
                        ) &&
                        GetUnitsInTargetArea(
                              out LocalChampionCollection, 
                              ShrineTurret, 
                              ReferencePosition, 
                              1200, 
                              AffectFriends | AffectHeroes) &&
                        GetCollectionCount(
                              out LocalChampCount, 
                              LocalChampionCollection) &&
                        GreaterInt(
                              LocalChampCount, 
                              0) &&
                        SubtractInt(
                              out ExtraChampCount, 
                              LocalChampCount, 
                              1) &&
                        MultiplyInt(
                              out ExtraGold, 
                              ExtraChampCount, 
                              50) &&
                        AddInt(
                              out TotalGold, 
                              100, 
                              ExtraGold) &&
                        DivideFloat(
                              out IndividualGold, 
                              TotalGold, 
                              LocalChampCount) &&
                        ForEach(LocalChampionCollection, LocalChamp => (
                              // Sequence name :Sequence
                              (
                                    GetUnitTeam(
                                          out UnitTeam, 
                                          LocalChamp) &&
                                    UnitTeam == TeamToGiveGold &&
                                    GiveChampionGold(
                                          LocalChamp, 
                                          IndividualGold)

                              ))
                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            );
      }
}

