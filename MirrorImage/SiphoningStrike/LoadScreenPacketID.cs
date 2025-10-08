using System;
using MirrorImage;
namespace 
SiphoningStrike
{
    public enum LoadScreenPacketID
    {
        RequestJoinTeam = 0x64,     // unused in replay ? 
        RequestReskin = 0x65,       // we skip 7 byte for get it work , possibly skipped important information 
        RequestRename = 0x66,       // we skip 7 byte for get it work , possibly skipped important information 
        TeamRosterUpdate = 0x67,    // Done  //complete
        Chat = 0x68, // unused in replay 
        egp_sendToServer = 0x69 // unused in replay 
    }
}
