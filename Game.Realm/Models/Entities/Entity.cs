using Game.Core;
using System.Text;

namespace Game.Realm
{
    public class Entity : Thing
    {
        public Loc Loc { get; set; }
        public Loc StartLoc { get; set; }
        public MoveDirection Facing { get; set; }

        public string Surname { get; set; }
        public string Title { get; set; }
        public string Origin { get; set; }
        public string Bio { get; set; }
        public string Diety { get; set; }
        public bool Attackable { get; set; }
        public int LastAttackerID { get; set; }
        public int LastTargetID { get; set; }
        public long LastActionRound { get; set; }

        public int SoundID { get; set; }
        public int VoiceID { get; set; }
        public int ThreatenSoundID { get; set; }
        public int AttackSoundID { get; set; }
        public int DeathSoundID { get; set; }

        public DateTime DeathTime { get; set; }

        public Stack<Aggro> Aggressors { get; set; }

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

        public int Age { get; set; }
        public int Level { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Luck { get; set; }
        public int AttackDelay { get; set; }

        public int AlignmentCount { get; set; }
        public AlignmentType Alignment { get; set; }

        public int Experience { get; set; }
        public int Gold { get; set; }
        public int Stealth { get; set; }

        public Stats Stats { get; set; }

        public int HPs { get; set; }
        public int MaxHPs { get; set; }
        public int MPs { get; set; }
        public int MaxMPs { get; set; }
        public int HPRegen { get; set; }
        public int MPRegen { get; set; }

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
        public List<int> Languages { get; set; }

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
            Attackable = true;

            // Base damage and regeneration for all entities
            MinDamage = 1;
            MaxDamage = 2;
            HPRegen = 1;
            MPRegen = 1;

            Loc = new Loc() { AreaID = areaId, HexID = hexId };
            Stats = new Stats() { Name = Name };

            Languages = new List<int> { 0 }; // Everyone sentient knows the common language
        }

        public string GetDescription(bool includeBio = true, bool includeLoc = false)
        {
            StringBuilder sb = new StringBuilder(this.FullName);
            if (!String.IsNullOrEmpty(Surname))
                sb.Append(" " + Surname);
            sb.Append(", level " + Level.ToString());
            if (!String.IsNullOrEmpty(Race.ToString()))
                sb.Append(" " + Race);
            if (!String.IsNullOrEmpty(Class.ToString()))
                sb.Append(" " + Class);
            sb.Append(" (State: " + State.ToString() + ")");
            if (includeBio)
            {
                if (!String.IsNullOrEmpty(Bio))
                    sb.Append("\r\n" + Bio);
            }
            if (includeLoc)
            {
                sb.Append(" at Area:" + Loc.AreaID.ToString() + 
                    "-Hex:" + Loc.HexID.ToString());
            }
            return sb.ToString();
        }

        public virtual void Die()
        {
            State = StateType.Dead;
            if (HPs > 0)
            {
                HPs = 0;
            }
            DeathTime = DateTime.Now;
        }

        public virtual bool Regen()
        {
            bool regen = false;

            if (State == StateType.Dead)
                return false;

            if (HPs < MaxHPs)
            {
                regen = true;
                HPs += HPRegen;

                if (HPs > MaxHPs)
                {
                    HPs = MaxHPs;
                }
            }

            if (MPs < MaxMPs)
            {
                regen = true;
                MPs += MPRegen;

                if (MPs > MaxMPs)
                {
                    MPs = MaxMPs;
                }
            }

            return regen;
        }

        public virtual void Revive()
        {
            State = StateType.Normal;
            HPs = MaxHPs;
            MPs = MaxMPs;
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
        public Stats GetStats()
        {
            return new Stats()
            {
                ID = base.ID,
                Name = Name,
                ImageName = ImageName,
                HPs = HPs,
                MaxHPs = MaxHPs,
                MPs = MPs,
                MaxMPs = MaxMPs,
                Age = Age,
                Level = Level,
                Experience = Experience,
                Gold = Gold,
                AttackDelay = AttackDelay,
                State = State,
                Facing = Facing,
            };
        }

        public void LevelUp()
        {
            Level++;
            MaxHPs += Randomizer.Next(1, 8) + Constitution;
            if (MaxMPs > 0)
            {
                MaxMPs += Randomizer.Next(1, 8) + Constitution;
            }
        }

        public Entity Clone()
        {
            return (Entity)this.MemberwiseClone();
        }
    }
}
