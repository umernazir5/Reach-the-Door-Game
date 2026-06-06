using Game_app.Enums;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class Enemy : GameObject
    {
        private Direction direction = Direction.Left;

        public Enemy(Image img, int x, int y)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = 120;
            Sprite.Height = 100;
            X = x;
            Y = y;
        }

        public void Move()
        {
            if (direction == Direction.Left)
            {
                X -= 8;
                if (X <= 0)
                    direction = Direction.Right;
            }
            else
            {
                X += 8;
                if (X + Sprite.Width >= 1262) 
                    direction = Direction.Left;
            }
        }

        public void CheckBoundary(int formWidth)
        {
            if (X + Sprite.Width >= formWidth)
                direction = Direction.Left;

            if (X <= 0)
                direction = Direction.Right;
        }

        public enemyFire Fire(Image fireImg)
        {
            int fireWidth = 70;
            int fireHeight = 50;
            int left = X + (Sprite.Width / 2) - (fireWidth / 2);
            int top = Y + Sprite.Height - fireHeight + 3;
            return new enemyFire(fireImg, left, top, 7);
        }
    }
}

