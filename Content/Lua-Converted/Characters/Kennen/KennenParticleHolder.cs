namespace Buffs
{
    public class KennenParticleHolder : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        Particle globeOne;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out globeOne, out _, "kennen_mos1.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(globeOne);
        }
    }
}
namespace PreLoads
{
    public class KennenParticleHolder : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("kennen_mos1.troy");
        }
    }
}