using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;

namespace CoTG.CoTGServer.GameObjects.StatsNS
{
    public class ReplicationAITurret : Replication
    {
        public ReplicationAITurret(BaseTurret owner) : base(owner)
        {
        }
        internal override void Update()
        {
            //FillBasicLocalRepDataTower


            //LOCAL_REP_DATA1 = 1 
            UpdateFloat(0, Stats.AttackDamage.TotalBase, 1, 0); //mBaseAttackDamage
            UpdateFloat(0, Stats.Armor.Total, 1, 1); //mArmor
            UpdateFloat(0, Stats.MagicResist.Total, 1, 2); //mSpellBlock

            UpdateFloat(2, Stats.AttackSpeedMultiplier.Total, 1, 3); //mAttackSpeedMod
            UpdateFloat(0, Stats.AttackDamage.FlatBonus, 1, 4); //mFlatPhysicalDamageMod
            UpdateFloat(2, Stats.AttackDamage.PercentBonus, 1, 5); //mPercentPhysicalDamageMod
            UpdateFloat(2, Stats.AbilityPower.Total, 1, 6); //mFlatMagicDamageMod
            UpdateFloat(0, Stats.CooldownReduction.Total, 1, 7); //mPercentCooldownMod


            //   UpdateFloat(1, Stats.HealthRegeneration.Total, 1, 7); //mHPRegenRate
            //SetupReplicationInfo
            UpdateFloat(1, Stats.CurrentHealth, 1, 8);
            UpdateFloat(1, Stats.HealthPoints.Total, 1, 9);

            //FillLocalRepData
            UpdateUint((uint)(Stats.ActionState | ActionState.IS_GHOSTED), 1, 10);
            UpdateBool(Stats.IsMagicImmune, 1, 11); //mHPRegenRate
            UpdateBool(Owner.IsInvulnerable, 1, 12); //mHPRegenRate
            UpdateBool(Owner.IsTargetable, 1, 13); //mHPRegenRate
            UpdateUint((uint)Stats.IsTargetableToTeam, 1, 14); //mHPRegenRate
            UpdateBool(Stats.IsPhysicalImmune, 1, 15); //mHPRegenRate

            //FillBasicMapRepData //MAP_REPDATA = 3 
            UpdateFloat(0, Stats.PerceptionRange.FlatBonus, 3, 0); //mFlatBubbleRadiusMod
            UpdateFloat(2, Stats.PerceptionRange.TotalPercent, 3, 1); //mPercentBubbleRadiusMod
            UpdateFloat(0, Stats.MoveSpeed.Total, 3, 2); //mMoveSpeed
            UpdateFloat(2, Stats.Size.Total, 3, 3); //mSkinScaleCoef(mistyped as mCrit)

            //SetupReplicationInfo 
            //mDebugDrawRadius
            if (Game.Config.ABClient)
            {

                UpdateFloat(2, Owner.CollisionRadius, 3, 4);
            }


        }
    }
}
