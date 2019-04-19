using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Planet
    {
        public List<Planet> moons = new List<Planet>();
        public float Mass { get; protected set; }
        public float distance;
        public float Period { get { return _period; } set
            {
                _period = value;
                if(_period > 360)
                {
                    _period -= 360;
                }
            } }
        private float _period;
        public Sprite sprite;
        public object center;

        public Planet(Sprite sprite,float mass, float distance)
        {
            Mass = mass;
            this.sprite = sprite;
            this.distance = distance;
        }
    }
}
