namespace ItemPassives
{
    public class ItemID_3154 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, true))
            {
                AddBuff(owner, owner, new Buffs.wrigglelantern(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MadredsRazors)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.MadredsRazors), owner);
                }
            }
        }
    }
}
namespace PreLoads
{
    public class ItemID_3154 : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("wrigglelantern");
            PreloadSpell("madredsrazors");
        }
    }
}