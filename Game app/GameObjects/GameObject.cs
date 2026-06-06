using System.Drawing;
using System.Windows.Forms;

namespace Game_app.GameObjects
{
    internal class GameObject
    {
        public PictureBox Sprite { get; set; }

        public bool IsAlive { get; set; } = true;

        public int X
        {
            get { return Sprite.Left; }
            set { Sprite.Left = value; }
        }

        public int Y
        {
            get { return Sprite.Top; }
            set { Sprite.Top = value; }
        }

        public Rectangle Bounds
        {
            get { return Sprite.Bounds; }
        }

        public virtual void Destroy()
        {
            IsAlive = false;
            Sprite.Hide();
        }
    }
}

