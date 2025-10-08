using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;
using SiphoningStrike.Game;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_PlayEmote : PacketHandlerBase<C2S_PlayEmote>
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public override bool HandlePacket(int userId, C2S_PlayEmote req)
        {
            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            champion.StopMovement();
            champion.UpdateMoveOrder(OrderType.Taunt);
            //for later use -> tracking, etc.
            var playerName = Game.PlayerManager.GetPeerInfo(userId).Champion.Model;
            switch ((Emotions)req.EmoteID)
            {
                case Emotions.DANCE:
                    _logger.Debug("Player " + playerName + " is dancing.");
                    break;
                case Emotions.TAUNT:
                    _logger.Debug("Player " + playerName + " is taunting.");
                    break;
                case Emotions.LAUGH:
                    _logger.Debug("Player " + playerName + " is laughing.");
                    break;
                case Emotions.JOKE:
                    _logger.Debug("Player " + playerName + " is joking.");
                    break;
            }

            S2C_PlayEmoteNotify((Emotions)req.EmoteID, champion.NetId);
            return true;
        }
    }
}
