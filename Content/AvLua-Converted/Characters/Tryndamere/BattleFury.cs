namespace Buffs
{
    public class BattleFury : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Battle Fury",
            BuffTextureName = "DarkChampion_Fury.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private float bonusCrit;
        private float furyPerHit = 5;
        private float furyPerCrit = 10;
        private float furyPerKill = 10;
        private float lastTimeExecuted2;
        private float lastTimeExecuted;

        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            if(attacker == null) return false;
            if (owner.Team != attacker.Team && IsDebuff(type))
            {
                AddRenektonInCombatBuff();
            }
            return true;
        }

        public override void OnActivate()
        {
            bonusCrit = 0;
        }

        public override void OnUpdateStats()
        {
            float fury = GetPAR(owner, PrimaryAbilityResourceType.Other);
            bonusCrit = 0.0035f * fury;
            IncFlatCritChanceMod(owner, bonusCrit);

            if (ExecutePeriodically(1, ref lastTimeExecuted2, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonInCombat)) == 0)
                {
                    IncPAR(owner, -5, PrimaryAbilityResourceType.Other);
                }
            }

            if (fury >= 3)
            {
                EnsureBloodlustParticleBuff();
            }
            else
            {
                RemoveBloodlustParticleBuff();
            }
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2, ref lastTimeExecuted, true))
            {
                UpdateBonusDamageTooltip();
            }
        }

        public override void OnKill(AttackableUnit target)
        {
            if (target is not LaneTurret && target is ObjAIBase)
            {
                IncPAR(owner, furyPerKill, PrimaryAbilityResourceType.Other);
            }
        }

        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            if (target is Champion)
            {
                TryResetSpellCooldown();
            }
        }

        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is not LaneTurret && target is ObjAIBase)
            {
                if (hitResult == HitResult.HIT_Critical)
                {
                    IncPAR(owner, furyPerCrit, PrimaryAbilityResourceType.Other);
                }
                else
                {
                    IncPAR(owner, furyPerHit, PrimaryAbilityResourceType.Other);
                }
            }
        }

        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddRenektonInCombatBuff();
        }

        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddRenektonInCombatBuff();
        }

        private void AddRenektonInCombatBuff()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }

        private void EnsureBloodlustParticleBuff()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BloodlustParticle)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BloodlustParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
            }
        }

        private void RemoveBloodlustParticleBuff()
        {
            SpellBuffRemove(owner, nameof(Buffs.BloodlustParticle), (ObjAIBase)owner, 0);
        }

        private void UpdateBonusDamageTooltip()
        {
            float totalAD = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusDamage = (totalAD - baseDamage) * 1.2f;
            float critDisplay = 100 * bonusCrit;
            SetBuffToolTipVar(1, critDisplay);
            SetSpellToolTipVar(bonusDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
        }

        private bool IsDebuff(BuffType type)
        {
            return type == BuffType.DAMAGE ||
                   type == BuffType.FEAR ||
                   type == BuffType.CHARM ||
                   type == BuffType.POLYMORPH ||
                   type == BuffType.SILENCE ||
                   type == BuffType.SLEEP ||
                   type == BuffType.SNARE ||
                   type == BuffType.STUN ||
                   type == BuffType.SLOW;
        }

        private void TryResetSpellCooldown()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (cooldown > 0)
                {
                    SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                }
            }
        }
    }
}