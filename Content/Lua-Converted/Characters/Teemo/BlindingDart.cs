namespace Spells
{
    public class BlindingDart : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "AstronautTeemo" },
        };

        private readonly float[] blindDurations = { 1.5f, 1.75f, 2f, 2.25f, 2.5f };
        private readonly int[] damageByLevel = { 80, 125, 170, 215, 260 };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            float blindDuration = blindDurations[level - 1];
            int damageAmount = damageByLevel[level - 1];

            AddBuff(attacker, target, new Buffs.BlindingDart(), 100, 1, blindDuration, BuffAddType.STACKS_AND_OVERLAPS, BuffType.BLIND, 0, true, false);
            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
        }
    }
}

namespace Buffs
{
    public class BlindingDart : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head" },
            AutoBuffActivateEffect = new[] { "Global_miss.troy" },
            BuffName = "Blind",
            BuffTextureName = "Teemo_TranquilizingShot.dds",
            PopupMessage = new[] { "game_floatingtext_Blinded" },
        };

        public override void OnActivate()
        {
            ApplyBlindEffect();
        }

        public override void OnUpdateStats()
        {
            ApplyBlindEffect();
        }

        private void ApplyBlindEffect()
        {
            IncFlatMissChanceMod(owner, 1);
        }
    }
}
