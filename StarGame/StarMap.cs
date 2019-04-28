using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class StarMap : IDrawable, IUpdateable, IDisposable
    {
        public static List<Star> Stars { get; set; } = new List<Star>();
        private static Rectangle size = new Rectangle(0, 0, 970, 580);
        public static Star playerStar;
        public Vector2 Position { get; set; }
        public StarMap(Vector2 pos)
        {
            Position = pos;
            playerStar = new Star
            {
                Position = new Vector2(30,30),
                color = Color.Red
            };
            Stars.Add(playerStar);
            size.Location = Position.ToPoint();
        }

        public void Draw(SpriteBatch sprite)
        {
            if (isDisposed) return;
            foreach(Star star in Stars)
            {
                star.Position += Position;
                star.Draw(sprite);
                star.Position -= Position;
            }
            for(int i = 0; i < 360; i++)
            {
                Vector2 pp = Physics.GetForwardVector(i) * Player.JumpDistance;
                if (!size.Contains(pp + playerStar.Position + Position)) continue;
                sprite.Draw(new Sprite("WhitePixel"), pp + playerStar.Position + Position, Color.Gainsboro);
            }
        }
        public static void GenerateStars(int amount)
        {
            int minDist = 50;
            Random rng = new Random();
            for(int i = 0; i < amount; i++)
            {
                Vector2 pos = Vector2.Zero;
                do
                {
                    pos = new Vector2(rng.Next(size.Width), rng.Next(size.Height));
                }while((from n in Stars where Vector2.Distance(n.Position, pos) < minDist select n).Count() > 0);
                Stars.Add(new Star(pos));
            }
        }
        bool onePress;
        public void Update()
        {
            if (isDisposed) return;
            if (Math.Abs(Vector2.Distance(Input.GetMousePosition(), playerStar.Position + Position)) > Player.JumpDistance) return;
            if (Input.IsMouseKeyDown(0) && !onePress) {
                onePress = true;
                var j =(from n in Stars where Vector2.Distance(n.Position + Position + new Vector2(4, 4), Input.GetMousePosition()) < 5 select n).FirstOrDefault();
                if (j != null)
                {
                    if (j == playerStar) return;
                    if (Vector2.Distance(j.Position, playerStar.Position) > Player.JumpDistance) return;

                    if (!JumpAssistant.IsPlayerInVaidPosition(j.Position)) return;

                    JumpAssistant.Jump();
                }
            }
            if (Input.IsMouseKeyUp(0))
            {
                onePress = false;
            }
        }
        bool isDisposed;
        public void Dispose()
        {
            isDisposed = true;
        }
    }
    class Star : IDrawable
    {
        public Vector2 Position { get; set; }
        public Color color;
        public Star()
        {

        }
        public Star(Vector2 pos)
        {
            Position = pos;
            color = Color.Orange;
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(new Sprite("blip"), Position, color);
        }
    }
}
