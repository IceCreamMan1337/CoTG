using CoTGEnumNetwork.Enums;
using CoTGLibrary.GameObjects.Stats;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;

namespace CoTG.CoTGServer.GameObjects.StatsNS
{
    public class ReplicationHero : Replication
    {
        new Champion Owner;
        public ReplicationHero(Champion owner) : base(owner)
        {
            Owner = owner;
        }

        public EvolutionState EvolutionState => Owner.EvolutionState;

        internal override void Update()
        {
            /* CharacterIntermediateHelper::FillHeroLocalRepData
         * CharacterIntermediateHelper::FillHeroLocalRepData

         * */
            // CLIENT_ONLY_REP_DATA = 0 
            UpdateFloat(0, Owner.Experience.Exp, 0, 0); //mExp
            UpdateFloat(0, Owner.GoldOwner.Gold, 0, 1); //mGold
                                                        //TODO: These
            UpdateUint((uint)Stats.SpellsEnabled, 0, 2); //mReplicatedSpellCanCastBits
            UpdateUint(3, 0, 3); //this seem enable summonerspell

            //UpdateUint((uint)(Stats.SpellsEnabled >> 32), 0, 3); //mReplicatedSpellCanCastBitsUpper1
            // UpdateUint((uint)Stats.SummonerSpellsEnabled, 0, 4); //mReplicatedSpellCanCastBitsLower2
            // UpdateUint((uint)(Stats.SummonerSpellsEnabled >> 32), 0, 5); //mReplicatedSpellCanCastBitsUpper2
            // UpdateUint((uint)Stats.SpellsEnabled, 0, 3); //mReplicatedSummonerSpellCanCastBits

            //TODO: this
            for (short i = 0; i < 4; i++)
            {
                UpdateFloat(0, Owner.Spells[i].ManaCost, 0, 4 + i);
            }

            //SpellSlots
            //ManaCost_{i}
            /* for (short i = 0; i < 16; i++)
                 UpdateFloat(0, Owner.Spells[(short)(SpellSlotType.ExtraSlots + i)].ManaCost, 0, 7 + i); //ManaCost_Ex{i}
            */

            //   

            //LOCAL_REP_DATA2 = 2 
            UpdateBool(Stats.IsMagicImmune, 2, 0); //MagicImmune
            UpdateBool(Owner.IsInvulnerable, 2, 1); //IsInvulnerable
                                                    //    
                                                    //     //mIsTargetableToTeamFlags
                                                    //    
                                                    //// LOCAL_REP_DATA1 and npc_LocalRepData1_4 = 1
                                                    // charState->mStates.mIndex = 0 
                                                    //FillLocalRepData

            //67109903 in replay of CS for stealthed 
            if (Stats.ActionState == ActionState.STEALTHED)
            {
                UpdateUint(67109903, 1, 0);
            }
            else
            {
                UpdateUint((uint)Stats.ActionState, 1, 0); //ActionState  in past we get | ActionState.IS_GHOSTED seem an hack 
            }
            UpdateBool(Stats.IsMagicImmune, 1, 1);
            UpdateBool(Owner.IsInvulnerable, 1, 2);
            UpdateBool(Owner.IsTargetable, 1, 3); //IsPhysicalImmune
            UpdateUint((uint)Stats.IsTargetableToTeam, 1, 4);
            UpdateBool(Stats.IsPhysicalImmune, 1, 5);
            //FillHeroLocalRepData
            UpdateFloat(0, Stats.AttackDamage.TotalBase, 1, 6); //mBaseAttackDamage
            UpdateFloat(0, Stats.AbilityPower.TotalBase, 1, 7); //mBaseAbilityDamage
            UpdateFloat(2, Stats.DodgeChance.Total, 1, 8); //mDodge
            UpdateFloat(2, Stats.CriticalChance.Total, 1, 9); //mCrit
            UpdateFloat(0, Stats.Armor.Total, 1, 10); //mArmor
            UpdateFloat(0, Stats.MagicResist.Total, 1, 11); //mSpellBlock
            UpdateFloat(1, Stats.HealthRegeneration.Total, 1, 12); //mHPRegenRate
            UpdateFloat(1, Stats.ManaRegeneration.Total, 1, 13); //mPARRegenRate
            UpdateFloat(0, Stats.Range.Total, 1, 14); //mAttackRange
            UpdateFloat(0, Stats.AttackDamage.FlatBonus, 1, 15); //mFlatPhysicalDamageMod
            UpdateFloat(2, Stats.AttackDamage.PercentBonus, 1, 16); //mPercentPhysicalDamageMod
                                                                    //todo: 
            UpdateFloat(0, Stats.AbilityPower.FlatBonus, 1, 17); //mFlatMagicDamageMod
            UpdateFloat(0, Stats.AbilityPower.PercentBonus, 1, 18); //mPercentMagicDamageMod // possibly spellvamp ? 
            UpdateFloat(0, Stats.MagicalReduction.TotalFlat, 1, 19); //mFlatMagicReduction
                                                                     //so when we see replay  20 = attackspeed , but codesource is mPercentMagicReduction ??? 
                                                                     // UpdateFloat(2, Stats.MagicalReduction.TotalPercent, 1, 20); //mPercentMagicReduction
            UpdateFloat(2, Stats.AttackSpeedMultiplier.Total, 1, 20); //mPercentMagicReduction
                                                                      //so what is 21 ? 
            UpdateFloat(2, Stats.DodgeChance.Total, 1, 21); //dodge
            UpdateFloat(2, -Stats.CooldownReduction.Total, 1, 22); //mPercentCooldownMo
            UpdateFloat(2, Stats.ArmorPenetration.TotalFlat, 1, 23); //flat armor pen 
            UpdateFloat(2, -Stats.ArmorPenetration.TotalPercent + 1f, 1, 24); //mPercentArmorPenetration
            UpdateFloat(2, Stats.MagicPenetration.TotalFlat, 1, 25); //mFlatMagicPenetration
            UpdateFloat(2, -Stats.MagicPenetration.TotalPercent + 1f, 1, 26); //flat magic pen 

            UpdateFloat(2, Stats.LifeSteal.Total, 1, 27); //steallife


            UpdateFloat(2, Stats.SpellVamp.Total, 1, 28); //spellvamp 


            //why two times rito ? 
            //  UpdateFloat(2, -Stats.CooldownReduction.Total, 1, 30); //mPercentCooldownMod

            //   UpdateFloat(0, Stats.PerceptionRange.FlatBonus, 3, 0); //mFlatBubbleRadiusMod
            //   UpdateFloat(2, Stats.PerceptionRange.TotalPercent, 3, 1); //mPercentBubbleRadiusMod
            //   UpdateFloat(2, Stats.MoveSpeed.Total, 3, 2); //mMoveSpeed
            //   UpdateFloat(2, Stats.Size.Total, 3, 3); //mSkinScaleCoef(mistyped as mCrit)


            //MAP_REPDATA = 3 
            UpdateFloat(0, Stats.CurrentHealth, 3, 0); //mHP
            UpdateFloat(0, Stats.CurrentMana, 3, 1); //mMP
            UpdateFloat(0, Stats.HealthPoints.Total, 3, 2); //mMaxHP
            UpdateFloat(0, Stats.ManaPoints.Total, 3, 3); //mMaxHP

            //FillHeroMapRepData
            UpdateFloat(0, Stats.PerceptionRange.FlatBonus, 3, 4);
            UpdateFloat(2, Stats.PerceptionRange.TotalPercent, 3, 5);
            UpdateFloat(2, Stats.MoveSpeed.Total, 3, 6);
            UpdateFloat(2, Stats.Size.Total, 3, 7);
            //mDebugDrawRadius
            //    
            //mPercentBubbleRadiusMod
            //mMoveSpeed
            //mSkinScaleCoef(mistyped as mCrit)
            UpdateInt(Owner.Experience.Level, 3, 8);
            //todo : mDebugDrawRadius.mIndex
#if DEBUG_AB || RELEASE_AB
            UpdateFloat(2, Owner.CollisionRadius, 3, 9);
            UpdateInt(Owner.ChampionStatistics.NeutralMinionsKilled, 3, 10);
#else
            UpdateInt(Owner.ChampionStatistics.NeutralMinionsKilled, 3, 9);
#endif
        }
    }
}