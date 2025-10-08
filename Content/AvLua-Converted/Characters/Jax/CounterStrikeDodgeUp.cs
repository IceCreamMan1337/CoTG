namespace Buffs
{
    public class CounterStrikeDodgeUp : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CounterStrikeDodgeUp",
            BuffTextureName = "Armsmaster_Disarm.dds",
        };

        public override void OnUpdateStats()
        {
            // Get the level of the specific spell slot
            int spellLevel = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);

            // Calculate dodge modifier based on spell level
            float dodgeMod = 0.08f + (spellLevel * 0.02f);

            // Increase the dodge modifier for the owner
            IncFlatDodgeMod(owner, dodgeMod);

            // Check if the buff from the caster exists
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CounterStrikeCanCast)) == 0)
            {
                // Seal the spell slot if the buff does not exist
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
    }
}
