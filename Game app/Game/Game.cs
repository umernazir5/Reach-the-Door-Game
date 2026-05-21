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
    internal class Game
    {
        Player player;
        Enemy enemy;
        Gate gate;
        HealthBar healthBar;
        List<Platform> platforms;
        List<enemyFire> enemyFire;

        InputManager inputManager;
        CollisionManager collisionManager;
        AudioManager audioManager;

        Form form;

        int fireTimer = 0;
        int fireInterval = 20;
        int groundLevel = 382;

   
        public event Action OnGameOver;
        public event Action OnGameWin;

        public Game(Form form)
        {
            this.form = form;
            platforms = new List<Platform>();
            enemyFire = new List<enemyFire>();
            inputManager = new InputManager();
            collisionManager = new CollisionManager();
            audioManager = new AudioManager();
        }

        public void Start()
        {
            CreateGate();
            CreatePlayer();
            
            CreatePlatforms();
            CreateEnemy();
            CreateHealthBar();
            audioManager.PlayBackgroundMusic();
        }

        private void CreatePlayer()
        {
            player = new Player(Game_app.Properties.Resources.Character, 380, 380, groundLevel);
            form.Controls.Add(player.Sprite);
            player.Sprite.BringToFront();
        }

        private void CreateEnemy()
        {
            enemy = new Enemy(Game_app.Properties.Resources.Enemy, 400, 0);
            form.Controls.Add(enemy.Sprite);
            enemy.Sprite.BringToFront();
        }

        private void CreateGate()
        {
            gate = new Gate(Game_app.Properties.Resources.Gate, 823, 55);
            form.Controls.Add(gate.Sprite);
            gate.Sprite.BringToFront();
        }

        private void CreatePlatforms()
        {
            AddPlatform(0, 420, 100, 20);
            AddPlatform(200, 350, 100, 20);
            AddPlatform(400, 270, 100, 20);
            AddPlatform(600, 190, 100, 20);
            AddPlatform(830, 140, 100, 20);
        }

        private void AddPlatform(int x, int y, int width, int height)
        {
            Platform platform = new Platform(Game_app.Properties.Resources.Platform, x, y, width, height);
            form.Controls.Add(platform.Sprite);
            platforms.Add(platform);
            platform.Sprite.BringToFront();
        }

        private void CreateHealthBar()
        {
            healthBar = new HealthBar(form.ClientSize.Height);
            form.Controls.Add(healthBar.Sprite);
            healthBar.Sprite.BringToFront();
        }

        public void Update()
        {
            if (player == null) return;

            HandleInput();
            player.UpdatePhysics(platforms);
            UpdateEnemy();
            SpawnEnemyFire();
            UpdateEnemyFire();
            player.HandleInvincibility();
            DetectCollisions();
        }

        private void HandleInput()
        {
            if (inputManager.MoveLeft())
                player.MoveLeft();

            if (inputManager.MoveRight())
                player.MoveRight(form.ClientSize.Width);

            if (inputManager.Jump())
                player.Jump();
        }

        private void UpdateEnemy()
        {
            enemy.Move();
            enemy.CheckBoundary(form.ClientSize.Width);
        }

        private void SpawnEnemyFire()
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

        private void UpdateEnemyFire()
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

        private void RemoveEnemyFire(enemyFire proj, int index)
        {
            form.Controls.Remove(proj.Sprite);
            proj.Sprite.Dispose();
            enemyFire.RemoveAt(index);
        }

        private void DetectCollisions()
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

        private void CreateGameOverAnimation()
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
