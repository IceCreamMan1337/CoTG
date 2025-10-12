namespace Spells
{
    public class ChronoShift : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 160f, 140f, 120f },
        };

        private readonly int[] baseHealthBoosts = { 600, 850, 1100 };
        private readonly int[] buffDurations = { 7, 7, 7 };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            float abilityPower = GetFlatMagicDamageMod(owner);
            float baseHealthBoost = baseHealthBoosts[level - 1];
            float totalHealthBoost = baseHealthBoost + (abilityPower * 0.2f);

            AddBuff(
                attacker,
                target,
                new Buffs.ChronoShift(totalHealthBoost, false),
                1,
                1,
                buffDurations[level - 1],
                BuffAddType.REPLACE_EXISTING,
                BuffType.COMBAT_ENCHANCER,
                0,
                true,
                false,
                false
            );
        }
    }
}

namespace Buffs
{
    public class ChronoShift : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "" },
            BuffName = "Chrono Shift",
            BuffTextureName = "Chronokeeper_Timetwister.dds",
            NonDispellable = true,
            OnPreDamagePriority = 4,
            PersistsThroughDeath = true,
        };

        private readonly float healthPlusAbility;
        private bool willRemove;
        private Particle effectParticle;

        public ChronoShift(float healthPlusAbility = default, bool willRemove = default)
        {
            this.healthPlusAbility = healthPlusAbility;
            this.willRemove = willRemove;
        }

        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out effectParticle, out _, "nickoftime_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(effectParticle);
        }

        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }

        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);

            if (currentHealth <= damageAmount)
            {
                damageAmount = currentHealth - 1; // Reduce damage to prevent death
                willRemove = true;

                if (!HasYorickBuffs(owner as ObjAIBase))
                {
                    AddBuff(GetBuffCasterUnit(), owner, new Buffs.ChronoRevive(healthPlusAbility), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }

        private bool HasYorickBuffs(ObjAIBase owner)
        {
            return GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombie)) > 0 ||
                   GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieLich)) > 0 ||
                   GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieKogMaw)) > 0 ||
                   GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAPetBuff2)) > 0;
        }
    }
}

namespace PreLoads
{
    public class ChronoShift : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("nickoftime_tar.troy");
            PreloadSpell("chronorevive");
        }
    }
}