using Game.Core;
using System.Text;

namespace Game.Realm
{
    public class Hex : Thing
    {
        public string AreaID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SoundID { get; set; }
        public string? MusicID { get; set; }

        public bool Solid = false;
        public bool Transparent = true;
        public bool NoCombat = false;

        public int LockID { get; set; }
        public int LockLevel { get; set; }

        public int RandomNPCsMax { get; set; }
        public int RandomEncounterChance { get; set; }

        public List<int> PermanentNPCs = new List<int>();
        public List<EncounterType> RandomNPCs = new List<EncounterType>();

        public Tile Tile { get; set; }
        public List<Tile> Tiles = new List<Tile>();

        public List<PC> PCs = new List<PC>();
        public List<NPC> NPCs = new List<NPC>();

        public List<Item> Items = new List<Item>();
        public List<Effect> Effects = new List<Effect>();
        public Loc? Up { get; set; }
        public Loc? Down { get; set; }
        public Loc? Teleport { get; set; }

        public Hex()
        {
            Tile = new Tile() {
                Tile1ID = "grass1"
            };

            Tiles.Add(Tile);
        }

        public string GetDescription(bool includeTitle = false)
        {
            string desc = String.Empty;

            if (includeTitle && !String.IsNullOrEmpty(Title))
            {
                desc += Title;

                if (!String.IsNullOrEmpty(Description))
                {
                    desc += Environment.NewLine;
                }
            }

            if (!String.IsNullOrEmpty(Description))
            {
                desc += this.Description;
            }

            return desc;
        }

        public Hex Clone()
        {
            return (Hex)this.MemberwiseClone();
        }
    }
}
