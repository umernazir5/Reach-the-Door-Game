using System.Drawing;
using System.Windows.Forms;
using Game_app.Enums;

namespace Game_app.GameObjects
{
    internal class Boss : GameObject
    {
        public int Health { get; set; } = 100; 
        private Direction direction = Direction.Right;

        public Boss(Image img, int x, int y)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = 200;
            Sprite.Height = 150;
            X = x;
            Y = y;
        }

        public void Move()
        {
            // Boss patrols the top area
            if (direction == Direction.Right)
            {
                X += 6;
                if (X > 700) direction = Direction.Left;
            }
            else
            {
                X -= 6;
                if (X < 50) direction = Direction.Right;
            }
        }
    }
}
