/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class WinLossState : BehaviourTree 
{
      out bool DifficultyScaling_IsWinState;
      out bool IsDifficultySet;
      TeamEnum MyTeam;
      bool DifficultyScaling_IsWinState;
      bool IsDifficultySet;

      bool WinLossState()
      {
      return
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        SetVarBool(
                              out PrevWinState, 
                              DifficultyScaling_IsWinState) &&
                        SetVarInt(
                              out NumKillsChaos, 
                              0) &&
                        SetVarInt(
                              out NumKillsOrder, 
                              0) &&
                        GetChampionCollection(
                              out AllEntities, 
                              out AllEntities) &&
                        AllEntities.ForEach( Entity => (                              // Sequence name :Sequence
                              (
                                    GetUnitTeam(
                                          out EntityTeam, 
                                          Entity) &&
                                    GetChampionKills(
                                          out Kills, 
                                          Entity) &&
                                    // Sequence name :Selector
                                    (
                                          // Sequence name :Order
                                          (
                                                EntityTeam == TeamId.TEAM_BLUE &&
                                                AddInt(
                                                      out NumKillsOrder, 
                                                      NumKillsOrder, 
                                                      Kills)
                                          ) ||
                                          // Sequence name :Chaos
                                          (
                                                EntityTeam == TeamId.TEAM_PURPLE &&
                                                AddInt(
                                                      out NumKillsChaos, 
                                                      NumKillsChaos, 
                                                      Kills)
                                          )
                                    )
                              )
                        ) &&
                        // Sequence name :CalculateDifference
                        (
                              // Sequence name :MyTeam==ORDER
                              (
                                    MyTeam == TeamId.TEAM_BLUE &&
                                    SubtractInt(
                                          out KillsDiff, 
                                          NumKillsOrder, 
                                          NumKillsChaos)
                              ) ||
                              // Sequence name :MyTeam==CHAOS
                              (
                                    MyTeam == TeamId.TEAM_PURPLE &&
                                    SubtractInt(
                                          out KillsDiff, 
                                          NumKillsChaos, 
                                          NumKillsOrder)
                              )
                        ) &&
                        // Sequence name :SwitchStates
                        (
                              // Sequence name :Win_To_Lose
                              (
                                    DifficultyScaling_IsWinState == true &&
                                    LessEqualInt(
                                          KillsDiff, 
                                          -3) &&
                                    SetVarBool(
                                          out DifficultyScaling_IsWinState, 
                                          False)
                              ) ||
                              // Sequence name :Win_To_Lose
                              (
                                    DifficultyScaling_IsWinState == False &&
                                    GreaterEqualInt(
                                          KillsDiff, 
                                          3) &&
                                    SetVarBool(
                                          out DifficultyScaling_IsWinState, 
                                          true)
                              )
                        ) &&
                        NotEqualBool(
                              DifficultyScaling_IsWinState, 
                              PrevWinState) &&
                        SetVarBool(
                              out IsDifficultySet, 
                              False)

                  )
            );
      }
}

*/