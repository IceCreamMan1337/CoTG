using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using CrystalSlash.Game;
using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.API.ApiMapFunctionManager;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_TeamSurrenderVote_106 : PacketHandlerBase<C2S_TeamSurrenderVote>
    {
        public override bool HandlePacket(int userId, C2S_TeamSurrenderVote req)
        {
            var c = Game.PlayerManager.GetPeerInfo(userId).Champion;
            HandleSurrender(userId, c, req.VotedYes);
            return true;
        }
    }
}
