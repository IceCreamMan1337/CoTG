using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class SubMission_ReturnToBaseClass : AImission_bt
{
     

     public bool SubMission_ReturnToBase(
           AttackableUnit Unit)
      {
      return
            // Sequence name :IssueGoto
            (
                  GetUnitAIBasePosition(
                        out BasePosition, 
                        Unit) &&
                  // Sequence name :HasValidGoto_Or_IssueGoto
                  (
                        TestAIEntityHasTask(
                              Unit, 
                             AITaskTopicType.DOMINION_RETURN_TO_BASE, 
                              null, 
                              BasePosition, 
                              0, 
                              true)     ||    
                        // Sequence name :IssueNewGotoTask
                        (
                              CreateAITask(
                                    out GotoTask,
                                    AITaskTopicType.DOMINION_RETURN_TO_BASE,
                                    null,
                                    BasePosition
                                    ) &&
                              AssignAITask(
                                    Unit, 
                                    GotoTask)

                        )
                  )
            );
      }
}

