using System.Numerics;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.Navigation.aimesh
{
    public class Line2D
    {
        // ----- ENUMERATIONS & CONSTANTS -----
        public enum PointClassification
        {
            OnLine,     // The point is on, or very near, the line
            LeftSide,   // looking from endpoint A to B, the test point is on the left
            RightSide   // looking from endpoint A to B, the test point is on the right
        }

        public enum LineClassification
        {
            Collinear,           // both lines are parallel and overlap each other
            LinesIntersect,      // lines intersect, but their segments do not
            SegmentsIntersect,   // both line segments bisect each other
            A_Bisects_B,         // line segment B is crossed by line A
            B_Bisects_A,         // line segment A is crossed by line B
            Parallel             // the lines are parallel
        }

        // ----- DATA -------------------------
        private Vector2 _pointA;  // Endpoint A of our line segment
        private Vector2 _pointB;  // Endpoint B of our line segment

        private Vector2 _normal;  // 'normal' of the ray. 
                                  // a vector pointing to the right-hand side of the line
                                  // when viewed from PointA towards PointB
        private bool _normalCalculated; // normals are only calculated on demand

        // ----- CREATORS ---------------------
        public Line2D()
        {
            _normalCalculated = false;
        }

        public Line2D(Vector2 pointA, Vector2 pointB)
        {
            _pointA = pointA;
            _pointB = pointB;
            _normalCalculated = false;
        }

        public Line2D(Line2D src)
        {
            _pointA = src._pointA;
            _pointB = src._pointB;
            _normal = src._normal;
            _normalCalculated = src._normalCalculated;
        }

        // ----- MUTATORS ---------------------
        public void SetEndPointA(Vector2 point)
        {
            _pointA = point;
            _normalCalculated = false;
        }

        public void SetEndPointB(Vector2 point)
        {
            _pointB = point;
            _normalCalculated = false;
        }

        public void SetPoints(Vector2 pointA, Vector2 pointB)
        {
            _pointA = pointA;
            _pointB = pointB;
            _normalCalculated = false;
        }

        public void SetPoints(float pointAx, float pointAy, float pointBx, float pointBy)
        {
            _pointA.X = pointAx;
            _pointA.Y = pointAy;
            _pointB.X = pointBx;
            _pointB.Y = pointBy;
            _normalCalculated = false;
        }

        public float SignedDistance(Vector2 point)
        {
            if (!_normalCalculated)
            {
                ComputeNormal();
            }

            Vector2 testVector = point - _pointA;
            return Vector2.Dot(testVector, _normal);
        }

        public PointClassification ClassifyPoint(Vector2 point, float epsilon = 0.0f)
        {
            float distance = SignedDistance(point);
            if (distance > epsilon)
            {
                return PointClassification.RightSide;
            }
            else if (distance < -epsilon)
            {
                return PointClassification.LeftSide;
            }

            return PointClassification.OnLine;
        }

        public LineClassification Intersection(Line2D line, out Vector2? intersectPoint)
        {
            intersectPoint = null;

            float ayMinusCy = _pointA.Y - line._pointA.Y;
            float dxMinusCx = line._pointB.X - line._pointA.X;
            float axMinusCx = _pointA.X - line._pointA.X;
            float dyMinusCy = line._pointB.Y - line._pointA.Y;
            float bxMinusAx = _pointB.X - _pointA.X;
            float byMinusAy = _pointB.Y - _pointA.Y;

            float numerator = (ayMinusCy * dxMinusCx) - (axMinusCx * dyMinusCy);
            float denominator = (bxMinusAx * dyMinusCy) - (byMinusAy * dxMinusCx);

            if (denominator == 0)
            {
                if (numerator == 0)
                {
                    return LineClassification.Collinear;
                }

                return LineClassification.Parallel;
            }

            float factorAB = numerator / denominator;
            float factorCD = ((ayMinusCy * bxMinusAx) - (axMinusCx * byMinusAy)) / denominator;

            if (factorAB >= 0.0f && factorAB <= 1.0f && factorCD >= 0.0f && factorCD <= 1.0f)
            {
                intersectPoint = new Vector2(
                    _pointA.X + (factorAB * bxMinusAx),
                    _pointA.Y + (factorAB * byMinusAy)
                );
                return LineClassification.SegmentsIntersect;
            }
            else if (factorCD >= 0.0f && factorCD <= 1.0f)
            {
                return LineClassification.A_Bisects_B;
            }
            else if (factorAB >= 0.0f && factorAB <= 1.0f)
            {
                return LineClassification.B_Bisects_A;
            }

            return LineClassification.LinesIntersect;
        }

        // ----- ACCESSORS --------------------
        public Vector2 EndPointA => _pointA;
        public Vector2 EndPointB => _pointB;

        public float Length => Vector2.Distance(_pointA, _pointB);

        public Vector2 Normal
        {
            get
            {
                if (!_normalCalculated)
                {
                    ComputeNormal();
                }
                return _normal;
            }
        }

        private void ComputeNormal()
        {
            Vector2 direction = _pointB - _pointA;
            Vector2.Normalize(direction);

            // Rotate by -90 degrees to get the normal of the line
            _normal = new Vector2(direction.Y, -direction.X);
            _normalCalculated = true;
        }
    }
}