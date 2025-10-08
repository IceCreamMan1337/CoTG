/* Copyright (C) Greg Snook, 2000. 
 * All rights reserved worldwide.
 *
 * This software is provided "as is" without express or implied
 * warranties. You may freely copy and compile this source into
 * applications you distribute provided that the copyright text
 * below is included in the resulting source code, for example:
 * "Portions Copyright (C) Greg Snook, 2000"
 */

using System.Collections.Generic;
using System.Numerics;

/*	NavigationPath
------------------------------------------------------------------------------------------
	
	NavigationPath is a collection of waypoints that define a movement path for an Actor.
	This object is owned by an Actor and filled by NavigationMesh::BuildNavigationPath().
	
------------------------------------------------------------------------------------------
*/
namespace CoTG.CoTGServer.Content.Navigation.aimesh
{
    public class NavigationPath
    {
        // ----- ENUMERATIONS & CONSTANTS -----

        // Definition of a waypoint
        public struct Waypoint
        {
            public Vector3 Position;    // 3D position of waypoint	
            public NavigationCell Cell; // The cell which owns the waypoint
        }

        public List<Waypoint> m_WaypointList = new();

        // ----- DATA -------------------------
        private NavigationMesh m_Parent;
        private Waypoint m_StartPoint;
        private Waypoint m_EndPoint;

        // ----- CREATORS ---------------------

        public NavigationPath()
        {
        }

        ~NavigationPath()
        {
        }

        // ----- MUTATORS ---------------------
        public void Setup(NavigationMesh parent, Vector3 startPoint, NavigationCell startCell, Vector3 endPoint, NavigationCell endCell)
        {
            m_WaypointList.Clear();

            m_Parent = parent;
            m_StartPoint.Position = startPoint;
            m_StartPoint.Cell = startCell;
            m_EndPoint.Position = endPoint;
            m_EndPoint.Cell = endCell;

            // Setup the waypoint list with our start and end points
            m_WaypointList.Add(m_StartPoint);
        }

        public void AddWayPoint(Vector3 point, NavigationCell cell)
        {
            Waypoint newPoint;
            newPoint.Position = point;
            newPoint.Cell = cell;

            m_WaypointList.Add(newPoint);
        }

        public void EndPath()
        {
            // Cap the waypoint path with the last endpoint
            m_WaypointList.Add(m_EndPoint);
        }

        // ----- ACCESSORS --------------------
        public NavigationMesh Parent()
        {
            return m_Parent;
        }

        public Waypoint StartPoint()
        {
            return m_StartPoint;
        }

        public Waypoint EndPoint()
        {
            return m_EndPoint;
        }

        public List<Waypoint> WaypointList()
        {
            return m_WaypointList;
        }

        public int GetFurthestVisibleWayPoint(int vantagePointIndex)
        {
            // See if we are already talking about the last waypoint
            if (vantagePointIndex >= m_WaypointList.Count - 1)
            {
                return vantagePointIndex;
            }

            Waypoint vantage = m_WaypointList[vantagePointIndex];
            int testWaypointIndex = vantagePointIndex + 1;

            if (testWaypointIndex >= m_WaypointList.Count)
            {
                return testWaypointIndex;
            }

            int visibleWaypointIndex = testWaypointIndex;
            testWaypointIndex++;

            while (testWaypointIndex < m_WaypointList.Count)
            {
                Waypoint test = m_WaypointList[testWaypointIndex];
                if (!m_Parent.LineOfSightTest(vantage.Cell, vantage.Position, test.Cell, test.Position))
                {
                    return visibleWaypointIndex;
                }
                visibleWaypointIndex = testWaypointIndex;
                testWaypointIndex++;
            }
            return visibleWaypointIndex;
        }
    }
}