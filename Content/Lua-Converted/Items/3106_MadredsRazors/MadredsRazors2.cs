namespace Buffs
{
    public class MadredsRazors2 : BuffScript
    {
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is ObjAIBase && RandomChance() < 0.15f && target is not LaneTurret && target is not Champion)
            {
                AddBuff(attacker, target, new Buffs.MadredsRazors(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0);
            }
        }
    }
}
namespace PreLoads
{
    public class MadredsRazors2 : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("madredsrazors");
        }
    }
}