namespace Spells
{
    public class AstralBeam : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 70, 120, 170, 220, 270 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float aPStat = GetFlatMagicDamageMod(owner);
            aPStat *= 0.5f;
            float baseDamage = effect0[level - 1];
            float dazzleDamage = baseDamage + aPStat;
            ApplyDamage(attacker, target, dazzleDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
        }
    }
}