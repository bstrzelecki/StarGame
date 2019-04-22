using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class SpeedOMeter : IDrawable
    {
        public Sprite sprite;
        public Sprite acceleration, velocity;
        private Player player;
        private float scale = .5f;
        public Vector2 position = new Vector2(0,0);
        public Vector2 center = new Vector2(57, 47);

        public SpeedOMeter()
        {
            sprite = new Sprite("meter");
            acceleration = new Sprite("blip");
            velocity = new Sprite("blip");
            player = MainScene.player;
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(this.sprite, position, Color.White);
            //if((9* player.physics.acceleration / scale).Length()<5)
            Vector2 acc = player.physics.acceleration / scale;
            if (acc.Length() < 34)
            {
                sprite.Draw(acceleration, center + acc * 9, Color.Red);
            }
            else
            {
                acc.Normalize();
                sprite.Draw(acceleration, center + acc * 34, Color.Red);
            }
            Vector2 vel = player.physics.velocity / scale;
            if(vel.Length() < 34)
            {
                sprite.Draw(velocity, center + vel, Color.Blue);
            }
            else
            {
                vel.Normalize();
                sprite.Draw(velocity, center + vel * 34, Color.Blue);

            }
        }
    }
}
