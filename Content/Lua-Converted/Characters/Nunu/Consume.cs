namespace Spells
{
    public class Consume : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 35f, 30f, 25f, 20f, 15f },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            SpellVOOverrideSkins = new[] { "NunuBot" },
        };

        private readonly int[] healAmounts = { 125, 180, 235, 290, 345 };
        private readonly float[] resistantDamage = { 200, 262.5f, 325, 387.5f, 450 };
        private readonly int[] normalDamage = { 400, 525, 650, 775, 900 };

        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);

            int levelIndex = level - 1;
            float healthIncrease = healAmounts[levelIndex];
            float abilityPower = GetFlatMagicDamageMod(owner);

            // Apply ability power scaling
            abilityPower *= 1;

            healthIncrease += abilityPower;
            IncHealth(owner, healthIncrease, owner);
        }

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            int levelIndex = level - 1;
            float damage = GetBuffCountFromCaster(target, target, nameof(Buffs.ResistantSkin)) > 0 ? resistantDamage[levelIndex] : normalDamage[levelIndex];

            ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
        }
    }
}

namespace Buffs
{
    public class Consume : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Consume_buf.troy" },
            BuffName = "Consume",
            BuffTextureName = "Yeti_Consume.dds",
        };

        private readonly float armorIncrease;

        public Consume(float armorIncrease = default)
        {
            this.armorIncrease = armorIncrease;
        }

        public override void UpdateBuffs()
        {
            IncFlatArmorMod(owner, armorIncrease);
        }
    }
}