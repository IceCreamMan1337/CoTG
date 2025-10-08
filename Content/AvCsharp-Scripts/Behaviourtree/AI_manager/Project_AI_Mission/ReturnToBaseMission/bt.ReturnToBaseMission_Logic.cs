using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class ReturnToBaseMission_LogicClass : AImission_bt 
{

      public bool ReturnToBaseMission_Logic() { 
        return
            // Sequence name :TaskAssignment
            (
                  // Sequence name :MaskFailure
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
                                               AITaskTopicType.DOMINION_RETURN_TO_BASE, 
                                                null, 
                                                BasePosition, 
                                                0, 
                                                true)                ||                          // Sequence name :IssueNewGotoTask
                                          (
                                                CreateAITask(
                                                      out GotoTask,
                                                      AITaskTopicType.DOMINION_RETURN_TO_BASE, 
                                                      null, 
                                                      BasePosition
                                                      ) &&
                                                AssignAITask(
                                                      Entity, 
                                                      GotoTask)

                                          )
                                    )
                              )
                        ))
                  )
            );
      }
}

