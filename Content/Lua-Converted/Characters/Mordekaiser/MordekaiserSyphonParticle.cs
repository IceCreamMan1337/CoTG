namespace Buffs
{
    public class MordekaiserSyphonParticle : BuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "mordakaiser_siphonOfDestruction_self.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            SpellEffectCreate(out _, out _, "mordakeiser_hallowedStrike_self_skin.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
        }
    }
}
namespace PreLoads
{
    public class MordekaiserSyphonParticle : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("mordakaiser_siphonofdestruction_self.troy");
            PreloadParticle("mordakeiser_hallowedstrike_self_skin.troy");
        }
    }
}