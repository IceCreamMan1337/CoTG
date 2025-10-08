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
using System.Collections.Generic;
using System.Numerics;

/* NavigationNode
------------------------------------------------------------------------------------------

A NavigationNode represents an entry in the NavigationHeap. It provides some simple
operators to classify it against other NavigationNodes when the heap is sorted.

------------------------------------------------------------------------------------------
*/

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.Navigation.aimesh
{
    public class NavigationNode : IComparable<NavigationNode>
    {
        public NavigationCell Cell { get; set; }
        public float Cost { get; set; }

        public NavigationNode()
        {
            Cell = null;
            Cost = 0;
        }

        public int CompareTo(NavigationNode other)
        {
            if (other == null) return 1;
            return Cost.CompareTo(other.Cost);
        }

        /*  public static bool operator <(NavigationNode a, NavigationNode b) => a.CompareTo(b) < 0;
          public static bool operator >(NavigationNode a, NavigationNode b) => a.CompareTo(b) > 0;
          public static bool operator ==(NavigationNode a, NavigationNode b) => a.Cell == b.Cell && a.Cost == b.Cost;
          public static bool operator !=(NavigationNode a, NavigationNode b) => !(a == b);
        */
        public override bool Equals(object obj)
        {
            if (obj is NavigationNode other)
            {
                return this == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Cell, Cost).GetHashCode();
        }
    }

}
/* NavigationHeap
------------------------------------------------------------------------------------------

A NavigationHeap is a priority-ordered list using a heap structure. It is used to hold
the current pathfinding session ID and the desired goal point for NavigationCells to query.

------------------------------------------------------------------------------------------
*/

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.Navigation.aimesh
{
    public class NavigationHeap
    {
        private List<NavigationNode> nodes = new();
        private int sessionID;
        private Vector3 goal;

        public NavigationHeap()
        {
        }

        public void Setup(int sessionID, Vector3 goal)
        {
            this.goal = goal;
            this.sessionID = sessionID;
            nodes.Clear();
        }

        public void AddCell(NavigationCell cell)
        {
            var newNode = new NavigationNode
            {
                Cell = cell,
                Cost = cell.PathfindingCost()
            };

            nodes.Add(newNode);
            nodes.Sort(); // Maintain heap order
        }

        public void AdjustCell(NavigationCell cell)
        {
            var index = nodes.FindIndex(node => node.Cell == cell);
            if (index != -1)
            {
                nodes[index].Cell = cell;
                nodes[index].Cost = cell.PathfindingCost();
                nodes.Sort(); // Reorder the heap
            }
        }

        public bool NotEmpty() => nodes.Count > 0;

        public void GetTop(out NavigationNode topNode)
        {
            topNode = nodes[0];
            nodes.RemoveAt(0);
        }

        public int SessionID() => sessionID;

        public Vector3 Goal() => goal;
    }
}
