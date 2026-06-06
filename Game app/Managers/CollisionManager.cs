using Game_app.GameObjects;
using System.Drawing;

namespace Game_app.Managers
{
    internal class CollisionManager
    {
        // ------------------------------------------------------------------
        // Fire -> Player  (polymorphic — handled by Fire.HitsPlayer directly)
        // The game loop calls proj.HitsPlayer(player) on the unified list,
        // so no separate method is needed here.  The methods below remain
        // for collisions that are NOT covered by the Fire hierarchy.
        // ------------------------------------------------------------------

        public bool CheckGateCollision(Gate gate, Player player)
        {
            int marginLeft = 65;
            int marginTop = 20;
            int marginRight = 15;
            int marginBottom = 20;

            Rectangle gateHitbox = new Rectangle(
                gate.X + marginLeft,
                gate.Y + marginTop,
                gate.Sprite.Width - marginLeft - marginRight,
                gate.Sprite.Height - marginTop - marginBottom
            );

            Rectangle playerHitbox = new Rectangle(
                player.X + 30,
                player.Y + 15,
                player.Sprite.Width - 50,
                player.Sprite.Height - 20
            );

            return playerHitbox.IntersectsWith(gateHitbox);
        }

        // playerFire hits a Zombie
        public bool CheckPlayerFireHitsEnemy(playerFire proj, Zombie zombie)
        {
            Rectangle fireHitbox = new Rectangle(
                proj.X + 20,
                proj.Y + 20,
                proj.Sprite.Width - 50,
                proj.Sprite.Height - 15
            );
            Rectangle enemyHitbox = new Rectangle(
                zombie.X + 5,
                zombie.Y + 5,
                zombie.Sprite.Width - 10,
                zombie.Sprite.Height - 7
            );
            return enemyHitbox.IntersectsWith(fireHitbox);
        }

        // Player body touches a Zombie
        public bool CheckPlayerHitsZombie(Player player, Zombie zombie)
        {
            Rectangle playerHitbox = new Rectangle(
                player.X + 30,
                player.Y + 15,
                player.Sprite.Width - 50,
                player.Sprite.Height - 20
            );

            Rectangle zombieHitbox = new Rectangle(
                zombie.X + 30,
                zombie.Y + 15,
                zombie.Sprite.Width - 50,
                zombie.Sprite.Height - 20
            );

            return playerHitbox.IntersectsWith(zombieHitbox);
        }

        // playerFire hits the Boss
        public bool CheckPlayerFireHitsEnemy(playerFire proj, Boss boss)
        {
            Rectangle fireHitbox = new Rectangle(
                proj.X + 20,
                proj.Y + 20,
                proj.Sprite.Width - 50,
                proj.Sprite.Height - 15
            );

            Rectangle bossHitbox = new Rectangle(
                boss.X + 10,
                boss.Y + 10,
                boss.Sprite.Width - 20,
                boss.Sprite.Height - 20
            );

            return bossHitbox.IntersectsWith(fireHitbox);
        }

        // Player body touches the Boss
        public bool CheckPlayerHitsBoss(Player player, Boss boss)
        {
            Rectangle playerHitbox = new Rectangle(
                player.X + 30,
                player.Y + 15,
                player.Sprite.Width - 50,
                player.Sprite.Height - 20
            );

            Rectangle bossHitbox = new Rectangle(
                boss.X + 10,
                boss.Y + 10,
                boss.Sprite.Width - 20,
                boss.Sprite.Height - 20
            );

            return playerHitbox.IntersectsWith(bossHitbox);
        }
    }
}
