using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class DominionRetreatClass : AI_Characters
{


    public bool DominionRetreat(
        out Vector3 __RetreatSafePosition,
     out float __RetreatPositionStartTime,
     AttackableUnit Self,
     Vector3 RetreatSafePosition,
     float RetreatPositionStartTime,
     Vector3 SelfPosition
        )
    {
        var _RetreatSafePosition = RetreatSafePosition;
        var _RetreatPositionStartTime = RetreatPositionStartTime;
        bool result =
                   // Sequence name :Sequence

                   GetGameTime(
                         out CurrentGameTime) &&
                   SubtractFloat(
                         out TimeDiff,
                         CurrentGameTime,
                         RetreatPositionStartTime) &&
                         // Sequence name :MaskFailure

                         // Sequence name :ValidSafePointOrFindSafePoint
                         (
                               (LessFloat(
                                     TimeDiff,
                                     3.5f) &&
                                     // Sequence name :ClosestCapturePoint

                                     GetUnitsInTargetArea(
                                           out CapturePoints,
                                           Self,
                                           SelfPosition,
                                           15000,
                                           AffectFriends | AffectMinions | AffectUseable) &&
                                     SetVarFloat(
                                           out ClosestDistance,
                                           20000) &&
                                     ForEach(CapturePoints, CapturePoint =>
                                                 // Sequence name :Sequence

                                                 GetUnitBuffCount(
                                                       out Count,
                                                       CapturePoint,
                                                       "OdinGuardianStatsByLevel") &&
                                                 GreaterInt(
                                                       Count,
                                                       0) &&
                                                 GetDistanceBetweenUnits(
                                                       out CurrentDistance,
                                                       Self,
                                                       CapturePoint) &&
                                                 LessFloat(
                                                       CurrentDistance,
                                                       ClosestDistance) &&
                                                 SetVarAttackableUnit(
                                                       out ClosestCapturePoint,
                                                       CapturePoint) &&
                                                 SetVarFloat(
                                                       out ClosestDistance,
                                                       CurrentDistance)

                                     ) &&
                                     LessFloat(
                                           ClosestDistance,
                                           15000) &&
                                     ComputeUnitAISpellPosition(
                                           ClosestCapturePoint,
                                           Self,
                                           450,
                                           false) &&
                                     GetUnitAISpellPosition(
                                           out _RetreatSafePosition) &&
                                     SetVarFloat(
                                           out _RetreatPositionStartTime,
                                           CurrentGameTime) &&
                                     ClearUnitAISpellPosition())
                                ||
                               // Sequence name :ToGoBase
                               (
                                     GetUnitAIBasePosition(
                                           out _RetreatSafePosition,
                                           Self) &&
                                     SetVarFloat(
                                           out _RetreatPositionStartTime,
                                           CurrentGameTime)
                               )
                         )
                    &&
                   IssueMoveToPositionOrder(
                         RetreatSafePosition)

             ;
        __RetreatSafePosition = _RetreatSafePosition;
        __RetreatPositionStartTime = _RetreatPositionStartTime;
        return result;

    }
}

