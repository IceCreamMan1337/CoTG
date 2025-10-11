namespace Buffs
{
    public class ShenWayOfTheNinjaAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Shen Passive Aura",
            BuffTextureName = "Shen_KiStrike.dds",
        };
        Particle leftHand;
        Particle rightHand;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, false);
            SpellEffectCreate(out leftHand, out _, "shen_kiStrike_ready_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, true, default, default, false);
            SpellEffectCreate(out rightHand, out _, "shen_kiStrike_ready_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_hand", default, target, default, default, true, default, default, false);
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            charVars.Count = 0;
            RemoveOverrideAutoAttack(owner, false);
            SpellEffectRemove(leftHand);
            SpellEffectRemove(rightHand);
            SetDodgePiercing(owner, true);
        }
    }
}
namespace PreLoads
{
    public class ShenWayOfTheNinjaAura : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("shen_kistrike_ready_indicator.troy");
        }
    }
}