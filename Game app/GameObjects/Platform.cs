using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class Platform : GameObject
    {
        public Platform(Image img, int x, int y, int width, int height)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = width;
            Sprite.Height = height;
            X = x;
            Y = y;
        }
    }
}

