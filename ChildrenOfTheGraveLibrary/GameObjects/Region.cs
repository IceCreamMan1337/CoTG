using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;

/// <summary>
/// Class used for all in-game visual effects meant to be explicitly networked by the server (never spawned client-side).
/// </summary>
/// TODO: Possibly turn this into a simple data storage object and put it in GameObject as a RegionParameters variable (or something similar).
public class Region : GameObject
{
    // Function Vars
    private float _currentTime;

    public int Type { get; }
    public GameObject? CollisionUnit { get; }
    public GameObject? VisionTarget { get; }
    public int OwnerClientID { get; }
    /// <summary>
    /// Total game-time that this region should exist for
    /// </summary>
    public float Lifetime { get; }
    public float GrassRadius { get; }
    /// <summary>
    /// Scale of the region used in networking
    /// </summary>
    public float Scale { get; }
    public float AdditionalSize { get; }
    public bool HasCollision { get; }
    public bool GrantVision { get; }
    public bool RevealsStealth { get; }

    /// <summary>
    /// Prepares the Particle, setting up the information required for networking it to clients.
    /// This particle will spawn and stay on the specified GameObject target.
    /// </summary>
    /// <param name="game">Game instance.</param>
    /// <param name="team">Team this region should be on.</param>
    /// <param name="pos">Position to spawn this region at.</param>
    /// <param name="type">Type of region to spawn. Values unknown. Default recommended.</param>
    /// <param name="collisionUnit">Unit which will use the collision radius of this region.</param>
    /// <param name="visionTarget">Bind target for vision.</param>
    /// <param name="giveVision">Whether or not this region should have vision.</param>
    /// <param name="visionRadius">Size of the vision area of this region.</param>
    /// <param name="revealStealth">Whether or not this region should reveal stealthed units when they within the vision radius.</param>
    /// <param name="hasCollision">Whether or not this region should have collision.</param>
    /// <param name="collisionRadius">Size of the collision area of this region.</param>
    /// <param name="grassRadius">Size of the grass area of this region. Currently non-functional.</param>
    /// <param name="scale">Scale of the Region.</param>
    /// <param name="addedSize"></param>
    /// <param name="lifetime">Number of seconds the Region should exist.</param>
    /// <param name="clientId">ClientID of the player that owns this region.</param>
    public Region
    (
        TeamId team,
        Vector2 pos,
        RegionType type = RegionType.Default,
        GameObject? collisionUnit = null,
        GameObject? visionTarget = null,
        float visionRadius = 0,
        bool revealStealth = false,
        float collisionRadius = 0,
        float grassRadius = 0,
        float scale = 1.0f,
        float addedSize = 0,
        float lifetime = 0,
        int clientId = 0
    ) : base(pos, "", 0, collisionRadius, visionRadius, team: team)
    {
        Type = (int)type;
        CollisionUnit = collisionUnit;
        VisionTarget = visionTarget;
        OwnerClientID = clientId;

        Lifetime = lifetime;
        GrassRadius = grassRadius;
        Scale = scale;
        AdditionalSize = addedSize;

        if (Scale > 0)
        {
            PathfindingRadius *= Scale;
            VisionRadius *= Scale;
        }

        if (AdditionalSize > 0)
        {
            PathfindingRadius += AdditionalSize;
            VisionRadius += AdditionalSize;
        }

        HasCollision = PathfindingRadius > 0;
        GrantVision = VisionRadius > 0;

        RevealsStealth = revealStealth;

        Game.ObjectManager.AddObject(this);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        AddRegionNotify(this, userId, team);
    }

    internal override void OnAdded()
    {
        if (HasCollision)
        {
            Game.Map.CollisionHandler.AddObject(this);
        }
        RegisterVision();
    }

    internal override void OnRemoved()
    {
        if (HasCollision)
        {
            Game.Map.CollisionHandler.RemoveObject(this);
        }
        UnregisterVision();
    }

    public override void SetTeam(TeamId team)
    {
        UnregisterVision();
        base.SetTeam(team);
        RegisterVision();
    }

    /// <summary>
    /// Additionally registers vision for both teams, if necessary.
    /// </summary>
    void RegisterVision()
    {
        // NEUTRAL Regions give global vision.
        if (Team == TeamId.TEAM_NEUTRAL)
        {
            Game.VisionManager.AddVisionProvider(this, TeamId.TEAM_ORDER);
            Game.VisionManager.AddVisionProvider(this, TeamId.TEAM_CHAOS);
        }
        else
        {
            Game.VisionManager.AddVisionProvider(this, Team);
        }
    }

    void UnregisterVision()
    {
        if (Team == TeamId.TEAM_NEUTRAL)
        {
            Game.VisionManager.RemoveVisionProvider(this, TeamId.TEAM_ORDER);
            Game.VisionManager.RemoveVisionProvider(this, TeamId.TEAM_CHAOS);
        }
        else
        {
            Game.VisionManager.RemoveVisionProvider(this, Team);
        }
    }

    /// <summary>
    /// Called by ObjectManager when the object is ontop of another object or when the object is inside terrain.
    /// </summary>
    public override void OnCollision(GameObject collider, CollisionTypeOurs collisionType)
    {
    }

    /// <summary>
    /// Called by ObjectManager every tick.
    /// </summary>
    /// <param name="diff">Number of milliseconds since this tick occurred.</param>
    internal override void Update()
    {
        if (CollisionUnit != null)
        {
            Position = CollisionUnit.Position;
        }


        if (RevealsStealth)
        {
            if (this.CollisionUnit is AttackableUnit)
            {

                var units = Functions_CS.GetUnitsInArea(this.CollisionUnit as AttackableUnit, this.Position3D, VisionRadius, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectWards | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectMinions);
                foreach (var u in units)
                {
                    u.Buffs.Add("RevealSpecificUnit", 0.1f, 1, null, u, this.CollisionUnit as ObjAIBase);
                }
            }


        }


        if (Lifetime > 0)
        {
            _currentTime += Game.Time.DeltaTimeSeconds;
            if (_currentTime >= Lifetime)
            {
                SetToRemove();
            }
        }
    }

    /// <summary>
    /// Returns the total game-time passed since the particle was added to ObjectManager
    /// </summary>
    public float GetTimeAlive()
    {
        return _currentTime;
    }

    /// <summary>
    /// Actions that should be performed after the Particle is removed from ObjectManager.
    /// </summary>
    public override void SetToRemove()
    {
        if (!IsToRemove())
        {
            base.SetToRemove();
            //TODO: Move to OnRemoved?
            RemoveRegionNotify(this);
        }
    }
    /// <summary>
    /// Retourne la rgion correspondant au NetId spcifi, ou null si la rgion n'est pas trouve.
    /// </summary>
    /// <param name="netId">NetId de la rgion  rechercher.</param>
    /// <returns>La rgion correspondant au NetId, ou null si la rgion n'est pas trouve.</returns>
    public static Region GetRegionByNetId(uint netId)
    {
        var region = Game.ObjectManager.GetObjectById(netId) as Region;
        return region;
    }
}