namespace Game.Core
{
    public class Loc
    {
        public string RealmID { get; set; }
        public int AreaID { get; set; }
        public int HexID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public Loc Clone()
        {
            return (Loc)this.MemberwiseClone();
        }
    }

}
