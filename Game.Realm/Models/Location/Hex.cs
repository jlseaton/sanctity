using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core;

namespace Game.Realm
{
    public class Hex : Thing
    {
        public int AreaID { get; set; }

        public int LockID { get; set; }
        public int LockLevel { get; set; }

        public List<int> PermanentNPCs = new List<int>();
        public List<EncounterType> RandomNPCs = new List<EncounterType>();
        public int RandomNPCsMax { get; set; }
        public int RandomEncounterChance { get; set; }

        public Tile Tile { get; set; }

        public List<PC> Players = new List<PC>();
        public List<NPC> NPCs = new List<NPC>();

        public List<Item> Items = new List<Item>();
        public List<Effect> Effects = new List<Effect>();

        public Hex()
        {
            Tile = new Tile();
        }

        public string GetDescription(int playerId)
        {
            string desc = "HexID #" + ID.ToString();

            if (!String.IsNullOrEmpty(Tile.Name))
            {
                desc += ": " + Tile.Name;
            }

            if (!String.IsNullOrEmpty(Tile.Text))
            {
                desc += "\r\n" + Tile.Text;
            }

            desc += "\r\n" + Tile.GetExits();

            string found = String.Empty;

            bool playersFound = false;
            foreach (PC player in Players)
            {
                if (player.ID != playerId)
                {
                    playersFound = true;
                    string corpse = String.Empty;
                    if (player.State == StateType.Dead)
                        corpse = "a corpse of ";
                    found += "\r\n" + corpse + player.FullName;
                }
            }

            foreach (NPC npc in NPCs)
            {
                string corpse = String.Empty;
                if (npc.State == StateType.Dead)
                    corpse = "a corpse of ";
                found += "\r\n" + corpse + npc.FullName;
            }

            foreach (Item item in Items)
            {
                found += "\r\n" + item.FullName;
            }

            if (playersFound || NPCs.Any() || Items.Any())
            {
                desc += "\r\n" + "You also see here:" + found;
            }

            return desc;
        }
    }
}
