namespace Spells
{
    public class OdinCaptureChannel : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 30f,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int hasPrilisasBlessing;
        Particle particleID;
        Particle particleID2;
        int chargeTimePassed;
        bool removeAnim;
        public override void ChannelingStart()
        {
            AddBuff(attacker, attacker, new Buffs.OdinCaptureImmobile(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.OdinGuardianSuppression(), 1, 1, 30, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.OdinCaptureChannel(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, true);
            hasPrilisasBlessing = 0;
            int prilisasBlessingCount = GetBuffCountFromAll(owner, nameof(Buffs.PrilisasBlessing));
            if (prilisasBlessingCount > 0)
            {
                hasPrilisasBlessing = 1;
                AddBuff(owner, target, new Buffs.PrilisasBlessing(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            TeamId teamOfOwner = GetTeamID_CS(owner);
            if (teamOfOwner == TeamId.TEAM_ORDER)
            {
                AddBuff(attacker, target, new Buffs.OdinGuardianSuppressionOrder(), 10, 1, 30, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out particleID, out particleID2, "OdinCaptureBeam.troy", "OdinCaptureBeam.troy", TeamId.TEAM_ORDER, 10, 0, TeamId.TEAM_CHAOS, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "crystal", default, false, false, false, false, false);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.OdinGuardianSuppressionChaos(), 10, 1, 30, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out particleID, out particleID2, "OdinCaptureBeam.troy", "OdinCaptureBeam.troy", TeamId.TEAM_CHAOS, 10, 0, TeamId.TEAM_ORDER, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "crystal", default, false, false, false, false, false);
            }
            chargeTimePassed = 0;
            float mana = GetPAR(target, PrimaryAbilityResourceType.MANA);
            float maxMana = GetMaxPAR(target, PrimaryAbilityResourceType.MANA);
            float pAR_Percent = mana / maxMana;
            if (pAR_Percent > 0.8f && owner.Team != target.Team)
            {
                AddBuff(owner, owner, new Buffs.OdinScoreNinja(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            PlayAnimation("Channel_WNDUP", 0, owner, true, true, false);
            removeAnim = true;
        }

        //TODO: Uncomment and fix
        public override void ChannelingUpdateActions()
        {
            float accumTime; // UNITIALIZED
                             // if(accumTime >= 1.5f)
                             // {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinShrineBombBuff)) > 0)
            {
                TeamId teamOfOwner = GetTeamID_CS(owner);
                if (teamOfOwner == TeamId.TEAM_ORDER)
                {
                    AddBuff(owner, target, new Buffs.OdinBombTickOrder(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else
                {
                    AddBuff(owner, target, new Buffs.OdinBombTickChaos(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                SpellBuffClear(owner, nameof(Buffs.OdinShrineBombBuff));
            }
            if (this.removeAnim)
            {
                UnlockAnimation(owner, true);
                PlayAnimation("Channel", 0, owner, true, true, false);
                this.removeAnim = false;
            }
            // }
            float distance = DistanceBetweenObjects(owner, target);
            if (distance >= 525)
            {
                SpellBuffClear(owner, nameof(Buffs.OdinShrineBombBuff));
                SpellBuffClear(owner, nameof(Buffs.OdinCaptureChannel));
            }
        }
        public override void ChannelingUpdateStats()
        {
            TeamId teamOfOwner;
            if (this.chargeTimePassed == 0)
            {
                float accumTime; // UNITIALIZED
                                 //  if(accumTime > 1.5f)
                                 //  {
                this.chargeTimePassed = 1;
                SpellEffectRemove(this.particleID);
                SpellEffectRemove(this.particleID2);
                teamOfOwner = GetTeamID_CS(owner);
                if (teamOfOwner == TeamId.TEAM_ORDER)
                {
                    SpellEffectCreate(out this.particleID, out this.particleID2, "OdinCaptureBeamEngaged_red.troy", "OdinCaptureBeamEngaged_green.troy", TeamId.TEAM_CHAOS, 10, 0, TeamId.TEAM_CHAOS, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "crystal", default, false, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out this.particleID, out this.particleID2, "OdinCaptureBeamEngaged_green.troy", "OdinCaptureBeamEngaged_red.troy", TeamId.TEAM_CHAOS, 10, 0, TeamId.TEAM_CHAOS, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "crystal", default, false, false, false, false, false);
                }
                // }
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCaptureChannel)) == 0)
            {
                StopChanneling(owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Move);
            }
            teamOfOwner = GetTeamID_CS(owner);
            if (teamOfOwner == TeamId.TEAM_ORDER)
            {
                if (GetBuffCountFromCaster(target, owner, nameof(Buffs.OdinGuardianSuppressionOrder)) == 0)
                {
                    StopChanneling(owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Move);
                }
            }
            else
            {
                if (GetBuffCountFromCaster(target, owner, nameof(Buffs.OdinGuardianSuppressionChaos)) == 0)
                {
                    StopChanneling(owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Move);
                }
            }
            TeamId targetTeam = GetTeamID_CS(target);
            TeamId myTeam = GetTeamID_CS(owner);
            if (myTeam == targetTeam)
            {
                StopChanneling(owner, ChannelingStopCondition.Success, ChannelingStopSource.Move);
            }
        }

        public override void ChannelingSuccessStop()
        {
            float cooldownToSet;
            SpellEffectRemove(particleID);
            SpellEffectRemove(particleID2);
            SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinGuardianSuppressionChaos), 1);
            SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinGuardianSuppressionOrder), 1);
            if (IsDead(target))
            {
                cooldownToSet = 0;
            }
            else
            {
                SetUseSlotSpellCooldownTime(3, owner, false);
                cooldownToSet = 3;
                AddBuff(owner, owner, new Buffs.OdinCaptureChannelCooldownBuff(), 1, 1, cooldownToSet, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            SpellBuffClear(owner, nameof(Buffs.OdinScoreNinja));
            UnlockAnimation(owner, true);
            if (hasPrilisasBlessing == 1)
            {
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.PrilisasBlessing), 1);
            }
            SpellBuffClear(owner, nameof(Buffs.OdinCaptureChannel));
        }
        public override void ChannelingCancelStop()
        {
            float cooldownToSet;
            SpellEffectCreate(out _, out _, "OdinCaptureCancel.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, "spine", default, false, false, false, false, false);
            UnlockAnimation(owner, true);
            SpellEffectRemove(particleID);
            SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinGuardianSuppressionChaos), 1);
            SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinGuardianSuppressionOrder), 1);
            SpellEffectRemove(particleID2);
            SpellBuffClear(owner, nameof(Buffs.OdinScoreNinja));
            if (IsDead(target))
            {
                cooldownToSet = 0;
            }
            else
            {
                SetUseSlotSpellCooldownTime(3, owner, false);
                cooldownToSet = 3;
                AddBuff(owner, owner, new Buffs.OdinCaptureChannelCooldownBuff(), 1, 1, cooldownToSet, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            if (hasPrilisasBlessing == 1)
            {
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.PrilisasBlessing), 1);
            }
            SpellBuffClear(owner, nameof(Buffs.OdinCaptureChannel));
        }
    }
}
namespace Buffs
{
    public class OdinCaptureChannel : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
        float channelStartTime;
        public override void OnActivate()
        {
            channelStartTime = GetBuffStartTime(owner, nameof(Buffs.OdinCaptureChannel));
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (attacker is Champion && damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC)
            {
                string buffName = GetDamagingBuffName();
                float damageStartTime = GetBuffStartTime(owner, buffName);
                bool cancelChannel = false;
                if (damageStartTime == 0)
                {
                    cancelChannel = true;
                }
                if (damageStartTime >= channelStartTime)
                {
                    cancelChannel = true;
                }
                if (cancelChannel)
                {
                    SpellEffectCreate(out _, out _, "Ezreal_essenceflux_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
                    SpellBuffClear(owner, nameof(Buffs.OdinCaptureChannel));
                    IssueOrder(owner, OrderType.Hold, default, owner);
                    AddBuff(attacker, owner, new Buffs.OdinCaptureInterrupt(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}
namespace PreLoads
{
    public class OdinCaptureChannel : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("odincapturechannel");
            PreloadParticle("ezreal_essenceflux_tar.troy");
            PreloadSpell("root");
            PreloadSpell("odincaptureinterrupt");
            PreloadSpell("odincaptureimmobile");
            PreloadSpell("odinguardiansuppression");
            PreloadSpell("prilisasblessing");
            PreloadSpell("odinguardiansuppressionorder");
            PreloadParticle("odincapturebeam.troy");
            PreloadSpell("odinguardiansuppressionchaos");
            PreloadSpell("odinscoreninja");
            PreloadSpell("odinshrinebombbuff");
            PreloadSpell("odinbombtickorder");
            PreloadSpell("odinbombtickchaos");
            PreloadParticle("odincapturebeamengaged_red.troy");
            PreloadParticle("odincapturebeamengaged_green.troy");
            PreloadSpell("odincapturechannelcooldownbuff");
            PreloadParticle("odincapturecancel.troy");
        }
    }
}