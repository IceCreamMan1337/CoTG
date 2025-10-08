using ChildrenOfTheGraveEnumNetwork.Enums;

namespace Spells
{
    public class DoomBot_TormentedSoil : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 25, 40, 55, 70, 85 };
        int[] effect1 = { -4, -5, -6, -7, -8 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_DamagePerTick = effect0[level - 1];
            int nextBuffVars_MRminus = effect1[level - 1];
            Minion other1 = SpawnMinion("birds", "TestCube", "idle.lua", targetPos, owner.Team, false, true, false, false, true, true, 0, false, true);

            AddBuff(attacker, other1, new Buffs.DoomBot_TormentedSoilParticle(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);


            AddBuff(attacker, attacker, new Buffs.DoomBot_TormentedSoil(nextBuffVars_DamagePerTick, nextBuffVars_TargetPos, nextBuffVars_MRminus), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class DoomBot_TormentedSoil : BuffScript
    {
        float damagePerTick;
        Vector3 targetPos;
        float mRminus;
        Particle particle2;
        Particle particle;
        float lastTimeExecuted;
        public DoomBot_TormentedSoil(float damagePerTick = default, Vector3 targetPos = default, float mRminus = default)
        {
            this.damagePerTick = damagePerTick;
            this.targetPos = targetPos;
            this.mRminus = mRminus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.targetPos);
            //RequireVar(this.mRminus);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = this.targetPos;
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_MRminus = mRminus;
          //  SpellEffectCreate(out particle2, out particle, "TormentedSoil_green_tar.troy", "TormentedSoil_red_tar.troy", teamOfOwner, 1200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.TormentedSoilDebuff(nextBuffVars_TargetPos, nextBuffVars_MRminus), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                SpellEffectCreate(out _, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "L_foot", default, unit, default, default, false, false, false, false, false);
                SpellEffectCreate(out _, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "R_foot", default, unit, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
           // SpellEffectRemove(particle);
           // SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos = this.targetPos;
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_MRminus = mRminus;
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                {
                    ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.TormentedSoilDebuff(nextBuffVars_TargetPos, nextBuffVars_MRminus), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                    SpellEffectCreate(out _, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "L_foot", default, unit, default, default, false, false, false, false, false);
                    SpellEffectCreate(out _, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "R_foot", default, unit, default, default, false, false, false, false, false);
                }
            }
        }
    }
}

namespace Buffs
{
    public class DoomBot_TormentedSoilParticle : BuffScript
    {
        Particle particle2;
        Particle particle;
        float[] effect0 = { 0.2f, 0.3f, 0.4f, 0.5f, 0.6f };
        float[] effect1 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        float[] effect2 = { 0.8f, 0.75f, 0.7f, 0.65f, 0.6f };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle2, out particle, "trundledesecrate_green.troy", "trundledesecrate_red.troy", teamID, 10, 0, TeamId.TEAM_ORDER, default, default, false, owner, default, default, owner, default, default, false, default, default, false, false);
            /*  SetTargetable(owner, false);
            SetInvulnerable(owner, true); */
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnUpdateStats()
        {
            /*  SetTargetable(owner, false); 
            SetInvulnerable(owner, true); */
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
        }
    }
}