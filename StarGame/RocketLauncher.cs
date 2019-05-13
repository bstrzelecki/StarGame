using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarGame
{
    class RocketLauncher : Weapon
    {
        public ITargetable target;
        public RocketLauncher(HomingMissle projectile) : base(projectile)
        {
        }

        public override void SpawnProjectile(Vector2 position, float rotation, Vector2 velocity)
        {
            if (Cooldown < 1 || MainScene.barArray.GetResource("power") <= 0)
            {
                return;
            }
            HomingMissle p = ((HomingMissle)projectile).Clone(target??MainScene.player);
            p.Position = position + Physics.GetForwardVector(rotation) * 30;
            p.physics.velocity = velocity;
            //p.Rotation = Input.GetRads(rotation) + (float)Math.PI / 2;
            Projectiles.Add(p);
            Cooldown = 0;
            MainScene.barArray.SubtractResource("power", 5);
        }
    }
}
