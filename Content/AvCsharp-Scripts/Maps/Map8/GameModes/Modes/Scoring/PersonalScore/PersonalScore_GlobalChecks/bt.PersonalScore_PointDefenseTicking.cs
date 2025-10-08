using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_PointDefenseTickingClass : OdinLayout 
{
    float lastTimeExecuted_EP_PersonalScore_PointDefenseTicking;
      public bool PersonalScore_PointDefenseTicking()
    {
        var for_EP_PersonalScore_PointDefenseTicking = new PersonalScore_PointDefenseTickingClass();

        List<Func<bool>> EP_PersonalScore_PointDefenseTicking = new List<Func<bool>> { () => 
        { return for_EP_PersonalScore_PointDefenseTicking.For_EP_PersonalScore_PointDefenseTicking(); } };


        return (
            ExecutePeriodically(
                ref lastTimeExecuted_EP_PersonalScore_PointDefenseTicking,
                EP_PersonalScore_PointDefenseTicking,
                        true,
                        8));
    }

    public bool For_EP_PersonalScore_PointDefenseTicking()
    {
        var findClosestCapturePointByPosition = new FindClosestCapturePointByPositionClass();
        var getGuardian = new GetGuardianClass();
        return
    
            // Sequence name :Sequence
            (
                  
                        // Sequence name :GivePersonalDefenderRewards
                        (
                              GetChampionCollection(
                                    out Champions_) &&
                              ForEach(Champions_ , Champ => (
                                    // Sequence name :Sequence
                                    (
                                          GetUnitTeam(
                                                out ChampTeam, 
                                                Champ) &&
                                          TestUnitCondition(
                                                Champ, 
                                                true) &&
                                          // Sequence name :MaskFailure
                                          (
                                                // Sequence name :Sequence
                                                (
                                                      findClosestCapturePointByPosition.FindClosestCapturePointByPosition(
                                                            out ClosestCapturePoint, 
                                                            Champ) &&
                                                      getGuardian.GetGuardian(
                                                            out Guardian, 
                                                            ClosestCapturePoint) &&
                                                      GetDistanceBetweenUnits(
                                                            out Distance, 
                                                            Guardian, 
                                                            Champ) &&
                                                      LessEqualFloat(
                                                            Distance, 
                                                            900) &&
                                                      GetUnitTeam(
                                                            out GuardianTeam, 
                                                            Guardian) &&
                                                      GuardianTeam == ChampTeam &&
                                                      IncrementPlayerScore(
                                                            Champ, 
                                                            ScoreCategory.Objective, 
                                                          ScoreEvent.Defender,
                                                            5
                                                            )

                                                )
                                                ||
                               DebugAction("MaskFailure")
                                          )
                                    ))
                              )
                        )
                  
            );
      }
}

