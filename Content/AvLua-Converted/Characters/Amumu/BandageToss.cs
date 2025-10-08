using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;

namespace Spells
{
    public class BandageToss : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 18f, 16f, 14f, 12f, 10f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, 1100, 0);
            //hack
            //SetSpell(owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, "SadMummyBandageToss");
            Particle nextBuffVars_ParticleID;
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out nextBuffVars_ParticleID, out _, "BandageToss_mis.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spine", default, target, "R_hand", default, false, false, false, false, false);
            SpellEffectCreate(out nextBuffVars_ParticleID, out _, "Bandage_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spine", default, target, "R_hand", default, false, false, false, false, false);
           //SpellEffectCreate(out particleID, out _, "FistReturn_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "head", default, owner, "R_hand", default, false, false, false, false, false);

            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false);
           
            //hack for bandagetoss
        }
    }
}