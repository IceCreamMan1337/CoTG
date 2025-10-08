using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class EvalAbilitiesClass : AI_Characters
{

    public CanCastChampionAbilityClass canCastChampionAbility = new();
    public bool EvalAbilities(
          out float _MembershipValue,
     AttackableUnit Self,
     bool HighOrLow,
     float ValueOfAbility0,
     float ValueOfAbility1,
     float ValueOfAbility2,
     float ValueOfAbility3,
     float MinAbilityScore,
     float MaxAbilityScore,
     bool UseMinMaxValues
        )
    {

        float MembershipValue = default;
        bool result =
                    // Sequence name :Init

                    (SetVarFloat(
                          out MembershipValue,
                          0) &&
                    SetVarFloat(
                          out TotalMembershipValue,
                          0.001f) &&
                          // Sequence name :ReadinessCheck

                          // Sequence name :MaskFailure
                          (
                                // Sequence name :UIltChecker
                                (
                                      GetUnitSpellLevel(
                                            out AbilityLevel,
                                            Self,
                                            SPELLBOOK_CHAMPION,
                                            3) &&
                                      AddFloat(
                                            out TotalMembershipValue,
                                            TotalMembershipValue,
                                            ValueOfAbility3) &&
                                      GreaterFloat(
                                            ValueOfAbility3,
                                            0) &&
                                      // Sequence name :Selector
                                      (
                                            TestUnitSpellToggledOn(
                                                  Self,
                                                  3)
                                            ||
                                            canCastChampionAbility.CanCastChampionAbility(
                                                  Self,
                                                  3,
                                                  default,
                                                  default,
                                                  default,
                                                 default,
                                                  default,
                                                  true,
                                                  false)
                                      ) &&
                                      AddFloat(
                                            out MembershipValue,
                                            MembershipValue,
                                            ValueOfAbility3)
                                )
                                || MaskFailure()
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Ability2
                                (
                                      GetUnitSpellLevel(
                                            out AbilityLevel,
                                            Self,
                                            SPELLBOOK_CHAMPION,
                                            2) &&
                                      AddFloat(
                                            out TotalMembershipValue,
                                            TotalMembershipValue,
                                            ValueOfAbility2) &&
                                      GreaterFloat(
                                            ValueOfAbility2,
                                            0) &&
                                      // Sequence name :Selector
                                      (
                                            TestUnitSpellToggledOn(
                                                  Self,
                                                  2)
                                            ||
                                            canCastChampionAbility.CanCastChampionAbility(
                                                  Self,
                                                  2,
                                                      default,
                                                  default,
                                                  default,
                                                 default,
                                                  default,
                                                  true,
                                                  false)
                                      ) &&
                                      AddFloat(
                                            out MembershipValue,
                                            MembershipValue,
                                            ValueOfAbility2)
                                ) || MaskFailure()
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Ability1
                                (
                                      GetUnitSpellLevel(
                                            out AbilityLevel,
                                            Self,
                                            SPELLBOOK_CHAMPION,
                                            1) &&
                                      AddFloat(
                                            out TotalMembershipValue,
                                            TotalMembershipValue,
                                            ValueOfAbility1) &&
                                      GreaterFloat(
                                            ValueOfAbility1,
                                            0) &&
                                      // Sequence name :ToggleOn_Or_CanCast
                                      (
                                            TestUnitSpellToggledOn(
                                                  Self,
                                                  1)
                                           ||
                                            canCastChampionAbility.CanCastChampionAbility(
                                                  Self,
                                                  1,
                                                    default,
                                                  default,
                                                  default,
                                                 default,
                                                  default,
                                                  true,
                                                  false)
                                      ) &&
                                      AddFloat(
                                            out MembershipValue,
                                            MembershipValue,
                                            ValueOfAbility1)
                                ) || MaskFailure()
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Ability0
                                (
                                      GetUnitSpellLevel(
                                            out AbilityLevel,
                                            Self,
                                            SPELLBOOK_CHAMPION,
                                            0) &&
                                      AddFloat(
                                            out TotalMembershipValue,
                                            TotalMembershipValue,
                                            ValueOfAbility0) &&
                                      GreaterFloat(
                                            ValueOfAbility0,
                                            0) &&
                                      // Sequence name :Selector
                                      (
                                            TestUnitSpellToggledOn(
                                                  Self,
                                                  0
                                                  )
                                           ||
                                           canCastChampionAbility.CanCastChampionAbility(
                                                  Self,
                                                  0,
                                                          default,
                                                  default,
                                                  default,
                                                 default,
                                                  default,
                                                  true,
                                                  false)
                                      ) &&
                                      AddFloat(
                                            out MembershipValue,
                                            MembershipValue,
                                            ValueOfAbility0)
                                ) || MaskFailure()
                          ) &&
                          DivideFloat(
                                out MembershipValue,
                                MembershipValue,
                                TotalMembershipValue) &&
                          MultiplyFloat(
                                out MembershipValue,
                                MembershipValue,
                                0.5f)
                     &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :NonReadinessValue
                          (
                                HighOrLow == false &&
                                SubtractFloat(
                                      out MembershipValue,
                                      0.5f,
                                      MembershipValue)
                          ) || MaskFailure()
                    ) &&
                    GetUnitPARType(
                          out PARType,
                          Self) &&
                    GetUnitPARRatio(
                          out PAR_Ratio,
                          Self,
                          PARType) &&
                    // Sequence name :PAR_Evaluation
                    (
                          // Sequence name :High
                          (
                                HighOrLow == true &&
                                InterpolateLine(
                                      out temp,
                                      0,
                                      0.7f,
                                      0,
                                      1,
                                      0,
                                      1,
                                      PAR_Ratio)
                          ) ||
                          InterpolateLine(
                                out temp,
                                0,
                                0.7f,
                                1,
                                0,
                                0,
                                1,
                                PAR_Ratio)
                    ) &&
                    MultiplyFloat(
                          out temp,
                          temp,
                          0.5f) &&
                    AddFloat(
                          out MembershipValue,
                          MembershipValue,
                          temp) &&
                                // Sequence name :MaskFailure

                                // Sequence name :MinMaxCheckers

                                UseMinMaxValues == true &&
                                MinFloat(
                                      out MembershipValue,
                                      MembershipValue,
                                      MaxAbilityScore) &&
                                MaxFloat(
                                      out MembershipValue,
                                      MembershipValue,
                                      MinAbilityScore))


                     || MaskFailure()
              ;

        _MembershipValue = MembershipValue;
        return result;
    }
}

