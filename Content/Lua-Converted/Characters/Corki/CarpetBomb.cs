namespace Spells
{
    public class CarpetBomb : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "UrfRiderCorki" },
        };

        // Unused variable placeholder for potential future functionality
        float tickDuration; // UNUSED
        private readonly int[] tickAmounts = { 15, 16, 17, 18, 19 };

        public override bool CanCast()
        {
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);

            if (!canCast || !canMove)
            {
                return false;
            }

            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);

            if (level > 0 && (!GetCanMove(owner) || !GetCanCast(owner)))
            {
                return false;
            }

            return true;
        }

        public override void SelfExecute()
        {
            int levelIndex = level - 1;
            float tickAmount = tickAmounts[levelIndex];
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);

            FaceDirection(owner, targetPos);

            if (distance > 800)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 800, 0);
            }

            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_SelfAP = GetFlatMagicDamageMod(owner);
            bool nextBuffVars_WillRemove = false;
            ownerPos = GetUnitPosition(owner);
            float moveSpeed = GetMovementSpeed(owner);
            float slashSpeed = moveSpeed + 650;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            float duration = distance / slashSpeed;
            tickDuration = duration / tickAmount;
            bool nextBuffVars_WillMove = true;
            float nextBuffVars_SlashSpeed = slashSpeed;

            AddBuff(
                attacker,
                owner,
                new Buffs.CarpetBomb(
                    nextBuffVars_WillMove,
                    nextBuffVars_TargetPos,
                    nextBuffVars_SelfAP,
                    nextBuffVars_WillRemove,
                    nextBuffVars_SlashSpeed
                ),
                1,
                1,
                0.05f + duration,
                BuffAddType.REPLACE_EXISTING,
                BuffType.INTERNAL,
                0,
                true,
                false,
                false
            );

            AddBuff(
                attacker,
                owner,
                new Buffs.ValkyrieSound(),
                1,
                1,
                duration,
                BuffAddType.REPLACE_EXISTING,
                BuffType.COMBAT_ENCHANCER,
                0,
                true,
                false,
                false
            );
        }
    }
}

namespace Buffs
{
    public class CarpetBomb : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "" },
        };

        private readonly bool willMove;
        private readonly Vector3 targetPos;
        private readonly float selfAP;
        private readonly bool willRemove;
        private readonly float slashSpeed;
        private Particle particle;
        private readonly float tickDuration;
        private float lastTimeExecuted;
        private readonly int[] damagePerTickLevels = { 30, 45, 60, 75, 90 };

        public CarpetBomb(
            bool willMove = default,
            Vector3 targetPos = default,
            float selfAP = default,
            bool willRemove = default,
            float slashSpeed = default,
            float tickDuration = default
        )
        {
            this.willMove = willMove;
            this.targetPos = targetPos;
            this.selfAP = selfAP;
            this.willRemove = willRemove;
            this.slashSpeed = slashSpeed;
            this.tickDuration = tickDuration;
        }

        public override void OnActivate()
        {
            Move(owner, targetPos, slashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out particle, out _, "corki_valkrie_speed.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, default, default, false);
            PlayAnimation("Spell2", 0, owner, true, false, true);
        }

        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, false);
            if (!willMove)
            {
                SpellEffectRemove(particle);
            }
        }

        public override void OnUpdateStats()
        {
            float moveSpeedVal = GetMovementSpeed(owner);
            if (moveSpeedVal < 300)
            {
                float moveSpeedDiff = 300 - moveSpeedVal;
                IncFlatMovementSpeedMod(owner, moveSpeedDiff);
            }
        }

        public override void OnUpdateActions()
        {
            int levelIndex = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots) - 1;

            if (ExecutePeriodically(0, ref lastTimeExecuted, true, tickDuration))
            {
                float damagePerTick = damagePerTickLevels[levelIndex];
                float aPBonus = 0.2f * selfAP;
                damagePerTick += aPBonus;
                Vector3 bombPos = GetUnitPosition(owner);
                TeamId teamOfOwner = GetTeamID_CS(owner);

                Minion bombMinion = SpawnMinion(
                    "HiddenMinion",
                    "TestCube",
                    "idle.lua",
                    bombPos,
                    teamOfOwner,
                    false,
                    true,
                    false,
                    true,
                    true,
                    false,
                    0,
                    default,
                    true,
                    (Champion)owner
                );

                AddBuff(attacker, bombMinion, new Buffs.DangerZone(damagePerTick), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            }

            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }

        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}
namespace PreLoads
{
    public class CarpetBomb : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("corki_valkrie_speed.troy");
            PreloadSpell("root");
            PreloadCharacter("testcube");
            PreloadSpell("dangerzone");
            PreloadSpell("valkyriesound");
        }
    }
}