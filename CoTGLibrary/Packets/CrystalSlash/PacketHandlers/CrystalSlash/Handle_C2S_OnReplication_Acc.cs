using CoTGEnumNetwork.Packets.Handlers;
using CrystalSlash.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_OnReplication_Acc_106 : PacketHandlerBase<C2S_OnReplication_Acc>
    {
        //at first view when client ask onreplication , we need send them  

        public override bool HandlePacket(int userId, C2S_OnReplication_Acc req)
        {


            return true;
        }
    }
}
