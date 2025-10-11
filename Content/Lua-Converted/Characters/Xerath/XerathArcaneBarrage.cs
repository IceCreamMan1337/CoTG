namespace Spells
{
    public class XerathArcaneBarrage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        int[] effect1 = { 150, 200, 250, 0, 0 };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(targetPos, ownerPos);
            float nextBuffVars_Distance = distance; // UNUSED
            SpellEffectCreate(out _, out _, "Xerath_E_cas.troy", default, TeamId.TEAM_ORDER, 100, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "chest", default, attacker, default, default, true, false, false, false, false);

            Minion other3 = SpawnMinion("HiddenMinion", "XerathArcaneBarrageLauncher", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, false, (Champion)owner);

            //hack fix particlke
            SpellEffectCreate(out _, out _, "Xerath_E_tar.troy", default, TeamId.TEAM_NEUTRAL, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, default, other3, default, targetPos, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "Xerath_Barrage_tar.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, default, other3, default, targetPos, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "Xerath_MageChains_consume.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, default, other3, default, targetPos, true, false, false, false, false);

            float nextBuffVars_SlowAmount = effect0[level - 1]; // UNUSED
            int nextBuffVars_DamageAmount = effect1[level - 1]; // UNUSED
            int nextBuffVars_Level = level; // UNUSED
            AddBuff(attacker, other3, new Buffs.XerathArcaneBarrage(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Region nextBuffVars_Bubble = AddPosPerceptionBubble(teamOfOwner, 600, targetPos, 4, default, false);
            AddBuff(owner, owner, new Buffs.XerathArcaneBarrageVision(nextBuffVars_Bubble), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(other3, owner, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
        }
    }
}
namespace Buffs
{
    public class XerathArcaneBarrage : BuffScript
    {
        Particle particle1;
        Particle particle;
        Particle a;
        public override void OnActivate()
        {
            //RequireVar(this.level);
            //RequireVar(this.damageAmount);
            //RequireVar(this.slowAmount);
            //RequireVar(this.distance);
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle1, out particle, "Xerath_E_cas_green.troy", "Xerath_E_cas_red.troy", teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, false, false, false, false);

            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle1);
            SpellEffectRemove(a);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}
namespace PreLoads
{
    public class XerathArcaneBarrage : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("xerath_e_cas_green.troy");
            PreloadParticle("xerath_e_cas_red.troy");
            PreloadParticle("xerath_e_tar.troy");
            PreloadParticle("xerath_barrage_tar.troy");
            PreloadSpell("xerathmagechainsroot");
            PreloadParticle("xerath_e_cas.troy");
            PreloadCharacter("xeratharcanebarragelauncher");
            PreloadSpell("xeratharcanebarrage");
            PreloadSpell("xeratharcanebarragevision");
        }
    }
}