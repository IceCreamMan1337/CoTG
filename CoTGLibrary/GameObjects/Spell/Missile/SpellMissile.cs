using System;
using System.Numerics;
using CoTGEnumNetwork;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.API;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.GameObjects.SpellNS.Missile;

public abstract class SpellMissile : GameObject
{
    public virtual MissileType Type => MissileType.None;
    public CastInfo CastInfo { get; }
    public AttackableUnit? TargetUnit { get; protected set; }
    public Spell SpellOrigin { get; }
    public Vector3 StartPoint { get; protected set; }
    public Vector3 EndPoint { get; protected set; }
    protected bool IsServerOnly { get; }
    protected SpellDataFlags Flags { get; } = 0;

    public override bool IsAffectedByFoW => true;
    public override bool SpawnShouldBeHidden => true;

    public Vector2 Destination => TargetUnit?.Position ?? EndPoint.ToVector2();

    public float CreationTime { get; internal set; }
    public float Lifetime => SpellOrigin.Data.MissileLifetime;
    public float FixedTravelTime => SpellOrigin.SpellData.MissileFixedTravelTime;
    public float TimeSinceCreation => (Game.Time.GameTime / 1000.0f) - CreationTime;
    // This parameter paves the way for the implementation of LuaOnMissileUpdateDistanceInterval in SpellData
    public float DistanceSinceCreation = 0;

    public float Speed =>
        SpellOrigin.Data.MissileAccel == 0 ?
        SpellOrigin.Data.MissileSpeed :
        Math.Clamp(
            SpellOrigin.Data.MissileSpeed + (SpellOrigin.Data.MissileAccel * TimeSinceCreation),
            SpellOrigin.Data.MissileMinSpeed,
            SpellOrigin.Data.MissileMaxSpeed
        );

    public SpellMissile(
        Spell spell,
        CastInfo castInfo,
        SpellDataFlags flags
    ) : base(
        position: castInfo.SpellCastLaunchPosition.ToVector2(),
        name: spell.SpellData.MissileEffect,
        collisionRadius: spell.SpellData.LineWidth,
        pathingRadius: 0,
        //TODO: spell.Data.MissilePerceptionBubbleRevealsStealth
        visionRadius: spell.Data.MissilePerceptionBubbleRadius,
        netId: 0
    )
    {
        IsServerOnly = spell.Data.MissileEffect != "";
        SpellOrigin = spell;
        CastInfo = castInfo;
        Flags = flags;

        TargetUnit = castInfo.Target?.Unit;
        StartPoint = castInfo.SpellCastLaunchPosition;
        EndPoint = castInfo.TargetPosition;
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        CreationTime = Game.Time.GameTime / 1000;

        if (SpellOrigin.Name
            is "BandageToss"
            or "VorpalSpikes"
            or "VorpalSpikesMissle"
            or "SadMummyBandageToss"
            or "Volley"
            or "VolleyAttack"
            or "GalioRighteousGust"
            or "GalioRighteousGustMissile"
            or "IreliaTranscendentBlades"
            or "IreliaTranscendentBladesSpell"
            or "CH1ConcussionGrenade"
            or "CH1ConcussionGrenadeSpell"
            or "CH1ConcussionGrenadeUpgrade"
            or "HextechMicroRockets"
            or "HextechMicroRocketsMissile"
            or "HextechMicroRockets"
            or "HextechMicroRocketsMissile"
            or "HowlingGaleMove"
            or "HowlingGale"
            or "HowlingGaleSpell"
            or "HowlingGaleSpell1"
            or "HowlingGaleSpell2"
            or "HowlingGaleSpell3"
            or "HowlingGaleSpell4"
            or "HowlingGaleSpell5"
            or "CH1ConcussionGrenadeSpell"
            or "CH1ConcussionGrenadeUpgrade"
            or "DeathLotusMissile"
            or "LuxLightBinding"
            or "LuxLightBindingMis"
            or "LuxPrismaticWave"
            or "LuxPrismaticWaveMissile"
            or "AlZaharCalloftheVoid"
            or "AlZaharCalloftheVoidMissile"
            or "MaokaiSapling2"
            or "MaokaiSapling2Boom"
            or "MissFortuneRicochetShot"
            or "MissFortuneRShotExtra"
            or "MissFortuneBullets"
            or "MissFortuneBulletsClone"
            or "SonaHymnofValor"
            or "SealFateMissile"
            or "WildCards"
            or "UrgotPlasmaGrenade"
            or "RivenLightsaberMissile"
            or "RivenLightsaberMissileSide"
            or "RivenIzunaBlade"
            or "TalonRakeMissileOne"
            or "TalonRakeMissileTwo"
            or "TalonShadowAssaultMisOne"
            or "TalonShadowAssaultMisTwo"
            or "FiddleSticksDarkWindMissile"
            or "BouncingBlades"
            or "DarkBindingMissile"
            || SpellOrigin.Name.Contains("Oriana")
            )
        {

            //hack : some spell need facedirection 
            if (this.SpellOrigin.Name is "VorpalSpikes"
                or "VorpalSpikesMissle"
                or "WildCards")
            {
                /*   if (this.SpellOrigin.Name == "WildCards")
                   {
                       this.SpellOrigin.Caster.FaceDirection()
                   }*/
                FaceDirectionNotify(CastInfo.Caster, CastInfo.Caster.Direction, true);
            }
            MissileReplicationNotify(this, userId);
        }
    }

