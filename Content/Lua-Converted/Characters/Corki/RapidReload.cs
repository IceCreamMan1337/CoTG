namespace Buffs
{
    public class RapidReload : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "RapidReload",
            BuffTextureName = "Corki_RapidReload.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not LaneTurret)
            {
                float trueDamageAmount = 0.1f * damageAmount;
                float temp1 = GetHealth(target, PrimaryAbilityResourceType.MANA);
                if (trueDamageAmount > temp1)
                {
                    trueDamageAmount = temp1 - 1;
                    //was DAMAGE_SOURCE_DEFAULT , but this one create stackoverflow due to other champion who have DAMAGE_SOURCE_DEFAULT in their autoattack 
                    ApplyDamage(attacker, target, trueDamageAmount, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, 1, false, false);
                }
                else
                {
                    ApplyDamage(attacker, target, trueDamageAmount, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, 1, false, false);
                }
            }
        }
    }
}