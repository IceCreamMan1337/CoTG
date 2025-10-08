using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class StaticLaneDistributionClass : AI_LaneDistribution
{
    private AssignToLaneClass assignToLane = new AssignToLaneClass();

    public bool StaticLaneDistribution(
              out AISquadClass Squad_PushBot,
      out AISquadClass Squad_PushMid,
      out AISquadClass Squad_PushTop,
      IEnumerable<AttackableUnit> AllEntities,
      int DisconnectAdjustmentEntityID,
      AISquadClass __Squad_PushBot,
      AISquadClass __Squad_PushMid,
      AISquadClass __Squad_PushTop
        )
      {

        AISquadClass _Squad_PushBot = __Squad_PushBot;
        AISquadClass _Squad_PushMid = __Squad_PushMid;
        AISquadClass _Squad_PushTop = __Squad_PushTop;

        bool result =
            // Sequence name :Sequence
            (
                  SetVarInt(
                        out DistributionCount, 
                        -1) &&
                        DebugAction("StaticLaneDistribution" + AllEntities.Count()) &&
                  ForEach(AllEntities ,Entity => (
                         // Sequence name :AssignToPushSquad
                         DebugAction("StaticLaneDistribution" + Entity.Model) &&
                        (
                              AddInt(
                                    out DistributionCount, 
                                    DistributionCount, 
                                    1) &&
                             /* NotEqualInt(
                                    DisconnectAdjustmentEntityID,  //todo understand this 
                                    DistributionCount) &&*/
                              ModulusInt(
                                    out LaneID, 
                                    DistributionCount, 
                                    3) &&
                              // Sequence name :Remap_LaneID
                              (
                                    // Sequence name :RemapLane1To2
                                    (
                                          LaneID == 1 &&
                                          SetVarInt(
                                                out LaneID, 
                                                2)
                                    ) ||
                                    // Sequence name :RemapLane2To1
                                    (
                                          LaneID == 2 &&
                                          SetVarInt(
                                                out LaneID, 
                                                1)
                                    ) ||
                                    LaneID == 0
                              ) &&
                                DebugAction("LaneID" + LaneID) &&
                              assignToLane.AssignToLane(
                                    __Squad_PushTop,
                                    __Squad_PushMid,
                                    __Squad_PushBot, 
                                    Entity, 
                                    LaneID)

                        ))
                  )
            );
        Squad_PushTop = __Squad_PushTop;
        Squad_PushMid = __Squad_PushMid;
        Squad_PushBot = __Squad_PushBot;
        return result;
      }
}

