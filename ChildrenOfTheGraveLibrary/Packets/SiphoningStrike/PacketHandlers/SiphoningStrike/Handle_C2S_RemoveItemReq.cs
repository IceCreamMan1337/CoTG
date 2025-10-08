using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Inventory;
using SiphoningStrike.Game;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_RemoveItemReq : PacketHandlerBase<C2S_RemoveItemReq>
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
