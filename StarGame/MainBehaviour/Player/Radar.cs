﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace StarGame
{
    internal class Radar : IDrawable
    {
        public Sprite sprite;
        public Vector2 position;
        public List<Blip> blips = new List<Blip>();
        public float scale = 1000;
        public void AddBlip(Vector2 player, Vector2 blip)
        {
            blip -= player;
            blips.Add(new Blip(blip / scale, Color.White));
        }
        public void AddBlip(Vector2 player, Vector2 blip, Color color)
        {
            blip -= player;
            blips.Add(new Blip(blip / scale, color));
        }
        public Radar(Vector2 position)
        {
            this.position = position;
            sprite = new Sprite(Game1.textures["radar"]);
            Debbuger.OnCmd += Debbuger_OnCmd;
        }

        private void Debbuger_OnCmd(CommandCompund cmd)
        {
            if (cmd.Check("radar"))
            {
                if (cmd == "scale")
                {
                    scale = cmd.GetInt(0);
                }
            }
        }

        public void Clear()
        {
            blips.Clear();
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(this.sprite, position, Color.White);
            foreach (Blip blip in blips)
            {
                if (blip.Position.Length() < 11)
                {
                    sprite.Draw(Game1.textures["blip"], position + blip.Position * 9 + new Vector2(120, 120), blip.Type);
                }
            }
            DrawSimulation(sprite);
        }

        private void DrawSimulation(SpriteBatch sprite)
        {
            foreach (Vector2 blip in MainScene.proxy.blips)
            {
                Vector2 b;
                b = blip - MainScene.player.position;
                b /= scale;
                if (b.Length() < 11)
                {
                    sprite.Draw(Game1.textures["WhitePixel"], new Rectangle((position + b * 9 + new Vector2(123, 123)).ToPoint(), new Point(3, 3)), Color.Blue);
                }
            }
        }
    }

    internal class Blip
    {
        public Vector2 Position { get; set; }
        public Color Type { get; set; }

        public Blip(Vector2 pos, Color color)
        {
            Position = pos;
            Type = color;
        }
    }
}
