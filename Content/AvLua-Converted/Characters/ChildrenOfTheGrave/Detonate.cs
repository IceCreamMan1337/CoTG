namespace Spells
{
    public class Detonate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 115, 155, 195, 235, 280 };
        int[] effect1 = { 25, 30, 35, 40, 45 };
        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "Detonate_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod = effect1[level - 1];
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
             //AddBuff(attacker, attacker, new Buffs.Detonate(nextBuffVars_Shield), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            //hack for moment 
            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, 1.0f), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
           
        }
    }
}
namespace Buffs
{
    public class Detonate : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Detonate",
            BuffTextureName = "ChildrenOfTheGrave_Detonate.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
       
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
           
         //need add way to check distance 
         // if distance between target and owner < 100 , apply slow 


           //AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
        }
    }
}