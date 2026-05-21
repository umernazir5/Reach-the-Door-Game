using Game_app.GameObjects;
using System.Drawing;

namespace Game_app.Managers
{
    internal class CollisionManager
    {
        public bool CheckenemyFireHitsPlayer(enemyFire proj, Player player)
        {
            Rectangle fireHitbox = new Rectangle(
                proj.X + 30,
                proj.Y + 20,
                proj.Sprite.Width - 50,
                proj.Sprite.Height - 15
            );

            Rectangle playerHitbox = new Rectangle(
                player.X + 30,
                player.Y + 15,
                player.Sprite.Width - 50,
                player.Sprite.Height - 20
            );

            return playerHitbox.IntersectsWith(fireHitbox);
        }

        public bool CheckGateCollision(Gate gate, Player player)
        {
            int marginLeft = 40;
            int marginTop = 10;
            int marginRight = 5;
            int marginBottom = 10;

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

        public bool CheckPlayerFireHitsEnemy(playerFire proj, Zombie zombie)
        {
            Rectangle fireHitbox = new Rectangle(
                proj.X + 20,
                proj.Y + 20,
                proj.Sprite.Width - 50,
                proj.Sprite.Height - 15
            );
            Rectangle enemyHitbox = new Rectangle(
                zombie.X + 30,
                zombie.Y + 15,
                zombie.Sprite.Width - 50,
                zombie.Sprite.Height - 20
            );
            return enemyHitbox.IntersectsWith(fireHitbox);
        }
        public bool CheckPlayerHitsZombie(Player player, Zombie zombie)
        {
            // Use the same tight player hitbox you use for enemy fire
            Rectangle playerHitbox = new Rectangle(
                player.X + 30,
                player.Y + 15,
                player.Sprite.Width - 50,
                player.Sprite.Height - 20
            );

            // Create a tight hitbox for the zombie
            Rectangle zombieHitbox = new Rectangle(
                zombie.X + 30,
                zombie.Y + 15,
                zombie.Sprite.Width - 50,
                zombie.Sprite.Height - 20
            );

            return playerHitbox.IntersectsWith(zombieHitbox);
        }
    }
}
