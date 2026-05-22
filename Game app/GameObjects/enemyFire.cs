using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class enemyFire : GameObject
    {
        private int speed;
        

        public enemyFire(Image img, int x, int y, int speed)
        {
            this.speed = speed;
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.AutoSize;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = img.Width;
            Sprite.Height = img.Height;
            X = x;
            Y = y;
        }

        public void ShipMove()
        {
            Y += speed;
        }
        public void ZombieMove()
        {
            X += speed;
        }

    }
}
    

