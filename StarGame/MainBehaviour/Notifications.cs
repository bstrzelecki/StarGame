using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    internal class Notifications
    {
        private static string displayedText = string.Empty;
        private static float notificationsTime = 60f;
        private static float decay;
        public static void DisplayNotification(string text)
        {
            if (displayedText == string.Empty)
            {
                decay = notificationsTime;
                displayedText = text;
                Time.OnTick += Time_OnTick;
            }
            else
            {
                queued = text;
            }
        }
        private static string queued;
        private static void Time_OnTick()
        {
            decay--;
            if (decay < 0)
            {
                displayedText = string.Empty;
                Time.OnTick -= Time_OnTick;
                if(queued != string.Empty)
                {
                    DisplayNotification(queued);
                    queued = string.Empty;
                }
            }
        }

        public static void Draw(SpriteBatch sprite)
        {
            if (string.IsNullOrEmpty(displayedText))
            {
                return;
            }

            sprite.DrawString(Game1.fonts["font"], displayedText, new Vector2(30, Game1.graphics.PreferredBackBufferHeight - 30), Color.Red);
        }
    }
}
