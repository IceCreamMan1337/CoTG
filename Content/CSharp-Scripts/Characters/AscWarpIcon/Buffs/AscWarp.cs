/*namespace Buffs
{
    internal class AscWarp : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
        };

        public override StatsModifier StatsModifier { get; protected set; }

        public override void OnActivate()
        {
            Buff.SetStatusEffect(StatusFlags.Stunned, true);
            Target.IconInfo.ChangeBorder("Teleport", "AscWarp");
            AddParticleLink(Target, "Global_Asc_teleport", Target, Target, Buff.Duration);
        }

        public override void OnDeactivate()
        {
            Target.IconInfo.ResetBorder();
            if (Target is ObjAIBase obj)
            {
                AddBuff("AscWarpReappear", 10.0f, 1, null, Target, obj);
            }
            AddBuff("AscWarpProtection", 2.5f, 1, null, Target, Owner);
        }
    }
}*/