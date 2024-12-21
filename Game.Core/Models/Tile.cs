namespace Game.Core
{
    public class Tile
    {
        public string? TileEffectID;
        public string? Tile1ID;
        public int Tile1Size { get; set; }
        public int Tile1Angle { get; set; }
        public int Tile1XOffset { get; set; }
        public int Tile1YOffset { get; set; }
        public string? Tile2ID;
        public int Tile2Size { get; set; }
        public int Tile2Angle { get; set; }
        public int Tile2XOffset { get; set; }
        public int Tile2YOffset { get; set; }
        public string? Tile3ID;
        public int Tile3Size { get; set; }
        public int Tile3Angle { get; set; }
        public int Tile3XOffset { get; set; }
        public int Tile3YOffset { get; set; }

        public Tile()
        {
            // WARNING: Setting string values here can really hurt performance
            Tile1Size = 100;
            Tile2Size = 100;
            Tile3Size = 100;
        }

        public Tile Clone()
        {
            return (Tile)this.MemberwiseClone();
        }
    }
}
