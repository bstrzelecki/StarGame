using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    class Inventory : IDrawable
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public UtilitySlot[] Utilities { get; set; }
        public Sprite slot;
        public Sprite util;
        public Inventory()
        {
            slot = new Sprite("slot");
            util = new Sprite("system");
        }
        private Vector2 slotsOffset = new Vector2(50, 50);
        private Vector2 utilitiesOffset = new Vector2(20, 60);
        private int slotCap = 6;
        public void Draw(SpriteBatch sprite)
        {
            int i = 0;
            int j = 0;
            foreach(Item item in Items)
            {
                sprite.Draw(slot, slotsOffset + new Vector2((item.Graphic.Size.Width + 5) * i + (item.Graphic.Size.Height + 5) * j),Color.White);
                sprite.Draw(item.Graphic, slotsOffset + new Vector2((item.Graphic.Size.Width + 5) * i + (item.Graphic.Size.Height + 5) * j),Color.White);
                if(i > slotCap)
                {
                    i = 0;
                    j++;
                }
            }
            i = 0;
            foreach(UtilitySlot us in Utilities)
            {
                sprite.Draw(util, utilitiesOffset + new Vector2(0, (us.item.Graphic.Size.Height * i)), Color.White);
                sprite.Draw(us.item.Graphic, utilitiesOffset + new Vector2(0, (us.item.Graphic.Size.Height * i)), Color.White);
            }
        }
    }
}
