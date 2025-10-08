using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class RespawnManagerClass : OdinLayout 
{
    private GetRespawnAdjustments_NexusHealthClass getRespawnAdjustments_NexusHealth = new GetRespawnAdjustments_NexusHealthClass();

    private GetRespawnAdjustments_DisconnectClass getRespawnAdjustments_Disconnect = new GetRespawnAdjustments_DisconnectClass();

    private RespawnHelperClass respawnHelper = new RespawnHelperClass();

    private RespawnWindowAdjustment_DisconnectClass respawnWindowAdjustment_Disconnect = new RespawnWindowAdjustment_DisconnectClass();

    public bool RespawnManager(
               out int __OrderRespawnMutator,
      out int __ChaosRespawnMutator,
      out int __RespawnWindowAdjustment_Chaos,
      out int __RespawnWindowAdjustment_Order,
    int OrderRespawnMutator,
    int ChaosRespawnMutator,
    float ChaosPoints,
    float OrderPoints,
    int RespawnWindowAdjustment_Chaos,
    int RespawnWindowAdjustment_Order)
    {

        int _OrderRespawnMutator = OrderRespawnMutator;
       int _ChaosRespawnMutator = ChaosRespawnMutator;
        int _RespawnWindowAdjustment_Chaos = RespawnWindowAdjustment_Chaos;
       int _RespawnWindowAdjustment_Order = RespawnWindowAdjustment_Order;

bool result = 
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        getRespawnAdjustments_NexusHealth.GetRespawnAdjustments_NexusHealth(
                              out Cur_NH_RespawnMod_Order, 
                              out Cur_NH_RespawnMod_Chaos, 
                              out Cur_NH_WindowMod_x10_Order, 
                              out Cur_NH_WindowMod_x10_Chaos, 
                              OrderPoints, 
                              ChaosPoints) &&
                        getRespawnAdjustments_Disconnect.GetRespawnAdjustments_Disconnect(
                              out Cur_DC_RespawnMod_Order, 
                              out Cur_DC_RespawnMod_Chaos, 
                              out Cur_DC_WindowMod_x10_Order, 
                              out Cur_DC_WindowMod_x10_Chaos
                            ) &&
                        AddInt(
                              out TotalRespawnModOrder, 
                              Cur_DC_RespawnMod_Order, 
                              Cur_NH_RespawnMod_Order) &&
                        AddInt(
                              out TotalRespawnModChaos, 
                              Cur_DC_RespawnMod_Chaos, 
                              Cur_NH_RespawnMod_Chaos) &&
                        AddInt(
                              out TotalWindowModChaos, 
                              Cur_DC_WindowMod_x10_Chaos, 
                              Cur_NH_WindowMod_x10_Chaos) &&
                        AddInt(
                              out TotalWindowModOrder, 
                              Cur_DC_WindowMod_x10_Order, 
                              Cur_NH_WindowMod_x10_Order) &&
                        respawnHelper.RespawnHelper(
                              out _OrderRespawnMutator, 
                              TotalRespawnModOrder, 
                              OrderRespawnMutator, 
                              TeamId.TEAM_ORDER) &&
                         respawnHelper.RespawnHelper(
                              out _ChaosRespawnMutator, 
                              TotalRespawnModChaos, 
                              ChaosRespawnMutator, 
                              TeamId.TEAM_CHAOS) &&
                        respawnWindowAdjustment_Disconnect.RespawnWindowAdjustment_Disconnect(
                              out _RespawnWindowAdjustment_Order, 
                              RespawnWindowAdjustment_Order, 
                              TotalWindowModOrder, 
                              TeamId.TEAM_ORDER) &&
                          respawnWindowAdjustment_Disconnect.RespawnWindowAdjustment_Disconnect(
                              out _RespawnWindowAdjustment_Chaos, 
                              RespawnWindowAdjustment_Chaos, 
                              TotalRespawnModChaos, 
                              TeamId.TEAM_CHAOS)

                  )
                  ||
                               DebugAction("MaskFailure")
            );
         __OrderRespawnMutator = _OrderRespawnMutator;
         __ChaosRespawnMutator = _ChaosRespawnMutator;
         __RespawnWindowAdjustment_Chaos = _RespawnWindowAdjustment_Chaos;
         __RespawnWindowAdjustment_Order = _RespawnWindowAdjustment_Order;

        return result;
      }
}

