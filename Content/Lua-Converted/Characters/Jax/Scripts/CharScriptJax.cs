
using Buffs;

namespace CharScripts
{
    public class CharScriptJax : CharScript
    {
        float[] FlatBonusMR = [ 20.0f, 20.0f, 35.0f, 50.0f ];
        public override void OnUpdateActions()
        {
            int level;
            AddBuff(owner, owner, new EquipmentMastery(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            
            if (!HasBuff(owner, "CounterStrikeDodgeUp"))
            {
                level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    AddBuff(owner, owner, new CounterStrikeDodgeUp(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
                }
            }

            level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (!HasBuff(owner, "RelentlessAssault") && level > 0)
            {
                AddBuff(owner, owner, new RelentlessAssault(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
            }

            float attackDamage = GetFlatPhysicalDamageMod(owner) * 0.4f;
            SetSpellToolTipVar(attackDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);

            float dodge = GetDodge(owner);
            level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //?
            float bonusMR = dodge * 100.0f + FlatBonusMR[level];
            charVars.BonusMR = FlatBonusMR[level];

            SetSpellToolTipVar(bonusMR, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.NumSwings = 0;
            charVars.LastHitTime = 0;
            SealSpellSlot(2, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnDodge(AttackableUnit attacker)
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                AddBuff(owner, owner, new CounterStrikeCanCast(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false);
            }
        }
    }
}
namespace PreLoads
{
    public class CharScriptJax : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("equipmentmastery");
            PreloadSpell("apbonusdamagetotowers");
            PreloadSpell("counterstrikedodgeup");
            PreloadSpell("relentlessassault");
            PreloadSpell("counterstrikecancast");
            PreloadSpell("championchampiondelta");
        }
    }
}