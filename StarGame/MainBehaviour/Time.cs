using System;

namespace StarGame
{
    internal class Time : IUpdateable
    {
        /// <summary>
        /// Difference between two ticks
        /// </summary>
        public static int DeltaTime { get; private set; }

        private static int start;
        /// <summary>
        /// Event thaty runs every tick
        /// </summary>
        public static event Action OnTick;
        private int tickCounter;
        private const int tickTime = 30;
        /// <summary>
        /// Returns true if time is frozen
        /// </summary>
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
