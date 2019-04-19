﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    class MainScene : IDrawable, IUpdateable
    {
        public Player player;
        public StarSystem sun;
        public List<Tile> background = new List<Tile>();
        public Texture2D tile;
        public void Draw(SpriteBatch sprite)
        {
            //Texture2D texture = Game1.textures["tile"];
            //for (int x = 0; x < Game1.graphics.GraphicsDevice.Viewport.Width/texture.Width; x++)
            //{

            //}
            foreach(Tile tile in background)
            {
                sprite.Draw(tile.sprite, tile.position * this.tile.Width + Input.cameraOffset, Color.White);
            }
            player.Draw(sprite);
            sun.Draw(sprite);
            sprite.DrawString(Game1.fonts["font"], player.position.ToString(), new Vector2(50, 50), Color.Red);
            sprite.DrawString(Game1.fonts["font"], Vector2.Distance(player.position, sun.position).ToString(), new Vector2(50, 100), Color.Red);
        }

        public void Update()
        {
            player.Update();
            Input.cameraOffset = -player.position + player.screenPosition;
            Vector2 playerInBackground = player.position/tile.Width;
            int backgroundWidth = Game1.graphics.GraphicsDevice.Viewport.Width / tile.Width;
            int backgroundHeight = Game1.graphics.GraphicsDevice.Viewport.Height / tile.Width;
            for (int x = (int)playerInBackground.X-backgroundWidth - 1; x < backgroundWidth + (int)playerInBackground.X+1; x++)
            {
                for (int y = (int)playerInBackground.Y-backgroundHeight - 1; y < backgroundHeight + (int)playerInBackground.Y + 1; y++)
                {
                    if ((from n in background where n.position == new Vector2(x, y) select n).Count() > 0) continue;
                    background.Add(new Tile(new Vector2(x, y)));
                }
            }
            List<Tile> toDelete = new List<Tile>();
            foreach(var t in (from n in background where Vector2.Distance(n.position,playerInBackground) > 5 select n))
            {
                toDelete.Add(t);
            }
            foreach(Tile t in toDelete)
            {
                background.Remove(t);
            }
        }

        internal void Init()
        {
            player = new Player();
            sun = new StarSystem(new Sprite(Game1.textures["planet1"]), 10);
            sun.AddPlanet(new Planet(new Sprite(Game1.textures["planet2"]), 10, 1000));
            sun.position = new Vector2(600, 600);
            tile = Game1.textures["tile"];
        }
    }
}