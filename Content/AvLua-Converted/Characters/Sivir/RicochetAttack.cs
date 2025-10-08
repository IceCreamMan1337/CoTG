

namespace Spells
{
    public class RicochetAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,

                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHitsByLevel = new[]{ 2, 3, 4, 5, 6, },
                
            },
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
       
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
    ref HitResult hitResult)
        {
            int targetNum;
            float baseAttackDamage;
            float counter;
            float damagePercent;
           //  targetNum = GetCastSpellTargetsHitPlusOne();
           targetNum = GetSpellTargetsHitPlusOne(spell);
            baseAttackDamage = GetBaseAttackDamage(owner);
            //hack 


            if (targetNum == 1)
            {
                ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, default, false, false);
            }
            else
            {
                counter = 1;
                damagePercent = 1;
                while (counter < targetNum)
                {
                    damagePercent *= 0.78f;
                    counter++;
                }
                ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, damagePercent, 1, default, false, false);
            }
           
        }
    }
}