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

namespace Game_app
{

    public partial class Level1 : Form
    {
        PictureBox pbPlayer;
        PictureBox Gate;
        PictureBox Enemy;
        List<PictureBox> Platforms = new List<PictureBox>();
        List<PictureBox> EnemyFire = new List<PictureBox>();

        int groundLevel = 382;
        
        bool isjumping = false;
        int JumpSpeed = 0;
        int gravity = 3;

        bool facingRight = false;

        bool enemyMovingLeft = true;

        int fireTimer = 0;
        int fireInterval = 20; 

        public Level1()
        {
            InitializeComponent();
            
            Createplayer();
            CreateGate();
            CreatePlatforms();
            CreateEnemy();
          
            gameLoop.Start();
           
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            
            if (pbPlayer == null) return;

            PlayerMovement();
            EnemyMovement();
            CreateEnemyFire();
            EnemyfireMovement();
       

            if(CheckGateCollision())
            {
                gameLoop.Stop();
                MessageBox.Show("Congratulations! You've reached the gate!");
            }
            
 
        }


        // Player creation and movement
        private void Createplayer()
        {
            pbPlayer = new PictureBox();
            pbPlayer.Image = Game_app.Properties.Resources.Character;
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

        // Enemy creation and movement

        private void CreateEnemy()
        {
            Enemy = new PictureBox();
            Image img = Game_app.Properties.Resources.Enemy;
            Enemy.Image = img;
            Enemy.SizeMode = PictureBoxSizeMode.StretchImage;
            Enemy.BackColor = Color.Transparent;
            Enemy.Width = 120;
            Enemy.Height = 100;
            Enemy.Left = 400;
            Enemy.Top = 0 ;
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
            Image img = Game_app.Properties.Resources.Fire;
            enemyfire.Image = img;
            enemyfire.SizeMode = PictureBoxSizeMode.StretchImage;
            enemyfire.BackColor = Color.Transparent;
            enemyfire.Width = 70;
            enemyfire.Height = 50;
            enemyfire.Left = Enemy.Left + (Enemy.Width / 2) - (enemyfire.Width / 2);
            enemyfire.Top = Enemy.Top + Enemy.Height;
            this.Controls.Add(enemyfire);
            enemyfire.BringToFront();
            EnemyFire.Add(enemyfire);
        }

        public void EnemyfireMovement()
        {
            foreach (PictureBox fire in EnemyFire)
            {
                fire.Top += 7;
                if (fire.Top > this.ClientSize.Height)
                {
                    this.Controls.Remove(fire);
                    EnemyFire.Remove(fire);
                    break;
                }
                if (CheckFireCollision(fire))
                {
                    gameLoop.Stop();
                    MessageBox.Show("Game Over! You were hit by the enemy's fire.");
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
            platform.Image = Game_app.Properties.Resources.Platform;
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
            Image img = Game_app.Properties.Resources.Gate;
            Gate.Image = img;
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
   

    }
}


