using System;
using System.Linq;
using System.Timers;
using CoTGEnumNetwork.Enums;
using CoTGEnumNetwork.NetInfo;
using SiphoningStrike.Game.Events;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.Logging;
using log4net;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.Handlers;

internal class StateHandler
{
    private static ILog _logger = LoggerProvider.GetLogger();

    /// <summary>
    /// Whether the server is running or not. Usually true after the network loop has started via CoTGServerLauncher.
    /// </summary>
    internal GameState State { get; private set; }
    internal float PauseTime { get; private set; }
    internal float ForceStartTime { get; private set; }
    internal float PauseTimeLeft { get; private set; }
    internal float ForceStartTimeLeft { get; private set; }


    /// <summary>
    /// Creates a new instance of the match state handler.
    /// </summary>
    /// <param name="pauseTime">Amount of pause time in minutes allotted to the match</param>
    /// <param name="forceStartTime">Amount of time in seconds before the game force starts</param>
    public StateHandler(float pauseTime, float forceStartTime)
    {
        PauseTime = pauseTime * 60;
        PauseTimeLeft = PauseTime * 1000;
        ForceStartTime = forceStartTime;
        ForceStartTimeLeft = forceStartTime;
        State = GameState.PREGAME;
    }

    // GameState Methods

    /// <summary>
    /// Function to set the game as running. Allows the game loop to start.
    /// </summary>
    internal void Start()
    {
        SetGameState(GameState.GAMELOOP);
        try
        {
            Game.Map.GameMode.OnMatchPreStart();
            Game.Map.PublishMutatorCallback(script => script.OnInitClient());
            Game.Map.LevelScript.OnLevelInitServer();
            Game.Map.PublishMutatorCallback(script => script.OnInitServer());
            Game.Map.GameMode.OnMatchStart();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }
    }

    /// <summary>
    /// Forces the game to start sooner as long as at least one player is connected.
    /// </summary>
    internal void ForceStart()
    {
        var players = Game.PlayerManager.GetPlayers(false);
        var isPossibleToStart = players.Any(p => !p.IsDisconnected);

        if (!isPossibleToStart)
        {
            return;
        }

        foreach (var player in players)
        {
            if (State is GameState.PAUSE)
            {
                PausePacketNotify(player, GetPauseTimeLeft(), true);
            }

            GameStartNotify(player.ClientId);

            if (State is GameState.GAMELOOP)
            {
                var announcement = new OnReconnect { OtherNetID = player.Champion.NetId };
                OnEventWorldNotify(announcement, player.Champion);
            }

            if (!player.IsMatchingVersion)
            {
                ChatManager.System(
                    player.ClientId,
                    "Your client version does not match the server. Check the server log for more information."
                );
            }

            SynchSimTimeNotify2(player.ClientId, Game.Time.GameTime);
            SyncMissionStartTimeS2CNotify(player.ClientId, Game.Time.GameTime);
        }

        Start();


    }

    /// <summary>
    /// Temporarily prevents the game loop from continuing and notifies players.
    /// </summary>
    internal void Pause(ClientInfo pauser = null)
    {
        if (PauseTimeLeft <= 0)
        {
            return;
        }

        if (pauser != null)
        {
            ChatManager.System($"{pauser.Name} paused the match.");
        }

        SetGameState(GameState.PAUSE);
        foreach (var player in Game.PlayerManager.GetPlayers(false))
        {
            PausePacketNotify(player, GetPauseTimeLeft(), true);
        }
    }

    /// <summary>
    /// Releases the game loop from a temporary pause.
    /// </summary>
    internal void Unpause(Champion unpauser = null)
    {
        if (unpauser == null)
        {
            unpauser = Game.PlayerManager.GetPlayers(false).First().Champion;
        }

        // Pure water framing
        var players = Game.PlayerManager.GetPlayers();
        foreach (var player in players)
        {
            ResumePacketNotify(unpauser, player, false);
        }

        SetGameState(GameState.GAMELOOP);
    }

    /// <summary>
    /// Prepares to close the Game 10 seconds after being called.
    /// </summary>
    internal void SetGameToExit()
    {
        _logger.Info("Game is over. Game Server will exit in 10 seconds.");
        SetGameState(GameState.PRE_EXIT);
        var timer = new Timer(10000) { AutoReset = false };
        timer.Elapsed += (a, b) => SetGameState(GameState.EXIT);
        timer.Start();
    }

    /// <summary>
    /// Method that sets the GameState.
    ///
    /// Official:
    /// PreGame
    /// Spawn
    /// GameLoop
    /// EndGame
    /// Pre_Exit
    /// Exit
    ///
    /// UnOfficial:
    /// Pause
    /// </summary>
    /// <param name="state">GameState enum that sets the match run state</param>
    internal void SetGameState(GameState state)
    {
        State = state;
    }

    internal int GetPauseTimeLeft()
    {
        return (int)PauseTimeLeft / 1000;
    }

    internal void UpdatePreGame()
    {
        if (ForceStartTimeLeft > 0)
        {
            if (ForceStartTimeLeft <= Game.Time.DeltaTime && !Game.PlayerManager.CheckIfAllPlayersLeft())
            {
                _logger.Info($"Patience is over. The game will start earlier.");
                ForceStart();
            }
        }

        ForceStartTimeLeft -= Game.Time.DeltaTime;
    }

    internal void UpdatePause()
    {
        PauseTimeLeft -= Game.Time.DeltaTime;
        if (PauseTimeLeft <= 0)
        {
            Game.StateHandler.Unpause();
        }
    }
}