namespace BehaviourTrees.all;

/*
class OdinTowerAutoAttackTarget : AI_Characters 
{
      AttackableUnit Target,
      AttackableUnit Self,

     public bool OdinTowerAutoAttackTarget()
      {
      return
            // Sequence name :OdinTowerAutoAttackTarget
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
            ),
      }
}

*/