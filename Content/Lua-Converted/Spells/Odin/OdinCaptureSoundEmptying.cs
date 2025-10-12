namespace Buffs
{
    public class OdinCaptureSoundEmptying : BuffScript
    {
        Particle particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle, out _, "Odin-Capture-Emptying.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "Crystal", owner.Position3D, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }
    }
}
namespace PreLoads
{
    public class OdinCaptureSoundEmptying : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("odin-capture-emptying.troy");
        }
    }
}