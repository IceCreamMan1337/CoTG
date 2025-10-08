namespace Buffs
{
    public class AlZaharCallR : BuffScript
    {
        Particle particle2;
        Particle particle3;
        public AlZaharCallR(Particle particle2 = default, Particle particle3 = default)
        {
            this.particle2 = particle2;
            this.particle3 = particle3;
        }
        public override void OnActivate()
        {
            //RequireVar(this.particle2);
            //RequireVar(this.particle3);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle3);
        }
    }
}