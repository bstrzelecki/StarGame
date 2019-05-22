using Microsoft.Xna.Framework;

namespace StarGame
{
    internal class UtilitySlot
    {
        public Slot slot;
        public Item item;
        public Rectangle size;
        public UtilitySlot(Slot slot, Item item)
        {
            this.slot = slot;
            this.item = item;

        }
        public void ApplyCollisions(Vector2 pos)
        {
            size = new Rectangle(pos.ToPoint(), new Sprite("system").Size.Size);
        }
    }
}
