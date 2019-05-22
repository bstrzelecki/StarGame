using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace StarGame
{
    internal class BarArray : IDrawable
    {
        public Sprite BarStage { get; set; } = new Sprite("bar");
        public List<Resource> Resources { get; set; } = new List<Resource>();
        public BarArray(params Resource[] res)
        {
            foreach (Resource r in res)
            {
                Resources.Add(r);
            }
            Debbuger.OnCmd += Debbuger_OnCmd;
        }

        private void Debbuger_OnCmd(CommandCompund cmd)
        {
            if (cmd.Check("resource"))
            {
                SetResource(cmd, cmd.GetInt(0));
            }
        }

        public Vector2 position = new Vector2(4, 125);
        public void Draw(SpriteBatch sprite)
        {
            int offset = 0;
            foreach (Resource res in Resources)
            {
                for (int i = 0; i < res.GetBarStage(); i++)
                {
                    sprite.Draw(BarStage, position + new Vector2(BarStage.Size.Width + 2, 0) * i + new Vector2(0, offset), res.color);
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
