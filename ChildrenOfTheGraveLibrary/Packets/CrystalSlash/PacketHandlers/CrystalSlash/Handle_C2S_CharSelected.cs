using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;
using CrystalSlash.Game;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_CharSelected_106 : PacketHandlerBase<C2S_CharSelected>
    {
        private static ILog Logger = LoggerProvider.GetLogger();

        public override bool HandlePacket(int userId, C2S_CharSelected req)
        {
            // TODO : Look at how 131ClientSpawns
            Logger.Debug("Spawning map");

            var userInfo = Game.PlayerManager.GetPeerInfo(userId);
            var players = Game.PlayerManager.GetPlayers(true);
            StartSpawnNotify(userId, userInfo.Team, [.. players]);

            if (Game.StateHandler.State is GameState.GAMELOOP)
            {
                Game.ObjectManager.OnReconnect(userId, userInfo.Team, true);
            }
            else
            {
                // Temporary
                // Game.PacketNotifier126.NotifyS2C_CreateHero(userInfo, userId, userInfo.Team, false);
                Game.ObjectManager.SpawnObjects(userInfo);
            }

            SpawnEndNotify(userId);
            return true;
        }
    }
}