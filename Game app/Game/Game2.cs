using Game_app.GameObjects;
using System.Windows.Forms;

namespace Game_app.Game
{
    
    internal class Game2 : BaseGame
    {
        public Game2(Form form) : base(form)
        {
            
        }

        protected override void CreateGate()
        {
           
            gate = new Gate(Game_app.Properties.Resources.Gate, 545, 30);
            form.Controls.Add(gate.Sprite);
        }

        protected override void CreatePlatforms()
        {
            
            AddPlatform(250, 390, 120, 20);
            AddPlatform(150, 310, 120, 20);
            AddPlatform(320, 230, 120, 20);
            AddPlatform(550, 280, 120, 20);
            AddPlatform(720, 200, 120, 20);
            AddPlatform(540, 125, 120, 20);
        }

        protected override void CreateZombies()
        {
            
            AddZombies(150, 250, 150, 270);
            AddZombies(320, 170, 320, 440);
            AddZombies(550, 220, 550, 670);
        }
    }
}

