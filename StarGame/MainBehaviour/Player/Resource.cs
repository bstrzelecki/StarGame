using Microsoft.Xna.Framework;
using System;

namespace StarGame
{
    internal class Resource
    {
        public int BarScale { get; set; } = 10;
        public string Name { get; set; }
        public float Quantity { get; set; } = 100;
        public Color color = Color.White;
        public Resource(string name, Color color, int quantity = 100, int scale = 10)
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

        public override string ToString()
        {
            string n = Name[0].ToString().ToUpper() + Name.Substring(1, Name.Length - 1);
            
            return n + ": " + Math.Floor(Quantity);
        }
    }
}
