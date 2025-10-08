namespace Spells
{
    public class TwitchVenomCask : DebilitatingPoison { }

    public class DebilitatingPoison : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };

        private readonly float[] slowDurationsByLevel = { 2f, 2.6f, 3.2f, 3.8f, 4.4f };
        private const float MoveSpeedReduction = -0.3f;

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.DebilitatingPoison(MoveSpeedReduction), 1, 1, slowDurationsByLevel[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
            }
        }
    }
}

namespace Buffs
{
    public class TwitchVenomCask : DebilitatingPoison { }

    public class DebilitatingPoison : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", "twitch_debilitatingPoison_tar.troy" },
            BuffName = "DebilitatingPoison",
            BuffTextureName = "Twitch_Fade.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed" },
        };

        private readonly float moveSpeedMod;

        public DebilitatingPoison(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }

        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
        }

        public override void OnUpdateStats()
        {
            int deadlyVenomCount = GetBuffCountFromAll(owner, nameof(Buffs.DeadlyVenom));
            float additionalMoveSpeedReduction = deadlyVenomCount * -0.06f;
            float totalMoveSpeedMod = additionalMoveSpeedReduction + moveSpeedMod;
            IncPercentMultiplicativeMovementSpeedMod(owner, totalMoveSpeedMod);
        }
    }
}