namespace Buffs
{
    internal class AscBuffIcon : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };
        public override StatsModifier StatsModifier { get; protected set; } = new();
        StatsModifier LevelUpModifier = new();

        public override void OnActivate()
        {
            //TODO: Add 100% mana/energy cost reduction and 50% Health cost reduction
            StatsModifier.HealthPoints.FlatBonus = 50.0f * GetLevel(Owner);
            StatsModifier.AttackDamage.FlatBonus = 12.0f * GetLevel(Owner);
            StatsModifier.AbilityPower.FlatBonus = 12.0f * GetLevel(Owner);
            StatsModifier.ArmorPenetration.PercentBonus = 0.15f;
            StatsModifier.MagicPenetration.PercentBonus = 0.15f;
            StatsModifier.CooldownReduction.PercentBonus = 0.25f;
            StatsModifier.Size.PercentBonus = 0.5f;
            Target.AddStatModifier(StatsModifier);

            LevelUpModifier.HealthPoints.FlatBonus = 50.0f;
            LevelUpModifier.AttackDamage.FlatBonus = 12.0f;
            LevelUpModifier.AbilityPower.FlatBonus = 12.0f;

            ApiEventManager.OnLevelUp.AddListener(this, Target, OnLevelUp, false);

            //TODO: Add buff tooltip updates when we find out why they can be updated ATM.
        }

        public void OnLevelUp(AttackableUnit unit)
        {
            StatsModifier.HealthPoints.IncFlatBonusPerm(50.0f);
            StatsModifier.AttackDamage.IncFlatBonusPerm(12.0f);
            StatsModifier.AbilityPower.IncFlatBonusPerm(12.0f);
            unit.AddStatModifier(LevelUpModifier);
        }
    }
}