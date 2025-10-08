using System;
using ChildrenOfTheGraveEnumNetwork.Packets.Enums;
using CrystalSlash;

namespace PacketDefinitions106
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PacketType106 : Attribute
    {
        public GamePacketID GamePacketId { get; }
        public LoadScreenPacketID LoadScreenPacketId { get; }
        public Channel ChannelId { get; }

        public PacketType106(GamePacketID packetId, Channel channel)
        {
            GamePacketId = packetId;
            ChannelId = channel;
        }
        public PacketType106(GamePacketID packetId) : this(packetId, Channel.CHL_C2S) { }
        public PacketType106(LoadScreenPacketID packetId, Channel channel)
        {
            LoadScreenPacketId = packetId;
            ChannelId = channel;
        }
        public PacketType106(LoadScreenPacketID packetId) : this(packetId, Channel.CHL_LOADING_SCREEN) { }

        public PacketType106(Channel channel)
        {
            ChannelId = channel;
        }
    }
}
