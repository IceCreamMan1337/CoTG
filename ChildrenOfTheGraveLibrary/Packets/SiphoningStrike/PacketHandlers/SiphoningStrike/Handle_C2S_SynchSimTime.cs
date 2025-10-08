using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;
using SiphoningStrike.Game;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_SynchSimTime : PacketHandlerBase<C2S_SynchSimTime>
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public override bool HandlePacket(int userId, C2S_SynchSimTime req)
        {
            //Check this
            var diff = req.TimeLastServer - req.TimeLastClient;
            if (req.TimeLastClient > req.TimeLastServer)
            {
                var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
                var msg = $"Client {peerInfo.ClientId} sent an invalid heartbeat - Timestamp error (diff: {diff})";
                _logger.Warn(msg);
            }

            return true;
        }
    }
}