    internal override void Update()
    {
        SpellOrigin.OnMissileUpdate(this);
        if (Lifetime > 0 && TimeSinceCreation > Lifetime)
        {
            SetToRemove();
        }
    }

    public override Vector3 GetPosition3D()
    {
        return Position.ToVector3(GetHeight() + SpellOrigin.Data.MissileTargetHeightAugment);
    }

    internal override float GetHeight()
    {
        return SpellOrigin.Data.MissileFollowsTerrainHeight ? base.GetHeight() : StartPoint.Y;
    }

    protected bool LinearMoveTo(Vector2 dest, float distance = 0)
    {
        var dir = (dest - Position).Normalized();
        Direction = dir.ToVector3(0);

        //TODO: FixedTravelTime + MinTravelTime

        float dist = FixedTravelTime > 0 ? Vector3.Distance(StartPoint, EndPoint) / FixedTravelTime * Game.Time.DeltaTime / 1000 : Speed * Game.Time.DeltaTime / 1000;
        if (dist * dist < Position.DistanceSquared(dest))
        {
            Position += dir * dist;
            DistanceSinceCreation += dist;
            return false;
        }
        Position = dest;
        return true;
    }

    public void SetToRemoveBlocked()
    {
        if (!IsToRemove())
        {
            base.SetToRemove();
            ApiEventManager.OnSpellMissileRemoved.Publish(this);
            //if(!IsServerOnly)
            {
                DestroyClientMissileNotify(this);
            }
        }
    }

    public override void SetToRemove()
    {
        if (!IsToRemove())
        {
            base.SetToRemove();
            SpellOrigin.OnMissileEnd(this);
            ApiEventManager.OnSpellMissileEnd.Publish(this);
            ApiEventManager.OnSpellMissileRemoved.Publish(this);
            //if(!IsServerOnly)
            {
                DestroyClientMissileNotify(this);
            }
        }
    }

    public override void OnCollision(GameObject collider, CollisionTypeOurs collisionType)
    {
        if (collisionType == CollisionTypeOurs.TERRAIN)
        {
            // check if necessary on 126
            //normally used for nautilus 
            //     SpellOrigin.OnSpellMissileHitTerrain(this);
            //  ApiEventManager.OnSpellMissileHitTerrain.Publish(this);
        }
    }

}
