﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGame
{
    class BarArray:IDrawable
    {
        public Sprite BarStage { get; set; } = new Sprite("bar");
        public List<Resource> Resources { get; set; } = new List<Resource>();
        public BarArray(params Resource[] res)
        {
            foreach(Resource r in res)
            {
                Resources.Add(r);
            }
            Debbuger.OnCmd += Debbuger_OnCmd;
        }

        private void Debbuger_OnCmd(string[] cmd)
        {
            if (cmd[0] == "resource")
            {
                SetResource(cmd[1], int.Parse(cmd[2]));
            }
        }

        public Vector2 position = new Vector2(4,125);
        public void Draw(SpriteBatch sprite)
        {
            int offset = 0;
            foreach (Resource res in Resources)
            {
                for(int i = 0; i < res.GetBarStage(); i++)
                {
                    sprite.Draw(BarStage, position + new Vector2(BarStage.Size.Width + 2, 0) * i + new Vector2(0,offset), res.color);
                }
                offset += BarStage.Size.Height + 2;
            }
        }
        public float GetResource(string name)
        {
            return (from n in Resources where n.Name == name select n.Quantity).FirstOrDefault();
        }
        public void SetResource(string name, float amount)
        {
            (from n in Resources where n.Name == name select n).FirstOrDefault().Quantity = amount;
        }
        public void AddResource(string name, float amount)
        {
            (from n in Resources where n.Name == name select n).FirstOrDefault().Quantity += amount;
        }
        public void SubtractResource(string name, float amount)
        {
            (from n in Resources where n.Name == name select n).FirstOrDefault().Quantity -= amount;
        }
    }
}