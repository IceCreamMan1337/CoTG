using System;
using System.Collections.Generic;
using System.Numerics;
using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.API;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using static PacketVersioning.PktVersioning;



namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;

/// <summmary>
/// Base class for all objects.
/// GameObjects normally follow these guidelines of functionality: Position, Direction, Collision, Vision, Team, and Networking.
/// </summmary>
public class GameObject
{
    // Function Vars
    protected bool _toRemove = false;

    protected Dictionary<TeamId, bool> _visibleByTeam;
    protected HashSet<int> _spawnedForPlayers = new();
    protected Dictionary<int, bool> _visibleForPlayers = new();
    public HashSet<int> _haslinkvision = new();
    public List<GameObject> _haslinkvisionBB = new();
    /// <summary>
    /// A set of players with vision of this GameObject.
    /// Can be iterated through.
    /// </summary>
    public IEnumerable<int> VisibleForPlayers
    {
        get
        {
            foreach (var kv in _visibleForPlayers)
            {
                if (kv.Value)
                {
                    yield return kv.Key;
                }
            }
        }
    }

    /// <summary>
    /// Comparison variable for small distance movements.
    /// </summary>
    public static readonly uint MOVEMENT_EPSILON = 5; //TODO: Verify if this should be changed

    public string Name { get; }
    public float SelectionHeight { get; protected set; }
    public float SelectionRadius { get; protected set; }

    /// <summary>
    ///  Identifier unique to this game object.
    /// </summary>
    public uint NetId { get; }
    /// <summary>
    /// Radius of the circle which is used for collision detection between objects or terrain.
    /// </summary>
    public float CollisionRadius { get; protected set; }
    /// <summary>
    /// Radius of the circle which is used for pathfinding around objects and terrain.
    /// </summary>
    public float PathfindingRadius { get; protected set; }
    /// <summary>
    /// Position of this GameObject from a top-down view.
    /// </summary>
    public Vector2 Position { get; protected set; }
    public Vector3 Position3D => GetPosition3D();
    /// <summary>
    /// 3D orientation of this GameObject (based on ground-level).
    /// </summary>
    public Vector3 Direction { get; protected set; }
    /// <summary>
    /// Team identifier, refer to TeamId enum.
    /// </summary>
    public TeamId Team { get; protected set; }
    /// <summary>
    /// Radius of the circle which is used for vision; detecting if objects are visible given terrain, and if so, networked to the player (or team) that owns this game object.
    /// </summary>
    public virtual float VisionRadius { get; set; }

    public CollisionType CollisionType;
    public virtual bool IsAffectedByFoW => false;
    public virtual bool SpawnShouldBeHidden => false;

    public string LastAnimation { get; private set; } = "";

