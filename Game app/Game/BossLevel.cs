using Game_app.GameObjects;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Game_app.Game
{
    internal class BossLevel : Game3
    {
        protected Boss boss;
        private int bossFireTimer = 0;
        private int bossFireInterval = 60;

        public BossLevel(Form form) : base(form)
        {

        }

        public override void Start()
        {
            base.Start();
            CreateBoss();
            boss.Sprite.BringToFront();
            foreach (Platform platform in platforms)
                platform.Sprite.BringToFront();
        }


        public override void Update()
        {
            base.Update();
            if (boss == null) return;
            boss.Move(form.ClientSize.Width, form.ClientSize.Height);
            SpawnBossFire();
        }


        protected override void CreateGate()
        {
            gate = new Gate(Game_app.Properties.Resources.Gate, 825, 45);
            gate.Sprite.Visible = false;
            form.Controls.Add(gate.Sprite);
        }

        protected void CreateBoss()
        {
            boss = new Boss(Game_app.Properties.Resources.FinalBoss, 400, 50);
            form.Controls.Add(boss.Sprite);
            
        }
        protected override void UpdateEnemyFire()
        {
            for (int i = enemyFire.Count - 1; i >= 0; i--)
            {
                enemyFire proj = enemyFire[i];
                if (!proj.IsAlive)
                {
                    RemoveEnemyFire(proj, i);
                    continue;
                }

                proj.DirectionalMove(); 

                if (proj.Y > form.ClientSize.Height ||  proj.Y < 0 || proj.X < 0 || proj.X > form.ClientSize.Width)
                {
                    RemoveEnemyFire(proj, i);
                }
            }
        }

        private void SpawnBossFire()
        {
            bossFireTimer++;
            if (bossFireTimer < bossFireInterval) return;
            bossFireTimer = 0;

            List<enemyFire> fires = boss.FireAllDirections( Game_app.Properties.Resources.enemyFire );

            foreach (enemyFire proj in fires)
            {
                form.Controls.Add(proj.Sprite);
                proj.Sprite.BringToFront();
                boss.Sprite.BringToFront();
                enemyFire.Add(proj); 
            }

            foreach (Platform platform in platforms)
                platform.Sprite.BringToFront();
            
            healthBar.Sprite.BringToFront();
        }

        protected override void DetectCollisions()
        {
            base.DetectCollisions();
            CheckPlayerFireHitsBoss();
            CheckPlayerHitsBoss();
            CheckBossDefeated();
        }

        protected override void CheckGateCollision()
        {
            if (gate != null)
            {
                gate.Sprite.Visible = (boss == null || boss.Health <= 0);
                if (gate.Sprite.Visible && collisionManager.CheckGateCollision(gate, player))
                {
                    TriggerGameWin();
                }
            }
        }

        protected void CheckPlayerFireHitsBoss()
        {
            if (boss == null) return;

            for (int i = playerFire.Count - 1; i >= 0; i--)
            {
                playerFire proj = playerFire[i];
                if (!proj.IsAlive) continue;

                if (collisionManager.CheckPlayerFireHitsEnemy(proj, boss))
                {
                    RemovePlayerFire(proj, i);
                    boss.Health -= 10;
                    break;
                }
            }
        }
        protected void CheckPlayerHitsBoss()
        {
            if (boss == null) return;

            if (collisionManager.CheckPlayerHitsBoss(player, boss))
            {
                HandlePlayerDamage();
            }
        }

        protected void CheckBossDefeated()
        {
            if (boss != null && boss.Health <= 0)
            {
                form.Controls.Remove(boss.Sprite);
                boss.Sprite.Dispose();
                boss = null;
            }
        }
        protected override void SpawnEnemyFire()
        {

        }

        protected override void CreateEnemy()
        {

        }

        protected override void CreateZombies()
        {

        }
    }
}