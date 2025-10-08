using CoTGEnumNetwork.Packets.Handlers;
using CoTG.CoTGServer.Content;
using CrystalSlash.Game;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.Packets.PacketHandlers;

public class Handle_C2S_Reconnect_106 : PacketHandlerBase<C2S_Reconnect>
{
    //TODO: Consider all possible cases and complete
    public override bool HandlePacket(int userId, C2S_Reconnect req)
    {
        var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
        peerInfo.IsStartedClient = true;
        peerInfo.IsDisconnected = false;
        Game.ObjectManager.OnReconnect(userId, peerInfo.Team, false);



        if (Game.Config.VersionOfClient == "1.0.0.106")
        {

            var info = Game.PlayerManager.GetPeerInfo(userId);
            var mapId = Game.Config.GameConfig.Map;
            SynchVersionNotify(
                userId, info.Team, [.. Game.PlayerManager.GetPlayers()], Config.VERSION_STRING,
                Game.Config.GameConfig.GameMode,
                ContentManager.GameFeatures,
                mapId,
                Game.Map.MutatorNames
            );
        }

        return true;



    }
}