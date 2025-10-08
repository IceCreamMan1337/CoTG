using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class QuestActivationClass : OdinLayout 
{


    public bool QuestActivation(
    out int QuestID,
    int QuestIndex,
    bool IsCaptureOrDefend,
    TeamId QuestTeam
          
          )
      {


       int _QuestID = default;

bool result = 
            // Sequence name :CaptureOrDefend
            (
                  // Sequence name :Capture
                  (
                        IsCaptureOrDefend == true &&
                        // Sequence name :PointSelection
                        (
                              // Sequence name :PointA
                              (
                                    QuestIndex == 0 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_capture_a", 
                                          QuestTeam,
                                         QuestType.Primary, 
                                          false, 
                                          "", 
                                          "")
                              ) ||
                              // Sequence name :PointB
                              (
                                    QuestIndex == 1 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_capture_b", 
                                          QuestTeam,
                                          QuestType.Primary,
                                          false, 
                                          "", 
                                          "")
                              ) ||
                              // Sequence name :PointC
                              (
                                    QuestIndex == 2 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_capture_c", 
                                          QuestTeam,
                                          QuestType.Primary,
                                          false,
                                          "", 
                                          "")
                              ) ||
                              // Sequence name :PointD
                              (
                                    QuestIndex == 3 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_capture_d", 
                                          QuestTeam,
                                          QuestType.Primary,
                                          false,
                                          "", 
                                          "")
                              ) ||
                              // Sequence name :PointE
                              (
                                    QuestIndex == 4 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_capture_e", 
                                          QuestTeam,
                                          QuestType.Primary,
                                          false,
                                          "", 
                                          "")
                              )
                        )
                  ) ||
                  // Sequence name :Defend
                  (
                        IsCaptureOrDefend == false &&
                        // Sequence name :PointSelection
                        (
                              // Sequence name :PointA
                              (
                                    QuestIndex == 0 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_defend_a", 
                                          QuestTeam, 
                                          QuestType.Primary, false, 
                                          "", 
                                          "")
                              ) ||
                              // Sequence name :PointB
                              (
                                    QuestIndex == 1 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_defend_b", 
                                          QuestTeam, 
                                          QuestType.Primary, false, 
                                          "", 
                                          "")
                              ) ||
                              // Sequence name :PointC
                              (
                                    QuestIndex == 2 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_defend_c", 
                                          QuestTeam, 
                                          QuestType.Primary, false, 
                                          "", 
                                          "")
                              ) ||
                              // Sequence name :PointD
                              (
                                    QuestIndex == 3 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_defend_d", 
                                          QuestTeam, 
                                          QuestType.Primary, false, 
                                          "", 
                                          "")
                              ) ||
                              // Sequence name :PointE
                              (
                                    QuestIndex == 4 &&
                                    ActivateQuestForTeam(
                                          out _QuestID,
                                          "game_quest_defend_e", 
                                          QuestTeam, 
                                          QuestType.Primary, false, 
                                          "", 
                                          "")

                              )
                        )
                  )
            );

        QuestID = _QuestID;
        return result;
      }
}

