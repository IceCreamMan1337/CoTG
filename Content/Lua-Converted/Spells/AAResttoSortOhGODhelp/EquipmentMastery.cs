namespace Buffs
{
    public class EquipmentMastery : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EquipmentMastery",
            BuffTextureName = "Armsmaster_MasterOfArms.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float apHealthAdded;
        float attackHealthAdded;
        float attackTotal;
        float apTotal;
        public override void OnActivate()
        {
            attackTotal = GetFlatPhysicalDamageMod(owner);
            apTotal = GetFlatMagicDamageMod(owner);
        }
        public override void OnUpdateStats()
        {
            apHealthAdded = apTotal * 2;
            attackHealthAdded = attackTotal * 3;

            IncMaxHealth(owner, apHealthAdded, false);
            IncMaxHealth(owner, attackHealthAdded, false);
            SetBuffToolTipVar(1, attackHealthAdded);
            SetBuffToolTipVar(2, apHealthAdded);
        }
        public override void OnUpdateActions()
        {
            attackTotal = GetFlatPhysicalDamageMod(owner);
            apTotal = GetFlatMagicDamageMod(owner);
        }
    }
}