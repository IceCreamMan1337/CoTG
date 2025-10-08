using CoTGEnumNetwork.Packets.Handlers;
using CoTG.CoTGServer;
using CoTG.CoTGServer.Logging;
using log4net;
using SiphoningStrike.Game;

namespace CoTGLibrary.Packets.PacketHandlers.SiphoningStrike
{
    public class Handle_C2S_OnScoreBoardOpened : PacketHandlerBase<C2S_OnScoreBoardOpened>
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public override bool HandlePacket(int userId, C2S_OnScoreBoardOpened req)
        {
            _logger.Debug($"Player {Game.PlayerManager.GetPeerInfo(userId).Name} has looked at the scoreboard.");
            // Send to that player stats packet
            //var champion = _playerManager.GetPeerInfo(userId).Champion;

            //Game.PacketNotifier.NotifyS2C_HeroStats(champion);
            return true;
        }
    }
}
