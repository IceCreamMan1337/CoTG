namespace Spells
{
    public class DrainChannel : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 5f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "SurprisePartyFiddlesticks", },
        };

        private Particle particleID;
        private float drainExecuted;
        private Particle glow;
        private Particle confetti;
        private readonly float[] drainPercentByLevel = { 0.6f, 0.65f, 0.7f, 0.75f, 0.8f };
        private readonly int[] baseDamageByLevel = { 30, 45, 60, 75, 90 };

        public override void ChannelingStart()
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float abilityPower = GetFlatMagicDamageMod(owner);
            AddBuff(owner, target, new Buffs.DrainChannel(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.Fearmonger_marker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.HEAL, 0, true, false, false);
            SpellEffectCreate(out particleID, out _, "Drain.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, target, "spine", default, false, false, false, false, false);
            drainExecuted = GetTime();

            float drainPercent = drainPercentByLevel[level - 1];
            bool drainedBool = false;
            AddBuff(owner, owner, new Buffs.GlobalDrain(drainPercent, drainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);

            float baseDamage = baseDamageByLevel[level - 1];
            float bonusDamage = abilityPower * 0.225f;
            float damageToDeal = bonusDamage + baseDamage;
            ApplyDamage(owner, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, owner);

            int fiddlesticksSkinID = GetSkinID(owner);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectCreate(out glow, out _, "Party_DrainGlow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, "spine", default, false, false, false, false, false);
                SpellEffectCreate(out confetti, out _, "Party_HornConfetti.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "BUFFBONE_CSTM_HORN", default, attacker, default, default, false, false, false, false, false);
            }
        }

        public override void ChannelingUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref drainExecuted, false))
            {
                float distance = DistanceBetweenObjects(target, owner);
                if (distance >= 650 || IsDead(target))
                {
                    StopChanneling(owner, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                }
                else if (!IsDead(owner))
                {
                    int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float drainPercent = drainPercentByLevel[level - 1];
                    bool drainedBool = false;
                    AddBuff(owner, owner, new Buffs.GlobalDrain(drainPercent, drainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);

                    float abilityPower = GetFlatMagicDamageMod(owner);
                    float baseDamage = baseDamageByLevel[level - 1];
                    float bonusDamage = abilityPower * 0.225f;
                    float damageToDeal = bonusDamage + baseDamage;
                    ApplyDamage(owner, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, attacker);
                }
            }
        }

        private void StopChannelingEffects()
        {
            if (target is not ObjAIBase)
            {
                SpellBuffRemove(target, nameof(Buffs.Drain), owner, 0);
            }
            SpellBuffRemove(owner, nameof(Buffs.Fearmonger_marker), owner, 0);
            SpellEffectRemove(particleID);

            int fiddlesticksSkinID = GetSkinID(owner);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(glow);
                SpellEffectRemove(confetti);
            }
        }

        public override void ChannelingSuccessStop() => StopChannelingEffects();

        public override void ChannelingCancelStop() => StopChannelingEffects();
    }
}

namespace Buffs
{
    public class DrainChannel : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Drain",
            BuffTextureName = "Fiddlesticks_ConjureScarecrow.dds",
            IsDeathRecapSource = true,
        };
    }
}