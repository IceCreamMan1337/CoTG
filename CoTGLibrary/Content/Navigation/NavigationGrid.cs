using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using CoTGEnumNetwork;
using CoTGEnumNetwork.Enums;
using CoTGLibrary.Extensions;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using System.Buffers;
using log4net;
using CoTG.CoTGServer.Logging;

namespace CoTG.CoTGServer.Content.Navigation
{
    public class NavigationGrid
    {
        ILog _logger = LoggerProvider.GetLogger();
        /// <summary>
        /// The minimum position on the NavigationGrid in normal coordinate space (bottom left in 2D).
        /// NavigationGridCells are undefined below these minimums.
        /// </summary>
        public Vector3 MinGridPosition { get; private set; }
        /// <summary>
        /// The maximum position on the NavigationGrid in normal coordinate space (top right in 2D).
        /// NavigationGridCells are undefined beyond these maximums.
        /// </summary>
        public Vector3 MaxGridPosition { get; private set; }
        /// <summary>
        /// Calculated resolution of the Navigation Grid (percentage of a cell 1 normal unit takes up, not to be confused with 1/CellSize).
        /// Multiple used to convert cell-based coordinates back into normal coordinates (CellCountX/Z / TranslationMaxGridPosition).
        /// </summary>
        public Vector3 TranslationMaxGridPosition { get; private set; }
        /// <summary>
        /// Ideal number of normal units a cell takes up (not fully accurate, but mostly, refer to TranslationMaxGridPosition for true size).
        /// </summary>
        public float CellSize { get; private set; }
        /// <summary>
        /// Width of the Navigation Grid in cells.
        /// </summary>
        public uint CellCountX { get; private set; }
        /// <summary>
        /// Height of the Navigation Grid in cells.
        /// </summary>
        public uint CellCountY { get; private set; }
        /// <summary>
        /// Array of all cells contained in this Navigation Grid.
        /// </summary>
        public NavigationGridCell[] Cells { get; private set; }
        /// <summary>
        /// Array of region tags where each index represents a cell's index.
        /// </summary>
        public uint[] RegionTags { get; private set; }
        /// <summary>
        /// Table of regions possible in the current Navigation Grid.
        /// Regions are the areas representing key points on a map. In the case of OldSR, this could be lanes top, middle, or bot, and the last region being jungle.
        /// *NOTE*: Regions only exist in Navigation Grids with a version of 5 or higher. OldSR is version 3.
        /// </summary>
        public NavigationRegionTagTable RegionTagTable { get; private set; }
        /// <summary>
        /// Number of sampled heights in the X coordinate plane.
        /// </summary>
        public uint SampledHeightsCountX { get; private set; }
        /// <summary>
        /// Number of sampled heights in the Y coordinate plane (Z coordinate in 3D space).
        /// </summary>
        public uint SampledHeightsCountY { get; private set; }
        /// <summary>
        /// Multiple used to convert from normal coordinates to an index format used to get sampled heights from the Navigation Grid.
        /// </summary>
        /// TODO: Seems to be volatile. If there ever comes a time when Navigation Grid editing becomes easy, that'd be the perfect time to rework the methods for getting sampled heights.
        public Vector2 SampledHeightsDistance { get; private set; }
        /// <summary>
        /// Array of sampled heights where each index represents a cell's index (depends on SampledHeightsCountX/Y).
        /// </summary>
        public float[] SampledHeights { get; private set; }
        /// <summary>
        /// Grid of hints.
        /// Function likely related to pathfinding.
        /// Currently Unused.
        /// </summary>
        public NavigationHintGrid HintGrid { get; private set; }
        /// <summary>
        /// Width of the map in normal coordinate space, where the origin is at (0, 0).
        /// *NOTE*: Not to be confused with MaxGridPosition.X, whos origin is at MinGridPosition.
        /// </summary>
        public float MapWidth { get; private set; }
        /// <summary>
        /// Height of the map in normal coordinate space, where the origin is at (0, 0).
        /// *NOTE*: Not to be confused with MaxGridPosition.Z, whos origin is at MinGridPosition.
        /// </summary>
        public float MapHeight { get; private set; }
        /// <summary>
        /// Center of the map in normal coordinate space.
        /// </summary>
        public Vector2 MiddleOfMap { get; private set; }

        public short OffsetX { get; private set; }

        public short OffsetZ { get; private set; }

