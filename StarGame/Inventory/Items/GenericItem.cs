using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class GenericItem : Item
    {
        public GenericItem(Slot slot, string name, Sprite sprite)
        {
            InventorySlot = slot;
            Name = name;
            Graphic = sprite;
        }
        public override void Apply()
        {
            
        }

        public override Item Clone()
        {
            return new GenericItem(InventorySlot, Name, Graphic);
        }

        public override void Remove()
        {
            
        }
    }
}
