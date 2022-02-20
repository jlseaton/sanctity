using Game.Core;

namespace Game.Realm
{
    public class DataManager
    {
        public DataManager()
        {

        }

        public List<Area> LoadAreas()
        {
            List<Area> Areas = new List<Area>();
            List<Hex> Hexes = new List<Hex>();

            try
            {
                Hexes.Add(new Hex()
                {
                    ID = 1,
                    Tile = new Tile()
                    {
                        Tile2ID = "stairsup",
                        Name = "Dungeon Entrance",
                        East = 2,
                        Up = -1,
                        Text = "You are standing at the entrance to the Dungeon Lab. A ladder leads up and to safety."
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
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.Animal },
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
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.Undead },
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
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.RareUndead },
                });

                Hexes.Add(new Hex()
                {
                    ID = 5,
                    Tile = new Tile()
                    {
                        Name = "Dragon Lair",
                        East = 6,
                        Text = "You smell smoke and a foul odor in the air.",
                    },
                    RandomNPCsMax = 5,
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
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.Demonic },
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
                    RandomNPCsMax = 5,
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
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.Animal },
                });

                Hexes.Add(new Hex()
                {
                    ID = 9,
                    QuestID = 1,
                    LockID = 1,
                    LockLevel = 50,
                    Tile = new Tile()
                    {
                        Name = "Demogorgon's Lair",
                        East = 10,
                        Text = "You are standing in the presence of pure evil itself."
                    },
                    PermanentNPCs = new List<int>() { 1 },
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
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.VeryRare },
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
                    RandomNPCs = new List<EncounterType>() { EncounterType.VeryRare },
                });

                Hexes.Add(new Hex()
                {
                    ID = 12,
                    Name = "Stairway to the Unknown",
                    Tile = new Tile()
                    {
                        Tile2ID = "stairsup",
                        North = 8,
                        West = 11,
                        Down = -1,
                        Text = "You are standing at the top of a winding staircase leading down into utter darkness. (Exit Dungeon)"
                    },
                    PermanentNPCs = new List<int>() { 10 },
                });

                Areas.Add(new Area()
                {
                    ID = 0,
                    Title = "Dungeon Lab",
                    Height = 3,
                    Width = 4,
                    Depth = 1,
                    StartX = 0,
                    StartY = 0,
                    Hexes = Hexes,
                });

                //XmlReader reader = XmlReader.Create(@"data\areas.xml");
                //while (reader.Read())
                //{
                //    System.Diagnostics.Debug.WriteLine(reader.Name);
                //    System.Diagnostics.Debug.WriteLine(reader.Value);
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return Areas;
        }

        public List<PC> LoadPCs()
        {
            List<PC> Players = new List<PC>();

            int id = 1;
            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.Hero,
                Race = RaceType.HalfOrc,
                Class = ClassType.Barbarian,
                Alignment = AlignmentType.Neutral,
                Origin = "Planar",
                Diety = "Bhaal",
                Name = "Hoxore",
                Surname = "Seaton",
                Age = 28,
                Level = 15,
                Strength = 19,
                Dexterity = 15,
                Constitution = 15,
                Intelligence = 10,
                Wisdom = 8,
                Luck = 10,
                HitPoints = 220,
                MaxHitPoints = 220,
                Experience = 2500,
                Gold = 250,
                Stealth = 5,
                ArmorClass = 10,
                LootClass = LootType.None,
                MainHandID = 1,
                ImageName = "barbarian",
                Bio = "An extremely powerful character primarily used for testing dungeons.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Gender = GenderType.Female,
                Race = RaceType.HalfElf,
                Class = ClassType.Wizard,
                Alignment = AlignmentType.ChaoticGood,
                Name = "Lexanna",
                Age = 15,
                Level = 5,
                Strength = 11,
                Dexterity = 14,
                Constitution = 12,
                Intelligence = 16,
                Wisdom = 12,
                Luck = 14,
                HitPoints = 30,
                MaxHitPoints = 30,
                ManaPoints = 35,
                MaxManaPoints = 35,
                Experience = 250,
                Gold = 50,
                Stealth = 15,
                LootClass = LootType.None,
                MainHandID = 4,
                ImageName = "wizardress",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Gender = GenderType.Female,
                Race = RaceType.HalfElf,
                Class = ClassType.Paladin,
                Alignment = AlignmentType.LawfulGood,
                Name = "Caitlanna",
                Age = 17,
                Level = 5,
                Strength = 15,
                Dexterity = 12,
                Constitution = 15,
                Intelligence = 12,
                Wisdom = 15,
                Luck = 14,
                HitPoints = 60,
                MaxHitPoints = 60,
                ManaPoints = 25,
                MaxManaPoints = 25,
                Experience = 250,
                Gold = 50,
                Stealth = 10,
                LootClass = LootType.None,
                MainHandID = 6,
                ImageName = "valkyrie",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Race = RaceType.Elf,
                Class = ClassType.Sorcerer,
                Alignment = AlignmentType.ChaoticEvil,
                Name = "Natillah",
                Age = 24,
                Level = 5,
                Strength = 8,
                Dexterity = 14,
                Constitution = 10,
                Intelligence = 18,
                Wisdom = 12,
                Luck = 10,
                HitPoints = 25,
                MaxHitPoints = 25,
                ManaPoints = 40,
                MaxManaPoints = 40,
                Experience = 250,
                Gold = 50,
                Stealth = 10,
                LootClass = LootType.None,
                MainHandID = 2,
                ImageName = "cleric",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Race = RaceType.Human,
                Class = ClassType.Bard,
                Alignment = AlignmentType.ChaoticGood,
                Name = "Faerune",
                Age = 25,
                Level = 5,
                Strength = 13,
                Dexterity = 12,
                Constitution = 12,
                Intelligence = 14,
                Wisdom = 14,
                Luck = 16,
                HitPoints = 35,
                MaxHitPoints = 35,
                ManaPoints = 15,
                MaxManaPoints = 15,
                Experience = 250,
                Gold = 50,
                Stealth = 10,
                LootClass = LootType.None,
                MainHandID = 4,
                ImageName = "monk",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Race = RaceType.Dwarf,
                Class = ClassType.Fighter,
                Alignment = AlignmentType.Neutral,
                Name = "Owlshonor",
                Age = 17,
                Level = 5,
                Strength = 18,
                Dexterity = 15,
                Constitution = 15,
                Intelligence = 10,
                Wisdom = 8,
                Luck = 10,
                HitPoints = 55,
                MaxHitPoints = 55,
                Experience = 250,
                Gold = 50,
                Stealth = 5,
                LootClass = LootType.None,
                MainHandID = 5,
                ImageName = "bluefighter",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Race = RaceType.Gnome,
                Class = ClassType.Druid,
                Alignment = AlignmentType.Neutral,
                Name = "Lyle",
                Age = 17,
                Level = 5,
                Strength = 11,
                Dexterity = 12,
                Constitution = 13,
                Intelligence = 12,
                Wisdom = 15,
                Luck = 15,
                HitPoints = 40,
                MaxHitPoints = 40,
                ManaPoints = 25,
                MaxManaPoints = 25,
                Experience = 250,
                Gold = 50,
                Stealth = 25,
                LootClass = LootType.None,
                MainHandID = 2,
                ImageName = "halfling",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Gender = GenderType.Female,
                Race = RaceType.Halfling,
                Class = ClassType.Cleric,
                Alignment = AlignmentType.LawfulGood,
                Name = "Chella",
                Age = 14,
                Level = 5,
                Strength = 11,
                Dexterity = 14,
                Constitution = 12,
                Intelligence = 11,
                Wisdom = 15,
                Luck = 14,
                HitPoints = 20,
                MaxHitPoints = 20,
                ManaPoints = 25,
                MaxManaPoints = 25,
                Experience = 250,
                Gold = 15,
                Stealth = 15,
                LootClass = LootType.None,
                MainHandID = 2,
                ImageName = "racoon",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Race = RaceType.Halfling,
                Class = ClassType.Thief,
                Alignment = AlignmentType.Neutral,
                Name = "Smindel",
                Age = 23,
                Level = 5,
                Strength = 11,
                Dexterity = 18,
                Constitution = 11,
                Intelligence = 12,
                Wisdom = 10,
                Luck = 15,
                HitPoints = 30,
                MaxHitPoints = 30,
                Experience = 250,
                Gold = 50,
                Stealth = 50,
                LootClass = LootType.None,
                MainHandID = 3,
                ImageName = "halfling",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Gender = GenderType.Male,
                Race = RaceType.HalfElf,
                Class = ClassType.Ranger,
                Alignment = AlignmentType.ChaoticGood,
                Name = "Arawn",
                Age = 23,
                Level = 5,
                Strength = 13,
                Dexterity = 14,
                Constitution = 12,
                Intelligence = 11,
                Wisdom = 13,
                Luck = 12,
                HitPoints = 35,
                MaxHitPoints = 35,
                ManaPoints = 15,
                MaxManaPoints = 15,
                Experience = 250,
                Gold = 25,
                Stealth = 15,
                LootClass = LootType.None,
                MainHandID = 5,
                ImageName = "grayfighter",
            });

            return Players;
        }

        //public void ReplacePlayer(PC oldPlayer, PC newPlayer)
        //{
        //for(int i= 0; i< Players.Count; i++)
        //{
        //    Players[i] = newPlayer;
        //}
        //}

        public List<NPC> LoadNPCs()
        {
            List<NPC> NPCs = new List<NPC>();

            var vorpal = LoadItems().Where(i => i.ID == 1).Single();
            var demoInven = new List<Item>();
            demoInven.Add(vorpal);

            var id = 1;

            NPCs.Add(new NPC()
            {
                ID = id++,
                QuestID = 1,
                Origin = "Gaping Maw",
                Type = EntityType.NPC,
                Race = RaceType.Demonic,
                Size = SizeType.Huge,
                Speed = 80,
                Level = 18,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 50,
                Article = "a",
                Name = "demogorgon",
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
                ID = id++,
                Type = EntityType.NPC,
                Level = 1,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 0,
                Article = "a",
                Name = "coppersnake",
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
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "siren",
                HitPoints = 15,
                MaxHitPoints = 15,
                MinDamage = 3,
                MaxDamage = 8,
                Experience = 25,
                Gold = 0,
                WanderRange = 5,
                EncounterClass = EncounterType.VeryRare,
                LootClass = LootType.Animal
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "racoon",
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
                ID = id++,
                Type = EntityType.NPC,
                Level = 5,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "blackspider",
                HitPoints = 20,
                MaxHitPoints = 20,
                MinDamage = 5,
                MaxDamage = 8,
                Experience = 30,
                Gold = 0,
                WanderRange = 5,
                Follows = 3,
                EncounterClass = EncounterType.Insect,
                LootClass = LootType.Insect
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 3,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 1,
                Article = "a",
                Name = "redspider",
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
                ID = id++,
                Type = EntityType.NPC,
                Level = 3,
                Alignment = AlignmentType.ChaoticEvil,
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
                ID = id++,
                Type = EntityType.NPC,
                Level = 1,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 1,
                Article = "a",
                Name = "greensnake",
                HitPoints = 3,
                MaxHitPoints = 5,
                MinDamage = 1,
                MaxDamage = 4,
                Experience = 8,
                Gold = 0,
                WanderRange = 2,
                Follows = 1,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.Animal,
            });

            var chest = LoadItems().Where(i => i.ID == 10).Single();
            var dragonInven = new List<Item>();
            dragonInven.Add(chest);

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 2,
                Article = "a",
                Name = "minatour",
                HitPoints = 45,
                MaxHitPoints = 45,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 640,
                Gold = 250,
                WanderRange = 5,
                Follows = 3,
                EncounterClass = EncounterType.VeryRare,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 15,
                Size = SizeType.Monstrous,
                Alignment = AlignmentType.LawfulEvil,
                ArmorClass = 25,
                Article = "a",
                Name = "reddragon",
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

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 2,
                Article = "a",
                Name = "wyvern",
                HitPoints = 45,
                MaxHitPoints = 45,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 640,
                Gold = 250,
                WanderRange = 5,
                Follows = 3,
                EncounterClass = EncounterType.VeryRare,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 2,
                Article = "a",
                Name = "griffon",
                HitPoints = 45,
                MaxHitPoints = 45,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 640,
                Gold = 250,
                WanderRange = 5,
                Follows = 3,
                EncounterClass = EncounterType.VeryRare,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 2,
                Article = "a",
                Name = "hellhound",
                HitPoints = 45,
                MaxHitPoints = 45,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 640,
                Gold = 250,
                WanderRange = 5,
                Follows = 3,
                EncounterClass = EncounterType.VeryRare,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.LawfulGood,
                ArmorClass = 2,
                Article = "a",
                Name = "eagle",
                HitPoints = 45,
                MaxHitPoints = 45,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 540,
                Gold = 150,
                WanderRange = 5,
                Follows = 3,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.AnimalRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "racoon",
                HitPoints = 4,
                MaxHitPoints = 4,
                MinDamage = 1,
                MaxDamage = 3,
                Experience = 30,
                Gold = 5,
                WanderRange = 2,
                EncounterClass = EncounterType.Common,
                LootClass = LootType.VeryCommon,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "frog",
                HitPoints = 3,
                MaxHitPoints = 3,
                MinDamage = 1,
                MaxDamage = 2,
                Experience = 20,
                Gold = 1,
                WanderRange = 2,
                EncounterClass = EncounterType.Common,
                LootClass = LootType.VeryCommon,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "goat",
                HitPoints = 5,
                MaxHitPoints = 5,
                MinDamage = 1,
                MaxDamage = 3,
                Experience = 45,
                Gold = 5,
                WanderRange = 2,
                EncounterClass = EncounterType.Common,
                LootClass = LootType.VeryCommon,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 5,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 3,
                Article = "a",
                Name = "mountainlion",
                HitPoints = 55,
                MaxHitPoints = 55,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 480,
                Gold = 50,
                WanderRange = 3,
                Follows = 3,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.AnimalRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 5,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 4,
                Article = "a",
                Name = "gorilla",
                HitPoints = 65,
                MaxHitPoints = 65,
                MinDamage = 2,
                MaxDamage = 10,
                Experience = 760,
                Gold = 50,
                WanderRange = 3,
                Follows = 3,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.AnimalRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "redhornbee",
                HitPoints = 12,
                MaxHitPoints = 12,
                MinDamage = 3,
                MaxDamage = 6,
                Experience = 60,
                Gold = 5,
                WanderRange = 3,
                EncounterClass = EncounterType.Insect,
                LootClass = LootType.Insect,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "scorpion",
                HitPoints = 15,
                MaxHitPoints = 15,
                MinDamage = 3,
                MaxDamage = 5,
                Experience = 20,
                Gold = 0,
                WanderRange = 5,
                EncounterClass = EncounterType.Insect,
                LootClass = LootType.Insect
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "whitespider",
                HitPoints = 15,
                MaxHitPoints = 15,
                MinDamage = 3,
                MaxDamage = 5,
                Experience = 20,
                Gold = 0,
                WanderRange = 5,
                EncounterClass = EncounterType.Insect,
                LootClass = LootType.Insect
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 7,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 5,
                Article = "a",
                Name = "lich",
                HitPoints = 35,
                MaxHitPoints = 55,
                MinDamage = 6,
                MaxDamage = 18,
                Experience = 220,
                Gold = 150,
                WanderRange = 2,
                EncounterClass = EncounterType.RareUndead,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 11,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 8,
                Article = "a",
                Name = "blackdragon",
                HitPoints = 55,
                MaxHitPoints = 85,
                MinDamage = 8,
                MaxDamage = 24,
                Experience = 320,
                Gold = 200,
                WanderRange = 2,
                Follows = 1,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.Exquisite
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 11,
                Alignment = AlignmentType.LawfulEvil,
                ArmorClass = 8,
                Article = "a",
                Name = "greendragon",
                HitPoints = 45,
                MaxHitPoints = 65,
                MinDamage = 6,
                MaxDamage = 18,
                Experience = 220,
                Gold = 120,
                WanderRange = 4,
                Follows = 1,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.Exquisite
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 11,
                Alignment = AlignmentType.LawfulGood,
                ArmorClass = 8,
                Article = "a",
                Name = "whitedragon",
                HitPoints = 50,
                MaxHitPoints = 70,
                MinDamage = 6,
                MaxDamage = 18,
                Experience = 340,
                Gold = 170,
                WanderRange = 2,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.Exquisite
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 11,
                Alignment = AlignmentType.LawfulGood,
                ArmorClass = 8,
                Article = "a",
                Name = "golddragon",
                HitPoints = 75,
                MaxHitPoints = 105,
                MinDamage = 12,
                MaxDamage = 36,
                Experience = 620,
                Gold = 300,
                WanderRange = 3,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.Exquisite
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 11,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 8,
                Article = "a",
                Name = "golem",
                HitPoints = 35,
                MaxHitPoints = 45,
                MinDamage = 6,
                MaxDamage = 10,
                Experience = 120,
                Gold = 30,
                WanderRange = 2,
                EncounterClass = EncounterType.Demonic,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 3,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 4,
                Article = "a",
                Name = "ghost",
                HitPoints = 12,
                MaxHitPoints = 14,
                MinDamage = 1,
                MaxDamage = 8,
                Experience = 15,
                Gold = 5,
                WanderRange = 2,
                Follows = 1,
                EncounterClass = EncounterType.Undead,
                LootClass = LootType.Common,
            });

            return NPCs;
        }

        public List<Item> LoadItems()
        {
            List<Item> Items = new List<Item>();

            var id = 1;

            Items.Add(new Item()
            {
                ID = id++,
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
                ID = id++,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "staff",
                Value = 10,
                MinDamage = 1,
                MaxDamage = 4,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "dagger",
                Value = 5,
                MinDamage = 1,
                MaxDamage = 6,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "mace",
                Value = 7,
                MinDamage = 2,
                MaxDamage = 6,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "longsword",
                Value = 10,
                MinDamage = 1,
                MaxDamage = 8,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "two-handed sword",
                Value = 15,
                MinDamage = 3,
                MaxDamage = 18,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Armor,
                Article = "a",
                Name = "leather armor",
                Value = 10,
                ArmorClass = 3,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Armor,
                Article = "a",
                Name = "chainmail",
                Value = 10,
                ArmorClass = 4,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Armor,
                Article = "a",
                Name = "buckler shield",
                Value = 10,
                ArmorClass = 2,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Treasure,
                Article = "a",
                Name = "chest",
                Value = 1000,
            });

            Items.Add(new Item()
            {
                ID = id++,
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

            var id = 1;

            Effects.Add(new Effect()
            {
                ID = id++,
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
                ID = id++,
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
                ID = id++,
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

            var id = 1;

            Quests.Add(new Quest()
            {
                ID = id++,
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