        public NavigationGrid(string fileLocation) : this(File.OpenRead(fileLocation)) { }
        public NavigationGrid(byte[] buffer) : this(new MemoryStream(buffer)) { }
        public NavigationGrid(Stream stream)
        {
            using (BinaryReader br = new(stream))
            {
                byte major = br.ReadByte();
                ushort minor = major != 2 ? br.ReadUInt16() : (ushort)0;
                if (major != 2 && major != 3 && major != 5 && major != 7)
                {
                    throw new Exception(string.Format("Unsupported Navigation Grid Version: {0}.{1}", major, minor));
                }

                MinGridPosition = br.ReadVector3();
                MaxGridPosition = br.ReadVector3();

                CellSize = br.ReadSingle();
                CellCountX = br.ReadUInt32();
                CellCountY = br.ReadUInt32();

                Cells = new NavigationGridCell[CellCountX * CellCountY];
                RegionTags = new uint[CellCountX * CellCountY];

                if (major == 2 || major == 3 || major == 5)
                {
                    for (int i = 0; i < Cells.Length; i++)
                    {
                        Cells[i] = NavigationGridCell.ReadVersion5(br, i);
                    }

                    if (major == 5)
                    {
                        for (int i = 0; i < RegionTags.Length; i++)
                        {
                            RegionTags[i] = br.ReadUInt16();
                        }
                    }
                }
                else if (major == 7)
                {
                    for (int i = 0; i < Cells.Length; i++)
                    {
                        Cells[i] = NavigationGridCell.ReadVersion7(br, i);
                    }
                    for (int i = 0; i < Cells.Length; i++)
                    {
                        Cells[i].SetFlags((NavigationGridCellFlags)br.ReadUInt16());
                    }

                    for (int i = 0; i < RegionTags.Length; i++)
                    {
                        RegionTags[i] = br.ReadUInt32();
                    }
                }

                if (major >= 5)
                {
                    uint groupCount = major == 5 ? 4u : 8u;
                    RegionTagTable = new NavigationRegionTagTable(br, groupCount);
                }

                SampledHeightsCountX = br.ReadUInt32();
                SampledHeightsCountY = br.ReadUInt32();
                SampledHeightsDistance = br.ReadVector2();
                SampledHeights = new float[SampledHeightsCountX * SampledHeightsCountY];
                for (int i = 0; i < SampledHeights.Length; i++)
                {
                    SampledHeights[i] = br.ReadSingle();
                }

                HintGrid = new NavigationHintGrid(br);

                MapWidth = MaxGridPosition.X + MinGridPosition.X;
                MapHeight = MaxGridPosition.Z + MinGridPosition.Z;
                MiddleOfMap = new Vector2(MapWidth / 2, MapHeight / 2);

                OffsetX = (short)((int)(MinGridPosition.X + MaxGridPosition.X) / 2);
                OffsetZ = (short)((int)(MinGridPosition.Z + MaxGridPosition.Z) / 2);

                foreach (var navigationGridCell in Cells)
                {
                    navigationGridCell.SetOpen(true);
                }

            }
        }


        private readonly PriorityQueue<(NavigationGridCell, float), float> forwardQueue = new(512);
        private readonly PriorityQueue<(NavigationGridCell, float), float> backwardQueue = new(512);
        private static readonly ArrayPool<NavigationGridCell> _cellPool = ArrayPool<NavigationGridCell>.Shared;


        public List<Vector2> GetPath(Vector2 from, Vector2 to, AttackableUnit pathfinder)
        {
            if (from == to)
            {
                return null;
            }
            var fromNav = TranslateToNavGrid(from);
            var cellFrom = GetCell(fromNav, false);
            to = GetClosestTerrainExit(to, pathfinder.PathfindingRadius);
            var toNav = TranslateToNavGrid(to);
            var cellTo = GetCell(toNav, false);
            if (cellFrom == null || cellTo == null)
            {
                return null;
            }
            if (cellFrom.ID == cellTo.ID)
            {
                return new List<Vector2>(2) { from, to };
            }
            forwardQueue.Clear();
            backwardQueue.Clear();
            forwardQueue.Enqueue((cellFrom, 0), Vector2.Distance(fromNav, toNav));
            backwardQueue.Enqueue((cellTo, 0), Vector2.Distance(fromNav, toNav));
            var forwardClosedList = new BitArray(Cells.Length, false);
            var backwardClosedList = new BitArray(Cells.Length, false);
            forwardClosedList.Set(cellFrom.ID, true);
            backwardClosedList.Set(cellTo.ID, true);
            var forwardPaths = _cellPool.Rent(Cells.Length);
            var backwardPaths = _cellPool.Rent(Cells.Length);
            Array.Fill(forwardPaths, null);
            Array.Fill(backwardPaths, null);
            forwardPaths[cellFrom.ID] = null;
            backwardPaths[cellTo.ID] = null;
            try
            {
                while (forwardQueue.Count > 0 && backwardQueue.Count > 0)
                {
                    if (ProcessQueue(forwardQueue, forwardClosedList, backwardClosedList, cellTo, pathfinder, forwardPaths, out var meetingPoint))
                    {
                        return ReconstructPath(forwardPaths, backwardPaths, meetingPoint, from, to, pathfinder).ToList();
                    }
                    if (ProcessQueue(backwardQueue, backwardClosedList, forwardClosedList, cellFrom, pathfinder, backwardPaths, out meetingPoint))
                    {
                        return ReconstructPath(forwardPaths, backwardPaths, meetingPoint, from, to, pathfinder).ToList();
                    }
                }

                return null;
            }
            finally
            {
                _cellPool.Return(forwardPaths);
                _cellPool.Return(backwardPaths);
            }
        }

