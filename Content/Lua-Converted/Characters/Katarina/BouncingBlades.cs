namespace Spells
{
    public class BouncingBlades : SpellScript
    {
        private float remainingBounces = 2; // Used to track remaining bounces in a spell chain
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHitsByLevel = new[] { 2, 3, 4, 5, 6 },
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };

        private readonly int[] baseDamageByLevel = { 60, 95, 130, 165, 200 };
        private readonly int[] killerInstinctDamage = { 8, 12, 16, 20, 24 };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = baseDamageByLevel[level - 1];

            float totalDamage = GetTotalAttackDamage(owner);
            float baseAttackDamage = GetBaseAttackDamage(owner);
            float bonusDamage = (totalDamage - baseAttackDamage) * 0.8f;
            float totalSpellDamage = baseDamage + bonusDamage;

            int killerInstinctLevel = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (killerInstinctLevel > 0)
            {
                totalSpellDamage += killerInstinctDamage[killerInstinctLevel - 1];
            }

            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KillerInstinct)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KillerInstinct), owner);
                AddBuff(attacker, owner, new Buffs.KillerInstinctBuff2(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }

            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KillerInstinctBuff2)) > 0)
            {
                ApplyKillerInstinctEffects(target, totalSpellDamage);
            }
            else
            {
                ApplyRegularDamage(target, totalSpellDamage);
            }

            // Handle bouncing logic
            if (remainingBounces > 0)
            {

                BounceToNextTarget(target);
            }
            else
            {
                ResetBounceCount();
            }


        }

        private void ApplyKillerInstinctEffects(AttackableUnit target, float damage)
        {
            remainingBounces -= 1;
            AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false, attacker);
        }

        private void ApplyRegularDamage(AttackableUnit target, float damage)
        {
            remainingBounces -= 1;
            float damageMultiplier = 1 - (remainingBounces * 0.1f);
            ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, damageMultiplier, 0.35f, 1, false, false, attacker);
        }

        private void BounceToNextTarget(AttackableUnit currentTarget)
        {
            foreach (AttackableUnit unit in GetRandomUnitsInArea(attacker, currentTarget.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 10, default, false))
            {
                if (unit != currentTarget && (!GetStealthed(unit) || CanSeeTarget(attacker, unit)))
                {
                    Vector3 attackerPos = GetUnitPosition(currentTarget);
                    int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    SpellCast(attacker, unit, default, default, 0, SpellSlotType.SpellSlots, level, true, true, false, false, false, true, attackerPos);
                    break;
                }
            }
        }

        private void ResetBounceCount()
        {
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            remainingBounces = MetaData.ChainMissileParameters.MaximumHitsByLevel[level - 1];
        }
    }
}

namespace Buffs
{
    public class BouncingBlades : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "" },
        };
    }
}