using CoTGEnumNetwork.Packets.Handlers;
using CoTG.CoTGServer;
using CrystalSlash.Game;

namespace CoTGLibrary.Packets.PacketHandlers.CrystalSlash;

public class Handle_C2S_BuyItemReq_106 : PacketHandlerBase<C2S_BuyItemReq>
{
    public override bool HandlePacket(int userId, C2S_BuyItemReq req)
    {
        var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
        return champion.ShopEnabled
            && !Game.Map.MapData.UnpurchasableItemList.Contains((int)req.ItemID)
            && champion.ItemInventory.BuyItem(req.ItemID);
    }
}