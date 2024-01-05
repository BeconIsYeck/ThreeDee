using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;


namespace ThreeDee {
    public static class Program {  
        public static Screen GlobalScreen;
        public static Form MainForm = new Form();

        static void GlobalScreenInit() {
            GlobalScreen = new Screen(50, 50, 10);
            GlobalScreen.Render(MainForm);

            Ready();
        }

        static void Ready() {  
            int fov = 90;

            var plr = new Player(new Vector2(20, -5), 90, fov, 1);

            var testWall0 = new Wall(40, 10, 4, new Vector2(2, 2), new Vector2(10, 10), Color.FromArgb(255, 0, 0));
            var testWall1 = new Wall(40, 10, 4, new Vector2(10, 10), new Vector2(30, 10), Color.FromArgb(0, 255, 0));
            var testWall2 = new Wall(40, 10, 4, new Vector2(30, 10), new Vector2(40, 0), Color.FromArgb(0, 0, 255));

            var incAmount = 5;

            var flip = false;

            while (true) {
                plr.RenderInFOV(30, GlobalScreen, 1d);

                plr.Position.Y -= 0;

                if (flip) {
                    plr.Orientation -= incAmount;
                }
                else {
                    plr.Orientation += incAmount;
                }
               
                if (plr.Orientation >= 150) {
                    flip = true;
                }
                if (plr.Orientation <= 30) {
                    flip = false;
                }

                Thread.Sleep(400);

                GlobalScreen.Refresh(MainForm);
            }
        }

        public static void Main(string[] args) {
            var initThread = new Thread(new ThreadStart(GlobalScreenInit));
            initThread.Start();

            MainForm.Text = "Three Dee";

            void MainFormFormClosed(object sender, FormClosedEventArgs e) {
				initThread.Abort();
			}

            MainForm.FormClosed += new FormClosedEventHandler(MainFormFormClosed);

            MainForm.ShowDialog();

        }
    }
}
