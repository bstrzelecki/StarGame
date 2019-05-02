using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    abstract class Item
    {
        public Sprite Graphic { get; set; }
        public string Name { get; set; }
        public Slot InventorySlot { get; set; }

        public int Price { get; set; }
        public abstract void Use();
        public abstract void Apply();
    }
    enum Slot
    {
        Thruster,
        JumpDrive,
        Weapon,
        Generator,
        Radar,
        Tank,
        Container
    }
}
