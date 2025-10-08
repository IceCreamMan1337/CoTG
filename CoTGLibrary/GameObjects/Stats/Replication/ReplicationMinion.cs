using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
namespace CoTG.CoTGServer.GameObjects.StatsNS
{
    public class ReplicationMinion : Replication
    {
        public ReplicationMinion(Minion owner) : base(owner)
        {
        }
        internal override void Update()
        {
            //LOCAL_REP_DATA1 = 1 //SetupReplicationInfo
            UpdateFloat(0, Stats.CurrentHealth, 1, 0); //mHP
            UpdateFloat(0, Stats.CurrentMana, 1, 1); //mMP
            UpdateFloat(0, Stats.HealthPoints.Total, 1, 2); //mMaxHP
            UpdateFloat(0, Stats.ManaPoints.Total, 1, 3); //mMaxMP


            //FillLocalRepData 
            //todo fix actionstate
            UpdateUint((uint)Stats.ActionState, 1, 4); //ActionState

            //    UpdateUint((uint)(Stats.ActionState | ActionState.IS_GHOSTED), 1, 4); //ActionState
            UpdateBool(Stats.IsMagicImmune, 1, 5); //MagicImmune
            UpdateBool(Owner.IsInvulnerable, 1, 6); //IsInvulnerable

            UpdateBool(Owner.IsTargetable, 1, 7); //mIsTargetable
            UpdateUint((uint)Stats.IsTargetableToTeam, 1, 8); //mIsTargetableToTeam
            UpdateBool(Owner.IsLifestealImmune, 1, 9); //mIsPhysicalImmune


            //FillBasicLocalRepData
            UpdateFloat(0, Stats.AttackDamage.TotalBase, 1, 10); //mBaseAttackDamage // totalbase ? 
            UpdateFloat(0, Stats.Armor.Total, 1, 11); //mArmor
            UpdateFloat(0, Stats.MagicResist.Total, 1, 12); //mSpellBlock
            UpdateFloat(2, Stats.AttackSpeedMultiplier.Total, 1, 13); //mAttackSpeedMod
            UpdateFloat(0, Stats.AttackDamage.FlatBonus, 1, 14); //mFlatPhysicalDamageMod
            UpdateFloat(2, Stats.AttackDamage.PercentBonus, 1, 15); //mPercentPhysicalDamageMod
            UpdateFloat(0, Stats.AbilityPower.Total, 1, 16); //mFlatMagicDamageMod
            UpdateFloat(0, Stats.AbilityPower.Total, 1, 17); //mPercentMagicDamageMod

            UpdateFloat(1, Stats.HealthRegeneration.Total, 1, 18); //mHPRegenRate
            UpdateFloat(1, Stats.ManaRegeneration.Total, 1, 19); //mPARRegenRate
            UpdateFloat(0, Stats.MagicalReduction.TotalFlat, 1, 20); //mFlatMagicReduction
                                                                     // UpdateFloat(0, Stats.MagicalReduction.PercentBonus, 1, 21); //mFlatMagicReduction

            //MAPREPDATA = 3 FillBasicMapRepData
            UpdateFloat(0, Stats.PerceptionRange.FlatBonus, 3, 0); //mFlatBubbleRadiusMod
            UpdateFloat(2, Stats.PerceptionRange.TotalPercent, 3, 1); //mPercentBubbleRadiusMod
            UpdateFloat(2, Stats.MoveSpeed.Total, 3, 2); //mMoveSpeed
            UpdateFloat(2, Stats.Size.Total, 3, 3); //mSkinScaleCoef(mistyped as mCrit)

            //mDebugDrawRadius
#if DEBUG_AB || RELEASE_AB
            UpdateFloat(2, Owner.CollisionRadius, 3, 4);
#endif
        }
    }
}