/*namespace Buffs
{
    internal class AscRelicBombBuff : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
            IsHidden = false
        };

        public override StatsModifier StatsModifier { get; protected set; }

        public override void OnActivate()
        {
            AddUnitPerceptionBubble(Target, 800.0f, 25000.0f, TeamId.TEAM_ORDER, false, null, 38.08f);
            AddUnitPerceptionBubble(Target, 800.0f, 25000.0f, TeamId.TEAM_CHAOS, false, null, 38.08f);
            AddParticleLink(Target, "Asc_RelicPrism_Sand", Target, Target, -1.0f, 1.0f, new Vector3(0.0f, 0.0f, -1.0f), "", "", flags: (FXFlags)304);
            AddParticleLink(Target, "Asc_relic_Sand_buf", Target, Target, -1.0f, bindBone: "", targetBone: "", flags: (FXFlags)32);
            Target.IconInfo.ChangeIcon("Relic");
        }
    }
}*/