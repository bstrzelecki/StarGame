using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StarGame
{
    class Player : IDrawable, IUpdateable
    {
        public Sprite sprite;
        public Vector2 screenPosition;
        public Vector2 position;
        public float Rotation { get { return Input.GetDegree(_rotation); } set { _rotation = Input.GetRads(value); } }
        private float _rotation;
        public Rectangle collider;
        public Physics physics = new Physics();
        public float mass = 10;
        public Player()
        {
            sprite = new Sprite(Game1.textures["player"]);
            collider = sprite.Texture.Bounds;
            screenPosition = new Vector2(Game1.graphics.PreferredBackBufferWidth / 2, Game1.graphics.PreferredBackBufferHeight / 2);
            Time.OnTick += Time_OnTick;
        }

        private void Time_OnTick()
        {
            position += physics.velocity;
            Rotation += physics.GetDeltaRotation();
            if (Input.IsKeyDown(Keys.W))
            {
                physics.acceleration += Physics.GetForwardVector(Rotation);
            }
            if (Input.IsKeyDown(Keys.S))
            {
                physics.acceleration -= Physics.GetForwardVector(Rotation) * 0.2f;
            }
            if (Input.IsKeyDown(Keys.A))
            {
                physics.deltaRotation -= 1;
            }
            if (Input.IsKeyDown(Keys.D))
            {
                physics.deltaRotation += 1;
            }
            ApplyGravity(MainScene.sun);
        }
        public void ApplyGravity(StarSystem system)
        {

            float force = Physics.G * (mass * system.StarMass) / (float)Math.Pow(Vector2.Distance(position, system.position), 2);
            physics.acceleration += force * (-position + system.position);

            foreach(Planet planet in system.planets)
            {
                force = Physics.G * (mass * planet.Mass) / (float)Math.Pow(Vector2.Distance(position, Physics.GetForwardVector(planet.Period) * planet.distance + system.position), 2);
                physics.acceleration += force * (-position + Physics.GetForwardVector(planet.Period) * planet.distance + system.position);
            }

        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(this.sprite, screenPosition, null, Color.White, _rotation, new Vector2(collider.Width / 2, collider.Height / 2), Vector2.One,SpriteEffects.None,0);
        }

        public void Update()
        {

        }
    }
}
