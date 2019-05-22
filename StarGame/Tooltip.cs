using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace StarGame
{
    internal class Tooltip
    {
        public static void Draw(Vector2 position, Item item, SpriteBatch sprite)
        {
            Rectangle size = new Rectangle(position.ToPoint() + new Vector2(8, 8).ToPoint(), new Point((int)Math.Max(item.NameLenght + 8, item.DescriptionSize.X + 8), (int)item.DescriptionSize.Y + 16));
            sprite.Draw(new Sprite(), new Rectangle(position.ToPoint(), new Point((int)Math.Max(item.NameLenght, item.DescriptionSize.X) + 24, (int)item.DescriptionSize.Y + 32)), Color.Green);
            sprite.Draw(new Sprite(), size, Color.Black);

            sprite.DrawString(Game1.fonts["font"], item.Name, position + new Vector2(12, 8), Color.Green);
            sprite.DrawString(Game1.fonts["font"], item.Description, position + new Vector2(12, 24), Color.Green);
        }
        public static void Draw(Vector2 position, Item item, int price, SpriteBatch sprite)
        {
            Rectangle size = new Rectangle(position.ToPoint() + new Vector2(8, 8).ToPoint(), new Point((int)Math.Max(item.NameLenght + 8, item.DescriptionSize.X + 8), (int)item.DescriptionSize.Y + 16));
            sprite.Draw(new Sprite(), new Rectangle(position.ToPoint(), new Point((int)Math.Max(item.NameLenght, item.DescriptionSize.X) + 24, (int)item.DescriptionSize.Y + 32)), Color.Green);
            sprite.Draw(new Sprite(), size, Color.Black);

            sprite.DrawString(Game1.fonts["font"], item.Name + " (" + price.ToString() + ")", position + new Vector2(12, 8), Color.Green);
            sprite.DrawString(Game1.fonts["font"], item.Description, position + new Vector2(12, 24), Color.Green);
        }
    }
}
