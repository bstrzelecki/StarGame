using System;
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
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(Game1.textures["starmap"], Input.cameraOffset, Color.White);
            player.Draw(sprite);
        }

        public void Update()
        {
            player.Update();
            Input.cameraOffset = player.screenPosition-player.position;
        }

        internal void Init()
        {
            player = new Player();
        }
    }
}
