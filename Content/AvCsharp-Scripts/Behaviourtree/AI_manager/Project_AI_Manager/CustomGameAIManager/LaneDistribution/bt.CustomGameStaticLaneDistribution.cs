
using AIScripts;

namespace BehaviourTrees.Map8;


class CustomGameStaticLaneDistributionClass : CommonAI
{
    private AssignToLaneClass assignToLane = new AssignToLaneClass();

    public bool CustomGameStaticLaneDistribution(
      out AISquadClass __Squad_PushBot,
      out AISquadClass __Squad_PushMid,
      out AISquadClass __Squad_PushTop,
      out int __Bot1Lane,
      out int __Bot2Lane,
      out int __Bot3Lane,
      out int __Bot4Lane,
    IEnumerable<AttackableUnit> AllEntities,
    AISquadClass Squad_PushBot,
    AISquadClass Squad_PushMid,
    AISquadClass Squad_PushTop,
    int Bot1Lane,
    int Bot2Lane,
    int Bot3Lane,
    int Bot4Lane
         )
      {
        AISquadClass _Squad_PushBot = Squad_PushBot;
        AISquadClass _Squad_PushMid = Squad_PushMid;
        AISquadClass _Squad_PushTop = Squad_PushTop;
        int _Bot1Lane = Bot1Lane;
        int _Bot2Lane = Bot2Lane;
        int _Bot3Lane = Bot3Lane;
        int _Bot4Lane = Bot4Lane;

        bool result = 
            // Sequence name :CustomGameStaticLaneDistribution
            (
                  SetVarInt(
                        out DistributionCount, 
                        0) &&
                  ForEach(AllEntities , Entity => (
                        // Sequence name :AssignToPushSquad
                        (
                              AddInt(
                                    out DistributionCount, 
                                    DistributionCount, 
                                    1) &&
                              // Sequence name :AssignToLane
                              (
                                    // Sequence name :Bot1
                                    (
                                          DistributionCount == 1 &&
                                          SetVarInt(
                                                out LaneID, 
                                                Bot1Lane)
                                    ) ||
                                    // Sequence name :Bot2
                                    (
                                          DistributionCount == 2 &&
                                          SetVarInt(
                                                out LaneID, 
                                                Bot2Lane)
                                    ) ||
                                    // Sequence name :Bot3
                                    (
                                          DistributionCount == 3 &&
                                          SetVarInt(
                                                out LaneID, 
                                                Bot3Lane)
                                    ) ||
                                    // Sequence name :Bot4
                                    (
                                          DistributionCount == 4 &&
                                          SetVarInt(
                                                out LaneID, 
                                                Bot4Lane)
                                    ) ||
                                    // Sequence name :Bot5
                                    (
                                          DistributionCount == 5 &&
                                          SetVarInt(
                                                out LaneID, 
                                                2)
                                    )
                              ) &&
                           assignToLane.   AssignToLane(
                                    Squad_PushTop, 
                                    Squad_PushMid, 
                                    Squad_PushBot, 
                                    Entity, 
                                    LaneID)

                        ))
                  )
            );

         __Squad_PushBot = _Squad_PushBot;
         __Squad_PushMid = _Squad_PushMid;
         __Squad_PushTop = _Squad_PushTop;
         __Bot1Lane = _Bot1Lane;
         __Bot2Lane = _Bot2Lane;
         __Bot3Lane = _Bot3Lane;
         __Bot4Lane = _Bot4Lane;
        return result;
      }
}

