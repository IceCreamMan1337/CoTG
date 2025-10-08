namespace BehaviourTrees.all;


class DominionLowThreatClass : AI_Characters
{
    private DominionMicroRetreatClass dominionMicroRetreat = new();

    public bool DominionLowThreat(
          out bool __LowThreatMode,
     out bool __TargetValid,
     out string _ActionPerformed,
     float StrengthRatioOverTime,
     bool LowThreatMode,
     AttackableUnit Self,
     bool TargetValid
        )
    {
        var _LowThreatMode = LowThreatMode;
        var _TargetValid = TargetValid;
        string ActionPerformed = default;

        bool result =
                  // Sequence name :LowThreat

                  TestUnitHasBuff(
                        Self,
                        default,
                        "OdinCaptureChannel",
                        false) &&
                  // Sequence name :EvalSuccessOrClearNode
                  (
                        // Sequence name :InitialLowThreat
                        (
                              GreaterFloat(
                                    StrengthRatioOverTime,
                                    8.5f) &&
                              ClearUnitAIAttackTarget() &&
                              SetVarBool(
                                    out LowThreatMode,
                                    true)
                        ) ||
                        // Sequence name :LowThreatModeEval
                        (
                              LowThreatMode == true &&
                              SetVarBool(
                                    out LowThreatMode,
                                    false) &&
                              GreaterFloat(
                                    StrengthRatioOverTime,
                                    4) &&
                              ClearUnitAIAttackTarget() &&
                              SetVarBool(
                                    out LowThreatMode,
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
                        out TargetValid,
                        false) &&
                  dominionMicroRetreat.DominionMicroRetreat() &&
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

