using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Physics : ICloneable
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
            if (deltaRotation > RotationLimit) deltaRotation = RotationLimit;
            if (deltaRotation < -RotationLimit) deltaRotation = -RotationLimit;

            float t = deltaRotation;
            return t;
        }
        public Physics()
        {
            Time.OnTick += Time_OnTick;
        }
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
            Physics ph = new Physics();
            ph.velocity = velocity;
            return ph;
        }
    }
}
