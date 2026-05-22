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
       
public ZombieFire Fire(Image fireImg)
{
    int fireWidth = 70;
    int fireHeight = 50;
    
    int left = X + (Sprite.Width / 2) - (fireWidth / 2);
    int top = Y + (Sprite.Height / 2) - 20;


            int fireSpeed;

            if (direction == Game_app.Enums.Direction.Left)
            {
                fireSpeed = -15; 
            }
            else
            {
                fireSpeed = 15; 
            }

            return new ZombieFire(fireImg, left, top, fireSpeed);
}
   
    }
}