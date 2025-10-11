namespace Spells
{
    public class DangerZone : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}

namespace Buffs
{
    public class DangerZone : BuffScript
    {
        private readonly float damagePerTick;
        private Particle particle;
        private float lastTimeExecuted;

        public DangerZone(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }

        public override void OnActivate()
        {
            SetUnitProperties();
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out _, "corki_valkrie_impact_cas.troy", default, teamOfOwner, 900, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            ResetUnitProperties();
            InflictSelfDamage();
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamageEffectToUnit(unit);
                }
            }
        }

        private void SetUnitProperties()
        {
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
        }

        private void ResetUnitProperties()
        {
            SetTargetable(owner, true);
        }

        private void InflictSelfDamage()
        {
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
        }

        private void ApplyDamageEffectToUnit(AttackableUnit unit)
        {
            SpellEffectCreate(out _, out _, "corki_fire_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "pelvis", default, unit, default, default, false, false, false, false, false);
            if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.DangerZoneTarget)) == 0)
            {
                AddBuff(attacker, unit, new Buffs.DangerZoneTarget(damagePerTick), 1, 1, 0.49f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace PreLoads
{
    public class DangerZone : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("corki_valkrie_impact_cas.troy");
            PreloadParticle("corki_fire_buf.troy");
            PreloadSpell("dangerzonetarget");
        }
    }
}