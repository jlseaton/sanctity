using Game.Core;

namespace Game.Realm
{
    public class DataManager
    {
        public Dictionary<int, int> LevelLookup = new Dictionary<int, int>();

        public DataManager()
        {
            LoadLevelLookup();
        }

        private void LoadLevelLookup()
        {
            int level = 1;
            LevelLookup.Add(level++,300);
            LevelLookup.Add(level++, 900);
            LevelLookup.Add(level++, 2700);
            LevelLookup.Add(level++, 6500);
            LevelLookup.Add(level++, 4000);
            LevelLookup.Add(level++, 23000);
            LevelLookup.Add(level++, 34000);
            LevelLookup.Add(level++, 48000);
            LevelLookup.Add(level++, 64000);
            LevelLookup.Add(level++, 85000);
            LevelLookup.Add(level++, 00000);
            LevelLookup.Add(level++, 20000);
            LevelLookup.Add(level++, 40000);
            LevelLookup.Add(level++, 65000);
            LevelLookup.Add(level++, 95000);
            LevelLookup.Add(level++, 225000);
            LevelLookup.Add(level++, 265000);
            LevelLookup.Add(level++, 305000);
            LevelLookup.Add(level++, 355000);
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
                        Name = "Entrance",
                        Text = "You are standing at the entrance to the Dungeon Lab.",
                        Tile2ID = "stairsup",
                        East = 2,
                        Up = new Loc() { AreaID = 0, HexID = 1 }, // Leads back to tavern
                    },
                });

                Hexes.Add(new Hex()
                {
                    ID = 2,
                    Tile = new Tile()
                    {
                        Name = "Birch Tree",
                        Tile2ID = "redmushrooms",
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
                        Name = "Cemetary Gate",
                        Tile2ID = "metalgate",
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
                        Text = "You are standing in a dank tomb. Before you lies a mysterious gate.",
                        Tile2ID = "stoneportal",
                        West = 3,
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
                        Text = "You smell smoke and a foul odor in the air. This is folly.",
                        Tile2ID = "goldchest",
                        East = 6,
                    },
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.DragonKind },
                });

                Hexes.Add(new Hex()
                {
                    ID = 6,
                    Tile = new Tile()
                    {
                        Name = "Gray Stone",
                        Tile2ID = "stonebridge",
                        South = 10,
                        East = 7,
                        West = 5,
                    },
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.Common },
                });

                Hexes.Add(new Hex()
                {
                    ID = 7,
                    Tile = new Tile()
                    {
                        Name = "Gazebo",
                        Tile2ID = "gazebo",
                        SoundID = "toads1",
                        North = 3,
                        South = 11,
                        East = 8,
                        West = 6,
                    },
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.VeryRare },
                });

                Hexes.Add(new Hex()
                {
                    ID = 8,
                    Tile = new Tile()
                    {
                        Name = "Animal Kingdom",
                        Tile2ID = "birchtree",
                        SoundID = "birds1",
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
                        Text = "You are standing in the presence of pure evil itself.",
                        Tile2ID = "knightstatue",
                        East = 10,
                    },
                    PermanentNPCs = new List<int>() { 1 },
                });

                Hexes.Add(new Hex()
                {
                    ID = 10,
                    Tile = new Tile()
                    {
                        Name = "Ocean",
                        Tile1ID = "water7",
                        Tile2ID = "floatingpyramid",
                        North = 6,
                        East = 11,
                        West = 9,
                    },
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.Aquatic },
                });

                Hexes.Add(new Hex()
                {
                    ID = 11,
                    Tile = new Tile()
                    {
                        Name = "Stables",
                        Tile1ID = "grassmud1",
                        Tile2ID = "brownhorse",
                        North = 7,
                        East = 12,
                        West = 10,
                    },
                    RandomNPCsMax = 5,
                    RandomNPCs = new List<EncounterType>() { EncounterType.VeryRare },
                });

                Hexes.Add(new Hex()
                {
                    ID = 12,
                    Name = "Stairway to the Unknown",
                    Tile = new Tile()
                    {
                        Text = "You are standing at the top of a winding staircase which leads down into utter darkness. You cannot make this descent.. yet.",
                        Tile2ID = "stairsup",
                        North = 8,
                        West = 11,
                        //Down = new Loc() { AreaID = 1, HexID = 12 }, // Leads back to same spot
                    },
                    PermanentNPCs = new List<int>() { 10 },
                });

                var TavernHexes = new List<Hex>();
                TavernHexes.Add(new Hex()
                {
                    ID = 1,
                    Name = "Umbra Tavern",
                    Tile = new Tile()
                    {
                        Tile2ID = "woodentable2",
                        SoundID = "tavernnoise1",
                        Text = "You are in a dimly lit tavern, where adventurers meet before venturting forth. A dark stairwell lead to the Dungeon below.",
                        Down = new Loc() { AreaID = 1, HexID = 1 }, // Leads to the dungeon
                    },
                });

                Areas.Add(new Area()
                {
                    ID = 0,
                    Title = "Umbra Tavern",
                    Height = 1,
                    Width = 1,
                    Depth = 1,
                    StartX = 0,
                    StartY = 0,
                    Hexes = TavernHexes,
                });

                Areas.Add(new Area()
                {
                    ID = 1,
                    Title = "Dungeon",
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
                AccountType = AccountType.DungeonMaster,
                Type = EntityType.Hero,
                Race = RaceType.Human,
                Class = ClassType.Barbarian,
                Alignment = AlignmentType.Neutral,
                Origin = "Planar",
                Diety = "Bhaal",
                Name = "Hoxore",
                Surname = "the Cranky",
                Age = 28,
                Level = 15,
                Strength = 19,
                Dexterity = 15,
                Constitution = 15,
                Intelligence = 10,
                Wisdom = 8,
                Luck = 10,
                HPs = 220,
                MaxHPs = 220,
                Experience = 95500,
                Gold = 250,
                Stealth = 25,
                ArmorClass = 10,
                LootClass = LootType.None,
                MainHandID = 1,
                ImageName = "malehumanbarbarianhero",
                Bio = "An extremely powerful character primarily used for testing dungeons.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                AccountType = AccountType.DungeonMaster,
                Type = EntityType.PC,
                Gender = GenderType.Female,
                Race = RaceType.HalfElf,
                Class = ClassType.Bard,
                Alignment = AlignmentType.ChaoticGood,
                Name = "Lexanna",
                Surname = "the Strange",
                Age = 15,
                Level = 1,
                Strength = 11,
                Dexterity = 14,
                Constitution = 12,
                Intelligence = 16,
                Wisdom = 12,
                Luck = 14,
                HPs = 30,
                MaxHPs = 30,
                MPs = 35,
                MaxMPs = 35,
                Experience = 0,
                Gold = 50,
                Stealth = 25,
                LootClass = LootType.None,
                MainHandID = 4,
                ImageName = "femaleelfredhead",
                Bio = "Lexanna has a strangely other wordly look about her.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                AccountType = AccountType.DungeonMaster,
                Type = EntityType.PC,
                Gender = GenderType.Female,
                Race = RaceType.HalfElf,
                Class = ClassType.Rogue,
                Alignment = AlignmentType.LawfulGood,
                Name = "Caitlanna",
                Surname = "the Surly",
                Age = 17,
                Level = 1,
                Strength = 15,
                Dexterity = 12,
                Constitution = 15,
                Intelligence = 12,
                Wisdom = 15,
                Luck = 14,
                HPs = 60,
                MaxHPs = 60,
                MPs = 25,
                MaxMPs = 25,
                Experience = 0,
                Gold = 50,
                Stealth = 40,
                LootClass = LootType.None,
                MainHandID = 3,
                ImageName = "femalehumandruid",
                Bio = "Caitlanna fights for the weak and loves her Bella.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                AccountType = AccountType.DungeonMaster,
                Type = EntityType.PC,
                Race = RaceType.Dwarven,
                Class = ClassType.Fighter,
                Alignment = AlignmentType.Neutral,
                Name = "Owlshonor",
                Surname = "the Strong",
                Age = 17,
                Level = 1,
                Strength = 18,
                Dexterity = 15,
                Constitution = 15,
                Intelligence = 10,
                Wisdom = 8,
                Luck = 10,
                HPs = 55,
                MaxHPs = 55,
                Experience = 0,
                Gold = 50,
                Stealth = 15,
                LootClass = LootType.None,
                MainHandID = 5,
                ImageName = "maledwarffighter",
                Bio = "Me big and strong.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                AccountType = AccountType.DungeonMaster,
                Type = EntityType.PC,
                Race = RaceType.Halfling,
                Class = ClassType.Druid,
                Alignment = AlignmentType.Neutral,
                Name = "Gnorm",
                Surname = "Lyle the Quick",
                Age = 17,
                Level = 1,
                Strength = 11,
                Dexterity = 12,
                Constitution = 13,
                Intelligence = 12,
                Wisdom = 15,
                Luck = 15,
                HPs = 40,
                MaxHPs = 40,
                MPs = 25,
                MaxMPs = 25,
                Experience = 0,
                Gold = 50,
                Stealth = 33,
                LootClass = LootType.None,
                MainHandID = 2,
                ImageName = "malehumandruid",
                Bio = "Josh the Wasp.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                AccountType = AccountType.DungeonMaster,
                Type = EntityType.PC,
                Race = RaceType.Elf,
                Class = ClassType.Sorcerer,
                Alignment = AlignmentType.ChaoticEvil,
                Name = "Natillah",
                Surname = "the Well Endowed",
                Age = 24,
                Level = 1,
                Strength = 8,
                Dexterity = 14,
                Constitution = 10,
                Intelligence = 18,
                Wisdom = 12,
                Luck = 10,
                HPs = 25,
                MaxHPs = 25,
                MPs = 40,
                MaxMPs = 40,
                Experience = 0,
                Gold = 50,
                Stealth = 25,
                LootClass = LootType.None,
                MainHandID = 2,
                ImageName = "femalehumanblond",
                Bio = "She can defeat you with just a look.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                AccountType = AccountType.DungeonMaster,
                Type = EntityType.PC,
                Race = RaceType.Human,
                Class = ClassType.Wizard,
                Alignment = AlignmentType.ChaoticGood,
                Name = "Faerune",
                Surname = "the Furious",
                Age = 25,
                Level = 1,
                Strength = 13,
                Dexterity = 12,
                Constitution = 12,
                Intelligence = 14,
                Wisdom = 14,
                Luck = 16,
                HPs = 35,
                MaxHPs = 35,
                MPs = 15,
                MaxMPs = 15,
                Experience = 0,
                Gold = 50,
                Stealth = 20,
                LootClass = LootType.None,
                MainHandID = 4,
                ImageName = "malehumanwizard",
                Bio = "He has crispy furious fingers.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                AccountType = AccountType.DungeonMaster,
                Type = EntityType.PC,
                Gender = GenderType.Male,
                Race = RaceType.Elf,
                Class = ClassType.Ranger,
                Alignment = AlignmentType.ChaoticGood,
                Name = "Tractive",
                Surname = "the Brave",
                Age = 21,
                Level = 1,
                Strength = 13,
                Dexterity = 14,
                Constitution = 12,
                Intelligence = 11,
                Wisdom = 13,
                Luck = 12,
                HPs = 35,
                MaxHPs = 35,
                MPs = 15,
                MaxMPs = 15,
                Experience = 0,
                Gold = 25,
                Stealth = 33,
                LootClass = LootType.None,
                MainHandID = 7,
                ImageName = "malehumanranger",
                Bio = "Hunting in the woods is what he does best.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Race = RaceType.Human,
                Class = ClassType.Paladin,
                Alignment = AlignmentType.LawfulGood,
                Name = "Derwin",
                Surname = "the Just",
                Age = 23,
                Level = 1,
                Strength = 14,
                Dexterity = 18,
                Constitution = 12,
                Intelligence = 11,
                Wisdom = 10,
                Luck = 15,
                HPs = 30,
                MaxHPs = 30,
                Experience = 0,
                Gold = 50,
                Stealth = 10,
                LootClass = LootType.None,
                MainHandID = 6,
                ImageName = "whiteknightpaladin",
                Bio = "Always ready to fight with passion.",
            });

            Players.Add(new PC()
            {
                ID = id++,
                Type = EntityType.PC,
                Gender = GenderType.Female,
                Race = RaceType.Gnome,
                Class = ClassType.Cleric,
                Alignment = AlignmentType.LawfulGood,
                Name = "Chella",
                Surname = "the Bitey",
                Age = 14,
                Level = 1,
                Strength = 11,
                Dexterity = 14,
                Constitution = 12,
                Intelligence = 11,
                Wisdom = 15,
                Luck = 14,
                HPs = 20,
                MaxHPs = 20,
                MPs = 25,
                MaxMPs = 25,
                Experience = 0,
                Gold = 15,
                Stealth = 25,
                LootClass = LootType.None,
                MainHandID = 2,
                ImageName = "badger",
                Bio = "She'll bite your face off. Guinea pigs Charli and Bella put together into one big killing machine.",
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

            int id = 10000;

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
                ImageName = "demogorgon",
                HPs = 120,
                MaxHPs = 120,
                MPs = 50,
                MaxMPs = 50,
                HPRegen = 5,
                MPRegen = 5,
                MinDamage = 12,
                MaxDamage = 72,
                Experience = 5000,
                Gold = 500,
                Mood = MoodType.Aggressive,
                EncounterClass = EncounterType.Unique,
                LootClass = LootType.Unique,
                Inventory = demoInven,
                Skills = new List<int>() { 1 },
                Bio = "A sense of utter fear strikes you as you gaze upon this horrible apparation.",
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                QuestID = 1,
                Origin = "Valinor",
                Attackable = false,
                Type = EntityType.NPC,
                Race = RaceType.Elf,
                Gender = GenderType.Female,
                Size = SizeType.Medium,
                Speed = 80,
                Level = 18,
                Alignment = AlignmentType.LawfulGood,
                ArmorClass = 50,
                Article = "a",
                Name = "elfqueen",
                ImageName = "elfqueen",
                HPs = 120,
                MaxHPs = 120,
                MPs = 50,
                MaxMPs = 50,
                MinDamage = 12,
                MaxDamage = 72,
                Experience = 5000,
                Gold = 500,
                Mood = MoodType.Normal,
                EncounterClass = EncounterType.Unique,
                LootClass = LootType.Unique,
                Skills = new List<int>() { 1 },
                Bio = "A sense of overwhelming calm and peacefulness comes over you as gaze upon this being.",
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                QuestID = 1,
                Origin = "Valinor",
                Attackable = false,
                Type = EntityType.NPC,
                Race = RaceType.Elf,
                Gender = GenderType.Male,
                Size = SizeType.Medium,
                Speed = 80,
                Level = 18,
                Alignment = AlignmentType.LawfulGood,
                ArmorClass = 50,
                Article = "a",
                Name = "humanking",
                ImageName = "humanking",
                HPs = 120,
                MaxHPs = 120,
                MPs = 50,
                MaxMPs = 50,
                MinDamage = 12,
                MaxDamage = 72,
                Experience = 5000,
                Gold = 500,
                Mood = MoodType.Normal,
                EncounterClass = EncounterType.Unique,
                LootClass = LootType.Unique,
                Skills = new List<int>() { 1 },
                Bio = "Although out of shape now, you can tell this king was once a formidable warrior.",
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                QuestID = 1,
                Origin = "Valinor",
                Attackable = false,
                Type = EntityType.NPC,
                Race = RaceType.Elf,
                Size = SizeType.Medium,
                Speed = 80,
                Level = 18,
                Alignment = AlignmentType.LawfulGood,
                ArmorClass = 50,
                Article = "a",
                Name = "pinkprincess",
                ImageName = "pinkprincess",
                HPs = 120,
                MaxHPs = 120,
                MPs = 50,
                MaxMPs = 50,
                MinDamage = 12,
                MaxDamage = 72,
                Experience = 5000,
                Gold = 500,
                Mood = MoodType.Normal,
                EncounterClass = EncounterType.Unique,
                LootClass = LootType.Unique,
                Skills = new List<int>() { 1 },
                Bio = "This is the strangest person you have ever seen, princess or not.",
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Race = RaceType.Animal,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "brownbear",
                ImageName = "brownbear",
                HPs = 15,
                MaxHPs = 15,
                MinDamage = 3,
                MaxDamage = 5,
                Experience = 20,
                Gold = 0,
                WanderRange = 5,
                Follows = 30,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.Animal
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 5,
                Race = RaceType.Insect,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "blackspider",
                ImageName = "blackspider",
                HPs = 20,
                MaxHPs = 20,
                MinDamage = 5,
                MaxDamage = 8,
                Experience = 30,
                Gold = 0,
                WanderRange = 5,
                Follows = 1,
                EncounterClass = EncounterType.Insect,
                LootClass = LootType.Insect
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 3,
                Race = RaceType.Insect,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 1,
                Article = "a",
                Name = "brownspider",
                ImageName = "brownspider",
                HPs = 10,
                MaxHPs = 10,
                MinDamage = 3,
                MaxDamage = 5,
                Experience = 20,
                Gold = 0,
                WanderRange = 1,
                Follows = 10,
                Skills = new List<int>() { 2, 3 },
                EncounterClass = EncounterType.Insect,
                LootClass = LootType.Insect,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 5,
                Race = RaceType.Insect,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 2,
                Article = "a",
                Name = "stagbeetle",
                ImageName = "stagbeetle",
                HPs = 8,
                MaxHPs = 8,
                MinDamage = 1,
                MaxDamage = 4,
                Experience = 10,
                Gold = 0,
                WanderRange = 1,
                Follows = 5,
                EncounterClass = EncounterType.Insect,
                LootClass = LootType.Insect
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 3,
                Race = RaceType.Undead,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 3,
                Article = "a",
                Name = "skeleton",
                ImageName = "skeleton",
                HPs = 10,
                MaxHPs = 10,
                MinDamage = 1,
                MaxDamage = 6,
                Experience = 10,
                Gold = 5,
                WanderRange = 5,
                Follows = 20,
                EncounterClass = EncounterType.Undead,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 3,
                Race = RaceType.Undead,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 3,
                Article = "a",
                Name = "malezombie",
                ImageName = "malezombie",
                HPs = 10,
                MaxHPs = 10,
                MinDamage = 1,
                MaxDamage = 6,
                Experience = 10,
                Gold = 5,
                WanderRange = 5,
                Follows = 20,
                EncounterClass = EncounterType.Undead,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 3,
                Race = RaceType.Undead,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 2,
                Article = "a",
                Name = "femalezombie",
                ImageName = "femalezombie",
                HPs = 8,
                MaxHPs = 8,
                MinDamage = 1,
                MaxDamage = 4,
                Experience = 8,
                Gold = 5,
                WanderRange = 5,
                Follows = 20,
                EncounterClass = EncounterType.Undead,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 3,
                Race = RaceType.Undead,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 3,
                Article = "a",
                Name = "skeletonpirate",
                ImageName = "skeletonpirate",
                HPs = 10,
                MaxHPs = 10,
                MinDamage = 1,
                MaxDamage = 6,
                Experience = 10,
                Gold = 5,
                WanderRange = 5,
                Follows = 20,
                EncounterClass = EncounterType.Undead,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 1,
                Race = RaceType.Animal,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 1,
                Article = "a",
                Name = "deer",
                ImageName = "deer",
                HPs = 5,
                MaxHPs = 5,
                MinDamage = 1,
                MaxDamage = 4,
                Experience = 8,
                Gold = 0,
                WanderRange = 2,
                Follows = 20,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.Animal,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 1,
                Race = RaceType.Animal,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 1,
                Article = "a",
                Name = "graywolf",
                ImageName = "graywolf",
                HPs = 15,
                MaxHPs = 15,
                MinDamage = 1,
                MaxDamage = 6,
                Experience = 15,
                Gold = 0,
                WanderRange = 2,
                Follows = 20,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.Animal,
            });

            var chest = LoadItems().Where(i => i.Name == "goldchest").Single();
            var dragonInven = new List<Item>();
            dragonInven.Add(chest);

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Dragon,
                Level = 15,
                Size = SizeType.Monstrous,
                Alignment = AlignmentType.LawfulEvil,
                ArmorClass = 25,
                Article = "a",
                Name = "reddragon",
                ImageName = "reddragon",
                HPs = 75,
                MaxHPs = 75,
                MinDamage = 3,
                MaxDamage = 18,
                Experience = 1500,
                Gold = 750,
                WanderRange = 10,
                Follows = 50,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.VeryRare,
                Skills = new List<int>() { 2, 3 },
                Inventory = dragonInven,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Dragon,
                Level = 11,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 8,
                Article = "a",
                Name = "blackdragon",
                ImageName = "blackdragon",
                HPs = 85,
                MaxHPs = 85,
                MinDamage = 8,
                MaxDamage = 24,
                Experience = 320,
                Gold = 200,
                WanderRange = 2,
                Follows = 50,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.Exquisite,
                Skills = new List<int>() { 1, 2, 3 },
                Inventory = dragonInven,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 11,
                Race = RaceType.Dragon,
                Alignment = AlignmentType.LawfulEvil,
                ArmorClass = 8,
                Article = "a",
                Name = "greendragon",
                ImageName = "greendragon",
                HPs = 65,
                MaxHPs = 65,
                MinDamage = 6,
                MaxDamage = 18,
                Experience = 220,
                Gold = 120,
                WanderRange = 4,
                Follows = 40,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.Exquisite,
                Skills = new List<int>() { 2, 3, 4 },
                Inventory = dragonInven,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 11,
                Race = RaceType.Dragon,
                Alignment = AlignmentType.LawfulEvil,
                ArmorClass = 8,
                Article = "a",
                Name = "bronzedragon",
                ImageName = "bronzedragon",
                HPs = 65,
                MaxHPs = 65,
                MinDamage = 6,
                MaxDamage = 18,
                Experience = 220,
                Gold = 120,
                WanderRange = 4,
                Follows = 40,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.Exquisite,
                Skills = new List<int>() { 2, 3, 4 },
                Inventory = dragonInven,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 11,
                Race = RaceType.Dragon,
                Alignment = AlignmentType.LawfulEvil,
                ArmorClass = 8,
                Article = "a",
                Name = "copperdragon",
                ImageName = "copperdragon",
                HPs = 65,
                MaxHPs = 65,
                MinDamage = 6,
                MaxDamage = 18,
                Experience = 220,
                Gold = 120,
                WanderRange = 4,
                Follows = 40,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.Exquisite,
                Skills = new List<int>() { 2, 3, 4 },
                Inventory = dragonInven,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Dragon,
                Level = 11,
                Alignment = AlignmentType.LawfulGood,
                ArmorClass = 8,
                Article = "a",
                Name = "prismaticdragon",
                ImageName = "prismaticdragon",
                HPs = 105,
                MaxHPs = 105,
                MinDamage = 12,
                MaxDamage = 36,
                Experience = 620,
                Gold = 300,
                WanderRange = 3,
                EncounterClass = EncounterType.DragonKind,
                LootClass = LootType.Exquisite,
                Skills = new List<int>() { 2, 3 },
                Inventory = dragonInven,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 7,
                Race = RaceType.Human,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 2,
                Article = "a",
                Name = "necromancer",
                ImageName = "necromancer",
                HPs = 45,
                MaxHPs = 45,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 640,
                Gold = 250,
                WanderRange = 5,
                Follows = 30,
                EncounterClass = EncounterType.VeryRare,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 6,
                Race = RaceType.Human,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 2,
                Article = "a",
                Name = "sorceress",
                ImageName = "sorceress",
                HPs = 40,
                MaxHPs = 40,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 540,
                Gold = 150,
                WanderRange = 3,
                Follows = 20,
                EncounterClass = EncounterType.VeryRare,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 2,
                Race = RaceType.Planar,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 2,
                Article = "a",
                Name = "voidfiend",
                ImageName = "voidfiend",
                HPs = 45,
                MaxHPs = 45,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 640,
                Gold = 250,
                WanderRange = 5,
                Follows = 50,
                EncounterClass = EncounterType.VeryRare,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Animal,
                Level = 5,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 4,
                Article = "a",
                Name = "ape",
                ImageName = "ape",
                HPs = 25,
                MaxHPs = 25,
                MinDamage = 2,
                MaxDamage = 10,
                Experience = 60,
                Gold = 5,
                WanderRange = 3,
                Follows = 20,
                EncounterClass = EncounterType.Animal,
                LootClass = LootType.AnimalRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 6,
                Race = RaceType.Undead,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 5,
                Article = "a",
                Name = "reaper",
                ImageName = "reaper",
                HPs = 35,
                MaxHPs = 35,
                MinDamage = 4,
                MaxDamage = 8,
                Experience = 130,
                Gold = 50,
                WanderRange = 2,
                Follows = 20,
                EncounterClass = EncounterType.RareUndead,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Level = 10,
                Race = RaceType.Undead,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 5,
                Article = "a",
                Name = "lich",
                ImageName = "lich",
                HPs = 75,
                MaxHPs = 75,
                MinDamage = 6,
                MaxDamage = 12,
                Experience = 230,
                Gold = 150,
                WanderRange = 2,
                Follows = 20,
                EncounterClass = EncounterType.RareUndead,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.HalfOrc,
                Level = 7,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 8,
                Article = "a",
                Name = "largetroll",
                ImageName = "largetroll",
                HPs = 20,
                MaxHPs = 20,
                MinDamage = 4,
                MaxDamage = 8,
                Experience = 60,
                Gold = 10,
                WanderRange = 2,
                Follows = 15,
                EncounterClass = EncounterType.Common,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.HalfOrc,
                Level = 5,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 8,
                Article = "a",
                Name = "smalltroll",
                ImageName = "smalltroll",
                HPs = 10,
                MaxHPs = 10,
                MinDamage = 2,
                MaxDamage = 4,
                Experience = 30,
                Gold = 5,
                WanderRange = 2,
                Follows = 15,
                EncounterClass = EncounterType.Common,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.HalfOrc,
                Level = 9,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 8,
                Article = "a",
                Name = "cavetroll",
                ImageName = "cavetroll",
                HPs = 35,
                MaxHPs = 35,
                MinDamage = 6,
                MaxDamage = 10,
                Experience = 100,
                Gold = 20,
                WanderRange = 2,
                Follows = 10,
                EncounterClass = EncounterType.Common,
                LootClass = LootType.Common
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Goblin,
                Level = 3,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 5,
                Article = "a",
                Name = "goblin",
                ImageName = "goblin",
                HPs = 8,
                MaxHPs = 8,
                MinDamage = 2,
                MaxDamage = 4,
                Experience = 20,
                Gold = 3,
                WanderRange = 2,
                Follows = 15,
                EncounterClass = EncounterType.Common,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Goblin,
                Level = 3,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 5,
                Article = "a",
                Name = "goblinworker",
                ImageName = "goblinworker",
                HPs = 8,
                MaxHPs = 8,
                MinDamage = 2,
                MaxDamage = 4,
                Experience = 20,
                Gold = 3,
                WanderRange = 2,
                Follows = 15,
                EncounterClass = EncounterType.Common,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Goblin,
                Level = 4,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 6,
                Article = "a",
                Name = "goblinleader",
                ImageName = "goblinleader",
                HPs = 12,
                MaxHPs = 12,
                MinDamage = 2,
                MaxDamage = 6,
                Experience = 30,
                Gold = 5,
                WanderRange = 2,
                Follows = 25,
                EncounterClass = EncounterType.Common,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Planar,
                Level = 8,
                Alignment = AlignmentType.ChaoticEvil,
                ArmorClass = 8,
                Article = "a",
                Name = "werewolf",
                ImageName = "werewolf",
                HPs = 25,
                MaxHPs = 25,
                MinDamage = 3,
                MaxDamage = 9,
                Experience = 80,
                Gold = 15,
                WanderRange = 2,
                Follows = 30,
                EncounterClass = EncounterType.Demonic,
                LootClass = LootType.VeryRare
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Aquatic,
                Level = 1,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 3,
                Article = "a",
                Name = "seaotter",
                ImageName = "seaotter",
                HPs = 5,
                MaxHPs = 5,
                MinDamage = 1,
                MaxDamage = 4,
                Experience = 6,
                Gold = 0,
                WanderRange = 2,
                Follows = 10,
                EncounterClass = EncounterType.Aquatic,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Aquatic,
                Level = 2,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 3,
                Article = "a",
                Name = "redoctopus",
                ImageName = "redoctopus",
                HPs = 5,
                MaxHPs = 5,
                MinDamage = 1,
                MaxDamage = 4,
                Experience = 8,
                Gold = 0,
                WanderRange = 2,
                Follows = 10,
                EncounterClass = EncounterType.Aquatic,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Aquatic,
                Level = 3,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 4,
                Article = "a",
                Name = "greatwhite",
                ImageName = "greatwhite",
                HPs = 12,
                MaxHPs = 12,
                MinDamage = 2,
                MaxDamage = 6,
                Experience = 20,
                Gold = 0,
                WanderRange = 2,
                Follows = 20,
                EncounterClass = EncounterType.Aquatic,
                LootClass = LootType.Common,
            });

            NPCs.Add(new NPC()
            {
                ID = id++,
                Type = EntityType.NPC,
                Race = RaceType.Aquatic,
                Level = 4,
                Alignment = AlignmentType.Neutral,
                ArmorClass = 5,
                Article = "a",
                Name = "megaladon",
                ImageName = "megaladon",
                HPs = 18,
                MaxHPs = 18,
                MinDamage = 2,
                MaxDamage = 8,
                Experience = 30,
                Gold = 0,
                WanderRange = 2,
                Follows = 20,
                EncounterClass = EncounterType.Aquatic,
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
                Name = "vorpalsword",
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
                Name = "twohandedsword",
                Value = 15,
                MinDamage = 3,
                MaxDamage = 18,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Weapon,
                Article = "a",
                Name = "longbow",
                Value = 25,
                MinDamage = 4,
                MaxDamage = 8,
                Range = 3,
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
                Name = "goldchest",
                Value = 1000,
            });

            Items.Add(new Item()
            {
                ID = id++,
                Type = ItemType.Armor,
                Article = "a",
                Name = "glittering shield",
                Value = 1000,
                ArmorClass = 30,
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
                FizzleChance = 90,
                Verb = " beheads ",
            });

            Effects.Add(new Effect()
            {
                ID = id++,
                Name = "dragonfire",
                Type = EffectType.Damage,
                Damage = DamageType.Fire,
                MinDamage = 3,
                MaxDamage = 18,
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
                MaximumTargets = 6,
                FizzleChance = 75,
                Verb = " induces fear in ",
            });

            Effects.Add(new Effect()
            {
                ID = id++,
                Name = "poisoncloud",
                Type = EffectType.Poison,
                Damage = DamageType.Poison,
                MinDamage = 1,
                MaxDamage = 4,
                MaximumTargets = 6,
                FizzleChance = 75,
                Verb = " issues forth a poisonous cloud on ",
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
                Description = "A new beginning.",
                RewardText = "You have completed your first quest.",
                Experience = 10000,
                Gold = 10000,
                LootClass = LootType.Unique,
                SpecificLoot = new List<int>(1),
            });

            return Quests;
        }

        //TODO: Finish load/save PCs to files/db
        public void SavePCs()
        {

        }
    }
}
