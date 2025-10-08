namespace Spells
{
    public class Cannibalism : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };

        private readonly float[] lifestealPercents = { 0.5f, 0.75f, 1f };
        private readonly float[] healPercents = { 0.25f, 0.375f, 0.5f };
        private readonly float[] attackSpeedMods = { 0.5f, 0.5f, 0.5f };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            float lifestealPercent = lifestealPercents[level - 1];
            float healPercent = healPercents[level - 1];
            float attackSpeedMod = attackSpeedMods[level - 1];

            AddBuff(
                attacker,
                target,
                new Buffs.Cannibalism(healPercent, lifestealPercent, attackSpeedMod),
                1,
                1,
                20,
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
    public class Cannibalism : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Cannibalism_buf.troy" },
            BuffTextureName = "Sion_Cannibalism.dds",
        };

        private readonly float healPercent;
        private readonly float lifestealPercent;
        private readonly float attackSpeedMod;

        public Cannibalism(float healPercent = default, float lifestealPercent = default, float attackSpeedMod = default)
        {
            this.healPercent = healPercent;
            this.lifestealPercent = lifestealPercent;
            this.attackSpeedMod = attackSpeedMod;
        }

        public override void OnActivate()
        {
            // Initialize or set any additional state needed on activation
        }

        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, lifestealPercent);
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
        }

        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (target is ObjAIBase && target is not LaneTurret && target.Team != owner.Team)
            {
                ApplyEffectAndHeal(target, damageAmount * healPercent);
            }
        }

        private void ApplyEffectAndHeal(AttackableUnit target, float healAmount)
        {
            SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);

            foreach (AttackableUnit unit in GetUnitsInArea(
                (ObjAIBase)owner,
                owner.Position3D,
                350,
                SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf,
                default,
                true
            ))
            {
                if (GetHealthPercent(unit, PrimaryAbilityResourceType.MANA) < 1)
                {
                    IncHealth(unit, healAmount, owner);
                    ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                }
                SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, false, false, false, false);
            }
        }
    }
}
