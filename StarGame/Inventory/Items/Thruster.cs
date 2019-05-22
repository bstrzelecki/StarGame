namespace StarGame
{
    internal class Thruster : Item
    {
        public Thruster()
        {
            Graphic = new Sprite("th");
            Name = "Thruster";
            Description = "Slightly improves yout speed";
            InventorySlot = Slot.Thruster;
        }

        private float diff;
        public Thruster(string name, float change)
        {
            diff = change;
            Graphic = new Sprite("th");
            Name = name;
            Description = "Slightly improves yout speed";
            InventorySlot = Slot.Thruster;
        }
        public override void Apply()
        {
            MainScene.player.speed = diff == 0 ? 1.6f : diff;
        }

        public override Item Clone()
        {
            return new Thruster();
        }

        public override void Remove()
        {
            MainScene.player.speed = 1.2f;
        }
    }
}
