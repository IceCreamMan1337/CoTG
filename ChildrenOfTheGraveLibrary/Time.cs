using static PacketVersioning.PktVersioning;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer
{
    public partial class Game
    {
        public static class Time
        {
            internal static float TickRate { get; private set; }
            internal static float NextTimeSyncTimer { get; set; }
            internal static float BaseHz { get; private set; } = 30.0f; // Fréquence de base en Hz
            internal static float CurrentHz { get; private set; } = 30.0f; // Fréquence actuelle en Hz
            internal static float TimeScale { get; private set; } = 1.0f; // Facteur d'accélération du temps

            public static float GameTime { get; internal set; }
            public static float DeltaTime { get; private set; } // Temps réel (pour les scripts Lua)
            public static float DeltaTimeMilliseconds { get; private set; }
            public static float DeltaTimeSeconds { get; private set; }
            public static float ScaledDeltaTime { get; private set; } // DeltaTime accéléré (pour la simulation)

            static Time()
            {
                // League public servers run at 30fps while competitive run at 60fps.
                SetTicksPerSecond(BaseHz);
                NextTimeSyncTimer = 10 * 1000;
            }

            internal static void SetTickRate(float milliseconds)
            {
                TickRate = milliseconds;
                CurrentHz = 1000.0f / milliseconds;
                UpdateTimeScale();
            }

            internal static void SetTicksPerSecond(float hz)
            {
                CurrentHz = hz;
                TickRate = 1000.0f / hz;
                UpdateTimeScale();
            }

            private static void UpdateTimeScale()
            {
                // Calculer le facteur d'accélération basé sur la fréquence actuelle vs la fréquence de base
                TimeScale = CurrentHz / BaseHz;
            }

            // Nouvelle méthode pour ajuster la fréquence en Hz
            internal static void AdjustHz(int deltaHz)
            {
                if (deltaHz == 0)
                    return;

                float newHz = CurrentHz + deltaHz;
                if (newHz < 1.0f)
                    newHz = 1.0f; // Clamp to sane minimum

                if (newHz == CurrentHz)
                    return; // No change

                SetTicksPerSecond(newHz);

                // Broadcast time-sync packets to clients
                BroadcastTimeSync();

                // Recalculer les intervalles de spawn des barraques avec le nouveau facteur d'accélération
                if (Game.Map != null)
                {
                    foreach (var teamBarracks in Barrack.Manager.Values)
                    {
                        foreach (var barrack in teamBarracks)
                        {
                            barrack.RecalculateSpawnIntervals();
                        }
                    }

                    // Log the change
                    _logger.Info($"Simulation tick rate adjusted: {newHz} Hz, tickDuration: {TickRate}ms, TimeScale: {TimeScale:F2}x");
                }
            }


            private static void BroadcastTimeSync()
            {
                // Envoyer les paquets de synchronisation aux clients
                float dt = TickRate / 1000.0f; // Convert to seconds
                float scale = CurrentHz / BaseHz;

                // Envoyer S2C_Set_Frequency
                SetFrequencyNotify(scale);

                // Envoyer S2C_Server_Tick  
                // TODO: Implémenter l'envoi du paquet S2C_Server_Tick avec dt

                SynchSimTimeNotify(GameTime);
            }

            internal static void Update(float deltaTime)
            {
                // Garder le temps réel pour les scripts Lua
                DeltaTime = DeltaTimeMilliseconds = deltaTime;
                DeltaTimeSeconds = DeltaTime / 1000.0f;

                // Calculer le delta time accéléré pour la simulation
                ScaledDeltaTime = DeltaTime * TimeScale;

                // By default, synchronize the game time between server and clients every 10 seconds
                NextTimeSyncTimer += DeltaTime; // Utiliser le temps réel pour la synchronisation
                if (NextTimeSyncTimer >= 10 * 1000)
                {
                    SynchSimTimeNotify(GameTime);
                    NextTimeSyncTimer = 0;
                }
            }
        }
    }
}