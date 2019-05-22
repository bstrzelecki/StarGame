using System;

namespace StarGame
{
    internal class RandomSpaceGenerator
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
            for (int i = rng.Next(3, 12); i > 0; i--)
            {
                GeneratePlanet(rng, system, i);
            }
            return system;
        }

        private void GeneratePlanet(Random rng, StarSystem system, int i)
        {
            Planet planet = new Planet(new Sprite(TextureBaseName + rng.Next(0, PossibleTextures)), rng.Next(30, 120), 6000 * i + 4000 + rng.Next(2000));
            planet.cycleTime = (float)Math.Sqrt(planet.distance * 0.000000001f);
            int moons = rng.Next(0, 15);
            if (moons < 3)
            {
                GenerateMoons(rng, planet, moons);
            }
            system.AddPlanet(planet);
        }

        private void GenerateMoons(Random rng, Planet planet, int moons)
        {
            for (int j = moons; j > 0; j--)
            {
                Planet moon = new Planet(new Sprite(TextureBaseName + rng.Next(0, PossibleTextures)), rng.Next(15, 60), 3000 + j * 2000 + rng.Next(1000));
                moon.Period = moon.distance * 0.001f;
                planet.moons.Add(moon);
            }
        }
    }
}
