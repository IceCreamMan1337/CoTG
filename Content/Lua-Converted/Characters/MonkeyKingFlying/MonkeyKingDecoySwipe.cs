using System.Linq;

namespace Spells
{
    public class MonkeyKingDecoySwipe : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseDamage = effect0[level - 1];
            TeamId teamID = GetTeamID_CS(owner);
            Champion caster = GetChampionBySkinName("MonkeyKing", teamID);
            float monkeyKingAP = GetFlatMagicDamageMod(caster);
            monkeyKingAP *= 0.6f;
            float damageToDeal = baseDamage + monkeyKingAP;

            //THIS IS AN FUCKING UGLY HACK , but i'm lazy to find the problem 
            int number = GetUnitsInArea(owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true).Count();

            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (number != 0)
                {
                    var totaldmg = damageToDeal / number;
                    ApplyDamage(caster, unit, totaldmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, true, false, caster);
                }
            }

            // ApplyDamage(caster, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, true, false, caster);

        }
    }
}
namespace PreLoads
{
    public class MonkeyKingDecoySwipe : IPreLoadScript
    {
        public void Preload()
        {
            PreloadCharacter("monkeyking");
        }
    }
}