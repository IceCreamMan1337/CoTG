namespace Buffs
{
    internal class AscWarpProtection : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
        };

        public override StatsModifier StatsModifier { get; protected set; }
    }
}