using Game_app.GameObjects;
using Game_app.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game_app.Game
{
    internal class Game1
    {
       
        protected Player player;
        protected Enemy enemy;
        protected Gate gate;
        protected HealthBar healthBar;

        protected List<Platform> platforms;
        protected List<enemyFire> enemyFire;
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

        public Game1(Form form)
        {
            this.form = form;
            platforms = new List<Platform>();
            enemyFire = new List<enemyFire>();
            playerFire = new List<playerFire>();
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
            CreateHealthBar();
            audioManager.PlayBackgroundMusic();

            if (player != null) player.Sprite.SendToBack();
            if (gate != null) gate.Sprite.SendToBack();
            foreach (Platform platform in platforms) platform.Sprite.BringToFront();
            if (enemy != null) enemy.Sprite.BringToFront();
            if (gate != null) gate.Sprite.BringToFront();
            if (healthBar != null) healthBar.Sprite.BringToFront();
        }

        public virtual void Update()
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

            DetectCollisions();
        }

        protected virtual void CreateGate()
        {
            gate = new Gate(Game_app.Properties.Resources.Gate, 823, 45);
            form.Controls.Add(gate.Sprite);
        }

        protected virtual void CreatePlatforms()
        {
            AddPlatform(0, 420, 100, 20);
            AddPlatform(200, 350, 100, 20);
            AddPlatform(400, 270, 100, 20);
            AddPlatform(600, 190, 100, 20);
            AddPlatform(830, 140, 100, 20);
        }

        
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

        protected void AddPlatform(int x, int y, int width, int height)
        {
            Platform platform = new Platform(Game_app.Properties.Resources.Platform, x, y, width, height);
            form.Controls.Add(platform.Sprite);
            platforms.Add(platform);
        }

        protected void HandleInput()
        {
            if (inputManager.MoveLeft()) player.MoveLeft();
            if (inputManager.MoveRight()) player.MoveRight(form.ClientSize.Width);
            if (inputManager.Jump()) player.Jump();
            if (inputManager.Fire()) SpawnPlayerFire();
        }

        protected void SpawnPlayerFire()
        {
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
                    RemovePlayerFire(proj, i);
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
                proj.ShipMove();
                if (proj.Y > form.ClientSize.Height) RemoveEnemyFire(proj, i);
            }
        }

        protected void RemoveEnemyFire(enemyFire proj, int index)
        {
            form.Controls.Remove(proj.Sprite);
            proj.Sprite.Dispose();
            enemyFire.RemoveAt(index);
        }

        // --- LEVEL 1 COLLISIONS ---
        protected virtual void DetectCollisions()
        {
            if (player == null || player.playerHealth <= 0) return;
            CheckEnemyFireCollisions();
            CheckGateCollision();
        }

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

        protected virtual void CheckGateCollision()
        {
            if (gate != null && gate.Sprite.Visible && collisionManager.CheckGateCollision(gate, player))
            {
                TriggerGameWin();
            }
        }

        protected void HandlePlayerDamage()
        {
            bool damageTaken = player.TakeDamage();
            if (damageTaken)
            {
                audioManager.PlayDamageMusic();
                healthBar.UpdateDisplay(player.playerHealth);
                if (player.playerHealth <= 0) TriggerGameOver();
            }
        }

        protected void TriggerGameWin()
        {
            OnGameWin?.Invoke();
        }

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