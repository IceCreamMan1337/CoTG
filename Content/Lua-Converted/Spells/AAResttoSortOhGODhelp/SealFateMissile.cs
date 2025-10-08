namespace Spells
{
    public class SealFateMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };

        int[] effect0 = { 60, 110, 160, 210, 260 };



        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
          ref HitResult hitResult)
        {


            ObjAIBase attacker = GetBuffCasterUnit();
            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.SealFateMissile)) == 0)
            {
                AddBuff(attacker, target, new Buffs.SealFateMissile(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.65f, 1, false, false, attacker);

            }

        }
    }
}
namespace Buffs
{
    public class SealFateMissile : BuffScript
    {

    }
}
