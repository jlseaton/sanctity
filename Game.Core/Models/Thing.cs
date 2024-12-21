namespace Game.Core
{
    public class Thing
    {
        public string ID { get; set; }

        public string? TileID { get; set; }
        public string? QuestID { get; set; }

        public string? Article { get; set; }
        private string? _name;
        public string? Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                {
                    _name = value.Trim();
                    ID = value.Trim();
                }
            }
        }

        public string FullName
        {
            get
            {
                if (!String.IsNullOrEmpty(Article))
                {
                    return Article + " " + Name;
                }

                return Name;
            }
        }
        public string? ImageName { get; set; }
    }
}
