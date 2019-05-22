using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarGame
{
    internal class StarMap : IDrawable, IUpdateable, IDisposable
    {
        public static List<Star> Stars { get; set; } = new List<Star>();
        private static Rectangle size = new Rectangle(0, 0, 970, 580);
        public static Star playerStar = new Star(new Vector2(30, 30));
        public Vector2 Position { get; set; }
        public StarMap(Vector2 pos)
        {
            Position = pos;
            playerStar.color = Color.Red;
            Stars.Add(playerStar);
            size.Location = Position.ToPoint();
        }
        public static void Init()
        {
            Debbuger.OnCmd += Debbuger_OnCmd;
        }

        private static void Debbuger_OnCmd(CommandCompund cmd)
        {
            if (cmd.Check("starmap"))
            {
                if (cmd == "recreate")
                {
                    Stars.Clear();
                    GenerateStars(150);
                    Stars.Add(playerStar);
                }
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            if (isDisposed)
            {
                return;
            }

            foreach (Star star in Stars)
            {
                star.Position += Position;
                star.Draw(sprite);
                star.Position -= Position;
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 pp = Physics.GetForwardVector(i) * Player.JumpDistance;
                if (!size.Contains(pp + playerStar.Position + Position))
                {
                    continue;
                }

                sprite.Draw(new Sprite("WhitePixel"), pp + playerStar.Position + Position, Color.Gainsboro);
            }
        }
        public static void GenerateStars(int amount)
        {
            int minDist = 30;
            Random rng = new Random();
            for (int i = 0; i < amount; i++)
            {
                Vector2 pos = Vector2.Zero;
                do
                {
                    pos = new Vector2(rng.Next(size.Width), rng.Next(size.Height));
                } while ((from n in Stars where Vector2.Distance(n.Position, pos) < minDist select n).Count() > 0);
                Stars.Add(new Star(pos));
            }
        }

        private bool onePress;
        public void Update()
        {
            if (isDisposed)
            {
                return;
            }

            if (Math.Abs(Vector2.Distance(Input.GetMousePosition(), playerStar.Position + Position)) > Player.JumpDistance)
            {
                return;
            }

            if (Input.IsMouseKeyDown(0) && !onePress)
            {
                onePress = true;
                var j = (from n in Stars where Vector2.Distance(n.Position + Position + new Vector2(4, 4), Input.GetMousePosition()) < 5 select n).FirstOrDefault();
                if (j != null)
                {
                    if (j == playerStar)
                    {
                        return;
                    }

                    if (Vector2.Distance(j.Position, playerStar.Position) > Player.JumpDistance)
                    {
                        return;
                    }

                    if (!JumpAssistant.IsPlayerInVaidPosition(j.Position))
                    {
                        return;
                    }

                    if (!JumpAssistant.DeductPower(100))
                    {
                        return;
                    }

                    JumpAssistant.Jump();
                    MainScene.ui.SetView(DisplayedUI.None);
                    playerStar.color = Color.Orange;
                    j.color = Color.Red;
                    playerStar = j;


                }
            }
            if (Input.IsMouseKeyUp(0))
            {
                onePress = false;
            }
        }

        private bool isDisposed;
        public void Dispose()
        {
            isDisposed = true;
        }
    }

    internal class Star : IDrawable
    {
        public Vector2 Position { get; set; }
        public Color color;
        //TODO:Add star system if visited
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
