namespace Game.Core
{
    public static class Constants
    {
        public static readonly int ServerPort = 1500;
        public static readonly int PacketBufferSize = 1500;
        public static readonly string PacketDelimiter = "\r\n}";
        public static readonly bool PacketCompression = true;

        public static readonly int RoundInterval = 2000;
        public static readonly int VisibleTiles = 49; // Default visible grid is 7 X 7 tiles

        public static readonly int MaxLevel = 20;
        public static readonly int MaxGroupSize = 6;
        public static readonly int NPCDefaultCorpseDecay = 30;
        public static readonly int PCDefaultCorpseDecay = 30;
        public static readonly int PCInactivityTimeout = 0;
        public static readonly string PCDefaultSecret = "loc";
    }

    public enum AccountType
    {
        Regular,
        Premium,
        Hero,
        DungeonMaster,
    }

    public enum MoveDirection
    {
        North,
        South,
        East,
        West,
        Northeast,
        Northwest,
        Southeast,
        Southwest,
        Up,
        Down,
    };

    public enum ActionType
    {
        Status,
        Join,
        Exit,
        Death,
        Command,
        Text,
        Broadcast,
        Yell,
        Say,
        Tell,
        LevelUp,
        Sound,
        Movement,
        Damage,
        Use,
    };

    public enum ItemType
    {
        Unknown,
        Weapon,
        Armor,
        Tools,
        Treasure,
        Quest,
    };

    public enum EntityType
    {
        Unknown,
        Merchant,
        PC,
        NPC,
        Hero,
        Demigod,
        LesserGod,
        GreaterGod,
        DungeonMaster,
    };

    public enum StateType
    {
        Normal,
        Dead,
        Combat,
        Stunned,
        Immobile,
        Invisible,
        Ethereal,
        Trading,
    };

    public enum GenderType
    {
        Unknown,
        Male,
        Female,
    };

    public enum SizeType
    {
        Medium,
        Tiny,
        Small,
        Large,
        VeryLage,
        Huge,
        Monstrous,
    };

    public enum RaceType
    {
        Unknown,
        Human,
        Halfling,
        Elf,
        HalfElf,
        Dwarven,
        Gnome,
        HalfOrc,
        Humanoid,
        Orc,
        Goblin,
        Animal,
        Aquatic,
        Insect,
        Reptillian,
        Undead,
        Demonic,
        Dragon,
        Planar,
    };

    public enum ClassType
    {
        Unknown,
        Barbarian,
        Bard,
        Cleric,
        Druid,
        Fighter,
        Monk,
        Paladin,
        Ranger,
        Rogue,
        Sorcerer,
        Wizard,
    };

    public enum AlignmentType
    {
        Neutral,
        LawfulGood,
        LawfulEvil,
        ChaoticGood,
        ChaoticEvil,
    }

    public enum Language
    {
        Common,
        Human,
        Elvish,
        Dwarven,
        Gnomish,
        Orcish,
        Planar,
    };

    public enum MoodType
    {
        Normal,
        Scared,
        Suspicous,
        Aggitated,
        Aggressive,
    };

    public enum DamageType
    {
        Normal,
        Fire,
        Ice,
        Acid,
        Poison,
        Mental,
    };

    public enum EncounterType
    {
        VeryCommon,
        Common,
        Rare,
        VeryRare,
        Animal,
        Aquatic,
        AquaticRare,
        RareAnimal,
        Insect,
        RareInsect,
        Undead,
        RareUndead,
        DragonKind,
        Demonic,
        Unique,
    };

    public enum LootType
    {
        None,
        Animal,
        AnimalRare,
        Insect,
        RareInsect,
        VeryCommon,
        Common,
        VeryRare,
        Exquisite,
        Unique,
    };

    public enum MusicType
    {
        None,
        Mystery,
    };

    public enum SoundType
    {
        None,
        Scary,
        Waterdrip,
    };

    public enum Command
    {
        None,
        Join,
        Quit,
        Say,
        Yell,
        Send,
        Mail,
        Appeal,
        Revive,
        Transcend,
        Move,
        Look,
        Attack,
        Cast,
        Hide,
        Get,
        Interact,
        Trade,
        Inventory,
    };
}
