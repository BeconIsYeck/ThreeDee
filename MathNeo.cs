using System;


namespace ThreeDee;

public static class MathNeo {
        public static double Distance(Vector2 v0, Vector2 v1) {
            return Math.Sqrt(Math.Pow(v1.X - v0.X, 2) + Math.Pow(v1.Y - v0.Y, 2));
        }

        public static double Slope(Vector2 v0, Vector2 v1) {
            return (v0.X - v1.X) / (v0.Y - v1.Y);
        }

        public static double LinearInterpolate(double first, double second, double by) {
            return first * (1 - by) + second * by;
        }

        public static Vector2 Vector2Lerp(Vector2 first, Vector2 second, double by) {
            return new Vector2(MathNeo.LinearInterpolate(first.X, second.X, by), MathNeo.LinearInterpolate(first.Y, second.Y, by));
        }

        public static bool Vector2OnLineSegment(Vector2 vecA, Vector2 vecB, Vector2 vecC) {
            var a2bDis = MathNeo.Distance(vecA, vecB);
            var a2cDis = MathNeo.Distance(vecA, vecC);
            var c2bDis = MathNeo.Distance(vecC, vecB);

            if (a2cDis + c2bDis == a2bDis) {
                return true;
            }
            else {
                return false;
            }
        }

        public static (Vector2, bool) GetIntersect(Vector2 A, Vector2 B, Vector2 C, Vector2 D, bool ResOnSeg) {
            var err = new Vector2(0, 0);

            var a1 = B.Y - A.Y;
            var b1 = A.X - B.X;

            var c1 = a1 * (A.X) + b1 * (A.Y);

            var a2 = D.Y - C.Y;
            var b2 = C.X - D.X;

            var c2 = a2 * (C.X) + b2 * (C.Y);

            var det = a1 * b2 - a2 * b1;

            if (det != 0) {
                var rX = (b2 * c1 - b1 * c2) / det;
                var rY = (a1 * c2 - a2 * c1) / det;

                var retRes = new Vector2(rX, rY);


                if (MathNeo.Vector2OnLineSegment(A, B, retRes) && MathNeo.Vector2OnLineSegment(C, D, retRes)) {
                    return (retRes, true);
                }
                else {
                    if (ResOnSeg) {
                        return (err, false);
                    }
                    else {
                        return (retRes, true);
                    }
                }

            }
            else {
                return (err, false);
            }

        }
        public static int Clamp(int value, int min, int max) {  
            return (value < min) ? min : (value > max) ? max : value;  
        }

        public static double ClampDouble(double value, double min, double max) {  
            return (value < min) ? min : (value > max) ? max : value;  
        }

        public static double GetAngleFromHorizontalVector2(Vector2 v0, Vector2 v1, bool degrees) {
            var slope = MathNeo.Slope(v0, v1);

            return GetAngleFromHorizontal(slope, degrees);
        }

        public static double GetAngleFromHorizontal(double slope, bool degrees) {
            return Math.Abs(Math.Atan(slope)) * (degrees ? 180/Math.PI : 1);
        }
    }