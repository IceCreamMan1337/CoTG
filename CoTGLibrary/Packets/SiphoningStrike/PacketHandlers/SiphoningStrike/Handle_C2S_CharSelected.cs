using CoTGEnumNetwork.Enums;
using CoTGEnumNetwork.Packets.Handlers;
using CoTG.CoTGServer.Logging;
using log4net;
using SiphoningStrike.Game;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_CharSelected : PacketHandlerBase<C2S_CharSelected>
    {
        private static ILog Logger = LoggerProvider.GetLogger();

        public override bool HandlePacket(int userId, C2S_CharSelected req)
        {
            // TODO : Look at how 131ClientSpawns
            Logger.Debug("Spawning map");

            var userInfo = Game.PlayerManager.GetPeerInfo(userId);
            StartSpawnNotify(userId, userInfo.Team, [.. Game.PlayerManager.GetPlayers(true)]);

            if (Game.StateHandler.State is GameState.GAMELOOP)
            {
                Game.ObjectManager.OnReconnect(userId, userInfo.Team, true);
            }
            else
            {
                // Temporary
                // Game.PacketNotifier.NotifyS2C_CreateHero(userInfo, userId, userInfo.Team, false);
                Game.ObjectManager.SpawnObjects(userInfo);
            }

            SpawnEndNotify(userId);
            return true;
        }
    }
}