using System.Collections.Generic;
using CoTGEnumNetwork.Enums;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.Tips

{
    public class TipParameters
    {
        public int PlayerNetworkID { get; }
        public uint ID { get; }
        public string Name { get; }
        public string Body { get; }
        public string ImagePath { get; }
        public string Tipcommand { get; } // found what is this 

        private static List<TipParameters> allTips = new();

        public TipParameters(int playerNetId, string tipName, string tipBody, string tipImgPath = "")
        {
            PlayerNetworkID = playerNetId;
            ID = NetIdManager.GenerateNewNetId();
            Name = tipName;
            Body = tipBody;
            ImagePath = ImagePath;

            allTips.Add(this);
        }


        public static void SendNormalTip(TipParameters tips, int userid, TipCommand tipCommand)
        {
            HandleTipUpdateNotify(userid, tips.Name, tips.Body, tips.ImagePath, (int)tipCommand, (uint)tips.PlayerNetworkID, tips.ID);
        }

        public static TipParameters FindTipByID(uint tipID)
        {
            return allTips.Find(tip => tip.ID == tipID);
        }

    }

}
