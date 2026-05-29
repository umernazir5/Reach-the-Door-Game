using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app
{
    public partial class Level1 : Form
    {
        Game.Game1 game;

        public Level1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            game = new Game.Game1(this);
            game.OnGameOver += HandleGameOver;
            game.OnGameWin += HandleGameWin;
            game.Start();
            // Level1's gameLoop is started by the designer (Enabled=true in InitializeComponent),
            // so no manual start is needed here.
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            game.Update();
        }

        private void HandleGameOver()
        {
            gameLoop.Stop();
            GameOver gameover = new GameOver();
            gameover.FormClosed += (s, args) => Application.Exit();
            gameover.Show();
            this.Hide();
        }

        private void HandleGameWin()
        {
            gameLoop.Stop();
            // Pass a factory lambda — Level2 is NOT constructed here.
            // It will only be created when the user clicks "Continue" in NextLevel.
            NextLevel transition = new NextLevel(() => new Level2());
            transition.FormClosed += (s, args) => Application.Exit();
            transition.Show();
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