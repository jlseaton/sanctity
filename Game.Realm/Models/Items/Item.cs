using Game.Core;

namespace Game.Realm
{
    public class Item : Thing
    {
        public int BaseID { get; set; }

        public ItemType Type { get; set; }

        public DamageType Damage { get; set; }

        public int Value { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Range { get; set; }
        public bool IsWeapon
        {
            get
            {
                if (MinDamage > 0 && MaxDamage > 0)
                    return true;

                return false;
            }
        }

        public int ToHitBonus { get; set; }
        public int DamageBonus { get; set; }

        public int ArmorClass { get; set; }

        public int Durability { get; set; }

        public List<int> Effects { get; set; }

        public Item Clone()
        {
            return (Item)this.MemberwiseClone();
        }
    }
}
