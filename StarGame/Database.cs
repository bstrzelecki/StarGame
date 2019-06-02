using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StarGame
{
    internal class Database
    {
        public static Dictionary<string, Item> Items { get; protected set; } = new Dictionary<string, Item>();

        public static Item EmptyItem { get; } = new GenericItem(Slot.Armor, "Empty Item", new Sprite("blip"));

        public static Item GetItem(string name)
        {
            if (Items.ContainsKey(name))
            {
                return Items[name];
            }
            else
            {
                Debug.WriteLine("Item does not exist");
            }
            return new GenericItem(Slot.Armor, "Empty Item", new Sprite("blip"));
        }
        public static List<Item> GetRandomItems(int amount)
        {
            Random rng = new Random();
            List<Item> i = new List<Item>();
            do
            {
                foreach (string key in Items.Keys)
                {
                    if (i.Count > amount)
                    {
                        break;
                    }

                    if (rng.Next(15) == 1)
                    {
                        i.Add(Items[key]);
                    }
                }
            } while (i.Count < amount);
            return i;
        }
        protected static void AddItem(string key, Item item)
        {
            if (!Items.ContainsKey(key))
            {
                Items.Add(key, item);
            }
        }
        public static void Init()
        {
            AddItem("thruster", new Thruster());
            AddItem("blaster", new PlasmaBlaster());
            AddItem("laser", new WeaponItem("Laser Beam", "Fires single beam.\nDeals 1dmg per hit.", new Sprite("w"), new LaserBeam(new Projectile(new Sprite("blip")))));
            AddItem("missle", new WeaponItem("Homing Missle Lanucher", "Launches single homing missle.\nDeals 1dmg per hit.", new Sprite("w"), new RocketLauncher(new HomingMissle(null, new Sprite("plasma")))));
        }
    }
}
