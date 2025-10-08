using System;
using System.Diagnostics;
using System.Runtime;
using log4net;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Performance
{
    /// <summary>
    /// Monitors and logs Garbage Collection statistics for performance analysis
    /// </summary>
    public static class GCMonitor
    {
        private static readonly ILog _logger = LoggerProvider.GetLogger();
        private static readonly Stopwatch _stopwatch = new();

        // GC collection counts from last measurement
        private static int _lastGen0Count;
        private static int _lastGen1Count;
        private static int _lastGen2Count;

        // Timing
        private static double _lastReportTime;
        private static readonly double _reportIntervalSeconds = 1.0; // Report every second

        // Memory tracking
        private static long _lastTotalMemory;
        private static long _peakMemory;

        // Thread-safe allocation tracking using total memory approach
        private static long _lastTotalMemoryForAllocations;
        private static readonly object _allocationLock = new();



        /// <summary>
        /// Initializes the GC monitor
        /// </summary>
        public static void Initialize()
        {
            _stopwatch.Start();

            // Initialize baseline GC counts
            _lastGen0Count = GC.CollectionCount(0);
            _lastGen1Count = GC.CollectionCount(1);
            _lastGen2Count = GC.CollectionCount(2);

            // Initialize memory baseline
            _lastTotalMemory = GC.GetTotalMemory(false);
            _peakMemory = _lastTotalMemory;

            _lastReportTime = 0;

            // Use total memory for allocation tracking instead of per-thread
            lock (_allocationLock)
            {
                _lastTotalMemoryForAllocations = _lastTotalMemory;
            }

            //_logger.Info("GC Monitor initialized - tracking garbage collection statistics");
            MonitoringConsoleClass.Log("GC Monitor initialized - tracking garbage collection statistics");
        }

        /// <summary>
        /// Updates GC monitoring and logs stats if report interval has elapsed
        /// </summary>
        public static void Update()
        {
            var currentTime = _stopwatch.Elapsed.TotalSeconds;

            if (currentTime - _lastReportTime >= _reportIntervalSeconds)
            {
                ReportGCStats();
                _lastReportTime = currentTime;
            }
        }

        /// <summary>
        /// Forces an immediate report of GC statistics
        /// </summary>
        public static void ForceReport()
        {
            ReportGCStats();
            _lastReportTime = _stopwatch.Elapsed.TotalSeconds;
        }

        private static void ReportGCStats()
        {
            // Get current GC counts
            int gen0Count = GC.CollectionCount(0);
            int gen1Count = GC.CollectionCount(1);
            int gen2Count = GC.CollectionCount(2);

            // Calculate collections since last report
            int gen0Collections = gen0Count - _lastGen0Count;
            int gen1Collections = gen1Count - _lastGen1Count;
            int gen2Collections = gen2Count - _lastGen2Count;

            // Get memory info
            long currentMemory = GC.GetTotalMemory(false);
            long memoryDelta = currentMemory - _lastTotalMemory;

            // Update peak memory
            if (currentMemory > _peakMemory)
            {
                _peakMemory = currentMemory;
            }

            // Calculate allocation rate using memory delta as approximation
            // This is more stable than per-thread tracking across multiple threads
            long allocationDelta;
            lock (_allocationLock)
            {
                allocationDelta = currentMemory - _lastTotalMemoryForAllocations;
                _lastTotalMemoryForAllocations = currentMemory;
            }

            // Safeguard against negative deltas (can happen after GC)
            // Use absolute value for allocation rate since GC can cause negative deltas
            long absAllocationDelta = Math.Abs(allocationDelta);

            // Build report string
            var report = new System.Text.StringBuilder();
            report.AppendLine("=== GC Statistics Report ===");
            report.AppendFormat("Collections: Gen0={0} (+{1}), Gen1={2} (+{3}), Gen2={4} (+{5})\n",
                gen0Count, gen0Collections,
                gen1Count, gen1Collections,
                gen2Count, gen2Collections);
            report.AppendFormat("Memory: Current={0:N0} KB, Delta={1:+#;-#;0} KB, Peak={2:N0} KB\n",
                currentMemory / 1024,
                memoryDelta / 1024,
                _peakMemory / 1024);

            // Report allocation rate with safeguards
            if (_reportIntervalSeconds > 0)
            {
                double allocationRateKBPerSec = absAllocationDelta / 1024.0 / _reportIntervalSeconds;
                report.AppendFormat("Net Memory Change Rate: {0:N0} KB/s (abs delta)\n", allocationRateKBPerSec);

                // Add warning for suspicious allocation rates
                if (allocationRateKBPerSec > 1024 * 1024) // > 1GB/s
                {
                    report.AppendFormat("WARNING: Extremely high allocation rate detected! Check for memory leaks.\n");
                }
            }

            report.AppendFormat("GC Mode: {0}, Server GC: {1}\n",
                GCSettings.IsServerGC ? "Server" : "Workstation",
                GCSettings.IsServerGC);

            // Log with appropriate level based on collections
            if (gen2Collections > 0)
            {
                //_logger.Warn(report.ToString());
                MonitoringConsoleClass.Log(report.ToString());
            }
            else if (gen1Collections > 0)
            {
                // _logger.Info(report.ToString());
                MonitoringConsoleClass.Log(report.ToString());
            }
            else
            {
                //_logger.Debug(report.ToString());
                MonitoringConsoleClass.Log(report.ToString());
            }

            // Update last values
            _lastGen0Count = gen0Count;
            _lastGen1Count = gen1Count;
            _lastGen2Count = gen2Count;
            _lastTotalMemory = currentMemory;
        }

        /// <summary>
        /// Gets a summary of current GC statistics
        /// </summary>
        public static string GetSummary()
        {
            var summary = new System.Text.StringBuilder();
            summary.AppendFormat("Total Collections: Gen0={0}, Gen1={1}, Gen2={2}\n",
                GC.CollectionCount(0), GC.CollectionCount(1), GC.CollectionCount(2));
            summary.AppendFormat("Current Memory: {0:N0} KB, Peak: {1:N0} KB\n",
                GC.GetTotalMemory(false) / 1024, _peakMemory / 1024);
            summary.AppendFormat("GC Settings: IsServerGC={0}, LatencyMode={1}\n",
                GCSettings.IsServerGC, GCSettings.LatencyMode);
            return summary.ToString();
        }
    }
}