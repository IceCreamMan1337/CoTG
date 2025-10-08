namespace Spells
{
    public class DesperatePower : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };

        private readonly float[] spellVampByLevel = { 0.15f, 0.2f, 0.25f };
        private readonly int[] buffDurationByLevel = { 5, 6, 7 };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            float spellVamp = spellVampByLevel[level - 1];
            int duration = buffDurationByLevel[level - 1];
            AddBuff(attacker, target, new Buffs.DesperatePower(spellVamp), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}

namespace Buffs
{
    public class DesperatePower : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "DesperatePower",
            BuffTextureName = "Ryze_DesperatePower.dds",
            NonDispellable = true,
        };

        private readonly float spellVamp;
        private Particle spellEffectParticle;

        public DesperatePower(float spellVamp = default)
        {
            this.spellVamp = spellVamp;
        }

        public override void OnActivate()
        {
            IncPercentSpellVampMod(owner, spellVamp);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out spellEffectParticle, out _, "ManaLeach_tar2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(spellEffectParticle);
        }

        public override void OnUpdateStats()
        {
            IncPercentSpellVampMod(owner, spellVamp);
        }
    }
}
