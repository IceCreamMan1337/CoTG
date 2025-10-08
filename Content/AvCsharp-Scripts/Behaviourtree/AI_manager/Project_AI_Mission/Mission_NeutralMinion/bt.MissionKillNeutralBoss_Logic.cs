using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using System.Numerics;

namespace BehaviourTrees;


class MissionKillNeutralBoss_LogicClass : AImission_bt 
{

    public bool MissionKillNeutralBoss_Logic() { 
        return
            // Sequence name :Sequence
            (
                  // Sequence name :Selector
                  (
                        // Sequence name :KillNeutralBoss_Logic
                        (
                              DebugAction(
                                    "GetMissionPosition"
                                    ) &&
                              GetAIMissionSelf(
                                    out MissionSelf) &&
                              GetAIMissionSquadMembers(
                                    out SquadMembers, 
                                    MissionSelf) &&
                              GetAIMissionPosition(
                                    out MissionPosition, 
                                    MissionSelf) &&
                              GetCollectionCount(
                                    out SquadCount, 
                                    SquadMembers) &&
                              GreaterInt(
                                    SquadCount, 
                                    1) &&
                              SetVarInt(
                                    out TotalBotsNearPosition, 
                                    0) &&
                              ForEach(SquadMembers ,SquadUnit => (                      
                              // Sequence name :CheckForUnderleveledDudes 
                                    (
                                          GetUnitLevel(
                                                out UnitLevel, 
                                                SquadUnit) &&
                                          GreaterEqualInt(
                                                UnitLevel, 
                                                16)
                                    )
                              )) &&
                              ForEach(SquadMembers ,Entity => (                              
                              // Sequence name :Selector
                                    (
                                          // Sequence name :DistanceChecker
                                          (
                                                DistanceBetweenObjectAndPoint(
                                                      out Distance, 
                                                      Entity, 
                                                      MissionPosition) &&
                                                LessFloat(
                                                      Distance, 
                                                      300) &&
                                                AddInt(
                                                      out TotalBotsNearPosition, 
                                                      TotalBotsNearPosition, 
                                                      1)
                                          )
                                    )
                              )) &&
                              // Sequence name :KillOrMoveTo
                              (
                                    // Sequence name :Kill
                                    (
                                          GreaterEqualInt(
                                                TotalBotsNearPosition, 
                                                SquadCount) &&
                                          ForEach(SquadMembers,Entity => (
                                                // Sequence name :AssignKillTask
                                                (
                                                      // Sequence name :AlreadyHasKillTask
                                                      (
                                                            TestAIEntityHasTask(
                                                                  Entity, 
                                                                 AITaskTopicType.KILL, 
                                                                  Target, 
                                                                  (Vector3)default, 
                                                                  0, 
                                                                  true)
                                                      ) ||
                                                      // Sequence name :AssignNewKillTask
                                                      (
                                                            // Sequence name :FindTarget
                                                            (
                                                                  DebugAction(
                                                                        
                                                                        "LOOKING FOR DRAGON") &&
                                                                  GetUnitsInTargetArea(
                                                                        out JungleTarget, 
                                                                        Entity, 
                                                                        MissionPosition, 
                                                                        1200, 
                                                                        AffectMinions | AffectNeutral) &&
                                                                  GetCollectionCount(
                                                                        out JungleTargetCount, 
                                                                        JungleTarget) &&
                                                                  GreaterInt(
                                                                        JungleTargetCount, 
                                                                        0) &&
                                                                  SetVarFloat(
                                                                        out CurrentClosestDistance, 
                                                                        2000) &&
                                                                  DebugAction(
                                                                        
                                                                        "FOUNDSOMETHING") &&
                                                                 ForEach(JungleTarget , CurrentJungleUnit => (                                                                        // Sequence name :Sequence
                                                                        (
                                                                              DistanceBetweenObjectAndPoint(
                                                                                    out DistanceFromPointToTarget, 
                                                                                    CurrentJungleUnit, 
                                                                                    MissionPosition) &&
                                                                              LessFloat(
                                                                                    DistanceFromPointToTarget, 
                                                                                    CurrentClosestDistance) &&
                                                                              SetVarFloat(
                                                                                    out CurrentClosestDistance, 
                                                                                    DistanceFromPointToTarget) &&
                                                                              SetVarAttackableUnit(
                                                                                    out Target, 
                                                                                    CurrentJungleUnit)
                                                                        )
                                                                  ) &&
                                                                  DebugAction(

                                                                        "TARGET FOUND")
                                                            ) &&
                                                            CreateAITask(
                                                                  out KillTask, 
                                                                 AITaskTopicType.KILL, 
                                                                  Target, 
                                                                  (Vector3)default
                                                                  ) &&
                                                            AssignAITask(
                                                                  Entity, 
                                                                  KillTask) &&
                                                            DebugAction(

                                                                  "TASK SENT")
                                                      )
                                                )
                                          ))
                                    ) ||
                                    // Sequence name :GotoPosition
                                    (
                                          ForEach(SquadMembers, Entity => (
                                                // Sequence name :GotoOrWait
                                                (
                                                      // Sequence name :Wait
                                                      (
                                                            DistanceBetweenObjectAndPoint(
                                                                  out Distance, 
                                                                  Entity, 
                                                                  MissionPosition) &&
                                                            LessFloat(
                                                                  Distance, 
                                                                  315) &&
                                                            // Sequence name :AssignWait
                                                            (
                                                                  // Sequence name :AlreadyHasWait
                                                                  (
                                                                        TestAIEntityHasTask(
                                                                              Entity,
                                                                              AITaskTopicType.WAIT, 
                                                                              null,
                                                                              (Vector3)default, 
                                                                              0, 
                                                                              true) &&
                                                                        DebugAction(
                                                                              "WAITING")
                                                                  ) ||
                                                                  // Sequence name :AssignNewWaitTask
                                                                  (
                                                                        CreateAITask(
                                                                              out WaitTask,
                                                                              AITaskTopicType.WAIT,
                                                                              null,
                                                                              MissionPosition
                                                                              ) &&
                                                                        AssignAITask(
                                                                              Entity, 
                                                                              WaitTask)
                                                                  )
                                                            )
                                                      ) ||
                                                      // Sequence name :AssignGoto
                                                      (
                                                            // Sequence name :AlreadyHasGoto
                                                            (
                                                                  TestAIEntityHasTask(
                                                                        Entity,
                                                                        AITaskTopicType.GOTO, 
                                                                        null, 
                                                                        MissionPosition, 
                                                                        0, 
                                                                        true)
                                                            ) ||
                                                            // Sequence name :AssignNewWaitTask
                                                            (
                                                                  DebugAction(
                                                                        "ON MY WAY"
                                                                        ) &&
                                                                  CreateAITask(
                                                                        out GotoTask,
                                                                        AITaskTopicType.GOTO, 
                                                                        null, 
                                                                        MissionPosition
                                                                        ) &&
                                                                  AssignAITask(
                                                                        Entity, 
                                                                        GotoTask)
                                                            )
                                                      )
                                                ))
                                          )
                                    )
                              )
                        ) ||
                        // Sequence name :OnFail
                        (
                              GetAIMissionSquad(
                                    out CurrentMissionSquad, 
                                    MissionSelf) &&
                              ForEach(SquadMembers,SquadMember => (
                                    // Sequence name :Sequence
                                    (
                                          RemoveAIEntityFromSquad(
                                                SquadMember, 
                                                CurrentMissionSquad)

                                    )
                              ))
                        )
                  )
            )
            //todo: verify parenthesis 
            )
            ;
      }
}

