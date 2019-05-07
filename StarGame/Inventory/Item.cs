using Microsoft.Xna.Framework;
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

        public int NameLenght
        {
            get
            {
                if(nameLenght == 0)
                {
                    nameLenght = (int)Game1.fonts["font"].MeasureString(Name).X;
                }
                return nameLenght;
            }
        }
        private int nameLenght = 0;

        public string Description { get; set; } = "asdasasdsadasd\nasasdasdasda\nsdasdasasdasdasdasdasdasdasdasadd\nassdsaasdasdasdasdasasdsd";
        public Vector2 DescriptionSize
        {
            get
            {
                if(descSize == Vector2.Zero)
                {
                    descSize = Game1.fonts["font"].MeasureString(Description);
                }
                return descSize;
            }
        }
        private Vector2 descSize = Vector2.Zero;

        public Slot InventorySlot { get; set; }

        public int Price { get; set; }
        public abstract void Use();
        public abstract void Apply();

        public abstract Item Clone();
    }
    enum Slot
    {
        Thruster,
        JumpDrive,
        Weapon,
        Generator,
        Radar,
        Tank,
        Armor
    }
}
