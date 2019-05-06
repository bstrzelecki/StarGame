using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    class Inventory : IDrawable, IUpdateable
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public UtilitySlot[] Utilities { get; set; } = new UtilitySlot[7];
        public Sprite slot;
        public Sprite util;

        private Rectangle[] slotCollisions = new Rectangle[72];
        public int InventorySize { get; set; } = 72;

        public bool AddItem(Item item)
        {
            if (Items.Count > 72)
            {
                Notifications.DisplayNotification("Inventory full");
                return false;
            }
            Items.Add(item);
            int p = Items.IndexOf(item);
            slotCollisions[p] = GetRectangle(p);
            return true;
        }
        public bool RemoveItem(int slot, out Item item)
        {
            if (Items.Count > slot && Items[slot] != null)
            {
                item = Items[slot];
                slotCollisions[(from n in slotCollisions where n != Rectangle.Empty select n).ToList().Count - 1] = Rectangle.Empty;
                Items.RemoveAt(slot);
                return true;
            }
            item = null;
            return false;
        }

        private Rectangle GetRectangle(int p)
        {
            return new Rectangle(p % (slotCap + 1) * (slot.Size.Width + 8) + (int)UIController.position.X + (int)slotsOffset.X,
                                 p / (slotCap + 1) * (slot.Size.Height + 5) + (int)UIController.position.Y + (int)slotsOffset.Y,
                slot.Size.Width,
                slot.Size.Height);
        }

        public Inventory()
        {
            slot = new Sprite("slot");
            util = new Sprite("system");
            //for(int i = 0; i < 72; i++)
            //{
            //    AddItem(new Thruster());
            //}
            AddItem(new GenericItem(Slot.Armor, "plate", new Sprite("ar")));
            AddItem(new GenericItem(Slot.Generator, "reactor", new Sprite("g")));
            AddItem(new GenericItem(Slot.JumpDrive, "stdJumpDrive", new Sprite("jd")));
            AddItem(new GenericItem(Slot.Armor, "basic armor", new Sprite("blip")));
            AddItem(new GenericItem(Slot.Armor, "advanced armor", new Sprite("ar")));
            AddItem(new GenericItem(Slot.Armor, "crystal plate", new Sprite("ar")));
            for(int i = 0; i < 7; i++)
            {
                Utilities[i] = new UtilitySlot((Slot)i, new Thruster());
                Utilities[i].ApplyCollisions(UIController.position + utilitiesOffset + new Vector2(0, util.Size.Height * i));
            }
        }
        private Vector2 slotsOffset = new Vector2(230, 86);
        private Vector2 utilitiesOffset = new Vector2(20, 80);
        private Vector2 resourceOffset = new Vector2(870, 150);
        private int slotCap = 8;
        bool drawUICollisions = false;
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
            i = 0;
            foreach(Resource res in MainScene.barArray.Resources)
            {
                sprite.DrawString(Game1.fonts["font"], res.ToString(), UIController.position + resourceOffset + new Vector2(0, 20 * i), Color.Green);
                i++;
            }
            if(dragItem != null)
            {
                sprite.Draw(slot, mouseRelativePosition + Input.GetMousePosition(), Color.White);
                sprite.Draw(dragItem.Graphic, mouseRelativePosition + Input.GetMousePosition() + new Vector2(6, 6), Color.White);
            }
            //sprite.Draw(new Sprite("WhitePixel"), inventorySpace, Color.Red);
            if (drawUICollisions)
            {
                foreach (UtilitySlot u in Utilities)
                {
                    sprite.Draw(new Sprite("WhitePixel"), u.size, Color.Red);
                }
                foreach (Rectangle rect in slotCollisions)
                {
                    sprite.Draw(new Sprite("WhitePixel"), rect, Color.Red);
                } 
            }
        }
        bool isDragging = false;
        bool firstClick = true;
        Item dragItem;
        Vector2 mouseRelativePosition;
        Rectangle inventorySpace;
        public void Update()
        {
            if (MainScene.ui.UI != DisplayedUI.Inventory) return;
            inventorySpace = new Rectangle(UIController.position.ToPoint() + slotsOffset.ToPoint(), new Point(slotCap * (slot.Size.Width + 16), InventorySize / slotCap * (slot.Size.Height - 2)));
            if (Input.IsMouseKeyDown(0) && !isDragging && firstClick)
            {
                firstClick = false;
                Rectangle rect;
                if (CheckCollisions(out rect))
                {
                    int i = slotCollisions.ToList().IndexOf(rect);
                    if(RemoveItem(i,out Item item))
                    {
                        dragItem = item;
                        mouseRelativePosition =  rect.Location.ToVector2() - Input.GetMousePosition();
                    }
                }
            }
            if (dragItem != null) isDragging = true;
            else isDragging = false;
            if(Input.IsMouseKeyUp(0))
            {
                Drop();
            }
        }

        private void Drop()
        {
            firstClick = true;
            if (isDragging)
            {
                if (inventorySpace.Contains(Input.GetMousePosition()))
                {
                    AddItem(dragItem.Clone());
                }
                if (UtilityCollisions(out int i))
                {
                    if (Utilities[i].slot == dragItem.InventorySlot)
                    {
                        if (Utilities[i].item == null)
                        {
                            Utilities[i].item = dragItem.Clone();
                        }
                        else
                        {
                            AddItem(Utilities[i].item.Clone());
                            Utilities[i].item = dragItem.Clone();
                        }
                    }
                    else
                    {
                        AddItem(dragItem.Clone());
                    }
                }
                dragItem = null;
            }
        }

        private bool UtilityCollisions(out int slot)
        {
            slot = -1;
            foreach(UtilitySlot u in Utilities)
            {
                slot++;
                if (u.size.Contains(Input.GetMousePosition()))
                {
                    return true;
                }
            }

            return false;
        }
        private bool CheckCollisions(out Rectangle r)
        {
            foreach (Rectangle rect in slotCollisions)
            {
                if (rect.Contains(Input.GetMousePosition()))
                {
                    r = rect;
                    return true;
                }
            }
            r = Rectangle.Empty;
            return false;
        }
    }
}
