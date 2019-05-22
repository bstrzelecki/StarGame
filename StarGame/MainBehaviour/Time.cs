using System;

namespace StarGame
{
    internal class Time : IUpdateable
    {
        public static int DeltaTime { get; private set; }

        private static int start;
        public static event Action OnTick;
        private int tickCounter;
        private const int tickTime = 30;
        public static bool IsStopped { get; set; }
        public void Update()
        {
            if (IsStopped)
            {
                return;
            }

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
