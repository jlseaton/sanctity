using System;

namespace Game.Core
{
    public class Thing
    {
        public int ID { get; set; }

        public int TileID { get; set; }
        public int QuestID { get; set; }

        public string Article { get; set; }
        public string Name { get; set; }
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
    }
}
