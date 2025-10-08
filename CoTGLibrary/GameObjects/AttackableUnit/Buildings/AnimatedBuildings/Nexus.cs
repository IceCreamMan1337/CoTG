using System.Collections.Generic;
using System.Numerics;
using Force.Crc32;
using System.Text;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.Content;
using CoTG.CoTGServer.GameObjects.StatsNS;
using CoTG.CoTGServer.Scripting.CSharp;
using FLS = CoTG.CoTGServer.Scripting.Lua.Functions;
using CoTGEnumNetwork;

namespace CoTG.CoTGServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;

public class Nexus : ObjAnimatedBuilding
{
    static List<Nexus> NexusList = new();
    Region VisionRegion { get; set; }
    public Nexus(
        string name,
        TeamId team,
        int collisionRadius = 40,
        Vector2 position = new(),
        int visionRadius = 0,
        Stats stats = null
    ) : base(name, collisionRadius, position, visionRadius, Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(name)) | 0xFF000000, team, stats)
    {
        //OnCreate
        Stats.HealthPoints.BaseValue = ContentManager.MapConfig.GetValue(Name, "mMaxHP", 3010.0f);
        Stats.ManaPoints.BaseValue = ContentManager.MapConfig.GetValue(Name, "mMaxMP", 0.0f);
        Armor = ContentManager.MapConfig.GetValue(Name, "mArmor", 0.0f);
        Stats.HealthRegeneration.BaseValue = ContentManager.MapConfig.GetValue(Name, "mBaseStaticHPRegen", 0.0f);
        SelectionHeight = ContentManager.MapConfig.GetValue(Name, "SelectionHeight", 0.0f);
        SelectionRadius = ContentManager.MapConfig.GetValue(Name, "SelectionRadius", 0.0f);
        PathfindingRadius = ContentManager.MapConfig.GetValue(Name, "PathfindingCollisionRadius", -1.0f);
        float radius = ContentManager.MapConfig.GetValue(Name, "PerceptionBubbleRadius", 1350.0f);
        //hack
        CollisionRadius = ContentManager.MapConfig.GetValue(Name, "SelectionRadius", -1.0f);


        if (SelectionRadius != -1.0f && PathfindingRadius <= 0)
        {
            PathfindingRadius = SelectionRadius * 0.95f;
        }

        // CollisionRadius = PathfindingRadius;

        Stats.CurrentHealth = Stats.HealthPoints.Total;
        VisionRegion = new(Team, Position, visionRadius: radius);

        SetStatus(StatusFlags.Targetable, false);
        Stats.IsTargetableToTeam = SpellDataFlags.NonTargetableAll;
        SetStatus(StatusFlags.Invulnerable, false);

        if (!Name.EndsWith("11") || !Name.EndsWith("21"))
        {
            NexusList.Add(this);
        }

        foreach (var cell in Game.Map.NavigationGrid.GetAllCellsInRange(position, PathfindingRadius))
        {
            cell.SetFlags(NavigationGridCellFlags.NOT_PASSABLE, true);
            cell.SetFlags(NavigationGridCellFlags.SEE_THROUGH, true);
        }
        // Game.PacketNotifier.Notify_WriteNavFlags(this.Position, this.CollisionRadius, NavigationGridCellFlags.NOT_PASSABLE | NavigationGridCellFlags.SEE_THROUGH);
    }
    internal override void Update()
    {
        //hack : nexus doesn't die when an minion kill it 

        if (Stats.CurrentHealth <= 0)
        {
            DeathData data = new()
            {
                BecomeZombie = false,
                DieType = DieType.MINION_DIE, // TODO: Unhardcode
                Unit = this,
                Killer = this,
                DamageType = DamageType.DAMAGE_TYPE_TRUE,
                DamageSource = DamageSource.DAMAGE_SOURCE_ATTACK,
                DeathDuration = 0 // TODO: Unhardcode
            };
            this.Die(data);
        }
    }
    public override void Die(DeathData data)
    {
        Game.Map.LevelScript.HandleDestroyedObject(this);
        base.Die(data);

        FLS.EndGame((int)CustomConvert.GetEnemyTeam(this.Team));
        //hack for close server 
        // Game.StateHandler.SetGameState(GameState.EXIT);

    }

    internal static Nexus? GetNexus(TeamId team)
    {
        return NexusList.Find(x => x.Team == team);
    }

    internal static float GetEoGPanTime()
    {
        return GlobalData.NexusVariables.EoGPanTime;
    }

    internal static bool GetEoGUseDeathAnimation()
    {
        return GlobalData.NexusVariables.EoGUseNexusDeathAnimation;
    }

    internal static float GetEoGNexusChangeSkinTime()
    {
        return GlobalData.NexusVariables.EoGNexusChangeSkinTime;
    }


    public override void TakeDamage(DamageData damageData, IEventSource sourceScript = null)
    {
        base.TakeDamage(damageData, sourceScript);

        if (damageData.Damage <= 0)
        {
            return;
        }

    }

}
