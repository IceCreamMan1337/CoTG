namespace Buffs
{
    public class Destiny_marker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Destiny Marker",
            BuffTextureName = "Destiny_temp.dds",
        };

        private readonly int[] spellCooldownByLevel = { 150, 135, 120 };

        public override void OnDeactivate(bool expired)
        {
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.Destiny));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float spellCooldown = spellCooldownByLevel[level - 1];
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
        }
    }
}