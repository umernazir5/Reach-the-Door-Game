using Game_app.Enums;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class Zombie : GameObject
    {
        public int Health = 2;
        private Direction direction = Direction.Left;

        public Zombie(Image img, int x, int y)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = img.Width;
            Sprite.Height = img.Height;
            X = x;
            Y = y;
        }

        public void Move()
        {
            if (direction == Direction.Left)
            {
                X -= 4;
                if (X <= 0) direction = Direction.Right;
            }
            else
            {
                X += 4;
            }
        }

        public void CheckBoundary(int left , int right)
        {
            if (X + Sprite.Width >= right) 
                direction = Direction.Left;
            if (X <= left) 
                direction = Direction.Right;
        }

        public void TakeHit()
        {
            Health--;
            if (Health <= 0) Destroy();
        }
    }
}
