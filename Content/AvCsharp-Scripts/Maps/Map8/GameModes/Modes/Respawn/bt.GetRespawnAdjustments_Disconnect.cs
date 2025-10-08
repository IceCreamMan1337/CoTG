using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GetRespawnAdjustments_DisconnectClass : OdinLayout 
{


      public bool GetRespawnAdjustments_Disconnect(
                out int RespawnModifier_Order,
      out int RespawnModifier_Chaos,
      out int RespawnWindowAdj_x10_Order,
      out int RespawnWindowAdj_x10_Chaos
          )
    {
        int _RespawnModifier_Order = default;
      int _RespawnModifier_Chaos = default;
        int _RespawnWindowAdj_x10_Order = default;
        int _RespawnWindowAdj_x10_Chaos
            = default;

bool result = 
            // Sequence name :Sequence
            (
                  GetNumberOfConnectedPlayers(
                        out NumConnectedPlayers_Order, 
                        TeamId.TEAM_ORDER) &&
                  GetNumberOfConnectedPlayers(
                        out NumConnectedPlayers_Chaos, 
                        TeamId.TEAM_CHAOS) &&
                  // Sequence name :Disconnect_MaxSpawn_Adjustment
                  (
                        // Sequence name :Order&gt;ChaosPlayers
                        (
                              GreaterInt(
                                    NumConnectedPlayers_Order, 
                                    NumConnectedPlayers_Chaos) &&
                              SetVarInt(
                                    out _RespawnModifier_Order, 
                                    0) &&
                              SetVarInt(
                                    out _RespawnModifier_Chaos, 
                                    2) &&
                              SetVarInt(
                                    out _RespawnWindowAdj_x10_Order, 
                                    0) &&
                              SetVarInt(
                                    out _RespawnWindowAdj_x10_Chaos, 
                                    10)
                        ) ||
                        // Sequence name :Chaos&gt;OrderPlayers
                        (
                              GreaterInt(
                                    NumConnectedPlayers_Chaos, 
                                    NumConnectedPlayers_Order) &&
                              SetVarInt(
                                    out _RespawnModifier_Order, 
                                    2) &&
                              SetVarInt(
                                    out _RespawnModifier_Chaos, 
                                    0) &&
                              SetVarInt(
                                    out _RespawnWindowAdj_x10_Order, 
                                    10) &&
                              SetVarInt(
                                    out _RespawnWindowAdj_x10_Chaos, 
                                    0)
                        ) ||
                        // Sequence name :Order==Chaos
                        (
                              SetVarInt(
                                    out _RespawnModifier_Order, 
                                    0) &&
                              SetVarInt(
                                    out _RespawnModifier_Chaos, 
                                    0) &&
                              SetVarInt(
                                    out _RespawnWindowAdj_x10_Order, 
                                    0) &&
                              SetVarInt(
                                    out _RespawnWindowAdj_x10_Chaos, 
                                    0)

                        )
                  )
            );
         RespawnModifier_Order = _RespawnModifier_Order;
         RespawnModifier_Chaos = _RespawnModifier_Chaos;
         RespawnWindowAdj_x10_Order = _RespawnWindowAdj_x10_Order;
        RespawnWindowAdj_x10_Chaos
           = _RespawnWindowAdj_x10_Chaos;

        return result;
      }
}

