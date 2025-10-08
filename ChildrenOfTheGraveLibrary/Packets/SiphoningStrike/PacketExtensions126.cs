using System;
using ChildrenOfTheGraveEnumNetwork;
using System.Numerics;
using System.Collections.Generic;
using System.Diagnostics;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.Navigation;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using SiphoningStrike.Game.Common;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

namespace PacketDefinitions126
{
    public static class PacketExtensions126
    {
        /// <summary>
        /// Converts the given list of Vector2s into a list of CompressedWaypoints compatible with LeaguePackets, which are Vector2s with their origin at the center of the map.
        /// </summary>
        /// <param name="wp">List of Vector2s to convert.</param>
        /// <param name="grid">NavigationGrid to use for conversion.</param>
        /// <returns>List of CompressedWaypoints (Vector2s with origin at the center of the map).</returns>
        public static List<CompressedWaypoint> Vector2ToWaypoint(List<Vector2> wp, NavigationGrid grid)
        {
            return wp.ConvertAll(v => Vector2ToWaypoint(TranslateToCenteredCoordinates(v, grid)));
        }

        /// <summary>
        /// Converts the given CompressedWaypoint into a Vector2, however it does not unconvert it, meaning it will still have its origin at the center of the map.
        /// </summary>
        /// <param name="cw">CompressedWaypoint to convert.</param>
        /// <returns>Vector2 with equivalent coordinates.</returns>
        public static Vector2 WaypointToVector2(CompressedWaypoint cw)
        {
            return new Vector2(cw.X, cw.Y);
        }

        /// <summary>
        /// Converts the given Vector2 into a CompressedWaypoint, however the origin is not converted. Vector2 must have its origin at the center of the map before conversion.
        /// </summary>
        /// <param name="cw">Vector2 to convert.</param>
        /// <returns>CompressedWaypoint with equivalent coordinates.</returns>
        public static CompressedWaypoint Vector2ToWaypoint(Vector2 cw)
        {
            return new CompressedWaypoint((short)cw.X, (short)cw.Y);
        }

        /// <summary>
        /// Converts the given Vector2 back into a Vector2 with an origin at the bottom left corner of the map.
        /// </summary>
        /// <param name="vector">Vector2 to convert.</param>
        /// <param name="grid">NavigationGrid used for grabbing center of the map.</param>
        /// <returns>Vector2 with origin at the center of the map.</returns>
        public static Vector2 TranslateFromCenteredCoordinates(Vector2 vector, NavigationGrid grid)
        {
            // For unk reason coordinates are translated to 0,0 as a map center, so we gotta get back the original
            // mapSize contains the real center point coordinates, meaning width/2, height/2
            //.126 use offset for calculation
            //return 2 * vector + grid.MiddleOfMap;
            return new Vector2((2 * vector.X) + grid.OffsetX, (2 * vector.Y) + grid.OffsetZ);
        }

        /// <summary>
        /// Converts the given Vector2 into a Vector2 with an origin at the center of the map.
        /// </summary>
        /// <param name="vector">Vector2 to convert.</param>
        /// <param name="grid">NavigationGrid used for grabbing center of the map.</param>
        /// <returns>Vector2 with origin at the center of the map.</returns>
        public static Vector2 TranslateToCenteredCoordinates(Vector2 vector, NavigationGrid grid)
        {
            // For unk reason coordinates are translated to 0,0 as a map center, so we gotta get back the original
            // mapSize contains the real center point coordinates, meaning width/2, height/2
            //.126 use offset for calculation
            // return (vector + grid.MiddleOfMap) / 2;
            return new Vector2((vector.X - grid.OffsetX) / 2, (vector.Y - grid.OffsetZ) / 2);
        }

        /// <summary>
        /// Creates the MovementDataStop.
        /// </summary>
        /// <param name="o">GameObject to create MovementData for.</param>
        public static MovementDataStop CreateMovementDataStop(GameObject o)
        {
            return new MovementDataStop
            {
                SyncID = Environment.TickCount,
                Position = o.Position,
                Forward = o.Direction.ToVector2()
            };
        }

        /// <summary>
        /// Creates the MovementDataNone.
        /// </summary>
        /// <param name="o">GameObject to create MovementData for.</param>
        public static MovementDataNone CreateMovementDataNone(GameObject o)
        {
            return new MovementDataNone
            {
                SyncID = 0 // Always zero in replays
            };
        }

        private static List<CompressedWaypoint> GetCenteredWaypoints(AttackableUnit unit, NavigationGrid grid)
        {
            var currentWaypoints = new List<Vector2>(unit.Waypoints);

            // Protection 
            if (currentWaypoints.Count == 0)
            {

                currentWaypoints.Add(unit.Position);
                Console.WriteLine($"[DEBUG] Empty waypoints for {unit.Name} (NetId: {unit.NetId}), using current position: {unit.Position}");
            }

            //why get this if normally we setup waypoint position in first position ? 
            if (currentWaypoints[0] != unit.Position)
            {
                currentWaypoints[0] = unit.Position;
            }

            //   currentWaypoints.Insert(0, unit.Position);
            int count = 2 + (currentWaypoints.Count - 1 - unit.CurrentWaypointKey);

            //if (count >= 2) ?? why equal ?  
            if (count > 2)
            {
                // Protection contre RemoveRange avec des indices invalides
                if (count < currentWaypoints.Count)
                {
                    currentWaypoints.RemoveRange(1, currentWaypoints.Count - count);
                }
            }
            return ToCenteredWaypoints(grid, currentWaypoints);
        }

