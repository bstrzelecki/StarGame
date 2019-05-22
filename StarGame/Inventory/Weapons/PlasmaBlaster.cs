namespace StarGame
{
    internal class PlasmaBlaster : Item
    {
        public PlasmaBlaster()
        {
            InventorySlot = Slot.Weapon;
            Name = "Plasma Blaster";
            Description = "Simple weapon.\nFires single bullet per 20 ticks.\nShot costs 5 power.";
            Graphic = new Sprite("w");
        }
        public override void Apply()
        {
            MainScene.player.rmb = new Blaster(new Projectile(new Sprite("plasma")));
        }

        public override Item Clone()
        {
            return new PlasmaBlaster();
        }

        public override void Remove()
        {
            MainScene.player.rmb = null;
        }
    }
}
