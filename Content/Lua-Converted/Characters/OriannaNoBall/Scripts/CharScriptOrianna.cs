namespace CharScripts
{
    public class CharScriptOrianna : CharScript
    {
        public override void OnActivate()
        {
            //bool ghostAlive; // UNUSED
            AddBuff(attacker, owner, new Buffs.OrianaSpellSword(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            //ghostAlive = false;
        }
        public override void OnResurrect()
        {
            AddBuff(owner, owner, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.GhostAlive = false;
            DestroyMissile(charVars.MissileID);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 1)
            {
                AddBuff(owner, owner, new Buffs.OrianaShockOrb(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace PreLoads
{
    public class CharScriptOrianna : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("yomuspellsword");
            PreloadSpell("yomughostself");
            PreloadSpell("apbonusdamagetotowers");
            PreloadSpell("yomushockorb");
        }
    }
}