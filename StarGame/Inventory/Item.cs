using Microsoft.Xna.Framework;

namespace StarGame
{
    internal abstract class Item
    {
        public Sprite Graphic { get; set; }
        public string Name { get; set; }

        public int HitPoints { get; set; } = 100;
        public int NameLenght
        {
            get
            {
                if (nameLenght == 0)
                {
                    nameLenght = (int)Game1.fonts["font"].MeasureString(Name).X;
                }
                return nameLenght;
            }
        }
        private int nameLenght = 0;

        public string Description { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla sed metus quis sem interdum vulputate.\nSed sem tellus, commodo ac augue quis, malesuada placerat purus.\nQuisque vulputate risus et hendrerit fringilla. Suspendisse imperdiet ligula et est tincidunt tempus.\nVivamus in finibus elit, ac hendrerit dui. Mauris ullamcorper metus ipsum, non rhoncus ipsum accumsan nec.";
        public Vector2 DescriptionSize
        {
            get
            {
                if (descSize == Vector2.Zero)
                {
                    descSize = Game1.fonts["font"].MeasureString(Description);
                }
                return descSize;
            }
        }
        private Vector2 descSize = Vector2.Zero;

        public Slot InventorySlot { get; set; }

        public int Price { get; set; }
        public abstract void Remove();
        public abstract void Apply();

        public abstract Item Clone();
    }

    internal enum Slot
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
