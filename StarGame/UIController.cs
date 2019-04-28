using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class UIController : IDrawable, IUpdateable
    {
        public DisplayedUI UI { get; set; }
        public Dictionary<DisplayedUI, Sprite> guis = new Dictionary<DisplayedUI, Sprite>();
        Vector2 position = new Vector2(30, 30);
        StarMap sm;
        public UIController()
        {
            guis.Add(DisplayedUI.StarMap, new Sprite("mapview"));
            
        }
        public void SetView(DisplayedUI ui)
        {
            UI = ui;
            if (ui == DisplayedUI.None) return;
            position = new Vector2((Game1.graphics.PreferredBackBufferWidth - guis[UI].Size.Width) / 2, (Game1.graphics.PreferredBackBufferHeight - guis[UI].Size.Height) / 2);
            if (UI == DisplayedUI.StarMap)
            {
                sm = new StarMap(new Vector2(122, 22) + position);
            }
            else
            {
                sm.Dispose();
                sm = null;
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
            }
        }

        
        private void DrawStarMap(SpriteBatch sprite)
        {
            sprite.Draw(guis[DisplayedUI.StarMap], position, Color.White);
            sm.Draw(sprite);
        }

        public void Update()
        {
            if (sm != null) sm.Update();
        }
    }
    public enum DisplayedUI
    {
        None,
        StarMap,
        Inventory,
        QuestLog
    }
}
