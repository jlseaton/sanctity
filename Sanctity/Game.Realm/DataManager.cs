using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Game.Core;

namespace Game.Realm
{
    public class DataManager
    {
        public DataManager()
        {
            //TODO: Implement JSON/XML/Database storage
        }

        public List<Area> LoadAreas()
        {
            List<Area> Areas = new List<Area>();
            List<Hex> Hexes = new List<Hex>();

            Hexes.Add(new Hex()
            {
                ID = 1,
                Tile = new Tile()
                {
                    Name = "Dungeon Entrance",
                    East = 2,
                    Up = -1,
                    Text = "You are standing at the entrance to Dungeon Lab. A ladder leads up and to safety."
                },
            });

            Hexes.Add(new Hex()
            {
                ID = 2,
                Tile = new Tile()
                {
                    East = 3,
                    West = 1,
                },
            });

            Hexes.Add(new Hex()
            {
                ID = 3,
                Tile = new Tile()
                {
                    South = 7,
                    East = 4,
                    West = 2,
                },
                RandomNPCs = new List<EncounterType>() { EncounterType.Animal },
            });

            Hexes.Add(new Hex()
            {
                ID = 4,
                Tile = new Tile()
                {
                    Name = "Undead Tomb",
                    West = 3,
                    Text = "You are standing in a dank tomb.",
                },
                RandomNPCs = new List<EncounterType>() { EncounterType.Undead },
            });

            Hexes.Add(new Hex()
            {
                ID = 5,
                Tile = new Tile()
                {
                    Name = "Smaug's Lair",
                    East = 6,
                    Text = "You have awoken a huge red dragon.",
                    SoundID = 1,
                },
                RandomNPCs = new List<EncounterType>() { EncounterType.DragonKind },
            });

            Hexes.Add(new Hex()
            {
                ID = 6,
                Tile = new Tile()
                {
                    South = 10,
                    East = 7,
                    West = 5,
                },
            });

            Hexes.Add(new Hex()
            {
                ID = 7,
                Tile = new Tile()
                {
                    North = 3,
                    South = 11,
                    East = 8,
                    West = 6,
                },
                RandomNPCs = new List<EncounterType>() { EncounterType.Common },
            });

            Hexes.Add(new Hex()
            {
                ID = 8,
                Tile = new Tile()
                {
                    Name = "Animal Kingdom",
                    South = 12,
                    West = 7,
                },
            });

            Hexes.Add(new Hex()
            {
                ID = 9,
                QuestID = 1,
                LockID = 1,
                LockLevel = 50,
                PermanentNPCs = new List<int>() { 1 },
                Tile = new Tile()
                {
                    Name = "Demogorgon's Lair",
                    East = 10,
                    Text = "You are standing in the presence of pure evil itself."
                },
            });

            Hexes.Add(new Hex()
            {
                ID = 10,
                Tile = new Tile()
                {
                    North = 6,
                    East = 11,
                    West = 9,
                },
            });

            Hexes.Add(new Hex()
            {
                ID = 11,
                Tile = new Tile()
                {
                    North = 7,
                    East = 12,
                    West = 10,
                },
            });

            Hexes.Add(new Hex()
            {
                ID = 12,
                Name = "Stairway to Hell",
                Tile = new Tile()
                {
                    North = 8,
                    West = 11,
                    Down = -1,
                    Text = "You are standing at the top of a winding staircase leading down into darkness."
                },
            });

            Areas.Add(new Area()
            {
                ID = 0,
                SoundID = 1,
                Title = "Dungeon Lab",
                Height = 3,
                Width = 4,
                Depth = 1,
                StartX = 0,
                StartY = 0,
                Hexes = Hexes,
            });

            return Areas;
        }

