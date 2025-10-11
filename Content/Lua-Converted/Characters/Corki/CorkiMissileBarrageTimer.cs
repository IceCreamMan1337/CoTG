namespace Buffs
{
    public class CorkiMissileBarrageTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = false,
        };
        public override void OnDeactivate(bool expired)
        {
            DebugSay(owner, "test");
            if (!IsDead(owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MissileBarrage(), 7, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
                DebugSay(owner, "test2");
            }
        }
    }
}
namespace PreLoads
{
    public class CorkiMissileBarrageTimer : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("missilebarrage");
        }
    }
}