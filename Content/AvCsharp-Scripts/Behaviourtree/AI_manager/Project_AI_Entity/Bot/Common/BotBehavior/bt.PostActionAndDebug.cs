using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class PostActionAndDebugClass : AI_Characters
{
    protected bool TryCallActiondebug(
    object procedureObject,
    AttackableUnit Self,
    string ActionPerformed
    )
    {
        if (procedureObject == null)
        {
            return false;
        }

        bool callSuccess = CallProcedureVariable(
            out object[] outputs,
            procedureObject,
            Self,
            ActionPerformed);

        if (callSuccess)
        {
            return true;
        }
        return false;
    }
    public bool PostActionAndDebug(
         AttackableUnit Self,
      string ActionPerformed,
      object ActionBehavior,
      int ActionDebugText,
      int TaskDebugText
         )
    {
        return
              // Sequence name :MaskFailure
              (
                    // Sequence name :Sequence
                    (
                          // Sequence name :MaskFailure
                          (
                            TryCallActiondebug(
                                ActionBehavior,
                                   Self,
                                   ActionPerformed)
                       ) &&
                          // Sequence name :Debug
                          (
                                SetVarBool(
                                      out Run,
                                      false) &&
                                Run == true &&
                                SetDebugHidden(
                                      ActionDebugText,
                                      false) &&
                                SetDebugHidden(
                                      TaskDebugText,
                                      false) &&
                                ModifyDebugText(
                                      ActionDebugText,
                                      ActionPerformed,
                                      false) &&
                                // Sequence name :TaskDebugText
                                (
                                      // Sequence name :PUSHLANE
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                                AITaskTopicType.PUSHLANE,
                                                 default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "PUSHLANE",
                                                  false)
                                      ) ||
                                      // Sequence name :GOTO
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                                AITaskTopicType.GOTO,
                                                   default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "GOTO",
                                                  false)
                                      ) ||
                                      // Sequence name :ASSIST
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                                 AITaskTopicType.ASSIST,
                                                  default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "ASSIST",
                                                  false)
                                      ) ||
                                      // Sequence name :DOMINION_GOTO
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                                 AITaskTopicType.DOMINION_GOTO,
                                                   default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "DOMINION_GOTO",
                                                  false)
                                      ) ||
                                      // Sequence name :DOMINION_WAIT
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                                 AITaskTopicType.DOMINION_WAIT,
                                                    default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "DOMINION_WAIT",
                                                  false)
                                      ) ||
                                      // Sequence name :ASSAULT_CAPTURE_POINT
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                                AITaskTopicType.ASSAULT_CAPTURE_POINT,
                                                 default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "ASSAULT_CAPTURE_POINT",
                                                  false)
                                      ) ||
                                      // Sequence name :PUSH_TO_CAPTURE_POINT
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                                AITaskTopicType.PUSH_TO_CAPTURE_POINT,
                                                   default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "PUSH_TO_CAPTURE_POINT",
                                                  false)
                                      ) ||
                                      // Sequence name :NINJA_CAPTURE_POINT
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                                AITaskTopicType.NINJA_CAPTURE_POINT,
                                                  default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "NINJA_CAPTURE_POINT",
                                                  false)
                                      ) ||
                                      // Sequence name :DEFEND_CAPTURE_POINT
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                               AITaskTopicType.DEFEND_CAPTURE_POINT,
                                                  default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "DEFEND_CAPTURE_POINT",
                                                  false)
                                      ) ||
                                      // Sequence name :DOMINION_RETURN_TO_BASE
                                      (
                                            TestAIEntityHasTask(
                                                  Self,
                                               AITaskTopicType.DOMINION_RETURN_TO_BASE,
                                                  default,
                                                 default,
                                                 default,
                                                  true) &&
                                            ModifyDebugText(
                                                  TaskDebugText,
                                                  "DOMINION_RETURN_TO_BASE",
                                                  false)

                                      )

                                )
                          )
                    )
                     || MaskFailure()
              );
    }
}

