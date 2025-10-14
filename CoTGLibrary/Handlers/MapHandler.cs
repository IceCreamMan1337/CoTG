using System;
using System.Collections.Generic;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.Scripting.CSharp;
using CoTG.CoTGServer.Content;
using CoTG.CoTGServer.Content.Navigation;
using CoTG.CoTGServer.Logging;
using log4net;
using MapScripts;
using static CoTGEnumNetwork.Content.HashFunctions;
using CoTG.CoTGServer.GameObjects.AttackableUnits.Buildings;
using CoTG.CoTGServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using CoTG.CoTGServer.Scripting.Lua;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.API;
using System.Linq;
using CoTGLibrary.Content;
using MapScripts.GameModes;
using MapScripts.Mutators;
using CoTG.CoTGServer.Content.Navigation.aimesh;



namespace CoTG.CoTGServer.Handlers;

/// <summary>
/// Class responsible for all map related game settings such as collision handler, navigation grid, announcer events, and map properties.
/// </summary>
internal class MapHandler
{
    // Crucial Vars
    private static readonly ILog _logger = LoggerProvider.GetLogger();
    private readonly Dictionary<ObjectType, List<MapObject>> MapObjects = new();

    /// <summary>
    /// Unique identifier for the Map (ex: 1 = Old SR, 11 = New SR)
    /// </summary>
    internal int Id { get; private set; }
    /// <summary>
    /// Collision Handler to be instanced by the map. Used for collisions between GameObjects or GameObjects and terrain.
    /// </summary>
    internal CollisionHandler CollisionHandler { get; private set; }
    /// <summary>
    /// Pathing Handler to be instanced by the map. Used for pathfinding for units.
    /// </summary>
    internal PathingHandler PathingHandler { get; private set; }
    /// <summary>
    /// Navigation Grid to be instanced by the map. Used for terrain data.
    /// </summary>
    internal NavigationGrid NavigationGrid { get; private set; }

    internal NaviMeshTest Aimesh { get; private set; }

    internal MapData MapData { get; private set; }
    /// <summary>
    /// MapProperties specific to a Map Id. Contains information about passive gold gen, lane minion spawns, experience to level, etc.
    /// </summary>
    internal ILevelScript LevelScript { get; private set; }
    internal IGameModeScript GameMode { get; private set; }
    internal IMutatorScript[] Mutators { get; } = new IMutatorScript[8];
    internal string[] MutatorNames { get; } = new string[8];
    internal uint ScriptNameHash { get; private set; }
    internal IEventSource? ParentScript => null;
    internal Dictionary<TeamId, SpawnPoint> SpawnPoints { get; set; } = new();
    internal readonly Dictionary<Lane, List<MapObject>> NavigationPoints = new();
    internal bool HasFirstBloodHappened { get; set; }

    //Consider moving this to MapScripts(?)
    internal readonly Dictionary<TeamId, SurrenderHandler> Surrenders = new();


    /// <summary>
    /// Instantiates map related game settings such as collision handler, navigation grid, announcer events, and map properties.
    /// </summary>
    /// <param name="game">Game instance.</param>
    internal MapHandler(int id)
    {
        Id = id;

        ScriptNameHash = HashString(Game.Config.GameConfig.GameMode);

        InitializeLevelScript();
        GameMode = Game.ScriptEngine.CreateObject<IGameModeScript>($"MapScripts.Map{Id}.GameModes", Game.Config.GameConfig.GameMode.ToUpper()) ?? new DefaultGamemode();
        InitializeMutators();

        try
        {
            NavigationGrid = ContentManager.GetNavigationGrid(this);
            Aimesh = ContentManager.GetAiMesh(this);
        }
        catch (ContentNotFoundException exception)
        {
            _logger.Error(exception.Message);
            return;
        }

        CollisionHandler = new CollisionHandler(this);
        PathingHandler = new PathingHandler(this);
        MapData = ContentManager.GetMapData(id);
        GlobalData.Init(MapData.MapConstants);

        //Do this properly later?
        _ = ApiFunctionManager.CreateTimer(LevelScript!.InitialSpawnTime, () =>
        {
            Barrack.SetSpawn(true);
        }, false, false);
    }

    /// <summary>
    /// Function called every tick of the game. Updates CollisionHandler, MapProperties, and executes AnnouncerEvents.
    /// </summary>
    /// <param name="diff">Number of milliseconds since this tick occurred.</param>
    internal void Update()
    {
        CollisionHandler.Update();
        PathingHandler.Update();
        GameAnnouncementManager.Update();

        foreach (var surrender in Surrenders.Values)
        {
            surrender.Update();
        }

        UpdateLuaTimers();
        GameMode.OnUpdate();

    }

    /// <summary>
    /// Initializes MapProperties. Usually only occurs once before players are added to Game.
    /// </summary>
    internal void OnLevelLoad()
    {
        // Load data package
        try
        {
            GameMode.OnLevelLoad(MapObjects);
            LevelScript.OnPostLevelLoad();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }
    }

