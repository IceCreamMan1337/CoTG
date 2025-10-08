using System;
using ChildrenOfTheGraveEnumNetwork.Packets.Enums;
using SiphoningStrike;

namespace PacketDefinitions126
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PacketType126 : Attribute
    {
        public GamePacketID GamePacketId { get; }
        public LoadScreenPacketID LoadScreenPacketId { get; }
        public Channel ChannelId { get; }

        public PacketType126(GamePacketID packetId, Channel channel)
        {
            GamePacketId = packetId;
            ChannelId = channel;
        }
        public PacketType126(GamePacketID packetId) : this(packetId, Channel.CHL_C2S) { }
        public PacketType126(LoadScreenPacketID packetId, Channel channel)
        {
            LoadScreenPacketId = packetId;
            ChannelId = channel;
        }
        public PacketType126(LoadScreenPacketID packetId) : this(packetId, Channel.CHL_LOADING_SCREEN) { }

        public PacketType126(Channel channel)
        {
            ChannelId = channel;
        }
    }
}
