using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace StarGame
{
    internal class StarSystem : IDrawable
    {
        public List<Planet> planets = new List<Planet>();
        public float StarMass { get; protected set; }
        public Vector2 position;
        public Sprite sprite;
        public StarSystem(Sprite sprite, float mass)
        {
            StarMass = mass;
            this.sprite = sprite;
            Time.OnTick += Time_OnTick;
        }

        private void Time_OnTick()
        {
            foreach (Planet planet in planets)
            {
                planet.Period += planet.cycleTime;
                foreach (Planet moon in planet.moons)
                {
                    moon.Period += planet.cycleTime;
                }
            }
        }

        public void AddPlanet(Planet planet)
        {
            planet.center = this;
            planets.Add(planet);
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(this.sprite, position + Input.cameraOffset, null, Color.White, 0, new Vector2(this.sprite.Size.Width / 2, this.sprite.Size.Height / 2), Vector2.One, SpriteEffects.None, 0);
            foreach (Planet planet in planets)
            {
                sprite.Draw(planet.sprite, Physics.GetForwardVector(planet.Period) * planet.distance + position + Input.cameraOffset, null, Color.White, 0, new Vector2(this.sprite.Size.Width / 2, this.sprite.Size.Height / 2), Vector2.One, SpriteEffects.None, 0);
                foreach (Planet moon in planet.moons)
                {
                    sprite.Draw(moon.sprite, Physics.GetForwardVector(moon.Period) * moon.distance + Physics.GetForwardVector(planet.Period) * planet.distance + position + Input.cameraOffset, null, Color.White, 0, new Vector2(this.sprite.Size.Width / 2, this.sprite.Size.Height / 2), Vector2.One, SpriteEffects.None, 0);
                }
            }
        }
    }
}
