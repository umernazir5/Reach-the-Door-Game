using System.Drawing;
using System.Windows.Forms;
using Game_app.Properties;
namespace Game_app.GameObjects
   
{
    internal class HealthBar : GameObject
    {
        public HealthBar(int formClientHeight)
        {
            Sprite = new PictureBox();
            Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = 220;
            Sprite.Height = 73;
            Sprite.Left = 16;
            Sprite.Top = formClientHeight - Sprite.Height - 8;
            Sprite.Image = Game_app.Properties.Resources.Health5;
        }

        public void UpdateDisplay(int health)
        {
            switch (health)
            {
                case 4: Sprite.Image = Game_app.Properties.Resources.Health4; break;
                case 3: Sprite.Image = Game_app.Properties.Resources.Health3; break;
                case 2: Sprite.Image = Game_app.Properties.Resources.Health2; break;
                case 1: Sprite.Image = Game_app.Properties.Resources.Health1; break;
                case 0: Sprite.Image = Game_app.Properties.Resources.Health0; break;
            }
        }
    }
}

