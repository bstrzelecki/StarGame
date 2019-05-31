using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace StarGame
{
    internal class FloatingScrap : IUpdateable, IDrawable, IDisposable
    {
        public Vector2 position;
        public float direction;
        private Sprite image;
        public FloatingScrap()
        {
            position = new Vector2(MainScene.rng.Next(-5000, 5000), MainScene.rng.Next(-5000, 5000));
            direction = MainScene.rng.Next(0, 360);
            image = new Sprite("w");
        }
        public bool IsDisposed { get; set; }
        public void Dispose()
        {
            image = null;
            IsDisposed = true;
        }

        public void Draw(SpriteBatch sprite)
        {
            if (IsDisposed)
            {
                return;
            }

            sprite.Draw(image, Input.cameraOffset + position, Color.White);
        }

        public void Update()
        {
            if (IsDisposed)
            {
                return;
            }

            position += Physics.GetForwardVector(direction) * 5;
            if (MainScene.player.collider.Contains(position))
            {
                if (MainScene.rng.Next(10) == 0)
                {
                    MainScene.inventory.AddItem(Database.GetRandomItems(1).First());
                }
                else
                {
                    Notifications.DisplayNotification("Its only a scrap");
                }
                Dispose();
            }
        }
    }
}
