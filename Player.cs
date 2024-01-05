using System;
using System.Windows.Forms;
using System.Drawing;


namespace ThreeDee;

public class Player {
    public Vector2 Position { get; set; }
    public double Orientation { get; set; }
    public double FOV { get; set; }
    public double Speed { get; set; }

    public Player(Vector2 pos, double orient, double fov, double spd) {
        Position = pos;
        Orientation = orient;
        FOV = fov;
        Speed = spd;
    }

    public void RenderInFOV(double viewLen, Screen screen, double dampening) {
        var orRad = this.Orientation * (Math.PI / 180);
        var fovRad = this.FOV * (Math.PI / 180);

        var oAng = (this.FOV / 2);
        var cAng = -(this.FOV / 2);

        // var fovMin = new Ray(this.Position, viewLen, this.Orientation + oAng);
        // var fovMax = new Ray(this.Position, viewLen, this.Orientation + cAng);

        var n = 0d;

        Console.WriteLine("-----New-----");

        for (var i = 0d; i <= (double)this.FOV; i += 1) {

            var nAng = oAng - i;

            var pixelRay = new Ray(this.Position, viewLen, this.Orientation + (this.FOV / 2) - i);

            var (hitWall, rayWallIntersect, dis, succ) = pixelRay.Fire(screen);

            var tricorn = new Vector2(this.Position.X, rayWallIntersect.Y);

            var disfromtricorn = MathNeo.Distance(tricorn, this.Position);

            var q = new Vector2(
                (rayWallIntersect.X + 100) * Math.Cos(orRad + (90 * (Math.PI / 180))),
                (rayWallIntersect.Y + 100) * Math.Sin(orRad + (90 * (Math.PI / 180)))
            );

            var lookEndP = new Vector2(
                this.Position.X + 100 * Math.Cos(orRad),
                this.Position.Y + 100 * Math.Sin(orRad)
            );

            var (corner, good) = MathNeo.GetIntersect(this.Position, lookEndP, rayWallIntersect, q, false);

            var dampenedDis = MathNeo.Distance(rayWallIntersect, corner);

            var lookVector = new Ray(this.Position, viewLen, this.Orientation);
            var (lVHit, lVInter, lVDis, lVSucc) = lookVector.Fire(screen);

            var dis2 = MathNeo.Distance(lVInter, pixelRay.End);
            var dampenedDis2 = dis2 * 0.1;

            var beta = this.Orientation;
            var theta = this.Orientation + oAng;

            var p = dis * Math.Cos(theta - beta);

            p = Math.Abs(p);

            var z = dis * Math.Cos(Math.Abs(this.Orientation - pixelRay.Direction));
            var zAbs = Math.Abs(z);

            var h = (screen.HeightY * hitWall.Height) / z;
            var hAbs = Math.Abs(h);
            var hAbsDampened = hAbs * dampening; 

            var pRay = new Ray(this.Position, p, this.Orientation);

            var dis3 = MathNeo.Distance(pRay.End, pixelRay.End);

            var newColor = Color.FromArgb(
                MathNeo.Clamp(hitWall.Color.R - (int)dampenedDis * 10, 0, 255),
                MathNeo.Clamp(hitWall.Color.G - (int)dampenedDis * 10, 0, 255),
                MathNeo.Clamp(hitWall.Color.B - (int)dampenedDis * 10, 0, 255)
            );

            var height = disfromtricorn;

            if (succ)
                Console.WriteLine($"{height}");

            var halfwayPoint = screen.HeightY / 2;

            var floor = 50 - height;
            var ceiling = 0 + height;

            if (succ) {
                Render.VerticalLine(screen,
                (int)MathNeo.ClampDouble(floor, 0, screen.HeightY),
                (int)MathNeo.ClampDouble(ceiling, 0 , screen.HeightY),
                (int)MathNeo.ClampDouble(n, 0, screen.WidthX - 1),
                // hitWall.Color);
                newColor);
            }
        
            n += screen.WidthX / this.FOV;
        }
    }
}