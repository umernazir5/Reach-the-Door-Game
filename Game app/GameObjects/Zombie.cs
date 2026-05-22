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

       
        public Zombie(Image img, int x, int y, int leftBound, int rightBound)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = 55;
            Sprite.Height = 60;
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
            CheckBoundary(leftBoundary, rightBoundary);
        }

        public void CheckBoundary(int left, int right)
        {
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