namespace Spells
{
    public class DarkBindingMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };

        private readonly int[] damageByLevel = { 80, 135, 190, 245, 300 };
        private readonly float[] snareDurationByLevel = { 2, 2.25f, 2.5f, 2.75f, 3 };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            float damageAmount = damageByLevel[level - 1];
            if (ShouldDestroyMissile(target))
            {
                DestroyMissile(missileNetworkID);
                ApplyDamageAndBuff(target, damageAmount);
            }
        }

        private bool ShouldDestroyMissile(AttackableUnit target)
        {
            return !GetStealthed(target) || (target is Champion) || CanSeeTarget(owner, target);
        }

        private void ApplyDamageAndBuff(AttackableUnit target, float damageAmount)
        {
            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.9f, 1, false, false);
            AddBuff(attacker, target, new Buffs.DarkBindingMissile(), 1, 1, snareDurationByLevel[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
        }
    }
}

namespace Buffs
{
    public class DarkBindingMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DarkBinding_tar.troy" },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[] { "game_floatingtext_Snared" },
        };

        public override void OnActivate()
        {
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }

        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }

        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}