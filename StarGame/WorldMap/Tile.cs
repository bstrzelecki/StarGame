using Microsoft.Xna.Framework;

namespace StarGame
{
    internal class Tile
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
