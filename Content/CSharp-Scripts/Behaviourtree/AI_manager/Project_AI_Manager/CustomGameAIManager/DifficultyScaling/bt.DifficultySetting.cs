/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class DifficultySetting : BehaviourTree 
{
      out float DynamicDistributionStartTime;
      out float DynamicDistributionUpdateTime;
      out bool IsDifficultySet;
      int DifficultyIndex;
      bool DifficultySetting_IsWinState;
      float DynamicDistributionStartTime;
      bool IsDifficultySet;
      bool OverrideDifficulty;

      bool DifficultySetting()
      {
      return
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        IsDifficultySet == False &&
                        // Sequence name :DifficultySetting
                        (
                              // Sequence name :NotEasyMode
                              (
                                    GreaterInt(
                                          DifficultyIndex, 
                                          0) &&
                                    SetVarFloat(
                                          out DynamicDistributionStartTime, 
                                          540) &&
                                    GenerateRandomFloat(
                                          out DynamicDistributionUpdateTime, 
                                          Value=, 
                                          Value=)
                              ) ||
                              // Sequence name :EasyMode
                              (
                                    DifficultyIndex == 0 &&
                                    SetVarFloat(
                                          out DynamicDistributionStartTime, 
                                          1200) &&
                                    // Sequence name :WinLossState
                                    (
                                          // Sequence name :OverridenDifficulty
                                          (
                                                OverrideDifficulty == true &&
                                                // Sequence name :Selector
                                                (
                                                      // Sequence name :WinState_Easier
                                                      (
                                                            DifficultySetting_IsWinState == true &&
                                                            GenerateRandomFloat(
                                                                  out DynamicDistributionUpdateTime, 
                                                                  Value=, 
                                                                  Value=)
                                                      ) ||
                                                      // Sequence name :LossState_Harder
                                                      (
                                                            DifficultySetting_IsWinState == False &&
                                                            GenerateRandomFloat(
                                                                  out DynamicDistributionUpdateTime, 
                                                                  Value=, 
                                                                  Value=)
                                                      )
                                                )
                                          ) ||
                                          // Sequence name :WinState_Easier
                                          (
                                                DifficultySetting_IsWinState == true &&
                                                GenerateRandomFloat(
                                                      out DynamicDistributionUpdateTime, 
                                                      Value=, 
                                                      Value=)
                                          ) ||
                                          // Sequence name :LossState_Harder
                                          (
                                                DifficultySetting_IsWinState == False &&
                                                GenerateRandomFloat(
                                                      out DynamicDistributionUpdateTime, 
                                                      Value=, 
                                                      Value=)
                                          )
                                    )
                              )
                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    OverrideDifficulty == true &&
                                    SetVarFloat(
                                          out DynamicDistributionStartTime, 
                                          0)
                              )
                        ) &&
                        SetVarBool(
                              out IsDifficultyInitialized, 
                              true) &&
                        SetVarBool(
                              out IsDifficultySet, 
                              true)

                  )
            );
      }
}

*/