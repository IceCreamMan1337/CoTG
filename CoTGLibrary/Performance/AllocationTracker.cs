using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using CoTG.CoTGServer.Logging;
using log4net;

namespace CoTG.CoTGServer.Performance
{
    /// <summary>
    /// Tracks allocations by source to identify what's allocating so much memory
    /// </summary>
    public static class AllocationTracker
    {
        private static readonly ILog _logger = LoggerProvider.GetLogger();
        private static readonly Dictionary<string, AllocationInfo> _allocations = new();
        private static readonly object _lock = new();
        private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private static double _lastReportTime;
        private static bool _enabled = false;

        private class AllocationInfo
        {
            public long TotalBytes;
            public long Count;
            public long BytesPerSecond;
            public DateTime LastReset = DateTime.Now;
        }

        public static void Enable(bool enabled)
        {
            _enabled = enabled;
            if (enabled)
            {
                // _logger.Info("Allocation tracking enabled");
                MonitoringConsoleClass.Log("Allocation tracking enabled");
                _lastReportTime = _stopwatch.Elapsed.TotalSeconds; // Reset report timer
            }
            else
            {
                _logger.Info("Allocation tracking disabled");
            }
        }

        public static bool IsEnabled => _enabled;

        /// <summary>
        /// Tracks an allocation from a specific source
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Track(string source, long bytes)
        {
            if (!_enabled) return;

            lock (_lock)
            {
                if (!_allocations.TryGetValue(source, out var info))
                {
                    info = new AllocationInfo();
                    _allocations[source] = info;
                }

                info.TotalBytes += bytes;
                info.Count++;
            }
        }

        /// <summary>
        /// Measures allocations in a code block
        /// </summary>
        public static long MeasureAllocations(Action action)
        {
            if (!_enabled)
            {
                action();
                return 0;
            }

            long before = GC.GetAllocatedBytesForCurrentThread();
            action();
            long after = GC.GetAllocatedBytesForCurrentThread();
            return after - before;
        }

        /// <summary>
        /// Measures and tracks allocations
        /// </summary>
        public static void MeasureAndTrack(string source, Action action)
        {
            if (!_enabled)
            {
                action();
                return;
            }

            long allocated = MeasureAllocations(action);
            if (allocated > 0)
            {
                Track(source, allocated);
            }
        }

        /// <summary>
        /// Force an immediate report
        /// </summary>
        public static void ForceReport()
        {
            if (!_enabled)
            {
                _logger.Info("Allocation tracking is not enabled. Use !trackalloc 1 to enable.");
                return;
            }

            ReportAllocations();
            _lastReportTime = _stopwatch.Elapsed.TotalSeconds;
        }

        /// <summary>
        /// Updates tracking and reports if interval has passed
        /// </summary>
        public static void Update()
        {
            if (!_enabled) return;

            var currentTime = _stopwatch.Elapsed.TotalSeconds;
            if (currentTime - _lastReportTime >= 5.0) // Report every 5 seconds
            {
                ReportAllocations();
                _lastReportTime = currentTime;
            }
        }

        private static void ReportAllocations()
        {
            var report = new StringBuilder();
            report.AppendLine("\n=== Allocation Sources Report ===");

            var sortedAllocations = new List<KeyValuePair<string, AllocationInfo>>();

            lock (_lock)
            {
                if (_allocations.Count == 0)
                {
                    _logger.Info("No allocations tracked yet. Make sure the game is running and allocation tracking is integrated.");
                    return;
                }

                foreach (var kvp in _allocations)
                {
                    var elapsed = (DateTime.Now - kvp.Value.LastReset).TotalSeconds;
                    if (elapsed > 0)
                    {
                        kvp.Value.BytesPerSecond = (long)(kvp.Value.TotalBytes / elapsed);
                    }
                    sortedAllocations.Add(kvp);
                }
            }

            // Sort by bytes per second descending
            sortedAllocations.Sort((a, b) => b.Value.BytesPerSecond.CompareTo(a.Value.BytesPerSecond));

            long totalBytesPerSec = 0;
            int shown = 0;

            foreach (var kvp in sortedAllocations)
            {
                if (shown >= 10) break; // Show top 10
                if (kvp.Value.BytesPerSecond < 1024 * 1024) continue; // Skip under 1 MB/s

                var mbPerSec = kvp.Value.BytesPerSecond / (1024.0 * 1024.0);
                var totalMb = kvp.Value.TotalBytes / (1024.0 * 1024.0);

                report.AppendFormat("{0,-30} {1,8:F1} MB/s  ({2,8:F1} MB total, {3,10:N0} allocs)\n",
                    kvp.Key,
                    mbPerSec,
                    totalMb,
                    kvp.Value.Count);

                totalBytesPerSec += kvp.Value.BytesPerSecond;
                shown++;
            }

            if (shown > 0)
            {
                report.AppendLine("----------------------------------------");
                report.AppendFormat("Top {0} sources: {1:F1} MB/s\n", shown, totalBytesPerSec / (1024.0 * 1024.0));
                //  _logger.Info(report.ToString());
                MonitoringConsoleClass.Log(report.ToString());
            }

            // Reset counters for next period
            lock (_lock)
            {
                foreach (var kvp in _allocations)
                {
                    kvp.Value.TotalBytes = 0;
                    kvp.Value.Count = 0;
                    kvp.Value.LastReset = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Gets current allocation stats
        /// </summary>
        public static Dictionary<string, long> GetCurrentStats()
        {
            var stats = new Dictionary<string, long>();
            lock (_lock)
            {
                foreach (var kvp in _allocations)
                {
                    stats[kvp.Key] = kvp.Value.TotalBytes;
                }
            }
            return stats;
        }
    }

    /// <summary>
    /// Helper class for tracking allocations in a using block
    /// </summary>
    public struct AllocationScope : IDisposable
    {
        private readonly string _source;
        private readonly long _startBytes;

        public AllocationScope(string source)
        {
            _source = source;
            _startBytes = GC.GetAllocatedBytesForCurrentThread();
        }

        public void Dispose()
        {
            long endBytes = GC.GetAllocatedBytesForCurrentThread();
            long allocated = endBytes - _startBytes;
            if (allocated > 0)
            {
                AllocationTracker.Track(_source, allocated);
            }
        }
    }
}