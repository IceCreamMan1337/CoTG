using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_OnReplication_Acc : PacketHandlerBase<C2S_OnReplication_Acc>
    {
        // this is an "accuser de reception" can be used for see if packet was not sended to someone 

        public override bool HandlePacket(int userId, C2S_OnReplication_Acc req)
        {

            if (PacketNotifier126._replicationAcknowledgments.ContainsKey(userId) && PacketNotifier126._replicationAcknowledgments[userId].ContainsKey(req.SyncID))
            {
                PacketNotifier126._replicationAcknowledgments[userId].Remove(req.SyncID);

                // Vérifier si tous les joueurs ont confirmé
                if (PacketNotifier126._replicationAcknowledgments[userId].Count == 0)
                {
                    PacketNotifier126._replicationAcknowledgments.Remove(userId);
                }
            }
            return true;
        }
    }
}
