using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class Player : GameObject
    {
        public bool facingRight = false;
        public bool isjumping = false;
        public int JumpSpeed = 0;
        public int gravity = 3;
        public int groundLevel;

        public int playerHealth = 5;
        public bool isInvincible = false;
        public int invincibleTimer = 0;

        public Player(Image img, int x, int y, int groundLevel)
        {
            this.groundLevel = groundLevel;
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = 130;
            Sprite.Height = 100;
            X = x;
            Y = y;
        }
        public playerFire Fire(Image fireImg)
        {
            int fireWidth = 70;
            int fireHeight = 50;
            int left = X + (Sprite.Width / 2) - (fireWidth / 2);
            int top = Y + Sprite.Height - fireHeight + 3;
            return new playerFire(fireImg, left, top, 7);
        }

        public void MoveLeft()
        {
            if (facingRight)
            {
                Sprite.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                Sprite.Refresh();
                facingRight = false;
            }
            if (X > 0)
                X -= 8;
        }

        public void MoveRight(int formWidth)
        {
            if (!facingRight)
            {
                Sprite.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                Sprite.Refresh();
                facingRight = true;
            }
            if (X + Sprite.Width < formWidth)
                X += 8;
        }

        public void Jump()
        {
            if (!isjumping)
            {
                isjumping = true;
                JumpSpeed = -20;
            }
        }

        public void UpdatePhysics(List<Platform> platforms)
        {
            Y += JumpSpeed;
            JumpSpeed += gravity;

            if (IsLanding(platforms))
            {
                isjumping = false;
                JumpSpeed = 0;
            }

            if (Y >= groundLevel)
            {
                Y = groundLevel;
                isjumping = false;
                JumpSpeed = 0;
            }
        }

        public bool IsLanding(List<Platform> platforms)
        {
            foreach (Platform platform in platforms)
            {
                int playerFeet = Y + Sprite.Height;
                int platformTop = platform.Y + 12;

                bool verticalCross = (playerFeet >= platformTop) && (playerFeet <= platformTop + JumpSpeed + gravity + 5);
                bool comingFromAbove = Y < platform.Y;

                int margin = 45;
                bool horizontalOverlap = (Sprite.Right - margin > platform.X) && (X + margin < platform.Sprite.Right);

                if (horizontalOverlap && verticalCross && comingFromAbove && JumpSpeed > 0)
                {
                    Y = platformTop - Sprite.Height;
                    return true;
                }
            }
            return false;
        }

        public bool TakeDamage()
        {
            if (isInvincible) return false;

            playerHealth--;
            isInvincible = true;
            invincibleTimer = 40;
            return true;
        }

        public void HandleInvincibility()
        {
            if (!isInvincible) return;

            invincibleTimer--;
            Sprite.Visible = (invincibleTimer % 8) < 4;

            if (invincibleTimer <= 0)
            {
                isInvincible = false;
                Sprite.Visible = true;
            }
        }
    }
}

