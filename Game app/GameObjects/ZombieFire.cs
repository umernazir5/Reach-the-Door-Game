using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class ZombieFire : GameObject
    {
        private int speedX;

        public ZombieFire(Image img, int x, int y, int speedX)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.AutoSize;
            Sprite.BackColor = Color.Transparent;
            X = x;
            Y = y;

            this.speedX = speedX;
        }

        public void Move()
        {
            X += speedX; 
        }
    }
}