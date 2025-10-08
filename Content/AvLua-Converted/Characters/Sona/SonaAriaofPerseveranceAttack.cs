namespace Spells
{
    public class SonaAriaofPerseveranceAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "GuqinSona", },
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            //og 
            // float attackDamage = GetTotalAttackDamage(owner);
            // ApplyDamage(owner, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);

            //hack copypaste buff Perseverance upgrade 
            int levelDamage = GetLevel(owner);
            float bonusDamage = levelDamage * 9;
            float totalDamage = bonusDamage + 14;
            float nextBuffVars_TotalDamage = totalDamage;
            float attackDamage = GetTotalAttackDamage(owner);
            ApplyDamage(owner, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            AddBuff(attacker, attacker, new Buffs.IfHasBuffCheck(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (target is ObjAIBase && target is not LaneTurret)
            {
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.SonaPowerChordDebuff(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            AddBuff(attacker, target, new Buffs.SonaAriaPCDeathRecapFix(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemove(owner, nameof(Buffs.SonaPowerChord), owner, 0);

        }
    }
}
namespace Buffs
{
    public class SonaAriaofPerseveranceAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
    }
}