namespace Spells
{
    public class BreathstealerSpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            float debuffMultiplier = -0.7f;

            // Calculate debuff amounts for ability power and physical damage
            float abilityPowerDebuff = debuffMultiplier * GetFlatMagicDamageMod(target);
            float bonusDamageDebuff = debuffMultiplier * GetFlatPhysicalDamageMod(target);
            float baseDamageDebuff = debuffMultiplier * GetBaseAttackDamage(target);

            // Apply the debuff as a buff on the target
            AddBuff(
                owner,
                target,
                new Buffs.BreathstealerSpell(abilityPowerDebuff, baseDamageDebuff, bonusDamageDebuff),
                1,
                1,
                4,
                BuffAddType.RENEW_EXISTING,
                BuffType.COMBAT_DEHANCER,
                0,
                true,
                false
            );
        }
    }
}

namespace Buffs
{
    public class BreathstealerSpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "head" },
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", "ItemBreathStealer_buf.troy" },
            BuffName = "Breathstealer",
            BuffTextureName = "3049_Prismatic_Sphere.dds",
        };

        private readonly float abilityPowerMod;
        private readonly float baseDamageMod;
        private readonly float bonusDamageMod;

        public BreathstealerSpell(float abilityPowerMod = default, float baseDamageMod = default, float bonusDamageMod = default)
        {
            this.abilityPowerMod = abilityPowerMod;
            this.baseDamageMod = baseDamageMod;
            this.bonusDamageMod = bonusDamageMod;
        }

        public override void OnActivate()
        {
            // Ensure variables are set, and possibly log them if needed
            // RequireVar(this.abilityPowerMod);
            // RequireVar(this.physicalDamageMod);
        }

        public override void OnUpdateStats()
        {
            // Apply the debuffs to the target
            IncFlatMagicDamageMod(owner, abilityPowerMod);
            IncFlatPhysicalDamageMod(owner, bonusDamageMod);
            IncFlatPhysicalDamageMod(owner, baseDamageMod);
        }
    }
}