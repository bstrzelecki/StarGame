using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace StarGame
{
    internal class Player : IDrawable, ICloneable, IUpdateable, ITargetable
    {
        public Sprite sprite;
        public Vector2 screenPosition;
        public Vector2 position;
        public float Rotation { get { return Input.GetDegree(_rotation); } set { _rotation = Input.GetRads(value); } }
        public static float JumpDistance { get; set; } = 120;
        private float _rotation;
        public Rectangle collider;
        public Physics physics = new Physics();
        public float mass = 10;
        public float speed = 1.2f;
        public int armor = 10;
        public Weapon rmb;
        private bool useGravity = true;
        public Player()
        {
            sprite = new Sprite(Game1.textures["player"]);
            collider = sprite.Texture.Bounds;
            screenPosition = new Vector2(Game1.graphics.PreferredBackBufferWidth / 2, Game1.graphics.PreferredBackBufferHeight / 2);
            Time.OnTick += Time_OnTick;

            Projectile proj = new Projectile(new Sprite("plasma"));
            Weapon wep = new Blaster(proj);

            rmb = wep;

            Debbuger.OnCmd += Debbuger_OnCmd;
        }

        private void Debbuger_OnCmd(CommandCompund cmd)
        {
            if (cmd.Check("player"))
            {
                if (cmd == "usegravity")
                {
                    useGravity = cmd.GetBool(0);
                }
                if (cmd == "position")
                {
                    position = new Vector2(cmd.GetInt(0), cmd.GetInt(1));
                }
            }
        }

        public void Time_OnTick()
        {
            position += physics.velocity;
            Rotation += physics.GetDeltaRotation();
            HandleUserInput();
            if (useGravity)
            {
                ApplyGravity(MainScene.sun);
            }

            RenewPower();
        }

        private void HandleUserInput()
        {
            if (MainScene.barArray.GetResource("fuel") > 0)
            {
                if (Input.IsKeyDown(Keys.W))
                {
                    physics.acceleration += Physics.GetForwardVector(Rotation) * speed;
                    MainScene.barArray.SubtractResource("fuel", 0.001f);
                }
                if (Input.IsKeyDown(Keys.S))
                {
                    physics.acceleration -= Physics.GetForwardVector(Rotation) * 0.2f * speed;
                    MainScene.barArray.SubtractResource("fuel", 0.001f);
                }
                if (Input.IsKeyDown(Keys.A))
                {
                    physics.deltaRotation -= speed * 0.1f;
                    MainScene.barArray.SubtractResource("fuel", 0.0005f);
                }
                if (Input.IsKeyDown(Keys.D))
                {
                    physics.deltaRotation += speed * 0.1f;
                    MainScene.barArray.SubtractResource("fuel", 0.0005f);
                }
            }
        }

        private void RenewPower()
        {
            float resupply = 10 / Vector2.Distance(MainScene.sun.position, position);
            if (resupply > 0.01f)
            {
                if (MainScene.barArray.GetResource("power") + resupply <= 100)
                {
                    MainScene.barArray.AddResource("power", resupply);
                }
                else if (MainScene.barArray.GetResource("power") != 100)
                {
                    float temp = 100 - MainScene.barArray.GetResource("power");
                    if (temp <= resupply)
                    {
                        MainScene.barArray.AddResource("power", temp);
                    }
                }
            }
        }

        public void ApplyGravity(StarSystem system)
        {

            float force = Physics.G * (mass * system.StarMass) / (float)Math.Pow(Vector2.Distance(position, system.position), 2);
            physics.acceleration += force * (-position + system.position);

            foreach (Planet planet in system.planets)
            {
                force = Physics.G * (mass * planet.Mass) / (float)Math.Pow(Vector2.Distance(position, Physics.GetForwardVector(planet.Period) * planet.distance + system.position), 2);
                physics.acceleration += force * (-position + Physics.GetForwardVector(planet.Period) * planet.distance + system.position);
            }

        }
        public void Draw(SpriteBatch sprite)
        {
            if (rmb != null)
            {
                rmb.DrawProjectile(sprite);
            }

            sprite.Draw(this.sprite, screenPosition, null, Color.White, _rotation, new Vector2(collider.Width / 2, collider.Height / 2), Vector2.One, SpriteEffects.None, 0);
            //sprite.Draw(new Sprite(), collider, Color.Red);
        }


        public object Clone()
        {
            Player p = new Player
            {
                physics = (Physics)physics.Clone(),
                position = position
            };
            return p;
        }

        public void Update()
        {
            collider.Location = position.ToPoint();
            if (rmb != null && Input.IsMouseKeyDown(2))
            {
                rmb.SpawnProjectile(position, Rotation, physics.velocity);
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
