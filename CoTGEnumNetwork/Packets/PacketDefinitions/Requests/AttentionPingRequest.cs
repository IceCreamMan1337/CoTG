using System.Numerics;
using CoTGEnumNetwork.Packets.Enums;

namespace CoTGEnumNetwork.Packets.PacketDefinitions.Requests
{
    public class AttentionPingRequest : ICoreRequest
    {
        public Vector2 Position { get; }
        public uint TargetNetID { get; }
        public Pings PingCategory { get; }

        public AttentionPingRequest(Vector2 position, uint targetNetId, Pings type)
        {
            Position = position;
            TargetNetID = targetNetId;
            PingCategory = type;
        }
    }
}
