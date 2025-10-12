namespace Spells
{
    public class NocturneParanoiaDashSound : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class NocturneParanoiaDashSound : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "NocturneParanoiaTarget",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        Particle loop;
        public override void OnActivate()
        {
            SpellEffectCreate(out loop, out _, "NocturneParanoiaDashSound.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(loop);
        }
    }
}
namespace PreLoads
{
    public class NocturneParanoiaDashSound : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("nocturneparanoiadashsound.troy");
            PreloadSpell("root");
        }
    }
}