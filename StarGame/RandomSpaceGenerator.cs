using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class RandomSpaceGenerator
    {
        public float Scale { get; set; } = 1f;
        public string TextureBaseName { get; set; } = "planet";
        public int PossibleTextures { get; set; } = 21;

        public string StarNamespace { get; set; } = "planet";
        public int PossibleStars { get; set; } = 21;
        public StarSystem Build()
        {
            Random rng = new Random();
            StarSystem system = new StarSystem(new Sprite(StarNamespace + rng.Next(0, PossibleStars)), rng.Next(60, 240));
            for(int i = rng.Next(3, 12); i > 0; i--)
            {
                Planet planet = new Planet(new Sprite(TextureBaseName + rng.Next(0, PossibleTextures)), rng.Next(30, 120), 6000 * i + 4000 + rng.Next(2000));
                planet.cycleTime = (float)Math.Sqrt(planet.distance * 0.000000001f);
                int moons = rng.Next(0, 15);
                if (moons < 3)
                {
                    for (int j = moons; j > 0; j--) {
                        Planet moon = new Planet(new Sprite(TextureBaseName + rng.Next(0, PossibleTextures)), rng.Next(15, 60), 3000 + j * 2000 + rng.Next(1000));
                        moon.Period = moon.distance * 0.001f;
                        planet.moons.Add(moon);
                    }
                }
                system.AddPlanet(planet);
            }
            return system;
        }
    }
}
