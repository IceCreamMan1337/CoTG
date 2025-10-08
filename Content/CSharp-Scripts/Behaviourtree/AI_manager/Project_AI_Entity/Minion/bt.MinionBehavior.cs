using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class MinionBehaviorClass : AI_Characters
{
    private InitializationClass initialization = new();
    private ReferenceUpdateClass referenceUpdate = new();
    private PassiveActionsClass passiveActions = new();
    private TargetVisibilityUpdatesClass targetVisibilityUpdates = new();
    private ActionsClass actions = new();
    public bool MinionBehavior(AttackableUnit Owner)
    {

        Self = Owner;
        SetOwner(Owner as ObjAIBase);

        return
                    // Sequence name :MinionBehavior

                    // Sequence name :Initialize
                    (
                          TestUnitAIFirstTime(
                                true) &&
                          DebugAction("MinionBehavior: Initialization started") &&
                          /*   GetUnitAISelf(
                                   out Self) &&*/
                          GetUnitTeam(
                                out SelfTeam,
                                Self) &&
                          SetVarBool(
                                out AttackIssued,
                                false) &&
                          SetVarAttackableUnit(
                                out AttackIssuedTarget,
                                Self) &&
                          SetVarFloat(
                                out AttackIssuedTimestamp,
                                0) &&
                          DebugAction("MinionBehavior: Initialization completed")
                    ) ||
                    // Sequence name :BehaviorUpdate
                    (
                          DebugAction("MinionBehavior: BehaviorUpdate started") &&
                          TestGameStarted(
                                true) &&
                          DebugAction("MinionBehavior: Game has started - proceeding") &&
                                // Sequence name :UpdateReferences

                                GetUnitPosition(
                                      out SelfPosition,
                                      Self) &&
                                GetGameTime(
                                      out GameTime) &&
                                GetUnitTargetAcquistionRange(
                                      out AcquisitionRange) &&
                                DebugAction($"MinionBehavior: References updated - Position: {SelfPosition}, GameTime: {GameTime}, AcquisitionRange: {AcquisitionRange}")
                           &&
                          // Sequence name :SelectBehavior
                          (
                                // Sequence name :AttackBehavior
                                (
                                      DebugAction("MinionBehavior: AttackBehavior branch started") &&
                                      // Sequence name :AcquireTarget
                                      (
                                            // Sequence name :RespondToCallForHelp
                                            (
                                                  TestHaveCallForHelpTarget(
                                                        true) &&
                                                  DebugAction("MinionBehavior: Responding to CallForHelp") &&
                                                  GetUnitCallForHelpTargetUnit(
                                                        out CFHTarget) &&
                                                  GetUnitCallForHelpCallerUnit(
                                                        out CFHCaller) &&
                                                  SetUnitAIAttackTarget(
                                                        CFHTarget) &&
                                                  SetUnitAIAssistTarget(
                                                        CFHCaller)
                                            ) ||
                                            // Sequence name :UsePreviousTarget
                                            (
                                                  TestUnitAIAttackTargetValid(
                                                        true) &&
                                                  DebugAction("MinionBehavior: Using previous target") &&
                                                  GetUnitAIAttackTarget(
                                                        out Target) &&
                                                  TestUnitIsVisible(
                                                        Self,
                                                        Target,
                                                        true) &&
                                                  SubtractFloat(
                                                        out PursuitTime,
                                                        GameTime,
                                                        AttackIssuedTimestamp) &&
                                                  LessFloat(
                                                        PursuitTime,
                                                        3)
                                            ) ||
                                            // Sequence name :AcquireNewTarget
                                            (
                                                  DebugAction("MinionBehavior: Acquiring new target") &&
                                                  GetUnitsInTargetArea(
                                                        out EnemyUnitsNearSelf,
                                                        Self,
                                                        SelfPosition,
                                                        AcquisitionRange,
                                                        AffectBuildings | AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets
                                                        ) &&
                                                  GetCollectionCount(
                                                        out EnemyCount,
                                                        EnemyUnitsNearSelf) &&
                                                  DebugAction($"MinionBehavior: Found {EnemyCount} enemy units in range") &&
                                                  GreaterInt(
                                                        EnemyCount,
                                                        0) &&
                                                        // Sequence name :FindClosestTarget

                                                        SetVarBool(
                                                              out TargetFound,
                                                              false) &&
                                                        SetVarFloat(
                                                              out CurrentClosestDistance,
                                                              AcquisitionRange) &&
                                                        ForEach(EnemyUnitsNearSelf, EnemyUnit =>
                                                                    // Sequence name :FindClosestTarget

                                                                    TestUnitIsVisible(
                                                                          Self,
                                                                          EnemyUnit,
                                                                          true) &&
                                                                    DistanceBetweenObjectAndPoint(
                                                                          out Distance,
                                                                          EnemyUnit,
                                                                          SelfPosition) &&
                                                                    LessFloat(
                                                                          Distance,
                                                                          CurrentClosestDistance) &&
                                                                    SetVarFloat(
                                                                          out CurrentClosestDistance,
                                                                          Distance) &&
                                                                    SetVarAttackableUnit(
                                                                          out CurrentClosestTarget,
                                                                          EnemyUnit) &&
                                                                    SetVarBool(
                                                                          out TargetFound,
                                                                          true)

                                                        )
                                                   &&
                                                  TargetFound == true &&
                                                  DebugAction("MinionBehavior: Target acquired, setting attack target") &&
                                                  SetUnitAIAssistTarget(
                                                        Self) &&
                                                  SetUnitAIAttackTarget(
                                                        CurrentClosestTarget) &&
                                                  SetVarFloat(
                                                        out AttackIssuedTimestamp,
                                                        GameTime)
                                            )
                                      ) &&
                                            // Sequence name :AttackTarget

                                            DebugAction("MinionBehavior: AttackTarget sequence started") &&
                                            GetUnitAIAttackTarget(
                                                  out Target) &&
                                            GetUnitTeam(
                                                  out TargetTeam,
                                                  Target) &&
                                            NotEqualUnitTeam(
                                                  TargetTeam,
                                                  SelfTeam) &&
                                            TestUnitAIAttackTargetValid(
                                                  true) &&
                                            // Sequence name :AutoAttack
                                            (
                                                  // Sequence name :AutoAttack
                                                  (
                                                        GetDistanceBetweenUnits(
                                                              out Distance,
                                                              Target,
                                                              Self) &&
                                                        GetUnitAttackRange(
                                                              out AttackRange,
                                                              Self) &&
                                                        DebugAction($"MinionBehavior: Distance to target: {Distance}, Attack range: {AttackRange}") &&
                                                        // Sequence name :PreventAttackFailuresFromKiting
                                                        (
                                                              // Sequence name :TestAttackIssued
                                                              (
                                                                    AttackIssued == true &&
                                                                    AttackIssuedTarget == Target &&
                                                                    MultiplyFloat(
                                                                          out AttackRange,
                                                                          AttackRange,
                                                                          1)
                                                              ) ||
                                                              MultiplyFloat(
                                                                    out AttackRange,
                                                                    AttackRange,
                                                                    0.7f)
                                                        ) &&
                                                        LessFloat(
                                                              Distance,
                                                              AttackRange) &&
                                                        DebugAction("MinionBehavior: In attack range, issuing attack order") &&
                                                        ClearUnitAIAttackTarget() &&
                                                        SetUnitAIAttackTarget(
                                                              Target) &&
                                                        IssueAttackOrder() &&
                                                        SetVarBool(
                                                              out AttackIssued,
                                                              true) &&
                                                        SetVarAttackableUnit(
                                                              out AttackIssuedTarget,
                                                              Target) &&
                                                        SetVarFloat(
                                                              out AttackIssuedTimestamp,
                                                              GameTime)
                                                  ) ||
                                                  // Sequence name :MoveToTarget
                                                  (
                                                        DebugAction("MinionBehavior: Moving to target") &&
                                                        IssueMoveToUnitOrder(
                                                              Target) &&
                                                        SetVarBool(
                                                              out AttackIssued,
                                                              false)
                                                  )
                                            )

                                ) ||
                                // Sequence name :PushLaneBehavior
                                (
                                      DebugAction("MinionBehavior: PushLaneBehavior branch started") &&
                                      ClearUnitAIAttackTarget() &&
                                            // Sequence name :GoToTaskPosition

                                            DebugAction("MinionBehavior: PushLaneBehavior branch started") &&
                                            ClearUnitAIAttackTarget() &&
                                                // Sequence name :GoToLaneWaypoint

                                                DebugAction("MinionBehavior: Moving to next lane waypoint") &&
                                                IssueMoveToPositionOrder((Owner as Minion).Closestwaypointofthelist().ToVector3(100))





                                /* (
                                    method with task implementation 
                                       // Sequence name :TestForExistingTask
                                       (
                                             TestUnitAIHasTask(
                                                   true) &&
                                             DebugAction("MinionBehavior: Has existing task, moving to task position") &&
                                             GetUnitAITaskPosition(
                                                   out TaskPosition) &&
                                             IssueMoveToPositionOrder(
                                                   TaskPosition)
                                       ) ||
                                       (
                                             DebugAction("MinionBehavior: No existing task, enabling AI task") &&
                                             IssueAIEnableTaskOrder()
                                       )
                                 )*/
                                )
                          )
                    )
              ;
    }
}

