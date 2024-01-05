using System;
using System.Windows.Forms;
using System.Drawing;


namespace ThreeDee;

public class Screen {
    public int WidthX { get; set; }
    public int HeightY { get; set; }

    public int PixelSize { get; set; }

    public Label[,] ScreenPixels { get; }

    public Screen(int widX, int heiX, int pixSize) {
        WidthX = widX;
        HeightY = heiX;

        PixelSize = pixSize;

        ScreenPixels = new Label[HeightY, WidthX];
    }

    public Label GetPixel(int y, int x) {
        return ScreenPixels[y, x];
    }

    public int Render(Form form) {
            var flip = 0;

        for (var y = 0; y < HeightY; y++) {
            for (var x = 0; x < WidthX; x++) {
                var lbl = new Label();

                if (flip < (WidthX * HeightY) / 2) {
                    lbl.BackColor = Color.FromArgb(125, 125, 125);
                    
                }
                else {
                    lbl.BackColor = Color.FromArgb(200, 200, 200);
                }

                flip++;

                // if (flip == 0) {
                //     lbl.BackColor = Color.FromArgb(150, 150, 150); 
                //     flip = 1;
                // }
                // else if (flip == 1) {
                //     lbl.BackColor = Color.FromArgb(175, 175, 175); 
                //     flip = 2;
                // }
                // else if (flip == 2) {
                //     lbl.BackColor = Color.FromArgb(200, 200, 200); 
                //     flip = 0;
                // }

                // lbl.BackColor = Color.FromArgb(175, 175, 175);

                lbl.Size = new Size(PixelSize, PixelSize);
                lbl.Location = new Point(x * PixelSize, y * PixelSize);

                ScreenPixels[y, x] = lbl;

                form.Invoke((MethodInvoker)delegate {
                    form.Controls.Add(lbl);
                });
            }
        }

        return 0;
    }

    public int Refresh(Form form) {
        var flip = 0;

        for (var y = 0; y < HeightY; y++) {
            for (var x = 0; x < WidthX; x++) {
                var lbl = this.GetPixel(y, x);

                if (flip < 1250) {
                    lbl.BackColor = Color.FromArgb(125, 125, 125);
                    
                }
                else {
                    lbl.BackColor = Color.FromArgb(200, 200, 200);
                }

                flip++;
            }
        }

        return 0;
    }

    public static int[] ScreenPosToArrayPos(int x, int y) {
        var screenX = ((x + 5) / 10) * 10;
        var screenY = ((x + 5) / 10) * 10;

        return new int[] { screenY / 10, screenX / 10 };
    }
}