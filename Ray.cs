using System;
using System.Windows.Forms;
using System.Drawing;

namespace ThreeDee;

public class Ray {
    public Vector2 Origin { get; set; }
    public double Magnitude { get; set; }
    public double Direction { get; set; } 

    public Vector2 End { get; }
    public double Slope { get; }

    public Ray(Vector2 origin, double magnitude, double dir) {
        Origin = origin;
        Magnitude = magnitude;
        Direction = dir;

        End = new Vector2(Origin.X + Magnitude * Math.Cos(Direction * Math.PI / 180), Origin.Y + Magnitude * Math.Sin(Direction * Math.PI / 180));
        Slope = MathNeo.Slope(Origin, End);
    }

    public (Wall, Vector2, double, bool) Fire(Screen screen) {
        var err = -0d;
        var errVec = new Vector2(err, err);
        var errWall = new Wall(0, 0, 0, errVec, errVec, Color.FromArgb(255, 255, 255));

        foreach (var wall in Global.World) {
            var (intersectWithRay, succ) = MathNeo.GetIntersect(wall.Start, wall.End, this.Origin, this.End, true);

            if (succ) {
                return (wall, intersectWithRay, MathNeo.Distance(this.Origin, intersectWithRay), true);
            }
        }

        return (errWall, errVec, err, false);
    }
}