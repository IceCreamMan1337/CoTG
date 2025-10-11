namespace Buffs
{
    public class SummonerOdinPromote : BuffScript
    {
    }
}

namespace PreLoads
{
    public class SummonerOdinPromote : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("summoner_flash.troy");
            PreloadSpell("summonerodinpromote");
            PreloadSpell("odinsuperminion");
            PreloadParticle("summoner_cast.troy");
        }
    }
}