using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZInput;
using WMPLib;

namespace Game_app
{

    public partial class Level1 : Form
    {
        PictureBox pbPlayer;
        PictureBox Gate;
        PictureBox Enemy;
        List<PictureBox> Platforms = new List<PictureBox>();
        List<PictureBox> EnemyFire = new List<PictureBox>();


        Image playerImage;
        Image enemyImage;
        Image fireImage;
        Image platformImage;
        Image gateImage;
        Image gameoverImage;

        int groundLevel = 382;


        bool isjumping = false;
        int JumpSpeed = 0;
        int gravity = 3;


        bool facingRight = false;


        bool enemyMovingLeft = true;

        int fireTimer = 0;
        int fireInterval = 20;


        PictureBox pbHealthBar;
        int playerHealth = 5;
        bool isInvincible = false;
        int invincibleTimer = 0;

        WindowsMediaPlayer musicPlayer;

        public Level1()
        {
            InitializeComponent();

            playerImage = Game_app.Properties.Resources.Character;
            enemyImage = Game_app.Properties.Resources.Enemy;
            fireImage = Game_app.Properties.Resources.Fire;
            platformImage = Game_app.Properties.Resources.Platform;
            gateImage = Game_app.Properties.Resources.Gate;
            gameoverImage = Game_app.Properties.Resources.gameover;

           


            this.SetStyle(ControlStyles.AllPaintingInWmPaint |ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            Createplayer();
            CreateGate();
            CreatePlatforms();
            CreateEnemy();
            
            CreateHealthBar(); 
            PlayBackgroundMusic();
            gameLoop.Start();

        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {

            if (pbPlayer == null) return;

            PlayerMovement();
            EnemyMovement();
            CreateEnemyFire();
            EnemyfireMovement();
            HandleInvincibility();
            if (CheckGateCollision())
            {
                gameLoop.Stop();
                MessageBox.Show("Congratulations! You've reached the gate!");
            }

        }


        // Player creation and movement
        private void Createplayer()
        {
            pbPlayer = new PictureBox();
            pbPlayer.Image = playerImage;
            pbPlayer.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPlayer.BackColor = Color.Transparent;
            pbPlayer.Width = 120;
            pbPlayer.Height = 100;
            pbPlayer.Left = 380;
            pbPlayer.Top = 380;
            this.Controls.Add(pbPlayer);

            pbPlayer.BringToFront();
        }

        public void PlayerMovement()
        {
            if (Keyboard.IsKeyPressed(Key.LeftArrow))
            {
                if (facingRight)
                {
                    pbPlayer.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    pbPlayer.Refresh();
                    facingRight = false;
                }
                if (pbPlayer.Left > 0)
                {
                    pbPlayer.Left -= 10;
                }
            }

            if (Keyboard.IsKeyPressed(Key.RightArrow))
            {
                if (!facingRight)
                {
                    pbPlayer.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    pbPlayer.Refresh();
                    facingRight = true;
                }
                if (pbPlayer.Left + pbPlayer.Width < this.ClientSize.Width)
                {
                    pbPlayer.Left += 10;
                }
            }

            if (Keyboard.IsKeyPressed(Key.UpArrow) && !isjumping)
            {
                isjumping = true;
                JumpSpeed = -20;
            }

            pbPlayer.Top += JumpSpeed;
            JumpSpeed += gravity;


            if (isLanding())
            {
                isjumping = false;
                JumpSpeed = 0;
            }


            if (pbPlayer.Top >= groundLevel)
            {
                pbPlayer.Top = groundLevel;
                isjumping = false;
                JumpSpeed = 0;
            }
        }
        public bool isLanding()
        {
            foreach (PictureBox platform in Platforms)
            {
                int playerFeet = pbPlayer.Top + pbPlayer.Height;
                int platformTop = platform.Top + 12;

                bool verticalCross = (playerFeet >= platformTop) && (playerFeet <= platformTop + JumpSpeed + gravity + 5);
                bool comingFromAbove = pbPlayer.Top < platform.Top;

                int margin = 45;
                bool horizontalOverlap = (pbPlayer.Right - margin > platform.Left) && (pbPlayer.Left + margin < platform.Right);

                if (horizontalOverlap && verticalCross && comingFromAbove && JumpSpeed > 0)
                {
                    pbPlayer.Top = platformTop - pbPlayer.Height;
                    return true;
                }
            }
            return false;
        }

        //player health system

        private void CreateHealthBar()
        {
            pbHealthBar = new PictureBox();
            pbHealthBar.SizeMode = PictureBoxSizeMode.StretchImage;
            pbHealthBar.BackColor = Color.Transparent;
            pbHealthBar.Width = 220;
            pbHealthBar.Height = 73;
            pbHealthBar.Left = 16;
            pbHealthBar.Top = this.ClientSize.Height - pbHealthBar.Height - 8;
            pbHealthBar.Image = Game_app.Properties.Resources.Health5; 
            this.Controls.Add(pbHealthBar);
            pbHealthBar.BringToFront();
        }

        private void TakeDamage()
        {
            if (isInvincible) return;

            playerHealth--;

            if (playerHealth == 4) pbHealthBar.Image = Game_app.Properties.Resources.Health4;
            if (playerHealth == 3) pbHealthBar.Image = Game_app.Properties.Resources.Health3;
            if (playerHealth == 2) pbHealthBar.Image = Game_app.Properties.Resources.Health2;
            if (playerHealth == 1) pbHealthBar.Image = Game_app.Properties.Resources.Health1;
            if (playerHealth <= 0)
            {
                pbHealthBar.Image = Game_app.Properties.Resources.Health0;
                CreateGameoverAnimation();
                musicPlayer.controls.stop();
                FahMusic();
                gameLoop.Stop();
                MessageBox.Show("Game Over!");
                return;
            }

            isInvincible = true;
            invincibleTimer = 40;
        }

        private void HandleInvincibility()
        {
            if (!isInvincible) 
                return;

            invincibleTimer--;
            pbPlayer.Visible = (invincibleTimer % 8) < 4; 

            if (invincibleTimer <= 0)
            {
                isInvincible = false;
                pbPlayer.Visible = true;
            }
        }

        // Enemy creation and movement

        private void CreateEnemy()
        {
            Enemy = new PictureBox();
            Enemy.Image = enemyImage;
            Enemy.SizeMode = PictureBoxSizeMode.StretchImage;
            Enemy.BackColor = Color.Transparent;
            Enemy.Width = 120;
            Enemy.Height = 100;
            Enemy.Left = 400;
            Enemy.Top = 0;
            this.Controls.Add(Enemy);
            Enemy.BringToFront();
        }

        public void EnemyMovement()
        {
            if (enemyMovingLeft)
            {
                Enemy.Left -= 8;

                if (Enemy.Left <= 0)
                {
                    enemyMovingLeft = false;
                }
            }
            else if (!enemyMovingLeft)
            {
                Enemy.Left += 8;

                if (Enemy.Left + Enemy.Width >= this.ClientSize.Width)
                {
                    enemyMovingLeft = true;
                }
            }
        }

        //Enemy fire creation and movement

        public void CreateEnemyFire()
        {
            fireTimer++;
            if (fireTimer < fireInterval) return;
            fireTimer = 0;

            PictureBox enemyfire = new PictureBox();
            
            enemyfire.Image = fireImage;
            enemyfire.SizeMode = PictureBoxSizeMode.StretchImage;
            enemyfire.BackColor = Color.Transparent;
            enemyfire.Width = 70;
            enemyfire.Height = 50;
            enemyfire.Left = Enemy.Left + (Enemy.Width / 2) - (enemyfire.Width / 2);
            enemyfire.Top = Enemy.Top + Enemy.Height - enemyfire.Height + 3;
            this.Controls.Add(enemyfire);
            enemyfire.BringToFront();
            EnemyFire.Add(enemyfire);
        }

        public void EnemyfireMovement()
        {
            for (int i = EnemyFire.Count - 1; i >= 0; i--)
            {
                var fire = EnemyFire[i];
                fire.Top += 7;

                if (fire.Top > this.ClientSize.Height)
                {
                    this.Controls.Remove(fire);
                    EnemyFire.RemoveAt(i);
                    fire.Dispose(); 
                    continue;
                }

                if (CheckFireCollision(fire))
                {
                    this.Controls.Remove(fire);
                    EnemyFire.RemoveAt(i);
                    fire.Dispose();
                    TakeDamage();
                    break;
                }
            }
        }
        private bool CheckFireCollision(PictureBox fire)
        {
            Rectangle fireHitbox = new Rectangle(
                fire.Left + 20,
                fire.Top + 20,
                fire.Width - 50,
                fire.Height - 15
            );


            Rectangle playerHitbox = new Rectangle(
                pbPlayer.Left + 35,
                pbPlayer.Top + 15,
                pbPlayer.Width - 70,
                pbPlayer.Height - 20
            );

            return playerHitbox.IntersectsWith(fireHitbox);
        }


        // Platform creation
        private void CreatePlatforms()
        {
            AddPlatform(0, 420, 100, 20);
            AddPlatform(200, 350, 100, 20);
            AddPlatform(400, 270, 100, 20);
            AddPlatform(600, 190, 100, 20);
            AddPlatform(830, 140, 100, 20);
        }

        private void AddPlatform(int left, int top, int width, int height)
        {
            PictureBox platform = new PictureBox();
            platform.Image = platformImage;
            platform.SizeMode = PictureBoxSizeMode.StretchImage;
            platform.BackColor = Color.Transparent;
            platform.Left = left;
            platform.Top = top;
            platform.Width = width;
            platform.Height = height;
            this.Controls.Add(platform);
            Platforms.Add(platform);
            platform.BringToFront();

        }

        // Gate creation and collision detection

        private void CreateGate()
        {
            Gate = new PictureBox();
            Gate.Image = gateImage;
            Gate.BackColor = Color.Transparent;
            Gate.SizeMode = PictureBoxSizeMode.StretchImage;
            Gate.Width = 110;
            Gate.Height = 100;
            Gate.Left = 823;
            Gate.Top = 55;
            this.Controls.Add(Gate);
            Gate.BringToFront();
        }
        private bool CheckGateCollision()
        {

            int marginLeft = 70;
            int marginTop = 15;
            int marginRight = 10;
            int marginBottom = 10;

            Rectangle orbHitbox = new Rectangle(
                Gate.Left + marginLeft,
                Gate.Top + marginTop,
                Gate.Width - marginLeft - marginRight,
                Gate.Height - marginTop - marginBottom
            );

            return pbPlayer.Bounds.IntersectsWith(orbHitbox);
        }

        // gameover animation

        private void CreateGameoverAnimation()
        {
            PictureBox gameover = new PictureBox();
            gameover.Image = gameoverImage;
            gameover.SizeMode = PictureBoxSizeMode.StretchImage;
            gameover.BackColor = Color.Transparent;
            gameover.Width = pbPlayer.Width;
            gameover.Height = pbPlayer.Height;
            gameover.Left = pbPlayer.Left;
            gameover.Top = pbPlayer.Top;
            this.Controls.Add(gameover);
            gameover.BringToFront();
        }



        // Music control

        private void PlayBackgroundMusic()
        {
            musicPlayer = new WindowsMediaPlayer();
            musicPlayer.URL = Application.StartupPath + @"\Main_Theme.mp3";
            musicPlayer.settings.setMode("loop", true);
            musicPlayer.controls.play();
        }

        private void FahMusic()
        {
            musicPlayer = new WindowsMediaPlayer();
            musicPlayer.URL = Application.StartupPath + @"\Fahh.mp3";
            musicPlayer.controls.play();
        }

        // function or making playback better a bit
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            base.OnPaint(e);
        }

    }
}


