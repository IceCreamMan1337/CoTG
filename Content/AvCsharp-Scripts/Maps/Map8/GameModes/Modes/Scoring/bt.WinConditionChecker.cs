using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class WinConditionCheckerClass : OdinLayout 
{


     public bool WinConditionChecker(
     out TeamId __FinalWinTeam,
    float RemainingOrderScore,
    float RemainingChaosScore,
    TeamId FinalWinTeam
          )
      {

        TeamId _FinalWinTeam = FinalWinTeam;
        bool result = 
            // Sequence name :MaskFailure
            (
                  // Sequence name :End_Game_Checker
                  (
                        // Sequence name :Selector
                        (
                              NotEqualUnitTeam(
                                    FinalWinTeam, 
                                    TeamId.TEAM_NEUTRAL)        ||
                                    // Sequence name :Order_Loses
                              (
                                    LessFloat(
                                          RemainingOrderScore, 
                                          1) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                GetChampionCollection(
                                                      out AllChampions) &&
                                                ForEach(AllChampions , Champ => (
                                                      // Sequence name :Sequence
                                                      (
                                                            GetUnitTeam(
                                                                  out ChampTeam, 
                                                                  Champ) &&
                                                            ChampTeam == TeamId.TEAM_CHAOS
                                                      ))
                                                )
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    ) &&
                                    SetVarUnitTeam(
                                          out FinalWinTeam, 
                                          TeamId.TEAM_CHAOS)
                              ) ||
                              // Sequence name :Chaos_Loses
                              (
                                    LessFloat(
                                          RemainingChaosScore, 
                                          1) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                GetChampionCollection(
                                                      out AllChampions) &&
                                                ForEach(AllChampions, Champ => (
                                                      // Sequence name :Sequence
                                                      (
                                                            GetUnitTeam(
                                                                  out ChampTeam, 
                                                                  Champ) &&
                                                            ChampTeam == TeamId.TEAM_ORDER
                                                      ))
                                                )
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    ) &&
                                    SetVarUnitTeam(
                                          out FinalWinTeam, 
                                          TeamId.TEAM_ORDER)
                              ) ||
                              // Sequence name :OrderSurrender
                              (
                                    TestTeamSurrendered(
                                          true, 
                                          TeamId.TEAM_ORDER) &&
                                    SetVarUnitTeam(
                                          out FinalWinTeam, 
                                          TeamId.TEAM_CHAOS)
                              ) ||
                              // Sequence name :ChaosSurrender
                              (
                                    TestTeamSurrendered(
                                          true, 
                                          TeamId.TEAM_CHAOS) &&
                                    SetVarUnitTeam(
                                          out FinalWinTeam, 
                                          TeamId.TEAM_ORDER)
                              )
                        ) &&
                        // Sequence name :Selector
                        (
                              // Sequence name :Sequence
                              (
                                    FinalWinTeam == TeamId.TEAM_ORDER &&
                                    SetBTInstanceStatus(
                                          true,
                                          "EndOfGameCeremony_OrderWon") &&
                                    SetBTInstanceStatus(
                                          false,
                                          "CapturePointManager")
                              ) ||
                              // Sequence name :Sequence
                              (
                                    FinalWinTeam == TeamId.TEAM_CHAOS &&
                                    SetBTInstanceStatus(
                                          true,
                                          "EndOfGameCeremony_ChaosWon") &&
                                    SetBTInstanceStatus(
                                          false,
                                          "CapturePointManager")

                              )
                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            );


        __FinalWinTeam = _FinalWinTeam;
        return result;
      }
}

