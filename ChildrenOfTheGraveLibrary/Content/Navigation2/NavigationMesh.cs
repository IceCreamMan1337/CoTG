/* Copyright (C) Greg Snook, 2000. 
 * All rights reserved worldwide.
 *
 * This software is provided "as is" without express or implied
 * warranties. You may freely copy and compile this source into
 * applications you distribute provided that the copyright text
 * below is included in the resulting source code, for example:
 * "Portions Copyright (C) Greg Snook, 2000"
 */

using ChildrenOfTheGraveEnumNetwork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;



namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.Navigation.aimesh
{
    public class NavigationMesh
    {
        private int m_PathSession;
        private NavigationHeap m_NavHeap = new();
        private List<NavigationCell> m_CellArray = new();

        // SnapPointToCell
        // ----------------------------------------------------------------------------------------
        //
        // Force a point to be inside the cell
        //

        public NavigationMesh()
        {
            m_PathSession = 0;
            m_CellArray.Clear();
        }

        ~NavigationMesh()
        {
            Clear();
        }

        // ----- MUTATORS ---------------------
        public void Clear()
        {
            foreach (var cell in m_CellArray)
            {
                cell?.Dispose(); // Assuming Dispose method or similar cleanup
            }

            m_CellArray.Clear();
        }

        public void AddCell(Vector3 pointA, Vector3 pointB, Vector3 pointC)
        {
            NavigationCell newCell = new();
            newCell.Initialize(pointA, pointB, pointC);
            m_CellArray.Add(newCell);
        }



        public void Update(float elapsedTime = 1.0f)
        {
            // Does nothing at this point. Stubbed for future use in animating the mesh.
        }

        // ----- ACCESSORS --------------------
        public int TotalCells()
        {
            return m_CellArray.Count;
        }

        public NavigationCell Cell(int index)
        {
            return m_CellArray[index];
        }

        //.c

        public Vector3 SnapPointToCell(NavigationCell cell, Vector3 point)
        {
            Vector3 pointOut = point;


            if (cell == null)
            {
                Debug.Assert(false, "La cellule est nulle dans SnapPointToCell. Impossible de calculer le point.");
                return point; // Retourne simplement le point d'origine, ou choisis un autre comportement en fonction de ton besoin.
            }

            if (!cell.IsPointInCellColumn(pointOut))
            {
                cell.ForcePointToCellColumn(ref pointOut);
            }

            cell.MapVectorHeightToCell(ref pointOut);
            return pointOut;
        }

        // SnapPointToMesh
        // ----------------------------------------------------------------------------------------
        //
        // Force a point to be inside the nearest cell on the mesh
        //
        public Vector3 SnapPointToMesh(out NavigationCell cellOut, Vector3 point)
        {
            Vector3 pointOut = point;
            cellOut = FindClosestCell(pointOut);
            return SnapPointToCell(cellOut, pointOut);
        }

        // FindClosestCell
        // ----------------------------------------------------------------------------------------
        //
        // Find the closest cell on the mesh to the given point
        //
        public NavigationCell FindClosestCell(Vector3 point)
        {
            float closestDistance = float.MaxValue;
            float closestHeight = float.MaxValue;
            bool foundHomeCell = false;
            float thisDistance;
            NavigationCell closestCell = null;

            foreach (var pCell in m_CellArray)
            {
                if (pCell.IsPointInCellColumn(point))
                {
                    Vector3 newPosition = point;
                    pCell.MapVectorHeightToCell(ref newPosition);

                    thisDistance = Math.Abs(newPosition.Y - point.Y);

                    if (foundHomeCell)
                        if (foundHomeCell)
                        {
                            if (thisDistance < closestHeight)
                            {
                                closestCell = pCell;
                                closestHeight = thisDistance;
                            }
                        }
                        else
                        {
                            closestCell = pCell;
                            closestHeight = thisDistance;
                            foundHomeCell = true;
                        }
                }

                if (!foundHomeCell)
                {
                    Vector2 start = new(pCell.CenterPoint().X, pCell.CenterPoint().X);
                    Vector2 end = new(point.X, point.Z);
                    Line2D motionPath = new(start, end);
                    NavigationCell nextCell;
                    NavigationCell.CELL_SIDE wallHit;
                    Vector2 pointOfIntersection = Vector2.Zero;

                    var result = pCell.ClassifyPathToCell(motionPath, out nextCell, out wallHit, pointOfIntersection);

                    if (result.HasFlag(NavigationCell.PATH_RESULT.EXITING_CELL))
                    {
                        Vector3 closestPoint3D = new(pointOfIntersection.X, 0.0f, pointOfIntersection.Y);
                        pCell.MapVectorHeightToCell(ref closestPoint3D);

                        closestPoint3D -= point;

                        thisDistance = closestPoint3D.Length();

                        if (thisDistance < closestDistance)
                        {
                            closestDistance = thisDistance;
                            closestCell = pCell;
                        }
                    }
                }
            }

            return closestCell;
        }

        // BuildNavigationPath
        // ----------------------------------------------------------------------------------------
        //
        // Build a navigation path using the provided points and the A* method
        //
        public bool BuildNavigationPath(NavigationPath navPath, NavigationCell startCell, Vector3 startPos, NavigationCell endCell, Vector3 endPos)
        {
            bool foundPath = false;

            // Increment our pathfinding session ID
            m_PathSession++;

            // Prepare NavigationHeap
            m_NavHeap.Setup(m_PathSession, startPos);

            // We are doing a reverse search, from EndCell to StartCell.
            endCell.QueryForPath(m_NavHeap, null, 0);

            // Process the heap until empty, or a path is found
            while (m_NavHeap.NotEmpty() && !foundPath)
            {
                NavigationNode thisNode;
                m_NavHeap.GetTop(out thisNode);

                if (thisNode.Cell == startCell)
                {
                    foundPath = true;
                }
                else
                {
                    thisNode.Cell.ProcessCell(m_NavHeap);
                }
            }

            // If a path was found, build waypoints
            if (foundPath)
            {
                NavigationCell testCell = startCell;
                Vector3 newWayPoint;

                navPath.Setup(this, startPos, startCell, endPos, endCell);

                while (testCell != null && testCell != endCell)
                {
                    int linkWall = testCell.ArrivalWall();

                    newWayPoint = testCell.WallMidpoint(linkWall);
                    newWayPoint = SnapPointToCell(testCell, newWayPoint);

                    navPath.AddWayPoint(newWayPoint, testCell);

                    testCell = testCell.Link(linkWall);
                }

                navPath.EndPath();
                return true;
            }
            return false;
        }
        public List<Vector2> BuildNavigationPath2(NavigationPath navPath, NavigationCell startCell, Vector3 startPos, NavigationCell endCell, Vector3 endPos)
        {
            bool foundPath = false;

            // Increment our pathfinding session ID
            m_PathSession++;

            // Prepare NavigationHeap
            m_NavHeap.Setup(m_PathSession, startPos);

            // We are doing a reverse search, from EndCell to StartCell.
            endCell.QueryForPath(m_NavHeap, null, 0);

            // Process the heap until empty, or a path is found
            while (m_NavHeap.NotEmpty() && !foundPath)
            {
                NavigationNode thisNode;
                m_NavHeap.GetTop(out thisNode);

                if (thisNode.Cell == startCell)
                {
                    foundPath = true;
                }
                else
                {
                    thisNode.Cell.ProcessCell(m_NavHeap);
                }
            }

            // If a path was found, build waypoints
            if (foundPath)
            {
                NavigationCell testCell = startCell;
                Vector3 newWayPoint;

                navPath.Setup(this, startPos, startCell, endPos, endCell);

                while (testCell != null && testCell != endCell)
                {
                    int linkWall = testCell.ArrivalWall();

                    newWayPoint = testCell.WallMidpoint(linkWall);
                    newWayPoint = SnapPointToCell(testCell, newWayPoint);

                    navPath.AddWayPoint(newWayPoint, testCell);

                    testCell = testCell.Link(linkWall);
                }

                navPath.EndPath();
                var navpathvector2 = new List<Vector2>();
                foreach (var nav in navPath.m_WaypointList)
                {
                    navpathvector2.Add(nav.Position.ToVector2());
                }
                return navpathvector2;  // Return the waypoint list if path is found
            }

            // Return null if no path is found
            return null;
        }

        // ResolveMotionOnMesh
        // ----------------------------------------------------------------------------------------
        //
        // Resolve a movement vector on the mesh
        //
        public void ResolveMotionOnMesh(Vector3 startPos, NavigationCell startCell, ref Vector3 endPos, out NavigationCell endCell)
        {
            Line2D motionPath = new(new Vector2(startPos.X, startPos.Z), new Vector2(endPos.X, endPos.Z));

            NavigationCell.PATH_RESULT result = NavigationCell.PATH_RESULT.NO_RELATIONSHIP;
            NavigationCell.CELL_SIDE wallNumber;
            Vector2 pointOfIntersection = Vector2.Zero;
            NavigationCell nextCell;

            NavigationCell testCell = startCell;

            while (!result.HasFlag(NavigationCell.PATH_RESULT.ENDING_CELL) && motionPath.EndPointA != motionPath.EndPointB)
            {
                result = testCell.ClassifyPathToCell(motionPath, out nextCell, out wallNumber, pointOfIntersection);

                if (result == NavigationCell.PATH_RESULT.EXITING_CELL)
                {
                    if (nextCell != null)
                    {
                        motionPath.SetEndPointA(pointOfIntersection);
                        testCell = nextCell;
                    }
                    else
                    {
                        motionPath.SetEndPointA(pointOfIntersection);
                        testCell.ProjectPathOnCellWall(wallNumber, ref motionPath);

                        Vector2 direction = motionPath.EndPointB - motionPath.EndPointA;
                        direction *= 0.9f;
                        motionPath.SetEndPointB(motionPath.EndPointA + direction);
                    }
                }
                else if (result == NavigationCell.PATH_RESULT.NO_RELATIONSHIP)
                {
                    Vector2 newOrigin = motionPath.EndPointA;
                    testCell.ForcePointToCellColumn(ref newOrigin);
                    motionPath.SetEndPointA(newOrigin);
                }
            }

            endCell = testCell;

            endPos.X = motionPath.EndPointB.X;
            endPos.Z = motionPath.EndPointB.Y;
            testCell.MapVectorHeightToCell(ref endPos);
        }

        // LineOfSightTest
        // ----------------------------------------------------------------------------------------
        //
        // Test to see if two points on the mesh can view each other
        //
        public bool LineOfSightTest(NavigationCell startCell, Vector3 startPos, NavigationCell endCell, Vector3 endPos)
        {
            Line2D motionPath = new(new Vector2(startPos.X, startPos.Z), new Vector2(endPos.X, endPos.Z));
            NavigationCell nextCell = startCell;
            NavigationCell.CELL_SIDE wallNumber;
            NavigationCell.PATH_RESULT result;

            while ((result = nextCell.ClassifyPathToCell(motionPath, out nextCell, out wallNumber, null)).HasFlag(NavigationCell.PATH_RESULT.EXITING_CELL))
            {
                if (nextCell == null) return false;
            }

            return result == NavigationCell.PATH_RESULT.ENDING_CELL;
        }

        // LinkCells
        // ----------------------------------------------------------------------------------------
        //
        // Link all the cells that are in our pool
        //
        public void LinkCells()
        {
            foreach (var pCellA in m_CellArray)
            {
                foreach (var pCellB in m_CellArray)
                {
                    if (pCellA != pCellB)
                    {
                        if (!pCellA.IsLinked((int)NavigationCell.CELL_SIDE.SIDE_AB) && pCellB.RequestLink(pCellA.Vertex(0), pCellA.Vertex(1), pCellA))
                        {
                            pCellA.SetLink(NavigationCell.CELL_SIDE.SIDE_AB, pCellB);
                        }
                        else if (!pCellA.IsLinked((int)NavigationCell.CELL_SIDE.SIDE_BC) && pCellB.RequestLink(pCellA.Vertex(1), pCellA.Vertex(2), pCellA))
                        {
                            pCellA.SetLink(NavigationCell.CELL_SIDE.SIDE_BC, pCellB);
                        }
                        else if (!pCellA.IsLinked((int)NavigationCell.CELL_SIDE.SIDE_CA) && pCellB.RequestLink(pCellA.Vertex(2), pCellA.Vertex(0), pCellA))
                        {
                            pCellA.SetLink(NavigationCell.CELL_SIDE.SIDE_CA, pCellB);
                        }
                    }
                }
            }
        }
    }
}

