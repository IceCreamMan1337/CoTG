using System.Numerics;



namespace CoTG.CoTGServer.Content.Navigation.aimesh
{
    public class Plane
    {
        // ----- DATA -------------------------
        private Vector3 m_Normal;
        private Vector3 m_Point;
        private float m_Distance;

        // ----- CREATORS ---------------------
        public Plane() { }

        public Plane(Plane src)
        {
            m_Normal = src.m_Normal;
            m_Point = src.m_Point;
            m_Distance = src.m_Distance;
        }

        public Plane(Vector3 point0, Vector3 point1, Vector3 point2)
        {
            Set(point0, point1, point2);
        }

        // ----- OPERATORS --------------------
        public Plane Assign(Plane src)
        {
            m_Normal = src.m_Normal;
            m_Point = src.m_Point;
            m_Distance = src.m_Distance;
            return this;
        }

        public static bool operator ==(Plane planeA, Plane planeB)
        {
            return planeA.m_Normal == planeB.m_Normal && planeA.m_Point == planeB.m_Point;
        }

        public static bool operator !=(Plane planeA, Plane planeB)
        {
            return !(planeA == planeB);
        }

        // ----- MUTATORS ---------------------
        public void Set(Vector3 point0, Vector3 point1, Vector3 point2)
        {
            m_Normal = Vector3.Cross(point1 - point0, point2 - point0);
            m_Normal = Vector3.Normalize(m_Normal);
            m_Point = point0;
            m_Distance = -Vector3.Dot(m_Point, m_Normal);
        }

        public float SolveForX(float y, float z)
        {
            if (m_Normal.X != 0)
            {
                return -((m_Normal.Y * y) + (m_Normal.Z * z) + m_Distance) / m_Normal.X;
            }
            return 0.0f;
        }

        public float SolveForY(float x, float z)
        {
            if (m_Normal.Y != 0)
            {
                return -((m_Normal.X * x) + (m_Normal.Z * z) + m_Distance) / m_Normal.Y;
            }
            return 0.0f;
        }

        public float SolveForZ(float x, float y)
        {
            if (m_Normal.Z != 0)
            {
                return -((m_Normal.X * x) + (m_Normal.Y * y) + m_Distance) / m_Normal.Z;
            }
            return 0.0f;
        }

        // ----- ACCESSORS --------------------
        public Vector3 Normal => m_Normal;
        public Vector3 Point => m_Point;
        public float Distance => m_Distance;

        public float SignedDistance(Vector3 point)
        {
            return Vector3.Dot(m_Normal, point) + m_Distance;
        }
    }
}