namespace Buffs
{
    public class CrestOfNaturesFury : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Natures Fury",
            BuffTextureName = "PlantKing_AnimateVitalis.dds",
            NonDispellable = true,
        };

        private Particle buffParticle;
        private const float AttackSpeedIncrease = 0.2f;
        private const float CooldownReduction = -0.1f;
        private const float MonsterBuffDurationMultiplier = 1.2f;
        private const float BaseBuffDuration = 60f;

        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "regen_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            IncPermanentPercentAttackSpeedMod(owner, AttackSpeedIncrease);
            IncPermanentPercentCooldownMod(owner, CooldownReduction);
        }

        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentAttackSpeedMod(owner, -AttackSpeedIncrease);
            IncPermanentPercentCooldownMod(owner, -CooldownReduction);
            SpellEffectRemove(buffParticle);
        }

        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (attacker is Champion && !IsDead(attacker))
            {
                ApplyBuffWithPotentialExtension(attacker);
            }
            else if (IsPetWithBuff(attacker, nameof(Buffs.APBonusDamageToTowers)))
            {
                ObjAIBase caster = GetPetOwner((Pet)attacker);
                if (caster is Champion && !IsDead(caster))
                {
                    ApplyBuffWithPotentialExtension(caster);
                }
            }
        }

        private void ApplyBuffWithPotentialExtension(ObjAIBase target)
        {
            float duration = BaseBuffDuration;
            if (GetBuffCountFromCaster(target, target, nameof(Buffs.MonsterBuffs)) > 0)
            {
                duration *= MonsterBuffDurationMultiplier;
            }
            AddBuff(target, target, new Buffs.CrestOfNaturesFury(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }

        private bool IsPetWithBuff(ObjAIBase unit, string buffName)
        {
            return unit is Pet && GetBuffCountFromAll(unit, buffName) > 0;
        }
    }
}