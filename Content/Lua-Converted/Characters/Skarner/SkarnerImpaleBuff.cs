namespace Buffs
{
    public class SkarnerImpaleBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SkarnerImpaleBuff",
            BuffTextureName = "SkarnerImpale.dds",
        };
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            SpellBuffClear(caster, nameof(Buffs.SkarnerImpale));
        }
    }
}
namespace PreLoads
{
    public class SkarnerImpaleBuff : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("skarnerimpale");
        }
    }
}