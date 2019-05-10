using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Weapon
    {
        public Sprite Sprite { get; set; }
        public float Speed {get; set;}
        public int Ammo { get; set; } = 20;

        public List<Projectile> Projectiles { get; set; } = new List<Projectile>();
        public float Cooldown { get; set; }
        public float CooldownTime { get; set; } = 0.1f;
        private Projectile projectile;
        public Weapon(Projectile projectile)
        {
            this.projectile = projectile;
            Time.OnTick += () =>
            {
                if (Cooldown < 1)
                    Cooldown += CooldownTime;
            };
        }
        public virtual void SpawnProjectile(Vector2 position, float rotation, Vector2 velocity)
        {
            if (Cooldown < 1 || MainScene.barArray.GetResource("power") <= 0)
            {
                return;
            }
            Projectile p = (Projectile)projectile.Clone();
            p.Position = position + Physics.GetForwardVector(rotation) * 30;
            p.physics.velocity = velocity + Physics.GetForwardVector(rotation) * 100;
            Time.OnTick += () =>
            {
                if (!p.IsDisposed)
                {
                    p.Position += p.physics.velocity;
                    p.Decay -= 0.1f;
                }
            };
            p.Rotation = Input.GetRads(rotation) + (float)Math.PI / 2;
            Projectiles.Add(p);
            Cooldown = 0;
            MainScene.barArray.SubtractResource("power", 5);
        }
        public virtual void SpawnProjectile()
        {
            if (Cooldown >= 1 && MainScene.barArray.GetResource("power") > 0)
            {
                Projectile p = (Projectile)projectile.Clone();
                p.Position = MainScene.player.position + Physics.GetForwardVector(MainScene.player.Rotation) * 30;
                p.physics.velocity = MainScene.player.physics.velocity + Physics.GetForwardVector(MainScene.player.Rotation) * 100;
                Time.OnTick += () =>
                {
                    if (!p.IsDisposed)
                    {
                        p.Position += p.physics.velocity;
                        p.Decay -= 0.1f;
                    }
                };
                p.Rotation = Input.GetRads(MainScene.player.Rotation) + (float)Math.PI / 2;
                Projectiles.Add(p);
                Cooldown = 0;
                MainScene.barArray.SubtractResource("power", 5);
            }
        }
        public virtual void DrawProjectile(SpriteBatch sprite)
        {
            foreach(Projectile projectile in Projectiles)
            {
                if (projectile.IsDisposed) continue;
                projectile.Draw(sprite);
            }
        }
    }
}
