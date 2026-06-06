using Game_app.GameObjects;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Game_app.Game
{
 
    internal class BossLevel : Game3
    {
        protected Boss boss;
        private int bossFireTimer    = 0;
        private int bossFireInterval = 60;

        public BossLevel(Form form) : base(form) { }

        public override void Start()
        {
            base.Start();
            CreateBoss();
            boss.Sprite.BringToFront();
            foreach (Platform p in platforms)
                p.Sprite.BringToFront();
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

        protected override void CreateEnemy()  {  }
        protected override void CreateZombies() { }
        protected override void SpawnEnemyFire() {  }

    
        protected void CreateBoss()
        {
            boss = new Boss(Game_app.Properties.Resources.FinalBoss, 400, 50);
            form.Controls.Add(boss.Sprite);
        }

    
        private void SpawnBossFire()
        {
            bossFireTimer++;
            if (bossFireTimer < bossFireInterval) return;
            bossFireTimer = 0;

            List<enemyFire> fires = boss.FireAllDirections(Game_app.Properties.Resources.enemyFire);
            foreach (enemyFire proj in fires)
            {
                
                AddFireToForm(proj, bringToFront: true);
                boss.Sprite.BringToFront();
            }

            foreach (Platform p in platforms)
                p.Sprite.BringToFront();

            healthBar.Sprite.BringToFront();
        }

     
        protected override void MoveProjectile(Fire proj)
        {
            if (proj is enemyFire ef && (ef.VelocityX != 0 || ef.VelocityY != 0))
                ef.DirectionalMove();
            else
                proj.Move();
        }

       
        protected override void DetectCollisions()
        {
            base.DetectCollisions();          
            CheckPlayerFireHitsBoss();
            CheckPlayerBodyHitsBoss();
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

        private void CheckPlayerFireHitsBoss()
        {
            if (boss == null) return;

            for (int i = allFire.Count - 1; i >= 0; i--)
            {
                if (!(allFire[i] is playerFire proj))
                {
                    continue;
                }
                if (!proj.IsAlive)
                {
                    continue;
                }

                if (collisionManager.CheckPlayerFireHitsEnemy(proj, boss))
                {
                    RemoveFire(proj, i);
                    boss.Health -= 10;
                    break;
                }
            }
        }

        private void CheckPlayerBodyHitsBoss()
        {
            if (boss == null) return;
            if (collisionManager.CheckPlayerHitsBoss(player, boss))
                HandlePlayerDamage();
        }

        private void CheckBossDefeated()
        {
            if (boss != null && boss.Health <= 0)
            {
                form.Controls.Remove(boss.Sprite);
                boss.Sprite.Dispose();
                boss = null;
            }
        }
    }
}
