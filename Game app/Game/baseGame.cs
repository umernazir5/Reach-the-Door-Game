using Game_app.GameObjects;
using Game_app.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app.Game
{
    internal class BaseGame
    {
        // 1. All variables are "protected" so Game1 and Game2 can access them
        protected Player player;
        protected Enemy enemy;
        protected Gate gate;
        protected HealthBar healthBar;

        protected List<Platform> platforms;
        protected List<enemyFire> enemyFire;
        protected List<Zombie> zombies;
        protected List<playerFire> playerFire;

        protected InputManager inputManager;
        protected CollisionManager collisionManager;
        protected AudioManager audioManager;

        public Form form;

        protected int fireTimer = 15;
        protected int fireInterval = 20;

        protected int playerFireTimer = 0;
        protected int playerFireInterval = 15;

        public int groundLevel = 382;

        public event Action OnGameOver;
        public event Action OnGameWin;

        public BaseGame(Form form)
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
            if (player != null) player.Sprite.SendToBack();
            if (gate != null) gate.Sprite.SendToBack();
            foreach (Zombie zombie in zombies) zombie.Sprite.BringToFront();
            
            foreach (Platform platform in platforms) platform.Sprite.BringToFront();
            
            if (enemy != null) enemy.Sprite.BringToFront();
            if (gate != null) gate.Sprite.BringToFront();
            if (healthBar != null) healthBar.Sprite.BringToFront();
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
           
        }

        // 2. These are marked "virtual" so your level scripts can override them!
        protected virtual void CreateGate() { }
        protected virtual void CreatePlatforms() { }
        protected virtual void CreateZombies() { }

        // 3. Core Creation Methods (Shared across all levels)
        protected virtual void CreatePlayer()
        {
            player = new Player(Game_app.Properties.Resources.Character, 380, 380, groundLevel);
            form.Controls.Add(player.Sprite);
        }

        protected virtual void CreateEnemy()
        {
            enemy = new Enemy(Game_app.Properties.Resources.Enemy, 400, 0);
            form.Controls.Add(enemy.Sprite);
        }

        protected virtual void CreateHealthBar()
        {
            healthBar = new HealthBar(form.ClientSize.Height);
            form.Controls.Add(healthBar.Sprite);
        }

        // 4. Helper Methods for adding items to the lists
        protected void AddPlatform(int x, int y, int width, int height)
        {
            Platform platform = new Platform(Game_app.Properties.Resources.Platform, x, y, width, height);
            form.Controls.Add(platform.Sprite);
            platforms.Add(platform);
        }

        protected void AddZombies(int x, int y, int leftBound, int rightBound)
        {
            Zombie zombie = new Zombie(Game_app.Properties.Resources.zombie, x, y, leftBound, rightBound);
            form.Controls.Add(zombie.Sprite);
            zombies.Add(zombie);
        }

        // 5. Game Loop Logic
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
            if (playerFireTimer < playerFireInterval) return;

            playerFireTimer = 0;

            playerFire proj = player.Fire(Game_app.Properties.Resources.playerFire);
            form.Controls.Add(proj.Sprite);

            // Simplest fix: Just bring the player's fire to the very front. 
            // It will fly right over the zombies and will NEVER get stuck behind the background!
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
            if (enemy == null) return;
            enemy.Move();
            enemy.CheckBoundary(form.ClientSize.Width);
        }

        protected void SpawnEnemyFire()
        {
            if (enemy == null) return;
            fireTimer++;
            if (fireTimer < fireInterval) return;
            fireTimer = 0;

            enemyFire proj = enemy.Fire(Game_app.Properties.Resources.enemyFire);
            form.Controls.Add(proj.Sprite);

            // NEW: Instantly pushes the bullet behind all platforms, zombies, and the enemy!
            proj.Sprite.SendToBack();
            player.Sprite.SendToBack();

            enemyFire.Add(proj);
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

        protected void UpdateZombies()
        {
            foreach (Zombie zombie in zombies)
            {
                zombie.Move();
            }
        }

        // 1. The Master Caller: Only triggers the specific checks
        protected void DetectCollisions()
        {
            if (player == null || player.playerHealth <= 0) return;

            CheckEnemyFireCollisions();
            CheckZombieCollisions();
            CheckPlayerFireCollisions();
            CheckGateCollision();
        }

        // 2. Responsibility: Check Enemy Fire vs Player
        protected void CheckEnemyFireCollisions()
        {
            for (int i = enemyFire.Count - 1; i >= 0; i--)
            {
                enemyFire proj = enemyFire[i];
                if (!proj.IsAlive) continue;

                if (collisionManager.CheckenemyFireHitsPlayer(proj, player))
                {
                    RemoveEnemyFire(proj, i);
                    HandlePlayerDamage();
                    break;
                }
            }
        }

        // 3. Responsibility: Check Zombie vs Player
        protected void CheckZombieCollisions()
        {
            foreach (Zombie zombie in zombies)
            {
                if (collisionManager.CheckPlayerHitsZombie(player, zombie))
                {
                    HandlePlayerDamage();
                    // Optional: Add break if player should only take damage from one zombie at a time
                }
            }
        }

        // 4. Responsibility: Check Player Fire vs Zombies
        protected void CheckPlayerFireCollisions()
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
                        HandleZombieDamage(zombie, j);
                        break;
                    }
                }
            }
        }

        // 5. Responsibility: Check Player vs Gate
        protected void CheckGateCollision()
        {
            if (gate != null && collisionManager.CheckGateCollision(gate, player))
            {
                OnGameWin?.Invoke();
            }
        }

        // 6. Responsibility: Handle Player Losing Health
        protected void HandlePlayerDamage()
        {
            bool damageTaken = player.TakeDamage(); // Safely triggers invincibility frames
            if (damageTaken)
            {
                healthBar.UpdateDisplay(player.playerHealth);

                if (player.playerHealth <= 0)
                {
                    TriggerGameOver();
                }
            }
        }

        // 7. Responsibility: Handle Zombie Losing Health
        protected void HandleZombieDamage(Zombie zombie, int index)
        {
            zombie.TakeHit();
            if (zombie.Health <= 0)
            {
                form.Controls.Remove(zombie.Sprite);
                zombie.Sprite.Dispose();
                zombies.RemoveAt(index);
            }
        }

        // 8. Responsibility: Handle Game Over Sequence
        protected void TriggerGameOver()
        {
            CreateGameOverAnimation();
            audioManager.Stop();
            audioManager.PlayGameOverMusic();
            OnGameOver?.Invoke();
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
