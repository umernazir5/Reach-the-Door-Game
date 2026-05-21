using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class Gate : GameObject
    {
        public Gate(Image img, int x, int y)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.BackColor = Color.Transparent;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.Width = 110;
            Sprite.Height = 100;
            X = x;
            Y = y;
        }
    }
}

