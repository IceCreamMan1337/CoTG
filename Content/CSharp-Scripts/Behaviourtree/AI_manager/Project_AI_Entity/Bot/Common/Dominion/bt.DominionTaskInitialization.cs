namespace BehaviourTrees.all;


class DominionTaskInitializationClass : AI_Characters
{


    public bool DominionTaskInitialization(
          out string _CurrentTask,
     out bool _RunBT_SuppressCapturePoint,
     out bool _RunBT_KillChampion,
     out bool _RunBT_HighThreat,
     out bool _RunBT_LowThreat,
     out bool _RunBT_CapturePoint,
     out bool _RunBT_ReturnToBase,
     out bool _RunBT_Attack,
     out bool _RunBT_Move,
     out bool _RunBT_Wander,
     out string __PreviousTask,
     out bool _RunBT_InterruptCapture,
     out bool _KillChampionAggressiveState,
     out bool _RunBT_RetreatFromEnemyCapturePoint,
     out bool _RunBT_MidThreat,
     out bool _RunBT_NinjaCapturePoint,
     AttackableUnit Self,
     string PreviousTask)
    {

        string CurrentTask = default;
        bool RunBT_SuppressCapturePoint = default;
        bool RunBT_KillChampion = default;
        bool RunBT_HighThreat = default;
        bool RunBT_LowThreat = default;
        bool RunBT_CapturePoint = default;
        bool RunBT_ReturnToBase = default;
        bool RunBT_Attack = default;
        bool RunBT_Move = default;
        bool RunBT_Wander = default;
        string _PreviousTask = default;
        bool RunBT_InterruptCapture = default;
        bool KillChampionAggressiveState = default;
        bool RunBT_RetreatFromEnemyCapturePoint = default;
        bool RunBT_MidThreat = default;
        bool RunBT_NinjaCapturePoint = default;

        bool result =
                        // Sequence name :MaskFailure

                        // Sequence name :Sequence

                        SetVarString(
                              out CurrentTask,
                              ""
                              ) &&
                        // Sequence name :Tasks
                        (
                              // Sequence name :GotoTask
                              (
                                    TestAIEntityHasTask(
                                          Self,
                                         AITaskTopicType.DOMINION_GOTO,
                                          default,
                                          default,
                                          default,
                                          true) &&
                                    SetVarString(
                                          out CurrentTask,
                                          "GotoTask")
                              ) ||
                              // Sequence name :WaitTask
                              (
                                    TestAIEntityHasTask(
                                          Self,
                                          AITaskTopicType.DOMINION_WAIT,
                                          default,
                                          default,
                                          default,
                                          true) &&
                                    SetVarString(
                                          out CurrentTask,
                                          "WaitTask")
                              ) ||
                              // Sequence name :AssaultCapturePoint
                              (
                                    TestAIEntityHasTask(
                                          Self,
                                          AITaskTopicType.ASSAULT_CAPTURE_POINT,
                                           default,
                                          default,
                                          default,
                                          true) &&
                                    SetVarString(
                                          out CurrentTask,
                                          "AssaultCapturePointTask")
                              ) ||
                              // Sequence name :PushToCapturePoint
                              (
                                    TestAIEntityHasTask(
                                          Self,
                                          AITaskTopicType.PUSH_TO_CAPTURE_POINT,
                                           default,
                                          default,
                                          default,
                                          true) &&
                                    SetVarString(
                                          out CurrentTask,
                                          "PushToCapturePointTask")
                              ) ||
                              // Sequence name :NinjaCapturePoint
                              (
                                    TestAIEntityHasTask(
                                          Self,
                                          AITaskTopicType.NINJA_CAPTURE_POINT,
                                          default,
                                          default,
                                          default,
                                          true) &&
                                    SetVarString(
                                          out CurrentTask,
                                          "NinjaCapturePointTask")
                              ) ||
                              // Sequence name :DefendCapturePoint
                              (
                                    TestAIEntityHasTask(
                                          Self,
                                          AITaskTopicType.DEFEND_CAPTURE_POINT,
                                          default,
                                          default,
                                          default,
                                          true) &&
                                    SetVarString(
                                          out CurrentTask,
                                          "DefendCapturePointTask")
                              ) ||
                              // Sequence name :ReturnToBase
                              (
                                    TestAIEntityHasTask(
                                          Self,
                                          AITaskTopicType.DOMINION_RETURN_TO_BASE,
                                             default,
                                          default,
                                          default,
                                          true) &&
                                    SetVarString(
                                          out CurrentTask,
                                          "ReturnToBaseTask")
                              ) ||
                              // Sequence name :AssistAlly
                              (
                                    TestAIEntityHasTask(
                                          Self,
                                          AITaskTopicType.ASSIST,
                                            default,
                                          default,
                                          default,
                                          true) &&
                                    SetVarString(
                                          out CurrentTask,
                                          "AssistTask")
                              ) ||
                                    // Sequence name :Sequence

                                    SetVarString(
                                          out CurrentTask,
                                          "UndefinedTask")

                        ) &&
                              // Sequence name :Reinit_Tasks?

                              CurrentTask == PreviousTask
                              &&
                              CurrentTask == "AssistTask"
                              &&
                                          // Sequence name :Reinit

                                          // Sequence name :InitBasedOnTask

                                          SetVarBool(
                                                out RunBT_NinjaCapturePoint,
                                                false) &&
                                          SetVarBool(
                                                out RunBT_SuppressCapturePoint,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_InterruptCapture,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_KillChampion,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_HighThreat,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_LowThreat,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_ReturnToBase,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_MidThreat,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_CapturePoint,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_Attack,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_Move,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_Wander,
                                                true) &&
                                          SetVarBool(
                                                out RunBT_RetreatFromEnemyCapturePoint,
                                                true) &&
                                          SetVarBool(
                                                out KillChampionAggressiveState,
                                                false)
                                     &&
                                    // Sequence name :SetBehaviorsBasedOnTasks
                                    (
                                          // Sequence name :GotoTask
                                          (
                                                TestAIEntityHasTask(
                                                      Self,
                                                      AITaskTopicType.DOMINION_GOTO,
                                                         default,
                                          default,
                                          default,
                                                      true) &&
                                                SetVarBool(
                                                      out RunBT_CapturePoint,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_Attack,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_Wander,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_ReturnToBase,
                                                      false)
                                          ) ||
                                          // Sequence name :WaitTask
                                          (
                                                TestAIEntityHasTask(
                                                      Self,
                                                      AITaskTopicType.DOMINION_WAIT,
                                                        default,
                                          default,
                                          default,
                                                      true) &&
                                                SetVarBool(
                                                      out RunBT_SuppressCapturePoint,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_CapturePoint,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_InterruptCapture,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_ReturnToBase,
                                                      false)
                                          ) ||
                                          // Sequence name :AssaultCapturePoint
                                          (
                                                TestAIEntityHasTask(
                                                      Self,
                                                      AITaskTopicType.ASSAULT_CAPTURE_POINT,
                                                        default,
                                          default,
                                          default,
                                                      true) &&
                                                SetVarBool(
                                                      out RunBT_Attack,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_Wander,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_LowThreat,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_ReturnToBase,
                                                      false) &&
                                                SetVarBool(
                                                      out KillChampionAggressiveState,
                                                      true)
                                          ) ||
                                          // Sequence name :PushToCapturePoint
                                          (
                                                TestAIEntityHasTask(
                                                      Self,
                                                      AITaskTopicType.PUSH_TO_CAPTURE_POINT,
                                                        default,
                                          default,
                                          default,
                                                      true) &&
                                                SetVarBool(
                                                      out RunBT_Wander,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_ReturnToBase,
                                                      false)
                                          ) ||
                                          // Sequence name :NinjaCapturePoint
                                          (
                                                TestAIEntityHasTask(
                                                      Self,
                                                      AITaskTopicType.NINJA_CAPTURE_POINT,
                                                       default,
                                          default,
                                          default,
                                                      true) &&
                                                SetVarBool(
                                                      out RunBT_NinjaCapturePoint,
                                                      true) &&
                                                SetVarBool(
                                                      out RunBT_SuppressCapturePoint,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_Attack,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_Wander,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_LowThreat,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_ReturnToBase,
                                                      false)
                                          ) ||
                                          // Sequence name :DefendCapturePoint
                                          (
                                                TestAIEntityHasTask(
                                                      Self,
                                                      AITaskTopicType.DEFEND_CAPTURE_POINT,
                                                       default,
                                          default,
                                          default,
                                                      true) &&
                                                SetVarBool(
                                                      out RunBT_SuppressCapturePoint,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_LowThreat,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_CapturePoint,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_ReturnToBase,
                                                      false) &&
                                                SetVarBool(
                                                      out KillChampionAggressiveState,
                                                      true)
                                          ) ||
                                          // Sequence name :ReturnToBase
                                          (
                                                TestAIEntityHasTask(
                                                      Self,
                                                     AITaskTopicType.DOMINION_RETURN_TO_BASE,
                                                         default,
                                          default,
                                          default,
                                                      true) &&
                                                SetVarBool(
                                                      out RunBT_SuppressCapturePoint,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_InterruptCapture,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_KillChampion,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_LowThreat,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_CapturePoint,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_Attack,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_Move,
                                                      false) &&
                                                SetVarBool(
                                                      out RunBT_Wander,
                                                      false)
                                          )
                                    ) &&
                                    SetVarString(
                                          out _PreviousTask,
                                          CurrentTask)




            ;
        _CurrentTask = CurrentTask;
        _RunBT_SuppressCapturePoint = RunBT_SuppressCapturePoint;
        _RunBT_KillChampion = RunBT_KillChampion;
        _RunBT_HighThreat = RunBT_HighThreat;
        _RunBT_LowThreat = RunBT_LowThreat;
        _RunBT_CapturePoint = RunBT_CapturePoint;
        _RunBT_ReturnToBase = RunBT_ReturnToBase;
        _RunBT_Attack = RunBT_Attack;
        _RunBT_Move = RunBT_Move;
        _RunBT_Wander = RunBT_Wander;
        __PreviousTask = _PreviousTask;
        _RunBT_InterruptCapture = RunBT_InterruptCapture;
        _KillChampionAggressiveState = KillChampionAggressiveState;
        _RunBT_RetreatFromEnemyCapturePoint = RunBT_RetreatFromEnemyCapturePoint;
        _RunBT_MidThreat = RunBT_MidThreat;
        _RunBT_NinjaCapturePoint = RunBT_NinjaCapturePoint;
        return result;
    }
}

