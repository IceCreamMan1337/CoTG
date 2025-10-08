using System.Collections.Generic;
using System.Numerics;
using Force.Crc32;
using System.Text;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveLibrary.Managers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;
using log4net;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;

public class Inhibitor : ObjAnimatedBuilding
{
    private static ILog _logger = LoggerProvider.GetLogger();

    static Dictionary<TeamId, List<Inhibitor>> Manager = new();
    public Lane Lane { get; private set; }
    public DampenerState State { get; internal set; }
    public float RespawnTime { get; set; }
    public float RespawnAnimationDuration { get; internal set; }
    public bool AnnounceNextRespawn { get; set; }
    public bool AnnounceNextRespawnAnimation { get; set; }
    Region VisionRegion;
    private const float GOLD_WORTH = 50.0f;

    // TODO assists
    public Inhibitor(
        string name,
        TeamId team,
        int collisionRadius = 40,
        Vector2 position = new(),
        int visionRadius = 0,
        Stats stats = null
    ) : base(name, collisionRadius, position, visionRadius, Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(name)) | 0xFF000000, team, stats)
    {
        State = DampenerState.RespawningState;
        Stats.HealthPoints.BaseValue =
            Game.Config.GameConfig.GameMode is "TUTORIAL" ?
            GlobalData.BarrackVariables.MaxHPTutorial :
            GlobalData.BarrackVariables.MaxHP;
        Stats.CurrentHealth = Stats.HealthPoints.Total;
        Lane = InhibitorHelper.FindLaneOfInhibitor(this);

        if (Manager.TryGetValue(Team, out List<Inhibitor> list))
        {
            list.Add(this);
        }
        else
        {
            Manager.Add(Team, new() { this });
        }

        //OnNetworkIDAssigned
        SelectionHeight = ContentManager.MapConfig.GetValue(Name, "SelectionHeight", -1.0f);
        SelectionRadius = ContentManager.MapConfig.GetValue(Name, "SelectionRadius", -1.0f); //Check
        PathfindingRadius = ContentManager.MapConfig.GetValue(Name, "PathfindingCollisionRadius", -1.0f);
        Stats.HealthRegeneration.BaseValue = ContentManager.MapConfig.GetValue(Name, "mBaseStaticHPRegen", 0.0f);
        //hack
        CollisionRadius = SelectionRadius;

        if (SelectionRadius != -1.0f && PathfindingRadius <= 0)
        {
            PathfindingRadius = SelectionRadius * 0.95f;
        }

        //CollisionRadius = PathfindingRadius;

        /*  SetStatus(StatusFlags.Targetable, false);
          Stats.IsTargetableToTeam = SpellDataFlags.NonTargetableAll;
          SetStatus(StatusFlags.Invulnerable, true);

        */
        SetStatus(StatusFlags.Targetable, false);
        SetStatus(StatusFlags.Invulnerable, true);



        //actually the collisionradius is stupid so we can't do that 
        foreach (var cell in Game.Map.NavigationGrid.GetAllCellsInRange(position, PathfindingRadius))
        {
            cell.SetFlags(NavigationGridCellFlags.NOT_PASSABLE, true);
            cell.SetFlags(NavigationGridCellFlags.SEE_THROUGH, true);
            cell.SetOpen(false);
        }
        //  Game.PacketNotifier126.Notify_WriteNavFlags(this.Position, this.CollisionRadius, NavigationGridCellFlags.NOT_PASSABLE | NavigationGridCellFlags.SEE_THROUGH);
    }

    public static Inhibitor GetInhibitor(TeamId team, Lane lane)
    {
        string seachStr = lane switch
        {
            Lane.LANE_L => "_L",
            Lane.LANE_C => "_C",
            _ => "_R"
        };

        var result = Manager[team].Find(x => x.Name.Contains(seachStr));



        return result;
    }
    public override void TakeDamage(DamageData damageData, IEventSource sourceScript = null)
    {
        base.TakeDamage(damageData, sourceScript);

        if (damageData.Damage <= 0)
        {
            return;
        }

    }
    public override void Die(DeathData data)
    {
        Game.Map.LevelScript.HandleDestroyedObject(this);
        NotifyState(data);
        base.Die(data);

        if (data.Killer is Champion c)
        {
            c.GoldOwner.AddGold(GOLD_WORTH);
            c.ChampionStatistics.BarracksKilled++;
        }

        AnnounceNextRespawn = true;
        AnnounceNextRespawnAnimation = true;
    }

    //Check
    public void SetState(DampenerState newState)
    {
        State = newState;
        if (newState == DampenerState.RespawningState)
        {
            Stats.HealthSetToMax();
            HealthRegenEnabled = true;
            //?
            Stats.IsDead = false;
        }
        else
        {
            //Check this else statement
            //_logger.Warn($"InvalidState on inhibitor; state={newState} newState={newState} oldState={state}");
        }
    }

    internal override void Update()
    {
        RespawnTime -= Game.Time.ScaledDeltaTime;
        if (RespawnTime < 15 && AnnounceNextRespawn)
        {
            State = DampenerState.RespawningState;
            NotifyState();
            AnnounceNextRespawn = false;
        }
        else if (RespawnTime < RespawnAnimationDuration && AnnounceNextRespawnAnimation)
        {
            //Check
            PlayAnimation("Respawn", -1, 0.0f, 1.0f, (AnimationFlags)9);
            AnnounceNextRespawnAnimation = false;
        }
    }

    public void NotifyState(DeathData data = null)
    {
        var opposingTeam = Team == TeamId.TEAM_ORDER ? TeamId.TEAM_CHAOS : TeamId.TEAM_ORDER;
        SetIsTargetableToTeam(opposingTeam, State == DampenerState.RespawningState);
        InhibitorStateNotify(this, data);
    }

    public override void SetToRemove()
    {
    }

    internal override Lane GetLane()
    {
        return Lane;
    }
}
