using CoTG.CoTGServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;

namespace CoTG.CoTGServer.GameObjects.StatsNS
{
    public class ReplicationAnimatedBuilding : Replication
    {
        public ReplicationAnimatedBuilding(ObjAnimatedBuilding owner) : base(owner)
        {
        }

        internal override void Update()
        {
            // Fix126
            //FillBasicLocalRepDataAIProp
            UpdateFloat(1, Stats.CurrentHealth, 1, 0); //mHP
                                                       //    UpdateFloat(1, Stats.CurrentHealth, 1, 1); //mHP
            UpdateBool(Owner.IsInvulnerable, 1, 1); //IsInvulnerable
            UpdateBool(Owner.IsTargetable, 1, 2); //mIsTargetable
            UpdateUint((uint)Stats.IsTargetableToTeam, 1, 3); //mIsTargetableToTeamFlags

            //MAPREPDATA = 3 FillBasicMapRepData
            /*   UpdateFloat(0, Stats.PerceptionRange.FlatBonus, 3, 0); //mFlatBubbleRadiusMod
               UpdateFloat(2, Stats.PerceptionRange.TotalPercent, 3, 1); //mPercentBubbleRadiusMod
               UpdateFloat(2, Stats.MoveSpeed.Total, 3, 2); //mMoveSpeed
               UpdateFloat(2, Stats.Size.Total, 3, 3); //mSkinScaleCoef(mistyped as mCrit)

               */
            //mDebugDrawRadius
            if (Game.Config.ABClient)
            {

                UpdateFloat(2, Owner.CollisionRadius, 1, 4);
            }
        }
    }
}
