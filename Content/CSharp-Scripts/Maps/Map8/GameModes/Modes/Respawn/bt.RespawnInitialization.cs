namespace BehaviourTrees;


class RespawnInitializationClass : OdinLayout
{


    public bool RespawnInitialization(
               out int ChaosRespawnMutator,
     out int OrderRespawnMutator,
     out int RespawnWindowAdjustment_Order,
     out int RespawnWindowAdjustment_Chaos)
    {
        int _ChaosRespawnMutator = default;
        int _OrderRespawnMutator = default;
        int _RespawnWindowAdjustment_Order = default;
        int _RespawnWindowAdjustment_Chaos = default;


        bool result =
                  // Sequence name :Sequence

                  SetVarInt(
                        out _ChaosRespawnMutator,
                        0) &&
                  SetVarInt(
                        out _OrderRespawnMutator,
                        0) &&
                  SetVarInt(
                        out _RespawnWindowAdjustment_Chaos,
                        0) &&
                  SetVarInt(
                        out _RespawnWindowAdjustment_Order,
                        0)

            ;
        ChaosRespawnMutator = _ChaosRespawnMutator;
        OrderRespawnMutator = _OrderRespawnMutator;
        RespawnWindowAdjustment_Chaos = _RespawnWindowAdjustment_Chaos;
        RespawnWindowAdjustment_Order = _RespawnWindowAdjustment_Order;


        return result;
    }
}

