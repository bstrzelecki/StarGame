using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    internal class HomingMissle : Projectile
    {
        public ITargetable target;
        public HomingMissle(ITargetable target, Sprite sprite) : base(sprite)
        {
            this.target = target;
            Time.OnTick += Time_OnTick;
            Rotation += Input.GetRads(180);
        }

        private Vector2 initialVelocity = Vector2.Zero;
        private void Time_OnTick()
        {
            if (initialVelocity == Vector2.Zero)
            {
                initialVelocity = physics.velocity;
            }

            if (target == null)
            {
                return;
            }

            Vector2 direction = target.GetPosition() - Position;
            direction.Normalize();
            float rotation = Vector3.Cross(new Vector3(direction, 0), new Vector3(Physics.GetForwardVector(Input.GetDegree(Rotation)), 0)).Z;
            Rotation += (rotation) * .1f;

            physics.velocity = initialVelocity - (Physics.GetForwardVector(Input.GetDegree(Rotation))) * 50;
            Position += physics.velocity;
        }
        public HomingMissle Clone(ITargetable target)
        {
            return new HomingMissle(target, Sprite);
        }
        public override void Draw(SpriteBatch sprite)
        {
            if (Decay < 0)
            {
                Dispose();
            }

            if (!IsDisposed)
            {
                sprite.Draw(Sprite, Position + Input.cameraOffset, null, Color.White, Rotation - Input.GetRads(90), new Vector2(Sprite.Size.Width / 2, Sprite.Size.Height / 2), 1, SpriteEffects.None, 0);
            }
        }

    }
}
