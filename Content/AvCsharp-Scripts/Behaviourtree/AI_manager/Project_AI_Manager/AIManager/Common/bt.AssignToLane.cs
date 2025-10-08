using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class AssignToLaneClass : CommonAI
{


    public bool AssignToLane(
    AISquadClass SquadTopLane,
    AISquadClass SquadMidLane,
    AISquadClass SquadBotLane,
    AttackableUnit EntityToAssign,
    int LaneToAssign)
      {
      return
            // Sequence name :DistributionBasedOnID
            (
             DebugAction("EntityToAssign = " + EntityToAssign + "to lane  " + LaneToAssign) &&
                  TestAIEntityHasTask(
                        EntityToAssign,
                        AITaskTopicType.ASSIST, 
                        null,
                        (Vector3)default)       ||
                        
                ( 
                  // Sequence name :Bot
                  (
                        LaneToAssign == 0 &&
                        AddAIEntityToSquad(
                              EntityToAssign, 
                              SquadBotLane)
                  ) ||
                  // Sequence name :Mid
                  (
                        LaneToAssign == 1 &&
                        AddAIEntityToSquad(
                              EntityToAssign, 
                              SquadMidLane)
                  ) ||
                  // Sequence name :Top
                  (
                        LaneToAssign == 2 &&
                        AddAIEntityToSquad(
                              EntityToAssign, 
                              SquadTopLane)

                  )
                  )
            );
      }
}

