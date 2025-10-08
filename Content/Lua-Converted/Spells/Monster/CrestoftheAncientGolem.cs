namespace Buffs
{
    public class CrestoftheAncientGolem : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "" },
            BuffName = "CrestoftheAncientGolem",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = true,
        };

        private Particle buffParticle;
        private const float CooldownReduction = 0.2f;
        private const float BaseManaRegen = 5f;
        private const float BuffDuration = 150f;
        private const float MonsterBuffMultiplier = 1.2f;
        private const float ManaRegenPercentage = 0.01f;

        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "NeutralMonster_buf_blue_defense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SetBuffToolTipVar(1, 20);
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }

        public override void OnUpdateStats()
        {
            if (owner is Champion)
            {
                ApplyCooldownReductionAndRegen(owner as ObjAIBase);
            }
        }

        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (attacker is Champion && !IsDead(attacker))
            {
                ApplyBuffWithPotentialExtension(attacker);
            }
            else if (IsPetWithAPBonus(attacker))
            {
                ObjAIBase caster = GetPetOwner((Pet)attacker);
                if (caster is Champion && !IsDead(caster))
                {
                    ApplyBuffWithPotentialExtension(caster);
                }
            }
        }

        private void ApplyCooldownReductionAndRegen(ObjAIBase target)
        {
            IncPercentCooldownMod(target, -CooldownReduction);

            float manaRegen = CalculateRegen(target, PrimaryAbilityResourceType.MANA);
            IncFlatPARRegenMod(target, BaseManaRegen + manaRegen, PrimaryAbilityResourceType.MANA);

            float energyRegen = CalculateRegen(target, PrimaryAbilityResourceType.Energy);
            IncFlatPARRegenMod(target, BaseManaRegen + energyRegen, PrimaryAbilityResourceType.Energy);
        }

        private float CalculateRegen(ObjAIBase target, PrimaryAbilityResourceType resourceType)
        {
            float maxResource = GetMaxPAR(target, resourceType);
            return maxResource * ManaRegenPercentage;
        }

        private void ApplyBuffWithPotentialExtension(ObjAIBase target)
        {
            float duration = BuffDuration;
            if (GetBuffCountFromCaster(target, target, nameof(Buffs.MonsterBuffs)) > 0)
            {
                duration *= MonsterBuffMultiplier;
            }
            AddBuff(target, target, new Buffs.CrestoftheAncientGolem(), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }

        private bool IsPetWithAPBonus(ObjAIBase unit)
        {
            return unit is Pet && GetBuffCountFromAll(unit, nameof(Buffs.APBonusDamageToTowers)) > 0;
        }
    }
}