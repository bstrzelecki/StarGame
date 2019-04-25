using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Resource
    {
        public int BarScale { get; set; } = 10;
        public string Name { get; set; }
        public float Quantity { get; set; } = 100;
        public Color color = Color.White;
        public Resource(string name, Color color, int quantity = 100,int scale = 10)
        {
            Name = name;
            this.color = color;
            Quantity = quantity;
            BarScale = scale;
        }
        public int GetBarStage()
        {
            return (int)Quantity / BarScale;
        }
    }
}
