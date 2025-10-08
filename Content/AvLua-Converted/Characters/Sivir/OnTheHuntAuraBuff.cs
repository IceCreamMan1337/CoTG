

namespace Buffs
{
    public class OnTheHuntAuraBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "OntheHunt_buf.troy", "", },
            BuffName = "On The Hunt",
            BuffTextureName = "Sivir_Deadeye.dds",
        };
        float moveSpeedMod;
        float attackSpeedMod;
        public OnTheHuntAuraBuff(float moveSpeedMod = default, float attackSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.attackSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}