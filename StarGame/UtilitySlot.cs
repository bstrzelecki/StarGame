using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class UtilitySlot
    {
        public Slot slot;
        public Item item;

        public UtilitySlot(Slot slot, Item item)
        {
            this.slot = slot;
            this.item = item;
        }
    }
}
