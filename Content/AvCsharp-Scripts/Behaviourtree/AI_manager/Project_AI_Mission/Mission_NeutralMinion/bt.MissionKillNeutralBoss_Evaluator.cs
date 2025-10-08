using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class MissionKillNeutralBoss_EvaluatorClass : AImission_bt 
{

    public bool MissionKillNeutralBoss_Evaluator() { 
      return
            // Sequence name :Sequence
            (
                  DebugAction(
                        
                        "DebugAction_SetMissionStatus_true")

            );
      }
}

