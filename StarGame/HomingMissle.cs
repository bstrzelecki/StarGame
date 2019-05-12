using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class HomingMissle : Projectile
    {
        public ITargetable target;
        public HomingMissle(ITargetable target, Sprite sprite) : base(sprite)
        {
            this.target = target;
            Time.OnTick += Time_OnTick;
        }
        Vector2 initialVelocity = Vector2.Zero;
        private void Time_OnTick()
        {
            if (initialVelocity == Vector2.Zero) initialVelocity = physics.velocity; 
            if (target == null) return;
            Vector2 direction = target.GetPosition() - Position;
            direction.Normalize();
            float rotation = Vector3.Cross(new Vector3(direction, 0), new Vector3(Physics.GetForwardVector(Rotation),0)).Z;
            Rotation = rotation - (float) Math.PI/ 90;

            physics.velocity = initialVelocity;
            Position += physics.velocity;
        }
        public HomingMissle Clone(ITargetable target)
        {
            return new HomingMissle(target, Sprite);
        }
    }
}
