namespace Spells
{
    public class DeathsCaress : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathsCaress)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.DeathsCaress), owner);
            }
            else
            {
                SpellCast(owner, owner, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
        }
    }
}

namespace Buffs
{
    public class DeathsCaress : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DeathsCaress_buf.troy" },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "Death's Caress",
            BuffTextureName = "Sion_DeathsCaress.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };

        private float totalShieldAmount;
        private float finalDamageAmount;
        private float lastExecutionTime;
        private float tickTimer;
        private float previousShieldAmount;

        private readonly int[] manaCostReduction = { -70, -80, -90, -100, -110 };

        public DeathsCaress(float totalShieldAmount = default, float finalDamageAmount = default, float tickTimer = default)
        {
            this.totalShieldAmount = totalShieldAmount;
            this.finalDamageAmount = finalDamageAmount;
            this.tickTimer = tickTimer;
        }

        public override void OnActivate()
        {
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DeathsCaress));
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 4);

            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float manaCostIncrement = manaCostReduction[level - 1];
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, manaCostIncrement, PrimaryAbilityResourceType.MANA);

            IncreaseShield(owner, totalShieldAmount, true, true);
        }

        public override void OnDeactivate(bool expired)
        {
            if (totalShieldAmount > 0)
            {
                RemoveShield(owner, totalShieldAmount, true, true);
                SpellEffectCreate(out _, out _, "DeathsCaress_nova.prt", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);

                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 525, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, finalDamageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
            }

            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DeathsCaressFull));

            float cooldownMultiplier = 1 + GetPercentCooldownMod(owner);
            float newCooldown = 8 * cooldownMultiplier;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }

        public override void OnUpdateStats()
        {
            SetBuffToolTipVar(1, totalShieldAmount);
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastExecutionTime, false))
            {
                tickTimer--;
                if (tickTimer < 4)
                {
                    Say(owner, " ", tickTimer);
                }
            }
        }

        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            previousShieldAmount = totalShieldAmount;

            if (totalShieldAmount >= damageAmount)
            {
                totalShieldAmount -= damageAmount;
                damageAmount = 0;
                ReduceShield(owner, previousShieldAmount - totalShieldAmount, true, true);
            }
            else
            {
                damageAmount -= totalShieldAmount;
                totalShieldAmount = 0;
                ReduceShield(owner, previousShieldAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}
namespace PreLoads
{
    public class DeathsCaress : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("deathscaress");
            PreloadParticle("deathscaress_nova.prt");
            PreloadSpell("deathscaressfull");
        }
    }
}