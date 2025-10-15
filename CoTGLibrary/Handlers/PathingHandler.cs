using System.Activities.Presentation.View;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CoTG.CoTGServer.GameObjects.AttackableUnits;

namespace CoTG.CoTGServer.Handlers
{
    /// <summary>
    /// Class which calls path based functions for GameObjects.
    /// </summary>
    internal class PathingHandler
    {
        private MapHandler _map;
        private readonly List<AttackableUnit> _pathfinders = new();
        private float pathUpdateTimer;

        internal PathingHandler(MapHandler map)
        {
            _map = map;
        }

        /// <summary>
        /// Adds the specified GameObject to the list of GameObjects to check for pathfinding. *NOTE*: Will fail to fully add the GameObject if it is out of the map's bounds.
        /// </summary>
        /// <param name="obj">GameObject to add.</param>
        public void AddPathfinder(AttackableUnit obj)
        {
            _pathfinders.Add(obj);
        }

        /// <summary>
        /// GameObject to remove from the list of GameObjects to check for pathfinding.
        /// </summary>
        /// <param name="obj">GameObject to remove.</param>
        /// <returns>true if item is successfully removed; false otherwise.</returns>
        public bool RemovePathfinder(AttackableUnit obj)
        {
            return _pathfinders.Remove(obj);
        }

        /// <summary>
        /// Function called every tick of the game by Map.cs.
        /// </summary>
        public void Update()
        {


            // TODO: Verify if this is the proper time between path updates.
            if (pathUpdateTimer >= 500.0f)
            {
                // we iterate over a copy of _pathfinders because the original gets modified
                var objectsCopy = new List<AttackableUnit>(_pathfinders);
                foreach (var obj in objectsCopy)
                {
                    UpdatePaths(obj);
                }

                pathUpdateTimer = 0;
            }

            pathUpdateTimer += Game.Time.DeltaTime;


        }




        /// <summary>
        /// Updates pathing for the specified object.
        /// </summary>
        /// <param name="obj">GameObject to check for incorrect paths.</param>
        public void UpdatePaths(AttackableUnit obj)
        {
            var path = obj.Waypoints;
            if (path.Count == 0)
            {
                return;
            }

            var lastWaypoint = path[path.Count - 1];
            if (obj.CurrentWaypoint.Equals(lastWaypoint) && lastWaypoint.Equals(obj.Position))
            {
                return;
            }

            var distanceTillWaypoint = Vector2.Distance(obj.Position, obj.CurrentWaypoint);
            // Seems to be around 500-600, tested on live LoL
            if (distanceTillWaypoint < 600)
            {
                var hasObstacle = !Game.Map.NavigationGrid.HasClearLineOfSight(obj.Position, obj.CurrentWaypoint, obj);
                if (hasObstacle)
                {
                    var newPath = Game.Map.NavigationGrid.GetPath(obj.Position, obj.Waypoints[^1], obj);
                    obj.SetWaypoints(newPath);
                }
            }



            //float Actor_Common::s_MinCollisionAvoidanceDist = 500.0; // idb
            //  float Actor_Common::s_MaxCollisionAvoidanceDist = 2000.0; // idb
            /* var ListOfPossibleCollision = Game.ObjectManager.GetUnitsInRange(obj.Position, 2000.0f, true);

             if (ListOfPossibleCollision.Count > 0 && (obj.Status & StatusFlags.Ghosted) != 0)
             {

                 newPath = Game.Map.NavigationGrid.CheckActorCollisionResponse(ListOfPossibleCollision, newPath, ( obj as ObjAIBase ) );

             }
            */
        }

        /// <summary>
        /// Checks if the given position can be pathed on.
        /// </summary>
        public bool IsWalkable(Vector2 pos, float radius = 0, bool checkObjects = false)
        {
            bool walkable = Game.Map.NavigationGrid.IsWalkable(pos, radius);

            if (checkObjects && Game.Map.CollisionHandler.GetNearestObjects(new Circle(pos, radius)).Any())
            {
                walkable = false;
            }

            return walkable;
        }

        /// <summary>
        /// Returns a path to the given target position from the given unit's position.
        /// </summary>
      /*  public List<Vector2> GetPath(AttackableUnit obj, Vector2 target, bool usePathingRadius = true)
        {
            List<Vector2> pathing = default;
            var ListOfPossibleCollision = new List<AttackableUnit>(); 
            //check if need redo that 
            if (usePathingRadius)
            {
                pathing = Game.Map.NavigationGrid.GetPath(obj.Position, target, obj);

               


                return pathing;

            }

            pathing = Game.Map.NavigationGrid.GetPath(obj.Position, target, obj);
            //   pathing = Game.Map.NavigationGrid.CheckActorCollisionResponse(ListOfPossibleCollision, pathing, (obj as ObjAIBase));






            return pathing;
        }
      */
        /// <summary>
        /// Returns a path to the given target position from the given start position.
        /// </summary>
        public List<Vector2> GetPath(Vector2 start, Vector2 target, AttackableUnit unit)
        {

            List<Vector2> pathing = default;

            pathing = Game.Map.NavigationGrid.GetPath(start, target, unit);


            return pathing;

            //used ? 
        }
    }
}