        public List<PC> LoadPCs()
        {
            List<PC> Players = new List<PC>();

            Players.Add(new PC()
            {
                ID = 1,
                Type = EntityType.Player,
                Race = RaceType.Orc,
                Class = ClassType.Barbarian,
                Alignment = AlignmentType.Neutral,
                Name = "Hoxore",
                Level = 15,
                Strength = 18,
                Dexterity = 15,
                Constitution = 15,
                Intelligence = 10,
                Wisdom = 8,
                Luck = 10,
                HitPoints = 200,
                MaxHitPoints = 200,
                Experience = 25000,
                Gold = 500,
                LootClass = LootType.None,
                MainHandID = 1,
            });

            Players.Add(new PC()
            {
                ID = 2,
                Type = EntityType.Player,
                Race = RaceType.Human,
                Class = ClassType.Fighter,
                Alignment = AlignmentType.Neutral,
                Name = "Derwin",
                Level = 5,
                Strength = 18,
                Dexterity = 15,
                Constitution = 15,
                Intelligence = 10,
                Wisdom = 8,
                Luck = 10,
                HitPoints = 75,
                MaxHitPoints = 75,
                Experience = 2500,
                Gold = 50,
                LootClass = LootType.None,
                MainHandID = 6,
            });

            Players.Add(new PC()
            {
                ID = 3,
                Type = EntityType.Player,
                Race = RaceType.Gnome,
                Class = ClassType.Thief,
                Alignment = AlignmentType.ChaoticNeutral,
                Name = "Smindel",
                Level = 5,
                Strength = 11,
                Dexterity = 18,
                Constitution = 11,
                Intelligence = 12,
                Wisdom = 10,
                Luck = 15,
                HitPoints = 30,
                MaxHitPoints = 30,
                Experience = 2500,
                Gold = 50,
                LootClass = LootType.None,
                MainHandID = 3,
            });

            Players.Add(new PC()
            {
                ID = 4,
                Type = EntityType.Player,
                Race = RaceType.Human,
                Class = ClassType.Paladin,
                Alignment = AlignmentType.LawfulGood,
                Name = "Astef",
                Level = 5,
                Strength = 16,
                Dexterity = 14,
                Constitution = 15,
                Intelligence = 12,
                Wisdom = 14,
                Luck = 12,
                HitPoints = 35,
                MaxHitPoints = 35,
                ManaPoints = 15,
                MaxManaPoints = 15,
                Experience = 2500,
                Gold = 50,
                LootClass = LootType.None,
                MainHandID = 5,
            });

            Players.Add(new PC()
            {
                ID = 5,
                Type = EntityType.Player,
                Race = RaceType.Elf,
                Class = ClassType.Wizard,
                Alignment = AlignmentType.ChaoticNeutral,
                Name = "Natillah",
                Level = 5,
                Strength = 8,
                Dexterity = 14,
                Constitution = 10,
                Intelligence = 18,
                Wisdom = 12,
                Luck = 10,
                HitPoints = 20,
                MaxHitPoints = 20,
                ManaPoints = 20,
                MaxManaPoints = 20,
                Experience = 2500,
                Gold = 50,
                LootClass = LootType.None,
                MainHandID = 2,
            });

            Players.Add(new PC()
            {
                ID = 6,
                Type = EntityType.Player,
                Race = RaceType.Dwarf,
                Class = ClassType.Cleric,
                Alignment = AlignmentType.ChaoticGood,
                Name = "Faerune",
                Level = 5,
                Strength = 14,
                Dexterity = 12,
                Constitution = 15,
                Intelligence = 12,
                Wisdom = 18,
                Luck = 12,
                HitPoints = 25,
                MaxHitPoints = 25,
                ManaPoints = 15,
                MaxManaPoints = 15,
                Experience = 2500,
                Gold = 50,
                LootClass = LootType.None,
                MainHandID = 4,
            });

            Players.Add(new PC()
            {
                ID = 7,
                Type = EntityType.Player,
                Gender = GenderType.Female,
                Race = RaceType.Elf,
                Class = ClassType.Fighter,
                Alignment = AlignmentType.ChaoticGood,
                Name = "Gayaa",
                Level = 5,
                Strength = 12,
                Dexterity = 20,
                Constitution = 15,
                Intelligence = 12,
                Wisdom = 11,
                Luck = 14,
                HitPoints = 60,
                MaxHitPoints = 60,
                ManaPoints = 15,
                MaxManaPoints = 15,
                Experience = 2500,
                Gold = 50,
                LootClass = LootType.None,
                MainHandID = 11,
            });

            return Players;
        }

        public void ReplacePlayer(PC oldPlayer, PC newPlayer)
        {
            //for(int i= 0; i< Players.Count; i++)
            //{
            //    Players[i] = newPlayer;
            //}
        }

