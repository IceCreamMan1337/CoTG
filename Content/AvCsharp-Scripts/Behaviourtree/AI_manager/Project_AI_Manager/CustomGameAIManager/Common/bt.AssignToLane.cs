/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class AssignToLane : BehaviourTree 
{
      AISquad SquadTopLane;
      AISquad SquadMidLane;
      AISquad SquadBotLane;
      AttackableUnit EntityToAssign;
      int LaneToAssign;

      bool AssignToLane()
      {
      return
            // Sequence name :DistributionBasedOnID
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
            );
      }
}

*/