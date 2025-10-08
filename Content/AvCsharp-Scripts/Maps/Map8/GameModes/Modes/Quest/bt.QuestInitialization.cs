using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class QuestInitializationClass : OdinLayout 
{


     public bool QuestInitialization(
      out float PreviousQuestCompleteTime,
      out int QuestID_Order,
      out int QuestID_Chaos,
      out int QuestObjective_Order,
      out int QuestObjective_Chaos,
      out int QuestGiver_EncounterID,
      out int QuestIconEncounterID,
      out int QuestIconSquadID_Order,
      out int QuestIconSquadID_Chaos,
      out int QuestIconEncounterID2,
      out int QuestRewardsID_Order,
      out int QuestRewardsID_Chaos
    )
    {

        float _PreviousQuestCompleteTime = default;
       int _QuestID_Order = default;
        int _QuestID_Chaos = default;
        int _QuestObjective_Order = default;
        int _QuestObjective_Chaos = default;
        int _QuestGiver_EncounterID = default;
        int _QuestIconEncounterID = default;
        int _QuestIconSquadID_Order = default;
        int _QuestIconSquadID_Chaos = default;
        int _QuestIconEncounterID2 = default;
        int _QuestRewardsID_Order = default;
        int _QuestRewardsID_Chaos = default;

bool result = 
            // Sequence name :Sequence
            (
                  SetVarFloat(
                        out _PreviousQuestCompleteTime, 
                        0) &&
                  SetVarInt(
                        out _QuestID_Order, 
                        -1) &&
                  SetVarInt(
                        out _QuestID_Chaos, 
                        -1) &&
                  SetVarInt(
                        out _QuestObjective_Chaos, 
                        -1) &&
                  SetVarInt(
                        out _QuestObjective_Order, 
                        -1) &&
                  CreateEncounterFromDefinition(
                        out _QuestGiver_EncounterID,
                        "OdinQuestBuffGiver"
                        
                        ) &&
                  CreateEncounterFromDefinition(
                        out _QuestIconEncounterID,
                        "OdinQuestIcon", 
                        1
                        ) &&
                  CreateEncounterFromDefinition(
                        out _QuestIconEncounterID2,
                        "OdinQuestIcon_Chaos", 
                        1
                        ) &&
                  SetVarInt(
                        out _QuestIconSquadID_Chaos, 
                        -1) &&
                  SetVarInt(
                        out _QuestIconSquadID_Order, 
                        -1) &&
                  SetVarInt(
                        out _QuestRewardsID_Order, 
                        -1) &&
                  SetVarInt(
                        out _QuestRewardsID_Chaos, 
                        -1)

            );
         PreviousQuestCompleteTime = _PreviousQuestCompleteTime;
         QuestID_Order = _QuestID_Order;
         QuestID_Chaos = _QuestID_Chaos;
         QuestObjective_Order = _QuestObjective_Order;
        QuestObjective_Chaos = _QuestObjective_Chaos;
         QuestGiver_EncounterID = _QuestGiver_EncounterID;
         QuestIconEncounterID = _QuestIconEncounterID;
         QuestIconSquadID_Order = _QuestIconSquadID_Order;
         QuestIconSquadID_Chaos = _QuestIconSquadID_Chaos;
         QuestIconEncounterID2 = _QuestIconEncounterID2;
         QuestRewardsID_Order = _QuestRewardsID_Order;
         QuestRewardsID_Chaos = _QuestRewardsID_Chaos;


        return result;
      }
}

