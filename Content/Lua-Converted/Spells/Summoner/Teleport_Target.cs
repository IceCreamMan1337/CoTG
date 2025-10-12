namespace Buffs
{
    public class Teleport_Target : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Teleport Target",
            BuffTextureName = "Summoner_teleport.dds",
        };
        Particle part;
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            SetInvulnerable(owner, true);
            SpellEffectCreate(out part, out _, "Teleport_target.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            SetInvulnerable(owner, false);
            SpellEffectRemove(part);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            SetInvulnerable(owner, true);
        }
    }
}
namespace PreLoads
{
    public class Teleport_Target : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("teleport_target.troy");
        }
    }
}