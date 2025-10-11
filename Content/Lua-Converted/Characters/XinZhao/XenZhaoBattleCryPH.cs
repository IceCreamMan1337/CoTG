namespace Buffs
{
    public class XenZhaoBattleCryPH : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoBattleCryPassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
        }
    }
}
namespace PreLoads
{
    public class XenZhaoBattleCryPH : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("xenzhaobattlecrypassive");
        }
    }
}