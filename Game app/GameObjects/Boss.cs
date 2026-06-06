using Game_app.Enums;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class Boss : GameObject
    {
        public int Health { get; set; } = 100;
        private Direction horizontalDir = Direction.Right;
        private Direction verticalDir = Direction.Down;
        private int speedX = 4;
        private int speedY = 3;

        public Boss(Image img, int x, int y)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = 200;
            Sprite.Height = 80;
            X = x;
            Y = y;
        }

      
         public void Move(int clientWidth, int clientHeight)
        {
            if (horizontalDir == Direction.Right)
            {
                X += speedX;
                if (X + Sprite.Width >= clientWidth) horizontalDir = Direction.Left;
            }
            else
            {
                X -= speedX;
                if (X <= 0) horizontalDir = Direction.Right;
            }

            if (verticalDir == Direction.Down)
            {
                Y += speedY;
                if (Y + Sprite.Height >= clientHeight) verticalDir = Direction.Up;
            }
            else
            {
                Y -= speedY;
                if (Y <= 0) verticalDir = Direction.Down;
            }
        }

        public List<enemyFire> FireAllDirections(Image img)
        {
            var fires = new List<enemyFire>();

            int x = X + Sprite.Width / 2 - 15;
            int y = Y + Sprite.Height / 2 - 15;

            var directions = new (int dx, int dy)[]
            {
                     (0,  6),    // down
                     (-6, 0),    // left
                     (6,  0),    // right
                     (-4, 4),    // diagonal down-left
                     (4,  4),    // diagonal down-right
            };

            foreach (var (dx, dy) in directions)
            {
                enemyFire proj = new enemyFire(img, x, y, 0); 
                proj.VelocityX = dx;
                proj.VelocityY = dy;
                fires.Add(proj);
            }

            return fires;
        }
    }
}