        private bool ProcessQueue(PriorityQueue<(NavigationGridCell, float), float> queue,
                           BitArray ownClosedList,
                           BitArray otherClosedList,
                           NavigationGridCell targetCell,
                           AttackableUnit pathfinder,
                           NavigationGridCell[] paths,
                           out NavigationGridCell meetingPoint)
        {
            if (queue.TryDequeue(out var element, out _))
            {
                if (element.Item1 is null)
                {
                    meetingPoint = null;
                    return false;
                }
                float currentCost = element.Item2;
                NavigationGridCell cell = element.Item1;
                if (otherClosedList.Get(cell.ID))
                {
                    meetingPoint = cell;
                    return true;
                }
                foreach (var (neighborCell, arrivalCost) in GetCellNeighborsWithCost(cell))
                {
                    if (!ownClosedList.Get(neighborCell.ID) && IsWalkable(neighborCell, pathfinder))
                    {
                        float baseCost = currentCost + arrivalCost + neighborCell.AdditionalCost;
                        float avoidPenalty = GetAvoidancePenalty(neighborCell, pathfinder);
                        float newCost = baseCost + avoidPenalty;

                        // we also put the penalty in the heuristic if you want:
                        float priority = newCost
                                            + Vector2.Distance(neighborCell.GetCenter(), targetCell.GetCenter())
                                            + (avoidPenalty * 0.5f);

                        queue.Enqueue((neighborCell, newCost), priority);
                        ownClosedList.Set(neighborCell.ID, true);
                        paths[neighborCell.ID] = cell;
                    }
                }
            }

            meetingPoint = null;
            return false;
        }
        private const float ObstacleProximityWeight = 8f;
        private float GetAvoidancePenalty(NavigationGridCell cell, AttackableUnit pathfinder)
        {
            const int checkRadius = 1;  // we look at the 8 neighbors around
            int blockedCount = 0;

            for (short dy = -checkRadius; dy <= checkRadius; dy++)
                for (short dx = -checkRadius; dx <= checkRadius; dx++)
                {
                    if (dx == 0 && dy == 0) continue;
                    var neighbour = GetCell((short)(cell.Locator.X + dx), (short)(cell.Locator.Y + dy));
                    if (!IsWalkable(neighbour, pathfinder))
                        blockedCount++;
                }

            return blockedCount * ObstacleProximityWeight;
        }

        // Improved version with better collision handling
        private Vector2[] ReconstructPath(
            NavigationGridCell[] forwardPaths,
            NavigationGridCell[] backwardPaths,
            NavigationGridCell meetingPoint,
            Vector2 from,
            Vector2 to,
            AttackableUnit pathfinder)
        {
            const int InitialCapacity = 512;
            var pool = ArrayPool<NavigationGridCell>.Shared;
            var pathStack = pool.Rent(InitialCapacity);
            int pathCount = 0;

            void AddCell(ref NavigationGridCell cell)
            {
                if (pathCount >= pathStack.Length)
                {
                    var newArray = pool.Rent(pathStack.Length * 2);
                    Array.Copy(pathStack, newArray, pathCount);
                    pool.Return(pathStack);
                    pathStack = newArray;
                }
                pathStack[pathCount++] = cell;
            }

            // Forward path construction
            var current = meetingPoint;
            while (current != null)
            {
                AddCell(ref current);
                current = forwardPaths[current.ID];
                //HACK: prevent infinite loop
                if (pathCount >= 0xFFFF)
                {
                    _logger.WarnFormat("Path exceeded designed limit({0})! Forcefully breaking out of the loop!", 0xFFFF);
                    break;
                }
            }

            Array.Reverse(pathStack, 0, pathCount);

            // Backward path construction
            current = backwardPaths[meetingPoint.ID];
            while (current != null)
            {
                AddCell(ref current);
                current = backwardPaths[current.ID];
                //HACK: prevent infinite loop
                if (pathCount >= 0xFFFF)
                {
                    _logger.WarnFormat("Path exceeded designed limit({0})! Forcefully breaking out of the loop!", 0xFFFF);
                    break;
                }
            }

            // Convert to world positions
            var result = new Vector2[pathCount + 2];
            result[0] = from;
            for (int i = 0; i < pathCount; i++)
                result[i + 1] = TranslateFromNavGrid(pathStack[i].Locator);
            result[^1] = to;

            pool.Return(pathStack);

            return PathTrimmed(result, pathfinder);


        }


        private Vector2[] PathTrimmed(Vector2[] path, AttackableUnit pathfinder)
        {
            if (path == null || path.Length < 3)
                return path;

            var trimmed = new List<Vector2> { path[0] };
            int current = 0;

            while (current < path.Length - 1)
            {
                // We look for the furthest point we can reach in a straight line
                int furthest = current + 1;
                for (int i = path.Length - 1; i > current; i--)
                {
                    if (HasLineOfSight(path[current], path[i], pathfinder))
                    {
                        furthest = i;
                        break;
                    }
                }

                trimmed.Add(path[furthest]);
                current = furthest;
            }

            return trimmed.ToArray();
        }

        /// <summary>
        /// Checks that the straight line between start and end doesn't cross any obstacles.
        /// We sample the segment every 'step' steps (here half-cell) and validate
        /// each position via GetCell()+IsWalkable.
        /// </summary>
        private bool HasLineOfSight(Vector2 start, Vector2 end, AttackableUnit pathfinder)
        {
            // Direction and distance in world
            Vector2 delta = end - start;
            float distance = delta.Length();
            if (distance < float.Epsilon)
                return true;

            Vector2 dir = delta / distance;
            // We sample every 0.5 * CellSize
            float step = CellSize * 0.5f;
            int samples = (int)Math.Ceiling(distance / step);

            for (int i = 1; i <= samples; i++)
            {
                Vector2 point = start + (dir * (i * step));
                // Get the corresponding navgrid cell
                var cell = GetCell(point, true);
                if (!IsWalkable(cell, pathfinder))
                    return false;
            }

            return true;
        }

