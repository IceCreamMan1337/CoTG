using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class LastHitClass : AI_Characters 
{
     
    private FindSafeLastHitPositionClass findSafeLastHitPosition= new FindSafeLastHitPositionClass();
     public bool LastHit(
          out bool _IssuedAttack,
      out AttackableUnit _IssuedAttackTarget,
      AttackableUnit Target,
      AttackableUnit Self,
      bool PrevIssuedAttack,
      AttackableUnit PrevIssuedAttackTarget,
      float ThresholdModifier
         )
    {
        bool IssuedAttack = default;
        AttackableUnit IssuedAttackTarget = default;
        bool result =
            // Sequence name :LastHitOrAutoAttack
            (
                  // Sequence name :AutoAttackIfConditionsMet
                  (
                        ThresholdModifier == 0 &&
                        GetUnitAttackDamage(
                              out SelfAttackDamage,
                              Self) &&
                        GetUnitCurrentHealth(
                              out TargetHealth,
                              Target) &&
                        GetGameTime(
                              out CurrentGameTime) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        CountUnitsInTargetArea(
                              out EnemyChampCount,
                              Self,
                              TargetPosition,
                              1200,
                              AffectEnemies | AffectHeroes,
                              "") &&
                        GetUnitLevel(
                              out SelfLevel,
                              Self) &&
                        MultiplyFloat(
                              out WaitThreshold,
                              SelfAttackDamage,
                              2.5f) &&
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        CountUnitsInTargetArea(
                              out FriendlyMinionCount,
                              Self,
                              SelfPosition,
                              800,
                              AffectFriends | AffectMinions,
                              "") &&
                        CountUnitsInTargetArea(
                              out EnemyMinionCount,
                              Self,
                              TargetPosition,
                              1000,
                              AffectEnemies | AffectMinions,
                              "") &&
                        SubtractInt(
                              out MinionDiff,
                              FriendlyMinionCount,
                              EnemyMinionCount) &&
                        AbsInt(
                              out MinionDiff,
                              MinionDiff) &&
                        CountUnitsInTargetArea(
                              out FriendlyStructureCount,
                              Self,
                              SelfPosition,
                              800,
                              AffectBuildings | AffectFriends | AffectTurrets,
                              "") &&
                        // Sequence name :Conditions
                        (
                              // Sequence name :NoEnemiesNearbyAndAttackDamage&gt,120
                              (
                                    EnemyChampCount == 0 &&
                                    GreaterFloat(
                                          SelfAttackDamage,
                                          120)
                              ) &&
                              GreaterFloat(
                                    CurrentGameTime,
                                    1200)
                              &&
                              GreaterInt(
                                    SelfLevel,
                                    11)
                              &&
                             FriendlyMinionCount == 0
                             &&
                                GreaterInt(
                                    FriendlyStructureCount,
                                    0)
                                &&
                                GreaterInt(
                                    MinionDiff,
                                    5)
                        ) &&
                        SetVarBool(
                              out IssuedAttack,
                              PrevIssuedAttack) &&
                        SetVarAttackableUnit(
                              out IssuedAttackTarget,
                              PrevIssuedAttackTarget) &&
                        SetUnitAIAttackTarget(
                              Target) &&
                        IssueChaseOrder()
                  ) ||
                  // Sequence name :LastHit
                  (
                        GetUnitAttackRange(
                              out AttackRange,
                              Self) &&
                        GetDistanceBetweenUnits(
                              out DistanceToTarget,
                              Target,
                              Self) &&
                        GetUnitCurrentHealth(
                              out TargetHealth,
                              Target) &&
                        GetUnitAttackDamage(
                              out SelfAttackDamage,
                              Self) &&
                        AddFloat(
                              out SelfAttackDamage,
                              SelfAttackDamage,
                              15) &&
                        AddFloat(
                              out SelfAttackDamage,
                              SelfAttackDamage,
                              ThresholdModifier) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        CountUnitsInTargetArea(
                              out FriendlyMinionCount,
                              Self,
                              TargetPosition,
                              400,
                              AffectFriends | AffectMinions,
                              "") &&
                        CountUnitsInTargetArea(
                              out EnemyMinionCount,
                              Self,
                              TargetPosition,
                              400,
                              AffectEnemies | AffectMinions,
                              "") &&
                        SubtractInt(
                              out MinionDiff,
                              FriendlyMinionCount,
                              EnemyMinionCount) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    GreaterInt(
                                          MinionDiff,
                                          2) &&
                                    MultiplyFloat(
                                          out MinionModifier,
                                          MinionDiff,
                                          9) &&
                                    AddFloat(
                                          out SelfAttackDamage,
                                          SelfAttackDamage,
                                          MinionModifier)
                              )
                              ||MaskFailure()
                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    GreaterFloat(
                                          DistanceToTarget,
                                          AttackRange) &&
                                    SubtractFloat(
                                          out DistanceModifier,
                                          DistanceToTarget,
                                          AttackRange) &&
                                    DivideFloat(
                                          out DistanceModifier,
                                          DistanceModifier,
                                          16) &&
                                    AddFloat(
                                          out SelfAttackDamage,
                                          SelfAttackDamage,
                                          DistanceModifier)
                              ) || MaskFailure()
                        ) &&
                        // Sequence name :LastHit
                        (
                              // Sequence name :LastHit
                              (
                                    LessFloat(
                                          TargetHealth,
                                          SelfAttackDamage) &&
                                    SetVarBool(
                                          out IssuedAttack,
                                          PrevIssuedAttack) &&
                                    SetVarAttackableUnit(
                                          out IssuedAttackTarget,
                                          PrevIssuedAttackTarget) &&
                                    SetUnitAIAttackTarget(
                                          Target) &&
                                    IssueChaseOrder()
                              ) ||
                              // Sequence name :MoveToSafePointOrWander
                              (
                                 findSafeLastHitPosition.FindSafeLastHitPosition(
                                          out SafePosition,
                                          Self) &&
                                    IssueWanderOrder(
                                             SafePosition, 
                                             250)

                              )
                        )
                  )
            );

         _IssuedAttack = IssuedAttack;
         _IssuedAttackTarget = IssuedAttackTarget;
        return result;
      }
}

