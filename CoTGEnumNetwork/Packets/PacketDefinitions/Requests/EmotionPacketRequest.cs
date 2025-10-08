using CoTGEnumNetwork.Packets.Enums;

namespace CoTGEnumNetwork.Packets.PacketDefinitions.Requests
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
