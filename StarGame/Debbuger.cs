using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace StarGame
{
    internal class Debbuger
    {
        [DllImport("kernel32")]
        private static extern bool AllocConsole();
        public delegate void Command(CommandCompund cmd);
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
            foreach (string cmd in cmds)
            {
                string[] rg = cmd.Split('.');
                string[] arg = rg[1].Split(' ');
                List<string> c = new List<string>();
                foreach (string n in arg)
                {
                    c.Add(n);
                }
                string t = c[0];
                c.RemoveAt(0);
                CommandCompund cp = new CommandCompund(rg[0], t, c.ToArray());
                OnCmd(cp);
            }
            cmds.Clear();
        }
    }

    internal class CommandCompund
    {
        public string Target { get; set; }
        public string Source { get; set; }
        public string[] Values { get; set; }

        public CommandCompund(string dest, string res, params string[] val)
        {
            Target = dest;
            Source = res;
            Values = val;
        }
        public bool Check(string a)
        {
            return Target == a;
        }
        public static implicit operator String(CommandCompund command)
        {
            return command.Source;
        }
        public int GetInt(int i)
        {
            int temp;
            if (int.TryParse(Values[i], out temp))
            {
                return temp;
            }
            else
            {
                Debug.WriteLine("Cont convert value to int !!!!");
            }
            return 0;
        }
        public bool GetBool(int i)
        {
            bool temp;
            if (bool.TryParse(Values[i], out temp))
            {
                return temp;
            }
            else
            {
                Debug.WriteLine("Cont convert value to bool !!!!");
            }
            return false;
        }
    }
}
