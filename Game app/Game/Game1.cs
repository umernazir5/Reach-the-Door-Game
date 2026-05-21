using Game_app.GameObjects;
using System.Windows.Forms;

namespace Game_app.Game
{
    internal class Game1 : BaseGame // Change "Game1" to "Game" if that matches your form instantiation
    {
        public Game1(Form form) : base(form)
        {
            // The base(form) passes the window to BaseGame so it can run the setup
        }

        protected override void CreateGate()
        {
            // Level 1 specific gate placement
            gate = new Gate(Game_app.Properties.Resources.Gate, 823, 45);
            form.Controls.Add(gate.Sprite);
        }

        protected override void CreatePlatforms()
        {
            // Level 1 specific staircase layout
            AddPlatform(0, 420, 100, 20);
            AddPlatform(200, 350, 100, 20);
            AddPlatform(400, 270, 100, 20);
            AddPlatform(600, 190, 100, 20);
            AddPlatform(830, 140, 100, 20);
        }
    }
}
