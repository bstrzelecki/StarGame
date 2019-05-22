using Microsoft.Xna.Framework;

namespace StarGame
{
    internal class SimulationProxy
    {
        public Player blip;
        public int simulationSpeed = 5;
        public int simulationSteps = 25;
        public Vector2[] blips;
        public int count = 0;

        public SimulationProxy()
        {
            blip = new Player();
            Time.OnTick += Time_OnTick;
            blips = new Vector2[simulationSteps];
            for (int i = 0; i < simulationSteps; i++)
            {
                blips[i] = new Vector2(0, 0);
            }
        }
        private void Time_OnTick()
        {
            for (int i = 0; i < simulationSpeed; i++)
            {
                blip.Time_OnTick();
            }
            if (count > simulationSteps - 1)
            {
                count = 0;
                blip = (Player)MainScene.player.Clone();
            }
            blips[count] = blip.position;
            count++;
        }
    }
}
