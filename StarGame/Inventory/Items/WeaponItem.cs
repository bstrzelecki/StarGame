namespace StarGame
{
    internal class WeaponItem : Item
    {
        private Weapon weapon;
        public WeaponItem(string name, string desc, Sprite sprite, Weapon weapon)
        {
            InventorySlot = Slot.Weapon;
            Name = name;
            Description = desc;
            Graphic = sprite;
            this.weapon = weapon;
        }

        public override void Apply()
        {
            MainScene.player.rmb = weapon;
        }

        public override Item Clone()
        {
            return new WeaponItem(Name, Description, Graphic, weapon);
        }

        public override void Remove()
        {
            MainScene.player.rmb = null;
        }
    }
}
