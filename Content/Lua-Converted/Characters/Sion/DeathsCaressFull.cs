namespace Spells
{
    public class DeathsCaressFull : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };

        private readonly int[] baseArmorAmountByLevel = { 100, 150, 200, 250, 300 };
        private const float AbilityPowerRatio = 0.9f;
        private const float BuffDuration = 10f;

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            float abilityPower = GetFlatMagicDamageMod(target);
            float baseArmorAmount = baseArmorAmountByLevel[level - 1];
            float bonusHealthFromAP = abilityPower * AbilityPowerRatio;
            float totalArmorAmount = baseArmorAmount + bonusHealthFromAP;

            AddBuff(
                attacker,
                target,
                new Buffs.DeathsCaress(totalArmorAmount, totalArmorAmount, BuffDuration),
                1,
                1,
                BuffDuration,
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
    public class DeathsCaressFull : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DeathsCaress_buf.troy" },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "Death's Caress",
            BuffTextureName = "Sion_DeathsCaress.dds",
        };
    }
}
namespace PreLoads
{
    public class DeathsCaressFull : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("deathscaress");
        }
    }
}