    private static Fade _defaultFade = new(0, 0, 0, 1);
    private Fade _currentFade = _defaultFade;
    private int _nextFadeId = 0;
    private float _currentFadeStartOpacity = 0; // What was the opacity at the moment when the current fade replaced the previous one.
    private Fade _previousFade = null;
    private List<Fade> _fades = new();
    private Dictionary<int, Fade> _lastFadeSeenByPlayer = new();
    public float GetOpacity()
    {
        float t = Math.Min(1, (Game.Time.GameTime - _currentFade.StartTime) / _currentFade.Duration);
        return (t * (_currentFade.Opacity - _currentFadeStartOpacity)) + _currentFadeStartOpacity;
    }
    private void SetFade(float opacity, float duration)
    {
        _currentFadeStartOpacity = GetOpacity();
        _previousFade = _currentFade;
        _currentFade = new Fade(_nextFadeId++, Game.Time.GameTime, duration, opacity);

    }
    public Fade FadeOut(float opacity, float duration)
    {
        duration *= 1000;

        SetFade(opacity, duration);
        _fades.Add(_currentFade);
        return _currentFade;
    }
    public bool FadeIn(Fade fade, float duration = 1f) //TODO: Find the default value actually used.
    {
        duration *= 1000;

        if (_fades.Remove(fade) && fade == _currentFade)
        {
            float opacity = 1;
            if (_fades.Count > 0)
            {
                Fade lastFade = _fades[_fades.Count - 1];
                float lastFadeTimeLeft = lastFade.Duration - (Game.Time.GameTime - lastFade.StartTime);

                duration = Math.Max(duration, lastFadeTimeLeft); //TODO: strictly obey the duration?
                opacity = lastFade.Opacity;
            }
            SetFade(opacity, duration);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Instantiation of an object which represents the base class for all objects in League of Legends.
    /// </summary>
    public GameObject(Vector2 position, string name, float collisionRadius = 40f, float pathingRadius = 40f, float visionRadius = 0f, uint netId = 0, TeamId team = TeamId.TEAM_NEUTRAL)
    {
        Name = name;
        if (netId != 0)
        {
            NetId = netId; // Custom netId
        }
        else
        {
            NetId = ChildrenOfTheGraveServer.NetIdManager.GenerateNewNetId(); // base class assigns a netId
        }
        Position = position;
        Direction = Vector3.UnitZ;
        CollisionRadius = collisionRadius;
        PathfindingRadius = pathingRadius;
        VisionRadius = visionRadius;

        _visibleByTeam = new Dictionary<TeamId, bool>
        {
            { TeamId.TEAM_ORDER, false },
            { TeamId.TEAM_CHAOS, false },
            { TeamId.TEAM_NEUTRAL, false }
        };

        Team = team;
    }

    /// <summary>
    /// Called by ObjectManager after AddObject (usually right after instatiation of GameObject).
    /// </summary>
    internal virtual void OnAdded()
    {
        Game.Map.CollisionHandler.AddObject(this);

        // Most objects become vision providers for their own team
        // Special cases like turrets and regions handle this in their own OnAdded methods
        if (!(this is BaseTurret) && !(this is Region))
        {
            // Only add vision providers for appropriate teams
            if (Team != TeamId.TEAM_UNKNOWN)
            {
                Game.VisionManager.AddVisionProvider(this, Team);
            }
        }
    }

    /// <summary>
    /// Called by ObjectManager every tick.
    /// </summary>
    /// <param name="diff">Number of milliseconds that passed before this tick occurred.</param>
    internal virtual void Update()
    {
    }

    internal virtual void LateUpdate()
    {
    }

    /// <summary>
    /// Whether or not the object should be removed from the game (usually both server and client-side). Refer to ObjectManager.
    /// </summary>
    internal bool IsToRemove()
    {
        return _toRemove;
    }

    /// <summary>
    /// Will cause ObjectManager to remove the object (usually) both server and client-side next update.
    /// </summary>
    public virtual void SetToRemove()
    {
        _toRemove = true;
    }

    /// <summary>
    /// Called by ObjectManager after the object has been SetToRemove.
    /// </summary>
    internal virtual void OnRemoved()
    {
        Game.Map.CollisionHandler.RemoveObject(this);
        Game.VisionManager.RemoveVisionProvider(this, Team);
    }

    /// <summary>
    /// Refers to the height that the object is at in 3D space.
    /// </summary>
    internal virtual float GetHeight()
    {
        return Game.Map.NavigationGrid.GetHeightAtLocation(Position);
    }

    /// <summary>
    /// Gets the position of this GameObject in 3D space, where the Y value represents height.
    /// Mostly used for packets.
    /// </summary>
    /// <returns>Vector3 position.</returns>
    public virtual Vector3 GetPosition3D()
    {
        return Position.ToVector3(GetHeight());
    }

    /// <summary>
    /// Sets the server-sided position of this object.
    /// </summary>
    public virtual void SetPosition(float x, float y)
    {
        SetPosition(new Vector2(x, y));
    }

    /// <summary>
    /// Sets the server-sided position of this object.
    /// </summary>
    public virtual void SetPosition(Vector2 vec)
    {
        Position = vec;
    }

    /// <summary>
    /// Sets the collision radius of this GameObject.
    /// </summary>
    /// <param name="newRadius">Radius to set.</param>
    public void SetCollisionRadius(float newRadius)
    {
        CollisionRadius = newRadius;
    }

    /// <summary>
    /// Sets the SetPathfindingRadius radius of this GameObject.
    /// </summary>
    /// <param name="newRadius">Radius to set.</param>
    public void SetPathfindingRadius(float newRadius)
    {
        PathfindingRadius = newRadius;
    }


    /// <summary>
    /// Sets this GameObject's current orientation (only X and Z are used in movement).
    /// </summary>
    public void FaceDirection(Vector3 newDirection, bool isInstant = true, float turnTime = 0.08333f)
    {
        if (newDirection != Vector3.Zero && !float.IsNaN(newDirection.X) && !float.IsNaN(newDirection.Y) && !float.IsNaN(newDirection.Z))
        {

            Direction = new Vector3(newDirection.X, 50.0f, newDirection.Z);

            if (Game.ObjectManager.GetObjectById(NetId) != null) //TODO: GameObject.IsAdded
            {
                //in .126 seem not so much used


                FaceDirectionNotify(this, Direction, isInstant, turnTime);
                // Game.PacketNotifier.NotifyFaceDirection(this, newDirection, isInstant, turnTime);

            }
        }
    }

    /// <summary>
    /// Whether or not the specified object is colliding with this object.
    /// </summary>
    /// <param name="o">An object that could be colliding with this object.</param>
    public virtual bool IsCollidingWith(GameObject o)
    {
        return Vector2.DistanceSquared(Position, o.Position) < (CollisionRadius + o.CollisionRadius) * (CollisionRadius + o.CollisionRadius);
    }

    /// <summary>
    /// Whether or not the specified object is colliding with this object (using the pathfinding radius).
    /// </summary>
    /// <param name="o">An object that could be colliding with this object.</param>
    public virtual bool IsPathCollidingWith(GameObject o)
    {
        return Vector2.DistanceSquared(Position, o.Position) < (PathfindingRadius + o.PathfindingRadius) * (PathfindingRadius + o.PathfindingRadius);
    }


    /// <summary>
    /// Called by ObjectManager when the object is ontop of another object or when the object is inside terrain.
    /// </summary>
    public virtual void OnCollision(GameObject collider, CollisionTypeOurs type)
    {
        // TODO: Verify if we should trigger events here.

        if (type == CollisionTypeOurs.TERRAIN)
        {
            // Escape functionality should be moved to GameObject.OnCollision.
            // only time we would collide with terrain is if we are inside of it, so we should teleport out of it.
            Vector2 exit = Game.Map.NavigationGrid.GetClosestTerrainExit(Position, PathfindingRadius + 1.0f);
            SetPosition(exit);
        }
    }

    protected virtual void OnSpawn(int userId, TeamId team, bool doVision)
    {
        EnterVisibilityClientNotify(this, userId);
    }

    protected virtual void OnEnterVision(int userId, TeamId team)
    {
        OnEnterTeamVisibilityNotify(this, team, userId);
    }

    protected virtual void OnSync(int userId, TeamId team)
    {
    }

    protected virtual void OnLeaveVision(int userId, TeamId team)
    {
        OnLeaveTeamVisibilityNotify(this, team, userId);
    }

    public virtual void Sync(int userId, TeamId team, bool visible, bool forceSpawn = false)
    {

        visible = visible || !IsAffectedByFoW;

        if (!forceSpawn && IsSpawnedForPlayer(userId))
        {
            if (IsAffectedByFoW && (IsVisibleForPlayer(userId) != visible))
            {
                if (visible)
                {
                    OnEnterVision(userId, team);
                }
                else
                {
                    OnLeaveVision(userId, team);
                }
                SetVisibleForPlayer(userId, visible);
            }
            else if (visible)
            {
                OnSync(userId, team);
            }

        }
        else
        {


            // Enhanced check for minions - don't spawn enemy minions unless they're actually visible
            bool isEnemyMinion = (this is LaneMinion || this is NeutralMinion) && this.Team != team && this.Team != TeamId.TEAM_NEUTRAL;

            // Only spawn objects that are:
            // 1. Actually visible right now, OR
            // 2. Not affected by fog of war, OR
            // 3. On the player's own team, AND
            // 4. NOT enemy minions without vision
            bool shouldSpawn = visible || (!SpawnShouldBeHidden && !isEnemyMinion) || (this.Team == team);

            if (shouldSpawn)
            {
                OnSpawn(userId, team, visible);
                SetVisibleForPlayer(userId, visible);
                SetSpawnedForPlayer(userId);
            }
        }

        // The same as IconInfo.Sync
        //TODO: optimize Dictionary usage?
        if (forceSpawn)
        {
            _lastFadeSeenByPlayer[userId] = _defaultFade;
        }
        Fade lastSeenFade = _lastFadeSeenByPlayer.GetValueOrDefault(userId, _defaultFade);
        float currentFadeTimeLeft = _currentFade.Duration - (Game.Time.GameTime - _currentFade.StartTime);
        float lastSeenFadeTimeLeft = lastSeenFade.Duration - (Game.Time.GameTime - lastSeenFade.StartTime);
        if (visible && !(
            lastSeenFade == _currentFade ||
            // To prevent sending extra packets.
            lastSeenFade.Opacity == _currentFade.Opacity &&
            lastSeenFadeTimeLeft <= 0 && currentFadeTimeLeft <= 0
        ))
        {
            if (currentFadeTimeLeft > 0)
            {
                // 126Fix
                S2C_SetFadeOut_PushNotify(this, GetOpacity(), 0, userId);
                S2C_SetFadeOut_PushNotify(this, _currentFade.Opacity, currentFadeTimeLeft, userId);
            }
            else
            {
                if (this is ObjAIBase)
                {
                    // 126Fix

                    S2C_SetFadeOut_PushNotify(this, GetOpacity(), 0, userId);
                }
                else
                {
                    S2C_SetFadeOut_PushNotify(this, GetOpacity(), 0, userId);
                }


            }
            _lastFadeSeenByPlayer[userId] = _currentFade;
        }
    }

    internal virtual void OnAfterSync()
    {
    }

    internal virtual void OnReconnect(int userId, TeamId team, bool hard)
    {
        if (IsSpawnedForPlayer(userId))
        {
            Sync(userId, team, IsVisibleForPlayer(userId), hard);
        }

        if (this is Champion champion)
        {
            ApiEventManager.OnReconnect.Publish(champion);
        }
    }

    /// <summary>
    /// Sets the object's team.
    /// </summary>
    /// <param name="team">TeamId.BLUE/PURPLE/NEUTRAL</param>
    public virtual void SetTeam(TeamId team)
    {
        Game.VisionManager.RemoveVisionProvider(this, Team);
        Team = team;
        Game.VisionManager.AddVisionProvider(this, Team);
        if (Game.StateHandler.State is GameState.GAMELOOP)
        {
            SetTeamNotify(this as AttackableUnit);
        }
    }

    /// <summary>
    /// Whether or not the object is within vision of the specified team.
    /// </summary>
    /// <param name="team">A team which could have vision of this object.</param>
    public bool IsVisibleByTeam(TeamId team)
    {
        return !IsAffectedByFoW || _visibleByTeam[team];
    }

    /// <summary>
    /// Sets the object as visible to a specified team.
    /// Should be called in the ObjectManager. By itself, it only affects the return value of IsVisibleByTeam.
    /// </summary>
    /// <param name="team">A team which could have vision of this object.</param>
    /// <param name="visible">New value.</param>
    public void SetVisibleByTeam(TeamId team, bool visible = true)
    {
        _visibleByTeam[team] = visible;
    }

    /// <summary>
    /// Gets a list of all teams that have vision of this object.
    /// </summary>
    public List<TeamId> TeamsWithVision()
    {
        List<TeamId> toReturn = new();
        foreach (var team in _visibleByTeam.Keys)
        {
            if (_visibleByTeam[team])
            {
                toReturn.Add(team);
            }
        }
        return toReturn;
    }

    /// <summary>
    /// Whether or not the object is visible for the specified player.
    /// <summary>
    /// <param name="userId">The player in relation to which the value is obtained</param>
    public bool IsVisibleForPlayer(int userId)
    {
        return !IsAffectedByFoW || _visibleForPlayers.GetValueOrDefault(userId, false);
    }

    /// <summary>
    /// Sets the object as visible and or not to a specified player.
    /// Should be called in the ObjectManager. By itself, it only affects the return value of IsVisibleForPlayer.
    /// <summary>
    /// <param name="userId">The player for which the value is set.</param>
    /// <param name="visible">New value.</param>
    public void SetVisibleForPlayer(int userId, bool visible = true)
    {
        _visibleForPlayers[userId] = visible;
    }

    /// <summary>
    /// Sets the object an visibility link 
    /// thsi mean the entity is visible by userid in all case ( until the link is removed) 
    /// <summary>
    /// <param name="userIdLink">The player who are link with this .</param>
    public void SetVisibilityLinkForEntity(int userIdLink)
    {

        _haslinkvision.Add(userIdLink);

    }


    /// <summary>
    /// remove the visibility link 
    /// <summary>
    /// <param name="userIdLink">The player who was link with this </param>
    public void RemoveVisibilityLinkForEntity(int userIdLink)
    {
        bool removed = _haslinkvision.Remove(userIdLink);
    }

    /// <summary>
    /// clear visibility link  
    /// <summary>
    /// <param name="userIdLink">The player who was link with this </param>
    public void RemoveAllVisibilityLinkForEntity()
    {
        if (_haslinkvision.Count > 0)
        {
            foreach (int uid in _haslinkvision)
            {
                Game.ObjectManager.GetObjectById((uint)uid).RemoveVisibilityLinkForEntity((int)this.NetId);
            }


            _haslinkvision = new HashSet<int>();
        }

    }




    /// <summary>
    /// Whether or not the object is spawned on the player's client side.
    /// <summary>
    /// <param name="userId">The player in relation to which the value is obtained</param>
    public bool IsSpawnedForPlayer(int userId)
    {
        return _spawnedForPlayers.Contains(userId);
    }

    /// <summary>
    /// Sets the object as spawned on the player's client side.
    /// Should be called in the ObjectManager. By itself, it only affects the return value of IsSpawnedForPlayer.
    /// <summary>
    /// <param name="userId">The player for which the value is set.</param>
    public void SetSpawnedForPlayer(int userId)
    {
        _spawnedForPlayers.Add(userId);
    }

    /// <summary>
    /// Forces this GameObject to perform the given internally named animation.
    /// </summary>
    /// <param name="animName">Internal name of an animation to play.</param>
    /// <param name="timeScale">How fast the animation should play. Default 1x speed.</param>
    /// <param name="startTime">Time in the animation to start at.</param>
    /// TODO: Verify if this description is correct, if not, correct it.
    /// <param name="speedScale">How much the speed of the GameObject should affect the animation.</param>
    /// <param name="flags">Animation flags. Refer to AnimationFlags enum.</param>
    public void PlayAnimation(string animName, float timeScale = 1.0f, float startTime = 0, float speedScale = 0, AnimationFlags flags = 0)
    {
        LastAnimation = animName;
        S2C_PlayAnimationNotify(this, animName, flags, timeScale, startTime, speedScale);
    }

    public void UnlockAnimation()
    {
        S2C_UnlockAnimationNotify(this, "");
    }

    /// <summary>
    /// Forces this GameObject's current animations to pause/unpause.
    /// </summary>
    /// <param name="pause">Whether or not to pause/unpause animations.</param>
    public void PauseAnimation(bool pause)
    {
        S2C_PauseAnimationNotify(this, pause);
    }

    /// <summary>
    /// Forces this GameObject to stop playing the specified animation (or optionally all animations) with the given parameters.
    /// </summary>
    /// <param name="animation">Internal name of the animation to stop playing. Set blank/null if stopAll is true.</param>
    /// <param name="stopAll">Whether or not to stop all animations. Only works if animation is empty/null.</param>
    /// <param name="fade">Whether or not the animation should fade before stopping.</param>
    /// <param name="ignoreLock">Whether or not locked animations should still be stopped.</param>
    public void StopAnimation(string animation, bool stopAll = false, bool fade = false, bool ignoreLock = true)
    {
        S2C_StopAnimationNotify(this, animation, stopAll, fade, ignoreLock);
    }

    internal virtual void UpdateStats()
    {
    }
}
