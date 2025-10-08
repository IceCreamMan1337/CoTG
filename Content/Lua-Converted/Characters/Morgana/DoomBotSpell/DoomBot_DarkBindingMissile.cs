namespace Spells
{
    public class DoomBot_DarkBindingMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };

        private readonly int[] damageByLevel = { 80, 135, 190, 245, 300 };
        private readonly float[] snareDurationByLevel = { 2, 2.25f, 2.5f, 2.75f, 3 };

        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.DoomBot_DarkBindingMissile2(), 1, 1, 4.0f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }

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
            AddBuff(attacker, target, new Buffs.DoomBot_DarkBindingMissile(), 1, 1, snareDurationByLevel[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
        }
    }
}

namespace Buffs
{
    public class DoomBot_DarkBindingMissile : BuffScript
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


namespace Buffs
{
    public class DoomBot_DarkBindingMissile2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DarkBinding_tar.troy" },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[] { "game_floatingtext_Snared" },
        };

        float lastTimeExecuted;
        Particle gatlingEffect; // UNUSED
        int numberofexecution;

        float[] angleOffsets = [0, -15, -30, -45, -30, -15, 0, 15, 30, 45, 30, 15, 0, -15];
        public override void OnActivate()
        {
            numberofexecution = 0;
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.2f, ref lastTimeExecuted, true))
            {
                float angleOffset = angleOffsets[numberofexecution % angleOffsets.Length];
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DarkBindingMissile));
                var targetPos = GetPointByUnitFacingOffset(owner, 1150, angleOffset);
                var level = GetLevel((ObjAIBase)owner);
                SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                numberofexecution++;
                owner.RestorePAR(GetPARCost(Buff.OriginSpell));
            }
        }

        public override void OnDeactivate(bool expired)
        {
            numberofexecution = 0;
        }
    }
}
