using Game_app.Enums;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class Zombie : GameObject
    {
        public int Health = 2;
        private Direction direction = Direction.Left;

        public int leftBoundary;
        public int rightBoundary;

        // NEW: Constructor now takes boundary limits
        public Zombie(Image img, int x, int y, int leftBound, int rightBound)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = 110;
            Sprite.Height = 100;
            X = x;
            Y = y;
            leftBoundary = leftBound;
            rightBoundary = rightBound;
        }

        public void Move()
        {
            if (direction == Direction.Left)
            {
                X -= 4;
            }
            else
            {
                X += 4;
            }

            // Check boundaries every time it moves
            CheckBoundary(leftBoundary, rightBoundary);
        }

        public void CheckBoundary(int left, int right)
        {
            // NEW: Use the center of the zombie so it doesn't glitch on small platforms
            int center = X + (Sprite.Width / 2);

            if (center >= right)
                direction = Direction.Left;
            if (center <= left)
                direction = Direction.Right;
        }

        public void TakeHit()
        {
            Health--;
            if (Health <= 0) Destroy();
        }
    }
}