        public List<NPC> LoadNPCs()
        {
            List<NPC> NPCs = new List<NPC>();

            var vorpal = LoadItems().Where(i => i.ID == 1).Single();
            var demoInven = new List<Item>();
            demoInven.Add(vorpal);

            NPCs.Add(new NPC()
            {
                ID = 1,
                QuestID = 1,
                Homeland = "Gaping Maw",
                Type = EntityType.NPC,
                Race = RaceType.Demon,
                Size = SizeType.Huge,
                Speed = 80,
                Level = 18,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 50,
                Article = "a",
                Name = "DemoGorgon",
                HitPoints = 70,
                MaxHitPoints = 70,
                ManaPoints = 50,
                MaxManaPoints = 50,
                MinDamage = 12,
                MaxDamage = 72,
                Experience = 5000,
                Gold = 5000,
                Mood = MoodType.Aggressive,
                EncounterClass = EncounterType.Unique,
                LootClass = LootType.Unique,
                Inventory = demoInven,
            });

            NPCs.Add(new NPC()
            {
                ID = 2,
                Type = EntityType.NPC,
                Level = 1,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 0,
                Article = "a",
                Name = "snake",
                HitPoints = 5,
                MaxHitPoints = 5,
                MinDamage = 1,
                MaxDamage = 3,
                Experience = 10,
                Gold = 0,
                WanderRange = 2,
                Follows = 1,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.Animal
            });

            NPCs.Add(new NPC()
            {
                ID = 3,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "bear",
                HitPoints = 15,
                MaxHitPoints = 15,
                MinDamage = 3,
                MaxDamage = 5,
                Experience = 20,
                Gold = 0,
                WanderRange = 5,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.Animal
            });

            NPCs.Add(new NPC()
            {
                ID = 4,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "tiger",
                HitPoints = 15,
                MaxHitPoints = 15,
                MinDamage = 3,
                MaxDamage = 5,
                Experience = 20,
                Gold = 0,
                WanderRange = 5,
                Follows = 3,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.Animal
            });

            NPCs.Add(new NPC()
            {
                ID = 5,
                Type = EntityType.NPC,
                Level = 5,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "lion",
                HitPoints = 20,
                MaxHitPoints = 20,
                MinDamage = 5,
                MaxDamage = 8,
                Experience = 30,
                Gold = 0,
                WanderRange = 5,
                Follows = 3,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.Animal
            });

            NPCs.Add(new NPC()
            {
                ID = 6,
                Type = EntityType.NPC,
                Level = 3,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 1,
                Article = "a",
                Name = "spider",
                HitPoints = 10,
                MaxHitPoints = 10,
                MinDamage = 3,
                MaxDamage = 5,
                Experience = 20,
                Gold = 0,
                WanderRange = 1,
                Follows = 1,
                EncounterClass = EncounterType.Insect,
                LootClass = LootType.Insect,
            });

            NPCs.Add(new NPC()
            {
                ID = 7,
                Type = EntityType.NPC,
                Level = 3,
                Alignment = AlignmentType.ChaoticNeutral,
                ArmorClass = 3,
                Article = "a",
                Name = "skeleton",
                HitPoints = 10,
                MaxHitPoints = 10,
                MinDamage = 1,
                MaxDamage = 6,
                Experience = 10,
                Gold = 5,
                WanderRange = 5,
                Follows = 3,
                EncounterClass = EncounterType.Undead,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = 8,
                Type = EntityType.NPC,
                Level = 3,
                Alignment = AlignmentType.ChaoticNeutral,
                ArmorClass = 4,
                Article = "a",
                Name = "ghoul",
                HitPoints = 12,
                MaxHitPoints = 12,
                MinDamage = 1,
                MaxDamage = 8,
                Experience = 15,
                Gold = 5,
                WanderRange = 2,
                Follows = 1,
                EncounterClass = EncounterType.Undead,
                LootClass = LootType.Common,
            });

            var chest = LoadItems().Where(i => i.ID == 10).Single();
            var dragonInven = new List<Item>();
            dragonInven.Add(chest);

            NPCs.Add(new NPC()
            {
                ID = 9,
                Type = EntityType.NPC,
                Level = 15,
                Size = SizeType.Monstrous,
                Alignment = AlignmentType.LawfulEvil,
                ArmorClass = 25,
                Article = "a",
                Name = "red dragon",
                HitPoints = 75,
                MaxHitPoints = 75,
                MinDamage = 3,
                MaxDamage = 18,
                Experience = 1500,
                Gold = 750,
                WanderRange = 10,
                Follows = 3,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.VeryRare,
                Skills = new List<int>() { 2, 3 }, 
                Inventory = dragonInven,
            });

            return NPCs;
        }

