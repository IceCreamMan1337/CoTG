using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class SuppressCapturePointClass : AI_Characters
{


    public bool SuppressCapturePoint(
        out string _ActionPerformed,
     AttackableUnit Self,
     float LastUseSpellTime,
     float CurrentGameTime
        )
    {

        string ActionPerformed = "";

        bool result =
                    // Sequence name :Sequence

                    GetUnitAITaskPosition(
                          out TaskPosition) &&
                    GetUnitsInTargetArea(
                          out CapturePoints,
                          Self,
                          TaskPosition,
                          1200,
                          AffectEnemies | AffectMinions | AffectUseable) &&
                    GetCollectionCount(
                          out CapturePointCount,
                          CapturePoints) &&
                    GreaterInt(
                          CapturePointCount,
                          0) &&
                    CountUnitsInTargetArea(
                          out NumNearbyAllies,
                          Self,
                          TaskPosition,
                          400,
                          AffectFriends | AffectHeroes | NotAffectSelf) &&
                    GreaterInt(
                          NumNearbyAllies,
                          0) &&
                    CountUnitsInTargetArea(
                          out NumSuppressingAllies,
                          Self,
                          TaskPosition,
                          400,
                          AffectFriends | AffectHeroes | NotAffectSelf,
                          "OdinCaptureChannel") &&
                    NumSuppressingAllies == 0 &&
                    ForEach(CapturePoints, Point =>
                                // Sequence name :Sequence

                                GetUnitBuffCount(
                                      out Count,
                                      Point,
                                      "OdinGuardianStatsByLevel") &&
                                GreaterInt(
                                      Count,
                                      0)

                    ) &&
                    GetDistanceBetweenUnits(
                          out Distance,
                          Point,
                          Self) &&
                    LessFloat(
                          Distance,
                          500) &&
                    GetUnitBuffCount(
                          out Count,
                          Point,
                          "OdinGuardianSuppression") &&
                    // Sequence name :SuppressPointIfNotAlreadyChanneling
                    (
                          // Sequence name :AlreadySuppressingPoint
                          (
                                TestUnitIsChanneling(
                                      Self,
                                      true) &&
                                TestUnitHasBuff(
                                      Self,
                                      default,
                                      "OdinCaptureChannel",
                                      true)
                          ) ||
                          // Sequence name :SuppressPoint
                          (
                                LessEqualInt(
                                      Count,
                                      0) &&
                                SubtractFloat(
                                      out TimePassed,
                                      CurrentGameTime,
                                      LastUseSpellTime) &&
                                GreaterEqualFloat(
                                      TimePassed,
                                      4) &&
                                TestCanUseObject(
                                      Self,
                                      Point) &&
                                IssueUseObjectOrder(
                                      Point)
                          )
                    ) &&
                    SetVarString(
                          out ActionPerformed,
                          "SuppressCapturePoint")

              ;

        _ActionPerformed = ActionPerformed;
        return result;
    }
}

