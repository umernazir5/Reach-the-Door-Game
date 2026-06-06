using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    /// <summary>
    /// Projectile fired by zombies.
    /// Moves horizontally (left or right) depending on the zombie's facing direction.
    /// Inherits damage-player collision logic from Fire base class.
    /// </summary>
    internal class ZombieFire : Fire
    {
        public ZombieFire(Image img, int x, int y, int speedX)
            : base(img, x, y, speedX, img.Width, img.Height)
        {
            Sprite.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        /// <summary>
        /// Moves horizontally. speed is positive (right) or negative (left).
        /// </summary>
        public override void Move()
        {
            X += speed;
        }
    }
}
