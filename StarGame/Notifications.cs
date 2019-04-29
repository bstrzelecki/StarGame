using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    class Notifications
    {
        private static string displayedText;
        private static float notificationsTime = 60f;
        private static float decay;
        public static void DisplayNotification(string text)
        {
            decay = notificationsTime;
            displayedText = text;
            Time.OnTick += Time_OnTick;
        }

        private static void Time_OnTick()
        {
            decay--;
            if(decay < 0)
            {
                displayedText = string.Empty;
                Time.OnTick -= Time_OnTick;
            }
        }

        public static void Draw(SpriteBatch sprite)
        {
            if (string.IsNullOrEmpty(displayedText)) return;

            sprite.DrawString(Game1.fonts["font"], displayedText, new Vector2(30, Game1.graphics.PreferredBackBufferHeight - 30), Color.Red);
        }
    }
}
