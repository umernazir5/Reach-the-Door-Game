using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app
{
    public partial class Level3 : Form
    {
        Game.Game3 game;

        public Level3()
        {
            InitializeComponent();

            
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

          
            game = new Game.Game3(this);
            game.OnGameOver += HandleGameOver;
            game.OnGameWin += HandleGameWin;
            game.Start();

           
            this.gameLoop.Tick += new System.EventHandler(this.gameLoop_Tick);
            this.gameLoop.Enabled = true;
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            game.Update();
        }

        private void HandleGameOver()
        {
            gameLoop.Stop();
            MessageBox.Show("Game Over!");

            // Optional: You can choose to restart the level or close the app here
            // Application.Exit(); 
        }

        private void HandleGameWin()
        {
            gameLoop.Stop();

            // Since this is Level 3, we show a final victory message instead of loading Level 2!
            MessageBox.Show("Congratulations! You've survived the zombie fire and beaten the game!");

            // Closes the entire game safely and frees up memory
            Application.Exit();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            base.OnPaint(e);
        }
    }
}