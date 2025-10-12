namespace Buffs
{
    public class XerathParticleBuff : BuffScript
    {
    }
}

namespace PreLoads
{
    public class XerathParticleBuff : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("Xerathidle.troy");
        }
    }
}