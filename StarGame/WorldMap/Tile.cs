using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Tile
    {
        public Sprite sprite;
        public Vector2 position;

        public Tile(Vector2 position)
        {
            this.position = position;
            sprite = new Sprite(Game1.textures["tile"]);
        }

    }
}
