using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Registry
    {
        public static MainScene scene = new MainScene();
        public static Time time = new Time();
        public static void LoadContent(Game1 game)
        {

        }
        public static void RegisterUpdates()
        {
            Game1.RegisterUpdate(scene);
            Game1.RegisterUpdate(time);
        }
        public static void RegisterRenderers()
        {
            Game1.RegisterRenderer(scene);
        }

        internal static void Init()
        {
            scene.Init();
            time.Init();
        }
    }
}
