using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_SwapItemReq : PacketHandlerBase<C2S_SwapItemReq>
    {
        public override bool HandlePacket(int userId, C2S_SwapItemReq req)
        {
            if (req.Source > 6 || req.Destination > 6)
            {
                return false;
            }

            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            champion.ItemInventory.SwapItems(req.Source, req.Destination);

            return true;
        }
    }
}
