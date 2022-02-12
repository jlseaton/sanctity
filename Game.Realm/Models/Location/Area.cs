using Game.Core;

namespace Game.Realm
{
    public class Area : Thing
    {
        public string Title { get; set; }
        public int SoundID { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }

        public int StartX { get; set; }
        public int StartY { get; set; }
        public int StartZ { get; set; }

        public List<Hex> Hexes { get; set; }
    }
}
