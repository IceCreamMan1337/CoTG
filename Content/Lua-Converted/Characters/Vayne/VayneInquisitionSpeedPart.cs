namespace Buffs
{
    public class VayneInquisitionSpeedPart : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        Particle speedParticle;
        public override void OnActivate()
        {
            Particle speedParticle;
            SpellEffectCreate(out speedParticle, out _, "vayne_ult_speed_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, default, default, false);
            this.speedParticle = speedParticle;
        }
        public override void OnDeactivate(bool expired)
        {
            Particle speedParticle = this.speedParticle; // UNUSED
            SpellEffectRemove(this.speedParticle);
        }
    }
}
namespace PreLoads
{
    public class VayneInquisitionSpeedPart : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("vayne_ult_speed_buf.troy");
        }
    }
}