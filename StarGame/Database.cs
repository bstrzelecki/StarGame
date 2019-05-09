using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Database
    {
        public static Dictionary<string, Item> Items { get; protected set; } = new Dictionary<string, Item>();

        public Item GetItem(string name)
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
        protected void AddItem(string key, Item item)
        {
            if(!Items.ContainsKey(key))
                Items.Add(key, item);
        }
        public void Init()
        {
            AddItem("thruster", new Thruster());
        }
    }
}
