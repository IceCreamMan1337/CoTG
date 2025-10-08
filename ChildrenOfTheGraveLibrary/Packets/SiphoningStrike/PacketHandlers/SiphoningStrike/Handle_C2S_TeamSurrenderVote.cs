using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;
using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.API.ApiMapFunctionManager;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_TeamSurrenderVote : PacketHandlerBase<C2S_TeamSurrenderVote>
    {
        public override bool HandlePacket(int userId, C2S_TeamSurrenderVote req)
        {
            var c = Game.PlayerManager.GetPeerInfo(userId).Champion;
            HandleSurrender(userId, c, req.VotedYes);
            return true;
        }
    }
}
