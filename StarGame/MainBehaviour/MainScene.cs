using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StarGame
{
    class MainScene : IDrawable, IUpdateable
    {
        public static Player player;
        public static StarSystem sun;
        public List<Tile> background = new List<Tile>();
        public Texture2D tile;
        public Radar radar;
        public static SimulationProxy proxy;
        public SpeedOMeter meter;
        public static BarArray barArray;
        public static UIController ui;
        public static Inventory inventory;
        public void Draw(SpriteBatch sprite)
        {
            foreach (Tile tile in background)
            {
                sprite.Draw(tile.sprite, tile.position * this.tile.Width + Input.cameraOffset, Color.White);
            }
            DrawObjects(sprite);
        }

        private void DrawObjects(SpriteBatch sprite)
        {
            player.Draw(sprite);
            sun.Draw(sprite);
            radar.Draw(sprite);
            meter.Draw(sprite);
            barArray.Draw(sprite);
            ui.Draw(sprite);
            Notifications.Draw(sprite);
        }

        public void Update()
        {
            Input.cameraOffset = -player.position + player.screenPosition;
            Vector2 playerInBackground = player.position / tile.Width;
            SetupBackground(playerInBackground);
            RenderRadar();
            UpdateObjects();
            HandleInput();
            ExeCommands();

        }

        private static void ExeCommands()
        {
            if (Input.IsKeyDown(Keys.C))
            {
                Debbuger.OpenConsole();
            }
            Debbuger.ExecuteCommands();
        }

        private static void HandleInput()
        {
            if (Input.IsKeyDown(Keys.M))
            {
                ui.SetView(DisplayedUI.StarMap);
            }
            if (Input.IsKeyDown(Keys.I))
            {
                ui.SetView(DisplayedUI.Inventory);
            }
            if (Input.IsKeyDown(Keys.N))
            {
                ui.SetView(DisplayedUI.None);
            }
            if (ui.UI != DisplayedUI.None)
            {
                Time.IsStopped = true;
            }
            else
            {
                Time.IsStopped = false;
            }
        }

        private static void UpdateObjects()
        {
            player.Update();
            ui.Update();
        }

        private void RenderRadar()
        {
            radar.Clear();
            radar.AddBlip(player.position, sun.position, Color.BurlyWood);

            foreach (Planet planet in sun.planets)
            {
                radar.AddBlip(player.position, Physics.GetForwardVector(planet.Period) * planet.distance + sun.position);
                foreach (Planet moon in planet.moons)
                {
                    radar.AddBlip(player.position, Physics.GetForwardVector(moon.Period) * moon.distance + Physics.GetForwardVector(planet.Period) * planet.distance + sun.position);
                }
            }
        }

        private void SetupBackground(Vector2 playerInBackground)
        {
            int backgroundWidth = Game1.graphics.GraphicsDevice.Viewport.Width / tile.Width;
            int backgroundHeight = Game1.graphics.GraphicsDevice.Viewport.Height / tile.Width;
            for (int x = (int)playerInBackground.X - backgroundWidth - 1; x < backgroundWidth + (int)playerInBackground.X + 1; x++)
            {
                for (int y = (int)playerInBackground.Y - backgroundHeight - 1; y < backgroundHeight + (int)playerInBackground.Y + 1; y++)
                {
                    if ((from n in background where n.position == new Vector2(x, y) select n).Count() > 0) continue;
                    background.Add(new Tile(new Vector2(x, y)));
                }
            }
            List<Tile> toDelete = new List<Tile>();
            foreach (var t in (from n in background where Vector2.Distance(n.position, playerInBackground) > 5 select n))
            {
                toDelete.Add(t);
            }
            foreach (Tile t in toDelete)
            {
                background.Remove(t);
            }
        }

        internal void Init()
        {
            player = new Player();

            RandomSpaceGenerator gen = new RandomSpaceGenerator();
            sun = gen.Build();
            sun.position = new Vector2(6000, 6000);

            tile = Game1.textures["tile"];
            radar = new Radar(new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width-250, Game1.graphics.GraphicsDevice.Viewport.Height-250));
            proxy = new SimulationProxy();
            meter = new SpeedOMeter();
            barArray = new BarArray(new Resource("fuel", Color.Green),new Resource("oxygen",Color.Blue),new Resource("power",Color.Yellow),new Resource("hull", Color.Red));
            ui = new UIController();
            inventory = new Inventory();
            StarMap.GenerateStars(150);
            StarMap.Init();
        }
    }
}
