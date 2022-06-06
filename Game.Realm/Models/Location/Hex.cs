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

            return desc;
        }
    }
}
