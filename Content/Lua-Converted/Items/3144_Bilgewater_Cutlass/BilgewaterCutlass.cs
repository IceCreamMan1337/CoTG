namespace Spells
{
    public class BilgewaterCutlass : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            for (int i = 0; i < 6; i++)
            {
                string slotName = GetSlotSpellName(owner, i, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
                if (slotName == nameof(Spells.BilgewaterCutlass))
                {
                    SetSlotSpellCooldownTimeVer2(60, i, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                }
            }

            Vector3 targetPos = GetUnitPosition(target);
            FaceDirection(owner, targetPos);
            SpellEffectCreate(out _, out _, "PirateCutlass_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            BreakSpellShields(target);
            ApplyDamage(attacker, target, 150, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);

            float slowAmount = -0.5f;
            AddBuff(attacker, target, new Buffs.BilgewaterCutlass(slowAmount), 1, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.SLOW, 0, true, false);
        }
    }
}

namespace Buffs
{
    public class BilgewaterCutlass : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, null, "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "BilgewaterCutlass",
            BuffTextureName = "3144_Bilgewater_Cutlass.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };

        private readonly float moveSpeedMod;
        private Particle slowEffect;

        public BilgewaterCutlass(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }

        public override void OnActivate()
        {
            SpellEffectCreate(out slowEffect, out _, "Global_Slow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(slowEffect);
        }

        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}
namespace PreLoads
{
    public class BilgewaterCutlass : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("global_slow.troy");
            PreloadParticle("piratecutlass_cas.troy");
        }
    }
}