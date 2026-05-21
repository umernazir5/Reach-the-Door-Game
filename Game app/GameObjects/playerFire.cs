using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class playerFire : GameObject
    {
        private int speed;
        
        bool playerMovingRight = true;

        public playerFire(Image img, int x, int y, int speed)
        {
            this.speed = speed;
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = 30;
            Sprite.Height = 30;
            X = x;
            Y = y;
        }
    
        public void Move(Player player)
        {
            if (player.facingRight)
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


