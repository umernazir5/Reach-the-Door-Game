using Game_app.GameObjects;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System;

namespace Game_app.Game
{
    internal class Game3 : Game2
    {
        int zombieFireTimer = 0;
        int zombieFireInterval = 30;
        protected List<ZombieFire> zombieFire;
        protected Random rand;

        public Game3(Form form) : base(form)
        {
            zombieFire = new List<ZombieFire>();
            rand = new Random();
        }

        public override void Update()
        {
            base.Update();

            SpawnZombieFire();
            UpdateZombieFire();
        }

        protected override void DetectCollisions()
        {

            base.DetectCollisions();
            DetectZombieFireCollisions();

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
        protected void SpawnZombieFire()
        {
            foreach (Zombie zombie in zombies)
            {

                if (rand.Next(0, 100) < 2)
                {
                    ZombieFire proj = zombie.Fire(Game_app.Properties.Resources.enemyFire);

                    form.Controls.Add(proj.Sprite);
                    proj.Sprite.BringToFront();

                    foreach (Zombie zombiess in zombies)
                    {
                        zombiess.Sprite.BringToFront();
                    }
                    zombieFire.Add(proj);
                }
            }
        }

        protected void RemoveZombieFire(ZombieFire proj)
        {
            form.Controls.Remove(proj.Sprite);
            proj.Sprite.Dispose();
            zombieFire.Remove(proj);
        }

        protected void UpdateZombieFire()
        {
            for (int i = zombieFire.Count - 1; i >= 0; i--)
            {
                ZombieFire proj = zombieFire[i];

                proj.Move();

                if (proj.Y > form.ClientSize.Height || proj.Y < 0 || proj.X < 0 || proj.X > form.ClientSize.Width)
                {
                    RemoveZombieFire(proj);
                }
            }
        }
        protected void DetectZombieFireCollisions()
        {
            foreach (ZombieFire proj in zombieFire)
            {
                if (collisionManager.CheckZombieFireHitsPlayer(proj, player))
                {
                    HandlePlayerDamage();
                    RemoveZombieFire(proj);
                    break;
                }
            }
        }
    }
}