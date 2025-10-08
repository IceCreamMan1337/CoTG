using CoTGEnumNetwork.Packets.Handlers;
using CoTG.CoTGServer;
using CrystalSlash.Game;
using static PacketVersioning.PktVersioning;

namespace CoTGLibrary.Packets.PacketHandlers.CrystalSlash;

public class Handle_C2S_ClientReady_106 : PacketHandlerBase<C2S_ClientReady>
{
    public override bool HandlePacket(int userId, C2S_ClientReady req)
    {
        if (Game.PlayerManager.AreAllPlayersReady())
        {
            GameStartNotify(userId);
            //Game.PacketNotifier.NotifyEnterTeamVision(Game.PlayerManager.GetPeerInfo((int)req.SenderNetID).Champion, userId);
            Game.StateHandler.Start();

            if (Game.Config.ChatCheatsEnabled)
            {
                var msg = "[CoTG] Chat commands are enabled in this game.";
                ChatManager.System(msg);
            }

        }


        return true;
    }
}