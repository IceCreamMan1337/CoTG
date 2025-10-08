/*namespace Buffs
{
    internal class AscWarpReappear : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
        };

        public override StatsModifier StatsModifier { get; protected set; }

        public override void OnActivate()
        {
            AddParticleLink(Target, "Global_Asc_Teleport_reappear", Target, Target, Buff.Duration);
        }
    }
}*/