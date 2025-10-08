/* Copyright (C) Greg Snook, 2000. 
 * All rights reserved worldwide.
 *
 * This software is provided "as is" without express or implied
 * warranties. You may freely copy and compile this source into
 * applications you distribute provided that the copyright text
 * below is included in the resulting source code, for example:
 * "Portions Copyright (C) Greg Snook, 2000"
 */

using System;
using System.Numerics;


namespace CoTG.CoTGServer.Content.Navigation.aimesh
{
    public class NavigationCell : IDisposable
    {
        // ----- ENUMERATIONS & CONSTANTS -----

        public enum CellVert
        {
            VERT_A = 0,
            VERT_B,
            VERT_C
        }


        public enum PATH_RESULT
        {
            NO_RELATIONSHIP,   // the path does not cross this cell
            ENDING_CELL,       // the path ends in this cell	
            EXITING_CELL       // the path exits this cell through side X
        }

        public enum CELL_SIDE
        {
            SIDE_AB = 0,
            SIDE_BC,
            SIDE_CA
        };

        // ----- DATA -------------------------
        private Plane m_CellPlane = new();     // A plane containing the cell triangle
        private Vector3[] m_Vertex = new Vector3[3];    // pointers to the vertices of this triangle
        private Vector3 m_CenterPoint = Vector3.Zero; // The center of the triangle
        private Line2D[] m_Side = new Line2D[3];        // a 2D line representing each cell Side
        private NavigationCell[] m_Link = new NavigationCell[3]; // pointers to cells that attach to this cell

        // Pathfinding Data
        private int m_SessionID = 0;
        private float m_ArrivalCost = 0.0f;
        private float m_Heuristic = 0.0f;
        private bool m_Open = false;
        private int m_ArrivalWall = 0;
        private Vector3[] m_WallMidpoint = new Vector3[3];
        private float[] m_WallDistance = new float[3];

        // ----- CREATORS ---------------------

        public NavigationCell()
        {

        }

        public NavigationCell(NavigationCell src)
        {
            this.Assign(src);
        }
        public void Dispose()
        {
            for (int i = 0; i < m_Link.Length; i++)
            {
                m_Link[i] = null; // D�-r�f�rence les liens
            }

            m_CellPlane = default;
            m_Vertex = null;
            m_CenterPoint = default;
            m_Side = null;
            m_Link = null;
            m_WallMidpoint = null;
            m_WallDistance = null;
        }
        // ----- OPERATORS --------------------
        public NavigationCell Assign(NavigationCell src)
        {
            if (this != src)
            {
                m_CellPlane = src.m_CellPlane;
                m_CenterPoint = src.m_CenterPoint;
                m_SessionID = src.m_SessionID;
                m_ArrivalCost = src.m_ArrivalCost;
                m_Heuristic = src.m_Heuristic;
                m_Open = src.m_Open;
                m_ArrivalWall = src.m_ArrivalWall;

                for (int i = 0; i < 3; i++)
                {
                    m_Vertex[i] = src.m_Vertex[i];
                    m_Side[i] = src.m_Side[i];
                    m_Link[i] = src.m_Link[i];
                    m_WallMidpoint[i] = src.m_WallMidpoint[i];
                    m_WallDistance[i] = src.m_WallDistance[i];
                }
            }
            return this;
        }

        // ----- MUTATORS ---------------------
        public void Initialize(Vector3 pointA, Vector3 pointB, Vector3 pointC)
        {
            m_Vertex[(int)CellVert.VERT_A] = pointA;
            m_Vertex[(int)CellVert.VERT_B] = pointB;
            m_Vertex[(int)CellVert.VERT_C] = pointC;

            m_Link[(int)CELL_SIDE.SIDE_AB] = null;
            m_Link[(int)CELL_SIDE.SIDE_BC] = null;
            m_Link[(int)CELL_SIDE.SIDE_CA] = null;

            m_Side = new Line2D[3] { new(), new(), new() };

            ComputeCellData();
        }

