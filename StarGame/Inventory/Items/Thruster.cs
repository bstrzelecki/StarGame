using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Thruster : Item
    {
        public Thruster()
        {
            Graphic = new Sprite("th");
            Name = "thruster";
            InventorySlot = Slot.Thruster;
        }
        public override void Apply()
        {
            
        }

        public override Item Clone()
        {
            return new Thruster();
        }

        public override void Remove()
        {
            
        }
    }
}
