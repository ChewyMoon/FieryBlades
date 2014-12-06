#region

using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
// ReSharper disable CompareOfFloatsByEqualityOperator

#endregion

namespace FieryBlade.Util
{
    /// <summary>
    /// This class was taken from LeaguesSharp.Common, released under the GNU General Public License version 3.
    /// </summary>
    public static class Geometry
    {
        //Vector3 class extended methods:

        /// <summary>
        ///     Converts a Vector3 to Vector2
        /// </summary>
        public static Vector2 To2D(this Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }

        /// <summary>
        ///     Returns the 2D distance (XY plane) between two vector.
        /// </summary>
        public static float Distance(this Vector3 v, Vector3 other, bool squared = false)
        {
            return v.To2D().Distance(other, squared);
        }

        //Vector2 class extended methods:

        /// <summary>
        ///     Returns true if the Vector2 is valid.
        /// </summary>
        public static bool IsValid(this Vector2 v)
        {
            return (v.X != 0 && v.Y != 0);
        }

        /// <summary>
        ///     Calculates the distance to the Vector2.
        /// </summary>
        public static float Distance(this Vector2 v, Vector2 to, bool squared = false)
        {
            return squared ? Vector2.DistanceSquared(v, to) : Vector2.Distance(v, to);
        }

        /// <summary>
        ///     Calculates the distance to the Vector3.
        /// </summary>
        public static float Distance(this Vector2 v, Vector3 to, bool squared = false)
        {
            return v.Distance(to.To2D(), squared);
        }

        /// <summary>
        ///     Retursn the distance to the line segment.
        /// </summary>
        public static float Distance(this Vector2 point,
            Vector2 segmentStart,
            Vector2 segmentEnd,
            bool onlyIfOnSegment = false,
            bool squared = false)
        {
            var objects = point.ProjectOn(segmentStart, segmentEnd);

            if (objects.IsOnSegment || onlyIfOnSegment == false)
            {
                return squared
                    ? Vector2.DistanceSquared(objects.SegmentPoint, point)
                    : Vector2.Distance(objects.SegmentPoint, point);
            }
            return float.MaxValue;
        }

        /// <summary>
        ///     Returns the vector normalized.
        /// </summary>
        public static Vector2 Normalized(this Vector2 v)
        {
            v.Normalize();
            return v;
        }

        public static Vector2 Extend(this Vector2 v, Vector2 to, float distance)
        {
            return v + distance*(to - v).Normalized();
        }

        public static Vector3 SwitchYZ(this Vector3 v)
        {
            return new Vector3(v.X, v.Z, v.Y);
        }

        /// <summary>
        ///     Returns the perpendicular vector.
        /// </summary>
        public static Vector2 Perpendicular(this Vector2 v)
        {
            return new Vector2(-v.Y, v.X);
        }

        /// <summary>
        ///     Returns the second perpendicular vector.
        /// </summary>
        public static Vector2 Perpendicular2(this Vector2 v)
        {
            return new Vector2(v.Y, -v.X);
        }

        /// <summary>
        ///     Rotates the vector a set angle (angle in radians).
        /// </summary>
        public static Vector2 Rotated(this Vector2 v, float angle)
        {
            var c = Math.Cos(angle);
            var s = Math.Sin(angle);

            return new Vector2((float) (v.X*c - v.Y*s), (float) (v.Y*c + v.X*s));
        }

        /// <summary>
        ///     Returns the cross product Z value.
        /// </summary>
        public static float CrossProduct(this Vector2 self, Vector2 other)
        {
            return other.Y*self.X - other.X*self.Y;
        }

        public static float RadianToDegree(double angle)
        {
            return (float) (angle*(180.0/Math.PI));
        }

        public static float DegreeToRadian(double angle)
        {
            return (float) (Math.PI*angle/180.0);
        }

        /// <summary>
        ///     Returns the polar for vector angle (in Degrees).
        /// </summary>
        public static float Polar(this Vector2 v1)
        {
            if (Close(v1.X, 0, 0))
            {
                if (v1.Y > 0)
                {
                    return 90;
                }
                if (v1.Y < 0)
                {
                    return 270;
                }

                return 0;
            }

            var theta = RadianToDegree(Math.Atan((v1.Y)/v1.X));
            if (v1.X < 0)
            {
                theta = theta + 180;
            }
            if (theta < 0)
            {
                theta = theta + 360;
            }
            return theta;
        }

