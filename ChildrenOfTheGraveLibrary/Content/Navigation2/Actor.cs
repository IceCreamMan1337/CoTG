using System;
using System.Collections.Generic;
using System.Numerics;
/* Actor
------------------------------------------------------------------------------------------
    
    An actor represents a game object that is mobile and attached to a NavigationMesh. This implementation
    shows how to navigate an object on a NavigationMesh and request path information from the mesh.
    
------------------------------------------------------------------------------------------
*/
namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.Navigation.aimesh
{
    public class Actor
    {
        // ----- ENUMERATIONS & CONSTANTS -----
        public struct Triangle
        {
            public Vector3[] Vert;        // Vertices of the triangle in clockwise order
                                          // public byte[][] Color;        // 4 color values (RGBA) for each vertex
        }

        public List<Triangle> Geometry { get; private set; } = new List<Triangle>();

        // ----- CREATORS ---------------------
        public Actor()
        {
            PathActive = false;
            CurrentCell = null;
            Movement = new Vector3(0.0f, 0.0f, 0.0f);
            Position = new Vector3(0.0f, 0.0f, 0.0f);
            MaxSpeed = 5.0f;
        }

        // ----- MUTATORS ---------------------
        public void Create(NavigationMesh parent, Vector3 position, NavigationCell currentCell)
        {
            Parent = parent;
            Position = position;
            CurrentCell = currentCell;

            Movement = new Vector3(0.0f, 0.0f, 0.0f);
            Geometry.Clear();

            if (Parent != null)
            {
                // If no starting cell is provided, find one by searching in the mesh
                if (CurrentCell == null)
                {
                    CurrentCell = Parent.FindClosestCell(Position);
                }

                // Make sure our position is inside the current cell
                Position = Parent.SnapPointToCell(CurrentCell, Position);
            }
        }

        public void AddTriangle(Triangle triangle)
        {
            Geometry.Add(triangle);
        }
        /*
        public void Render()
        {
            GL.PushMatrix();
            GL.Translate(Position.X, Position.Y, Position.Z);
            GL.Begin(PrimitiveType.Triangles);

            // Draw each triangle contained in the Geometry list
            foreach (var poly in Geometry)
            {
                for (int i = 0; i < 3; ++i)
                {
                    GL.Color4(poly.Color[i][0], poly.Color[i][1], poly.Color[i][2], poly.Color[i][3]);
                    GL.Vertex3(poly.Vert[i].X, poly.Vert[i].Y, poly.Vert[i].Z);
                }
            }

            GL.End();
            GL.PopMatrix();

            // If the path is active, draw it
            if (PathActive)
            {
                GL.PushMatrix();
                GL.Translate(0.0f, 0.0f, 0.0f);
                GL.Begin(PrimitiveType.Lines);

                // For line drawing, add 0.1 to all Y values to ensure lines are visible above the Navigation Mesh

                var iter = Path.WaypointList().GetEnumerator();
                if (iter.MoveNext())
                {
                    var lastWaypoint = iter.Current;
                    while (iter.MoveNext())
                    {
                        var waypoint = iter.Current;

                        GL.Color4(0x00, 0x00, 0xff, 0xff);
                        GL.Vertex3(lastWaypoint.Position.X, lastWaypoint.Position.Y + 0.1f, lastWaypoint.Position.Z);

                        GL.Color4(0x00, 0x00, 0xff, 0xff);
                        GL.Vertex3(waypoint.Position.X, waypoint.Position.Y + 0.1f, waypoint.Position.Z);

                        lastWaypoint = waypoint;
                    }
                }

                // Draw the current direction in red
                GL.Color4(0xff, 0x00, 0x00, 0xff);
                GL.Vertex3(Position.X, Position.Y + 0.1f, Position.Z);

                var currentWaypoint = NextWaypoint.Current;
                GL.Color4(0xff, 0x00, 0x00, 0xff);
                GL.Vertex3(currentWaypoint.Position.X, currentWaypoint.Position.Y + 0.1f, currentWaypoint.Position.Z);

                GL.End();
                GL.PopMatrix();
            }
        }
        */
        public void Update(float elapsedTime = 1.0f)
        {
            float distance;
            float maxDistance = MaxSpeed * elapsedTime;

            // If we don't have a parent mesh, return. We are simply a static object.
            if (Parent == null) return;

            // Determine our movement
            if (PathActive)
            {
                if (NextWaypoint.MoveNext())
                {
                    Movement = NextWaypoint.Current.Position - Position;
                }
                else
                {
                    PathActive = false;
                    Movement = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }
            else
            {
                // Apply friction to our current movement
                Movement *= 0.75f;
            }

            // Adjust movement
            distance = Movement.Length();
            if (distance > maxDistance)
            {
                Vector3.Normalize(Movement);
                Movement *= maxDistance;
            }

            // Constrain movement within the NavigationMesh
            if (Parent != null && (Movement.X != 0 || Movement.Z != 0))
            {
                var nextPosition = Position + Movement;
                NavigationCell nextCell;
                Parent.ResolveMotionOnMesh(Position, CurrentCell, ref nextPosition, out nextCell);

                Position = nextPosition;
                CurrentCell = nextCell;
            }
            else if (PathActive)
            {
                Position = NextWaypoint.Current.Position;
                Movement = new Vector3(0.0f, 0.0f, 0.0f);
                distance = 0.0f;
                PathActive = NextWaypoint.MoveNext();

                if (!PathActive)
                {
                    Movement = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }
        }

        public void GotoLocation(Vector3 position, NavigationCell cell)
        {
            if (Parent != null)
            {
                Movement = new Vector3(0.0f, 0.0f, 0.0f);
                PathActive = Parent.BuildNavigationPath(Path, CurrentCell, Position, cell, position);

                if (PathActive)
                {
                    NextWaypoint = Path.WaypointList().GetEnumerator();
                }
            }
        }

        public void GotoRandomLocation()
        {
            if (Parent != null)
            {
                var index = new Random().Next(Parent.TotalCells());
                var pCell = Parent.Cell(index);
                GotoLocation(pCell.CenterPoint(), pCell);
            }
        }

        public void AddMovement(Vector3 movement)
        {
            Movement += movement;
        }

        public void SetMovement(Vector3 movement)
        {
            Movement = movement;
        }

        public void SetMovementX(float x)
        {
            Movement = new Vector3(Movement.X + x, Movement.Y, Movement.Z);
        }

        public void SetMovementY(float y)
        {
            Movement = new Vector3(Movement.X, Movement.Y + y, Movement.Z);
        }

        public void SetMovementZ(float z)
        {
            Movement = new Vector3(Movement.X, Movement.Y, Movement.Z + z);
        }

        public void SetMaxSpeed(float speed)
        {
            MaxSpeed = speed;
        }

        // ----- ACCESSORS --------------------
        public bool PathIsActive()
        {
            return PathActive;
        }

        public Vector3 Position { get; private set; }
        public Vector3 Movement { get; private set; }
        public NavigationCell CurrentCell { get; private set; }

        // ----- DATA -------------------------
        private NavigationMesh Parent;
        private float MaxSpeed;
        private bool PathActive;
        private NavigationPath Path;
        private IEnumerator<NavigationPath.Waypoint> NextWaypoint;
    }
}


/*   

*/ 