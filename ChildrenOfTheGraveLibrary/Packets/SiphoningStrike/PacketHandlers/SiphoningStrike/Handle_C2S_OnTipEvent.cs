using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_OnTipEvent : PacketHandlerBase<C2S_OnTipEvent>
    {
        public override bool HandlePacket(int userId, C2S_OnTipEvent req)
        {
            // TODO: can we use player net id from request?
            var playerNetId = Game.PlayerManager.GetPeerInfo(userId).Champion.NetId;
            HandleTipUpdateNotify(userId, "", "", "", 0, playerNetId, req.TipID);

            var msg = $"Clicked blue tip with netid: {req.TipID}";
            ChatManager.System(msg);
            return true;
        }
    }
}
