namespace Spells
{
    public class KarthusDefile : Defile { }

    public class Defile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };

        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Defile)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Defile), owner, 0);
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.Defile(), 1, 1, 30000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}

namespace Buffs
{
    public class Defile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_weapon", "" },
            AutoBuffActivateEffect = new[] { "Defile_glow.troy", "" },
            BuffName = "Defile",
            BuffTextureName = "Lich_Defile.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };

        private Particle particle;
        private Particle particle2;
        private float lastTimeExecuted;
        private readonly int[] damageByLevel = { 30, 50, 70, 90, 110 };
        private readonly int[] manaCostByLevel = { 30, 42, 54, 66, 78 };

        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float initialDamage = damageByLevel[level - 1];

            ApplyAreaDamage(initialDamage);
            CreateSpellEffect();
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);

                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathDefiedBuff)) == 0)
                {
                    if (!ConsumeMana(level))
                    {
                        SpellBuffRemoveCurrent(owner);
                        return;
                    }
                }

                float periodicDamage = damageByLevel[level - 1];
                ApplyAreaDamage(periodicDamage);
            }
        }

        private void ApplyAreaDamage(float damageAmount)
        {
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
            }
        }

        private bool ConsumeMana(int level)
        {
            float manaCost = manaCostByLevel[level - 1];
            float currentMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);

            if (currentMana < manaCost)
            {
                return false;
            }

            IncPAR(owner, -manaCost, PrimaryAbilityResourceType.MANA);
            return true;
        }

        private void CreateSpellEffect()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out particle2, "Defile_green_cas.troy", "Defile_red_cas.troy", teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
    }
}