namespace ItemPassives
{
    public class ItemID_3126 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                AddBuff(owner, owner, new Buffs.Bloodrazor(), 1, 1, 11, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}
namespace PreLoads
{
    public class ItemID_3126 : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("bloodrazor");
        }
    }
}