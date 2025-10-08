

namespace Spells
{
    public class SpiralBladeMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {20, 70, 120, 170, 220};
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
    ref HitResult hitResult)
        {

            //Particle afa; // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            float percentOfAttack = charVars.PercentOfAttack;
            float totalDamage = GetTotalAttackDamage(owner);
            float bonusWeaponDamage = 0.75f * totalDamage;
            ApplyDamage(attacker, target, bonusWeaponDamage + this.effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0.75f, 1, false, false, attacker);
            charVars.PercentOfAttack *= 0.9f;
            charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.4f);
            SpellEffectCreate(out _, out _, "SpiralBlade_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, false, false, false, false);
        }
    }
}