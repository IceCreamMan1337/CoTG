namespace BehaviourTrees.Map8;

/*
class MissionPush_LogicClass : AImission_bt
{

     public bool MissionPush_Logic()
    { 


            // Sequence name :TaskAssignment
            return 
                (  // Sequence name :MaskFailure
                  (
                        // Sequence name :Initialize
                        (
                              TestAIFirstTime(
                                    true) &&
                              GetGameTime(
                                    out LastUpdateTime) &&
                              GetAIMissionSelf(
                                    out ThisMission)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :TaskAssignmentPerSecond
                  (
                        GetGameTime(
                              out CurrentGameTime) &&
                        SubtractFloat(
                              out TimeDiff, 
                              CurrentGameTime, 
                              LastUpdateTime) &&
                        GreaterEqualFloat(
                              TimeDiff, 
                              1) &&
                        SetVarFloat(
                              out LastUpdateTime, 
                              CurrentGameTime) &&
                        GetAIMissionIndex(
                              out MyLane, 
                              ThisMission) &&
                        GetAIMissionSquadMembers(
                              out SquadMembers, 
                              ThisMission) &&
                        ForEach(SquadMembers,Entity => (                             
                        // Sequence name :Find_Reference_Unit
                              (
                                    SetVarAttackableUnit(
                                          out ReferenceUnit, 
                                          Entity) &&
                                    GetUnitPosition(
                                          out ReferencePosition, 
                                          ReferenceUnit)
                              ))
                        ) &&
                        ForEach(SquadMembers,Entity => (
                              // Sequence name :CheckDistanceToLane
                              (
                                    // Sequence name :IssuePush
                                    (
                                          // Sequence name :HasPushTask
                                          (
                                                TestAIEntityHasTask(
                                                      Entity, 
                                                     AITaskTopicType.PUSHLANE, 
                                                      null, 
                                                      (Vector3)default, 
                                                      MyLane, 
                                                      true)
                                          ) ||
                                          // Sequence name :AssignTask
                                          (
                                                CreateAITask(
                                                      out PushLaneTask,
                                                      AITaskTopicType.PUSHLANE,
                                                      null,
                                                      (Vector3)default,
                                                      MyLane) &&
                                                AssignAITask(
                                                      Entity, 
                                                      PushLaneTask)

                                          )
                                    )
                              ))
                        )
                  )
            );
      }
}

*/