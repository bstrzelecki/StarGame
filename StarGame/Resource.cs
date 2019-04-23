using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Resource
    {
        public const int BarScale = 10;
        public int fuel = 100;
        public int oxygen = 100;
        public int hull = 100;
        public int electric = 100;
        public Dictionary<string, int> other = new Dictionary<string, int>();
        public Resource()
        {

        }
        private int ToBar(int value)
        {
            return value / BarScale;
        }
    }
}
