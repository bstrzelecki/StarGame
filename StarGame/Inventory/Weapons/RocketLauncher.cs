using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace StarGame
{
    internal class RocketLauncher : Weapon
    {
        public ITargetable target = null;
        public RocketLauncher(HomingMissle projectile) : base(projectile)
        {
        }

        public override void SpawnProjectile(Vector2 position, float rotation, Vector2 velocity)
        {
            if (Cooldown < 1)
            {
                return;
            }
            HomingMissle p = ((HomingMissle)projectile).Clone(target ?? MainScene.player);
            p.Position = position + Physics.GetForwardVector(rotation) * 30;
            p.physics.velocity = velocity;
            //p.Rotation = Input.GetRads(rotation) + (float)Math.PI / 2;
            Projectiles.Add(p);
            Debug.WriteLine(p.Position);
            Cooldown = 0;
        }
    }
}
