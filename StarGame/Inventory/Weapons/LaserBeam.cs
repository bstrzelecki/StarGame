﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarGame
{
    class LaserBeam : Weapon
    {
        public LaserBeam(Projectile projectile) : base(projectile)
        {
        }

        public override void SpawnProjectile(Vector2 position, float rotation, Vector2 velocity)
        {
            if (MainScene.barArray.GetResource("power") <= 0)
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
            MainScene.barArray.SubtractResource("power", 1);
        }
    }
}