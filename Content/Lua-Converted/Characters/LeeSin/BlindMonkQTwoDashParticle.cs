namespace Buffs
{
    public class BlindMonkQTwoDashParticle : BuffScript
    {
        public override void OnActivate()
        {
            Vector3 targetPos; // UNITIALIZED
            targetPos = default; //TODO: Verify
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false);
            SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_tar_blood.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false);
            SpellEffectCreate(out _, out _, "blindmonk_resonatingstrike_tar_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false);
        }
    }
}
namespace PreLoads
{
    public class BlindMonkQTwoDashParticle : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("blindmonk_q_resonatingstrike_tar.troy");
            PreloadParticle("blindmonk_q_resonatingstrike_tar_blood.troy");
            PreloadParticle("blindmonk_resonatingstrike_tar_sound.troy");
        }
    }
}