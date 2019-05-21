using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class SimpleArmor : Item
    {
        int hp; 
        public SimpleArmor(string name , int hull)
        {
            Name = name;
            Description = "Simple armor\nthat provides " + hull.ToString() + "aditional\nhullpoints";
            hp = hull;
        }
        public override void Apply()
        {
            MainScene.player.armor = hp;
        }

        public override Item Clone()
        {
            return new SimpleArmor(Name, hp);
        }

        public override void Remove()
        {
            MainScene.player.armor = 10;
        }
    }
}
