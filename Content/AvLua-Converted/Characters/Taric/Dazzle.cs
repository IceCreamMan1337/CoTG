namespace Spells
{
    public class Dazzle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 12f, 11f, 10f, 9f, 8f },
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };

        private readonly int[] baseDamageByLevel = { 40, 70, 100, 130, 160 };
        private const float APScalingFactor = 0.4f;
        private const float MaxDamageMultiplier = 2f;
        private const float FullDamageRange = 250;

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            Vector3 ownerPos = GetUnitPosition(owner);
            Vector3 targetPos = GetUnitPosition(target);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            float abilityPower = GetFlatMagicDamageMod(owner) * APScalingFactor;
            float baseDamage = baseDamageByLevel[level - 1];
            float dazzleDamage = baseDamage + abilityPower;

            dazzleDamage *= CalculateDamageMultiplier(distance);

            ApplyStun(attacker, target, 1.5f);
            ApplyDamage(attacker, target, dazzleDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
        }

        private float CalculateDamageMultiplier(float distance)
        {
            float castRange = GetCastRange(owner, 2, SpellSlotType.SpellSlots);
            float varyingRange = castRange - FullDamageRange;

            if (distance < castRange)
            {
                float multiplier = (1 - (distance - FullDamageRange) / varyingRange) * (MaxDamageMultiplier - 1) + 1;
                return Math.Min(multiplier, MaxDamageMultiplier);
            }

            return 1;
        }
    }
}