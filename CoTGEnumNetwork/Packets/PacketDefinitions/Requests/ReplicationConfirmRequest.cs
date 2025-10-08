namespace CoTGEnumNetwork.Packets.PacketDefinitions.Requests
{
    public class ReplicationConfirmRequest : ICoreRequest
    {
        public uint SyncID { get; }

        public ReplicationConfirmRequest(uint syncId)
        {
            SyncID = syncId;
        }
    }
}
