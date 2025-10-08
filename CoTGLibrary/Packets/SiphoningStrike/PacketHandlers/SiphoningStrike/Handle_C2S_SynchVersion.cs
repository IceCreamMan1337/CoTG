using CoTGEnumNetwork.Packets.Handlers;
using CoTG.CoTGServer.Content;
using CoTG.CoTGServer.Logging;
using log4net;
using SiphoningStrike.Game;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_SynchVersion : PacketHandlerBase<C2S_SynchVersion>
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public override bool HandlePacket(int userId, C2S_SynchVersion req)
        {
            //Logging->writeLine("Client version: %s", version->version);

            var mapId = Game.Config.GameConfig.Map;
            _logger.Debug("Current map: " + mapId);

            var info = Game.PlayerManager.GetPeerInfo(userId);
            var versionMatch = true;
            info.IsMatchingVersion = versionMatch;

            // Version might be an invalid value, currently it trusts the client
            if (!versionMatch)
            {
                // _logger.Warn($"Client's version ({req.Version}) does not match server's {Config.VERSION}");
            }
            else
            {
                _logger.DebugFormat("Accepted client version {0} from client = {1} & PlayerID = {2}", req.VersionString, req.ClientID, info.PlayerId);
            }

            SynchVersionNotify(
                userId, info.Team, [.. Game.PlayerManager.GetPlayers()], Config.VERSION_STRING,
                Game.Config.GameConfig.GameMode,
                ContentManager.GameFeatures,
                mapId,
                Game.Map.MutatorNames
            );

            return true;

        }
    }
}