//from league 
/*
void __userpurge NavigationMesh::Nav_AddActor(Actor_Common *inActor@<esi>, NavigationMesh *this)
{
  TestNavGrid *mNavGrid; // edi
  TestNavGridCell *Cell; // edi
  TestNavGrid *v4; // [esp+0h] [ebp-18h]
  float Z; // [esp+14h] [ebp-4h]

  if ( !inActor->m_CurrentGridCell )
  {
    mNavGrid = this->mNavGrid;
    Z = inActor->m_Position.Z;
    inActor->m_CurrentGridCellLocator.mX = (int)((inActor->m_Position.X - this->mNavGrid->mMinGridPos.X)
                                               / this->mNavGrid->mCellSize);
    inActor->m_CurrentGridCellLocator.mY = (int)((Z - mNavGrid->mMinGridPos.Z) / mNavGrid->mCellSize);
    Cell = TestNavGrid::GetCell(this->mNavGrid, &inActor->m_CurrentGridCellLocator);
    inActor->m_CurrentGridCell = Cell;
    if ( inActor == Cell->mActorList
      && soft_assert_log(
           0,
           stru_91C0C4.VertexShaders[197].m_InFName._Bx._Buf,
           &stru_91C0C4.VertexShaders[197].Name[36],
           stru_91C0C4.VertexShaders[196].m_InFName._Bx._Buf,
           (const char *)0x11,
           &stru_91C0C4.VertexShaders[196].Name[56]) )
    {
      __debugbreak();
    }
    inActor->m_NextActorInGridCell = Cell->mActorList;
    Cell->mActorList = inActor;
    TestNavGrid::AddActor2(inActor, v4);
  }
}
*/