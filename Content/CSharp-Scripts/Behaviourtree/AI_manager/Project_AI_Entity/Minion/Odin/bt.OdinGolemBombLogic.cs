using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class OdinGolemBombLogicClass : IODIN_MinionAIBT
{
    private FindClosestTargetBuffClass findClosestTargetBuffClass = new();
    public bool OdinGolemBombLogic(ObjAIBase self)
    {
        Self = self;
        var findClosestTarget = new FindClosestTargetClass();
        return


         (SetUnitAISelf(
          Self) &&
         DebugAction("OdinGolemBombLogic: Started") &&
            // Sequence name :Selector
            (

                      // Sequence name :Init
                      (
                            DebugAction("OdinGolemBombLogic: Init sequence") &&
                            TestUnitAIFirstTime() &&

                            SetVarAttackableUnit(
                                  out Target,
                                  Self) &&
                            SetVarBool(
                                  out TargetValid,
                                  false) &&
                            SetVarBool(
                                  out FirstTarget,
                                  true) &&
                            DebugAction("OdinGolemBombLogic: Init completed")
                      ) ||
                  // Sequence name :Fear
                  (
                        TestUnitHasBuff(
                              Self,
                              null,
                              "Fear"
                              ) &&
                        DebugAction("OdinGolemBombLogic: Fear detected, wandering") &&
                        IssueWanderOrder(

                              )
                  ) ||
                  // Sequence name :Taunt
                  (
                        TestUnitHasBuff(
                              Self,
                              null,
                              "Taunt") &&
                        DebugAction("OdinGolemBombLogic: Taunt detected, moving to taunter") &&
                        GetUnitBuffCaster(
                              out Taunter,
                              Self,
                              "Taunt") &&
                        IssueMoveToUnitOrder(
                              Taunter)
                  ) ||
                        // Sequence name :TargetValidTargetAcquisition

                        (DebugAction("OdinGolemBombLogic: Target acquisition started") &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    TargetValid == true &&
                                    TestUnitCondition(
                                          Target, false) &&
                                    DebugAction("OdinGolemBombLogic: Target invalid, clearing") &&
                                    SetVarBool(
                                          out TargetValid,
                                          false)
                              )
                              ||
                               DebugAction("OdinGolemBombLogic: MaskFailure")
                        ) &&
                        TargetValid == false &&
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        DebugAction($"OdinGolemBombLogic: Self position: {SelfPosition}") &&
                                    // Sequence name :FindClosest_Or_NoValid

                                    // Sequence name :ClosestEnemy

                                    FirstTarget == true &&
                                    DebugAction("OdinGolemBombLogic: Searching for closest enemy guardian") &&
                                    GetUnitsInTargetArea(
                                          out UnitsToSearch,
                                          Self,
                                          SelfPosition,
                                          25000,
                                          AffectEnemies | AffectMinions) &&
                                     findClosestTargetBuffClass.FindClosestTargetBuff(
                                          out ClosestEnemyGuardian,
                                          Self,
                                          UnitsToSearch,
                                          null,
                                          false,
                                           false,
                                          SelfPosition,
                                          true,
                                          "OdinGuardianBuff",
                                          true)
                                          &&
                                    SetVarBool(
                                          out TargetValid,
                                          true) &&
                                    SetVarAttackableUnit(
                                          out Target,
                                          ClosestEnemyGuardian) &&
                                    SetVarBool(
                                          out FirstTarget,
                                          false) &&
                                    DebugAction("OdinGolemBombLogic: Closest enemy guardian found"))
                               ||
                              // Sequence name :ClosestEnemy_or_Neutral
                              (
                                    DebugAction("OdinGolemBombLogic: Searching for closest enemy or neutral") &&
                                    GetUnitsInTargetArea(
                                          out UnitsToSearch,
                                          Self,
                                          SelfPosition,
                                          25000,
                                          AffectEnemies | AffectMinions | AffectNeutral) &&
                                    findClosestTargetBuffClass.FindClosestTargetBuff(
                                         out ClosestEnemyGuardian,
                                            Self,
                                          UnitsToSearch,
                                          null,
                                          false,
                                          false,
                                          SelfPosition,
                                          true,
                                          "OdinGuardianBuff",
                                          true)
                                    &&
                                    SetVarBool(
                                          out TargetValid,
                                          true) &&
                                    SetVarAttackableUnit(
                                          out Target,
                                          ClosestEnemyGuardian) &&
                                    SetVarBool(
                                          out FirstTarget,
                                          false) &&
                                    DebugAction("OdinGolemBombLogic: Closest enemy or neutral found")
                              ) ||
                              // Sequence name :Sequence
                              (
                                    DebugAction("OdinGolemBombLogic: No target found, stopping movement") &&
                                    StopUnitMovement(
                                          Self) &&
                                    Say(
                                          Self,
                                          "I'm Homeless")
                              )

                  )) ||
                  // Sequence name :Attack
                  (
                        TargetValid == true &&
                        DebugAction("OdinGolemBombLogic: Attack sequence started") &&
                        GetDistanceBetweenUnits(
                              out Distance,
                              Target,
                              Self) &&
                        GetUnitAttackRange(
                              out AttackRange,
                              Self) &&
                        AddString(
                              out ToPrint,
                              "Distance:",
                              $"{Distance}") &&
                        DebugAction($"OdinGolemBombLogic: Distance to target: {Distance}, Attack range: {AttackRange}") &&
                        LessEqualFloat(
                              Distance,
                              AttackRange) &&
                        DebugAction("OdinGolemBombLogic: In attack range, issuing attack order") &&
                        SetUnitAIAttackTarget(
                              Target) &&
                        IssueAttackOrder()
                  ) ||
                  // Sequence name :Execution
                  (
                        TargetValid == true &&
                        DebugAction("OdinGolemBombLogic: Execution sequence - moving to target") &&
                        SetUnitAIAttackTarget(
                              Target) &&
                        IssueMoveToUnitOrder(
                              Target)

                  )

            ;
    }
}