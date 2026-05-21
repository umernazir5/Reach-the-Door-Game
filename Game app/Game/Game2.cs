using Game_app.GameObjects;
using Game_app.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Game_app.GameObjects;
using Game_app.Properties;

namespace Game_app.Game
{
    internal class Game2
    {
        Player player;
        Enemy enemy;
        Gate gate;
        HealthBar healthBar;

        List<Platform> platforms;
        List<enemyFire> enemyFire;
        List<Zombie> zombies;
        List<playerFire> playerFire;

        InputManager inputManager;
        CollisionManager collisionManager;
        AudioManager audioManager;

        public Form form;

        int fireTimer = 15;
        int fireInterval = 20;

        int playerFireTimer = 0;
        int playerFireInterval = 15;

        public int groundLevel = 382;


        public event Action OnGameOver;
        public event Action OnGameWin;


        public Game2(Form form)
        {
            this.form = form;
            platforms = new List<Platform>();
            enemyFire = new List<enemyFire>();
            playerFire = new List<playerFire>();
            zombies = new List<Zombie>();
            inputManager = new InputManager();
            collisionManager = new CollisionManager();
            audioManager = new AudioManager();
        }

        public virtual void Start()
        {
            CreateGate();
            CreatePlayer();
            CreatePlatforms();
            CreateEnemy();
            CreateZombies();
            CreateHealthBar();
            audioManager.PlayBackgroundMusic();

            // Set exact Z-Order by stacking them from back to front
            foreach (Platform platform in platforms) platform.Sprite.BringToFront();
            foreach (Zombie zombie in zombies) zombie.Sprite.BringToFront();
            player.Sprite.BringToFront();
            enemy.Sprite.BringToFront(); // Enemy goes behind the gate
            gate.Sprite.BringToFront();  // Gate goes completely in front
            healthBar.Sprite.BringToFront();
        }
        public void Update()
        {
            if (player == null) return;


            playerFireTimer++;

            HandleInput();
            player.UpdatePhysics(platforms);
            UpdateEnemy();
            SpawnEnemyFire();
            UpdateEnemyFire();
            player.HandleInvincibility();
            UpdatePlayerFire();
            UpdateZombies();

            DetectCollisions();
            detectPlayerFireCollisions();
        }

        protected void CreatePlayer()
        {
            player = new Player(Game_app.Properties.Resources.Character, 380, 380, groundLevel);
            form.Controls.Add(player.Sprite);
            player.Sprite.BringToFront();
        }

        protected void CreateEnemy()
        {
            enemy = new Enemy(Game_app.Properties.Resources.Enemy, 400, 0);
            form.Controls.Add(enemy.Sprite);
            enemy.Sprite.BringToFront();
        }
        protected void CreateZombies()
        {

            AddZombies(150, 225, 150, 270);
            AddZombies(320, 145, 320, 440);
            AddZombies(550, 195, 550, 670);
        }

        protected void AddZombies(int x, int y, int leftBound, int rightBound)
        {
            // Update to use the new constructor format
            Zombie zombie = new Zombie(Game_app.Properties.Resources.zombie, x, y, leftBound, rightBound);
            form.Controls.Add(zombie.Sprite);
            zombies.Add(zombie);
        }
        protected void UpdateZombies()
        {
            foreach (Zombie zombie in zombies)
            {
                zombie.Move();
            }
        }

        protected void CreateGate()
        {
            gate = new Gate(Game_app.Properties.Resources.Gate, 545, 30);
            form.Controls.Add(gate.Sprite);
            gate.Sprite.BringToFront();
        }

        protected void CreatePlatforms()
        {
            AddPlatform(250, 390, 120, 20);
            AddPlatform(150, 310, 120, 20);
            AddPlatform(320, 230, 120, 20);
            AddPlatform(550, 280, 120, 20);
            AddPlatform(720, 200, 120, 20);
            AddPlatform(540, 125, 120, 20);
        }

        protected void AddPlatform(int x, int y, int width, int height)
        {
            Platform platform = new Platform(Game_app.Properties.Resources.Platform, x, y, width, height);
            form.Controls.Add(platform.Sprite);
            platforms.Add(platform);
            platform.Sprite.BringToFront();
        }

        protected void CreateHealthBar()
        {
            healthBar = new HealthBar(form.ClientSize.Height);
            form.Controls.Add(healthBar.Sprite);
            healthBar.Sprite.BringToFront();
        }

      

