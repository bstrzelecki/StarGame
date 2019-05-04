using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Sprite
    {
        public Texture2D Texture { get; protected set; }
        public Rectangle Size { get; protected set; }
        public Sprite(Texture2D sprite)
        {
            Texture = sprite;
            Size = sprite.Bounds;
        }
        public Sprite(string sprite)
        {
            if (Game1.textures.ContainsKey(sprite))
                Texture = Game1.textures[sprite];
            else
                Texture = Game1.textures["WhitePixel"];
            Size = Texture.Bounds;
        }
        public static implicit operator Texture2D(Sprite sprite)
        {
            return sprite.Texture;
        }
    }
}
