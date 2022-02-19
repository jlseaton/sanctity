using Game.Core;

namespace Game.Realm
{
    public class Entity : Thing
    {
        public int UID { get; set; }

        public int AttackingTileID { get; set; }
        public int DeadTileID { get; set; }
        public MoveDirection Facing { get; set; }

        public string Title { get; set; }
        public string Homeland { get; set; }
        public string Diety { get; set; }
        public string Bio { get; set; }
        public string ImageName { get; set; }

        public Location Loc { get; set; }

        public long LastActionRound { get; set; }
        public int LastAttackerID { get; set; }
        public int LastTargetID { get; set; }

        public int SoundID { get; set; }
        public int VoiceID { get; set; }
        public int ThreatenSoundID { get; set; }
        public int AttackSoundID { get; set; }
        public int DeathSoundID { get; set; }

        public DateTime DeathTime { get; set; }

        public int FactionID { get; set; }
        public int FactionWeight { get; set; }
        public int Speed { get; set; }
        public int Initiative { get; set; }

        public EntityType Type { get; set; }
        public StateType State { get; set; }
        public GenderType Gender { get; set; }
        public SizeType Size { get; set; }
        public RaceType Race { get; set; }
        public ClassType Class { get; set; }

        public bool NotAttackable { get; set; }

        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Luck { get; set; }

        public int AlignmentCount { get; set; }
        public AlignmentType Alignment { get; set; }

        public int Experience { get; set; }
        public int Gold { get; set; }
        public int Stealth { get; set; }

        public Stats Stats { get; set; }

        public int HitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int ManaPoints { get; set; }
        public int MaxManaPoints { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public int MainHandID { get; set; }
        public Item MainHand { get; set; }
        public int OffHandID { get; set; }
        public Item OffHand { get; set; }

        public LootType LootClass { get; set; }
        public List<string> SpecificLoot { get; set; }

        public List<int> Skills { get; set; }
        public List<int> Spells { get; set; }

        public List<Resist> Resists { get; set; }
        public List<Item> Inventory { get; set; }

        public List<Item> Armor { get; set; }
        public int ArmorClass { get; set; }
        //public int EquippedArmorClass
        //{
        //    get
        //    {
        //        int ac = 0;
        //        if (Armor.Any())
        //        {
        //            foreach(var item in Armor)
        //            {
        //                ac += item.ArmorClass;
        //            }
        //        }
        //        return ac;
        //    }
        //}

        //Entity LastTarget = null;

        public Entity(int areaId = 0, int hexId = 1)
        {
            // Base damage
            MinDamage = 1;
            MaxDamage = 3;

            Loc = new Location() { AreaID = areaId, HexID = hexId };
            Stats = new Stats() { Name = Name };
        }

        public virtual void Die()
        {
            State = StateType.Dead;
            DeathTime = DateTime.Now;
        }

        public void EquipWeapon(Item weapon, bool offhand = false)
        {
            if (weapon.IsWeapon && !offhand)
            {
                if (offhand)
                {
                    OffHand = weapon.Clone();

                    //TODO: Add offhand swing chances to combat equations
                }
                else
                {
                    MainHand = weapon.Clone();

                    MinDamage = weapon.MinDamage;
                    MaxDamage = weapon.MaxDamage;
                }
            }
        }

        public bool AttemptToHide()
        {
            int stealthCheck = Randomizer.Next(0, 100);

            if (stealthCheck <= Stealth)
            {
                return true;
            }

            return false;
        }

        public Entity Clone()
        {
            return (Entity)this.MemberwiseClone();
        }
    }
}
