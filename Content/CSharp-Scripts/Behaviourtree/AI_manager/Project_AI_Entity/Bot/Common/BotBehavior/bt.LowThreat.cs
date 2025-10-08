namespace BehaviourTrees.all;


class LowThreatClass : AI_Characters
{

    private MicroRetreatClass microRetreat = new();
    public bool LowThreat(
            out bool __LowThreatMode,
     out bool __TargetValid,
     out string _ActionPerformed,
     float StrengthRatioOverTime,
     bool LowThreatMode,
     AttackableUnit Self,
     bool TargetValid
        )
    {

        bool _LowThreatMode = LowThreatMode;
        bool _TargetValid = TargetValid;
        string ActionPerformed = default;

        bool result =
                        // Sequence name :LowThreat

                        // Sequence name :EvalSuccessOrClearNode
                        (
                              // Sequence name :InitialLowThreat
                              (
                                    GreaterFloat(
                                          StrengthRatioOverTime,
                                          8.5f) &&
                                    ClearUnitAIAttackTarget() &&
                                    SetVarBool(
                                          out _LowThreatMode,
                                          true)
                              ) ||
                              // Sequence name :LowThreatModeEval
                              (
                                    LowThreatMode == true &&
                                    SetVarBool(
                                          out _LowThreatMode,
                                          false) &&
                                    GreaterFloat(
                                          StrengthRatioOverTime,
                                          5.5f) &&
                                    ClearUnitAIAttackTarget() &&
                                    SetVarBool(
                                          out _LowThreatMode,
                                          true)
                              ) ||


                              // Sequence name :ClearSafePosition
                              (
                                    ClearUnitAISafePosition() &&
                                    SetVarBool(
                                          out Run,
                                          false) &&
                                    Run == true
                              )
                        ) &&
                        SetVarBool(
                              out _TargetValid,
                              false) &&
                     microRetreat.MicroRetreat(
                              Self) &&
                        SetVarString(
                              out ActionPerformed,
                              "LowThreat")

                  ;
        __LowThreatMode = _LowThreatMode;
        __TargetValid = _TargetValid;
        _ActionPerformed = ActionPerformed;
        return result;
    }
}

