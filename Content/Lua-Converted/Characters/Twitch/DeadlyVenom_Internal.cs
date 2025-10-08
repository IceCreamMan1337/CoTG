namespace Buffs
{
    public class TwitchDeadlyVenomMarker : DeadlyVenom_Internal { }

    public class DeadlyVenom_Internal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };

        private readonly float damageAmount;
        private int lastCount;
        private Particle particle;
        private float lastTimeExecuted;

        public DeadlyVenom_Internal(float damageAmount = default, int lastCount = default)
        {
            this.damageAmount = damageAmount;
            this.lastCount = lastCount;
        }

        public override void OnActivate()
        {
            SpellEffectCreate(out particle, out _, "twitch_poison_counter_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.DeadlyVenom)) > 0)
                {
                    int count = GetBuffCountFromAll(owner, nameof(Buffs.DeadlyVenom));
                    float damageToDeal = damageAmount * count;
                    ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, 0, false, false, attacker);
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }

        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.DeadlyVenom_marker)) > 0)
            {
                UpdateParticleEffect();
            }
        }

        private void UpdateParticleEffect()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.DeadlyVenom));
            string particleName = $"twitch_poison_counter_0{Math.Min(count, 6)}.troy";

            if (count != lastCount || count == 6)
            {
                SpellEffectRemove(particle);
                SpellEffectCreate(out particle, out _, particleName, default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
            }

            lastCount = count;
        }
    }
}