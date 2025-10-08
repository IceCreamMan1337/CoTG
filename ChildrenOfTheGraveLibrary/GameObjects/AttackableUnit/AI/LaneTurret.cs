using System.Collections.Generic;
using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

public class LaneTurret : BaseTurret
{
    private static ILog _logger = LoggerProvider.GetLogger();
    private static readonly List<LaneTurret> Manager = new();
    internal static readonly List<MapObject> TurretBuildings = new();
    public int TurretIndex { get; }

    public LaneTurret(
        string name,
        string model,
        Vector2 position,
        int turretIndex,
        TeamId team = TeamId.TEAM_ORDER,
        Lane lane = Lane.LANE_Unknown,
        MapObject mapObject = default,
        Stats stats = null,
        string aiScript = "Turret.lua",
        uint netId = 0
    ) : base(name, model, position, team, netId, lane, mapObject, stats: stats, aiScript: aiScript)
    {
        TurretIndex = turretIndex;
        IsLifestealImmune = true;
        //hack
        CollisionRadius = SelectionRadius;
        if (name.Contains("Shrine"))
        {
            // SetIsTargetableToTeam(TeamId.TEAM_ORDER, false);
            // SetIsTargetableToTeam(TeamId.TEAM_CHAOS, false);
            //actually range of shrine is too big si this is an hack for reduce them 
            this.Stats.Range.IncBaseValuePerm(-this.Stats.Range.Total);
            this.Stats.AcquisitionRange.IncBaseValuePerm(-this.Stats.AcquisitionRange.Total);
            this.Stats.Range.IncBaseValuePerm(GlobalData.SpawnPointVariables.RegenRadius + 200.0f); //hack
            this.Stats.AcquisitionRange.IncBaseValuePerm(GlobalData.SpawnPointVariables.RegenRadius + 200.0f);
        }
        else
        {
            foreach (var cell in Game.Map.NavigationGrid.GetAllCellsInRange(position, PathfindingRadius))
            {
                cell.SetFlags(NavigationGridCellFlags.NOT_PASSABLE, true);
                cell.SetFlags(NavigationGridCellFlags.SEE_THROUGH, true);
            }
        }

        if (Lane is Lane.LANE_Unknown)
        {
            if (name.Contains("_R_"))
            {
                Lane = Lane.LANE_R;
            }
            else if (name.Contains("_C_"))
            {
                Lane = Lane.LANE_C;
            }
            else if (name.Contains("_L_"))
            {
                Lane = Lane.LANE_L;
            }
            else if (name.Contains("Shrine"))
            {
                Lane = Lane.LANE_C;
            }
            else
            {
                _logger.Error("Invalid TurretLane!");
                Lane = Lane.LANE_R;
            }
        }

        Manager.Add(this);
        Game.ObjectManager.AddObject(this);
    }

    public static LaneTurret? GetTurret(TeamId team, Lane lane, int position)
    {
        return Manager.Find(x => x.Team == team && x.Lane == lane && x.TurretIndex == position);
    }

    public static List<LaneTurret> GetLivingTurrets()
    {
        List<LaneTurret> livingTurrets = new();

        foreach (var turret in Manager)
        {
            if (!turret.Stats.IsDead)
            {
                livingTurrets.Add(turret);
            }
        }

        return livingTurrets;
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        CreateTurretNotify(this, userId, doVision);
    }

    //TODO: Decide wether we want MapScrits to handle this with Events or leave this here
    public override void Die(DeathData data)
    {
        Game.Map.LevelScript.HandleDestroyedObject(this);

        float localGold = CharData.LocalGoldGivenOnDeath;
        float globalGold = CharData.GlobalGoldGivenOnDeath;
        float globalEXP = CharData.GlobalExpGivenOnDeath;

        //TODO: change this to assists
        var championsInRange = Game.ObjectManager.GetChampionsInRange(Position, Stats.Range.Total * 1.5f, true);

        if (localGold > 0 && championsInRange.Count > 0)
        {
            foreach (var champion in championsInRange)
            {
                if (champion.Team == Team)
                {
                    continue;
                }

                float gold = CharData.LocalGoldGivenOnDeath / championsInRange.Count;
                champion.GoldOwner.AddGold(gold, false);
                champion.GoldOwner.AddGold(globalGold, true, this);
            }


            //Investigate if we should change this to ExpOwners
            foreach (var player in Game.PlayerManager.GetPlayers(true))
            {
                var champion = player.Champion;
                if (player.Team != Team)
                {
                    if (!championsInRange.Contains(champion))
                    {
                        champion.GoldOwner.AddGold(globalGold);
                    }
                    champion.Experience.AddEXP(globalEXP);
                }
            }
        }
        else
        {
            foreach (var player in Game.PlayerManager.GetPlayers(true))
            {
                var champion = player.Champion;
                if (player.Team != Team)
                {
                    {
                        champion.GoldOwner.AddGold(globalGold);
                        champion.Experience.AddEXP(globalEXP);
                    }
                }
            }
        }
        if (!Name.Contains("Shrine"))
        {
            foreach (var cell in Game.Map.NavigationGrid.GetAllCellsInRange(Position, PathfindingRadius))
            {
                cell.SetFlags(NavigationGridCellFlags.NOT_PASSABLE, true);
                cell.SetFlags(NavigationGridCellFlags.SEE_THROUGH, true);
            }
        }
        base.Die(data);
    }

    internal static LaneTurret? CreateChildTurret(string turretName, string turretModel, TeamId team, int turretIndex, Lane turretLane)
    {
        MapObject? turretObj = TurretBuildings.Find(x => x.Name == turretName);

        if (turretObj is null)
        {
            string errorMsg = $"{turretName} turret not found for CreateChildTurret. Turret names are:\n";

            TurretBuildings.ForEach(x =>
            {
                errorMsg += $"{x.Name},\n";
            });

            _logger.Error(errorMsg);
            return null;
        }

        return new LaneTurret(turretName + "_A", turretModel, turretObj.Position, turretIndex, team, turretLane, turretObj);
    }

    //Delete?
    internal static LaneTurret? DoCreateChildTurret(MapObject turretObj, string turretName, string turretModel, TeamId team, int turretIndex, Lane lane)
    {
        return null;
    }
}
