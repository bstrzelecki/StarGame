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
    class Player : IDrawable, ICloneable, IUpdateable
    {
        public Sprite sprite;
        public Vector2 screenPosition;
        public Vector2 position;
        public float Rotation { get { return Input.GetDegree(_rotation); } set { _rotation = Input.GetRads(value); } }
        private float _rotation;
        public Rectangle collider;
        public Physics physics = new Physics();
        public float mass = 10;
        public float speed = 1.2f;

        public Weapon rmb;
        public Weapon lmb;
        public Weapon mmb;

        public Player()
        {
            sprite = new Sprite(Game1.textures["player"]);
            collider = sprite.Texture.Bounds;
            screenPosition = new Vector2(Game1.graphics.PreferredBackBufferWidth / 2, Game1.graphics.PreferredBackBufferHeight / 2);
            Time.OnTick += Time_OnTick;

            Projectile proj = new Projectile(new Sprite("plasma"));
            Weapon wep = new Weapon(proj);

            rmb = wep;
        }

        public void Time_OnTick()
        {
            position += physics.velocity;
            Rotation += physics.GetDeltaRotation();
            if (Input.IsKeyDown(Keys.W))
            {
                physics.acceleration += Physics.GetForwardVector(Rotation) * speed;
            }
            if (Input.IsKeyDown(Keys.S))
            {
                physics.acceleration -= Physics.GetForwardVector(Rotation) * 0.2f * speed;
            }
            if (Input.IsKeyDown(Keys.A))
            {
                physics.deltaRotation -= speed * 0.1f;
            }
            if (Input.IsKeyDown(Keys.D))
            {
                physics.deltaRotation += speed * 0.1f;
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
            if (rmb != null)
                rmb.DrawProjectile(sprite);
            sprite.Draw(this.sprite, screenPosition, null, Color.White, _rotation, new Vector2(collider.Width / 2, collider.Height / 2), Vector2.One,SpriteEffects.None,0);
        }


        public object Clone()
        {
            Player p = new Player();
            p.physics = (Physics)physics.Clone();
            p.position = position;
            return p;
        }

        public void Update()
        {
            if (rmb != null && Input.IsMouseKeyDown(2))
            {
                rmb.SpawnProjectile();
            }
        }
    }
}
