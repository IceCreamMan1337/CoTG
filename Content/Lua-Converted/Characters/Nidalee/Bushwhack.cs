namespace Spells
{
    public class Bushwhack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };

        public override void SelfExecute()
        {
            Vector3 targetPosition = GetSpellTargetPos(spell);
            TeamId teamID = GetTeamID_CS(owner);

            Minion trap = SpawnMinion(
                "Noxious Trap",
                "Nidalee_Spear",
                "idle.lua",
                targetPosition,
                teamID,
                false,
                true,
                false,
                true,
                true,
                true,
                0,
                false,
                false,
                (Champion)owner
            );

            PlayAnimation("Spell1", 1, trap, false, false, true);
            AddBuff(attacker, trap, new Buffs.Bushwhack(), 1, 1, 240, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}

namespace Buffs
{
    public class Bushwhack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
        };

        private bool isActive;
        private bool isSprung;
        private Particle particle;
        private Particle emptyParticle;
        private float lastTimeExecuted;

        private readonly float[] damagePerTick = { 20, 31.25f, 42.5f, 53.75f, 65 };
        private readonly float[] debuffAmounts = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };

        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SetGhosted(owner, true);
            SetInvulnerable(owner, true);
            SetCanMove(owner, false);
            SetTargetable(owner, false);

            isActive = false;
            isSprung = false;

            SpellEffectCreate(out _, out _, "nidalee_bushwhack_set_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(emptyParticle);
            ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }

        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -1);
        }

        public override void OnUpdateActions()
        {
            TeamId teamID = GetTeamID_CS(attacker);

            if (isActive)
            {
                HandleTrapTrigger(teamID);
            }
            else
            {
                if (ExecutePeriodically(0.9f, ref lastTimeExecuted, false))
                {
                    ActivateTrap(teamID);
                }
            }
        }

        private void HandleTrapTrigger(TeamId teamID)
        {
            foreach (AttackableUnit unit in GetUnitsInArea(
                attacker,
                owner.Position3D,
                150,
                SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes,
                default,
                true
            ))
            {
                BreakSpellShields(unit);
                TriggerTrapEffects(teamID, unit);

                int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float damage = damagePerTick[level - 1];
                float debuff = debuffAmounts[level - 1];

                AddBuff(attacker, unit, new Buffs.BushwhackDebuff(debuff), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.BushwhackDamage(0, damage), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);

                isSprung = true;
            }

            if (isSprung)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }

        private void TriggerTrapEffects(TeamId teamID, AttackableUnit unit)
        {
            SpellEffectCreate(out particle, out _, "nidalee_bushwhack_trigger_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle, out _, "nidalee_bushwhack_trigger_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, false, false, false, false);
        }

        private void ActivateTrap(TeamId teamID)
        {
            isActive = true;
            SpellEffectCreate(out particle, out emptyParticle, "nidalee_trap_team_id_green.troy", "empty.troy", teamID, 0, 0, TeamId.TEAM_UNKNOWN, teamID, default, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
    }
}
namespace PreLoads
{
    public class Bushwhack : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("nidalee_bushwhack_set_02.troy");
            PreloadParticle("nidalee_bushwhack_trigger_01.troy");
            PreloadParticle("nidalee_bushwhack_trigger_02.troy");
            PreloadSpell("bushwhackdebuff");
            PreloadSpell("destealth");
            PreloadParticle("nidalee_trap_team_id_green.troy");
            PreloadParticle("empty.troy");
            PreloadCharacter("nidalee_spear");
        }
    }
}