using Game_app.GameObjects;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app.Game
{
  
    internal class Game2 : Game1
    {
        protected List<Zombie> zombies;

        public Game2(Form form) : base(form)
        {
            zombies = new List<Zombie>();
        }

        public override void Start()
        {
            base.Start();
            CreateZombies();
            foreach (Zombie zombie in zombies) zombie.Sprite.BringToFront();
        }

        public override void Update()
        {
            base.Update();

            if (player == null || player.playerHealth <= 0) return;

            UpdateZombies();
        }

        
        // Level 2 layout overrides
       
        protected override void CreateGate()
        {
            gate = new Gate(Game_app.Properties.Resources.Gate, 545, 30);
            gate.Sprite.Visible = false;
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

        
        // Zombie creation
      
        protected virtual void CreateZombies()
        {
            AddZombies    (150, 250, 150, 270);
            AddZombies    (320, 170, 320, 440);
            AddZombies    (550, 220, 550, 670);
        }

        protected void AddZombies(int x, int y, int leftBound, int rightBound)
        {
            Zombie zombie = new Zombie(Game_app.Properties.Resources.zombie, x, y, leftBound, rightBound);
            form.Controls.Add(zombie.Sprite);
            zombies.Add(zombie);
        }

        protected void AddZombiesFlip(int x, int y, int leftBound, int rightBound)
        {
            Zombie zombie = new Zombie(Game_app.Properties.Resources.zombie, x, y, leftBound, rightBound);
            zombie.Sprite.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            form.Controls.Add(zombie.Sprite);
            zombies.Add(zombie);
        }

        protected virtual void UpdateZombies()
        {
            foreach (Zombie zombie in zombies)
                zombie.Move();
        }

        
        // Collision detection
       
        protected override void DetectCollisions()
        {
            base.DetectCollisions();         
            CheckZombieBodyCollisions();
            CheckPlayerFireHitsZombies();
        }

       
        protected virtual void CheckZombieBodyCollisions()
        {
            foreach (Zombie zombie in zombies)
            {
                if (collisionManager.CheckPlayerHitsZombie(player, zombie))
                {
                    HandlePlayerDamage();
                    break;
                }
            }
        }

       
        protected virtual void CheckPlayerFireHitsZombies()
        {
            for (int i = allFire.Count - 1; i >= 0; i--)
            {
               
                if (!(allFire[i] is playerFire proj)) continue;
                if (!proj.IsAlive) continue;

                for (int j = zombies.Count - 1; j >= 0; j--)
                {
                    Zombie zombie = zombies[j];
                    if (collisionManager.CheckPlayerFireHitsEnemy(proj, zombie))
                    {
                        RemoveFire(proj, i);
                        HandleZombieDamage(zombie, j);
                        break;
                    }
                }
            }
        }

        protected virtual void HandleZombieDamage(Zombie zombie, int index)
        {
            zombie.TakeHit();
            if (zombie.Health <= 0)
            {
                form.Controls.Remove(zombie.Sprite);
                zombie.Sprite.Dispose();
                zombies.RemoveAt(index);
            }
        }

        protected override void CheckGateCollision()
        {
            if (gate != null)
            {
                gate.Sprite.Visible = (zombies.Count == 0);
                if (gate.Sprite.Visible && collisionManager.CheckGateCollision(gate, player))
                {
                    TriggerGameWin();
                }
            }
        }
    }
}
