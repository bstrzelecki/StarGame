using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace StarGame
{
    internal class UIController : IDrawable, IUpdateable
    {
        public DisplayedUI UI { get; set; }
        public Dictionary<DisplayedUI, Sprite> guis = new Dictionary<DisplayedUI, Sprite>();
        public static Vector2 position = new Vector2(30, 30);
        private StarMap sm;
        private TradeUI trade;

        public UIController()
        {
            guis.Add(DisplayedUI.StarMap, new Sprite("mapview"));
            guis.Add(DisplayedUI.Inventory, new Sprite("inventory"));
            guis.Add(DisplayedUI.Trade, new Sprite("trade"));
            position = new Vector2((Game1.graphics.PreferredBackBufferWidth - guis[DisplayedUI.Inventory].Size.Width) / 2, (Game1.graphics.PreferredBackBufferHeight - guis[DisplayedUI.Inventory].Size.Height) / 2);

        }
        public void SetView(DisplayedUI ui)
        {
            if (ui == DisplayedUI.Trade && MainScene.TradeShip == null) {
                Notifications.DisplayNotification("No trade ship nearby");
                return;
            }

            UI = ui;
            if (ui == DisplayedUI.None)
            {
                return;
            }

            position = new Vector2((Game1.graphics.PreferredBackBufferWidth - guis[UI].Size.Width) / 2, (Game1.graphics.PreferredBackBufferHeight - guis[UI].Size.Height) / 2);
            if (UI == DisplayedUI.StarMap)
            {
                sm = new StarMap(new Vector2(122, 22) + position);
            }
            else
            {
                if (sm != null)
                {
                    sm.Dispose();
                    sm = null;
                }
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            switch (UI)
            {
                case DisplayedUI.None:
                    return;
                case DisplayedUI.StarMap:
                    DrawStarMap(sprite);
                    break;
                case DisplayedUI.Inventory:
                    DrawInventory(sprite);
                    break;
                case DisplayedUI.Trade:
                    DrawTrade(sprite);
                    break;
            }
        }

        private void DrawTrade(SpriteBatch sprite)
        {
            sprite.Draw(guis[DisplayedUI.Trade], position, Color.White);
            if (trade == null)
            {
                trade = new TradeUI();
            }
            trade.Draw(sprite);
        }

        private void DrawInventory(SpriteBatch sprite)
        {
            sprite.Draw(guis[DisplayedUI.Inventory], position, Color.White);
            MainScene.inventory.Draw(sprite);
        }

        private void DrawStarMap(SpriteBatch sprite)
        {
            sprite.Draw(guis[DisplayedUI.StarMap], position, Color.White);
            sm.Draw(sprite);
        }

        public void Update()
        {
            if (sm != null)
            {
                sm.Update();
            }

            if (trade != null)
            {
                trade.Update();
            }
        }
    }
    public enum DisplayedUI
    {
        None,
        StarMap,
        Inventory,
        Trade,
        QuestLog

    }
}
