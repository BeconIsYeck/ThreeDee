using System;
using System.Windows.Forms;
using System.Drawing;


namespace ThreeDee;

public static class Render {
    public static void VerticalLine(Screen screen, int floorHeight, int ceilingHeight, int x, Color color) {
        var i = 1;

        for (var y = ceilingHeight; y < floorHeight; y++) {
            var lbl = screen.GetPixel(y, x);
            lbl.BackColor = color;

            i++;
        }
    }
}