using CoTGEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_OnQuestEvent : PacketHandlerBase<C2S_OnQuestEvent>
    {
        public override bool HandlePacket(int userId, C2S_OnQuestEvent req)
        {
            var msg = $"Clicked quest with netid: {req.QuestID}";
            ChatManager.System(msg);
            return true;
        }
    }
}
