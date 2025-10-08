namespace BehaviourTrees;


class AssignTaskWithPositionClass : AImission_bt
{


    public bool AssignTaskWithPosition(
               AITaskTopicType TaskType,
   Vector3 TaskPosition,
     AttackableUnit UnitToAssign
         )



    {
        return
                    // Sequence name :IssueTask

                    TestAIEntityHasTask(
                          UnitToAssign,
                          TaskType,
                          null,
                          TaskPosition,
                          0,
                          true) ||     // Sequence name :IssueTask
                    (
                          CreateAITask(
                                out ToAssign,
                                TaskType,
                                null,
                                TaskPosition
                                ) &&
                          AssignAITask(
                                UnitToAssign,
                                ToAssign)

                    )
              ;
    }
}

