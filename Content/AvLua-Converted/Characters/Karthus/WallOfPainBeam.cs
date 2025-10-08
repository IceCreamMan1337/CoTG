namespace Buffs
{
    public class WallOfPainBeam : BuffScript
    {
        Particle particle1;
        Particle particle;
        Particle particle2;
        Particle particle3;
        Particle particleID6;
        Particle noParticle; // UNUSED
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            SpellEffectCreate(out particle1, out particle, "wallofpain_new_post_green.troy", "wallofpain_new_post_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out particle2, out particle3, "wallofpain_new_post_green.troy", "wallofpain_new_post_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, attacker, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out particleID6, out noParticle, "wallofpain__new_beam.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottom", default, attacker, "bottom", default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle1);
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle3);
            SpellEffectRemove(particleID6);
        }
    }
}