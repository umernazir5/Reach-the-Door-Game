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
            GameOver gameover = new GameOver();
            gameover.FormClosed += (s, args) => Application.Exit();
            gameover.Show();
            this.Hide();
        }

        private void HandleGameWin()
        {
            gameLoop.Stop();

            Level3 level3 = new Level3();
            NextLevel transition = new NextLevel(level3);
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
