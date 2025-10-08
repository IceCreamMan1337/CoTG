using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class AutoAttackClass : AI_Characters
{


    public bool AutoAttack(
          out bool _IssuedAttack,
     out AttackableUnit _IssuedAttackTarget,
     AttackableUnit Target,
     AttackableUnit Self,
     bool PrevIssuedAttack,
     AttackableUnit PrevIssuedAttackTarget
        )
    {
        bool IssuedAttack = default ;
        AttackableUnit IssuedAttackTarget = default;


        bool result =
               // Sequence name :AutoAttack
               (
                 
                     GetUnitAttackRange(
                           out AttackRange,
                           Self) &&

                               DebugAction("AttackRange" + AttackRange) &&
                     GetDistanceBetweenUnits(
                           out Distance,
                           Target,
                           Self) &&

                             DebugAction("Distance" + Distance) &&
                     // Sequence name :AutoAttack
                     (
                           // Sequence name :Minion_IsInFront_MicroRetreat
                           (
                                 GetUnitType(
                                       out TargetType,
                                       Target) &&
                                 TargetType == MINION_UNIT &&
                                 GetDistanceBetweenUnits(
                                       out Distance,
                                       Target,
                                       Self) &&
                                 // Sequence name :EitherTooCloseOrInFront
                                 (
                                       // Sequence name :I_Am_In_Front_Of_Minion
                                       (
                                             IsTargetInFrontfunc(
                                                   out IsTargetInFront,
                                                   Self,
                                                   Target) &&
                                             IsTargetInFront == false
                                       ) ||
                                       // Sequence name :TooCloseToTarget
                                       (
                                             MultiplyFloat(
                                                   out ThresholdRange,
                                                   AttackRange,
                                                   0.4f) &&
                                             LessEqualFloat(
                                                   Distance,
                                                   ThresholdRange)
                                       )
                                 ) &&
                                 ComputeUnitAISafePosition(
                                       600,
                                       false,
                                       false) &&
                                 GetUnitAISafePosition(
                                       out SafePosition) &&
                                 IssueMoveToPositionOrder(
                                       SafePosition) &&
                                 ClearUnitAISafePosition()
                           ) ||
                           // Sequence name :AutoAttack
                           (
                                 SetVarBool(
                                       out IssuedAttack,
                                       PrevIssuedAttack) &&
                                 SetVarAttackableUnit(
                                       out IssuedAttackTarget,
                                       PrevIssuedAttackTarget) &&
                                 // Sequence name :Sequence
                                 (
                                       SetUnitAIAttackTarget(
                                             Target) &&
                                       IssueChaseOrder()

                                 )
                           )
                     )
               );

        _IssuedAttack = IssuedAttack;
        _IssuedAttackTarget = IssuedAttackTarget;
        return result;

    }

}

