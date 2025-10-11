namespace Buffs
{
    public class UdyrTigerShred : BuffScript
    {
        Particle lhand;
        Particle rhand;
        int[] effect0 = { 30, 80, 130, 180, 230 };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out lhand, out _, "Udyr_Tiger_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_Finger", default, owner, default, default, true, default, default, false, false);
            SpellEffectCreate(out rhand, out _, "Udyr_Tiger_buf_R.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_Finger", default, owner, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(lhand);
            SpellEffectRemove(rhand);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not LaneTurret)
            {
                TeamId teamID = GetTeamID_CS(owner); // UNUSED
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float baseDamage = effect0[level - 1];
                float tAD = GetTotalAttackDamage(owner);
                float dotDamage = tAD * 1.5f;
                dotDamage += baseDamage;
                dotDamage *= 0.25f;
                float nextBuffVars_DotDamage = dotDamage;
                AddBuff(attacker, target, new Buffs.UdyrTigerPunchBleed(nextBuffVars_DotDamage), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.UdyrTigerShred), (ObjAIBase)owner, 0);
            }
        }
    }
}
namespace PreLoads
{
    public class UdyrTigerShred : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("udyr_tiger_buf.troy");
            PreloadParticle("udyr_tiger_buf_r.troy");
            PreloadSpell("udyrtigerpunchbleed");
            PreloadSpell("udyrtigershred");
        }
    }
}