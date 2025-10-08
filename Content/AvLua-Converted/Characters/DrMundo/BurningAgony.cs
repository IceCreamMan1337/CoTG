namespace Spells
{
    public class BurningAgony : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };

        private readonly int[] healthCosts = { 20, 25, 30, 35, 40 };

        public override bool CanCast()
        {
            float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);

            if (level > 0)
            {
                float healthCost = healthCosts[level - 1];
                return currentHealth > healthCost;
            }

            return true;
        }

        public override float AdjustCooldown()
        {
            return GetBuffCountFromCaster(owner, owner, nameof(Buffs.BurningAgony)) == 0 ? 0 : float.NaN;
        }

        public override void SelfExecute()
        {
            string buffName = nameof(Buffs.BurningAgony);
            if (GetBuffCountFromCaster(owner, owner, buffName) > 0)
            {
                SpellBuffRemove(owner, buffName, owner, 0);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.BurningAgony(), 1, 1, 30000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}

namespace Buffs
{
    public class BurningAgony : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "root" },
            AutoBuffActivateEffect = new[] { "dr_mundo_burning_agony_cas_02.troy", "" },
            BuffName = "BurningAgony",
            BuffTextureName = "DrMundo_BurningAgony.dds",
            SpellToggleSlot = 2,
        };

        private float lastTimeExecuted;

        private readonly float[] durationModifiers = { 0.85f, 0.8f, 0.75f, 0.7f, 0.65f };
        private readonly int[] damageAmounts = { 40, 55, 70, 85, 100 };
        private readonly int[] healthCosts = { 10, 15, 20, 25, 30 };

        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float durationMod = durationModifiers[level - 1];

            if (owner.Team != attacker.Team)
            {
                switch (type)
                {
                    case BuffType.SNARE:
                    case BuffType.SLOW:
                    case BuffType.FEAR:
                    case BuffType.CHARM:
                    case BuffType.SLEEP:
                    case BuffType.STUN:
                    case BuffType.TAUNT:
                    case BuffType.SILENCE:
                    case BuffType.BLIND:
                        duration *= durationMod;
                        break;
                }

                duration = Math.Max(0.3f, duration);
            }

            return true;
        }

        public override void OnActivate()
        {
            ApplyDamageInArea();
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float healthCost = healthCosts[level - 1];
                float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);

                if (currentHealth < healthCost)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    IncHealth(owner, -healthCost, owner);
                    ApplyDamageInArea();
                }
            }
        }

        private void ApplyDamageInArea()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float damageAmount = damageAmounts[level - 1];
            var unitsInArea = GetUnitsInArea(
                (ObjAIBase)owner,
                owner.Position3D,
                325,
                SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes,
                default,
                true
            );

            foreach (var unit in unitsInArea)
            {
                ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
            }
        }
    }
}