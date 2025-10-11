namespace Buffs
{
    public class OdinDebacleTimer : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            bool nextBuffVars_WillRemove = false;
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinDebacleCloak(nextBuffVars_WillRemove), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
        }
    }
}
namespace PreLoads
{
    public class OdinDebacleTimer : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("OdinDebacleCloak");
        }
    }
}