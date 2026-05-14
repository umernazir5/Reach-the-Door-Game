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
        int groundLevel = 380;
        List<PictureBox> Platforms = new List<PictureBox>();
        bool isjumping = false;
        int JumpSpeed = 0;
        int gravity = 3;

        public Level1()
        {
            InitializeComponent();
            createplayer();
            createPlatforms();
            gameLoop.Start();
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            this.Validate();
            if (pbPlayer == null) return;


            if (Keyboard.IsKeyPressed(Key.LeftArrow))
                pbPlayer.Left -= 10;

            if (Keyboard.IsKeyPressed(Key.RightArrow))
                pbPlayer.Left += 10;

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
                bool feetOnPlatform = (playerFeet >= platformTop) && (playerFeet <= platformTop + 15);

                int margin = 45;
                bool horizontalOverlap = (pbPlayer.Right - margin > platform.Left) && (pbPlayer.Left + margin < platform.Right);

                if (horizontalOverlap && feetOnPlatform && JumpSpeed > 0)
                {
                    pbPlayer.Top = platformTop - pbPlayer.Height;
                    return true;
                }
            }
            return false;
        }

        private void createplayer()
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

        private void createPlatforms()
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
    }
}


