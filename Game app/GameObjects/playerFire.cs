using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class playerFire : GameObject
    {
        private int speed;

        private bool isMovingRight;


        public playerFire(Image img, int x, int y, int speed, bool movingRight)
        {
            this.speed = speed;
            this.isMovingRight = movingRight; 

            Sprite = new PictureBox();

            
            Sprite.Image = (Image)img.Clone();

            if (movingRight)
            {
                Sprite.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }

            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = 30;
            Sprite.Height = 30;
            X = x;
            Y = y;
        }


        public void Move()
        {
            
            if (isMovingRight)
            {
                X += speed;
            }
            else
            {
                X -= speed;
            }
        }
    }
}

    


