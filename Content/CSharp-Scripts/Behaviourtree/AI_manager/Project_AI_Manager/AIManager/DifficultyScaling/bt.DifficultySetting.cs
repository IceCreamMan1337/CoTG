namespace BehaviourTrees;


class DifficultySettingClass : AI_DifficultyScaling
{


    public bool DifficultySetting(
        out float DynamicDistributionStartTime,
        out float DynamicDistributionUpdateTime,
        out bool IsDifficultySet,
        int DifficultyIndex,
        bool DifficultySetting_IsWinState,
        float __DynamicDistributionStartTime,
        bool __IsDifficultySet,
        bool OverrideDifficulty)
    {


        float _DynamicDistributionStartTime = __DynamicDistributionStartTime;
        float _DynamicDistributionUpdateTime = default;
        bool _IsDifficultySet = __IsDifficultySet;



        bool result =
                  // Sequence name :MaskFailure

                  // Sequence name :Sequence
                  (
                        __IsDifficultySet == false &&
                        // Sequence name :DifficultySetting
                        (
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
                                                                  30,
                                                                  22)
                                                      ) ||
                                                      // Sequence name :LossState_Harder
                                                      (
                                                            DifficultySetting_IsWinState == false &&
                                                            GenerateRandomFloat(
                                                                  out DynamicDistributionUpdateTime,
                                                                  21,
                                                                  13)
                                                      )
                                                )
                                          ) ||
                                          // Sequence name :WinState_Easier
                                          (
                                                DifficultySetting_IsWinState == true &&
                                                GenerateRandomFloat(
                                                      out DynamicDistributionUpdateTime,
                                                      50,
                                                      40)
                                          ) ||
                                          // Sequence name :LossState_Harder
                                          (
                                                DifficultySetting_IsWinState == false &&
                                                GenerateRandomFloat(
                                                      out DynamicDistributionUpdateTime,
                                                      32,
                                                      28)
                                          )
                                    )
                              ) ||
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
                                          21,
                                          13)
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
                              ||
                               DebugAction("MaskFailure")
                        ) &&
                        SetVarBool(
                              out IsDifficultyInitialized,
                              true) &&
                        SetVarBool(
                              out _IsDifficultySet,
                              true)

                  )
                  ||
                               DebugAction("MaskFailure")
            ;
        DynamicDistributionStartTime = _DynamicDistributionStartTime;
        DynamicDistributionUpdateTime = _DynamicDistributionUpdateTime;
        IsDifficultySet = _IsDifficultySet;
        return result;
    }
}

