namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    //public class HandleUnpauseReq : PacketHandlerBase<UnpauseRequest>
    //{
    //    public override bool HandlePacket(int userId, UnpauseRequest req)
    //    {
    //        if (Game.StateHandler.State is not GameState.PAUSE)
    //        {
    //            return false;
    //        }

    //        var unpauser = Game.PlayerManager.GetPeerInfo(userId).Champion;

    //        foreach (var player in Game.PlayerManager.GetPlayers(false))
    //        {
    //            Game.PacketNotifier.NotifyResumePacket(unpauser, player, true);
    //        }
    //        var timer = new Timer
    //        {
    //            AutoReset = false,
    //            Enabled = true,
    //            Interval = 5000
    //        };
    //        timer.Elapsed += (sender, args) =>
    //        {
    //            foreach (var player in Game.PlayerManager.GetPlayers(false))
    //            {
    //                Game.PacketNotifier.NotifyResumePacket(unpauser, player, false);
    //            }
    //            Game.StateHandler.Unpause(unpauser);
    //        };
    //        return true;
    //    }
    //}
}