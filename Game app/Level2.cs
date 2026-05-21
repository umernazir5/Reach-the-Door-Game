using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app
{
    public partial class Level2 : Form
    {
        Game.Game2 game;

        public Level2()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            game = new Game.Game2(this);
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
        }

        private void HandleGameWin()
        {
            gameLoop.Stop();
            MessageBox.Show("Congratulations! You've reached the gate!");
            Level2 level2 = new Level2();
            level2.Show();
            this.Hide();
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
