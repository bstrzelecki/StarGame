﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Projectile : IDrawable, ICloneable, IDisposable
    {
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Decay { get; set; } = 10;
        public Physics physics = new Physics();
        public Projectile(Sprite sprite)
        {
            Sprite = sprite;
        }
        public object Clone()
        {
            return new Projectile(Sprite);
        }

        public virtual void Draw(SpriteBatch sprite)
        {
            if (Decay < 0) Dispose();
            if(!IsDisposed)
                sprite.Draw(Sprite, Position + Input.cameraOffset,null, Color.White, Rotation, new Vector2(Sprite.Size.Width/2,Sprite.Size.Height/2),1,SpriteEffects.None,0);
        }
        public bool IsDisposed { get; set; }
        public void Dispose()
        {
            Sprite = null;
            Position = Vector2.Zero;
            Rotation = 0;
            physics = null;
            IsDisposed = true;
        }
    }
}