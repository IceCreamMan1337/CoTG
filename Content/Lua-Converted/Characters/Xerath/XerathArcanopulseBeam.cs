namespace Buffs
{
    public class XerathArcanopulseBeam : BuffScript
    {
        Particle particleID; // UNUSED
        Particle particleID2; // UNUSED
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            SpellEffectCreate(out particleID, out particleID2, "XerathR_beam_warning_green.troy", "XerathR_beam_warning_red.troy", teamOfOwner, 550, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottom", default, attacker, "bottom", default, true, false, false, false, false);
        }
    }
}
namespace PreLoads
{
    public class XerathArcanopulseBeam : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("xerathr_beam_warning_green.troy");
            PreloadParticle("xerathr_beam_warning_red.troy");
        }
    }
}