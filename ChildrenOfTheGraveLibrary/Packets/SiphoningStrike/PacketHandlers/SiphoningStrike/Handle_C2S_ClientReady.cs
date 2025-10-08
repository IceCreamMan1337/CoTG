using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer;
using SiphoningStrike.Game;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGraveLibrary.Packets.PacketHandlers.SiphoningStrike;

public class Handle_C2S_ClientReady : PacketHandlerBase<C2S_ClientReady>
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
                var msg = "[ChildrenOfTheGrave] Chat commands are enabled in this game.";
                ChatManager.System(msg);
            }
            Game.PlayerManager.GetPeerInfo(userId).Champion.ScriptInternal.OnActivate();
        }


        return true;
    }
}