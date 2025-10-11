namespace Buffs
{
    public class TalonRakeMarker : BuffScript
    {
        Particle particleZ;
        public override void OnActivate()
        {
            SpellEffectCreate(out particleZ, out _, "BladeRgoue_BladeAOE_TEMP.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, attacker, default, default, false, default, default, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleZ);
        }
    }
}
namespace PreLoads
{
    public class TalonRakeMarker : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("bladergoue_bladeaoe_temp.troy");
            PreloadSpell("root");
        }
    }
}