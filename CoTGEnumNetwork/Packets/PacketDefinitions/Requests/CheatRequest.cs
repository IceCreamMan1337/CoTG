
using CoTGEnumNetwork.Packets.Enums;
using SiphoningStrike.Game.Cheat;

namespace CoTGEnumNetwork.Packets.PacketDefinitions.Requests
{
    public class CheatRequest : ICoreRequest
    {
        public CheatIDEnum CheatID { get; }

        public CheatRequest(CheatID cheatID)
        {
            CheatID = (CheatIDEnum)cheatID;
        }
    }
}