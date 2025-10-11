namespace Spells
{
    public class XerathLocusOfPowerToggle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.XerathLocusOfPower), owner);
        }
    }
}
namespace Buffs
{
    public class XerathLocusOfPowerToggle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GlacialStorm",
            BuffTextureName = "Cryophoenix_GlacialStorm.dds",
            SpellToggleSlot = 2,
        };
    }
}
namespace PreLoads
{
    public class XerathLocusOfPowerToggle : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("xerathlocusofpower");
        }
    }
}