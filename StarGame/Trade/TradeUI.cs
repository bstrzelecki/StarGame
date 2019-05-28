using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace StarGame
{
    internal class TradeUI : IDrawable, IUpdateable
    {
        public Vector2 slotsOffset = new Vector2(30, 90);
        private Vector2 vendorOffset = new Vector2(680, 90);
        private Rectangle inventorySpace;
        private Rectangle[] slotCollisions = new Rectangle[72];
        private List<Rectangle> vendorCollisions = new List<Rectangle>();
        private Rectangle dumpSite;
        public TradeUI()
        {
            Sprite slot = MainScene.inventory.slot;
            inventorySpace = new Rectangle(UIController.position.ToPoint() + slotsOffset.ToPoint(), new Point(8 * (slot.Size.Width + 16), 72 / 8 * (slot.Size.Height - 2)));
            UpdateCollisions();
            int o = 0;
            int c = 0;
            foreach (Item it in MainScene.TradeShip.Items)
            {
                vendorCollisions.Add(new Rectangle((UIController.position + vendorOffset + new Vector2((slot.Size.Width + 8) * o, (slot.Size.Height + 5) * c)).ToPoint(), new Point(slot.Size.Width, slot.Size.Height)));
                o++;
                if (o > 1)
                {
                    o = 0;
                    c++;
                }
            }
            dumpSite = new Rectangle((int)UIController.position.X + 950, (int)UIController.position.Y + 270, 64, 64);
        }

        private void UpdateCollisions()
        {
            int i = 0;
            foreach (var s in MainScene.inventory.slotCollisions)
            {
                Rectangle n = s;
                n.X -= (int)MainScene.inventory.slotsOffset.X - (int)slotsOffset.X;
                slotCollisions[i] = n;
                i++;
            }
        }

        private Vector2 mouseRelativePosition;
        private bool drawDebug = true;
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
            if (hoverItem != null)
            {
                if (isDraggingVendorItem)
                {
                    Tooltip.Draw(Input.GetMousePosition(), hoverItem, hoverItem.Price, sprite);
                }
                else
                {
                    Tooltip.Draw(Input.GetMousePosition(), hoverItem, sprite);
                }
            }
            if (dragItem != null)
            {
                sprite.Draw(slot, mouseRelativePosition + Input.GetMousePosition(), Color.White);
                sprite.Draw(dragItem.Graphic, mouseRelativePosition + Input.GetMousePosition() + new Vector2(6, 6), Color.White);
            }
            sprite.DrawString(Game1.fonts["font"], MainScene.Cash.ToString(), UIController.position + new Vector2(960, 80), Color.Green);
            sprite.DrawString(Game1.fonts["font"], MainScene.TradeShip.ScrapPrice.ToString(), UIController.position + new Vector2(960, 240), Color.Green);

            DrawResourcePrices(sprite);


            if (drawDebug)
            {
                foreach (var s in slotCollisions)
                {
                    //sprite.Draw(new Sprite(), s, Color.Red);
                }
                foreach (var s in vendorCollisions)
                {
                    //sprite.Draw(new Sprite(), s, Color.Red);
                }
                foreach (var s in resCollisions)
                {
                    //sprite.Draw(new Sprite(), s, Color.Red);
                }
                //sprite.Draw(new Sprite(), inventorySpace, Color.Red);
                //sprite.Draw(new Sprite(), dumpSite, Color.Red);
            }
        }

        private Rectangle[] resCollisions = new Rectangle[4];
        private void DrawResourcePrices(SpriteBatch sprite)
        {
            int i = 0;
            foreach (int price in MainScene.TradeShip.ResourcePrices)
            {
                if (resCollisions[i] == Rectangle.Empty)
                {
                    resCollisions[i] = new Rectangle((UIController.position + new Vector2(880, 380) + new Vector2(0, 50 * i)).ToPoint(), new Point(200, 40));
                }
                sprite.DrawString(Game1.fonts["font"], "Buy 1 " + GetLabel(i) + " for " + price.ToString() + " (" + MainScene.barArray.Resources[i].Quantity + "/100)", UIController.position + new Vector2(880, 380) + new Vector2(0, 50 * i), GetResourceColor(i));
                i++;
            }
        }
        private Color GetResourceColor(int i)
        {
            if (IsOverResource(i))
            {
                if (Input.IsMouseKeyDown(0))
                {
                    return Color.Red;
                }
                else
                {
                    return Color.Orange;
                }
            }
            else
            {
                return Color.Green;
            }
        }
        private string GetLabel(int i)
        {
            switch (i)
            {
                case 0:
                    return "Fuel";
                case 1:
                    return "Oxygen";
                case 2:
                    return "Power";
                case 3:
                    return "Armor";
            }
            return string.Empty;
        }
        private bool IsOverResource(int i)
        {
            return resCollisions[i].Contains(Input.GetMousePosition());
        }
        private Item dragItem = null;
        private Item hoverItem = null;
        private bool isDraggingVendorItem = false;
        public void Update()
        {
            if (MainScene.ui.UI != DisplayedUI.Trade)
            {
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                if (IsOverResource(i) && Input.IsMouseKeyDown(0))
                {
                    if (MainScene.Cash >= MainScene.TradeShip.ResourcePrices[i] && MainScene.barArray.Resources[i].Quantity < 100)
                    {
                        MainScene.Cash -= MainScene.TradeShip.ResourcePrices[i];
                        MainScene.barArray.Resources[i].Quantity++;
                    }
                }
            }

            if (dragItem == null)
            {
                if (Input.IsMouseKeyDown(0))
                {
                    foreach (Rectangle rect in vendorCollisions)
                    {
                        if (rect.Contains(Input.GetMousePosition()) && MainScene.TradeShip.Items.Count > vendorCollisions.IndexOf(rect))
                        {
                            dragItem = MainScene.TradeShip.Items[vendorCollisions.IndexOf(rect)].Clone();
                            mouseRelativePosition = rect.Location.ToVector2() - Input.GetMousePosition();
                            MainScene.TradeShip.Items.RemoveAt(vendorCollisions.IndexOf(rect));
                            isDraggingVendorItem = true;
                        }
                    }
                    foreach (Rectangle rect in slotCollisions)
                    {
                        if (rect.Contains(Input.GetMousePosition()))
                        {
                            dragItem = MainScene.inventory.Items[slotCollisions.ToList().IndexOf(rect)].Clone();
                            mouseRelativePosition = rect.Location.ToVector2() - Input.GetMousePosition();
                            MainScene.inventory.RemoveItem(slotCollisions.ToList().IndexOf(rect), out Item it);
                            isDraggingVendorItem = false;
                        }
                    }
                }
                else
                {
                    hoverItem = null;
                    foreach (Rectangle rect in vendorCollisions)
                    {
                        if (rect.Contains(Input.GetMousePosition()) && MainScene.TradeShip.Items.Count > vendorCollisions.IndexOf(rect))
                        {
                            hoverItem = MainScene.TradeShip.Items[vendorCollisions.ToList().IndexOf(rect)].Clone();
                            isDraggingVendorItem = true;
                            break;
                        }
                    }
                    foreach (Rectangle rect in slotCollisions)
                    {
                        if (hoverItem != null)
                        {
                            return;
                        }

                        if (rect.Contains(Input.GetMousePosition()))
                        {
                            hoverItem = MainScene.inventory.Items[slotCollisions.ToList().IndexOf(rect)].Clone();
                            isDraggingVendorItem = false;
                            break;
                        }
                    }
                }

            }
            else
            {
                if (isDraggingVendorItem)
                {
                    if (Input.IsMouseKeyUp(0))
                    {
                        if (inventorySpace.Contains(Input.GetMousePosition()))
                        {
                            if (MainScene.Cash >= dragItem.Price)
                            {
                                MainScene.inventory.AddItem(dragItem.Clone());
                                MainScene.Cash -= dragItem.Price;
                                UpdateCollisions();
                            }
                            else
                            {
                                MainScene.TradeShip.Items.Add(dragItem.Clone());
                            }
                            dragItem = null;
                        }
                        else
                        {
                            MainScene.TradeShip.Items.Add(dragItem.Clone());
                            dragItem = null;
                        }

                    }
                }
                else
                {
                    if (Input.IsMouseKeyUp(0))
                    {
                        if (dumpSite.Contains(Input.GetMousePosition()))
                        {
                            MainScene.Cash += MainScene.TradeShip.ScrapPrice;

                        }
                        else
                        {
                            MainScene.inventory.AddItem(dragItem.Clone());
                        }
                        dragItem = null;
                    }
                }
            }
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
