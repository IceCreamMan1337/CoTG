using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class IssueEventClass : AI_Characters 
{
    

     public bool IssueEvent(
           out string __PreviousActionPerformed,
      out float __LastIssuedEventTime,
      string ActionPerformed,
      string PreviousActionPerformed,
      AttackableUnit Self,
      AttackableUnit EventTarget,
      float LastIssuedEventTime
         )
      {

        string _PreviousActionPerformed = PreviousActionPerformed;
        float _LastIssuedEventTime = LastIssuedEventTime;
        bool result =
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Conditions
                              (
                                    // Sequence name :NewActionPerformed
                                    (
                                          NotEqualString(
                                                ActionPerformed,
                                                PreviousActionPerformed) &&
                                          // Sequence name :EventType
                                          (
                                                // Sequence name :KillChampion/HighThreatManagement
                                                (
                                                      // Sequence name :Conditions
                                                      (
                                                                                              
                                                      // Sequence name :KillChampion
                                                            (
                                                                  ActionPerformed == "KillChampion" &&
                                                                  NotEqualString(
                                                                        PreviousActionPerformed,
                                                                        "HighThreatManagement") &&
                                                                  TestAIEntityHasTask(
                                                                        Self,
                                                                       AITaskTopicType.ASSIST,
                                                                        default,
                                                                        default,
                                                                        default,
                                                                        false)
                                                            ) ||
                                                            // Sequence name :HighThreatManagement
                                                            (
                                                                  ActionPerformed == "HighThreatManagement" &&
                                                                  NotEqualString(
                                                                        PreviousActionPerformed,
                                                                        "KillChampion") &&
                                                                  GetUnitPosition(
                                                                        out SelfPosition,
                                                                        Self) &&
                                                                  GetUnitAIClosestTargetInArea(
                                                                        out EventTarget,
                                                                        Self,
                                                                        default,
                                                                        true,
                                                                        SelfPosition,
                                                                        800,
                                                                        AffectEnemies | AffectHeroes)
                                                            )
                                                      ) &&
                                                      SendMessageToManager(
                                                            Self,
                                                            EventTarget,
                                                           AITaskTopicType.ASSIST,
                                                            "Help") &&
                                                      GetGameTime(
                                                            out LastIssuedEventTime)
                                                ) ||
                                                // Sequence name :Don'tNeedHelpAnymore
                                                (
                                                      // Sequence name :Conditions
                                                      (
                                                            // Sequence name :KillChampion
                                                            (
                                                                  PreviousActionPerformed == "KillChampion" &&
                                                                  NotEqualString(
                                                                        ActionPerformed,
                                                                        "HighThreatManagement")
                                                            ) ||
                                                            // Sequence name :HighThreatManagement
                                                            (
                                                                  PreviousActionPerformed == "HighThreatManagement" &&
                                                                  NotEqualString(
                                                                        ActionPerformed,
                                                                        "KillChampion")
                                                            )
                                                      ) &&
                                                      SendMessageToManager(
                                                            Self,
                                                            Self,
                                                            AITaskTopicType.DONE,
                                                            "Done")
                                                )
                                          )
                                    ) ||
                                    // Sequence name :StillInKillChampionOrHighThreat
                                    (
                                          // Sequence name :ActionPerformed
                                          (
                                                ActionPerformed == "KillChampion"
                                                ||
                                                ActionPerformed == "HighThreatManagement"
                                          ) &&
                                          GetGameTime(
                                                out CurrentGameTime) &&
                                          SubtractFloat(
                                                out TimeDiff,
                                                CurrentGameTime,
                                                LastIssuedEventTime) &&
                                          GreaterFloat(
                                                TimeDiff,
                                                2) &&
                                          // Sequence name :Conditions
                                          (
                                                // Sequence name :KillChampion
                                                (
                                                      ActionPerformed == "KillChampion" &&
                                                      TestAIEntityHasTask(
                                                            Self,
                                                            AITaskTopicType.ASSIST,
                                                            default,
                                                           default,
                                                          default,
                                                            false)
                                                ) ||
                                                // Sequence name :HighThreatManagement
                                                (
                                                     ActionPerformed == "HighThreatManagement" &&
                                                      GetUnitPosition(
                                                            out SelfPosition,
                                                            Self) &&
                                                      GetUnitAIClosestTargetInArea(
                                                            out EventTarget,
                                                            Self,
                                                            default,
                                                            true,
                                                            SelfPosition,
                                                            800,
                                                            AffectEnemies | AffectHeroes)
                                                )
                                          ) &&
                                          SendMessageToManager(
                                                Self,
                                                EventTarget,
                                                AITaskTopicType.ASSIST,
                                                "Help") &&
                                          GetGameTime(
                                                out LastIssuedEventTime)
                                    ) ||
                                    // Sequence name :PushingButInAssistLogic
                                    (
                                          ActionPerformed == "PushLane" &&
                                          TestAIEntityHasTask(
                                                Self,
                                              AITaskTopicType.ASSIST,
                                                default,
                                               default,
                                               default,
                                                true) &&
                                          SendMessageToManager(
                                                Self,
                                                Self,
                                               AITaskTopicType.PUSH,
                                                "Push")
                                    )
                              ) || MaskFailure()
                        ) &&
                        SetVarString(
                              out _PreviousActionPerformed,
                              ActionPerformed)

                  )
                   || MaskFailure()
            );

         __PreviousActionPerformed = _PreviousActionPerformed;
         __LastIssuedEventTime = _LastIssuedEventTime;
        return result;
    }
}

