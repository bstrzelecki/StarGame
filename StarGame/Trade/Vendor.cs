using System;
using System.Collections.Generic;

namespace StarGame
{
    internal class Vendor
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public int ScrapPrice { get; set; }

        public int[] ResourcePrices = new int[4];
        public Vendor()
        {
            Random rng = new Random();
            ScrapPrice = rng.Next(10, 15);
            Items = Database.GetRandomItems(5);
            for (int i = 0; i < 4; i++)
            {
                ResourcePrices[i] = rng.Next(2, 10);
            }
        }
    }
}
