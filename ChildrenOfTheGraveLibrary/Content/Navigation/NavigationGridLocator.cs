using System;
using System.IO;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.Navigation
{
    public struct NavigationGridLocator
    {
        public short X { get; private set; }
        public short Y { get; private set; }

        public NavigationGridLocator(short x, short y)
        {
            X = x;
            Y = y;
        }
        public NavigationGridLocator(BinaryReader br)
        {
            X = br.ReadInt16();
            Y = br.ReadInt16();
        }

        public static int Distance(NavigationGridLocator a, NavigationGridLocator b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
        public static NavigationGridLocator[] sArrivalOffsets = new NavigationGridLocator[]
    {
        new(1, 0),
        new(1, 1),
        new(0, 1),
        new(-1, 1),
        new(-1, 0),
        new(-1, -1),
        new(0, -1),
        new(1, -1)
    };
    }
    public struct CollisionState
    {
        public bool mGhosted { get; set; }
        public bool mGhostProof { get; set; }

        public bool mIgnoreCollisions { get; set; }
        public CollisionState(bool _mGhosted, bool _mGhostProof, bool _mIgnoreCollisions)
        {
            mGhosted = _mGhosted;
            mGhostProof = _mGhostProof;
            mIgnoreCollisions = mIgnoreCollisions;
        }
    }
}