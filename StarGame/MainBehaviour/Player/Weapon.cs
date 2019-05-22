using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace StarGame
{
    internal abstract class Weapon
    {
        public Sprite Sprite { get; set; }
        public float Speed { get; set; }
        public int Ammo { get; set; } = 20;

        public List<Projectile> Projectiles { get; set; } = new List<Projectile>();
        public float Cooldown { get; set; }
        public float CooldownTime { get; set; } = 0.1f;
        protected Projectile projectile;
        public Weapon(Projectile projectile)
        {
            this.projectile = projectile;
            Time.OnTick += () =>
            {
                if (Cooldown < 1)
                {
                    Cooldown += CooldownTime;
                }
            };
        }
        public abstract void SpawnProjectile(Vector2 position, float rotation, Vector2 velocity);
        public virtual void DrawProjectile(SpriteBatch sprite)
        {
            foreach (Projectile projectile in Projectiles)
            {
                if (projectile.IsDisposed)
                {
                    continue;
                }

                projectile.Draw(sprite);
            }
        }
    }
}
