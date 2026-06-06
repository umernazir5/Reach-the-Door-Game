using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    
    internal class enemyFire : Fire
    {
        
        public int VelocityX { get; set; } = 0;
        public int VelocityY { get; set; } = 0;

        public enemyFire(Image img, int x, int y, int speed) : base(img, x, y, speed, img.Width, img.Height)
        {
           
            Sprite.SizeMode = PictureBoxSizeMode.AutoSize;
        }

       
        public override void Move()
        {
            Y += speed;
        }

        /// <summary>
        /// Directional movement used by the boss level.
        /// </summary>
        public void DirectionalMove()
        {
            X += VelocityX;
            Y += VelocityY;
        }

        // Out-of-bounds for downward fire only needs the bottom edge,
        // but the base class default (all four edges) is fine for boss fire too.
    }
}
