using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
   
    internal class playerFire : Fire
    {
        private readonly bool isMovingRight;

        public playerFire(Image img, int x, int y, int speed, bool movingRight) : base(img, x, y, speed, 30, 30)
        {
            this.isMovingRight = movingRight;

            Sprite.Image = (Image)img.Clone();
            if (movingRight)
            {
                Sprite.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
        }

        public override void Move()
        {
            if (isMovingRight)
                X += speed;
            else
                X -= speed;
        }

    
        public override bool HitsPlayer(Player player)
        {
            return false;
        }

    }
}
