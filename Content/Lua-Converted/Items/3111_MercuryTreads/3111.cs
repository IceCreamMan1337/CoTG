namespace ItemPassives
{
    public class ItemID_3111 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                AddBuff(attacker, target, new Buffs.Hardening(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void OnActivate() //UNUSED
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MercuryTreads)) == 0)
            {
                AddBuff(owner, owner, new Buffs.MercuryTreads(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false); //Reduced useless buff duration from 10 to 1 seconds to mitigate the issue
            }
        }
    }
}
namespace PreLoads
{
    public class ItemID_3111 : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("hardening");
            PreloadSpell("mercurytreads");
        }
    }
}