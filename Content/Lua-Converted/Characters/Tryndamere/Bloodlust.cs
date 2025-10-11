namespace Spells
{
    public class Bloodlust : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 30f, 26f, 22f, 18f, 14f },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "TryndamereDemonsword" },
        };

        private readonly int[] baseHealByLevel = { 30, 40, 50, 60, 70 };
        private readonly float[] healthPerFuryByLevel = { 0.5f, 0.95f, 1.4f, 1.85f, 2.3f };

        public override void SelfExecute()
        {
            float currentFury = GetPAR(owner, PrimaryAbilityResourceType.Other);
            float baseHeal = baseHealByLevel[level - 1];
            float healthPerFury = healthPerFuryByLevel[level - 1];
            float totalHeal = baseHeal + (currentFury * healthPerFury);

            // Add ability power modification
            float spellPower = GetFlatMagicDamageMod(owner);
            totalHeal += 1.5f * spellPower;

            // Heal the owner
            IncHealth(owner, totalHeal, owner);

            // Create healing visual effect
            SpellEffectCreate(
                out _,
                out _,
                "Tryndamere_Heal.troy",
                default,
                TeamId.TEAM_UNKNOWN,
                0,
                0,
                TeamId.TEAM_UNKNOWN,
                default,
                owner,
                false,
                owner,
                default,
                default,
                target,
                default,
                default,
                false,
                false,
                false,
                false,
                false
            );

            // Remove all fury
            IncPAR(owner, -currentFury, PrimaryAbilityResourceType.Other);
        }
    }
}

namespace Buffs
{
    public class Bloodlust : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "" },
            BuffName = "Bloodlust",
            BuffTextureName = "DarkChampion_Bloodlust.dds",
            SpellToggleSlot = 1,
        };

        private readonly float damageMod;
        private readonly float critDamageMod;

        public Bloodlust(float damageMod = default, float critDamageMod = default)
        {
            this.damageMod = damageMod;
            this.critDamageMod = critDamageMod;
        }

        public override void OnActivate()
        {
            int buffCount = GetBuffCountFromAll(owner, nameof(Buffs.Bloodlust));
            float totalDamage = buffCount * damageMod;
            float totalCritDamage = buffCount * critDamageMod * 100; // Convert to percentage

            SetBuffToolTipVar(1, totalDamage);
            SetBuffToolTipVar(2, totalCritDamage);
        }

        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageMod);
            IncFlatCritDamageMod(owner, critDamageMod);
        }
    }
}

namespace PreLoads
{
    public class Bloodlust : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("bloodlust");
            PreloadParticle("tryndamere_heal.troy");
        }
    }
}