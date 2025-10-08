using System;
using System.IO;
using System.Numerics;
using CoTG.CoTGServer.Content.FileSystem;
//using System.Text.Json.Serialization;
using CoTGEnumNetwork.Enums;

namespace CoTG.CoTGServer.Content
{
    public class SpellData
    {
        public string AfterEffectName { get; set; } = "";

        //AIEndOnly
        //AILifetime
        //AIRadius
        //AIRange
        //AISendEvent
        //AISpeed
        public string AlternateName { get; set; } = "";

        public bool AlwaysSnapFacing { get; set; }

        //AmmoCountHiddenInUI
        public float[] AmmoRechargeTime { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        public string AnimationLeadOutName { get; set; } = "";
        public string AnimationLoopName { get; set; } = "";
        public string AnimationName { get; set; } = "";

        public string AnimationWinddownName { get; set; } = "";

        //ApplyAttackDamage
        //ApplyAttackEffect
        //ApplyMaterialOnHitSound
        public bool BelongsToAvatar { get; set; }
        public float BounceRadius { get; set; } = 450;
        public bool CanCastWhileDisabled { get; set; }
        public float CancelChargeOnRecastTime { get; set; }
        public bool CanMoveWhileChanneling { get; set; }
        public bool CannotBeSuppressed { get; set; }
        public bool CanOnlyCastWhileDead { get; set; }
        public bool CanOnlyCastWhileDisabled { get; set; }
        public bool CantCancelWhileChanneling { get; set; }
        public bool CantCancelWhileWindingUp { get; set; }
        public bool CantCastWhileRooted { get; set; }
        public float CastConeAngle { get; set; } = 45;
        public float CastConeDistance { get; set; } = 400;
        public float CastFrame { get; set; }
        public float[] CastRadius { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };

        public float CastRadiusSecondary { get; set; } = 0.0f;

        //CastRadiusSecondaryTexture
        //CastRadiusTexture
        public float[] CastRange { get; set; } = { 400, 400, 400, 400, 400, 400, 400 };
        public float CastRangeDisplayOverride { get; set; }

        //CastRangeTextureOverrideName
        public bool CastRangeUseBoundingBoxes { get; set; }
        public float CastTargetAdditionalUnitsRadius { get; set; }
        public CastType CastType { get; set; }
        public float[] ChannelDuration { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        public float ChargeUpdateInterval { get; set; }
        public float CircleMissileAngularVelocity { get; set; }
        public float CircleMissileRadialVelocity { get; set; }
        public float Coefficient { get; set; }

        public float Coefficient2 { get; set; }

        //ClientOnlyMissileTargetBoneName
        public bool ConsideredAsAutoAttack { get; set; }

        public float[] Cooldown { get; set; } = [0, 0, 0, 0, 0, 0, 0];

        //CursorChangesInGrass
        //CursorChangesInTerrain
        public float DeathRecapPriority { get; set; }
        public float DelayCastOffsetPercent { get; set; } // spell buffering window

        public float DelayTotalTimePercent { get; set; }

        //Description
        //DisableCastBar
        //DisplayName
        public bool DoesntBreakChannels { get; set; }

        public bool DoNotNeedToFaceTarget { get; set; }

        //DrawSecondaryLineIndicator
        //DynamicExtended
        //string DynamicTooltip
        public float[] Effect1Amount { get; set; } = [0, 0, 0, 0, 0, 0, 0];
        public float[] Effect2Amount { get; set; } = [0, 0, 0, 0, 0, 0, 0];
        public float[] Effect3Amount { get; set; } = [0, 0, 0, 0, 0, 0, 0];
        public float[] Effect4Amount { get; set; } = [0, 0, 0, 0, 0, 0, 0];
        public float[] Effect5Amount { get; set; } = [0, 0, 0, 0, 0, 0, 0];
        public float[] Effect6Amount { get; set; } = [0, 0, 0, 0, 0, 0, 0];

        public SpellDataFlags Flags { get; set; }

        //FloatStaticsDecimalsX
        //FloatVarsDecimalsX
        public bool HaveAfterEffect { get; set; }
        public bool HaveHitBone { get; set; }
        public bool HaveHitEffect { get; set; }

        public bool HavePointEffect { get; set; }

        //HideRangeIndicatorWhenCasting
        public string HitBoneName { get; set; } = "";
        public string HitEffectName { get; set; } = "";

        public int HitEffectOrientType { get; set; } = 1;

        //HitEffectPlayerName
        public bool IgnoreAnimContinueUntilCastFrame { get; set; }

        public bool IgnoreRangeCheck { get; set; }

        //InventoryIconX
        public bool IsDisabledWhileDead { get; set; } = true;

        public bool IsToggleSpell { get; set; }

        //KeywordWhenAcquired
        //LevelXDesc
        public float LineDragLength { get; set; }
        public int LineMissileBounces { get; set; }
        public bool LineMissileCollisionFromStartPoint { get; set; }
        public float LineMissileDelayDestroyAtEndSeconds { get; set; } // Always 0 in 4.20
        public bool LineMissileEndsAtTargetPoint { get; set; }
        public bool LineMissileFollowsTerrainHeight { get; set; }
        public float LineMissileTargetHeightAugment { get; set; } = 100;
        public float LineMissileTimePulseBetweenCollisionSpellHits { get; set; } // Always 0 in 4.20
        public bool LineMissileTrackUnits { get; set; }

        public bool LineMissileUsesAccelerationForBounce { get; set; }

        //LineTargetingBaseTextureOverrideName
        //LineTargetingBaseTextureOverrideName
        public float LineWidth { get; set; }
        public float[] LocationTargettingLength { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        public float[] LocationTargettingWidth { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };

        public bool LockConeToPlayer { get; set; }

        //LookAtPolicy
        public float LuaOnMissileUpdateDistanceInterval { get; set; }

        public float[] ManaCost { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };

        //Map_X_EffectYLevelZAmmount
        public int MaxAmmo { get; set; } = 1;

        //MaxGrowthRangeTextureName
        //MinimapIcon
        //MinimapIconDisplayFlag
        //MinimapIconRotation
        public float MissileAccel { get; set; }
        public string MissileBoneName { get; set; } = "";
        public string MissileEffect { get; set; } = "";
        public string MissileEffectPlayer { get; set; } = "";
        public float MissileFixedTravelTime { get; set; }
        public bool MissileFollowsTerrainHeight { get; set; }
        public float MissileGravity { get; set; }
        public float MissileLifetime { get; set; }
        public float MissileMaxSpeed { get; set; }
        public float MissileMinSpeed { get; set; }
        public float MissileMinTravelTime { get; set; }
        public float MissilePerceptionBubbleRadius { get; set; }
        public bool MissilePerceptionBubbleRevealsStealth { get; set; } // Always 0 in 4.20
        public float MissileSpeed { get; set; } = 500;
        public float MissileTargetHeightAugment { get; set; } = 100;
        public bool MissileUnblockable { get; set; }

        public bool NoWinddownIfCancelled { get; set; }

        //NumSpellTargeters
        //OrientRadiusTextureFromPlayer
        //OrientRangeIndicatorToCursor
        //OrientRangeIndicatorToFacing
        public float OverrideCastTime { get; set; } = -1f;

        public Vector3 ParticleStartOffset { get; set; } = new(0, 0, 0);

        //PlatformEnabled
        public string PointEffectName { get; set; } = "";

        //RangeIndicatorTextureName
        public string RequiredUnitTags { get; set; } = "";

        public string SelectionPreference { get; set; } = "";

        //Sound_CastName
        //Sound_HitName
        //Sound_VOEventCategory
        public float SpellCastTime { get; set; }
        public float SpellRevealsChampion { get; set; }
        public float SpellTotalTime { get; set; }
        public float StartCooldown { get; set; }

        public bool SubjectToGlobalCooldown { get; set; } = true;

        //TargeterConstrainedToRange
        public TargetingType TargetingType { get; set; } = TargetingType.Target;
        public string TextFlags { get; set; } = "";
        public bool TriggersGlobalCooldown { get; set; } = true;
        public bool UpdateRotationWhenCasting { get; set; } = true;
        public bool UseAnimatorFramerate { get; set; }
        public bool UseAutoattackCastTime { get; set; }
        public bool UseChargeChanneling { get; set; }
        public bool UseChargeTargeting { get; set; }
        public bool UseGlobalLineIndicator { get; set; }

        public bool UseMinimapTargeting { get; set; }
        //Version
        //x1,x2,x3,x4,x5

        public float GetCastTime()
        {
            if (DelayCastOffsetPercent == 0 && OverrideCastTime == 0)
                return 0;
            else
            {
                return CastFrame >= 0
                    ? CastFrame *
                      (1f / 60f) //need find correctframerate , and check if is in function of DelayCastOffsetPercent
                    : (1.0f + DelayCastOffsetPercent) * 0.5f;
            }
        }


        // TODO: Implement this (where it is verified to be needed)
        public float GetCastTimeTotal()
        {
            return (1.0f + DelayTotalTimePercent) * 2.0f;
        }

        // TODO: read Global Character Data constants from constants.var (gcd_AttackDelay = 1.600f, gcd_AttackDelayCastPercent = 0.300f)
        public float GetCharacterAttackDelay
        (
            float attackSpeedMod,
            float attackDelayOffsetPercent,
            float attackMinimumDelay = 0.4f,
            float attackMaximumDelay = 5.0f
        )
        {
            float result = (attackDelayOffsetPercent + 1.0f) * 1.600f / attackSpeedMod;
            return Math.Clamp(result, attackMinimumDelay, attackMaximumDelay);
        }

        public float GetCharacterAttackCastDelay
        (
            float attackSpeedMod,
            float attackDelayOffsetPercent,
            float attackDelayCastOffsetPercent,
            float attackDelayCastOffsetPercentAttackSpeedRatio,
            float attackMinimumDelay = 0.4f,
            float attackMaximumDelay = 5.0f
        )
        {
            float castPercent = Math.Min(0.300f + attackDelayCastOffsetPercent, 0.0f);
            float percentDelay =
                GetCharacterAttackDelay(1.0f, attackDelayOffsetPercent, attackMinimumDelay, attackMaximumDelay) *
                castPercent;
            float attackDelay = GetCharacterAttackDelay(attackSpeedMod, attackDelayCastOffsetPercent,
                attackMinimumDelay, attackMaximumDelay);
            float result =
                (((attackDelay * castPercent) - percentDelay) * attackDelayCastOffsetPercentAttackSpeedRatio) +
                percentDelay;
            return Math.Min(result, attackDelay);
        }

        public SpellData()
        {
        }

        public SpellData(string name)
        {
            RFile? f = Cache.GetFile(Path.Join(ContentManager.SpellsPath, name + ".ini"));
            if (f is null)
            {
                return;
            }

            AfterEffectName = f.GetValue("SpellData", "AfterEffectName", AfterEffectName);
            //AIEndOnly
            //AILifetime
            //AIRadius
            //AIRange
            //AISendEvent
            //AISpeed
            AlternateName = f.GetValue("SpellData", "AlternateName", name);
            AlwaysSnapFacing = f.GetValue("SpellData", "AlwaysSnapFacing", AlwaysSnapFacing);
            //AmmoCountHiddenInUI
            float lastValidTime = 0;
            for (var i = 1; i <= 6 + 1; i++)
            {
                float time = f.GetValue("SpellData", $"AmmoRechargeTime{i}", 0f);

                if (time > 0)
                {
                    AmmoRechargeTime[i - 1] = time;
                    lastValidTime = time;
                }
                else
                {
                    AmmoRechargeTime[i - 1] = lastValidTime;
                }
            }

            AnimationLeadOutName = f.GetValue("SpellData", "AnimationLeadOutName", name);
            AnimationLoopName = f.GetValue("SpellData", "AnimationLoopName", name);
            AnimationName = f.GetValue("SpellData", "AnimationName", name);
            AnimationWinddownName = f.GetValue("SpellData", "AnimationWinddownName", name);
            Coefficient = f.GetValue("SpellData", "Coefficient", Coefficient);
            //ApplyAttackDamage
            //ApplyAttackEffect
            //ApplyMaterialOnHitSound
            BelongsToAvatar = f.GetValue("SpellData", "BelongsToAvatar", BelongsToAvatar);
            BounceRadius = f.GetValue("SpellData", "BounceRadius", BounceRadius);
            CanCastWhileDisabled = f.GetValue("SpellData", "CanCastWhileDisabled", CanCastWhileDisabled);
            CancelChargeOnRecastTime = f.GetValue("SpellData", "CancelChargeOnRecastTime", CancelChargeOnRecastTime);
            CanMoveWhileChanneling = f.GetValue("SpellData", "CanMoveWhileChanneling", CanMoveWhileChanneling);
            CannotBeSuppressed = f.GetValue("SpellData", "CannotBeSuppressed", CannotBeSuppressed);
            CanOnlyCastWhileDead = f.GetValue("SpellData", "CanOnlyCastWhileDead", CanOnlyCastWhileDead);
            CanOnlyCastWhileDisabled = f.GetValue("SpellData", "CanOnlyCastWhileDisabled", CanOnlyCastWhileDisabled);
            CantCancelWhileChanneling = f.GetValue("SpellData", "CantCancelWhileChanneling", CantCancelWhileChanneling);
            CantCancelWhileWindingUp = f.GetValue("SpellData", "CantCancelWhileWindingUp", CantCancelWhileWindingUp);
            CantCastWhileRooted = f.GetValue("SpellData", "CantCastWhileRooted", CantCastWhileRooted);
            CastConeAngle = f.GetValue("SpellData", "CastConeAngle", CastConeAngle);
            CastConeDistance = f.GetValue("SpellData", "CastConeDistance", CastConeDistance);
            CastFrame = f.GetValue("SpellData", "CastFrame", CastFrame);
            CastRadiusSecondary = f.GetValue("SpellData", "CastRadiusSecondary", 0.0f);
            //CastRadiusSecondaryTexture
            //CastRadiusTexture
            CastRangeDisplayOverride = f.GetValue("SpellData", "CastRangeDisplayOverride", CastRangeDisplayOverride);
            //CastRangeTextureOverrideName
            CastRangeUseBoundingBoxes = f.GetValue("SpellData", "CastRangeUseBoundingBoxes", CastRangeUseBoundingBoxes);
            CastTargetAdditionalUnitsRadius = f.GetValue("SpellData", "CastTargetAdditionalUnitsRadius",
                CastTargetAdditionalUnitsRadius);
            CastType = (CastType)f.GetValue("SpellData", "CastType", (int)CastType);
            ChargeUpdateInterval = f.GetValue("SpellData", "ChargeUpdateInterval", ChargeUpdateInterval);
            CircleMissileAngularVelocity =
                f.GetValue("SpellData", "CircleMissileAngularVelocity", CircleMissileAngularVelocity);
            CircleMissileRadialVelocity =
                f.GetValue("SpellData", "CircleMissileRadialVelocity", CircleMissileRadialVelocity);
            //ClientOnlyMissileTargetBoneName
            ConsideredAsAutoAttack = f.GetValue("SpellData", "ConsideredAsAutoAttack", ConsideredAsAutoAttack);

            CastRadius[0] = f.GetValue("SpellData", "CastRadius", 0.0f);
            Cooldown[0] = f.GetValue("SpellData", "Cooldown", 0.0f);
            ChannelDuration[0] = f.GetValue("SpellData", "ChannelDuration", 0.0f);
            CastRange[0] = f.GetValue("SpellData", "CastRange", 0.0f);
            for (int i = 1; i <= 6; i++)
            {
                Cooldown[i] = f.GetValue("SpellData", $"Cooldown{i}", Cooldown[i - 1]);
                CastRadius[i] = f.GetValue("SpellData", $"CastRadius{i}", CastRadius[i - 1]);
                ChannelDuration[i] = f.GetValue("SpellData", $"ChannelDuration{i}", ChannelDuration[i - 1]);
                CastRange[i] = f.GetValue("SpellData", "CastRange", CastRange[i - 1]);
            }

            //CursorChangesInGrass
            //CursorChangesInTerrain
            DeathRecapPriority = f.GetValue("SpellData", "DeathRecapPriority", DeathRecapPriority);
            DelayCastOffsetPercent = f.GetValue("SpellData", "DelayCastOffsetPercent", DelayCastOffsetPercent);
            DelayTotalTimePercent = f.GetValue("SpellData", "DelayTotalTimePercent", DelayTotalTimePercent);
            //Description
            //DisableCastBar
            //DisplayName
            DoesntBreakChannels = f.GetValue("SpellData", "DoesntBreakChannels", DoesntBreakChannels);
            DoNotNeedToFaceTarget = f.GetValue("SpellData", "DoNotNeedToFaceTarget", DoNotNeedToFaceTarget);
            //DrawSecondaryLineIndicator
            //DynamicExtended
            //string DynamicTooltip
            //EffectXLevelYAmount

            for (int i = 0; i < 7; i++)
            {
                Effect1Amount[i] = f.GetValue("SpellData", $"Effect1Level{i}Amount", 0f);
                Effect2Amount[i] = f.GetValue("SpellData", $"Effect2Level{i}Amount", 0f);
                Effect3Amount[i] = f.GetValue("SpellData", $"Effect3Level{i}Amount", 0f);
                Effect4Amount[i] = f.GetValue("SpellData", $"Effect4Level{i}Amount", 0f);
                Effect5Amount[i] = f.GetValue("SpellData", $"Effect5Level{i}Amount", 0f);
                Effect6Amount[i] = f.GetValue("SpellData", $"Effect6Level{i}Amount", 0f);
            }

            Flags = (SpellDataFlags)f.GetValue("SpellData", "Flags", (int)Flags);
            //FloatStaticsDecimalsX
            //FloatVarsDecimalsX
            HaveAfterEffect = f.GetValue("SpellData", "HaveAfterEffect", HaveAfterEffect);
            HaveHitBone = f.GetValue("SpellData", "HaveHitBone", HaveHitBone);
            HaveHitEffect = f.GetValue("SpellData", "HaveHitEffect", HaveHitEffect);
            HavePointEffect = f.GetValue("SpellData", "HavePointEffect", HavePointEffect);
            //HideRangeIndicatorWhenCasting
            HitBoneName = f.GetValue("SpellData", "HitBoneName", HitBoneName);
            HitEffectName = f.GetValue("SpellData", "HitEffectName", HitEffectName);
            HitEffectOrientType = f.GetValue("SpellData", "HitEffectOrientType", HitEffectOrientType);
            //HitEffectPlayerName
            IgnoreAnimContinueUntilCastFrame = f.GetValue("SpellData", "IgnoreAnimContinueUntilCastFrame",
                IgnoreAnimContinueUntilCastFrame);
            IgnoreRangeCheck = f.GetValue("SpellData", "IgnoreRangeCheck", IgnoreRangeCheck);
            //InventoryIconX
            IsDisabledWhileDead = f.GetValue("SpellData", "IsDisabledWhileDead", IsDisabledWhileDead);
            IsToggleSpell = f.GetValue("SpellData", "IsToggleSpell", IsToggleSpell);
            //KeywordWhenAcquired
            //LevelXDesc
            LineDragLength = f.GetValue("SpellData", "LineDragLength", LineDragLength);
            LineMissileBounces = f.GetValue("SpellData", "LineMissileBounces", LineMissileBounces);
            LineMissileCollisionFromStartPoint = f.GetValue("SpellData", "LineMissileCollisionFromStartPoint",
                LineMissileCollisionFromStartPoint);
            LineMissileDelayDestroyAtEndSeconds = f.GetValue("SpellData", "LineMissileDelayDestroyAtEndSeconds",
                LineMissileDelayDestroyAtEndSeconds);
            LineMissileEndsAtTargetPoint =
                f.GetValue("SpellData", "LineMissileEndsAtTargetPoint", LineMissileEndsAtTargetPoint);
            LineMissileFollowsTerrainHeight = f.GetValue("SpellData", "LineMissileFollowsTerrainHeight",
                LineMissileFollowsTerrainHeight);
            LineMissileTargetHeightAugment = f.GetValue("SpellData", "LineMissileTargetHeightAugment",
                LineMissileTargetHeightAugment);
            LineMissileTimePulseBetweenCollisionSpellHits = f.GetValue("SpellData",
                "LineMissileTimePulseBetweenCollisionSpellHits", LineMissileTimePulseBetweenCollisionSpellHits);
            LineMissileTrackUnits = f.GetValue("SpellData", "LineMissileTrackUnits", LineMissileTrackUnits);
            LineMissileUsesAccelerationForBounce = f.GetValue("SpellData", "LineMissileUsesAccelerationForBounce",
                LineMissileUsesAccelerationForBounce);
            //LineTargetingBaseTextureOverrideName
            //LineTargetingBaseTextureOverrideName
            LineWidth = f.GetValue("SpellData", "LineWidth", LineWidth);

            for (int i = 0; i < 6; i++)
            {
                LocationTargettingWidth[i] = f.GetValue("SpellData", $"LocationTargettingWidth{i + 1}", 0.0f);
            }

            for (int i = 0; i < 6; i++)
            {
                LocationTargettingLength[i] = f.GetValue("SpellData", $"LocationTargettingLength{i + 1}", 0.0f);
            }

            LockConeToPlayer = f.GetValue("SpellData", "LockConeToPlayer", LockConeToPlayer);
            //LookAtPolicy
            LuaOnMissileUpdateDistanceInterval = f.GetValue("SpellData", "LuaOnMissileUpdateDistanceInterval",
                LuaOnMissileUpdateDistanceInterval);
            Coefficient2 = f.GetValue("SpellData", "Coefficient2", Coefficient2);
            for (int i = 0; i < 6; i++)
            {
                ManaCost[i] = f.GetValue("SpellData", $"ManaCost{i + 1}", 0.0f);
            }

            //Map_X_EffectYLevelZAmmount
            MaxAmmo = f.GetValue("SpellData", "MaxAmmo", MaxAmmo);
            //MaxGrowthRangeTextureName
            //MinimapIcon
            //MinimapIconDisplayFlag
            //MinimapIconRotation
            MissileSpeed = f.GetValue("SpellData", "MissileSpeed", MissileSpeed);
            MissileAccel = f.GetValue("SpellData", "MissileAccel", MissileAccel);
            MissileBoneName = f.GetValue("SpellData", "MissileBoneName", MissileBoneName);
            MissileEffect = f.GetValue("SpellData", "MissileEffect", MissileEffect);
            MissileEffectPlayer = f.GetValue("SpellData", "MissileEffectPlayer", MissileEffectPlayer);
            MissileFixedTravelTime = f.GetValue("SpellData", "MissileFixedTravelTime", MissileFixedTravelTime);
            MissileFollowsTerrainHeight =
                f.GetValue("SpellData", "MissileFollowsTerrainHeight", MissileFollowsTerrainHeight);
            MissileGravity = f.GetValue("SpellData", "MissileGravity", MissileGravity);
            MissileLifetime = f.GetValue("SpellData", "MissileLifetime", MissileLifetime);
            MissileMaxSpeed = f.GetValue("SpellData", "MissileMaxSpeed", MissileSpeed);
            MissileMinSpeed = f.GetValue("SpellData", "MissileMinSpeed", MissileSpeed);
            MissileMinTravelTime = f.GetValue("SpellData", "MissileMinTravelTime", MissileMinTravelTime);
            MissilePerceptionBubbleRadius =
                f.GetValue("SpellData", "MissilePerceptionBubbleRadius", MissilePerceptionBubbleRadius);
            MissilePerceptionBubbleRevealsStealth = f.GetValue("SpellData", "MissilePerceptionBubbleRevealsStealth",
                MissilePerceptionBubbleRevealsStealth);
            //MissileSpeed = file.GetFloat("SpellData", "MissileSpeed", MissileSpeed);
            MissileTargetHeightAugment =
                f.GetValue("SpellData", "MissileTargetHeightAugment", MissileTargetHeightAugment);
            MissileUnblockable = f.GetValue("SpellData", "MissileUnblockable", MissileUnblockable);
            NoWinddownIfCancelled = f.GetValue("SpellData", "NoWinddownIfCancelled", NoWinddownIfCancelled);
            //NumSpellTargeters
            //OrientRadiusTextureFromPlayer
            //OrientRangeIndicatorToCursor
            //OrientRangeIndicatorToFacing
            OverrideCastTime = f.GetValue("SpellData", "OverrideCastTime", OverrideCastTime);
            //public Vector3 ParticleStartOffset { get; set; } = new Vector3(0, 0, 0);
            ParticleStartOffset = f.GetValue("SpellData", "ParticleStartOffset", Vector3.Zero);
            //PlatformEnabled
            PointEffectName = f.GetValue("SpellData", "PointEffectName", PointEffectName);
            //RangeIndicatorTextureName
            RequiredUnitTags = f.GetValue("SpellData", "RequiredUnitTags", RequiredUnitTags);
            SelectionPreference = f.GetValue("SpellData", "SelectionPreference", SelectionPreference);
            //Sound_CastName
            //Sound_HitName
            //Sound_VOEventCategory
            SpellCastTime = f.GetValue("SpellData", "SpellCastTime", SpellCastTime);
            SpellRevealsChampion = f.GetValue("SpellData", "SpellRevealsChampion", SpellRevealsChampion);
            SpellTotalTime = f.GetValue("SpellData", "SpellTotalTime", SpellTotalTime);
            StartCooldown = f.GetValue("SpellData", "StartCooldown", StartCooldown);
            SubjectToGlobalCooldown = f.GetValue("SpellData", "SubjectToGlobalCooldown", SubjectToGlobalCooldown);
            //TargeterConstrainedToRange
            TargetingType = (TargetingType)f.GetValue("SpellData", "TargettingType", (int)TargetingType);
            TextFlags = f.GetValue("SpellData", "TextFlags", TextFlags);
            TriggersGlobalCooldown = f.GetValue("SpellData", "TriggersGlobalCooldown", TriggersGlobalCooldown);
            UpdateRotationWhenCasting = f.GetValue("SpellData", "UpdateRotationWhenCasting", UpdateRotationWhenCasting);
            UseAnimatorFramerate = f.GetValue("SpellData", "UseAnimatorFramerate", UseAnimatorFramerate);
            UseAutoattackCastTime = f.GetValue("SpellData", "UseAutoattackCastTime", UseAutoattackCastTime);
            UseChargeChanneling = f.GetValue("SpellData", "UseChargeChanneling", UseChargeChanneling);
            UseChargeTargeting = f.GetValue("SpellData", "UseChargeTargeting", UseChargeTargeting);
            UseGlobalLineIndicator = f.GetValue("SpellData", "UseGlobalLineIndicator", UseGlobalLineIndicator);
            UseMinimapTargeting = f.GetValue("SpellData", "UseMinimapTargeting", UseMinimapTargeting);
        }
    }
}