        public List<Item> LoadItems()
        {
            List<Item> Items = new List<Item>();

            Items.Add(new Item()
            {
                ID = 1,
                QuestID = 1,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "Vorpal Sword",
                Value = 5000,
                ToHitBonus = 3,
                DamageBonus = 2,
                MinDamage = 6,
                MaxDamage = 12,
                Effects = new List<int>() { 1 },
            });

            Items.Add(new Item()
            {
                ID = 2,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "staff",
                Value = 10,
                MinDamage = 1,
                MaxDamage = 4,
            });

            Items.Add(new Item()
            {
                ID = 3,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "dagger",
                Value = 5,
                MinDamage = 1,
                MaxDamage = 6,
            });

            Items.Add(new Item()
            {
                ID = 4,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "mace",
                Value = 7,
                MinDamage = 2,
                MaxDamage = 6,
            });

            Items.Add(new Item()
            {
                ID = 5,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "longsword",
                Value = 10,
                MinDamage = 1,
                MaxDamage = 8,
            });

            Items.Add(new Item()
            {
                ID = 6,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "two-handed sword",
                Value = 15,
                MinDamage = 3,
                MaxDamage = 18,
            });

            Items.Add(new Item()
            {
                ID = 7,
                Type = ItemType.Armor,
                Article = "a",
                Name = "leather armor",
                Value = 10,
                ArmorClass = 3,
            });

            Items.Add(new Item()
            {
                ID = 8,
                Type = ItemType.Armor,
                Article = "a",
                Name = "chainmail",
                Value = 10,
                ArmorClass = 4,
            });

            Items.Add(new Item()
            {
                ID = 9,
                Type = ItemType.Armor,
                Article = "a",
                Name = "buckler shield",
                Value = 10,
                ArmorClass = 2,
            });

            Items.Add(new Item()
            {
                ID = 10,
                Type = ItemType.Treasure,
                Article = "a",
                Name = "chest",
                Value = 1000,                
            });

            Items.Add(new Item()
            {
                ID = 11,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "longbow",
                Value = 10,
                MinDamage = 3,
                MaxDamage = 12,
            });

            return Items;
        }

        public List<Effect> LoadEffects()
        {
            List<Effect> Effects = new List<Effect>();

            Effects.Add(new Effect()
            {
                ID = 1,
                Name = "behead",
                Type = EffectType.Death,
                Damage = DamageType.Acid,
                MinDamage = 100,
                MaxDamage = 1000,
                MaximumTargets = 1,
                FizzleChance = 85,
                Verb = " beheads ",
            });

            Effects.Add(new Effect()
            {
                ID = 2,
                Name = "dragonfire",
                Type = EffectType.Damage,
                Damage = DamageType.Fire,
                MinDamage = 12,
                MaxDamage = 24,
                MaximumTargets = 6,
                FizzleChance = 75,
                Verb = " breathes fire on ",
            });

            Effects.Add(new Effect()
            {
                ID = 3,
                Name = "dragonfear",
                Type = EffectType.Stun,
                Damage = DamageType.Mental,
                MinDamage = 1,
                MaxDamage = 3,
                Value = 3,
                MaximumTargets = 6,
                FizzleChance = 90,
                Verb = " induces fear in ",
            });

            return Effects;
        }

        public List<Quest> LoadQuests()
        {
            List<Quest> Quests = new List<Quest>();

            Quests.Add(new Quest()
            {
                ID = 1,
                Name = "Sanctity's Edge",
                Description = "Main quest line.",
                RewardText = "You have completed the game.",
                Experience = 10000,
                Gold = 10000,
                LootClass = LootType.Unique,
                SpecificLoot = new List<int>(1),
            });

            return Quests;
        }
    }
}
