namespace Buffs
{
    public class BloodlustMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "" },
            BuffName = "Blood Lust",
            BuffTextureName = "DarkChampion_Bloodlust.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private readonly int[] baseDamageByLevel = { 5, 10, 15, 20, 25 };
        private readonly int[] bonusDamageByMissingHealth = { 15, 20, 25, 30, 35 };

        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = baseDamageByLevel[level - 1];
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            float missingHealthPercent = 1 - healthPercent;
            float additionalDamage = bonusDamageByMissingHealth[level - 1] * missingHealthPercent;
            float totalBonusDamage = baseDamage + additionalDamage;

            IncFlatPhysicalDamageMod(owner, totalBonusDamage);
        }
    }
}
