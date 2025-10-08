using System;
using System.Linq;
using System.Numerics;
using System.Text;
using Force.Crc32;
using CoTGEnumNetwork;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.Content;
using CoTG.CoTGServer.GameObjects.StatsNS;

namespace CoTG.CoTGServer.GameObjects.AttackableUnits.AI;

/// <summary>
/// Base class for Turret GameObjects.
/// In League, turrets are separated into visual and AI objects, so this GameObject represents the AI portion,
/// while the visual object is handled automatically by clients via packets.
/// </summary>
public class BaseTurret : ObjAIBase
{
    /// <summary>
    /// Current lane this turret belongs to.
    /// </summary>
    public Lane Lane { get; protected set; }
    /// <summary>
    /// MapObject that this turret was created from.
    /// </summary>
    public MapObject ParentObject { get; private set; }
    /// <summary>
    /// Supposed to be the NetID for the visual counterpart of this turret. Used only for packets.
    /// </summary>
    public uint ParentNetId { get; private set; }
    /// <summary>
    /// Region assigned to this turret for vision and collision.
    /// </summary>
    public Region BubbleRegion { get; private set; }

    public override bool SpawnShouldBeHidden => false;

    public BaseTurret(
        string name,
        string model,
        Vector2 position,
        TeamId team,
        uint netId = 0,
        Lane lane = Lane.LANE_Unknown,
        MapObject mapObject = default,
        int skinId = 0,
        Stats stats = null,
        string aiScript = ""
    ) : base(model, name, position: position, visionRadius: 800, skinId: skinId, netId: netId, team: team, stats: stats, aiScript: aiScript)
    {
        ParentNetId = Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(name)) | 0xFF000000;
        Lane = lane;
        ParentObject = mapObject;
        SetTeam(team);
        Replication = new ReplicationAITurret(this);

