using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class EnemyStation : Planet
    {
        public bool HasJammer { get; set; }
        public int hp= 100;
        public Weapon weapon;
        public EnemyStation(Sprite sprite, float mass, float distance) : base(sprite, mass, distance)
        {

        }
        
        public void Fire()
        {
            Vector2 position;
            position = Physics.GetForwardVector(Period) * distance + (center is StarSystem str ? str.position : (center is Planet pl ? ((StarSystem)pl.center).position : Vector2.Zero)) + Input.cameraOffset;
            if(weapon != null)
                weapon.SpawnProjectile(position, Input.GetDegree((float)Math.Sin((MainScene.player.position - position).Length())),Vector2.Zero);
        }
        
    }
}
