using System;
using System.Collections.Generic;

namespace StarGame
{
    internal class Vendor
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public int ScrapPrice { get; set; }
        public Vendor()
        {
            ScrapPrice = new Random().Next(10, 15);
            Items = Database.GetRandomItems(5);
        }
    }
}
