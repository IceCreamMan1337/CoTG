using System.Collections.Generic;

namespace CoTG.CoTGServer.Inventory
{
    public class RuneInventory
    {
        public Dictionary<int, int> Runes { get; }

        public RuneInventory()
        {
            Runes = new Dictionary<int, int>();
        }

        public void Add(int runeSlotId, int runeId)
        {
            Runes.Add(runeSlotId, runeId);
        }
    }
}
