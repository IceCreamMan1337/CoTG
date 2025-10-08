namespace Spells
{
    public class AstralBlessing : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };

        private readonly int[] baseHealByLevel = { 70, 140, 210, 280, 350 };
        private readonly int[] armorBonusByLevel = { 25, 50, 75, 100, 125 };
        private const float abilityPowerScaling = 0.45f;

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            float abilityPower = GetFlatMagicDamageMod(owner);
            float healAmount = baseHealByLevel[level - 1] + (abilityPower * abilityPowerScaling);
            float armorBonus = armorBonusByLevel[level - 1];

            AddBuff(attacker, target, new Buffs.AstralBlessing(armorBonus), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            IncHealth(target, healAmount, owner);
        }
    }
}

namespace Buffs
{
    public class AstralBlessing : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "soraka_astralBless_buf.troy" },
            BuffName = "Astral Blessing",
            BuffTextureName = "Soraka_Bless.dds",
        };

        private readonly float astralArmor;

        public AstralBlessing(float astralArmor = default)
        {
            this.astralArmor = astralArmor;
        }

        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
        }

        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, astralArmor);
        }
    }
}