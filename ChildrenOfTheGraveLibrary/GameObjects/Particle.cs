using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Content;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;
using static PacketVersioning.PktVersioning;
using System.IO;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;

/// <summary>
/// Class used for all in-game visual effects meant to be explicitly networked by the server (never spawned client-side).
/// </summary>
public class Particle : GameObject
{
    private static ILog _logger = LoggerProvider.GetLogger();

    /// <summary>
    /// Creator of this particle, contains the PackageHash.
    /// </summary>
    public GameObject Caster { get; }
    /// <summary>
    /// Primary bind target.
    /// </summary>
    public GameObject BindObject { get; }
    /// <summary>
    /// Client-sided, internal name of the particle used in networking, usually always ends in .troy
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Client-sided, internal name of the particle used in networking when displayed to enemies, usually always ends in .troy
    /// </summary>
    public string NameForEnemies { get; }
    /// <summary>
    /// Secondary bind target. Null when not attached to anything.
    /// </summary>
    public GameObject TargetObject { get; }
    /// <summary>
    /// Position this object is spawned at.
    /// </summary>
    public Vector2 StartPosition { get; }
    /// <summary>
    /// Position this object is aimed at and/or moving towards.
    /// </summary>
    public Vector2 EndPosition { get; }
    /// <summary>
    /// Client-sided, internal name of the bone that this particle should be attached to on the owner, for networking.
    /// </summary>
    public string BoneName { get; } = "";
    /// <summary>
    /// Client-sided, internal name of the bone that this particle should be attached to on the target, for networking.
    /// </summary>
    public string TargetBoneName { get; } = "";
    /// <summary>
    /// Scale of the particle used in networking
    /// </summary>
    public float Scale { get; }
    /// <summary>
    /// Total game-time that this particle should exist for
    /// </summary>
    public float Lifetime { get; }
    public uint PackageHash { get; }
    /// <summary>
    /// Returns the total game-time passed since the particle was created
    /// </summary>
    public float TimeAlive { get; private set; }
    /// <summary>
    /// Whether or not the particle should be titled along the ground towards its end position.
    /// Effectively uses the ground height for the end position.
    /// </summary>
    public bool FollowsGroundTilt { get; }
    /// <summary>
    /// Flags which determine how the particle behaves. Values unknown.
    /// </summary>
    public FXFlags Flags { get; }
    /// <summary>
    /// Particle visual direction
    /// </summary>
    public new Vector3 Direction { get; }
    /// <summary>
    /// Only player which is allowed to see this particle.
    /// </summary>
    public Champion VisibilityOwner { get; }

    public override bool IsAffectedByFoW => true;
    public override bool SpawnShouldBeHidden => true;

    public bool IsAnBeam { get; set; } = false;

