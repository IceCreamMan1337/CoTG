using CoTGEnumNetwork.Packets.Handlers;
using CoTG.CoTGServer.Inventory;
using CrystalSlash.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_RemoveItemReq_106 : PacketHandlerBase<C2S_RemoveItemReq>
    {
        public override bool HandlePacket(int userId, C2S_RemoveItemReq req)
        {
            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            var inventory = champion.ItemInventory;
            Item item;
            return champion.ShopEnabled
                && (item = inventory.GetItem(req.Slot)) != null
                && inventory.SellItem(item);
        }
    }
}
