using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GetRespawnAdjustments_NexusHealthClass : OdinLayout 
{


     public bool GetRespawnAdjustments_NexusHealth(
                out int RespawnModifier_Order,
      out int RespawnModifier_Chaos,
      out int RespawnWindowAdj_x10_Order,
      out int RespawnWindowAdj_x10_Chaos,
    float ScoreOrder,
    float ScoreChaos
          
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
                  SubtractFloat(
                        out PointsDiff, 
                        ScoreOrder, 
                        ScoreChaos) &&
                  NotEqualFloat(
                        PointsDiff, 
                        0) &&
                  AbsFloat(
                        out AbsPointDiff, 
                        PointsDiff) &&
                  DivideFloat(
                        out Modifier, 
                        AbsPointDiff, 
                        50) &&
                  FloorFloat(
                        out NewModifier, 
                        Modifier) &&
                  AddInt(
                        out NewModifier, 
                        NewModifier, 
                        2) &&
                  MinInt(
                        out NewModifier, 
                        NewModifier, 
                        2) &&
                  MultiplyInt(
                        out NegativeModifier, 
                        NewModifier, 
                        -1) &&
                  // Sequence name :Order_Chaos
                  (
                        // Sequence name :OrderAhead_ChaosBehind
                        (
                              GreaterEqualFloat(
                                    ScoreOrder, 
                                    ScoreChaos) &&
                              SetVarInt(
                                    out _RespawnModifier_Order, 
                                    NegativeModifier) &&
                              SetVarInt(
                                    out _RespawnModifier_Chaos, 
                                    NewModifier) &&
                              SetVarInt(
                                    out _RespawnWindowAdj_x10_Order, 
                                    0) &&
                              SetVarInt(
                                    out _RespawnWindowAdj_x10_Chaos, 
                                    NewModifier) &&
                              MultiplyInt(
                                    out _RespawnWindowAdj_x10_Chaos, 
                                    _RespawnWindowAdj_x10_Chaos, 
                                    10) &&
                              DivideInt(
                                    out _RespawnWindowAdj_x10_Chaos, 
                                    _RespawnWindowAdj_x10_Chaos, 
                                    2)
                        ) ||
                        // Sequence name :ChaosAhead_OrderBehind
                        (
                              LessFloat(
                                    ScoreOrder, 
                                    ScoreChaos) &&
                              SetVarInt(
                                    out _RespawnModifier_Order, 
                                    NewModifier) &&
                              SetVarInt(
                                    out _RespawnWindowAdj_x10_Order, 
                                    NewModifier) &&
                              MultiplyInt(
                                    out _RespawnWindowAdj_x10_Order, 
                                    _RespawnWindowAdj_x10_Order, 
                                    10) &&
                              DivideInt(
                                    out _RespawnWindowAdj_x10_Order, 
                                    _RespawnWindowAdj_x10_Order, 
                                    2) &&
                              SetVarInt(
                                    out _RespawnModifier_Chaos, 
                                    NegativeModifier) &&
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