        private Vector2 GetCellCenter(int x, int y)
        {
            return new Vector2(
                (x * CellSize) + MinGridPosition.X + (CellSize / 2),
                (y * CellSize) + MinGridPosition.Y + (CellSize / 2)
            );
        }






        /// <summary>
        /// Translates the given Vector2 into cell format where each unit is a cell.
        /// This is to simplify the calculations required to get cells.
        /// </summary>
        /// <param name="vector">Vector2 to translate.</param>
        /// <returns>Cell formatted Vector2.</returns>
        public Vector2 TranslateToNavGrid(Vector2 vector)
        {
            return new Vector2
            (
                (vector.X - MinGridPosition.X) / CellSize,
                (vector.Y - MinGridPosition.Z) / CellSize
            );
        }

        /// <summary>
        /// Translates the given cell locator position back into normal coordinate space as a Vector2.
        /// *NOTE*: Returns the coordinates of the center of the cell.
        /// </summary>
        /// <param name="locator">Cell locator.</param>
        /// <returns>Normal coordinate space Vector2.</returns>
        public Vector2 TranslateFromNavGrid(NavigationGridLocator locator)
        {
            return TranslateFromNavGrid(new Vector2(locator.X, locator.Y)) + (Vector2.One * 0.5f * CellSize);
        }

        /// <summary>
        /// Translates the given cell formatted Vector2 back into normal coordinate space.
        /// </summary>
        /// <param name="vector">Vector2 to translate.</param>
        /// <returns>Normal coordinate space Vector2.</returns>
        public Vector2 TranslateFromNavGrid(Vector2 vector)
        {
            return new Vector2
            (
                (vector.X * CellSize) + MinGridPosition.X,
                (vector.Y * CellSize) + MinGridPosition.Z
            );
        }

        public NavigationGridCell GetCell(Vector2 coords, bool translate = true)
        {
            if (translate)
            {
                coords = TranslateToNavGrid(coords);
            }
            return GetCell((short)coords.X, (short)coords.Y);
        }

        /// <summary>
        /// Gets the cell at the given cell based coordinates.
        /// </summary>
        /// <param name="x">cell based X coordinate</param>
        /// <param name="y">cell based Y coordinate.</param>
        /// <returns>Cell instance.</returns>
        public NavigationGridCell GetCell(short x, short y)
        {
            long index = (y * CellCountX) + x;
            if (x < 0 || x > CellCountX || y < 0 || y > CellCountY || index >= Cells.Length)
            {
                return null;
            }
            return Cells[index];
        }




        /// <summary>
        /// Gets a list of all cells within 8 cardinal directions of the given cell.
        /// </summary>
        /// <param name="cell">Cell to start the check at.</param>
        /// <returns>List of neighboring cells.</returns>
        private IEnumerable<(NavigationGridCell, float)> GetCellNeighborsWithCost(NavigationGridCell cell)
        {
            for (short dirY = -1; dirY <= 1; dirY++)
            {
                for (short dirX = -1; dirX <= 1; dirX++)
                {
                    short nx = (short)(cell.Locator.X + dirX);
                    short ny = (short)(cell.Locator.Y + dirY);
                    NavigationGridCell neighborCell = GetCell(nx, ny);
                    if (neighborCell != null)
                    {
                        float cost = (dirX != 0 && dirY != 0) ? 1.4f : 1f;
                        yield return (neighborCell, cost);
                    }
                }
            }
        }



        /// <summary>
        /// Gets a list of cells within the specified range of a specified point.
        /// </summary>
        /// <param name="origin">Vector2 with normal coordinates to start the check.</param>
        /// <param name="radius">Range to check around the origin.</param>
        /// <returns>List of all cells in range. Null if range extends outside of NavigationGrid boundaries.</returns>
        public IEnumerable<NavigationGridCell> GetAllCellsInRange(Vector2 origin, float radius, bool translate = true)
        {
            radius = (float)Math.Ceiling(radius / CellSize);

            if (translate)
            {
                origin = TranslateToNavGrid(origin);
            }

            short fx = (short)(origin.X - radius);
            short lx = (short)(origin.X + radius);
            short fy = (short)(origin.Y - radius);
            short ly = (short)(origin.Y + radius);

            for (short x = fx; x <= lx; x++)
            {
                for (short y = fy; y <= ly; y++)
                {
                    float distSquared = Extensions.DistanceSquaredToRectangle(
                        new Vector2(x + 0.5f, y + 0.5f), 1f, 1f, origin
                    );
                    if (distSquared <= radius * radius)
                    {
                        var cell = GetCell(x, y);
                        if (cell != null)
                        {
                            yield return cell;
                        }
                    }
                }
            }
        }

        bool IsWalkable(NavigationGridCell? cell)
        {
            return cell != null
                && !cell.HasFlag(NavigationGridCellFlags.NOT_PASSABLE)
                && !cell.HasFlag(NavigationGridCellFlags.SEE_THROUGH);
        }
        bool IsWalkable(NavigationGridCell? cell, AttackableUnit pathfinder)
        {

            if ((pathfinder.Status & StatusFlags.Ghosted) != 0)
            {
                return IsWalkable(cell);
            }
            else
            {
                return cell != null
                 && !cell.HasFlag(NavigationGridCellFlags.NOT_PASSABLE)
                 && !cell.HasFlag(NavigationGridCellFlags.SEE_THROUGH)
                 && (cell.IsOpen || cell.BlockingActor == pathfinder);
            }

        }

