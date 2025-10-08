using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class MinionAutoAttackTargetClass : AI_Characters 
{
      

     public bool MinionAutoAttackTarget(
         AttackableUnit Target,
      AttackableUnit Self)
      {
        return
              // Sequence name :MinionAutoAttackTarget
              (
                    TestUnitAIAttackTargetValid(
                          true) &&
                    // Sequence name :Attack or Move
                    (
                          // Sequence name :Attack
                          (
                                GetDistanceBetweenUnits(
                                      out Distance,
                                      Target,
                                      Self) &&
                                GetUnitAttackRange(
                                      out AttackRange,
                                      Self) &&
                                MultiplyFloat(
                                      out AttackRange,
                                      AttackRange,
                                      0.9f) &&
                                LessEqualFloat(
                                      Distance,
                                      AttackRange) &&
                                ClearUnitAIAttackTarget() &&
                                SetUnitAIAttackTarget(
                                      Target) &&
                                IssueAttackOrder()
                          ) ||
                          IssueMoveToUnitOrder(
                                Target)

                    )
              );
      }
}

