using Game_app.GameObjects;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Game_app.Game
{
    internal class Game3 : Game2
    {
        
        protected List<enemyFire> zombieFire;

        public Game3(Form form) : base(form)
        {
             zombieFire = new List<enemyFire>();
        }

        public override void Update()
        {
            base.Update(); 

            
        }

        protected override void DetectCollisions()
        {
            base.DetectCollisions(); 

            
        }

        protected override void CreateGate()
        {
            gate = new Gate(Game_app.Properties.Resources.Gate, 825, 45);
            gate.Sprite.Visible = false;
            form.Controls.Add(gate.Sprite);
        }

        protected override void CreatePlatforms()
        {
          AddPlatform(5, 400, 120, 20);
          AddPlatform(5, 320, 120, 20);
          AddPlatform(5, 240, 120, 20);
          AddPlatform(5, 160, 120, 20);

          AddPlatform(820, 400, 120, 20);
          AddPlatform(820, 320, 120, 20);
          AddPlatform(820, 240, 120, 20);
          AddPlatform(820, 160, 120, 20);

          AddPlatform(400, 400, 120, 20);
          AddPlatform(500, 320, 120, 20);
          AddPlatform(300, 240, 120, 20);
          AddPlatform(500, 160, 120, 20);

        }

        protected override void CreateZombies()
        {
            AddZombies(5, 340, 30, 120);
            AddZombies(5, 260, 30, 120);
            AddZombies(5, 180, 30, 120);
            AddZombies(5, 100, 30, 120);

            AddZombiesFlip(820, 340, 820, 920);
            AddZombiesFlip(820, 260, 820, 920);
            AddZombiesFlip(820, 180, 820, 920);
            AddZombiesFlip(820, 100, 820, 920);
        }
    }
}