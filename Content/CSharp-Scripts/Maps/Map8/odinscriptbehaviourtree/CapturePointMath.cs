namespace BehaviourTrees;


class CapturePointMathClass : OdinLayout
{


    public bool CapturePointMath(
              out float __CaptureProgress,
     out float __CaptureRate,
   int AttackingPlayers,
   float CaptureProgress,
   TeamId CaptureTeam

        )
    {

        float _CaptureProgress = CaptureProgress;
        float _CaptureRate = default;

        bool result =
                          // Sequence name :Sequence

                          SetVarUnitTeam(
                                out Test,
                                CaptureTeam) &&
                          SubtractInt(
                                out ExtraAttackers,
                                AttackingPlayers,
                                1) &&
                          SetVarFloat(
                                out BaseCaptureContribution,
                                0.85f) &&
                          SetVarFloat(
                                out ExtraCaptureContributionPerPlayer,
                                0.175f) &&
                          SetVarFloat(
                                out _CaptureRate,
                                0) &&
                          // Sequence name :Modify the capture value based on attacking team
                          (
                                // Sequence name :Sequence
                                (
                                      CaptureTeam == TeamId.TEAM_ORDER
                                ) ||
                                // Sequence name :Multiply chaos attacks by -1
                                (
                                      MultiplyFloat(
                                            out BaseCaptureContribution,
                                            BaseCaptureContribution,
                                            -1) &&
                                      MultiplyFloat(
                                            out ExtraCaptureContributionPerPlayer,
                                            ExtraCaptureContributionPerPlayer,
                                            -1)
                                )
                          ) &&
                          AddFloat(
                                out _CaptureRate,
                                _CaptureRate,
                                BaseCaptureContribution) &&
                          MultiplyFloat(
                                out ExtraContribution,
                                ExtraAttackers,
                                ExtraCaptureContributionPerPlayer) &&
                          AddFloat(
                                out _CaptureRate,
                                _CaptureRate,
                                ExtraContribution) &&
                          NormalizeFloatToTickRate(
                                out NormalizedCaptureRate,
                                _CaptureRate) &&
                          AddFloat(
                                out _CaptureProgress,
                                _CaptureProgress,
                                NormalizedCaptureRate) &&
                          AddString(
                                out DebugString,
                                "Capture Progress:",
                                $"{BaseCaptureContribution}") &&
                          AddString(
                                out DebugString,
                                DebugString,
                                 "/") &&
                          AddString(
                                out DebugString,
                                DebugString,
                                $"{ExtraContribution}") &&
                          DebugAction(

                                DebugString) &&
                          MaxFloat(
                                out _CaptureProgress,
                                _CaptureProgress,
                                -100) &&
                          MinFloat(
                                out _CaptureProgress,
                                _CaptureProgress,
                                100)

                    ;
        __CaptureRate = _CaptureRate;
        __CaptureProgress = _CaptureProgress;
        return result;
    }
}

