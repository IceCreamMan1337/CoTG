using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class MissionPush_LogicClass : AImission_bt
{

    public bool MissionPush_Logic()
    {

        var findFurthestUnitOrTurretInLane = new FindFurthestUnitOrTurretInLaneClass();

        return
                    // Sequence name :TaskAssignment

                    DebugAction("MissionPush_Logic: Début de l'exécution") &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :Initialize
                          (
                                DebugAction("MissionPush_Logic: TestAIFirstTime") &&
                                TestAIFirstTime(
                                      true) &&
                                DebugAction("MissionPush_Logic: GetGameTime") &&
                                GetGameTime(
                                      out LastUpdateTime) &&
                                DebugAction("MissionPush_Logic: GetAIMissionSelf") &&
                                GetAIMissionSelf(
                                      out ThisMission) &&
                                DebugAction($"MissionPush_Logic: Initialisation terminée - LastUpdateTime: {LastUpdateTime}, Mission: {ThisMission?.Topic}")
                          )
                          ||
                             (DebugAction("MissionPush_Logic: MaskFailure appelé") && MaskFailure())
                    ) &&
                          // Sequence name :TaskAssignmentPerSecond

                          DebugAction("MissionPush_Logic: Début TaskAssignmentPerSecond") &&
                          GetGameTime(
                                out CurrentGameTime) &&
                          DebugAction($"MissionPush_Logic: CurrentGameTime: {CurrentGameTime}") &&
                          SubtractFloat(
                                out TimeDiff,
                                CurrentGameTime,
                                LastUpdateTime) &&
                          DebugAction($"MissionPush_Logic: TimeDiff: {TimeDiff}") &&
                          GreaterEqualFloat(
                                TimeDiff,
                                1) &&
                          DebugAction("MissionPush_Logic: TimeDiff >= 1, mise à jour du temps") &&
                          SetVarFloat(
                                out LastUpdateTime,
                                CurrentGameTime) &&

                          DebugAction("MissionPush_Logic: GetAIMissionIndex") &&
                          GetAIMissionIndex(
                                out MyLane,
                                ThisMission) &&
                          DebugAction($"MissionPush_Logic: MyLane: {MyLane}") &&

                          DebugAction("MissionPush_Logic: GetAIMissionSquadMembers") &&
                          GetAIMissionSquadMembers(
                                out SquadMembers,
                                ThisMission) &&
                          DebugAction($"MissionPush_Logic: SquadMembers count: {SquadMembers?.Count() ?? 0}") &&
                          ForEach(SquadMembers, Entity =>
                                      // Sequence name :Find_Reference_Unit

                                      DebugAction($"MissionPush_Logic: Find_Reference_Unit pour {Entity?.Name}") &&
                                      SetVarAttackableUnit(
                                            out ReferenceUnit,
                                            Entity) &&
                                      GetUnitPosition(
                                            out ReferencePosition,
                                            ReferenceUnit) &&
                                      DebugAction($"MissionPush_Logic: ReferencePosition: {ReferencePosition}")

                          ) &&

                          DebugAction("MissionPush_Logic: Initialisation UnitsCollectionInitialized = false") &&
                          SetVarBool(
                                out UnitsCollectionInitialized,
                                false) &&


                          ForEach(SquadMembers, Entity =>
                                DebugAction($"MissionPush_Logic: CheckDistanceToLane pour {Entity?.Name}") &&
                                      // Sequence name :CheckDistanceToLane

                                      // Sequence name :Init_Unit_Collection
                                      (
                                            (DebugAction($"MissionPush_Logic: UnitsCollectionInitialized: {UnitsCollectionInitialized}") &&
                                            UnitsCollectionInitialized == true) ||
                                            // Sequence name :Initialize
                                            (
                                                  DebugAction("MissionPush_Logic: Initialisation de la collection d'unités") &&
                                                  GetUnitsInTargetArea(
                                                        out FriendlyUnitsTurretsInhibs,
                                                        ReferenceUnit,
                                                        ReferencePosition,
                                                        20000,
                                                        AffectBuildings | AffectFriends | AffectMinions | AffectTurrets | AffectUntargetable  //hack AffectUntargetable 
                                                        ) &&
                                                  DebugAction($"MissionPush_Logic: FriendlyUnitsTurretsInhibs count: {FriendlyUnitsTurretsInhibs?.Count() ?? 0}") &&
                                                  SetVarBool(
                                                        out UnitsCollectionInitialized,
                                                        true) &&
                                                  DebugAction("MissionPush_Logic: UnitsCollectionInitialized = true")
                                            )
                                      ) &&
                                      // Sequence name :TryFindFurthestUnit_Or_IssuePush
                                      (
                                            (DebugAction("MissionPush_Logic: TryFindFurthestUnit_Or_IssuePush") &&
                                                  // Option 1: Essayer de trouver l'unité la plus éloignée et faire IssueGoto

                                                  DebugAction("MissionPush_Logic: FindFurthestUnitOrTurretInLane") &&
                                                  findFurthestUnitOrTurretInLane.FindFurthestUnitOrTurretInLane(
                                                       out FurthestUnit,
                                                       out FurthestDistance,
                                                       FriendlyUnitsTurretsInhibs,
                                                       MyLane,
                                                       Entity,
                                                       true) &&
                                                  DebugAction($"MissionPush_Logic: FurthestUnit: {FurthestUnit?.Name}, Distance: {FurthestDistance}") &&
                                                  GetUnitPosition(
                                                       out FurthestPosition,
                                                       FurthestUnit) &&
                                                 GetDistanceBetweenUnits(
                                                       out DistanceToLane,
                                                       Entity,
                                                       FurthestUnit) &&
                                                 DebugAction($"MissionPush_Logic: DistanceToLane: {DistanceToLane}") &&
                                                       // IssueGoto (désactivé par hack)

                                                       DebugAction("MissionPush_Logic: IssueGoto (désactivé par hack)") &&
                                                       false) //hack test

                                             ||
                                            // Option 2: Si FindFurthestUnitOrTurretInLane échoue, faire IssuePush
                                            (
                                                  DebugAction("MissionPush_Logic: FindFurthestUnitOrTurretInLane échoué, fallback vers IssuePush") &&
                                                  // Sequence name :IssuePush
                                                  (
                                                        (DebugAction("MissionPush_Logic: IssuePush (fallback)") &&
                                                              // Sequence name :HasPushTask

                                                              DebugAction("MissionPush_Logic: TestAIEntityHasTask PUSHLANE") &&
                                                              TestAIEntityHasTask(
                                                                    Entity,
                                                                    AITaskTopicType.PUSHLANE,
                                                                    null,
                                                                    default,
                                                                    MyLane,
                                                                    true))
                                                         ||
                                                        // Sequence name :AssignTask

                                                        (
                                                              DebugAction("MissionPush_Logic: CreateAITask PUSHLANE") &&
                                                              CreateAITask(
                                                                    out PushLaneTask,
                                                                    AITaskTopicType.PUSHLANE,
                                                                     null,
                                                                    default,
                                                                    MyLane) &&
                                                              DebugAction("MissionPush_Logic: AssignAITask PUSHLANE") &&
                                                              AssignAITask(
                                                                    Entity,
                                                                    PushLaneTask) &&
                                                              DebugAction($"MissionPush_Logic: PushLaneTask assigné à {Entity?.Name}")
                                                        )
                                                  )
                                            )
                                      )

                          ) &&
                          DebugAction("MissionPush_Logic: Fin de l'exécution")

              ;
    }
}

