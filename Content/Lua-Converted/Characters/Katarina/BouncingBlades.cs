namespace Spells
{
    public class BouncingBlades : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = [2, 3, 4, 5, 6],
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
            int bbCounter = GetCastSpellTargetsHitPlusOne();

            if (HasBuff(owner, "KillerInstinct"))
            {
                SpellBuffRemove(owner, nameof(Buffs.KillerInstinct), owner);
                AddBuff(attacker, owner, new Buffs.KillerInstinctBuff2(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }

            if (HasBuff(owner, "KillerInstinctBuff2"))
            {
                AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                int TargetNum = GetCastSpellTargetsHitPlusOne();
                ApplyDamage(attacker, target, totalSpellDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false, attacker);
            }
            else
            {
                float  bbCount = bbCounter - 1;
                float inverseVar = bbCount * 0.1f;
                float percentVar = MathF.Abs(inverseVar - 1); //This abs call wasn't present in the original script, but otherwise you'd deal negative damage
                ApplyDamage(attacker, target, totalSpellDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentVar, 0.35f, 1, false, false, attacker);
            }
        }
    }
}

namespace Buffs
{
    public class BouncingBlades : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = [""],
        };
    }
}
namespace PreLoads
{
    public class BouncingBlades : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("killerinstinct");
            PreloadSpell("killerinstinctbuff2");
            PreloadSpell("internal_50ms");
            PreloadSpell("grievouswound");
        }
    }
}