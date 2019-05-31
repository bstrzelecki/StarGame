namespace StarGame
{
    internal class SimpleArmor : Item
    {
        private int hp;
        public SimpleArmor(string name, int hull)
        {
            Name = name;
            Description = "Simple armor\nthat provides " + hull.ToString() + " aditional\nhullpoints";
            hp = hull;
            Graphic = new Sprite();
        }
        public override void Apply()
        {
            MainScene.player.armor = hp;
        }

        public override Item Clone()
        {
            return new SimpleArmor(Name, hp);
        }

        public override void Remove()
        {
            MainScene.player.armor = 10;
        }
    }
}