        public void ComputeCellData()
        {
            // Create 2D versions of our vertices
            Vector2 point1 = new(m_Vertex[(int)CellVert.VERT_A].X, m_Vertex[(int)CellVert.VERT_A].Z);
            Vector2 point2 = new(m_Vertex[(int)CellVert.VERT_B].X, m_Vertex[(int)CellVert.VERT_B].Z);
            Vector2 point3 = new(m_Vertex[(int)CellVert.VERT_C].X, m_Vertex[(int)CellVert.VERT_C].Z);

            // Initialize our sides
            m_Side[(int)CELL_SIDE.SIDE_AB].SetPoints(point1, point2); // line AB
            m_Side[(int)CELL_SIDE.SIDE_BC].SetPoints(point2, point3); // line BC
            m_Side[(int)CELL_SIDE.SIDE_CA].SetPoints(point3, point1); // line CA

            m_CellPlane.Set(m_Vertex[(int)CellVert.VERT_A], m_Vertex[(int)CellVert.VERT_B], m_Vertex[(int)CellVert.VERT_C]);

            // Compute midpoint as centroid of polygon
            m_CenterPoint = (m_Vertex[(int)CellVert.VERT_A] + m_Vertex[(int)CellVert.VERT_B] + m_Vertex[(int)CellVert.VERT_C]) / 3.0f;

            // Compute the midpoint of each cell wall
            m_WallMidpoint[0] = (m_Vertex[(int)CellVert.VERT_A] + m_Vertex[(int)CellVert.VERT_B]) / 2.0f;
            m_WallMidpoint[1] = (m_Vertex[(int)CellVert.VERT_C] + m_Vertex[(int)CellVert.VERT_B]) / 2.0f;
            m_WallMidpoint[2] = (m_Vertex[(int)CellVert.VERT_C] + m_Vertex[(int)CellVert.VERT_A]) / 2.0f;

            // Compute the distances between the wall midpoints
            Vector3 wallVector = m_WallMidpoint[0] - m_WallMidpoint[1];
            m_WallDistance[0] = wallVector.Length();

            wallVector = m_WallMidpoint[1] - m_WallMidpoint[2];
            m_WallDistance[1] = wallVector.Length();

            wallVector = m_WallMidpoint[2] - m_WallMidpoint[0];
            m_WallDistance[2] = wallVector.Length();
        }

        public bool RequestLink(Vector3 pointA, Vector3 pointB, NavigationCell caller)
        {
            if (m_Vertex[(int)CellVert.VERT_A] == pointA)
            {
                if (m_Vertex[(int)CellVert.VERT_B] == pointB)
                {
                    m_Link[(int)CELL_SIDE.SIDE_AB] = caller;
                    return true;
                }
                else if (m_Vertex[(int)CellVert.VERT_C] == pointB)
                {
                    m_Link[(int)CELL_SIDE.SIDE_CA] = caller;
                    return true;
                }
            }
            else if (m_Vertex[(int)CellVert.VERT_B] == pointA)
            {
                if (m_Vertex[(int)CellVert.VERT_A] == pointB)
                {
                    m_Link[(int)CELL_SIDE.SIDE_AB] = caller;
                    return true;
                }
                else if (m_Vertex[(int)CellVert.VERT_C] == pointB)
                {
                    m_Link[(int)CELL_SIDE.SIDE_BC] = caller;
                    return true;
                }
            }
            else if (m_Vertex[(int)CellVert.VERT_C] == pointA)
            {
                if (m_Vertex[(int)CellVert.VERT_A] == pointB)
                {
                    m_Link[(int)CELL_SIDE.SIDE_CA] = caller;
                    return true;
                }
                else if (m_Vertex[(int)CellVert.VERT_B] == pointB)
                {
                    m_Link[(int)CELL_SIDE.SIDE_BC] = caller;
                    return true;
                }
            }

            return false;
        }

        public void SetLink(CELL_SIDE side, NavigationCell caller)
        {
            m_Link[(int)side] = caller;
        }

        public void MapVectorHeightToCell(ref Vector3 motionPoint)
        {
            motionPoint.Y = m_CellPlane.SolveForY(motionPoint.X, motionPoint.Z);
        }

        // ----- ACCESSORS --------------------
        public bool IsPointInCellColumn(Vector2 testPoint)
        {
            int interiorCount = 0;

            for (int i = 0; i < 3; i++)
            {
                Line2D.PointClassification sideResult = m_Side[i].ClassifyPoint(testPoint);

                if (sideResult.HasFlag(Line2D.PointClassification.LeftSide))
                {
                    interiorCount++;
                }
            }

            return interiorCount == 3;
        }