    internal Particle
    (
        string particleName,
        GameObject caster,

        Vector2 startPos = default,
        GameObject bindObj = null,
        string boneName = "",

        Vector2 endPos = default,
        GameObject targetObj = null,
        string targetBoneName = "",

        float lifetime = 1.0f,
        float scale = 1.0f,
        Vector3 direction = default,

        string particleEnemyName = null,

        bool followGroundTilt = false,
        FXFlags flags = FXFlags.BindDirection,
        TeamId forceTeam = TeamId.TEAM_UNKNOWN,
        Champion visibilityOwner = null


    ) : base(startPos, particleName, 0, 0, 0, 0, forceTeam)
    {
        particleName = particleName.ToLower();
        particleEnemyName = particleEnemyName?.ToLower();

        Name = particleName.EndsWith(".prt") ? $"{particleName[..^4]}.troy" :
               particleName.EndsWith(".troy") ? particleName :
               $"{particleName}.troy";
        NameForEnemies = string.IsNullOrEmpty(particleEnemyName) ? Name :
               particleEnemyName.ToLower().EndsWith(".troy") ? particleEnemyName :
               $"{particleEnemyName}.troy";

        Caster = caster;

        BindObject = bindObj;
        StartPosition = startPos;
        if (BindObject != null)
        {
            StartPosition = BindObject.Position;
            BoneName = boneName;
            Position = StartPosition;
        }

        TargetObject = targetObj;
        EndPosition = endPos;
        if (TargetObject != null)
        {
            EndPosition = TargetObject.Position;
            TargetBoneName = targetBoneName;
            Position = EndPosition;
        }

        if (StartPosition == default)
        {
            StartPosition = EndPosition;
        }
        if (EndPosition == default)
        {
            EndPosition = StartPosition;
        }

        Lifetime = lifetime;
        PackageHash = 0;
        Flags = flags;
        var pd = ContentManager.GetParticleData(Path.GetFileNameWithoutExtension(Name), Caster, BindObject, TargetObject);
        if (pd != null)
        {
            Lifetime = pd.LifeTime;
            if (!string.IsNullOrEmpty(pd.Model))
            {
                PackageHash = HashFunctions.HashStringNorm(
                    "[character]"
                    + pd.Model
                    + ((pd.SkinId >= 0) ? pd.SkinId.ToString().PadLeft(2, '0') : "")
                );
            }
            if (pd.bindtoemitter == 1)
            {
                Flags = FXFlags.BindDirection; // 32
            }
        }
        else
        {
            _logger.Error($"Particle \"{Name}\" not found");
        }

        Direction = direction;


        Scale = scale;
        FollowsGroundTilt = followGroundTilt;
        VisibilityOwner = visibilityOwner;

        if (forceTeam != TeamId.TEAM_UNKNOWN)
        {
            Team = forceTeam;
        }
        else if (TargetObject != null)
        {
            Team = TargetObject.Team;
        }
        else if (BindObject != null)
        {
            Team = BindObject.Team;
        }
        else if (Caster != null)
        {
            Team = Caster.Team;
        }

        if (Name.Contains("beam"))
        {
            IsAnBeam = true;
            if (BindObject != null && TargetObject != null)
            {
                TargetObject.SetVisibilityLinkForEntity((int)BindObject.NetId);
                BindObject.SetVisibilityLinkForEntity((int)TargetObject.NetId);
            }

        }


        Game.ObjectManager.AddObject(this);

    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        FXCreateGroupNotify(this, team == Team ? Name : NameForEnemies, userId);
    }

    protected override void OnEnterVision(int userId, TeamId team)
    {
        FXEnterTeamVisibilityNotify(this, team, userId);
    }

    protected override void OnLeaveVision(int userId, TeamId team)
    {
        FXLeaveTeamVisibilityNotify(this, team, userId);
    }

    /// <summary>
    /// Called by ObjectManager every tick.
    /// </summary>
    /// <param name="diff">Number of milliseconds since this tick occurred.</param>
    internal override void Update()
    {
        TimeAlive += Game.Time.DeltaTimeSeconds;

        if (IsAnBeam)
        {
            if (BindObject != null && TargetObject != null)
            {
                if (!TargetObject._haslinkvision.Contains((int)BindObject.NetId))
                    SetToRemove();
                if (!BindObject._haslinkvision.Contains((int)TargetObject.NetId))
                    SetToRemove();

            }


        }


        if (Lifetime >= 0 && TimeAlive >= Lifetime)
        {
            //base.SetToRemove(); // Do not send a NotifyFXKill packet
            SetToRemove(); // To make sure that the particle is not left on the client.
        }
    }

    /// <summary>
    /// Actions that should be performed after the Particle is removed from ObjectManager.
    /// </summary>
    public override void SetToRemove()
    {



        if (!IsToRemove())
        {
            base.SetToRemove();
            FXKillNotify(this);
        }
        else if (this.Name.Contains("beam"))
        {
            //need force client to kill beam 
            base.SetToRemove();
            FXKillNotify(this);
        }
    }
}