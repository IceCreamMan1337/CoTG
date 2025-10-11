namespace Buffs
{
    public class JackInTheBoxDamageSensor : BuffScript
    {
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (target is Champion)
            {
                if (damageSource == default)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.JackInTheBoxHardLock(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (damageSource == default)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.JackInTheBoxHardLock(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            SetTriggerUnit(attacker);
            if (attacker is Champion && owner.Team != attacker.Team)
            {
                AddBuff((ObjAIBase)owner, attacker, new Buffs.JackInTheBoxHardLock(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace PreLoads
{
    public class JackInTheBoxDamageSensor : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("jackintheboxhardlock");
        }
    }
}