        bool IsWalkable(NavigationGridCell cell, float checkRadius)
        {
            return IsWalkable(cell.GetCenter(), checkRadius, false);
        }

        /// <summary>
        /// Whether or not the cell at the given position can be pathed on.
        /// </summary>
        /// <param name="coords">Vector2 position to check.</param>
        /// <param name="checkRadius">Radius around the given point to check for walkability.</param>
        /// <param name="translate">Whether or not to translate the given position to cell-based format.</param>
        /// <returns>True/False.</returns>
        public bool IsWalkable(Vector2 coords, float checkRadius = 0, bool translate = true)
        {
            if (checkRadius == 0)
            {
                return IsWalkable(GetCell(coords, translate));
            }

            var cells = GetAllCellsInRange(coords, checkRadius, translate);
            foreach (NavigationGridCell c in cells)
            {
                if (!IsWalkable(c))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Whether or not the given position is see-through. In other words, if it does not block vision.
        /// </summary>
        /// <param name="coords">Vector2 position to check.</param>
        /// <param name="translate">Whether or not to translate the given position to cell-based format.</param>
        /// <returns>True/False.</returns>
        public bool IsVisible(Vector2 coords, bool translate = true)
        {
            return IsVisible(GetCell(coords, translate)); //TODO: implement bush logic here
        }

        static bool IsVisible(NavigationGridCell? cell)
        {
            return cell != null
                && (!cell.HasFlag(NavigationGridCellFlags.NOT_PASSABLE)
                || cell.HasFlag(NavigationGridCellFlags.SEE_THROUGH)
                || cell.HasFlag(NavigationGridCellFlags.HAS_GLOBAL_VISION));
        }

        public bool IsBush(Vector2 coords, bool translate = true)
        {
            return GetCell(coords, translate)?.HasFlag(NavigationGridCellFlags.HAS_GRASS) ?? false;
        }

        /// <summary>
        /// Whether or not the given position has the specified flags.
        /// </summary>
        /// <param name="coords">Vector2 position to check.</param>
        /// <param name="translate">Whether or not to translate the given position to cell-based format.</param>
        /// <returns>True/False.</returns>
        public bool HasFlag(Vector2 coords, NavigationGridCellFlags flag, bool translate = true)
        {
            return GetCell(coords, translate)?.HasFlag(flag) ?? false;
        }

        /// <summary>
        /// Gets the height of the ground at the given position. Used purely for packets.
        /// </summary>
        /// <param name="location">Vector2 position to check.</param>
        /// <returns>Height (3D Y coordinate) at the given position.</returns>
        public float GetHeightAtLocation(Vector2 location)
        {
            // Uses SampledHeights to get the height of a given location on the Navigation Grid
            // This is the method the game uses to get height data

            if (location.X >= MinGridPosition.X && location.Y >= MinGridPosition.Z &&
                location.X <= MaxGridPosition.X && location.Y <= MaxGridPosition.Z)
            {
                float reguestedHeightX = (location.X - MinGridPosition.X) / SampledHeightsDistance.X;
                float requestedHeightY = (location.Y - MinGridPosition.Z) / SampledHeightsDistance.Y;

                int sampledHeight1IndexX = (int)reguestedHeightX;
                int sampledHeight1IndexY = (int)requestedHeightY;
                int sampledHeight2IndexX;
                int sampledHeight2IndexY;

                float v13;
                float v15;

                if (reguestedHeightX >= SampledHeightsCountX - 1)
                {
                    v13 = 1.0f;
                    sampledHeight2IndexX = sampledHeight1IndexX--;
                }
                else
                {
                    v13 = 0.0f;
                    sampledHeight2IndexX = sampledHeight1IndexX + 1;
                }
                if (requestedHeightY >= SampledHeightsCountY - 1)
                {
                    v15 = 1.0f;
                    sampledHeight2IndexY = sampledHeight1IndexY--;
                }
                else
                {
                    v15 = 0.0f;
                    sampledHeight2IndexY = sampledHeight1IndexY + 1;
                }

                uint sampledHeightsCount = SampledHeightsCountX * SampledHeightsCountY;
                int v1 = (int)SampledHeightsCountX * sampledHeight1IndexY;
                int x0y0 = v1 + sampledHeight1IndexX;

                if (v1 + sampledHeight1IndexX < sampledHeightsCount)
                {
                    int v19 = sampledHeight2IndexX + v1;
                    if (v19 < sampledHeightsCount)
                    {
                        int v20 = sampledHeight2IndexY * (int)SampledHeightsCountX;
                        int v21 = v20 + sampledHeight1IndexX;

                        if (v21 < sampledHeightsCount)
                        {
                            int v22 = sampledHeight2IndexX + v20;
                            if (v22 < sampledHeightsCount)
                            {
                                float height = ((1.0f - v13) * SampledHeights[x0y0])
                                          + (v13 * SampledHeights[v19])
                                          + (((SampledHeights[v21] * (1.0f - v13))
                                          + (SampledHeights[v22] * v13)) * v15);

                                return (1.0f - v15) * height;
                            }
                        }
                    }
                }

            }

            return 0.0f;
        }

        /// <summary>
        /// Casts a ray and returns false when failed, with a stopping position, or true on success with the given destination.
        /// </summary>
        /// <param name="origin">Vector position to start the ray cast from.</param>
        /// <param name="destination">Vector2 position to end the ray cast at.</param>
        /// <param name="checkWalkable">Whether or not the ray stops when hitting a position which blocks pathing.</param>
        /// <param name="checkVisible">Whether or not the ray stops when hitting a position which blocks vision.</param>
        /// <returns>True = Reached destination. True = Failed.</returns>
        public bool CastRay(Vector2 origin, Vector2 destination, bool checkWalkable = false, bool checkVisible = false, bool translate = true)
        {
            // Out of bounds
            if (origin.X < MinGridPosition.X || origin.X >= MaxGridPosition.X || origin.Y < MinGridPosition.Z || origin.Y >= MaxGridPosition.Z)
            {
                return true;
            }

            if (translate)
            {
                origin = TranslateToNavGrid(origin);
                destination = TranslateToNavGrid(destination);
            }

            var cells = GetAllCellsInLineInt(origin, destination).GetEnumerator();

            bool prevPosHadBush = HasFlag(origin, NavigationGridCellFlags.HAS_GRASS, false);
            bool destinationHasGrass = HasFlag(destination, NavigationGridCellFlags.HAS_GRASS, false);

            bool hasNext;
            while (hasNext = cells.MoveNext())
            {
                var cell = cells.Current;

                //TODO: Implement methods for maps whose NavGrids don't use SEE_THROUGH flags for buildings
                if (checkWalkable)
                {
                    if (!IsWalkable(cell))
                    {
                        break;
                    }
                }

                if (checkVisible)
                {
                    if (!IsVisible(cell))
                    {
                        break;
                    }

                    bool isGrass = cell.HasFlag(NavigationGridCellFlags.HAS_GRASS);

                    // If you are outside of a bush
                    if (!prevPosHadBush && isGrass)
                    {
                        break;
                    }

                    // If you are in a different bush
                    if (prevPosHadBush && destinationHasGrass && !isGrass)
                    {
                        break;
                    }
                }
            }

            return hasNext;
        }
        public bool HasClearLineOfSightlow(Vector2 start, Vector2 end, AttackableUnit pathfinder)
        {
            // Bresenham's line algorithm to check for obstacles
            var x0 = (int)start.X;
            var y0 = (int)start.Y;
            var x1 = (int)end.X;
            var y1 = (int)end.Y;

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            var sx = x0 < x1 ? 1 : -1;
            var sy = y0 < y1 ? 1 : -1;
            var err = dx - dy;

            while (true)
            {
                var cell = GetCell(new Vector2(x0, y0));
                if (!IsWalkable(cell, pathfinder))
                    return false;

                if (x0 == x1 && y0 == y1)
                    break;

                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }

            return true;
        }

        public bool HasClearLineOfSight(Vector2 start, Vector2 end, AttackableUnit pathfinder)
        {
            var x0 = (int)start.X;
            var y0 = (int)start.Y;
            var x1 = (int)end.X;
            var y1 = (int)end.Y;

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            var sx = x0 < x1 ? 1 : -1;
            var sy = y0 < y1 ? 1 : -1;
            var err = dx - dy;

            // Unit radius to consider
            var radius = (int)Math.Ceiling(pathfinder.CollisionRadius / CellSize); // We assume Radius is defined on the unit

            while (true)
            {
                // Check all cells around (x0, y0) based on the radius
                for (int x = x0 - radius; x <= x0 + radius; x++)
                {
                    for (int y = y0 - radius; y <= y0 + radius; y++)
                    {
                        var cell = GetCell(new Vector2(x, y));
                        if (!IsWalkable(cell, pathfinder))
                            return false;
                    }
                }

                if (x0 == x1 && y0 == y1)
                    break;

                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }

                // If we're moving diagonally, check adjacent cells
                if (sx != 0 && sy != 0)
                {
                    var cell1 = GetCell(new Vector2(x0 - sx, y0)); // adjacent horizontal cell
                    var cell2 = GetCell(new Vector2(x0, y0 - sy)); // adjacent vertical cell

                    // If one of the two cells is not walkable, we block the movement
                    if (!IsWalkable(cell1, pathfinder) || !IsWalkable(cell2, pathfinder))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private IEnumerable<NavigationGridCell> GetAllCellsInLineInt(Vector2 v0, Vector2 v1)
        {
            int x1 = (int)v0.X, y1 = (int)v0.Y;
            int x2 = (int)v1.X, y2 = (int)v1.Y;
            int dx, dy;
            int incx, incy;
            int balance;

            if (x2 >= x1)
            {
                dx = x2 - x1;
                incx = 1;
            }
            else
            {
                dx = x1 - x2;
                incx = -1;
            }

            if (y2 >= y1)
            {
                dy = y2 - y1;
                incy = 1;
            }
            else
            {
                dy = y1 - y2;
                incy = -1;
            }

            int x = x1;
            int y = y1;

            if (dx >= dy)
            {
                dy <<= 1;
                balance = dy - dx;
                dx <<= 1;

                while (x != x2)
                {
                    yield return GetCell((short)x, (short)y);
                    if (balance >= 0)
                    {
                        y += incy;
                        balance -= dx;
                    }
                    balance += dy;
                    x += incx;
                }
                yield return GetCell((short)x, (short)y);
            }
            else
            {
                dx <<= 1;
                balance = dx - dy;
                dy <<= 1;

                while (y != y2)
                {
                    yield return GetCell((short)x, (short)y);
                    if (balance >= 0)
                    {
                        x += incx;
                        balance -= dy;
                    }
                    balance += dx;
                    y += incy;
                }
                yield return GetCell((short)x, (short)y);
            }
        }

        // https://playtechs.blogspot.com/2007/03/raytracing-on-grid.html
        private IEnumerable<NavigationGridCell> GetAllCellsInLine(Vector2 v0, Vector2 v1)
        {
            double dx = Math.Abs(v1.X - v0.X);
            double dy = Math.Abs(v1.Y - v0.Y);

            short x = (short)Math.Floor(v0.X);
            short y = (short)Math.Floor(v0.Y);

            int n = 1;
            short x_inc, y_inc;
            double error;

            if (dx == 0)
            {
                x_inc = 0;
                error = float.PositiveInfinity;
            }
            else if (v1.X > v0.X)
            {
                x_inc = 1;
                n += (int)Math.Floor(v1.X) - x;
                error = (Math.Floor(v0.X) + 1 - v0.X) * dy;
            }
            else
            {
                x_inc = -1;
                n += x - (int)Math.Floor(v1.X);
                error = (v0.X - Math.Floor(v0.X)) * dy;
            }

            if (dy == 0)
            {
                y_inc = 0;
                error = float.NegativeInfinity;
            }
            else if (v1.Y > v0.Y)
            {
                y_inc = 1;
                n += (int)Math.Floor(v1.Y) - y;
                error -= (Math.Floor(v0.Y) + 1 - v0.Y) * dx;
            }
            else
            {
                y_inc = -1;
                n += y - (int)Math.Floor(v1.Y);
                error -= (v0.Y - Math.Floor(v0.Y)) * dx;
            }

            for (; n > 0; --n)
            {
                yield return GetCell(x, y);

                if (error > 0)
                {
                    y += y_inc;
                    error -= dx;
                }
                else if (error < 0)
                {
                    x += x_inc;
                    error += dy;
                }
                else //if (error == 0)
                {
                    yield return GetCell((short)(x + x_inc), y);
                    yield return GetCell(x, (short)(y + y_inc));

                    x += x_inc;
                    y += y_inc;
                    error += dy - dx;
                    n--;
                }
            }
        }

        public async Task<bool> CastCircleAsync(Vector2 orig, Vector2 dest, float radius, bool translate = true)
        {
            if (translate)
            {
                orig = TranslateToNavGrid(orig);
                dest = TranslateToNavGrid(dest);
            }
            //here
            float tradius = (float)Math.Ceiling(radius / CellSize);
            // float tradius = radius / CellSize;
            Vector2 p = (dest - orig).Normalized().Perpendicular() * tradius;

            var cells = GetAllCellsInRange(orig, radius, false)
            .Concat(GetAllCellsInRange(dest, radius, false))
            .Concat(GetAllCellsInLine(orig + p, dest + p))
            .Concat(GetAllCellsInLine(orig - p, dest - p));

            int minY = (int)(Math.Min(orig.Y, dest.Y) - tradius) - 1;
            int maxY = (int)(Math.Max(orig.Y, dest.Y) + tradius) + 1;

            int countY = maxY - minY + 1;
            var xRanges = new short[countY, 3];
            foreach (var cell in cells)
            {
                if (!IsWalkable(cell))
                {
                    return true;
                }
                int y = cell.Locator.Y - minY;
                if (xRanges[y, 2] == 0)
                {
                    xRanges[y, 0] = cell.Locator.X;
                    xRanges[y, 1] = cell.Locator.X;
                    xRanges[y, 2] = 1;
                }
                else
                {
                    xRanges[y, 0] = Math.Min(xRanges[y, 0], cell.Locator.X);
                    xRanges[y, 1] = Math.Max(xRanges[y, 1], cell.Locator.X);
                }
            }

            for (int y = 0; y < countY; y++)
            {
                for (int x = xRanges[y, 0] + 1; x < xRanges[y, 1]; x++)
                {
                    if (!IsWalkable(GetCell((short)x, (short)(minY + y))))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Casts a ray in the given direction and returns false when failed, with a stopping position, or true on success with the given destination.
        /// *NOTE*: Is not actually infinite, just travels (direction * 1024) units ahead of the given origin.
        /// </summary>
        /// <param name="origin">Vector position to start the ray cast from.</param>
        /// <param name="direction">Ray cast direction.</param>
        /// <param name="checkWalkable">Whether or not the ray stops when hitting a position which blocks pathing.</param>
        /// <param name="checkVisible">Whether or not the ray stops when hitting a position which blocks vision. *NOTE*: Does not apply if checkWalkable is also true.</param>
        /// <returns>False = Reached destination. True = Failed.</returns>
        public bool CastInfiniteRay(Vector2 origin, Vector2 direction, bool checkWalkable = true, bool checkVisible = false)
        {
            return CastRay(origin, origin + (direction * 1024), checkWalkable, checkVisible);
        }

        /// <summary>
        /// Whether or not there is anything blocking the two given GameObjects from either seeing eachother or pathing straight towards eachother (depending on checkVision).
        /// </summary>
        /// <param name="a">GameObject to start the check from.</param>
        /// <param name="b">GameObject to end the check at.</param>
        /// <param name="checkVision">True = Check for positions that block vision. False = Check for positions that block pathing.</param>
        /// <returns>True/False.</returns>
        public bool IsAnythingBetween(GameObject a, GameObject b, bool checkVision = false)
        {


            if (!CastRay(a.Position, b.Position, !checkVision, checkVision))
            {
                return false;
            }


            var cellInRadius = GetAllCellsInRange(a.Position, 25);


            foreach (var cell in cellInRadius)
            {

                if (cell.HasFlag(NavigationGridCellFlags.HAS_GRASS))
                {

                    if (!CastRay(TranslateFromNavGrid(cell.GetCenter()), b.Position, !checkVision, checkVision))
                    {
                        return false;
                    }
                }
            }


            return true;
        }

        /// <summary>
        /// Gets the closest pathable position to the given position. *NOTE*: Computationally heavy, use sparingly.
        /// </summary>
        /// <param name="location">Vector2 position to start the check at.</param>
        /// <param name="distanceThreshold">Amount of distance away from terrain the exit should be.</param>
        /// <returns>Vector2 position which can be pathed on.</returns>
        public Vector2 GetClosestTerrainExit(Vector2 location, float distanceThreshold = 0)
        {
            double angle = Math.PI / 4;

            // x = r * cos(angle)
            // y = r * sin(angle)
            // r = distance from center
            // Draws spirals until it finds a walkable spot
            for (int r = 1; !IsWalkable(location, distanceThreshold); r++)
            {
                location.X += r * (float)Math.Cos(angle);
                location.Y += r * (float)Math.Sin(angle);
                angle += Math.PI / 4;
            }

            return location;
        }

        /// <summary>
        /// test for find clos
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="distanceThreshold"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        public Vector2 GetClosestWalkableToInFunctionOfFrom(Vector2 from, Vector2 to, float distanceThreshold = 0, float maxDistance = float.MaxValue)
        {
            double angle = 0;
            float stepSize = 1f;
            float radius = 0;

            Vector2 currentPoint = to;

            while (!IsWalkable(currentPoint, distanceThreshold))
            {
                currentPoint.X = to.X + (radius * (float)Math.Cos(angle));
                currentPoint.Y = to.Y + (radius * (float)Math.Sin(angle));

                // Check if the distance between 'from' and 'currentPoint' exceeds 'maxDistance'
                if (Vector2.Distance(from, currentPoint) > maxDistance)
                {
                    break; // Stop the search if the maximum distance is exceeded
                }

                if (IsWalkable(currentPoint, distanceThreshold))
                {
                    return currentPoint;
                }

                angle += Math.PI / 4;
                radius += stepSize;
            }

            return to; // If no point is found, return 'to' by default
        }


        public NavigationGridCell GetClosestWalkableCell(Vector2 coords, float distanceThreshold = 0, bool translate = true)
        {
            if (translate)
            {
                coords = TranslateToNavGrid(coords);
            }
            float closestDist = 0;
            NavigationGridCell closestCell = null!;
            foreach (var cell in Cells)
            {
                if (IsWalkable(cell, distanceThreshold))
                {
                    float dist = Vector2.DistanceSquared(cell.GetCenter(), coords);
                    if (closestCell == null || dist < closestDist)
                    {
                        closestCell = cell;
                        closestDist = dist;
                    }
                }
            }
            return closestCell;
        }

        private static float CalculateOccupiedSize(AttackableUnit unit, NavigationGridCell cell)
        {
            var objPosition = unit.Position;
            var unitRadius = unit.PathfindingRadius;
            var cellPosition = Game.Map.NavigationGrid.TranslateFromNavGrid(cell.Locator);
            var cellRadius = Game.Map.NavigationGrid.CellSize / 2;

            var distance = Vector2.Distance(objPosition, cellPosition);

            // If the distance is greater than the sum of the radii, the circles do not intersect
            if (distance >= unitRadius + cellRadius)
            {
                return 0f;
            }

            // If one circle is completely inside the other
            if (distance <= Math.Abs(unitRadius - cellRadius))
            {
                // Return the area of the smaller circle
                float smallerRadius = Math.Min(unitRadius, cellRadius);
                return MathF.PI * smallerRadius * smallerRadius;
            }

            // Calculate the intersection area using the formula for the intersection of two circles
            var objRadiusSquared = unitRadius * unitRadius;
            var cellRadiusSquared = cellRadius * cellRadius;
            var distanceSquared = distance * distance;

            var angle1 = 2 * MathF.Acos((objRadiusSquared + distanceSquared - cellRadiusSquared) / (2 * unitRadius * distance));
            var angle2 = 2 * MathF.Acos((cellRadiusSquared + distanceSquared - objRadiusSquared) / (2 * cellRadius * distance));

            var area1 = 0.5f * objRadiusSquared * (angle1 - MathF.Sin(angle1));
            var area2 = 0.5f * cellRadiusSquared * (angle2 - MathF.Sin(angle2));

            return area1 + area2;
        }

        public static List<NavigationGridCell> CloseCellsWithUnit(AttackableUnit attackableUnit)
        {
            var list = new List<NavigationGridCell>();
            var cells = Game.Map.NavigationGrid.GetAllCellsInRange(attackableUnit.Position,
                attackableUnit.PathfindingRadius);

            foreach (var navigationGridCell in cells)
            {
                var size = CalculateOccupiedSize(attackableUnit, navigationGridCell);

                if (size > 200)
                {
                    list.Add(navigationGridCell);
                    navigationGridCell.CloseWithActor(attackableUnit);
                }
            }

            return list;
        }



    }
}