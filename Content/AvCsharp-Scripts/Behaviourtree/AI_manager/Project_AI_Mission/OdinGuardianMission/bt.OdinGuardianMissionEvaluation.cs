using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class OdinGuardianMissionEvaluationClass : AImission_bt 
{

    public bool OdinGuardianMissionEvaluation()
    {

    return
            // Sequence name :Sequence
            (
                  SetVarBool(
                        out Run, 
                        true)

            );
      }
}

