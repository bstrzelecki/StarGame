using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarGame
{
    class Debbuger
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();
        public delegate void Command(string[] cmd); 
        public static event Command OnCmd;
        private static List<string> cmds = new List<string>();
        public static void OpenConsole()
        {
            AllocConsole();
            Thread th = new Thread(() =>
            {
                while (true)
                {
                    cmds.Add(Console.ReadLine());
                }
            });
            th.Start();
        }
        public static void ExecuteCommands()
        {
            foreach(string cmd in cmds)
            {
                string[] rg = cmd.Split('.');
                string[] arg = rg[1].Split(' ');
                List<string> c = new List<string>();
                c.Add(rg[0]);
                foreach(string n in arg)
                {
                    c.Add(n);
                }
                OnCmd(c.ToArray());
            }
            cmds.Clear();
        }
    }
}
