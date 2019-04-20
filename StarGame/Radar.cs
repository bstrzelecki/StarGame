using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Radar : IDrawable
    {
        public Sprite sprite;
        public Vector2 position;
        private Vector2 center;
        public List<Vector2> blips = new List<Vector2>();
        public float scale = 1000;
        public void AddBlip(Vector2 player, Vector2 blip)
        {
            blip -= player;
            blips.Add(blip / scale);
        }
        public Radar(Vector2 position)
        {
            this.position = position;
            sprite = new Sprite(Game1.textures["radar"]);
        }
        public void Clear()
        {
            blips.Clear();
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(this.sprite, position, Color.White);
            foreach(Vector2 blip in blips)
            {
                if(blip.Length() < 11)
                {
                    sprite.Draw(Game1.textures["blip"], position + blip * 9 + new Vector2(120,120), Color.White);
                }
            }
            foreach (Vector2 blip in MainScene.proxy.blips)
            {
                Vector2 b;
                b = blip - MainScene.player.position;
                b /= scale;
                if (b.Length() < 11)
                {
                    sprite.Draw(Game1.textures["WhitePixel"],new Rectangle((position + b * 9 + new Vector2(123, 123)).ToPoint(),new Point(3,3)), Color.Blue);
                }
            }
        }
    }
}
