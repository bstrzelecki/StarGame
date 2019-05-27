using Microsoft.Xna.Framework;
using System;

namespace StarGame
{
    internal class Physics : ICloneable
    {
        public Vector2 velocity;
        public Vector2 acceleration;
        public float deltaRotation;
        public const float G = 1;
        public const float RotationLimit = 15;
        public void ApplyAcceleration()
        {
            velocity += acceleration;
            acceleration = Vector2.Zero;
        }
        public float GetDeltaRotation()
        {
            if (deltaRotation > RotationLimit)
            {
                deltaRotation = RotationLimit;
            }

            if (deltaRotation < -RotationLimit)
            {
                deltaRotation = -RotationLimit;
            }

            float t = deltaRotation;
            return t;
        }
        public Physics()
        {
            Time.OnTick += Time_OnTick;
        }
        /// <summary>
        /// Calculates vecttor for input direction
        /// </summary>
        /// <param name="rotation">Rotation in degrees</param>
        /// <returns></returns>
        public static Vector2 GetForwardVector(float rotation)
        {
            float x = (float)Math.Cos((double)Input.GetRads(rotation));
            float y = (float)Math.Sin((double)Input.GetRads(rotation));
            return new Vector2(x, y);
        }


        private void Time_OnTick()
        {
            ApplyAcceleration();
        }

        public object Clone()
        {
            Physics ph = new Physics
            {
                velocity = velocity
            };
            return ph;
        }
    }
}
