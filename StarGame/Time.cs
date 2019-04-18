using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Time : IUpdateable
    {
        public static int DeltaTime { get; private set; }
        static int start;
        public static event Action OnTick;
        private int tickCounter;
        private const int tickTime = 30;
        public void Update()
        {
            DeltaTime = (int)DateTime.Now.Millisecond - start;
            DeltaTime = DeltaTime > 0 ? DeltaTime : 0;
            start = DateTime.Now.Millisecond;
            tickCounter += DeltaTime;
            if (tickCounter > tickTime)
            {
                tickCounter = 0;
                OnTick();
            }
        }

        internal void Init()
        {
            start = DateTime.Now.Millisecond;
        }
    }
}
