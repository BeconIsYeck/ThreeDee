using System;
using System.Drawing;

using ThreeDee;

public class Wall {
    public int FloorHeight { get; set; }
    public int CeilingHeight { get; set; }
    public int Height { get; set; }
    public Vector2 Start { get; set; }
    public Vector2 End { get; set; }
    public Color Color { get; set; }

    public Wall(int floorHeight, int ceilingHeight, int height, Vector2 start, Vector2 end, Color color) {
        FloorHeight = floorHeight;
        CeilingHeight = ceilingHeight;
        Height = height;
        Start = start;
        End = end;
        Color = color;

        Global.World.Add(this);
    }

    public void Render(Screen screen, Player plr) {
        for (var i = Start.X; i < End.X; i++) {
            var slope = (End.Y - Start.Y) / (End.X - Start.Y);

            var zFH = FloorHeight - (i * slope);
            var zCH = CeilingHeight + (i * slope);

            double dis = MathNeo.Distance(Start, plr.Position);

            ThreeDee.Render.VerticalLine(screen, (int)zFH, (int)zCH, (int)i, Color);
        }
    }

}