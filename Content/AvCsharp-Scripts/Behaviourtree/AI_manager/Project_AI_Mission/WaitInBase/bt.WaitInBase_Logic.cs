using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class WaitInBase_LogicClass : AImission_bt 
{
    /// <summary>
    ///  ExecutePeriodically(  true,   5)*/
    /// </summary>
    public bool WaitInBase_Logic() { 
      
        return
        /*    ExecutePeriodically(
                  true, 
                  5)*/
                  // Sequence name :TaskAssignment
                  (
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Initialize
                              (
                                    TestAIFirstTime(
                                          true) &&
                                    GetGameTime(
                                          out LastUpdateTime)   &&
                                  GetAIMissionSelf(
                                          out ThisMission)
                              )
                              ||
                               DebugAction("MaskFailure")
                        ) &&
                        // Sequence name :TaskAssignmentPerSecond
                        (
                              GetAIMissionSquadMembers(
                                    out SquadMembers, 
                                    ThisMission) &&
                              ForEach(SquadMembers,Entity => (
                                    // Sequence name :IssueGoto
                                    (
                                          GetUnitAIBasePosition(
                                                out BasePosition, 
                                                Entity) &&
                                          // Sequence name :HasValidGoto_Or_IssueGoto
                                          (
                                                TestAIEntityHasTask(
                                                      Entity, 
                                                     AITaskTopicType.GOTO, 
                                                      null, 
                                                      BasePosition, 
                                                      0, 
                                                      true) 
                                                ||                                          
                                                      // Sequence name :IssueNewGotoTask
                                                (
                                                      CreateAITask(
                                                            out GotoTask,
                                                             AITaskTopicType.GOTO,
                                                            null,
                                                            BasePosition
                                                            ) &&
                                                      AssignAITask(
                                                            Entity, 
                                                            GotoTask)

                                                )
                                          )
                                    )
                              )
                        )
                  )
            );
      }
}

