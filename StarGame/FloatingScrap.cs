using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    class FloatingScrap : IUpdateable, IDrawable, IDisposable
    {
        public Vector2 position;
        public float direction;
        private Sprite image;
        public FloatingScrap()
        {      
            position = new Vector2(MainScene.rng.Next(-50000, 50000), MainScene.rng.Next(-50000, 50000));
            direction = MainScene.rng.Next(0, 360);
            image = new Sprite();
        }
        public bool IsDisposed { get; set; }
        public void Dispose()
        {
            image = null;
            IsDisposed = true;
        }

        public void Draw(SpriteBatch sprite)
        {
            if (IsDisposed) return;
            sprite.Draw(image, Input.cameraOffset + position, Color.White);
        }

        public void Update()
        {
            if (IsDisposed) return;
            position = Physics.GetForwardVector(direction) * 50;
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