    internal void LoadMapObjects()
    {
        foreach (var mapObject in MapData.MapObjects)
        {
            if ((short)mapObject.Type is -1)
            {
                continue;
            }

            switch (mapObject.Type)
            {
                case ObjectType.SpawnPoint:
                    //Optimize this later
                    SpawnPoint sp = new(mapObject.Name, mapObject.Position, mapObject.Team);
                    Game.ObjectManager.AddObject(sp);
                    SpawnPoints.Add(sp.Team, sp);
                    continue;
                case ObjectType.Inhibitor:
                    Game.ObjectManager.AddObject(new Inhibitor(mapObject.Name, mapObject.Team, position: mapObject.Position));
                    continue;
                case ObjectType.Nexus:
                    Game.ObjectManager.AddObject(new Nexus(mapObject.Name, mapObject.Team, position: mapObject.Position));
                    continue;
                case ObjectType.Barracks:
                    Game.ObjectManager.AddObject(new Barrack(mapObject.Name, mapObject.Team, position: mapObject.Position));
                    continue;
                case ObjectType.Turret:
                    LaneTurret.TurretBuildings.Add(mapObject);
                    continue;
                case ObjectType.Shop:
                    Game.ObjectManager.AddObject(new Shop(mapObject.Name, mapObject.Position, mapObject.Team));
                    continue;
                case ObjectType.NavPoint:
                    NavigationPoints.TryAdd(mapObject.Lane, new());
                    NavigationPoints[mapObject.Lane].Add(mapObject);
                    break;
            }

            if (!MapObjects.ContainsKey(mapObject.Type))
            {
                MapObjects.Add(mapObject.Type, new List<MapObject>());
            }

            MapObjects[mapObject.Type].Add(mapObject);
        }

        if (MapData.Id == 8)
        {
            foreach (Lane lane in NavigationPoints.Keys)
            {
                NavigationPoints[lane] = NavigationPoints[lane].OrderBy(x => int.Parse(x.Name[^3..])).ToList();
            }
        }
        else
        {
            foreach (Lane lane in NavigationPoints.Keys)
            {
                NavigationPoints[lane] = NavigationPoints[lane].OrderBy(x => int.Parse(x.Name[^2..])).ToList();
            }
        }

    }

    /// <summary>
    /// This function was directly translated from league's decompiled code
    /// </summary>
    private static void UpdateLuaTimers()
    {
        for (int i = 0; i < Functions.NeutralTimers.Count; i++)
        {
            NeutralTimer timer = Functions.NeutralTimers[i];
            if (timer.Enabled)
            {
                timer.Elapsed += Game.Time.ScaledDeltaTime / 1000.0f;
                if (timer.Elapsed >= timer.Delay)
                {
                    do
                    {
                        timer.Elapsed -= timer.Delay;

                        if (!timer.Repeat)
                        {
                            timer.Enabled = false;
                        }

                        timer.Function();
                    }
                    while (timer.Elapsed >= timer.Delay);
                }
            }
        }

        for (int i = 0; i < Functions.LevelTimers.Count; i++)
        {
            AiTimer timer = Functions.LevelTimers[i];
            if (timer.Enabled)
            {
                timer.Elapsed += Game.Time.ScaledDeltaTime / 1000.0f;
                if (timer.Elapsed >= timer.Delay)
                {
                    while (timer.Elapsed > timer.Delay)
                    {
                        timer.Elapsed -= timer.Delay;

                        if (!timer.Repeat)
                        {
                            timer.Enabled = false;
                        }

                        timer.Callback();
                    }
                }
            }
        }

        //Check this
        Functions.NeutralTimers.RemoveAll(x => !x.Enabled);
        Functions.LevelTimers.RemoveAll(x => !x.Enabled);
    }

    private void InitializeLevelScript()
    {
        LevelScript = Game.ScriptEngine.CreateObject<ILevelScript>($"MapScripts.Map{Id}", "LevelScript") ?? new DefaultLevelScript();
        if (LevelScript is LuaLevelScript luaScript)
        {
            luaScript.OnScriptInit();
        }
        LevelScript.OnLevelInit();
    }

    private void InitializeMutators()
    {
        byte lastAdditionIndex = 0;

        if (Game.Config.GameConfig.Mutators.Length > Mutators.Length)
        {
            //If you somehow manage to hit this, you fucked up big time, congrats!
            _logger.Warn($"Mutator-Count count is greater than {Mutators.Length}({Game.Config.GameConfig.Mutators.Length})! All mutators past the 8th one will be ignored!");
        }

        foreach (string? mutator in Game.Config.GameConfig.Mutators)
        {
            if (string.IsNullOrEmpty(mutator))
            {
                continue;
            }

            for (byte i = lastAdditionIndex; i < Mutators.Length; i++)
            {
                if (Mutators[i] is null)
                {
                    Mutators[i] = Game.ScriptEngine.CreateObject<IMutatorScript>($"MapScripts.Map{Id}.Mutators", $"Mutator{mutator}") ?? new DefaultMutator();
                    MutatorNames[i] = mutator;
                    lastAdditionIndex = i;
                    if (Mutators[i] is LuaMutator luaScript)
                    {
                        luaScript.Script = LuaScriptEngine.CreateTableReferringGlobal();
                        LuaScriptEngine.DoScript(mutator + ".lua", luaScript.Script);
                        luaScript.OnInit();
                        break;
                    }
                }
            }
        }
    }

    internal void PublishMutatorCallback(Action<IMutatorScript> action)
    {
        foreach (IMutatorScript? script in Mutators)
        {
            if (script is not null)
            {
                action(script);
            }
        }
    }


}