        SetStatus(StatusFlags.Targetable, false);
        SetStatus(StatusFlags.Invulnerable, true);
        SetStatus(StatusFlags.CanMove, false);
        SetStatus(StatusFlags.CanMoveEver, false);
    }

    /// <summary>
    /// Called when this unit dies.
    /// </summary>
    /// <param name="killer">Unit that killed this unit.</param>
    public override void Die(DeathData data)
    {
        if (data.Killer is Champion ch)
        {
            ch.ChampionStatistics.TurretsKilled++;
        }

        // Make sure to remove the region and turret from vision providers
        if (BubbleRegion != null)
        {
            Game.VisionManager.RemoveVisionProvider(BubbleRegion, Team);
            BubbleRegion.SetToRemove();
        }

        Game.VisionManager.RemoveVisionProvider(this, Team);

        base.Die(data);
    }

    /// <summary>
    /// Function called when this GameObject has been added to ObjectManager.
    /// </summary>
    internal override void OnAdded()
    {
        // Add the turret to the collision handler
        Game.Map.CollisionHandler.AddObject(this);

        // Register the turret with turret tracking
        //Game.ObjectManager.AddTurret(this);

        // Critical: Add turret as vision provider for its OWN team only
        Game.VisionManager.AddVisionProvider(this, Team);

        // Create bubble region with the correct team
        BubbleRegion = new Region
        (
            Team, Position,
            RegionType.Unknown2,
            collisionUnit: this,
            visionTarget: null,
            visionRadius: 800f,
            revealStealth: true,
            collisionRadius: PathfindingRadius,
            lifetime: 25000.0f
        );

        // Add bubble region as vision provider for the turret's team only
        Game.VisionManager.AddVisionProvider(BubbleRegion, Team);
    }

    /// <summary>
    /// Override IsValidTarget to ensure turrets can target enemies in range
    /// </summary>
    public override bool IsValidTarget(AttackableUnit target)
    {
        // Make sure the target is an enemy
        if (target.Team != this.Team.GetEnemyTeam())
            return false;

        // Basic checks - is it targetable and alive?
        if (!target.IsTargetable || target.Stats.IsDead)
            return false;

        // Check if target is a ward
        if (target.Name.ToLower().Contains("ward"))
            return false;

        // Check if target is in range - this is all we need
        float range = GetTotalAttackRange();
        return Vector2.DistanceSquared(Position, target.Position) <= range * range;
    }

    /// <summary>
    /// Update method to actively look for targets
    /// </summary>
    internal override void Update()
    {
        base.Update();

        // If turret doesn't have a target, look for one
        if (TargetUnit == null || TargetUnit.Stats.IsDead || !IsValidTarget(TargetUnit))
        {
            // Get all units in attack range
            var targetsInRange = Game.ObjectManager.GetUnitsInRange(Position, GetTotalAttackRange(), true);

            // Filter to enemy units only
            var enemyTargets = targetsInRange.Where(u => u.Team == Team.GetEnemyTeam() && IsValidTarget(u)).ToList();

            // Sort by priority
            if (enemyTargets.Count > 0)
            {
                // Sort by priority (lower number = higher priority)
                enemyTargets.Sort((a, b) => GetTargetPriority(a).CompareTo(GetTargetPriority(b)));

                // Set the highest priority target
                SetTargetUnit(enemyTargets[0]);
            }
        }
    }

    public override int GetTargetPriority(AttackableUnit target, AttackableUnit victim = null)
    {
        switch (target)
        {
            case Pet:
                return 2;
            case LaneMinion m:
                return (MinionSpawnType)m.MinionSpawnType switch
                {
                    MinionSpawnType.SUPER_MINION_NAME or MinionSpawnType.CASTER_MINION_NAME => 3,
                    MinionSpawnType.MELEE_MINION_NAME => 4,
                    MinionSpawnType.ARCHER_MINION_NAME => 5,
                    _ => throw new ArgumentOutOfRangeException(),
                };
            case Minion trap:
                return trap.Owner == null ? 6 : 1;
        }
        return 14;
    }


    public override void CallForHelp(ObjAIBase attacker, ObjAIBase target)
    {
        if (target is Champion)
        {
            switch (attacker)
            {
                case Champion:
                case Pet:
                case Minion { Owner: { } }:
                    base.CallForHelp(attacker, target);
                    break;
            }
        }
    }

    public override void OnCollision(GameObject collider, CollisionTypeOurs collisionType)
    {
        // TODO: Verify if we need this for things like SionR.
    }

    /// <summary>
    /// Overridden function unused by turrets.
    /// </summary>
    protected override void RefreshWaypoints(float idealRange)
    {
    }

    /// <summary>
    /// Sets this turret's Lane to the specified Lane.
    /// Only sets if its current Lane is NONE.
    /// Used for ObjectManager.
    /// </summary>
    /// <param name="newId"></param>
    public void SetLaneID(Lane newId)
    {
        Lane = newId;
    }

    /// <summary>
    /// Sets this turret's team and updates its vision region accordingly
    /// </summary>
    public override void SetTeam(TeamId team)
    {
        // Store old team for cleanup
        var oldTeam = Team;

        // Remove from old team's vision providers
        Game.VisionManager.RemoveVisionProvider(this, oldTeam);

        // Remove bubble region from old team's vision providers
        if (BubbleRegion != null)
        {
            Game.VisionManager.RemoveVisionProvider(BubbleRegion, oldTeam);
        }

        // Change team
        base.SetTeam(team);

        // Add to new team's vision providers
        Game.VisionManager.AddVisionProvider(this, team);

        // Re-add bubble region with new team
        if (BubbleRegion != null)
        {
            BubbleRegion.SetTeam(team);
            Game.VisionManager.AddVisionProvider(BubbleRegion, team);
        }
    }

    internal override float GetAttackRatioWhenAttackingTurret()
    {
        return GlobalData.DamageRatios.BuildingToBuilding;
    }
    internal override float GetAttackRatioWhenAttackingMinion()
    {
        return GlobalData.DamageRatios.BuildingToUnit;
    }
    internal override float GetAttackRatioWhenAttackingChampion()
    {
        return GlobalData.DamageRatios.BuildingToHero;
    }
    internal override float GetAttackRatioWhenAttackingBuilding()
    {
        return GlobalData.DamageRatios.BuildingToBuilding;
    }
    internal override float GetAttackRatio(AttackableUnit attackingUnit)
    {
        return attackingUnit.GetAttackRatioWhenAttackingTurret();
    }
}
