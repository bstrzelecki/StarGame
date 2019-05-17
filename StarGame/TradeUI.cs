using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    class TradeUI : IDrawable, IUpdateable
    {
        public Vector2 slotsOffset = new Vector2(30, 90);
        private Vector2 vendorOffset = new Vector2(680, 90);
        private Rectangle inventorySpace;
        private Rectangle[] slotCollisions = new Rectangle[72];
        public TradeUI()
        {
            Sprite slot = MainScene.inventory.slot;
            inventorySpace = new Rectangle(UIController.position.ToPoint() + slotsOffset.ToPoint(), new Point(8 * (slot.Size.Width + 16), 72 / 8 * (slot.Size.Height - 2)));
            int i = 0;
            foreach (var s in MainScene.inventory.slotCollisions)
            {
                Rectangle n = s;
                n.X -= (int)MainScene.inventory.slotsOffset.X - (int)slotsOffset.X;
                slotCollisions[i] = n;
                i++;
            }
            
        }

        bool drawDebug = true;
        public void Draw(SpriteBatch sprite)
        {
            int i = 0;
            int j = 0;
            Sprite slot = MainScene.inventory.slot;
            foreach (Item item in MainScene.inventory.Items)
            {
                sprite.Draw(slot, UIController.position + slotsOffset + new Vector2((slot.Size.Width + 8) * i, (slot.Size.Height + 5) * j), Color.White);
                sprite.Draw(item.Graphic, UIController.position + slotsOffset + new Vector2((slot.Size.Width + 8) * i, (slot.Size.Height + 5) * j) + new Vector2(6, 6), Color.White);
                i++;
                if (i > 8)
                {
                    i = 0;
                    j++;
                }
            }
            DrawVendorItems(sprite, slot);

            if (drawDebug)
            {
                foreach(var s in slotCollisions)
                {
                    sprite.Draw(new Sprite(), s, Color.Red);
                }
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        private void DrawVendorItems(SpriteBatch sprite, Sprite slot)
        {
            int i = 0;
            int j = 0;
            foreach (Item item in MainScene.TradeShip.Items)
            {
                sprite.Draw(slot, UIController.position + vendorOffset + new Vector2((slot.Size.Width + 8) * i, (slot.Size.Height + 5) * j), Color.White);
                sprite.Draw(item.Graphic, UIController.position + vendorOffset + new Vector2((slot.Size.Width + 8) * i, (slot.Size.Height + 5) * j) + new Vector2(6, 6), Color.White);
                i++;
                if (i > 1)
                {
                    i = 0;
                    j++;
                }
            }
        }
    }
}
