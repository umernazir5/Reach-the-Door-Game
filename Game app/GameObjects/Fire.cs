using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    
    internal abstract class Fire : GameObject
    {
   
        protected int speed;

        protected Fire(Image img, int x, int y, int speed, int width, int height)
        {
            this.speed = speed;

            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = width;
            Sprite.Height = height;
            X = x;
            Y = y;
        }

      
        public abstract void Move();

      
        public virtual bool HitsPlayer(Player player)
        {
            Rectangle fireHitbox = new Rectangle(
                X + 30,
                Y + 20,
                Sprite.Width - 50,
                Sprite.Height - 15
            );

            Rectangle playerHitbox = new Rectangle(
                player.X + 30,
                player.Y + 15,
                player.Sprite.Width - 50,
                player.Sprite.Height - 20
            );

            return playerHitbox.IntersectsWith(fireHitbox);
        }

     
        public virtual bool IsOutOfBounds(int formWidth, int formHeight)
        {
            return X > formWidth || X < 0 || Y > formHeight || Y < 0;
        }
    }
}
