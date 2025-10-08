using System;
using System.Threading;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.Missile;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Performance
{
    /// <summary>
    /// Tracks allocations from various game systems
    /// </summary>
    public static class SystemAllocationTracker
    {
        private static readonly ThreadLocal<bool> _isTracking = new(() => false);

        /// <summary>
        /// Tracks allocations in ObjectManager.Update
        /// </summary>
        public static void TrackObjectManagerUpdate(ObjectManager manager, Action baseUpdate)
        {
            if (_isTracking.Value)
            {
                baseUpdate();
                return;
            }

            _isTracking.Value = true;
            try
            {
                // Track UpdateStats
                AllocationTracker.MeasureAndTrack("ObjectManager.UpdateStats", () =>
                {
                    // Call private UpdateStats via reflection
                    var method = manager.GetType().GetMethod("UpdateStats",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    method?.Invoke(manager, null);
                });

                // Track UpdateActions  
                AllocationTracker.MeasureAndTrack("ObjectManager.UpdateActions", () =>
                {
                    var method = manager.GetType().GetMethod("UpdateActions",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    method?.Invoke(manager, null);
                });

                // Track object removal
                AllocationTracker.MeasureAndTrack("ObjectManager.RemoveObjects", () =>
                {
                    // Rest of the update logic
                    baseUpdate();
                });
            }
            finally
            {
                _isTracking.Value = false;
            }
        }

        /// <summary>
        /// Tracks allocations from specific object types
        /// </summary>
        public static void TrackObjectUpdate(GameObject obj)
        {
            if (_isTracking.Value || !AllocationTracker.IsEnabled) return;

            _isTracking.Value = true;
            try
            {
                string category = obj switch
                {
                    Champion => "Champion.Update",
                    NeutralMinion => "NeutralMinion.Update",
                    Minion => "Minion.Update",
                    BaseTurret => "Turret.Update",
                    SpellMissile => "Missile.Update",
                    _ => "GameObject.Update"
                };

                using (var scope = new AllocationScope(category))
                {
                    obj.Update();
                }
            }
            finally
            {
                _isTracking.Value = false;
            }
        }

        /// <summary>
        /// Tracks string allocations
        /// </summary>
        public static void TrackStringOperation(string operation, Func<string> stringFunc)
        {
            if (_isTracking.Value || !AllocationTracker.IsEnabled)
            {
                stringFunc();
                return;
            }

            using (var scope = new AllocationScope($"String.{operation}"))
            {
                stringFunc();
            }
        }

        /// <summary>
        /// Tracks network packet allocations
        /// </summary>
        public static void TrackPacketCreation(string packetType, Action createPacket)
        {
            if (_isTracking.Value || !AllocationTracker.IsEnabled)
            {
                createPacket();
                return;
            }

            using (var scope = new AllocationScope($"Packet.{packetType}"))
            {
                createPacket();
            }
        }

        /// <summary>
        /// Tracks script execution allocations
        /// </summary>
        public static void TrackScriptExecution(string scriptName, Action executeScript)
        {
            if (_isTracking.Value || !AllocationTracker.IsEnabled)
            {
                executeScript();
                return;
            }

            using (var scope = new AllocationScope($"Script.{scriptName}"))
            {
                executeScript();
            }
        }

        /// <summary>
        /// Tracks collection operations
        /// </summary>
        public static T TrackCollectionOperation<T>(string operation, Func<T> collectionFunc)
        {
            if (_isTracking.Value || !AllocationTracker.IsEnabled)
            {
                return collectionFunc();
            }

            using (var scope = new AllocationScope($"Collection.{operation}"))
            {
                return collectionFunc();
            }
        }
    }
}