namespace BehaviourTrees;


class WinLossStateClass : AI_DifficultyScaling
{


    public bool WinLossState(
      out bool DifficultyScaling_IsWinState,
      out bool IsDifficultySet,
        TeamId MyTeam,
        bool __DifficultyScaling_IsWinState,
        bool __IsDifficultySet)
    {



        bool _DifficultyScaling_IsWinState = __DifficultyScaling_IsWinState;
        bool _IsDifficultySet = __IsDifficultySet;



        bool result =
                  // Sequence name :MaskFailure

                  // Sequence name :Sequence
                  (
                        SetVarBool(
                              out PrevWinState,
                              __DifficultyScaling_IsWinState) &&
                        SetVarInt(
                              out NumKillsChaos,
                              0) &&
                        SetVarInt(
                              out NumKillsOrder,
                              0) &&
                        GetChampionCollection(
                              out AllEntities) &&
                        ForEach(AllEntities, Entity =>
                                    // Sequence name :Sequence

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
                                                EntityTeam == TeamId.TEAM_ORDER &&
                                                AddInt(
                                                      out NumKillsOrder,
                                                      NumKillsOrder,
                                                      Kills)
                                          ) ||
                                          // Sequence name :Chaos
                                          (
                                                EntityTeam == TeamId.TEAM_CHAOS &&
                                                AddInt(
                                                      out NumKillsChaos,
                                                      NumKillsChaos,
                                                      Kills)
                                          )
                                    )

                        ) &&
                        // Sequence name :CalculateDifference
                        (
                              // Sequence name :MyTeam==ORDER
                              (
                                    MyTeam == TeamId.TEAM_ORDER &&
                                    SubtractInt(
                                          out KillsDiff,
                                          NumKillsOrder,
                                          NumKillsChaos)
                              ) ||
                              // Sequence name :MyTeam==CHAOS
                              (
                                    MyTeam == TeamId.TEAM_CHAOS &&
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
                                    __DifficultyScaling_IsWinState == true &&
                                    LessEqualInt(
                                          KillsDiff,
                                          -3) &&
                                    SetVarBool(
                                          out DifficultyScaling_IsWinState,
                                          false)
                              ) ||
                              // Sequence name :Win_To_Lose
                              (
                                    __DifficultyScaling_IsWinState == false &&
                                    GreaterEqualInt(
                                          KillsDiff,
                                          3) &&
                                    SetVarBool(
                                          out DifficultyScaling_IsWinState,
                                          true)
                              )
                        ) &&
                        NotEqualBool(
                              __DifficultyScaling_IsWinState,
                              PrevWinState) &&
                        SetVarBool(
                              out _IsDifficultySet,
                              false)

                  )
                  ||
                               DebugAction("MaskFailure")
            ;



        IsDifficultySet = _IsDifficultySet;
        DifficultyScaling_IsWinState = _DifficultyScaling_IsWinState;

        return result;
    }
}

