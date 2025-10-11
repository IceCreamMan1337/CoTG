

namespace Chars
{
    public class CharScriptSivir : CharScript
    {
        float[] effect0 = { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f };
        public override void OnUpdateStats()
        {
            bool temp;
            temp = IsMoving(owner);
            if (temp)
            {
                IncFlatDodgeMod(owner, charVars.DodgeChance);
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.DodgeChance = this.effect0[level];
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName();
            if (spellName == nameof(Spells.SpiralBlade))
            {
                charVars.PercentOfAttack = 1;
            }
        }
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.FleetofFoot(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false);
        }
    }
}
namespace PreLoads
{
    public class CharScriptSivir : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("fleetoffoot");
            PreloadSpell("apbonusdamagetotowers");
            PreloadSpell("championchampiondelta");
        }
    }
}