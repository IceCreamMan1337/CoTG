namespace Buffs
{
    public class Backstab : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Backstab",
            BuffTextureName = "Jester_CarefulStrikes.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private readonly float[] moveSpeedDebuff = { -0.2f, -0.225f, -0.25f, -0.275f, -0.3f };
        private readonly float[] missChanceIncrease = { 0.2f, 0.225f, 0.25f, 0.275f, 0.3f };

        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellName == nameof(Spells.TwoShivPoison) && IsInFront(owner, target) && IsBehind(target, owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CastFromBehind(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }

        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not LaneTurret)
            {
                if (IsBackstabConditionMet())
                {
                    ApplyBackstabEffect(target, ref damageAmount);
                }

                TryApplyTwoShivPoison(target, hitResult);
            }
        }

        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is ObjAIBase && target is not LaneTurret && IsInFront(owner, target) && IsBehind(target, owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.FromBehind(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }

        private bool IsBackstabConditionMet()
        {
            return GetBuffCountFromCaster(owner, owner, nameof(Buffs.FromBehind)) > 0 || (IsInFront(owner, target) && IsBehind(target, owner));
        }

        private void ApplyBackstabEffect(AttackableUnit target, ref float damageAmount)
        {
            damageAmount *= 1.2f;
            SpellEffectCreate(out _, out _, "AbsoluteZero_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
        }

        private void TryApplyTwoShivPoison(AttackableUnit target, HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            ObjAIBase attacker = GetChampionBySkinName("Shaco", teamID);
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float time = GetSlotSpellCooldownTime(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);

            if (level >= 1 && time <= 0 && hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                float moveSpeedMod = moveSpeedDebuff[level - 1];
                float missChanceMod = missChanceIncrease[level - 1];
                AddBuff(attacker, target, new Buffs.TwoShivPoison(moveSpeedMod, missChanceMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}
namespace PreLoads
{
    public class Backstab : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("castfrombehind");
            PreloadSpell("frombehind");
            PreloadParticle("absolutezero_tar.troy");
            PreloadSpell("twoshivpoison");
        }
    }
}