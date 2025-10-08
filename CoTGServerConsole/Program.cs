using CommandLine;
using CoTGServerConsole.Properties;
using CoTG.CoTGServer;
using CoTG.CoTGServer.Logging;
using CoTG.CoTGServerConsole.Logic;
using CoTG.CoTGServerConsole.Utility;
using log4net;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace CoTG.CoTGServerConsole
{
    /// <summary>
    /// Class representing the program piece, or commandline piece of the server; where everything starts (CoTGServerConsole -> CoTGServer, etc).
    /// </summary>
    internal class Program
    {
        // So we can print debug info via the command line interface.
        private static ILog _logger = LoggerProvider.GetLogger();

        private static void Main(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetWindowSize((int)(Console.LargestWindowWidth * 0.75f), Console.WindowHeight);
            }

            // Limit the number of ThreadPool threads to 2 to simulate 2 CPU cores
            ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);

            // Limit to 2 concurrent threads
            int maxThreads = Math.Max(1, Environment.ProcessorCount / 4); // Ex: 8 cœurs -> max 2 threads
            ThreadPool.SetMaxThreads(maxThreads, completionPortThreads);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(
                (sender, args) => _logger.Fatal(null, (Exception)args.ExceptionObject));

            // Culture can cause interference in reading numbers and dates
            var culture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // If the command line interface was ran with additional parameters (perhaps via a shortcut or just via another command line)
            // Refer to ArgsOptions for all possible launch parameters
            ArgsOptions parsedArgs = ArgsOptions.Parse(args);
            parsedArgs.GameInfoJson = LoadConfig(parsedArgs.GameInfoJsonPath, parsedArgs.GameInfoJson, Encoding.UTF8.GetString(Resources.GameInfo));

            var build = $"CoTG Build: 0.0.116";//{BuildInfo.ServerVersion} {BuildInfo.ServerBuildTime}";
            Console.Title = build;
            _logger.Debug(build);
            _logger.Info($"Game started on port: {parsedArgs.ServerPort}");

            Config _config = new(parsedArgs.GameInfoJson);
            Game _game = new(_config, parsedArgs.ServerPort);

#if DEBUG
            // When debugging, optionally the game client can be launched automatically given the path (placed in CoTGServerSettings.json) to the folder containing the League executable.
            string config = LoadConfig(parsedArgs.CoTGServerSettingsJsonPath, parsedArgs.CoTGServerSettingsJson, Encoding.UTF8.GetString(Resources.CoTGServerSettings));
            CoTGServerConfig configCoTGServerSettings = CoTGServerConfig.LoadFromJson(config);

            if (configCoTGServerSettings.AutoStartClient)
            {
                LaunchGameClient(configCoTGServerSettings, parsedArgs);
            }
            else
            {
                _logger.Info("Server is ready, clients can now connect.");
            }
#endif
            // This is where the actual CoTGServer starts.
#if !DEBUG
            try
            {
#endif
            _game.GameLoop();
#if !DEBUG
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
#endif
        }

        private static void LaunchGameClient(CoTGServerConfig CoTGServerSettings, ArgsOptions args)
        {
            var leaguePath = CoTGServerSettings.ClientLocation;
            if (Directory.Exists(leaguePath))
            {
                leaguePath = Path.Combine(leaguePath, "League of Legends.exe");
            }
            if (File.Exists(leaguePath))
            {
                string argumentsFor131 = $"""  "" "" "" "127.0.0.1 {args.ServerPort} {Game.Config.Players.First().BlowfishKey} {Game.Config.Players.First().PlayerID}" """;
                var startInfo = new ProcessStartInfo(leaguePath)
                {
                    Arguments = argumentsFor131,
                    WorkingDirectory = Path.GetDirectoryName(leaguePath)
                };

                var leagueProcess = Process.Start(startInfo);

                _logger.Info("Launching League of Legends. You can disable this in CoTGServerSettings.json.");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    WindowsConsoleCloseDetection.SetCloseHandler((_) =>
                    {
                        if (!leagueProcess.HasExited)
                        {
                            leagueProcess.Kill();
                        }
                        return true;
                    });
                }
            }
            else
            {
                _logger.Warn("Unable to find League of Legends.exe. Check the CoTGServerSettings.json settings and your League location.");
            }
        }

        /// <summary>
        /// Used to parse any of the configuration files used for the CoTGServer, ex: GameInfo.json or CoTGServerSettings.json. 
        /// </summary>
        /// <param name="filePath">Full path to the configuration file.</param>
        /// <param name="currentJsonString">String representing the content of the configuration file. Usually empty.</param>
        /// <param name="defaultJsonString">String representing the default content of the configuration file. Usually what is already defined in the respective configuration file.</param>
        /// <returns>The string defined in the configuration file or defined via launch arguments.</returns>
        private static string LoadConfig(string filePath, string currentJsonString, string defaultJsonString)
        {
            if (!string.IsNullOrEmpty(currentJsonString))
            {
                return currentJsonString;
            }
            try
            {
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
                var settingsDirectory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(settingsDirectory))
                {
                    throw new Exception(string.Format("Creating Config File failed. Invalid Path: {0}", filePath));
                }
                Directory.CreateDirectory(settingsDirectory);

                File.WriteAllText(filePath, defaultJsonString);
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }

            return defaultJsonString;
        }
    }

    /// <summary>
    /// Class housing launch arguments and their parsing used for the CoTGServerConsole.
    /// </summary>
    public class ArgsOptions
    {
        [Option("config", Default = "Settings/GameInfo.json")]
        public string GameInfoJsonPath { get; set; }

        [Option("config-CoTGServer", Default = "Settings/CoTGServerSettings.json")]
        public string CoTGServerSettingsJsonPath { get; set; }

        [Option("config-json", Default = "")]
        public string GameInfoJson { get; set; }

        [Option("config-CoTGServer-json", Default = "")]
        public string CoTGServerSettingsJson { get; set; }

        [Option("port", Default = (ushort)5119)]
        public ushort ServerPort { get; set; }


        public static ArgsOptions Parse(string[] args)
        {
            ArgsOptions options = null;
            Parser.Default.ParseArguments<ArgsOptions>(args).WithParsed(argOptions => options = argOptions);
            return options;
        }
    }
}
