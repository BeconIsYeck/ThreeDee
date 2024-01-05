using System;


namespace ThreeDee;

public class Vector2 {
    public double X { get; set; }
    public double Y { get; set; }
    public double Length { get; }

    public Vector2(double x, double y) {
        X = x;
        Y = y;
        Length = Math.Sqrt(X * X + Y * X);
    }
}