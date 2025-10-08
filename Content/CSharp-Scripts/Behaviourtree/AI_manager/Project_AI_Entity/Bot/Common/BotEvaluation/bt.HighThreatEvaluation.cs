namespace BehaviourTrees.all;


class HighThreatEvaluationClass : AI_Characters
{


    public bool HighThreatEvaluation(
              out bool _IsHighBurst,
     out bool _IsLowHP,
     AttackableUnit Self,
     float DamageTakenOverTime,
     float DamageRatioThreshold,
     float LowHealthPercentThreshold
        )
    {
        bool IsHighBurst = default;
        bool IsLowHP = default;

        bool result =
                    // Sequence name :RetreatHighThreat

                    (SetVarBool(
                          out IsHighBurst,
                          false) &&
                    SetVarBool(
                          out IsLowHP,
                          false) &&
                    GetUnitCurrentHealth(
                          out CurrentHealth,
                          Self) &&
                    GetUnitHealthRatio(
                          out HealthRatio,
                          Self) &&
                    // Sequence name :Selector
                    (
                                // Sequence name :CurrentHealth&gt,0

                                GreaterFloat(
                                      CurrentHealth,
                                      0)
                           ||
                                // Sequence name :SetCurrentHealth=1

                                SetVarFloat(
                                      out CurrentHealth,
                                      1)

                    ) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :HighBurstEvaluation
                          (
                                DivideFloat(
                                      out Damage_Ratio,
                                      DamageTakenOverTime,
                                      CurrentHealth) &&
                                GreaterFloat(
                                      Damage_Ratio,
                                      DamageRatioThreshold) &&
                                SetVarBool(
                                      out IsHighBurst,
                                      true)
                          )
                          || MaskFailure()
                    ) &&
                                // Sequence name :MaskFailure

                                // Sequence name :LowHealthEvaluation

                                LessEqualFloat(
                                      HealthRatio,
                                      LowHealthPercentThreshold) &&
                                SetVarBool(
                                      out IsLowHP,
                                      true))


                     || MaskFailure()
              ;
        _IsHighBurst = IsHighBurst;
        _IsLowHP = IsLowHP;
        return result;
    }
}

