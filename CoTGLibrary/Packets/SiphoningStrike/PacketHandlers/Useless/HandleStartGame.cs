/*
namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class HandleStartGame : PacketHandlerBase<C2S_ClientReady>
    {
        public override bool HandlePacket(int userId, C2S_ClientReady req)
        {
            var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
            peerInfo.IsDisconnected = false;

            if (Game.StateHandler.State is GameState.GAMELOOP)
            {
                StartFor(peerInfo);
                return true;
            }

            TryStart();
            return true;
        }

        private static void TryStart()
        {
            var players = Game.PlayerManager.GetPlayers(false);

            var isPossibleToStart = players.All(p => !p.IsDisconnected);

            if (!isPossibleToStart)
            {
                return;
            }

            foreach (var player in players)
            {
                if (!player.IsDisconnected)
                {
                    StartFor(player);
                }
            }

            Game.StateHandler.Start();
        }

        private static void StartFor(ClientInfo player)
        {
            if (Game.StateHandler.State is GameState.PAUSE)
            {
                Game.PacketNotifier.NotifyPausePacket(player, Game.StateHandler.GetPauseTimeLeft(), true);
            }

            Game.PacketNotifier.NotifyGameStart(player.ClientId);

            if (Game.StateHandler.State is GameState.GAMELOOP)
            {
                //var announcement = new OnReconnect { OtherNetID = player.Champion.NetId };
                //Game.PacketNotifier.NotifyS2C_OnEventWorld(announcement, player.Champion);
            }

            if (!player.IsMatchingVersion)
            {
                ChatManager.System(
                    player.ClientId,
                    "Your client version does not match the server. Check the server log for more information."
                );
            }

            /*
            // TODO: send this in one place only
            Game.PacketNotifier.NotifyS2C_HandleTipUpdate(player.ClientId,
                "Welcome to League Sandbox!", "This is a WIP project.",
                "", 0, player.Champion.NetId, Game.NetworkIdManager.GetNewNetId());
            Game.PacketNotifier.NotifyS2C_HandleTipUpdate(player.ClientId,
                "Server Build Date", ServerContext.BuildDateString,
                "", 0, player.Champion.NetId, Game.NetworkIdManager.GetNewNetId());
            Game.PacketNotifier.NotifyS2C_HandleTipUpdate(player.ClientId,
                "Your Champion:", player.Champion.Model,
                "", 0, player.Champion.NetId, Game.NetworkIdManager.GetNewNetId());
            */
/*
         Game.PacketNotifier.NotifySynchSimTimeS2C(player.ClientId, Game.Time.GameTime);
         Game.PacketNotifier.NotifySyncMissionStartTimeS2C(player.ClientId, Game.Time.GameTime);
     }
 }
}*/