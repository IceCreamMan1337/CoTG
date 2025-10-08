/*namespace Buffs
{
    /*internal class AscXerathControl : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
        };

        public override StatsModifier StatsModifier { get; protected set; }

        public override void OnActivate()
        {
            OverrideAnimation(Target, "IDLE1OVERRIDE", "IDLE1");
            int avgLevel = GetPlayerAverageLevel();
            if (Target is Minion xerath && xerath.MinionLevel < avgLevel)
            {
                xerath.LevelUp(avgLevel - xerath.MinionLevel);
            }
        }
    }
}*/