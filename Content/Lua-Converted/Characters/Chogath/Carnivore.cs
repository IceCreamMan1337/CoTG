namespace Buffs
{
    public class Carnivore : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Carnivore",
            BuffTextureName = "GreenTerror_TailSpike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private float lastHeal;
        private float lastTimeExecuted;

        // Unused variable placeholder for potential future functionality
        private int lastMana; // UNUSED

        private readonly int[] healAmounts = { 34, 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70 };
        private readonly float[] manaAmounts = { 3.5f, 3.75f, 4, 4.25f, 4.5f, 4.75f, 5, 5.25f, 5.5f, 5.75f, 6, 6.25f, 6.5f, 6.75f, 7, 7.25f, 7.5f, 7.75f, 8, 8.25f };

        public override void OnActivate()
        {
            lastHeal = 0;
            lastMana = 0; // Unused but initialized for completeness
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int levelIndex = GetLevel(owner) - 1;
                float currentHeal = healAmounts[levelIndex];
                float manaAmount = manaAmounts[levelIndex];

                if (currentHeal > lastHeal)
                {
                    lastHeal = currentHeal;
                    SetBuffToolTipVar(1, currentHeal);
                    SetBuffToolTipVar(2, manaAmount);
                }
            }
        }

        public override void OnKill(AttackableUnit target)
        {
            int levelIndex = GetLevel(owner) - 1;
            float manaAmount = manaAmounts[levelIndex];
            float healAmount = healAmounts[levelIndex];

            IncPAR(owner, manaAmount, PrimaryAbilityResourceType.MANA);
            IncHealth(owner, healAmount, owner);
            SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }

        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            float feastCount = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
            int stacksToRemove = CalculateStacksToRemove(feastCount);

            if (stacksToRemove > 0)
            {
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.Feast), stacksToRemove);
            }
        }

        private int CalculateStacksToRemove(float feastCount)
        {
            feastCount *= 0.5f;

            if (feastCount < 1.5f)
            {
                return 1;
            }
            else if (feastCount < 2.5f)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
namespace PreLoads
{
    public class Carnivore : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("eternalthirst_buf.troy");
            PreloadSpell("feast");
        }
    }
}