        /// <summary>
        ///     Returns the angle with the vector p2.
        /// </summary>
        public static float AngleBetween(this Vector2 p1, Vector2 p2)
        {
            var theta = p1.Polar() - p2.Polar();
            if (theta < 0)
            {
                theta = theta + 360;
            }
            if (theta > 180)
            {
                theta = 360 - theta;
            }
            return theta;
        }

        /// <summary>
        ///     Returns the closest vector from a list.
        /// </summary>
        public static Vector2 Closest(this Vector2 v, List<Vector2> vList)
        {
            var result = new Vector2();
            var dist = float.MaxValue;

            foreach (var vector in vList)
            {
                var distance = Vector2.DistanceSquared(v, vector);
                if (distance < dist)
                {
                    dist = distance;
                    result = vector;
                }
            }

            return result;
        }

        /// <summary>
        ///     Returns the projection of the Vector2 on the segment.
        /// </summary>
        public static ProjectionInfo ProjectOn(this Vector2 point, Vector2 segmentStart, Vector2 segmentEnd)
        {
            var cx = point.X;
            var cy = point.Y;
            var ax = segmentStart.X;
            var ay = segmentStart.Y;
            var bx = segmentEnd.X;
            var by = segmentEnd.Y;
            var rL = ((cx - ax)*(bx - ax) + (cy - ay)*(by - ay))/
                     ((float) Math.Pow(bx - ax, 2) + (float) Math.Pow(by - ay, 2));
            var pointLine = new Vector2(ax + rL*(bx - ax), ay + rL*(by - ay));
            float rS;
            if (rL < 0)
            {
                rS = 0;
            }
            else if (rL > 1)
            {
                rS = 1;
            }
            else
            {
                rS = rL;
            }

            var isOnSegment = rS.CompareTo(rL) == 0;
            var pointSegment = isOnSegment ? pointLine : new Vector2(ax + rS*(bx - ax), ay + rS*(@by - ay));
            return new ProjectionInfo(isOnSegment, pointSegment, pointLine);
        }

        //From: http://social.msdn.microsoft.com/Forums/vstudio/en-US/e5993847-c7a9-46ec-8edc-bfb86bd689e3/help-on-line-segment-intersection-algorithm
        /// <summary>
        ///     Intersects two line segments.
        /// </summary>
        public static IntersectionResult Intersection(this Vector2 lineSegment1Start,
            Vector2 lineSegment1End,
            Vector2 lineSegment2Start,
            Vector2 lineSegment2End)
        {
            double deltaACy = lineSegment1Start.Y - lineSegment2Start.Y;
            double deltaDCx = lineSegment2End.X - lineSegment2Start.X;
            double deltaACx = lineSegment1Start.X - lineSegment2Start.X;
            double deltaDCy = lineSegment2End.Y - lineSegment2Start.Y;
            double deltaBAx = lineSegment1End.X - lineSegment1Start.X;
            double deltaBAy = lineSegment1End.Y - lineSegment1Start.Y;

            var denominator = deltaBAx*deltaDCy - deltaBAy*deltaDCx;
            var numerator = deltaACy*deltaDCx - deltaACx*deltaDCy;

            if (denominator == 0)
            {
                if (numerator != 0) return new IntersectionResult();
                // collinear. Potentially infinite intersection points.
                // Check and return one of them.
                if (lineSegment1Start.X >= lineSegment2Start.X && lineSegment1Start.X <= lineSegment2End.X)
                {
                    return new IntersectionResult(true, lineSegment1Start);
                }
                if (lineSegment2Start.X >= lineSegment1Start.X && lineSegment2Start.X <= lineSegment1End.X)
                {
                    return new IntersectionResult(true, lineSegment2Start);
                }
                return new IntersectionResult();
                // parallel
            }

            var r = numerator/denominator;
            if (r < 0 || r > 1)
            {
                return new IntersectionResult();
            }

            var s = (deltaACy*deltaBAx - deltaACx*deltaBAy)/denominator;
            if (s < 0 || s > 1)
            {
                return new IntersectionResult();
            }

            return new IntersectionResult(
                true,
                new Vector2((float) (lineSegment1Start.X + r*deltaBAx), (float) (lineSegment1Start.Y + r*deltaBAy)));
        }

