using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Force.Crc32;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.Barracks;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings;

public class Barrack : ObjBuilding
{
    private ILog _logger = LoggerProvider.GetLogger();
    internal static Dictionary<TeamId, List<Barrack>> Manager = new();
    internal static bool SpawnsEnabled;

    //Riot::GameTimer waveTimer;
    internal int WaveCount { get; set; }
    internal int CurrentSpawnNum { get; set; }
    //Riot::GameTimer curSpawnTimer;
    internal int WaveSpawnInterval { get; set; }
    internal int MinionSpawnInterval { get; set; }
    internal Dictionary<string, MinionData> MinionTable { get; private set; } = new();
    //TODO: Fix MapScripts and Unhardcode this
    internal List<string> MinionSpawnOrder { get; private set; }
    internal float ExperienceRadius { get; private set; }
    internal float GoldRadius { get; private set; }
    internal bool IsDestroyed { get; private set; }
    internal int BarrackLane { get; private set; }
    internal float InhibitorRespawnTime { get; private set; }
    internal bool InhibitorDestroyed { get; set; }
    internal float SuperMinionSpawnTime { get; private set; }
    internal bool SuperMinionsEnabled { get; private set; }
    internal bool BarracksEnabled => Game.Config.GameFeatures.HasFlag(FeatureFlags.EnableLaneMinions);
    internal Region VisionRegion { get; set; }
    public Lane Lane { get; internal set; } = (Lane)(-1);

