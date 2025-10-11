namespace Buffs
{
    public class Pantheon_AegisApplicator : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}
namespace PreLoads
{
    public class Pantheon_AegisApplicator : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("pantheon_aegisshield");
        }
    }
}