        public static Object[] VectorMovementCollision(Vector2 startPoint1,
            Vector2 endPoint1,
            float v1,
            Vector2 startPoint2,
            float v2,
            float delay = 0f)
        {
            float sP1X = startPoint1.X,
                sP1Y = startPoint1.Y,
                eP1X = endPoint1.X,
                eP1Y = endPoint1.Y,
                sP2X = startPoint2.X,
                sP2Y = startPoint2.Y;

            float d = eP1X - sP1X, e = eP1Y - sP1Y;
            float dist = (float) Math.Sqrt(d*d + e*e), t1 = float.NaN;
            float s = dist != 0f ? v1*d/dist : 0, k = (dist != 0) ? v1*e/dist : 0f;

            float r = sP2X - sP1X, j = sP2Y - sP1Y;
            var c = r*r + j*j;


            if (dist > 0f)
            {
                if (v1 == float.MaxValue)
                {
                    var t = dist/v1;
                    t1 = v2*t >= 0f ? t : float.NaN;
                }
                else if (v2 == float.MaxValue)
                {
                    t1 = 0f;
                }
                else
                {
                    float a = s*s + k*k - v2*v2, b = -r*s - j*k;

                    if (a == 0f)
                    {
                        if (b == 0f)
                        {
                            t1 = (c == 0f) ? 0f : float.NaN;
                        }
                        else
                        {
                            var t = -c/(2*b);
                            t1 = (v2*t >= 0f) ? t : float.NaN;
                        }
                    }
                    else
                    {
                        var sqr = b*b - a*c;
                        if (sqr >= 0)
                        {
                            var nom = (float) Math.Sqrt(sqr);
                            var t = (-nom - b)/a;
                            t1 = v2*t >= 0f ? t : float.NaN;
                            t = (nom - b)/a;
                            var t2 = (v2*t >= 0f) ? t : float.NaN;

                            if (!float.IsNaN(t2) && !float.IsNaN(t1))
                            {
                                if (t1 >= delay && t2 >= delay)
                                    t1 = Math.Min(t1, t2);
                                else if (t2 >= delay)
                                    t1 = t2;
                            }
                        }
                    }
                }
            }
            else if (dist == 0f)
            {
                t1 = 0f;
            }

            return new Object[] {t1, (!float.IsNaN(t1)) ? new Vector2(sP1X + s*t1, sP1Y + k*t1) : new Vector2()};
        }

        /// <summary>
        ///     Returns the total distance of a path.
        /// </summary>
        public static float PathLength(this List<Vector2> path)
        {
            var distance = 0f;
            for (var i = 0; i < path.Count - 1; i++)
            {
                distance += path[i].Distance(path[i + 1]);
            }
            return distance;
        }

        /// <summary>
        ///     Converts a 3D path to 2D
        /// </summary>
        public static List<Vector2> To2D(this List<Vector3> path)
        {
            return path.Select(point => point.To2D()).ToList();
        }

        /// <summary>
        ///     Returns the two intersection points between two circles.
        /// </summary>
        public static Vector2[] CircleCircleIntersection(Vector2 center1, Vector2 center2, float radius1, float radius2)
        {
            var d = center1.Distance(center2);
            //The Circles dont intersect:
            if (d > radius1 + radius2)
            {
                return new Vector2[] {};
            }

            var a = (radius1*radius1 - radius2*radius2 + d*d)/(2*d);
            var h = (float) Math.Sqrt(radius1*radius1 - a*a);
            var direction = (center2 - center1).Normalized();
            var pa = center1 + a*direction;
            var s1 = pa + h*direction.Perpendicular();
            var s2 = pa - h*direction.Perpendicular();
            return new[] {s1, s2};
        }

        public static bool Close(float a, float b, float eps)
        {
            if (eps == 0)
            {
                eps = (float) 1e-9;
            }
            return Math.Abs(a - b) <= eps;
        }

        public struct IntersectionResult
        {
            public bool Intersects;
            public Vector2 Point;

            public IntersectionResult(bool intersects = false, Vector2 point = new Vector2())
            {
                Intersects = intersects;
                Point = point;
            }
        }

        public struct ProjectionInfo
        {
            public bool IsOnSegment;
            public Vector2 LinePoint;
            public Vector2 SegmentPoint;

            public ProjectionInfo(bool isOnSegment, Vector2 segmentPoint, Vector2 linePoint)
            {
                IsOnSegment = isOnSegment;
                SegmentPoint = segmentPoint;
                LinePoint = linePoint;
            }
        }
    }
}