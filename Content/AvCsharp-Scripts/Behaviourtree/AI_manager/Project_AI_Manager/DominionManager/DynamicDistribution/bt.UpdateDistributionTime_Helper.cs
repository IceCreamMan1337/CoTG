using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class UpdateDistributionTime_HelperClass : bt_OdinManager 
{


     public bool UpdateDistributionTime_Helper(
                out bool IsBeingCaptured,
      out float NextUpdateTime,
    TeamId ReferenceTeam,
    AttackableUnit CapturePoint,
    bool __IsBeingCaptured,
    float __NextUpdateTime)
      {
        bool _IsBeingCaptured = __IsBeingCaptured;
        float _NextUpdateTime = __NextUpdateTime;
            // Sequence name :MaskFailure

        bool result =
            (
                  // Sequence name :Sequence
                  (
                        GetUnitBuffCount(
                              out Count, 
                              CapturePoint,
                              "OdinGuardianSuppression") &&
                        // Sequence name :Selector
                        (
                              // Sequence name :NotBeingCaptured
                              (
                                    Count == 0 &&
                                    SetVarBool(
                                          out _IsBeingCaptured, 
                                          false)
                              ) ||
                              // Sequence name :BeingCapturedAndPreviouslyWasNot
                              (
                                    __IsBeingCaptured == false &&
                                    GreaterInt(
                                          Count, 
                                          0) &&
                                    SetVarBool(
                                          out _IsBeingCaptured, 
                                          true) &&
                                    GetUnitTeam(
                                          out CapturePointTeam, 
                                          CapturePoint) &&
                                    // Sequence name :Selector
                                    (
                                          CapturePointTeam == ReferenceTeam    ||
                                          CapturePointTeam == TeamId.TEAM_NEUTRAL
                                    ) &&
                                    SetVarFloat(
                                          out _NextUpdateTime, 
                                          0)

                              )
                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            );
        IsBeingCaptured = _IsBeingCaptured;
        NextUpdateTime = _NextUpdateTime;

        return result;


      }
}

