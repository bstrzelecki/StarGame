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
        public UtilitySlot[] Utilities { get; set; } = new UtilitySlot[7];
        public Sprite slot;
        public Sprite util;
        public Inventory()
        {
            slot = new Sprite("slot");
            util = new Sprite("system");
            for(int i = 0; i < 72; i++)
            {
                Items.Add(new Thruster());
            }
            for(int i = 0; i < 7; i++)
            {
                Utilities[i] = new UtilitySlot((Slot)i, new Thruster());
            }
        }
        private Vector2 slotsOffset = new Vector2(230, 86);
        private Vector2 utilitiesOffset = new Vector2(20, 80);
        private int slotCap = 8;
        public void Draw(SpriteBatch sprite)
        {
            int i = 0;
            int j = 0;
            foreach(Item item in Items)
            {
                sprite.Draw(slot,UIController.position +  slotsOffset + new Vector2((slot.Size.Width + 8) * i , (slot.Size.Height + 5) * j),Color.White);
                sprite.Draw(item.Graphic, UIController.position + slotsOffset + new Vector2((slot.Size.Width + 8) * i, (slot.Size.Height + 5) * j) + new Vector2(6,6), Color.White);
                i++;
                if (i > slotCap)
                {
                    i = 0;
                    j++;
                }
            }
            i = 0;
            foreach (UtilitySlot us in Utilities)
            {
                sprite.Draw(util, UIController.position + utilitiesOffset + new Vector2(0, util.Size.Height * i), Color.White);
                sprite.Draw(us.item.Graphic, UIController.position + utilitiesOffset + new Vector2(0, (util.Size.Height * i)) + new Vector2(116,13), Color.White);
                sprite.DrawString(Game1.fonts["font"], us.slot.ToString(), UIController.position + utilitiesOffset + new Vector2(0, util.Size.Height * i) + new Vector2(12,6), Color.Green);
                sprite.DrawString(Game1.fonts["font"], us.item.Name, UIController.position + utilitiesOffset + new Vector2(0, util.Size.Height * i) + new Vector2(12,26), Color.Green);
                i++;
            }
        }
    }
}
