using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Fiddlesticks_PostActionBehaviorClass : AI_Characters
{


    public bool Fiddlesticks_PostActionBehavior(
         AttackableUnit Self,
     string ActionPerformed
        )
    {
        return
                    // Sequence name :Fiddlesticks_PostActionBehavior

                    ActionPerformed == "Channeling" &&
                    TestUnitHasBuff(
                          Self,
                          default,
                          "Crowstorm",
                          true) &&
                    GetUnitPosition(
                          out SelfPosition,
                          Self) &&
                    GetUnitsInTargetArea(
                          out EnemyChampCollection,
                          Self,
                          SelfPosition,
                          1000,
                          AffectEnemies | AffectHeroes) &&
                   ForEach(EnemyChampCollection, EnemyChamp =>
                                // Sequence name :Sequence

                                TestUnitHasBuff(
                                      EnemyChamp,
                                     default,
                                      "DrainChannel",
                                      true) &&
                                SetVarAttackableUnit(
                                      out Target,
                                      EnemyChamp)

                    ) &&
                    GetDistanceBetweenUnits(
                          out DistanceToTarget,
                          Self,
                          Target) &&
                    GreaterFloat(
                          DistanceToTarget,
                          300) &&
                    IssueMoveToUnitOrder(
                          Target)

              ;
    }
}

