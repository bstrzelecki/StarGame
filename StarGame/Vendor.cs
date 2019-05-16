using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Vendor
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
