using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace StarGame
{
    internal class EnemyStation : Planet, IUpdateable, IDrawable
    {
        public bool HasJammer { get; set; }
        public int hp = 100;
        public Weapon weapon; 
        private readonly ITargetable target = MainScene.player;
        public EnemyStation(Sprite sprite, float mass, float distance) : base(sprite, mass, distance)
        {
            weapon = new RocketLauncher(new HomingMissle(null, new Sprite("plasma")));
        }

        private Vector2 _position = Vector2.Zero;
        public Vector2 Position
        {
            get
            {
                _position = Physics.GetForwardVector(Period) * distance + (center is StarSystem str ? str.position : (center is Planet pl ? ((StarSystem)pl.center).position : Vector2.Zero)) + Input.cameraOffset;
                return _position;
            }
        }

        public void Fire()
        {
            if (weapon != null)
            {
                weapon.SpawnProjectile(Position - Input.cameraOffset, Input.GetDegree((float)Math.Sin((target.GetPosition() - Position - Input.cameraOffset).Length())), new Vector2(1,1));
            }
        }

        public void Update()
        {
            //if (Vector2.Distance(Position, MainScene.player.position) < 2000)
            {
                Fire();
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            weapon.DrawProjectile(sprite);
        }
    }
}
