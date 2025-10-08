namespace Buffs
{
    public class CrestofCrushingWrath : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Crushing Wrath",
            BuffTextureName = "WaterWizard_Vortex.dds",
            NonDispellable = true,
        };

        private Particle buffParticle;
        private float damageVar;
        private float lastTimeExecuted;
        private readonly int[] damageLevels = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "Speed_runes_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            damageVar = 0;
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }

        public override void OnUpdateStats()
        {
            float levelModifier = GetLevel(owner) * 0.01f;
            IncPercentPhysicalDamageMod(owner, levelModifier);
            IncPercentMagicDamageMod(owner, levelModifier);
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float currentDamage = damageLevels[Math.Max(level - 1, 0)];

                if (currentDamage > damageVar)
                {
                    damageVar = currentDamage;
                    SetBuffToolTipVar(1, currentDamage);
                }
            }
        }
    }
}