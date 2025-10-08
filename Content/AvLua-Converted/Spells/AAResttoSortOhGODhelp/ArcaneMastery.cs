namespace Spells
{
    public class ArcaneMastery : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}

namespace Buffs
{
    public class ArcaneMastery : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Arcane Mastery",
            BuffTextureName = "Ryze_SpellStrike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private const float CooldownReduction = 1.0f;

        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                int currentSlot = GetSpellSlot(spell);
                for (int slot = 0; slot < 4; slot++)
                {
                    if (slot != currentSlot)
                    {
                        ReduceCooldown(slot);
                    }
                }
            }
        }

        private void ReduceCooldown(int slot)
        {
            float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, slot, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (cooldown > 0)
            {
                float newCooldown = Math.Max(0, cooldown - CooldownReduction);
                SetSlotSpellCooldownTimeVer2(newCooldown, slot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
        }
    }
}