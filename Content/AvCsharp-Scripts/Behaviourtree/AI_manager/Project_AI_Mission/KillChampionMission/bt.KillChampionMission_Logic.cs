using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class KillChampionMission_LogicClass : AImission_bt 
{

      public bool KillChampionMission_Logic() {
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
                                // Sequence name :IssueKill
                                (
                                      GetAIMissionTarget(
                                            out Target,
                                            ThisMission) &&
                                      // Sequence name :HasValidKill_Or_IssueKill
                                      (
                                            TestAIEntityHasTask(
                                                  Entity,
                                                 AITaskTopicType.ASSIST,
                                                  Target,
                                                  (Vector3)default, 
                                                0, 
                                                true)    ||                                      // Sequence name :Sequence
                                          (
                                                CreateAITask(
                                                      out KillTask,
                                                      AITaskTopicType.ASSIST,
                                                  Target,
                                                  (Vector3)default
                                                      ) &&
                                                AssignAITask(
                                                      Entity, 
                                                      KillTask)

                                          )
                                    )
                              ))
                        )
                  )
            );


      }
}

