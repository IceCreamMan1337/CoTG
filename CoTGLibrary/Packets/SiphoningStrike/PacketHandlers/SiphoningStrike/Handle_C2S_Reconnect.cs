using CoTGEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers;

public class Handle_C2S_Reconnect : PacketHandlerBase<C2S_Reconnect>
{
    //TODO: Consider all possible cases and complete
    public override bool HandlePacket(int userId, C2S_Reconnect req)
    {
        var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
        peerInfo.IsStartedClient = true;
        peerInfo.IsDisconnected = false;
        Game.ObjectManager.OnReconnect(userId, peerInfo.Team, false);
        var info = Game.PlayerManager.GetPeerInfo(userId);
        var mapId = Game.Config.GameConfig.Map;
        /*  SynchVersionNotify(
              userId, info.Team, Game.PlayerManager.GetPlayers(), Config.VERSION_STRING,
              Game.Config.GameConfig.GameMode,
              ContentManager.GameFeatures,
              mapId,
              Game.Map.MutatorNames
          ); */

        return true;
    }
}