        private static List<CompressedWaypoint> ToCenteredWaypoints(NavigationGrid grid, List<Vector2> currentWaypoints)
        {
            return currentWaypoints.ConvertAll(v => Vector2ToWaypoint(TranslateToCenteredCoordinates(v, grid)));
        }

        /// <summary>
        /// Creates the MovementDataNormal.
        /// </summary>
        /// <param name="unit">AttackableUnit to create MovementData for.</param>
        /// <param name="grid">NavigationGrid used for grabbing center of the map.</param>
        /// <returns>MovementDataNormal if unit has enough waypoints (>= 1), otherwise MovementDataStop.</returns>
        public static MovementData CreateMovementDataNormalIfPossible(AttackableUnit unit, NavigationGrid grid, bool useTeleportID = false)
        {
            // Prevent 0 waypoints packet error.
            if (unit.Waypoints.Count < 1)
            {
                return CreateMovementDataStop(unit);
            }
            return CreateMovementDataNormal(unit, grid, useTeleportID);
        }

        /// <summary>
        /// Creates the MovementDataNormal.
        /// </summary>
        /// <param name="unit">AttackableUnit to create MovementData for.</param>
        /// <param name="grid">NavigationGrid used for grabbing center of the map.</param>
        /// <returns>MovementDataNormal if unit has enough waypoints (>= 1), otherwise crashes.</returns>
        public static MovementDataNormal CreateMovementDataNormal(AttackableUnit unit, NavigationGrid grid, bool useTeleportID = false)
        {
            // Debug.Assert(unit.Waypoints.Count >= 1);
            if (unit is Pet)
            {
                var mv = new MovementDataNormal
                {
                    SyncID = 0,
                    TeleportNetID = unit.NetId,
                    HasTeleportID = useTeleportID,
                    TeleportID = useTeleportID ? unit.TeleportID : (byte)0,
                    Waypoints = GetCenteredWaypoints(unit, grid)

                };
                if (mv.Waypoints.Count >= 1)
                {
                    return mv;
                }
                return null;
            }
            else
            {
                var mv = new MovementDataNormal
                {
                    SyncID = Environment.TickCount,
                    TeleportNetID = unit.NetId,
                    HasTeleportID = useTeleportID,
                    TeleportID = useTeleportID ? unit.TeleportID : (byte)0,
                    Waypoints = GetCenteredWaypoints(unit, grid)
                };
                if (mv.Waypoints.Count >= 1)
                {
                    return mv;
                }
                return null;

            }
        }

        /// <summary>
        /// Creates the MovementDataNormal.
        /// </summary>
        /// <param name="unit">AttackableUnit to create MovementData for.</param>
        /// <param name="grid">NavigationGrid used for grabbing center of the map.</param>
        /// <returns>
        /// MovementDataWithSpeed if unit has MovementParameters (!= null),
        /// else if unit has enough waypoints (>= 1) - MovementDataNormal,
        /// otherwise MovementDataStop.
        /// </returns>
        public static MovementData CreateMovementDataWithSpeedIfPossible(AttackableUnit unit, NavigationGrid grid, bool useTeleportID = false)
        {
            // Prevent 0 waypoints packet error.
            if (unit.Waypoints.Count < 1)
            {
                return CreateMovementDataStop(unit);
            }
            else if (unit.MovementParameters == null)
            {
                return CreateMovementDataNormal(unit, grid, useTeleportID);
            }
            return CreateMovementDataWithSpeed(unit, grid, useTeleportID);
        }

        /// <summary>
        /// Creates the MovementDataWithSpeed.
        /// </summary>
        /// <param name="unit">AttackableUnit to create MovementData for.</param>
        /// <param name="grid">NavigationGrid used for grabbing center of the map.</param>
        /// <returns>MovementDataWithSpeed if unit has MovementParameters (!= null) and enough waypoints (>= 1), otherwise crashes.</returns>
        public static MovementDataWithSpeed CreateMovementDataWithSpeed(AttackableUnit unit, NavigationGrid grid, bool useTeleportID = false)
        {
            Debug.Assert(unit.Waypoints.Count >= 1);
            Debug.Assert(unit.MovementParameters != null);





            var mdws = new MovementDataWithSpeed
            {
                SyncID = Environment.TickCount,
                TeleportNetID = unit.NetId,
                HasTeleportID = useTeleportID,
                TeleportID = useTeleportID ? unit.TeleportID : (byte)0,
                //Waypoints = GetCenteredWaypoints(unit, grid),
                SpeedParams = new SpeedParams
                {
                    PathSpeedOverride = unit.MovementParameters.PathSpeedOverride,
                    ParabolicGravity = unit.MovementParameters.ParabolicGravity,
                    ParabolicStartPoint = unit.MovementParameters.ParabolicStartPoint,
                    Facing = unit.MovementParameters.KeepFacingDirection,
                    FollowNetID = unit.MovementParameters.FollowUnit?.NetId ?? 0,
                    FollowDistance = unit.MovementParameters.FollowDistance,
                    FollowBackDistance = unit.MovementParameters.FollowBackDistance,
                    FollowTravelTime = unit.MovementParameters.FollowTravelTime
                }
            };

            if (unit.MovementParameters.FollowUnit != null)
            {
                mdws.SpeedParams.FollowNetID = unit.MovementParameters.FollowUnit.NetId;
                mdws.Waypoints = ToCenteredWaypoints(grid, new() { unit.Position, unit.MovementParameters.FollowUnit.Position });
            }
            else
            {
                mdws.Waypoints = ToCenteredWaypoints(grid, new() { unit.Position, unit.MovementParameters.FollowPosition });
            }

            return mdws;
        }
    }
}
