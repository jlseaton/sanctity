using Game.Core;
using System.Text;

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

        public List<PC> PCs = new List<PC>();
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

            StringBuilder players = new StringBuilder();
            StringBuilder npcs = new StringBuilder();
            StringBuilder items = new StringBuilder();

            bool playersFound = false;
            foreach (PC player in PCs)
            {
                if (player.ID != playerId)
                {
                    playersFound = true;
                    string corpse = String.Empty;
                    if (player.State == StateType.Dead)
                        corpse = "a corpse of ";
                    players.Append(corpse + player.FullName + ", ");
                }
            }

            if (playersFound)
            {
                players.Length -= 2; // Remove trailing comma
                desc += "\r\n" + "You see the following players here:\r\n" + players;
            }

            bool npcsFound = false;
            foreach (NPC npc in NPCs)
            {
                npcsFound = true;
                string corpse = String.Empty;
                if (npc.State == StateType.Dead)
                    corpse = "a corpse of ";
                npcs.Append(corpse + npc.FullName + ", ");
            }

            if (npcsFound)
            {
                npcs.Length -= 2; // Remove trailing comma
                desc += "\r\n" + "You also see here:\r\n" + npcs;
            }

            bool itemsFound = false;
            foreach (Item item in Items)
            {
                itemsFound = true;
                items.Append(item.FullName + ", ");
            }

            if (itemsFound)
            {
                items.Length -= 2; // Remove trailing comma
                desc += "\r\n" + "You find here:\r\n" + items;
            }

            return desc;
        }
    }
}
