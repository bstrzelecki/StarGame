using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class EnemyStation : Planet, IUpdateable
    {
        public bool HasJammer { get; set; }
        public int hp= 100;
        public Weapon weapon;
        public EnemyStation(Sprite sprite, float mass, float distance) : base(sprite, mass, distance)
        {
            Game1.RegisterUpdate(this);
        }

        private Vector2 _position = Vector2.Zero;
        public Vector2 Position {
            get
            {
                _position = Physics.GetForwardVector(Period) * distance + (center is StarSystem str ? str.position : (center is Planet pl ? ((StarSystem)pl.center).position : Vector2.Zero)) + Input.cameraOffset;
                return _position;
            }
        }

        public void Fire()
        {
            if(weapon != null)
                weapon.SpawnProjectile(Position, Input.GetDegree((float)Math.Sin((MainScene.player.position - Position).Length())),Vector2.Zero);
        }

        public void Update()
        {
            if(Vector2.Distance(Position, MainScene.player.position) < 2000)
            {
                Fire();
            }
        }
    }
}
