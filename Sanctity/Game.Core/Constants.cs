using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public static class Constants
    {
        public static readonly bool PacketCompression = true;
        public static readonly int PacketBufferSize = 4096;
        public static readonly string PacketDelimiter = "\r\n}";

        public static readonly int MaxLevel = 50;
        public static readonly int MaxGroupSize = 6;
        public static readonly int NPCCorpseDecay = 30;
        public static readonly int PlayerCorpseDecay = 30;
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
        Command,
        Text,
        Broadcast,
        Yell,
        Say,
        Tell,
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
        Player,
        NPC,
        Merchant,
        DM,
    };

    public enum StateType
    {
        Normal,
        Dead,
        Combat,
        Stunned,
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
        Elf,
        Dwarf,
        Gnome,
        Orc,
        Animal,
        Demon,
    };

    public enum ClassType
    {
        Unknown,
        Fighter,
        Barbarian,
        Paladin,
        Cleric,
        Wizard,
        Thief,
        Assassin,
    };

    public enum AlignmentType
    {
        Neutral,
        LawfulGood,
        ChaoticGood,
        ChaoticNeutral,
        ChaoticEvil,
        LawfulEvil,
    }

    public enum MoodType
    {
        Normal,
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
}