        protected void HandleInput()
        {
            if (inputManager.MoveLeft())
                player.MoveLeft();

            if (inputManager.MoveRight())
                player.MoveRight(form.ClientSize.Width);

            if (inputManager.Jump())
                player.Jump();
            if (inputManager.Fire())
                SpawnPlayerFire();
        }
        protected void SpawnPlayerFire()
        {
            // Check if the cooldown has finished
            if (playerFireTimer < playerFireInterval) return;

            
            playerFireTimer = 0;

            playerFire proj = player.Fire(Game_app.Properties.Resources.playerFire);
            form.Controls.Add(proj.Sprite);
            proj.Sprite.BringToFront();
            playerFire.Add(proj);
        }

        protected void UpdatePlayerFire()
        {
            for (int i = playerFire.Count - 1; i >= 0; i--)
            {
                playerFire proj = playerFire[i];
                if (!proj.IsAlive)
                {
                    RemovePlayerFire(proj, i);
                    continue;
                }

                proj.Move();

                if (proj.X > form.ClientSize.Width || proj.X < 0)
                {
                    RemovePlayerFire(proj, i);
                }
            }
        }

        protected void RemovePlayerFire(playerFire proj, int index)
        {
            form.Controls.Remove(proj.Sprite);
            proj.Sprite.Dispose();
            playerFire.RemoveAt(index);
        }

        protected void UpdateEnemy()
        {
            enemy.Move();
            enemy.CheckBoundary(form.ClientSize.Width);
        }

        protected void SpawnEnemyFire()
        {
            fireTimer++;
            if (fireTimer < fireInterval) return;
            fireTimer = 0;

            enemyFire proj = enemy.Fire(Game_app.Properties.Resources.enemyFire);
            form.Controls.Add(proj.Sprite);
            proj.Sprite.BringToFront();
            enemyFire.Add(proj);
            foreach (Platform platform in platforms)
            {
                platform.Sprite.BringToFront();
            }
        }

        protected void UpdateEnemyFire()
        {
            for (int i = enemyFire.Count - 1; i >= 0; i--)
            {
                enemyFire proj = enemyFire[i];

                if (!proj.IsAlive)
                {
                    RemoveEnemyFire(proj, i);
                    continue;
                }

                proj.Move();

                if (proj.Y > form.ClientSize.Height)
                {
                    RemoveEnemyFire(proj, i);
                }
            }
        }

        protected void RemoveEnemyFire(enemyFire proj, int index)
        {
            form.Controls.Remove(proj.Sprite);
            proj.Sprite.Dispose();
            enemyFire.RemoveAt(index);
        }

        protected void DetectCollisions()
        {

            for (int i = enemyFire.Count - 1; i >= 0; i--)
            {
                enemyFire proj = enemyFire[i];
                if (!proj.IsAlive) continue;

                if (collisionManager.CheckenemyFireHitsPlayer(proj, player))
                {
                    RemoveEnemyFire(proj, i);

                    bool damageTaken = player.TakeDamage();
                    if (damageTaken)
                    {
                        healthBar.UpdateDisplay(player.playerHealth);

                        if (player.playerHealth <= 0)
                        {
                            CreateGameOverAnimation();
                            audioManager.Stop();
                            audioManager.PlayGameOverMusic();
                            OnGameOver?.Invoke();
                            return;
                        }
                    }
                    break;
                }
            }

            if (collisionManager.CheckGateCollision(gate, player))
            {
                OnGameWin?.Invoke();
            }
        }

        public void detectPlayerFireCollisions()
        {
            for (int i = playerFire.Count - 1; i >= 0; i--)
            {
                playerFire proj = playerFire[i];
                if (!proj.IsAlive) continue;

                for (int j = zombies.Count - 1; j >= 0; j--)
                {
                    Zombie zombie = zombies[j];
                    if (collisionManager.CheckPlayerFireHitsEnemy(proj, zombie))
                    {
                        RemovePlayerFire(proj, i);
                        zombie.TakeHit();

                        
                        if (zombie.Health <= 0)
                        {
                            form.Controls.Remove(zombie.Sprite);
                            zombie.Sprite.Dispose();
                            zombies.RemoveAt(j);
                        }
                        break;
                    }
                }
            }
        }

        protected void CreateGameOverAnimation()
        {
            PictureBox gameover = new PictureBox();
            gameover.Image = Game_app.Properties.Resources.gameover;
            gameover.SizeMode = PictureBoxSizeMode.StretchImage;
            gameover.BackColor = Color.Transparent;
            gameover.Width = player.Sprite.Width;
            gameover.Height = player.Sprite.Height;
            gameover.Left = player.X;
            gameover.Top = player.Y;
            form.Controls.Add(gameover);
            gameover.BringToFront();
        }
    }
}
