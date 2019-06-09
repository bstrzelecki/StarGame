using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    internal class Sprite
    {
        public Texture2D Texture { get; protected set; }
        public Rectangle Size { get; protected set; }
        public Sprite(Texture2D sprite)
        {
            Texture = sprite ?? Game1.textures["WhitePixel"];
            Size = sprite.Bounds;
        }
        public Sprite(string sprite)
        {
            if (Game1.textures.ContainsKey(sprite))
            {
                Texture = Game1.textures[sprite];
            }
            else
            {
                Texture = Game1.textures["WhitePixel"];
            }

            Size = Texture.Bounds;
        }
        public Sprite()
        {
            Texture = Game1.textures["WhitePixel"];
            Size = Texture.Bounds;
        }
        public static implicit operator Texture2D(Sprite sprite)
        {
            if(sprite == null)
            {
                return Game1.textures["WhitePixel"];
            }
            return sprite.Texture ;
        }
    }
}
