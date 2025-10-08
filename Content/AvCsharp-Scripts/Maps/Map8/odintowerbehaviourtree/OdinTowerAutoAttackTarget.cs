using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class OdinTowerAutoAttackTargetClass : OdinLayout 
{
     

     public bool OdinTowerAutoAttackTarget(
           AttackableUnit Target,
    AttackableUnit Self
          )
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
            );
      }
}

