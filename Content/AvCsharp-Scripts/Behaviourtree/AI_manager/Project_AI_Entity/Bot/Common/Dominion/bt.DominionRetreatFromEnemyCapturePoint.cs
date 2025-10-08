using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DominionRetreatFromEnemyCapturePointClass : AI_Characters 
{

    private DominionRetreatClass dominionRetreat = new DominionRetreatClass();
     public bool DominionRetreatFromEnemyCapturePoint(
             out float __RetreatFromCP_RetreatUntil,
      out string _ActionPerformed,
      out Vector3 __RetreatSafePosition,
      out float __RetreatPositionStartTime,
      AttackableUnit Self,
      Vector3 SelfPosition,
      float RetreatFromCP_RetreatUntil,
      Vector3 RetreatSafePosition,
      float RetreatPositionStartTime
         )
    {

        float _RetreatFromCP_RetreatUntil = RetreatFromCP_RetreatUntil;
        string ActionPerformed = default;
        Vector3 _RetreatSafePosition = RetreatSafePosition;
       float _RetreatPositionStartTime = RetreatPositionStartTime;
        bool result =
              // Sequence name :Sequence
              (
                    // Sequence name :NotAlreadyCapturingPoint
                    (
                          TestUnitHasBuff(
                                Self,
                               default,
                                "OdinCaptureChannel",
                                false)
                          ||
                          TestUnitIsChanneling(
                                Self,
                                false)
                    ) &&
                    GetGameTime(
                          out CurrentTime) &&
                    SubtractFloat(
                          out RetreatTimeRemaining,
                          RetreatFromCP_RetreatUntil,
                          CurrentTime) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :CheckIfWeShouldRetreat
                          (
                                LessEqualFloat(
                                      RetreatTimeRemaining,
                                      0) &&
                                GetUnitsInTargetArea(
                                      out CapturePoints,
                                      Self,
                                      SelfPosition,
                                      650,
                                      AffectEnemies | AffectMinions | AffectUseable) &&
                                ForEach(CapturePoints, CapturePoint => (
                                      // Sequence name :Sequence
                                      (
                                            GetUnitBuffCount(
                                                  out BuffCount,
                                                  CapturePoint,
                                                  "OdinGuardianStatsByLevel") &&
                                            GreaterInt(
                                                  BuffCount,
                                                  0)
                                      ))
                                ) &&
                                GenerateRandomFloat(
                                      out TimeToAdd,
                                      1,
                                      3) &&
                                AddFloat(
                                      out _RetreatFromCP_RetreatUntil,
                                      CurrentTime,
                                      TimeToAdd) &&
                                SetVarFloat(
                                      out RetreatTimeRemaining,
                                      TimeToAdd)
                          )
                    ) &&
                    // Sequence name :SillRetreating
                    (
                          GreaterFloat(
                                RetreatTimeRemaining,
                                0) &&
                       dominionRetreat. DominionRetreat(
                                out _RetreatSafePosition,
                                out _RetreatPositionStartTime,
                                Self,
                                RetreatSafePosition,
                                RetreatPositionStartTime,
                                SelfPosition)
                    ) &&
                    SetVarString(
                          out ActionPerformed,
                          "RetreatFromEnemyCapturePoint")

              );

         __RetreatFromCP_RetreatUntil = _RetreatFromCP_RetreatUntil;
         _ActionPerformed = ActionPerformed;
         __RetreatSafePosition = _RetreatSafePosition;
         __RetreatPositionStartTime = _RetreatPositionStartTime;
        return result;
    }
}