    private int ElapsedTime;
    private int WaveTimer;
    private int MinionTimer;
    private int AverageMinionLevel;
    public Barrack(
        string name,
        TeamId team,
        Vector2 position
        ) :
        base(name, "", -1, position, 0, 0xFF000000 | Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(name)), team)
    {

        if (!Status.HasFlag(StatusFlags.Invulnerable))
        {
            SetStatus(StatusFlags.Invulnerable, true);
        }
        SetStatus(StatusFlags.Targetable, false);

        float radius = ContentManager.MapConfig.GetValue(Name, "PerceptionBubbleRadius", 0.0f);
        VisionRegion = new(Team, Position, visionRadius: radius);

        SelectionHeight = ContentManager.MapConfig.GetValue(Name, "SelectionHeight", -1.0f);
        SelectionRadius = ContentManager.MapConfig.GetValue(Name, "SelectionRadius", -1.0f);
        PathfindingRadius = ContentManager.MapConfig.GetValue(Name, "PathfindingCollisionRadius", -1.0f);
        Stats.HealthRegeneration.BaseValue = ContentManager.MapConfig.GetValue(Name, "mBaseStaticHPRegen", 0.0f);

        if (SelectionRadius != -1.0f && PathfindingRadius <= 0)
        {
            PathfindingRadius = SelectionRadius * 0.95f;
        }

        if (Manager.TryGetValue(Team, out List<Barrack> list))
        {
            list.Add(this);
        }
        else
        {
            Manager.Add(Team, new() { this });
        }

        //OnCreate

        //I know this isn't exactly the most efficient way of doing this, but it's how Rito does it ¯\_(ツ)_/¯
        if (name.Contains("__R"))
        {
            Lane = Lane.LANE_R;
        }
        else if (name.Contains("__C"))
        {
            Lane = Lane.LANE_C;
        }
        else if (name.Contains("__L"))
        {
            Lane = Lane.LANE_L;
        }

        if ((int)Lane is -1)
        {
            _logger.Error("Invalid BarrackLane!");
            Lane = Lane.LANE_R;
        }

        InitMinionSpawnInfo spawnInfo = Game.Map.LevelScript.GetInitSpawnInfo(Lane, Team);
        if (spawnInfo is not null)
        {
            // Ajuster les intervalles de spawn en fonction du facteur d'accélération du temps
            WaveSpawnInterval = (int)(spawnInfo.WaveSpawnInterval / Game.Time.TimeScale);
            MinionSpawnInterval = (int)(spawnInfo.MinionSpawnInterval / Game.Time.TimeScale);
            IsDestroyed = spawnInfo.IsDestroyed;
            MinionTable = spawnInfo.InitialMinionData;
        }
    }

    internal override void Update()
    {
        if (!IsDestroyed)
        {
            if (InhibitorDestroyed)
            {
                if (InhibitorRespawnTime <= 0)
                {
                    ReactivateDampener();
                }
                else
                {
                    InhibitorRespawnTime -= Game.Time.ScaledDeltaTime;
                }
            }
            if (SuperMinionsEnabled)
            {
                if (SuperMinionSpawnTime <= 0)
                {
                    SuperMinionsEnabled = false;
                    if (Inhibitor.GetInhibitor(Team, Lane) is not null)
                    {
                        Game.Map.LevelScript.DisableSuperMinions(Team, Lane);
                    }
                }
                else
                {
                    SuperMinionSpawnTime -= Game.Time.ScaledDeltaTime;
                }
            }
            if (/*bar_bSpawnEnabled.var &&*/SpawnsEnabled && BarracksEnabled)
            {
                if (ElapsedTime >= WaveTimer)
                {
                    // Ajuster l'intervalle de la prochaine vague en fonction du facteur d'accélération
                    WaveTimer = (int)(WaveSpawnInterval / Game.Time.TimeScale);
                    WaveCount++;

                    MinionSpawnInfo spawnInfo = Game.Map.LevelScript.GetMinionSpawnInfo(Lane, WaveCount, Team);

                    if (spawnInfo is not null)
                    {
                        IsDestroyed = spawnInfo.IsDestroyed;
                        ExperienceRadius = spawnInfo.ExperienceRadius;
                        GoldRadius = spawnInfo.GoldRadius;

                        MinionTable = spawnInfo.MinionData;
                        MinionSpawnOrder = spawnInfo.MinionSpawnOrder;
                    }

                    ElapsedTime = 0;
                    // Ajuster l'intervalle de spawn des minions en fonction du facteur d'accélération
                    MinionTimer = (int)(MinionSpawnInterval / Game.Time.TimeScale);
                }
                else
                {
                    ElapsedTime += (int)Game.Time.ScaledDeltaTime;
                }

                MinionTimer += (int)Game.Time.ScaledDeltaTime;
                if (MinionTimer >= MinionSpawnInterval)
                {
                    //  Console.WriteLine("MinionTimer >= MinionSpawnInterval");
                    AverageMinionLevel = Champion.GetAverageChampionLevel();

                    if (MinionSpawnOrder is not null)
                    {
                        foreach (var minionType in MinionSpawnOrder)
                        {
                            //  Console.WriteLine("just for test spam 2 ");
                            if (MinionTable[minionType].NumToSpawnForWave > 0)
                            {
                                // Console.WriteLine(MinionTable[minionType].NumToSpawnForWave);
                                MinionTable[minionType].NumToSpawnForWave--;
                                // int minionSpawnType = GetMinionSpawnType(minionType);
                                //  Console.WriteLine(minionType);
                                int minionSpawnType = GetMinionSpawnType(minionType);

                                Game.ObjectManager.AddObject(new LaneMinion(
                                    $"Minion_T{Team}L{Lane}S{minionSpawnType}N{++CurrentSpawnNum}",
                                    MinionTable[minionType].CoreName,
                                    Position,
                                    this,
                                    MinionTable[minionType],
                                    minionSpawnType,
                                    AverageMinionLevel)
                                {
                                    SpawnTypeOverride = MinionTable[minionType].SpawnTypeOverride
                                });
                                break;
                            }

                        }
                        MinionTimer = 0;
                    }
                }
            }
        }
    }

    int GetMinionSpawnType(string model)
    {
        switch (model)
        {
            case "Melee":
                return (int)MinionSpawnType.MELEE_MINION_NAME;
            case "Caster":
                return (int)MinionSpawnType.ARCHER_MINION_NAME;
            case "Cannon":
                return (int)MinionSpawnType.CASTER_MINION_NAME;
            case "Super":
                return (int)MinionSpawnType.SUPER_MINION_NAME;
            default:
                return MinionTable.Keys.ToList().IndexOf(model);
        }
        return MinionTable.Keys.ToList().IndexOf(model);
    }

    internal static void SetSpawn(bool enabled)
    {
        SpawnsEnabled = enabled;
    }

    internal static Barrack? GetBarrack(TeamId team, Lane lane)
    {
        return Manager[team].Find(x => x.Lane == lane);
    }

    // Méthode pour recalculer les intervalles de spawn quand le facteur d'accélération change
    internal void RecalculateSpawnIntervals()
    {
        // Recalculer les intervalles de base depuis le script Lua
        InitMinionSpawnInfo spawnInfo = Game.Map.LevelScript.GetInitSpawnInfo(Lane, Team);
        if (spawnInfo is not null)
        {
            // Ajuster les intervalles de spawn en fonction du facteur d'accélération du temps
            WaveSpawnInterval = (int)(spawnInfo.WaveSpawnInterval / Game.Time.TimeScale);
            MinionSpawnInterval = (int)(spawnInfo.MinionSpawnInterval / Game.Time.TimeScale);
        }
    }

    internal void ReactivateDampener()
    {
        Inhibitor inhibitor = Inhibitor.GetInhibitor(Team, Lane);

        if (inhibitor.State is not DampenerState.RespawningState)
        {
            Game.Map.LevelScript.BarrackReactiveEvent(inhibitor.Team, inhibitor.Lane);
            Stats.IsDead = false;
            inhibitor.State = DampenerState.RespawningState;
            //AttackableUnits::ToGridType(v2->TeamID);
            Stats.CurrentHealth = Stats.HealthPoints.Total;
            HealthRegenEnabled = true;
            inhibitor.NotifyState();
        }
    }

    internal override Lane GetLane()
    {
        return Lane;
    }

    internal void DisableInhibitor(float seconds)
    {
        InhibitorRespawnTime = seconds * 1000;
        InhibitorDestroyed = true;
        SuperMinionSpawnTime = (seconds * 1000) - (2 * WaveSpawnInterval);
        SuperMinionsEnabled = true;

        //Check how this is done
        Inhibitor inhib = Inhibitor.GetInhibitor(Team, Lane);
        inhib.RespawnTime = InhibitorRespawnTime;
    }

    public override void Sync(int userId, TeamId team, bool visible, bool forceSpawn = false)
    {
    }
}