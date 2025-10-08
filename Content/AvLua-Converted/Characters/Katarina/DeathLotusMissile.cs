namespace Spells
{
    public class DeathLotusMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };

        private readonly int[] daggerBaseDamageByLevel = { 50, 65, 80 };
        private readonly int[] kiDamageByLevel = { 8, 12, 16, 20, 24 };
        private const float BonusDamageMultiplier = 0.5f;

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            float daggerBase = daggerBaseDamageByLevel[level - 1];
            int kiLevel = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float kiDamage = kiDamageByLevel[kiLevel - 1];

            float totalAttackDamage = GetTotalAttackDamage(owner);
            float baseAttackDamage = GetBaseAttackDamage(owner);
            float bonusAttackDamage = totalAttackDamage - baseAttackDamage;

            float bonusDamage = bonusAttackDamage * BonusDamageMultiplier;
            float totalDamage = daggerBase + bonusDamage + kiDamage;

            ApplyDamage(owner, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
        }
    }
}