using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

public class LaneMinion : Minion
{
    internal static List<LaneMinion> Manager = new();
    /// <summary>
    /// Const waypoints that define the minion's route
    /// </summary>
    public List<Vector2> PathingWaypoints { get; }
    /// <summary>
    /// Name of the Barracks that spawned this lane minion.
    /// </summary>
    public Barrack BarrackSpawn { get; }
    internal int MinionSpawnType { get; }
    public bool SpawnTypeOverride { get; set; }

    public override bool IsAffectedByFoW => true;
    public override bool SpawnShouldBeHidden => true;

    public LaneMinion(
        string name,
        string model,
        Vector2 position,
        Barrack barrackSpawn,
        MinionData minionData,
        int minionSpawnType,
        int level,
        string AIScript = "Minion.lua"
    ) : base(null, position, model, name, barrackSpawn.Team, AIScript: AIScript)
    {
        _aiPaused = false;
        IsLaneMinion = true;
        BarrackSpawn = barrackSpawn;
        MinionSpawnType = minionSpawnType;
        PathingWaypoints = GetLaneMinionWaypoints();

        LevelUp(level, false);

        HealthBonus = minionData.BonusHealth;
        DamageBonus = minionData.BonusAttack;

        //Check
        Stats.HealthPoints.FlatBonus = minionData.BonusHealth;
        Stats.AttackDamage.FlatBonus = minionData.BonusAttack;
        Stats.Armor.BaseValue = minionData.Armor;
        Stats.MagicResist.BaseValue = minionData.MagicResistance;
        Stats.GoldGivenOnDeath.BaseValue = minionData.GoldGiven;
        Stats.ExpGivenOnDeath.BaseValue = minionData.ExpGiven;
        Stats.HealthSetToMax();

        MinionFlags = MinionFlags.IsMinion;

        //SetStatus(StatusFlags.Ghosted, false); //hack
        PathfindingRadius = CollisionRadius; //hack


        StopMovement();

        MoveOrder = OrderType.Hold;
        Replication = new ReplicationLaneMinion(this);
        Manager.Add(this);
    }

    public override void Die(DeathData data)
    {

        base.Die(data);

        Manager.Remove(this);
    }

    public override void CallForHelp(ObjAIBase attacker, ObjAIBase target)
    {
        if (attacker is Champion && target is Champion)
        {
            base.CallForHelp(attacker, target);
        }
    }

    public override int GetTargetPriority(AttackableUnit target, AttackableUnit victim = null)
    {
        return target switch
        {
            Champion when victim is Champion => 1,
            LaneMinion when victim is Champion => 2,
            LaneMinion when victim is LaneMinion => 3,
            BaseTurret when victim is LaneMinion => 4,
            Champion when victim is LaneMinion => 5,
            LaneMinion => 6,
            Champion => 7,
            _ => 8
        };


    }

    public override bool IsValidTarget(AttackableUnit target)
    {
        return this.Team.GetEnemyTeam() == target.Team && base.IsValidTarget(target);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        if (SpawnTypeOverride)
        {
            MinionSpawnedNotify(this, userId, team, doVision);
        }
        else
        {
            LaneMinionSpawnedNotify(this, userId, doVision);

        }

        //hack
        //ExpirationTimer hack because minion can be stuck 
        // this.Buffs.Add("ExpirationTimer2", 180.0f, 1, null, this, this);
    }

    internal List<Vector2> GetLaneMinionWaypoints()
    {
        List<Vector2> toReturn = new(Game.Map.NavigationPoints[BarrackSpawn.Lane].Select(x => x.Position));

        if (Vector2.Distance(Position, toReturn.Last()) < Vector2.Distance(Position, toReturn.First()))
        {
            toReturn.Reverse();
        }

        //Hack?
        toReturn.Add(Barrack.GetBarrack(BarrackSpawn.Team is TeamId.TEAM_ORDER ? TeamId.TEAM_CHAOS : TeamId.TEAM_ORDER, BarrackSpawn.Lane).Position);

        return toReturn;
    }
}