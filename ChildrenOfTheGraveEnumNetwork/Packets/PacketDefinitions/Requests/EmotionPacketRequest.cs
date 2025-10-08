using ChildrenOfTheGraveEnumNetwork.Packets.Enums;

namespace ChildrenOfTheGraveEnumNetwork.Packets.PacketDefinitions.Requests
{
    public class EmotionPacketRequest : ICoreRequest
    {
        public Emotions EmoteID;

        public EmotionPacketRequest(Emotions emoteId)
        {
            EmoteID = emoteId;
        }
    }
}