        public bool IsPointInCellColumn(Vector3 testPoint)
        {
            Vector2 testPoint2D = new(testPoint.X, testPoint.Z);
            return IsPointInCellColumn(testPoint2D);
        }

        public Vector3 Vertex(int vert)
        {
            return m_Vertex[vert];
        }

        public Vector3 CenterPoint()
        {
            return m_CenterPoint;
        }

        public NavigationCell Link(int side)
        {
            return m_Link[side];
        }
        public bool IsLinked(int side)
        {
            if (m_Link[side] != null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public float ArrivalCost()
        {
            return m_ArrivalCost;
        }

        public float Heuristic()
        {
            return m_Heuristic;
        }

        public float PathfindingCost()
        {
            return m_ArrivalCost + m_Heuristic;
        }

        public bool Open()
        {
            return m_Open;
        }

        public int ArrivalWall()
        {
            return m_ArrivalWall;
        }

        public Vector3 WallMidpoint(int wall)
        {
            return m_WallMidpoint[wall];
        }

        public float WallDistance(int wall)
        {
            return m_WallDistance[wall];
        }

        public int SessionID()
        {
            return m_SessionID;
        }

        public void SetArrivalCost(float cost)
        {
            m_ArrivalCost = cost;
        }

        public void SetHeuristic(float heuristic)
        {
            m_Heuristic = heuristic;
        }

        public void SetOpen(bool open)
        {
            m_Open = open;
        }

        public void SetArrivalWall(int arrivalWall)
        {
            m_ArrivalWall = arrivalWall;
        }

        public void SetSessionID(int sessionID)
        {
            m_SessionID = sessionID;
        }




        public PATH_RESULT ClassifyPathToCell(Line2D MotionPath, out NavigationCell pNextCell, out CELL_SIDE Side, Vector2? pPointOfIntersection = null)
        {
            pNextCell = null;
            Side = CELL_SIDE.SIDE_AB;
            int InteriorCount = 0;

            for (int i = 0; i < 3; ++i)
            {
                if (!m_Side[i].ClassifyPoint(MotionPath.EndPointB).HasFlag(Line2D.PointClassification.RightSide))
                {
                    if (!m_Side[i].ClassifyPoint(MotionPath.EndPointA).HasFlag(Line2D.PointClassification.LeftSide))
                    {
                        var IntersectResult = MotionPath.Intersection(m_Side[i], out pPointOfIntersection);

                        if (IntersectResult.HasFlag(Line2D.LineClassification.SegmentsIntersect) || IntersectResult.HasFlag(Line2D.LineClassification.A_Bisects_B))
                        {
                            pNextCell = m_Link[i];
                            Side = (CELL_SIDE)i;
                            return PATH_RESULT.EXITING_CELL;
                        }
                    }
                }
                else
                {
                    InteriorCount++;
                }
            }

            if (InteriorCount == 3)
            {
                return PATH_RESULT.ENDING_CELL;
            }

            return PATH_RESULT.NO_RELATIONSHIP;
        }

        public void ProjectPathOnCellWall(CELL_SIDE SideNumber, ref Line2D MotionPath)
        {
            Vector2 WallNormal = m_Side[(int)SideNumber].EndPointB - m_Side[(int)SideNumber].EndPointA;
            Vector2.Normalize(WallNormal);

            Vector2 MotionVector = MotionPath.EndPointB - MotionPath.EndPointA;

            float DotResult = Vector2.Dot(MotionVector, WallNormal);

            MotionVector = DotResult * WallNormal;

            MotionPath.SetEndPointB(MotionPath.EndPointA + MotionVector);

            Vector2 NewPoint = MotionPath.EndPointA;
            ForcePointToCellColumn(ref NewPoint);
            MotionPath.SetEndPointA(NewPoint);

            NewPoint = MotionPath.EndPointB;
            ForcePointToWallInterior(SideNumber, ref NewPoint);
            MotionPath.SetEndPointB(NewPoint);
        }

        public bool ForcePointToWallInterior(CELL_SIDE SideNumber, ref Vector2 TestPoint)
        {
            float Distance = m_Side[(int)SideNumber].SignedDistance(TestPoint);
            float Epsilon = 0.001f;

            if (Distance <= Epsilon)
            {
                if (Distance <= 0.0f)
                {
                    Distance -= Epsilon;
                }

                Distance = Math.Abs(Distance);
                Distance = Epsilon > Distance ? Epsilon : Distance;

                Vector2 Normal = m_Side[(int)SideNumber].Normal;
                TestPoint += Normal * Distance;
                return true;
            }
            return false;
        }

        public bool ForcePointToWallInterior(CELL_SIDE SideNumber, ref Vector3 TestPoint)
        {
            Vector2 TestPoint2D = new(TestPoint.X, TestPoint.Z);
            bool PointAltered = ForcePointToWallInterior(SideNumber, ref TestPoint2D);

            if (PointAltered)
            {
                TestPoint.X = TestPoint2D.X;
                TestPoint.Y = TestPoint2D.Y;
            }

            return PointAltered;
        }

        public bool ForcePointToCellColumn(ref Vector2 TestPoint)
        {
            bool PointAltered = false;

            Line2D TestPath = new(new Vector2(m_CenterPoint.X, m_CenterPoint.Z), TestPoint);
            Vector2 PointOfIntersection = Vector2.Zero;
            CELL_SIDE Side;
            NavigationCell NextCell;

            PATH_RESULT result = ClassifyPathToCell(TestPath, out NextCell, out Side, PointOfIntersection);

            if (result == PATH_RESULT.EXITING_CELL)
            {
                Vector2 PathDirection = PointOfIntersection - new Vector2(m_CenterPoint.X, m_CenterPoint.Z);
                PathDirection *= 0.9f;
                TestPoint = new Vector2(m_CenterPoint.X, m_CenterPoint.Z) + PathDirection;
                return true;
            }
            else if (result == PATH_RESULT.NO_RELATIONSHIP)
            {
                TestPoint = new Vector2(m_CenterPoint.X, m_CenterPoint.Z);
                return true;
            }

            return PointAltered;
        }

        public bool ForcePointToCellColumn(ref Vector3 TestPoint)
        {
            Vector2 TestPoint2D = new(TestPoint.X, TestPoint.Z);
            bool PointAltered = ForcePointToCellColumn(ref TestPoint2D);

            if (PointAltered)
            {
                TestPoint.X = TestPoint2D.X;
                TestPoint.Z = TestPoint2D.Y;
            }
            return PointAltered;
        }

        public bool ProcessCell(NavigationHeap pHeap)
        {
            if (m_SessionID == pHeap.SessionID())
            {
                m_Open = false;

                for (int i = 0; i < 3; ++i)
                {
                    if (m_Link[i] != null)
                    {
                        m_Link[i].QueryForPath(pHeap, this, m_ArrivalCost + m_WallDistance[Math.Abs(i - m_ArrivalWall)]);
                    }
                }
                return true;
            }
            return false;
        }

        public bool QueryForPath(NavigationHeap pHeap, NavigationCell Caller, float arrivalCost)
        {
            if (m_SessionID != pHeap.SessionID())
            {
                m_SessionID = pHeap.SessionID();

                if (Caller != null)
                {
                    m_Open = true;
                    ComputeHeuristic(pHeap.Goal());
                    m_ArrivalCost = arrivalCost;

                    m_ArrivalWall = Caller == m_Link[0] ? 0 : (Caller == m_Link[1] ? 1 : 2);
                }
                else
                {
                    m_Open = false;
                    m_ArrivalCost = 0;
                    m_Heuristic = 0;
                    m_ArrivalWall = 0;
                }
                pHeap.AddCell(this);
                return true;
            }
            else if (m_Open && (arrivalCost + m_Heuristic) < (m_ArrivalCost + m_Heuristic))
            {
                m_ArrivalCost = arrivalCost;
                m_ArrivalWall = Caller == m_Link[0] ? 0 : (Caller == m_Link[1] ? 1 : 2);
                pHeap.AdjustCell(this);
                return true;
            }
            return false;
        }

        public void ComputeHeuristic(Vector3 Goal)
        {
            float XDelta = Math.Abs(Goal.X - m_CenterPoint.X);
            float YDelta = Math.Abs(Goal.Y - m_CenterPoint.Y);
            float ZDelta = Math.Abs(Goal.Z - m_CenterPoint.Z);

            m_Heuristic = Math.Max(Math.Max(XDelta, YDelta), ZDelta);
        }
    }





}