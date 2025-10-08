/*namespace Buffs
{
    internal class AscRespawn : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };
        public override StatsModifier StatsModifier { get; protected set; } = new();

        public override void OnActivate()
        {
            if (Target is ObjAIBase obj && obj.ItemInventory != null)
            {
                AddBuff("AscTrinketStartingCD", 0.3f, 1, null, Target, obj);
                ApiEventManager.OnResurrect.AddListener(this, obj, OnRespawn, false);
            }
        }

        public void OnRespawn(ObjAIBase owner)
        {
            owner.Spells[6 + (byte)SpellSlotType.InventorySlots].SetCooldown(0);
        }
    }
}*/