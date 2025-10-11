namespace Spells
{
    public class VorpalSpikes : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VorpalSpikes)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.VorpalSpikes), owner);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.VorpalSpikes(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                // AddBuff(owner, owner, new Buffs.PowerFist(nextBuffVars_SpellCooldown), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class VorpalSpikes : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Vorpal Spikes",
            BuffTextureName = "GreenTerror_ChitinousExoplates.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };

    }
}
//    
namespace PreLoads
{
    public class VorpalSpikes : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("vorpalspikes");
        }
    }
}