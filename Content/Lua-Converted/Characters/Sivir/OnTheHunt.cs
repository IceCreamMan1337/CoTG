

namespace Spells
{
    public class OnTheHunt : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 90f, 90f, 90f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.25f, 0.25f, 0.25f };
        float[] effect1 = { 0.3f, 0.6f, 0.9f };
        int[] effect2 = { 15, 15, 15 };
        float[] effect3 = { 0.1f, 0.2f, 0.3f };
        int[] effect4 = { 15, 15, 15 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
    ref HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            nextBuffVars_MoveSpeedMod = this.effect0[level - 1];
            if (target == owner)
            {
                nextBuffVars_AttackSpeedMod = this.effect1[level - 1];
                AddBuff(attacker, target, new Buffs.OnTheHunt(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, this.effect2[level], BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0);
            }
            else
            {
                nextBuffVars_AttackSpeedMod = this.effect3[level - 1];
                AddBuff(attacker, target, new Buffs.OnTheHuntAuraBuff(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, this.effect4[level], BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0);
            }
        }
    }
}
namespace Buffs
{
    public class OnTheHunt : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", "", "", "", },
            AutoBuffActivateEffect = new[] { "OntheHunt_buf.troy", "OntheHuntBase_buf.troy", "", "", },
            BuffName = "On The Hunt",
            BuffTextureName = "Sivir_Deadeye.dds",
        };
        float moveSpeedMod;
        float attackSpeedMod;
        public OnTheHunt(float moveSpeedMod = default, float attackSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.attackSpeedMod);
            if (attacker != owner)
            {
                this.moveSpeedMod /= 2;
                this.attackSpeedMod /= 2;
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}