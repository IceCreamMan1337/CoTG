namespace Buffs
{
    public class RumbleCarpetBombSound2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "RumbleDangerZone",
            BuffTextureName = "Annie_GhastlyShield.dds",
        };
        Particle test;
        public override void OnActivate()
        {
            SpellEffectCreate(out test, out _, "RumbleCarpetBombSoundEnd.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(test);
        }
    }
}
namespace PreLoads
{
    public class RumbleCarpetBombSound2 : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("rumblecarpetbombsoundend.troy");
        }
    }
}