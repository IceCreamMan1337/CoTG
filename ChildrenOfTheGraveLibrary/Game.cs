using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Chatbox;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Handlers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using MirrorImage;
using PacketVersioning;
using ChildrenOfTheGraveLibrary.Vision;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Performance;


namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer
{
    /// <summary>
    /// Class that contains and manages all qualities of the game such as managers for networking and game mechanics, as well as the starting, pausing, and stopping of the game.
    /// </summary>
    public partial class Game
    {
        // Crucial Game Vars

        private static List<GameScriptTimer> _gameScriptTimers = new();
        /// <summary>
        /// Class containing all information about the game's configuration such as game content location, map spawn points, whether cheat commands are enabled, etc.
        /// </summary>
        public static Config Config { get; protected set; }

        // Function Vars

        private static ILog _logger = LoggerProvider.GetLogger();

        // Server Vars

        /// <summary>
        /// Time until the game unpauses (if paused).
        /// </summary>
        public static long PauseTimeLeft { get; private set; }



        private static PktVersioning PktVersioning { get; set; }

        internal static PktVersioning PktVersioningNotifier { get; set; }

        /// <summary>
        /// Handler for request packets sent by game clients.
        /// </summary>
        internal static NetworkHandler<BasePacket> RequestHandler { get; set; }

        /// <summary>
        /// Interface containing all (public) functions used by ObjectManager. ObjectManager manages GameObjects, their properties, and their interactions such as being added, removed, colliding with other objects or terrain, vision, teams, etc.
        /// </summary>
        internal static ObjectManager ObjectManager { get; private set; } = new();

        /// <summary>
        /// Manages vision for game objects, teams, and regions
        /// </summary>
        internal static VisionManager VisionManager { get; private set; } = new();

        /// <summary>
        /// Class which manages all Spell.
        /// </summary>
        internal static SpellManager SpellManager { get; private set; } = new();
        /// <summary>
        /// Class which manages all chat based commands.
        /// </summary>
        internal static ChatCommandManager ChatCommandManager { get; private set; }
        /// <summary>
        /// Interface of functions used to identify players or their properties (such as their champion).
        /// </summary>
        public static PlayerManager PlayerManager { get; private set; } = new();
        /// <summary>
        /// Class that handles the loading and passing of GameClient Info
        /// </summary>
        internal static ContentManager ContentManager { get; private set; }
        /// <summary>
        /// Contains all map related game settings such as collision handler, navigation grid, announcer events, and map properties. Doubles as a Handler/Manager for all MapScripts.
        /// </summary>
        internal static MapHandler Map { get; private set; }
        internal static StateHandler StateHandler { get; private set; }

        // Scripting Vars

        /// <summary>
        /// Class that compiles and loads all scripts which will be used for the game (ex: spells, items, AI, maps, etc).
        /// </summary>
        internal static CSharpScriptEngine ScriptEngine { get; private set; }
        internal static FileSystemWatcher? ScriptsHotReloadWatcher { get; private set; }

        public static string nameofreplay { get; set; }

        public static bool isUploadingfinished { get; set; } = false;

        public static List<Action> pendingActions = new();

        /// <summary>
        /// Instantiates all game managers and handlers.
        /// </summary>
        public Game(Config config, ushort port)
        {
            nameofreplay = $"Replay_of_{DateTime.Now:yyyyMMdd_HHmmss}.rep";

            Config = config;
            Time.SetTicksPerSecond(config.TickRate);

            _logger.Info("Creating Server Instance");

            // Initialize VisionManager
            VisionManager = new VisionManager();

            ContentManager = new ContentManager(Config);
            StateHandler = new StateHandler(30, Config.ForcedStart);
            ChatCommandManager = new ChatCommandManager(Config.ChatCheatsEnabled);

            ScriptEngine = new(Config.AssemblyPaths);

            Map = new MapHandler(Config.GameConfig.Map);
            Map.LoadMapObjects();

            _logger.Info("Setting up Networking...");
            SetupNetworking(port, Config.Players);

            _logger.Info("Setting-Up Player Clients...");
            PlayerManager.AddPlayers(Config.Players);

            Map.OnLevelLoad();

            _logger.Info("ChildrenOfTheGraveServer is ready to start!");


            // Initialize GC monitoring
            if (Config.EnableAllocationTracker)
            {

                MonitoringConsoleClass.Start();

                GCMonitor.Initialize();
                _logger.Info("GC Monitoring enabled");
            }

            // Initialize allocation tracking
            AllocationTracker.Enable(Config.EnableAllocationTracker);

            // Test allocation tracking
            AllocationTracker.MeasureAndTrack("Test.Allocation", () =>
            {
                var test = new byte[1024 * 1024]; // 1 MB allocation
            });


            PktVersioningNotifier = new PktVersioning();
        }

        /// <summary>
        /// TODO    : this will permit to communicate with api_server for say if ChildrenOfTheGraveServer start game or get error 
        /// for moment is just an simple heartbeat 
        /// </summary>
        /// <param name="launcherServerUrl"></param>
        public static void SendHeartbeat(string launcherServerUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent("heartbeat", Encoding.UTF8, "text/plain");
                    var response = client.PostAsync(launcherServerUrl, content).Result; // Attente synchrone

                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
            }
        }




        /// <summary>
        /// Function which initiates ticking of the game's logic.
        /// </summary>
        public void GameLoop()
        {
            double timeout = 0;

            Stopwatch lastMapDurationWatch = new();

            //bool wasNotPaused = true;

            while (StateHandler.State is not GameState.EXIT)
            {
                double lastSleepDuration = lastMapDurationWatch.Elapsed.TotalMilliseconds;
                lastMapDurationWatch.Restart();

                Time.Update((float)lastSleepDuration);

                switch (StateHandler.State)
                {
                    case GameState.PREGAME:
                        StateHandler.UpdatePreGame();
                        break;
                    case GameState.GAMELOOP:
                        Update();
                        break;
                    case GameState.PAUSE:
                        StateHandler.UpdatePause();
                        break;
                    case GameState.SPAWN:
                    case GameState.ENDGAME:
                    case GameState.PRE_EXIT:
                    case GameState.EXIT:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Invalid GameState: {StateHandler.State}");
                }

                double lastUpdateDuration = lastMapDurationWatch.Elapsed.TotalMilliseconds;
                double oversleep = lastSleepDuration - timeout;
                timeout = Math.Max(0, Time.TickRate - lastUpdateDuration - oversleep);


                // Update GC monitoring
                if (Config.EnableAllocationTracker)
                {
                    GCMonitor.Update();
                    AllocationTracker.Update();
                }

                if (pendingActions.Count > 0 && StateHandler.State == GameState.GAMELOOP)
                {
                    foreach (var action in pendingActions)
                    {

                        action();
                    }
                    pendingActions.Clear();
                }
                PktVersioningNotifier.NetLoop((uint)timeout);
            }



            while (!isUploadingfinished)
            {

            }
        }

        /// <summary>
        /// Function called every tick of the game.
        /// </summary>
        private static void Update()
        {
            // This section dictates the priority of updates.
            Time.GameTime += Time.ScaledDeltaTime;

            AllocationTracker.MeasureAndTrack("Map.Update", () => Map.Update());

            // Objects
            var startTime = DateTime.Now;
            var elapsedTime = (DateTime.Now - startTime).TotalMilliseconds;


            /*     // If the elapsed time is less than 100 ms, wait for the remaining time
                 if (elapsedTime < 100)
                 {
            */
            AllocationTracker.MeasureAndTrack("ObjectManager.Update", () => ObjectManager.Update());
            /*
                            elapsedTime -= 100;

                        }
            */
            AllocationTracker.MeasureAndTrack("ChatCommandManager.Update", () => ChatCommandManager.Update());


            //An error occurs here every end of game, cause is unknown
            try
            {
                // Iterate backwards to safely remove items during iteration
                for (int i = _gameScriptTimers.Count - 1; i >= 0; i--)
                {
                    var timer = _gameScriptTimers[i];
                    timer.Update();
                    if (timer.ToRemove)
                    {
                        _gameScriptTimers.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        // Networking Methods

        /// <summary>
        /// Sets up the server's needed networking classes and variables
        /// </summary>
        /// <param name="port"></param>
        /// <param name="players"></param>
        private static void SetupNetworking(ushort port, IReadOnlyList<PlayerConfig> players)
        {
            var blowfishKeys = new string[players.Count];
            for (var i = 0; i < players.Count; i++)
            {
                blowfishKeys[i] = players[i].BlowfishKey;
            }

            //TODO: Determine if ResponseHandler should still be used in the projects current/future state
            //ResponseHandler = new NetworkHandler<ICoreRequ�est>();

            RequestHandler = new NetworkHandler<MirrorImage.BasePacket>();
#if !DEBUG
            try
            {
#endif
            //setupserver 

            PktVersioning.SetupVersioningServer(port, blowfishKeys);
            PktVersioning.InitializePacketHandlers(RequestHandler);

#if !DEBUG
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
#endif

            // TODO: switch the notifier with ResponseHandler
            PktVersioning.InitializePacketNotifier();

        }

        /// <summary>
        /// Registers Request Handlers for each request packet.
        /// </summary>


        /// <summary>
        /// Adds a timer to the list of timers so that it ticks with the game.
        /// </summary>
        /// <param name="timer">Timer instance.</param>
        public static void AddGameScriptTimer(GameScriptTimer timer)
        {
            _gameScriptTimers.Add(timer);
        }

    }
}
