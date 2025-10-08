using CoTGEnumNetwork.Enums;

namespace CoTGEnumNetwork.Packets.PacketDefinitions.Requests
{
    public class QuestClickedRequest : ICoreRequest
    {
        public QuestEvent QuestEvent { get; }
        public uint QuestID { get; }
        public QuestClickedRequest(uint questNetId, QuestEvent questEvent)
        {
            QuestID = questNetId;
            QuestEvent = questEvent;
        }
    }
}
