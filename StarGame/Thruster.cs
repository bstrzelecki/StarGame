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
            Graphic = new Sprite("blip");
            Name = "thruster";
            InventorySlot = Slot.Thruster;
        }
        public override void Apply()
        {
            throw new NotImplementedException();
        }

        public override void Use()
        {
            throw new NotImplementedException();
        }
    }
}
